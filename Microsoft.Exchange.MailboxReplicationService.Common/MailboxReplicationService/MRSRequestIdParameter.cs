using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public abstract class MRSRequestIdParameter : IIdentityParameter
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x00026500 File Offset: 0x00024700
		protected MRSRequestIdParameter()
		{
			this.indexToUse = null;
			this.indexIds = null;
			this.requestGuid = Guid.Empty;
			this.mailboxName = null;
			this.mailboxId = null;
			this.requestName = null;
			this.organizationId = null;
			this.organizationName = null;
			this.rawIdentity = null;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00026556 File Offset: 0x00024756
		protected MRSRequestIdParameter(RequestBase request) : this((RequestIndexEntryObjectId)request.Identity)
		{
			this.organizationId = request.OrganizationId;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00026578 File Offset: 0x00024778
		protected MRSRequestIdParameter(RequestJobObjectId requestJobId)
		{
			if (requestJobId == null)
			{
				throw new ArgumentNullException("requestJobId");
			}
			if (requestJobId.RequestGuid == Guid.Empty)
			{
				throw new ArgumentException(MrsStrings.InvalidRequestJob);
			}
			this.indexToUse = null;
			this.indexIds = null;
			this.requestGuid = requestJobId.RequestGuid;
			this.mailboxName = null;
			this.mailboxId = null;
			this.requestName = null;
			this.organizationId = null;
			this.organizationName = null;
			this.rawIdentity = null;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00026600 File Offset: 0x00024800
		protected MRSRequestIdParameter(RequestStatisticsBase requestStats)
		{
			if (requestStats == null)
			{
				throw new ArgumentNullException("requestStats");
			}
			if (requestStats.RequestType != this.RequestType)
			{
				throw new ArgumentException(MrsStrings.ImproperTypeForThisIdParameter, "requestStats");
			}
			this.indexToUse = null;
			this.indexIds = requestStats.IndexIds;
			this.requestGuid = requestStats.RequestGuid;
			this.mailboxName = null;
			this.mailboxId = null;
			this.requestName = null;
			this.organizationId = requestStats.OrganizationId;
			this.organizationName = null;
			this.rawIdentity = null;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00026694 File Offset: 0x00024894
		protected MRSRequestIdParameter(RequestIndexEntryObjectId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.RequestType != this.RequestType)
			{
				throw new ArgumentException(MrsStrings.ImproperTypeForThisIdParameter, "identity");
			}
			this.indexToUse = identity.IndexId;
			this.indexIds = null;
			this.requestGuid = identity.RequestGuid;
			this.mailboxName = null;
			this.mailboxId = null;
			this.requestName = null;
			this.organizationId = identity.OrganizationId;
			this.organizationName = null;
			this.rawIdentity = null;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00026728 File Offset: 0x00024928
		protected MRSRequestIdParameter(Guid guid)
		{
			this.requestGuid = guid;
			this.mailboxName = null;
			this.mailboxId = null;
			this.requestName = null;
			this.indexToUse = null;
			this.indexIds = null;
			this.organizationId = null;
			this.organizationName = null;
			this.rawIdentity = null;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002677C File Offset: 0x0002497C
		protected MRSRequestIdParameter(string request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (request.Equals(string.Empty))
			{
				throw new ArgumentException(MrsStrings.MustProvideNonEmptyStringForIdentity);
			}
			if (request.Contains("\\"))
			{
				int num = request.LastIndexOf('\\');
				string g = request.Substring(num + 1);
				Guid guid;
				if (GuidHelper.TryParseGuid(g, out guid))
				{
					this.mailboxName = null;
					this.requestGuid = guid;
					this.mailboxId = null;
					this.requestName = null;
					this.organizationName = request.Substring(0, num);
				}
				else
				{
					this.mailboxName = request.Substring(0, num);
					this.requestGuid = Guid.Empty;
					this.mailboxId = null;
					this.requestName = g;
					this.organizationName = null;
				}
				this.indexToUse = null;
				this.indexIds = null;
			}
			else
			{
				Guid guid;
				if (!GuidHelper.TryParseGuid(request, out guid))
				{
					throw new ArgumentException(MrsStrings.IdentityWasNotInValidFormat(request));
				}
				this.requestGuid = guid;
				this.mailboxName = null;
				this.mailboxId = null;
				this.requestName = null;
				this.indexToUse = null;
				this.indexIds = null;
				this.organizationName = null;
			}
			this.organizationId = null;
			this.rawIdentity = request;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000268B0 File Offset: 0x00024AB0
		protected MRSRequestIdParameter(Guid requestGuid, OrganizationId orgId, string mailboxName)
		{
			this.indexToUse = null;
			this.indexIds = null;
			this.requestGuid = requestGuid;
			this.mailboxName = mailboxName;
			this.mailboxId = null;
			this.requestName = null;
			this.organizationId = orgId;
			this.organizationName = null;
			this.rawIdentity = mailboxName + "\\" + requestGuid;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00026912 File Offset: 0x00024B12
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0002691A File Offset: 0x00024B1A
		internal RequestIndexId IndexToUse
		{
			get
			{
				return this.indexToUse;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00026922 File Offset: 0x00024B22
		internal List<RequestIndexId> IndexIds
		{
			get
			{
				return this.indexIds;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002692A File Offset: 0x00024B2A
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00026932 File Offset: 0x00024B32
		internal string MailboxName
		{
			get
			{
				return this.mailboxName;
			}
			set
			{
				this.mailboxName = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0002693B File Offset: 0x00024B3B
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x00026943 File Offset: 0x00024B43
		internal string OrganizationName
		{
			get
			{
				return this.organizationName;
			}
			set
			{
				this.organizationName = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002694C File Offset: 0x00024B4C
		internal OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00026954 File Offset: 0x00024B54
		internal Guid RequestGuid
		{
			get
			{
				return this.requestGuid;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0002695C File Offset: 0x00024B5C
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00026964 File Offset: 0x00024B64
		internal ADObjectId MailboxId
		{
			get
			{
				return this.mailboxId;
			}
			set
			{
				this.mailboxId = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0002696D File Offset: 0x00024B6D
		internal MRSRequestType RequestType
		{
			get
			{
				return MRSRequestIdParameter.GetRequestType(base.GetType());
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002697C File Offset: 0x00024B7C
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00026994 File Offset: 0x00024B94
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (!typeof(T).Equals(typeof(IRequestIndexEntry)) && !typeof(T).Equals(typeof(RequestBase)) && !typeof(IRequestIndexEntry).IsAssignableFrom(typeof(T)) && !typeof(RequestBase).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(MrsStrings.ImproperTypeForThisIdParameter);
			}
			return this.InternalGetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00026A28 File Offset: 0x00024C28
		public void Initialize(ObjectId objectId)
		{
			RequestIndexEntryObjectId requestIndexEntryObjectId = objectId as RequestIndexEntryObjectId;
			if (requestIndexEntryObjectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (requestIndexEntryObjectId.RequestGuid == Guid.Empty || requestIndexEntryObjectId.IndexId == null)
			{
				throw new ArgumentException(MrsStrings.InitializedWithInvalidObjectId);
			}
			this.requestGuid = requestIndexEntryObjectId.RequestGuid;
			this.indexToUse = requestIndexEntryObjectId.IndexId;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00026A8C File Offset: 0x00024C8C
		public override string ToString()
		{
			if (this.RawIdentity != null)
			{
				return this.RawIdentity;
			}
			return string.Format("{0}", this.requestGuid);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00026AB2 File Offset: 0x00024CB2
		internal static MRSRequestType GetRequestType<T>() where T : MRSRequestIdParameter
		{
			return MRSRequestIdParameter.GetRequestType(typeof(T));
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00026AC4 File Offset: 0x00024CC4
		internal IEnumerable<T> InternalGetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			RequestIndexEntryProvider requestIndexEntryProvider = session as RequestIndexEntryProvider;
			if (requestIndexEntryProvider == null)
			{
				throw new ArgumentException(MrsStrings.MustProvideValidSessionForFindingRequests);
			}
			if (this.requestGuid != Guid.Empty && this.indexToUse != null)
			{
				List<T> list = new List<T>(1);
				RequestIndexEntryObjectId identity = new RequestIndexEntryObjectId(this.requestGuid, this.RequestType, this.OrganizationId, this.indexToUse, null);
				T t = (T)((object)requestIndexEntryProvider.Read<T>(identity));
				if (t != null)
				{
					list.Add(t);
					notFoundReason = null;
				}
				else
				{
					notFoundReason = new LocalizedString?(MrsStrings.NoSuchRequestInSpecifiedIndex);
				}
				return list;
			}
			if (string.IsNullOrEmpty(this.requestName) || this.indexToUse == null)
			{
				notFoundReason = new LocalizedString?(MrsStrings.NotEnoughInformationSupplied);
				return new List<T>(0);
			}
			if (this.mailboxId != null)
			{
				QueryFilter filter = new RequestIndexEntryQueryFilter(this.requestName, this.mailboxId, this.RequestType, this.indexToUse, true);
				notFoundReason = new LocalizedString?(MrsStrings.NoSuchRequestInSpecifiedIndex);
				return requestIndexEntryProvider.FindPaged<T>(filter, rootId, true, null, 2);
			}
			QueryFilter filter2 = new RequestIndexEntryQueryFilter(this.requestName, null, this.RequestType, this.indexToUse, false);
			notFoundReason = new LocalizedString?(MrsStrings.NoSuchRequestInSpecifiedIndex);
			return requestIndexEntryProvider.FindPaged<T>(filter2, rootId, true, null, 2);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00026C20 File Offset: 0x00024E20
		internal void SetDefaultIndex(RequestIndexId index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			if (this.indexToUse == null)
			{
				this.indexToUse = index;
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00026C3F File Offset: 0x00024E3F
		internal void SetSpecifiedIndex(RequestIndexId index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			this.indexToUse = index;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00026C56 File Offset: 0x00024E56
		private static MRSRequestType GetRequestType(Type type)
		{
			return MRSRequestIdParameter.RequestTypes[type];
		}

		// Token: 0x04000964 RID: 2404
		private static readonly Dictionary<Type, MRSRequestType> RequestTypes = new Dictionary<Type, MRSRequestType>
		{
			{
				typeof(MailboxExportRequestIdParameter),
				MRSRequestType.MailboxExport
			},
			{
				typeof(MailboxImportRequestIdParameter),
				MRSRequestType.MailboxImport
			},
			{
				typeof(MailboxRelocationRequestIdParameter),
				MRSRequestType.MailboxRelocation
			},
			{
				typeof(MailboxRestoreRequestIdParameter),
				MRSRequestType.MailboxRestore
			},
			{
				typeof(MergeRequestIdParameter),
				MRSRequestType.Merge
			},
			{
				typeof(PublicFolderMigrationRequestIdParameter),
				MRSRequestType.PublicFolderMigration
			},
			{
				typeof(PublicFolderMailboxMigrationRequestIdParameter),
				MRSRequestType.PublicFolderMailboxMigration
			},
			{
				typeof(PublicFolderMoveRequestIdParameter),
				MRSRequestType.PublicFolderMove
			},
			{
				typeof(FolderMoveRequestIdParameter),
				MRSRequestType.FolderMove
			},
			{
				typeof(SyncRequestIdParameter),
				MRSRequestType.Sync
			}
		};

		// Token: 0x04000965 RID: 2405
		private RequestIndexId indexToUse;

		// Token: 0x04000966 RID: 2406
		private List<RequestIndexId> indexIds;

		// Token: 0x04000967 RID: 2407
		private Guid requestGuid;

		// Token: 0x04000968 RID: 2408
		private string mailboxName;

		// Token: 0x04000969 RID: 2409
		private ADObjectId mailboxId;

		// Token: 0x0400096A RID: 2410
		private string organizationName;

		// Token: 0x0400096B RID: 2411
		private readonly string requestName;

		// Token: 0x0400096C RID: 2412
		private OrganizationId organizationId;

		// Token: 0x0400096D RID: 2413
		private readonly string rawIdentity;
	}
}
