using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Providers;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class AlternateMailboxIdParameter : IIdentityParameter
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x0001E68C File Offset: 0x0001C88C
		public AlternateMailboxIdParameter()
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001E694 File Offset: 0x0001C894
		public AlternateMailboxIdParameter(AlternateMailboxObject am)
		{
			this.m_objectId = (AlternateMailboxObjectId)am.Identity;
			this.m_rawName = this.m_objectId.FullName;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		public AlternateMailboxIdParameter(ObjectId objId)
		{
			if (objId is AlternateMailboxObjectId)
			{
				this.m_objectId = (AlternateMailboxObjectId)objId;
				this.m_rawName = this.m_objectId.FullName;
				return;
			}
			if (objId is ADObjectId)
			{
				ADObjectId adobjectId = (ADObjectId)objId;
				this.m_objectId = new AlternateMailboxObjectId(string.Empty);
				this.m_objectId.UserId = new Guid?(adobjectId.ObjectGuid);
				this.m_objectId.UserName = (string.IsNullOrEmpty(adobjectId.Name) ? adobjectId.ObjectGuid.ToString() : adobjectId.Name);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001E762 File Offset: 0x0001C962
		public ObjectId ObjectId
		{
			get
			{
				return this.m_objectId;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001E76A File Offset: 0x0001C96A
		public string RawIdentity
		{
			get
			{
				return this.m_rawName;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001E772 File Offset: 0x0001C972
		internal string UserName
		{
			get
			{
				return this.m_objectId.UserName;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001E77F File Offset: 0x0001C97F
		internal string AmName
		{
			get
			{
				return this.m_objectId.AmName;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001E78C File Offset: 0x0001C98C
		public static AlternateMailboxIdParameter Parse(string identity)
		{
			return new AlternateMailboxIdParameter
			{
				m_rawName = identity,
				m_objectId = new AlternateMailboxObjectId(identity)
			};
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001E7B3 File Offset: 0x0001C9B3
		public void Initialize(ObjectId objectId)
		{
			this.m_objectId = (AlternateMailboxObjectId)objectId;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001E7C1 File Offset: 0x0001C9C1
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001E7CE File Offset: 0x0001C9CE
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		public override string ToString()
		{
			if (this.m_objectId == null)
			{
				return null;
			}
			return this.m_objectId.FullName;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001E7F0 File Offset: 0x0001C9F0
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001E808 File Offset: 0x0001CA08
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			QueryFilter queryFilter = new AlternateMailboxQueryFilter(this.m_objectId);
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					optionalData.AdditionalFilter
				});
			}
			return session.FindPaged<T>(queryFilter, rootId, true, null, 0);
		}

		// Token: 0x04000255 RID: 597
		private AlternateMailboxObjectId m_objectId;

		// Token: 0x04000256 RID: 598
		private string m_rawName;
	}
}
