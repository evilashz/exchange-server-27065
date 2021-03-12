using System;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000154 RID: 340
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MRSSubscriptionId : ISubscriptionId, ISnapshotId, IMigrationSerializable
	{
		// Token: 0x060010D9 RID: 4313 RVA: 0x00047017 File Offset: 0x00045217
		public MRSSubscriptionId(MigrationType migrationType, IMailboxData mailboxData)
		{
			this.subscriptionId = Guid.Empty;
			this.MigrationType = migrationType;
			this.MailboxData = mailboxData;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00047038 File Offset: 0x00045238
		public MRSSubscriptionId(Guid id, MigrationType migrationType, IMailboxData mailboxData)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(id, "id");
			this.subscriptionId = id;
			this.MigrationType = migrationType;
			this.MailboxData = mailboxData;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00047060 File Offset: 0x00045260
		public Guid Id
		{
			get
			{
				if (this.RequestType == MRSRequestType.Move)
				{
					MailboxData mailboxData = this.MailboxData as MailboxData;
					if (mailboxData != null)
					{
						if (mailboxData.ExchangeObjectId != Guid.Empty)
						{
							return mailboxData.ExchangeObjectId;
						}
						if (mailboxData.UserMailboxADObjectId != null)
						{
							return mailboxData.UserMailboxADObjectId.ObjectGuid;
						}
					}
				}
				return this.subscriptionId;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x000470B7 File Offset: 0x000452B7
		public Guid RequestGuid
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000470BF File Offset: 0x000452BF
		public MRSRequestType RequestType
		{
			get
			{
				return MRSSubscriptionId.MRSRequestTypeFromMigrationType(this.MigrationType);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000470CC File Offset: 0x000452CC
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000470D4 File Offset: 0x000452D4
		public MigrationType MigrationType { get; private set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000470DD File Offset: 0x000452DD
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x000470E5 File Offset: 0x000452E5
		public IMailboxData MailboxData { get; private set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000470EE File Offset: 0x000452EE
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MRSSubscriptionId.MRSSubscriptionIdPropertyDefinitions;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x000470F5 File Offset: 0x000452F5
		public bool HasValue
		{
			get
			{
				return this.Id != Guid.Empty;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00047108 File Offset: 0x00045308
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004712C File Offset: 0x0004532C
		public override bool Equals(object obj)
		{
			MRSSubscriptionId mrssubscriptionId = obj as MRSSubscriptionId;
			return mrssubscriptionId != null && this.Id.Equals(mrssubscriptionId.Id) && object.Equals(this.MailboxData.OrganizationId, mrssubscriptionId.MailboxData.OrganizationId);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00047178 File Offset: 0x00045378
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.MailboxData.OrganizationId.GetHashCode();
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000471AC File Offset: 0x000453AC
		public IIdentityParameter GetIdParameter()
		{
			if (this.RequestType == MRSRequestType.Move)
			{
				return this.MailboxData.GetIdParameter<MoveRequestIdParameter>();
			}
			if (this.RequestType == MRSRequestType.Sync)
			{
				return new SyncRequestIdParameter(this.subscriptionId, this.MailboxData.OrganizationId, this.MailboxData.MailboxIdentifier);
			}
			RequestIndexEntryObjectId identity = new RequestIndexEntryObjectId(this.subscriptionId, this.RequestType, this.MailboxData.OrganizationId, new RequestIndexId(RequestIndexLocation.AD), null);
			MRSRequestType requestType = this.RequestType;
			switch (requestType)
			{
			case MRSRequestType.Merge:
				return new MergeRequestIdParameter(identity);
			case MRSRequestType.MailboxImport:
				return new MailboxImportRequestIdParameter(identity);
			default:
				if (requestType != MRSRequestType.PublicFolderMailboxMigration)
				{
					throw new NotSupportedException("don't support request type yet...");
				}
				return new PublicFolderMailboxMigrationRequestIdParameter(identity);
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00047259 File Offset: 0x00045459
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.subscriptionId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemMRSId, false);
			return this.subscriptionId != Guid.Empty;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004727D File Offset: 0x0004547D
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			if (this.subscriptionId == Guid.Empty)
			{
				message.Delete(MigrationBatchMessageSchema.MigrationJobItemMRSId);
				return;
			}
			message[MigrationBatchMessageSchema.MigrationJobItemMRSId] = this.Id;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000472B3 File Offset: 0x000454B3
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return new XElement("MRSSubscriptionId", new XElement("RequestGuid", this.RequestGuid));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000472E0 File Offset: 0x000454E0
		private static MRSRequestType MRSRequestTypeFromMigrationType(MigrationType migrationType)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				switch (migrationType)
				{
				case MigrationType.IMAP:
				case MigrationType.XO1:
					return MRSRequestType.Sync;
				case MigrationType.IMAP | MigrationType.XO1:
					goto IL_45;
				case MigrationType.ExchangeOutlookAnywhere:
					return MRSRequestType.Merge;
				default:
					if (migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_45;
					}
					break;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove)
			{
				if (migrationType == MigrationType.PSTImport)
				{
					return MRSRequestType.MailboxImport;
				}
				if (migrationType != MigrationType.PublicFolder)
				{
					goto IL_45;
				}
				return MRSRequestType.PublicFolderMailboxMigration;
			}
			return MRSRequestType.Move;
			IL_45:
			MigrationUtil.AssertOrThrow(false, "Unsupported migration type '{0}' for MRSSubscriptionId.", new object[]
			{
				migrationType
			});
			return MRSRequestType.Move;
		}

		// Token: 0x040005E5 RID: 1509
		internal static readonly PropertyDefinition[] MRSSubscriptionIdPropertyDefinitions = new StorePropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemMRSId
		};

		// Token: 0x040005E6 RID: 1510
		private Guid subscriptionId;
	}
}
