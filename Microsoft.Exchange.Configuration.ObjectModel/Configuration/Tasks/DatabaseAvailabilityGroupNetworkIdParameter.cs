using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class DatabaseAvailabilityGroupNetworkIdParameter : IIdentityParameter
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x0001FCC2 File Offset: 0x0001DEC2
		public DatabaseAvailabilityGroupNetworkIdParameter()
		{
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001FCCA File Offset: 0x0001DECA
		public DatabaseAvailabilityGroupNetworkIdParameter(DatabaseAvailabilityGroupNetwork dagNet)
		{
			this.m_objectId = (DagNetworkObjectId)dagNet.Identity;
			this.m_rawName = this.m_objectId.FullName;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		public DatabaseAvailabilityGroupNetworkIdParameter(ObjectId objId)
		{
			if (objId is DagNetworkObjectId)
			{
				this.m_objectId = (DagNetworkObjectId)objId;
				this.m_rawName = this.m_objectId.FullName;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001FD21 File Offset: 0x0001DF21
		public ObjectId ObjectId
		{
			get
			{
				return this.m_objectId;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001FD29 File Offset: 0x0001DF29
		public string RawIdentity
		{
			get
			{
				return this.m_rawName;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001FD31 File Offset: 0x0001DF31
		internal string DagName
		{
			get
			{
				return this.m_objectId.DagName;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001FD3E File Offset: 0x0001DF3E
		internal string NetName
		{
			get
			{
				return this.m_objectId.NetName;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		public static DatabaseAvailabilityGroupNetworkIdParameter Parse(string identity)
		{
			DatabaseAvailabilityGroupNetworkIdParameter databaseAvailabilityGroupNetworkIdParameter = new DatabaseAvailabilityGroupNetworkIdParameter();
			databaseAvailabilityGroupNetworkIdParameter.m_rawName = identity;
			databaseAvailabilityGroupNetworkIdParameter.m_objectId = new DagNetworkObjectId(identity);
			if (databaseAvailabilityGroupNetworkIdParameter.m_objectId.DagName == string.Empty || databaseAvailabilityGroupNetworkIdParameter.m_objectId.NetName == string.Empty)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
			return databaseAvailabilityGroupNetworkIdParameter;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
		public void Initialize(ObjectId objectId)
		{
			this.m_objectId = (DagNetworkObjectId)objectId;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001FDC5 File Offset: 0x0001DFC5
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001FDD2 File Offset: 0x0001DFD2
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001FDDC File Offset: 0x0001DFDC
		public override string ToString()
		{
			if (this.m_objectId == null)
			{
				return null;
			}
			return this.m_objectId.FullName;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001FE0C File Offset: 0x0001E00C
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			QueryFilter queryFilter = new DagNetworkQueryFilter(this.m_objectId);
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

		// Token: 0x04000266 RID: 614
		private DagNetworkObjectId m_objectId;

		// Token: 0x04000267 RID: 615
		private string m_rawName;
	}
}
