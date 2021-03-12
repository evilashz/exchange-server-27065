using System;
using System.Globalization;
using System.Web.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ProvisioningStepHandlerBase : IStepHandler
	{
		// Token: 0x06000203 RID: 515 RVA: 0x000098A2 File Offset: 0x00007AA2
		protected ProvisioningStepHandlerBase(IMigrationDataProvider dataProvider)
		{
			this.DataProvider = dataProvider;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000098B1 File Offset: 0x00007AB1
		public bool ExpectMailboxData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000098B4 File Offset: 0x00007AB4
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000098BC File Offset: 0x00007ABC
		private protected IMigrationDataProvider DataProvider { protected get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000098C5 File Offset: 0x00007AC5
		protected IProvisioningHandler ProvisioningHandler
		{
			get
			{
				return MigrationApplication.ProvisioningHandler;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000098CC File Offset: 0x00007ACC
		public IStepSettings Discover(MigrationJobItem jobItem, MailboxData localMailbox)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem.MigrationJob, "MigrationJob");
			if (jobItem.MigrationType == MigrationType.ExchangeOutlookAnywhere)
			{
				ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = jobItem.ProvisioningData as ExchangeProvisioningDataStorage;
				NspiMigrationDataReader nspiDataReader = jobItem.MigrationJob.SourceEndpoint.GetNspiDataReader(jobItem.MigrationJob);
				ExchangeMigrationRecipient recipientData = nspiDataReader.GetRecipientData(jobItem.RemoteIdentifier ?? jobItem.Identifier, this.GetProvisioningType(jobItem));
				string encryptedPassword = null;
				if ((recipientData.RecipientType == MigrationUserRecipientType.Mailbox || recipientData.RecipientType == MigrationUserRecipientType.Mailuser) && !jobItem.MigrationJob.IsStaged && (exchangeProvisioningDataStorage == null || exchangeProvisioningDataStorage.ExchangeRecipient == null))
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "provisioning a password for job-item {0}", new object[]
					{
						jobItem.Identifier
					});
					string clearString = Membership.GeneratePassword(16, 3);
					encryptedPassword = MigrationServiceFactory.Instance.GetCryptoAdapter().ClearStringToEncryptedString(clearString);
				}
				exchangeProvisioningDataStorage.ExchangeRecipient = recipientData;
				exchangeProvisioningDataStorage.EncryptedPassword = encryptedPassword;
				if (localMailbox != null)
				{
					exchangeProvisioningDataStorage.ExchangeRecipient.DoesADObjectExist = true;
				}
				return exchangeProvisioningDataStorage;
			}
			return null;
		}

		// Token: 0x06000209 RID: 521
		protected abstract ProvisioningType GetProvisioningType(MigrationJobItem jobItem);

		// Token: 0x0600020A RID: 522 RVA: 0x000099BC File Offset: 0x00007BBC
		public void Validate(MigrationJobItem jobItem)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000099BE File Offset: 0x00007BBE
		public IStepSnapshot Inject(MigrationJobItem jobItem)
		{
			return null;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000099C4 File Offset: 0x00007BC4
		public IStepSnapshot Process(ISnapshotId id, MigrationJobItem jobItem, out bool updated)
		{
			updated = false;
			ProvisioningId provisioningId = (ProvisioningId)id;
			this.EnsureJobRegistered(jobItem.MigrationJob);
			if (!this.ProvisioningHandler.IsItemQueued(provisioningId))
			{
				if (!this.ProvisioningHandler.HasCapacity(jobItem.MigrationJobId))
				{
					throw new ProvisioningThrottledTransientException();
				}
				IProvisioningData provisioningData = this.GetProvisioningData(jobItem);
				if (provisioningData == null)
				{
					return ProvisioningSnapshot.CreateCompleted(provisioningId);
				}
				this.ProvisioningHandler.QueueItem(jobItem.MigrationJobId, jobItem.ProvisioningId, provisioningData);
			}
			IStepSnapshot stepSnapshot = this.RetrieveCurrentSnapshot(provisioningId, jobItem);
			if (stepSnapshot.Status == SnapshotStatus.Finalized)
			{
				this.TryUnregisterJob(provisioningId.JobGuid);
			}
			return stepSnapshot;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009A58 File Offset: 0x00007C58
		public void Start(ISnapshotId id)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009A5C File Offset: 0x00007C5C
		public IStepSnapshot Stop(ISnapshotId id)
		{
			ProvisioningId provisioningId = (ProvisioningId)id;
			if (!this.ProvisioningHandler.IsJobRegistered(provisioningId.JobGuid))
			{
				return null;
			}
			if (!this.ProvisioningHandler.IsItemQueued(provisioningId))
			{
				return null;
			}
			IStepSnapshot stepSnapshot = this.RetrieveCurrentSnapshot(provisioningId, null);
			if (stepSnapshot.Status == SnapshotStatus.Finalized)
			{
				this.TryUnregisterJob(provisioningId.JobGuid);
			}
			return stepSnapshot;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009AB4 File Offset: 0x00007CB4
		public void Delete(ISnapshotId id)
		{
			ProvisioningId provisioningId = (ProvisioningId)id;
			if (!this.ProvisioningHandler.IsJobRegistered(provisioningId.JobGuid))
			{
				return;
			}
			if (!this.ProvisioningHandler.IsItemQueued(provisioningId))
			{
				return;
			}
			if (this.ProvisioningHandler.IsItemCompleted(provisioningId))
			{
				this.ProvisioningHandler.DequeueItem(provisioningId);
				this.TryUnregisterJob(provisioningId.JobGuid);
			}
			this.ProvisioningHandler.CancelItem(provisioningId);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009B1E File Offset: 0x00007D1E
		public bool CanProcess(MigrationJobItem jobItem)
		{
			this.EnsureJobRegistered(jobItem.MigrationJob);
			return this.ProvisioningHandler.HasCapacity(jobItem.MigrationJobId) || this.ProvisioningHandler.IsItemQueued(jobItem.ProvisioningId);
		}

		// Token: 0x06000211 RID: 529
		public abstract MigrationUserStatus ResolvePresentationStatus(MigrationFlags flags, IStepSnapshot stepSnapshot = null);

		// Token: 0x06000212 RID: 530
		protected abstract IProvisioningData GetProvisioningData(MigrationJobItem jobItem);

		// Token: 0x06000213 RID: 531 RVA: 0x00009B58 File Offset: 0x00007D58
		private void EnsureJobRegistered(MigrationJob job)
		{
			if (this.ProvisioningHandler.IsJobRegistered(job.JobId))
			{
				return;
			}
			Guid jobId = job.JobId;
			CultureInfo adminCulture = job.AdminCulture;
			Guid ownerExchangeObjectId = job.OwnerExchangeObjectId;
			ADObjectId ownerId = job.OwnerId;
			DelegatedPrincipal delegatedAdminOwner = job.DelegatedAdminOwner;
			if (ownerId == null && delegatedAdminOwner == null)
			{
				throw MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationUnknownException>("Cannot do provisioning since both owner id and delegated admin are null");
			}
			this.ProvisioningHandler.RegisterJob(jobId, adminCulture, ownerExchangeObjectId, ownerId, delegatedAdminOwner, job.SubmittedByUserAdminType, this.DataProvider.ADProvider.TenantOrganizationName, this.DataProvider.OrganizationId);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009BE1 File Offset: 0x00007DE1
		private void TryUnregisterJob(Guid jobId)
		{
			if (!this.ProvisioningHandler.IsJobRegistered(jobId))
			{
				return;
			}
			if (this.ProvisioningHandler.CanUnregisterJob(jobId))
			{
				this.ProvisioningHandler.UnregisterJob(jobId);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009C0C File Offset: 0x00007E0C
		private IStepSnapshot RetrieveCurrentSnapshot(ProvisioningId provisioningId, MigrationJobItem jobItem)
		{
			if (!this.ProvisioningHandler.IsItemCompleted(provisioningId))
			{
				return ProvisioningSnapshot.CreateInProgress(provisioningId);
			}
			ProvisionedObject provisionedObject = this.ProvisioningHandler.DequeueItem(provisioningId);
			if (provisionedObject.Type == ProvisioningType.GroupMember)
			{
				GroupProvisioningSnapshot groupProvisioningSnapshot = new GroupProvisioningSnapshot(provisionedObject);
				if (jobItem != null && jobItem.MigrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					GroupProvisioningSnapshot groupProvisioningSnapshot2 = jobItem.ProvisioningStatistics as GroupProvisioningSnapshot;
					if (groupProvisioningSnapshot2 != null)
					{
						groupProvisioningSnapshot.CountOfProvisionedMembers += groupProvisioningSnapshot2.CountOfProvisionedMembers;
						groupProvisioningSnapshot.CountOfSkippedMembers += groupProvisioningSnapshot2.CountOfSkippedMembers;
					}
					ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = jobItem.ProvisioningData as ExchangeProvisioningDataStorage;
					MigrationUtil.ThrowOnNullArgument(exchangeProvisioningDataStorage, "we only currently provision groups for Exchange provisioning");
					ExchangeMigrationGroupRecipient exchangeMigrationGroupRecipient = exchangeProvisioningDataStorage.ExchangeRecipient as ExchangeMigrationGroupRecipient;
					MigrationUtil.ThrowOnNullArgument(exchangeMigrationGroupRecipient, "we should only be provisioning a group if we have already discovered its information");
					int num = groupProvisioningSnapshot.CountOfProvisionedMembers + groupProvisioningSnapshot.CountOfSkippedMembers;
					if (exchangeMigrationGroupRecipient.Members == null || num >= exchangeMigrationGroupRecipient.Members.Length)
					{
						groupProvisioningSnapshot.ProvisioningState = GroupMembershipProvisioningState.MemberRetrievedAndProvisioned;
					}
					else
					{
						groupProvisioningSnapshot.Status = SnapshotStatus.InProgress;
					}
				}
				MigrationLogger.Log(MigrationEventType.Verbose, "job-item {0} now totals {1} members provisioned ({2} skipped)", new object[]
				{
					provisioningId.JobItemGuid.ToString(),
					groupProvisioningSnapshot.CountOfProvisionedMembers,
					groupProvisioningSnapshot.CountOfSkippedMembers
				});
				return groupProvisioningSnapshot;
			}
			return new ProvisioningSnapshot(provisionedObject);
		}

		// Token: 0x040000BF RID: 191
		private const int MaxPasswordLength = 16;

		// Token: 0x040000C0 RID: 192
		private const int NumberAlphaNumericChars = 3;

		// Token: 0x040000C1 RID: 193
		public static readonly MigrationStage[] AllowedStages = new MigrationStage[]
		{
			MigrationStage.Discovery,
			MigrationStage.Processing
		};
	}
}
