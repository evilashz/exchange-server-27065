using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D2 RID: 210
	internal class ConditionalRegistrationCache
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00023827 File Offset: 0x00021A27
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0002382E File Offset: 0x00021A2E
		public static Action<ConditionalRegistration> TESTHOOK_PersistRegistration { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00023836 File Offset: 0x00021A36
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0002383D File Offset: 0x00021A3D
		public static Action<string> TESTHOOK_DeleteRegistration { get; set; }

		// Token: 0x060008FD RID: 2301 RVA: 0x00023845 File Offset: 0x00021A45
		private ConditionalRegistrationCache()
		{
			this.cache = new ExactTimeoutCache<string, BaseConditionalRegistration>(new RemoveItemDelegate<string, BaseConditionalRegistration>(this.HandleRemoveRegistration), null, null, 1000, false);
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00023882 File Offset: 0x00021A82
		public int Count
		{
			get
			{
				return this.cache.Count;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00023890 File Offset: 0x00021A90
		public bool PropertyIsActive(PropertyDefinition propDef)
		{
			bool result;
			lock (this.activePropertiesLock)
			{
				int num = 0;
				this.activeProperties.TryGetValue(propDef, out num);
				result = (num > 0);
			}
			return result;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000238E4 File Offset: 0x00021AE4
		internal List<ConditionalResults> Evaluate(IReadOnlyPropertyBag propertyBag)
		{
			List<BaseConditionalRegistration> values = this.cache.Values;
			if (values == null || values.Count == 0)
			{
				return null;
			}
			List<ConditionalResults> list = null;
			foreach (BaseConditionalRegistration baseConditionalRegistration in values)
			{
				ConditionalResults conditionalResults = baseConditionalRegistration.Evaluate(propertyBag);
				if (conditionalResults != null)
				{
					if (list == null)
					{
						list = new List<ConditionalResults>();
					}
					list.Add(conditionalResults);
					ConditionalRegistration conditionalRegistration = baseConditionalRegistration as ConditionalRegistration;
					if (conditionalRegistration != null)
					{
						int currentHits = conditionalRegistration.CurrentHits;
						if (currentHits >= conditionalRegistration.MaxHits)
						{
							ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "[ConditionalRegistrationCache.Evaluate] Removing entry '{0}' because current hits {1} exceeds MaxHits {2}.", conditionalRegistration.Cookie, currentHits, conditionalRegistration.MaxHits);
							this.cache.Remove(baseConditionalRegistration.Cookie);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000239C0 File Offset: 0x00021BC0
		internal void Register(ConditionalRegistration registration)
		{
			TimeSpan expiration = (ConditionalRegistrationCache.MaxActiveRegistrationTimeEntry.Value < registration.TimeToLive) ? ConditionalRegistrationCache.MaxActiveRegistrationTimeEntry.Value : registration.TimeToLive;
			if (this.cache.TryAddAbsolute(registration.Cookie, registration, expiration))
			{
				this.UpdateActiveProperties(registration, true);
				if (ConditionalRegistrationCache.TESTHOOK_PersistRegistration == null)
				{
					ConditionalRegistrationLog.Save(registration);
					return;
				}
				ConditionalRegistrationCache.TESTHOOK_PersistRegistration(registration);
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00023A30 File Offset: 0x00021C30
		private void UpdateActiveProperties(BaseConditionalRegistration newRegistration, bool adding)
		{
			lock (this.activePropertiesLock)
			{
				foreach (PropertyDefinition key in newRegistration.PropertiesToFetch)
				{
					int num;
					this.activeProperties.TryGetValue(key, out num);
					num += (adding ? 1 : -1);
					if (num <= 0)
					{
						num = 0;
					}
					this.activeProperties[key] = num;
				}
				foreach (PropertyDefinition key2 in newRegistration.QueryFilter.FilterProperties())
				{
					int num2;
					this.activeProperties.TryGetValue(key2, out num2);
					num2 += (adding ? 1 : -1);
					if (num2 <= 0)
					{
						num2 = 0;
					}
					this.activeProperties[key2] = num2;
				}
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00023B28 File Offset: 0x00021D28
		internal void Register(PersistentConditionalRegistration persistentRegistration)
		{
			if (this.cache.TryAddAbsolute(persistentRegistration.Cookie, persistentRegistration, DateTime.MaxValue))
			{
				this.UpdateActiveProperties(persistentRegistration, true);
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00023B68 File Offset: 0x00021D68
		internal void GetRegistrationMetadata(string userIdentity, out List<BaseConditionalRegistration> active, out List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata> hits)
		{
			if (string.IsNullOrEmpty(userIdentity))
			{
				active = this.cache.Values;
				hits = ConditionalRegistrationLog.GetHitsMetadata("");
				return;
			}
			active = (from s in this.cache.Values
			where s.User == userIdentity
			select s).ToList<BaseConditionalRegistration>();
			hits = ConditionalRegistrationLog.GetHitsMetadata(userIdentity);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00023BE0 File Offset: 0x00021DE0
		internal void GetRegistrationMetadata(string userIdentity, string cookie, out BaseConditionalRegistration reg, out ConditionalRegistrationLog.ConditionalRegistrationHitMetadata hit)
		{
			reg = this.GetRegistration(cookie);
			hit = ConditionalRegistrationLog.GetHitsForCookie(userIdentity, cookie);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00023BF5 File Offset: 0x00021DF5
		internal void Clear()
		{
			this.cache.Clear();
			this.activeProperties.Clear();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00023C10 File Offset: 0x00021E10
		internal bool Remove(string cookie)
		{
			if (ConditionalRegistrationCache.TESTHOOK_DeleteRegistration == null)
			{
				ConditionalRegistrationLog.DeleteRegistration(cookie);
			}
			else
			{
				ConditionalRegistrationCache.TESTHOOK_DeleteRegistration(cookie);
			}
			BaseConditionalRegistration baseConditionalRegistration = this.cache.Remove(cookie.ToString());
			if (baseConditionalRegistration != null)
			{
				this.UpdateActiveProperties(baseConditionalRegistration, false);
			}
			return baseConditionalRegistration != null;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00023C5B File Offset: 0x00021E5B
		internal List<string> GetAllKeys()
		{
			return this.cache.Keys;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00023C68 File Offset: 0x00021E68
		internal List<BaseConditionalRegistration> GetAllValues()
		{
			return this.cache.Values;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00023C78 File Offset: 0x00021E78
		internal BaseConditionalRegistration GetRegistration(string cookie)
		{
			BaseConditionalRegistration result;
			this.cache.TryGetValue(cookie.ToString(), out result);
			return result;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00023C9C File Offset: 0x00021E9C
		private void HandleRemoveRegistration(string key, BaseConditionalRegistration value, RemoveReason reason)
		{
			ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string, RemoveReason, string>((long)this.GetHashCode(), "[ConditionalRegistrationCache.HandleRemoveRegistration] Cookie: {0} was removed for reason {1}.  Description: '{2}'", key, reason, value.Description ?? "<NULL>");
			if (ConditionalRegistrationCache.TESTHOOK_DeleteRegistration == null)
			{
				ConditionalRegistrationLog.DeleteRegistration(key);
			}
			else
			{
				ConditionalRegistrationCache.TESTHOOK_DeleteRegistration(key);
			}
			this.UpdateActiveProperties(value, false);
			if (value.OnExpired != null)
			{
				value.OnExpired(value, reason);
			}
		}

		// Token: 0x04000433 RID: 1075
		private static TimeSpanAppSettingsEntry MaxActiveRegistrationTimeEntry = new TimeSpanAppSettingsEntry("MaxActiveRegistrationTime", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(1440.0), ExTraceGlobals.DiagnosticHandlersTracer);

		// Token: 0x04000434 RID: 1076
		public static readonly ConditionalRegistrationCache Singleton = new ConditionalRegistrationCache();

		// Token: 0x04000435 RID: 1077
		private ExactTimeoutCache<string, BaseConditionalRegistration> cache;

		// Token: 0x04000436 RID: 1078
		private object activePropertiesLock = new object();

		// Token: 0x04000437 RID: 1079
		private Dictionary<PropertyDefinition, int> activeProperties = new Dictionary<PropertyDefinition, int>();
	}
}
