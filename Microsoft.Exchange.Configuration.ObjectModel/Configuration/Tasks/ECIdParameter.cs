using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public class ECIdParameter : IIdentityParameter
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x00020E0F File Offset: 0x0001F00F
		public ECIdParameter()
		{
			this.Identity = null;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00020E1E File Offset: 0x0001F01E
		public ECIdParameter(ObjectId id)
		{
			this.Initialize(id);
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00020E2D File Offset: 0x0001F02D
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00020E35 File Offset: 0x0001F035
		internal string RawIdentity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00020E3D File Offset: 0x0001F03D
		public static ECIdParameter Parse(string input)
		{
			return new ECIdParameter(EventCategoryIdentity.Parse(input));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00020E4A File Offset: 0x0001F04A
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00020E53 File Offset: 0x0001F053
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return null;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00020E6A File Offset: 0x0001F06A
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00020E74 File Offset: 0x0001F074
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00020E84 File Offset: 0x0001F084
		internal virtual void Initialize(ObjectId id)
		{
			this.Identity = (id as EventCategoryIdentity);
			if (this.Identity.EventSource != null && !this.Identity.EventSource.StartsWith("*") && !this.Identity.EventSource.EndsWith("*"))
			{
				bool flag = false;
				foreach (string strB in EventCategoryIdentity.EventSources)
				{
					if (string.Compare(this.Identity.EventSource, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new ArgumentException(Strings.ErrorInvalidIdentity(this.Identity.EventSource), "Identity.EventSource");
				}
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00020F30 File Offset: 0x0001F130
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
			return session.FindPaged<T>((optionalData == null) ? null : optionalData.AdditionalFilter, this.Identity, false, null, 0);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00020F64 File Offset: 0x0001F164
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00020F7C File Offset: 0x0001F17C
		internal bool IsUnique()
		{
			return this.Identity.Category != null && null != this.Identity.EventSource;
		}

		// Token: 0x04000275 RID: 629
		private EventCategoryIdentity Identity;
	}
}
