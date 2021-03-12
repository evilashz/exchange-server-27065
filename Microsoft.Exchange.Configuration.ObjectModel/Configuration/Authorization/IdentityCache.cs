using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000242 RID: 578
	internal sealed class IdentityCache<T> where T : class
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x0004C5FC File Offset: 0x0004A7FC
		private IdentityCache()
		{
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0004C60F File Offset: 0x0004A80F
		public static IdentityCache<T> Current
		{
			get
			{
				return IdentityCache<T>.instance;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0004C618 File Offset: 0x0004A818
		private TimeoutCache<string, T> CachedData
		{
			get
			{
				if (this.cachedData == null)
				{
					lock (this.lockObject)
					{
						this.cachedData = (this.cachedData ?? new TimeoutCache<string, T>(20, 5000, false));
					}
				}
				return this.cachedData;
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0004C680 File Offset: 0x0004A880
		public bool Add(IIdentity identity, T data)
		{
			if (identity == null || data == null)
			{
				return false;
			}
			if (!identity.IsAuthenticated)
			{
				return false;
			}
			string text = IdentityCache<T>.CreateKey(identity);
			if (text == null)
			{
				return false;
			}
			this.CachedData.InsertAbsolute(text, data, AppSettings.Current.SidsCacheTimeoutInHours, new RemoveItemDelegate<string, T>(IdentityCache<T>.OnKeyToRemoveBudgetsCacheValueRemoved));
			return true;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0004C6D4 File Offset: 0x0004A8D4
		public bool TryGetValue(IIdentity identity, out T data)
		{
			data = default(T);
			if (identity == null)
			{
				return false;
			}
			string text = IdentityCache<T>.CreateKey(identity);
			return text != null && this.CachedData.TryGetValue(text, out data);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004C708 File Offset: 0x0004A908
		private static string CreateKey(IIdentity identity)
		{
			try
			{
				return identity.AuthenticationType + "|" + identity.GetSafeName(true);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004C748 File Offset: 0x0004A948
		private static void OnKeyToRemoveBudgetsCacheValueRemoved(string key, T value, RemoveReason reason)
		{
			if (reason != RemoveReason.Removed)
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug<string, RemoveReason>(0L, "{0} is removed from budgets dictionary after timeout. Remove reason = {1}", key, reason);
			}
		}

		// Token: 0x040005E4 RID: 1508
		private static readonly IdentityCache<T> instance = new IdentityCache<T>();

		// Token: 0x040005E5 RID: 1509
		private object lockObject = new object();

		// Token: 0x040005E6 RID: 1510
		private TimeoutCache<string, T> cachedData;
	}
}
