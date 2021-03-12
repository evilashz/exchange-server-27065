using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000500 RID: 1280
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MRSRequest : ADConfigurationObject
	{
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x000DBB37 File Offset: 0x000D9D37
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x000DBB49 File Offset: 0x000D9D49
		public Guid MailboxMoveRequestGuid
		{
			get
			{
				return (Guid)this[MRSRequestSchema.MailboxMoveRequestGuid];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveRequestGuid] = value;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000DBB5C File Offset: 0x000D9D5C
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x000DBB6E File Offset: 0x000D9D6E
		public RequestStatus MailboxMoveStatus
		{
			get
			{
				return (RequestStatus)this[MRSRequestSchema.MailboxMoveStatus];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveStatus] = value;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000DBB81 File Offset: 0x000D9D81
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000DBB93 File Offset: 0x000D9D93
		public RequestFlags MailboxMoveFlags
		{
			get
			{
				return (RequestFlags)this[MRSRequestSchema.MailboxMoveFlags];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveFlags] = value;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000DBBA6 File Offset: 0x000D9DA6
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x000DBBB8 File Offset: 0x000D9DB8
		public string MailboxMoveBatchName
		{
			get
			{
				return (string)this[MRSRequestSchema.MailboxMoveBatchName];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveBatchName] = value;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000DBBC6 File Offset: 0x000D9DC6
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000DBBD8 File Offset: 0x000D9DD8
		public string MailboxMoveRemoteHostName
		{
			get
			{
				return (string)this[MRSRequestSchema.MailboxMoveRemoteHostName];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveRemoteHostName] = value;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000DBBE6 File Offset: 0x000D9DE6
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000DBBF8 File Offset: 0x000D9DF8
		public string MailboxMoveFilePath
		{
			get
			{
				return (string)this[MRSRequestSchema.MailboxMoveFilePath];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveFilePath] = value;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x000DBC06 File Offset: 0x000D9E06
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x000DBC18 File Offset: 0x000D9E18
		public ADObjectId MailboxMoveTargetMDB
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.MailboxMoveTargetMDB];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveTargetMDB] = ADObjectIdResolutionHelper.ResolveDN(value);
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000DBC2B File Offset: 0x000D9E2B
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000DBC3D File Offset: 0x000D9E3D
		public ADObjectId MailboxMoveSourceMDB
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.MailboxMoveSourceMDB];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveSourceMDB] = ADObjectIdResolutionHelper.ResolveDN(value);
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000DBC50 File Offset: 0x000D9E50
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x000DBC62 File Offset: 0x000D9E62
		public ADObjectId MailboxMoveStorageMDB
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.MailboxMoveStorageMDB];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveStorageMDB] = ADObjectIdResolutionHelper.ResolveDN(value);
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000DBC75 File Offset: 0x000D9E75
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x000DBC87 File Offset: 0x000D9E87
		public ADObjectId MailboxMoveTargetUser
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.MailboxMoveTargetUser];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveTargetUser] = value;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000DBC95 File Offset: 0x000D9E95
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x000DBCA7 File Offset: 0x000D9EA7
		public ADObjectId MailboxMoveSourceUser
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.MailboxMoveSourceUser];
			}
			set
			{
				this[MRSRequestSchema.MailboxMoveSourceUser] = value;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000DBCB5 File Offset: 0x000D9EB5
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000DBCC7 File Offset: 0x000D9EC7
		public string DisplayName
		{
			get
			{
				return (string)this[MRSRequestSchema.DisplayName];
			}
			set
			{
				this[MRSRequestSchema.DisplayName] = value;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000DBCD5 File Offset: 0x000D9ED5
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x000DBCE7 File Offset: 0x000D9EE7
		public MRSRequestType RequestType
		{
			get
			{
				return (MRSRequestType)this[MRSRequestSchema.MRSRequestType];
			}
			set
			{
				this[MRSRequestSchema.MRSRequestType] = value;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000DBCFA File Offset: 0x000D9EFA
		internal override ADObjectSchema Schema
		{
			get
			{
				return MRSRequest.schema;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000DBD01 File Offset: 0x000D9F01
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMRSRequest";
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000DBD08 File Offset: 0x000D9F08
		internal override ADObjectId ParentPath
		{
			get
			{
				return MRSRequest.parentPath;
			}
		}

		// Token: 0x040026C2 RID: 9922
		internal const string MRSContainerName = "CN=Mailbox Replication";

		// Token: 0x040026C3 RID: 9923
		internal const string MergeContainerName = "CN=MergeRequests";

		// Token: 0x040026C4 RID: 9924
		internal const string MailboxImportContainerName = "CN=MailboxImportRequests";

		// Token: 0x040026C5 RID: 9925
		internal const string MailboxExportContainerName = "CN=MailboxExportRequests";

		// Token: 0x040026C6 RID: 9926
		internal const string MailboxRestoreContainerName = "CN=MailboxRestoreRequests";

		// Token: 0x040026C7 RID: 9927
		internal const string PublicFolderMoveContainerName = "CN=PublicFolderMoveRequests";

		// Token: 0x040026C8 RID: 9928
		internal const string PublicFolderMigrationContainerName = "CN=PublicFolderMigrationRequests";

		// Token: 0x040026C9 RID: 9929
		internal const string PublicFolderMailboxMigrationContainerName = "CN=PublicFolderMailboxMigrationRequests";

		// Token: 0x040026CA RID: 9930
		internal const string SyncContainerName = "CN=SyncRequests";

		// Token: 0x040026CB RID: 9931
		internal const string FolderMoveContainerName = "CN=FolderMoveRequests";

		// Token: 0x040026CC RID: 9932
		private const string MostDerivedClass = "msExchMRSRequest";

		// Token: 0x040026CD RID: 9933
		private static MRSRequestSchema schema = ObjectSchema.GetInstance<MRSRequestSchema>();

		// Token: 0x040026CE RID: 9934
		private static ADObjectId parentPath = new ADObjectId("CN=Mailbox Replication");
	}
}
