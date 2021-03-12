using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000138 RID: 312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DataMigrationStepHandler : IStepHandler
	{
		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004320D File Offset: 0x0004140D
		public DataMigrationStepHandler(IMigrationDataProvider dataProvider, MigrationType migrationType, string jobName)
		{
			this.DataProvider = dataProvider;
			this.SubscriptionAccessor = MigrationServiceFactory.Instance.GetSubscriptionAccessor(this.DataProvider, migrationType, jobName, true, false);
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00043236 File Offset: 0x00041436
		public bool ExpectMailboxData
		{
			get
			{
				return !(this.SubscriptionAccessor is MRSXO1SyncRequestAccessor);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00043249 File Offset: 0x00041449
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00043251 File Offset: 0x00041451
		private protected IMigrationDataProvider DataProvider { protected get; private set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0004325A File Offset: 0x0004145A
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00043262 File Offset: 0x00041462
		private protected SubscriptionAccessorBase SubscriptionAccessor { protected get; private set; }

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004326C File Offset: 0x0004146C
		public IStepSettings Discover(MigrationJobItem jobItem, MailboxData localMailbox)
		{
			if (jobItem.MigrationJob.MigrationType != MigrationType.ExchangeOutlookAnywhere)
			{
				return null;
			}
			ExchangeOutlookAnywhereEndpoint exchangeOutlookAnywhereEndpoint = jobItem.MigrationJob.SourceEndpoint as ExchangeOutlookAnywhereEndpoint;
			MigrationUtil.AssertOrThrow(exchangeOutlookAnywhereEndpoint != null, "An SEM job should have an ExchangeOutlookAnywhereEndpoint as its source.", new object[0]);
			string text = jobItem.RemoteIdentifier ?? jobItem.Identifier;
			if (!exchangeOutlookAnywhereEndpoint.UseAutoDiscover)
			{
				NspiMigrationDataReader nspiDataReader = exchangeOutlookAnywhereEndpoint.GetNspiDataReader(jobItem.MigrationJob);
				return nspiDataReader.GetSubscriptionSettings(text);
			}
			IMigrationAutodiscoverClient autodiscoverClient = MigrationServiceFactory.Instance.GetAutodiscoverClient();
			AutodiscoverClientResponse userSettings = autodiscoverClient.GetUserSettings(exchangeOutlookAnywhereEndpoint, text);
			if (userSettings.Status == AutodiscoverClientStatus.NoError)
			{
				return ExchangeJobItemSubscriptionSettings.CreateFromAutodiscoverResponse(userSettings);
			}
			MigrationLogger.Log(MigrationEventType.Warning, "job item {0} couldn't get auto-discover settings {1}", new object[]
			{
				this,
				userSettings.ErrorMessage
			});
			if (userSettings.Status == AutodiscoverClientStatus.ConfigurationError)
			{
				throw new AutoDiscoverFailedConfigurationErrorException(userSettings.ErrorMessage);
			}
			throw new AutoDiscoverFailedInternalErrorException(userSettings.ErrorMessage);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00043350 File Offset: 0x00041550
		public void Validate(MigrationJobItem jobItem)
		{
			if (jobItem.MigrationType == MigrationType.ExchangeLocalMove || jobItem.MigrationType == MigrationType.ExchangeRemoteMove || jobItem.MigrationType == MigrationType.ExchangeOutlookAnywhere || jobItem.MigrationType == MigrationType.PSTImport || jobItem.MigrationType == MigrationType.IMAP)
			{
				if (jobItem.LocalMailbox == null)
				{
					throw new MigrationObjectNotFoundInADException(jobItem.Identifier, this.DataProvider.ADProvider.GetPreferredDomainController());
				}
				MigrationUserRecipientType recipientType = jobItem.RecipientType;
				if (recipientType != MigrationUserRecipientType.Mailbox)
				{
					switch (recipientType)
					{
					case MigrationUserRecipientType.Mailuser:
						if (jobItem.LocalMailbox.RecipientType != MigrationUserRecipientType.Mailuser)
						{
							throw new InvalidRecipientTypeException(MigrationUserRecipientType.Mailbox.ToString(), MigrationUserRecipientType.Mailuser.ToString());
						}
						break;
					case MigrationUserRecipientType.MailboxOrMailuser:
						break;
					default:
						throw new UnsupportedRecipientTypeForProtocolException(jobItem.RecipientType.ToString(), jobItem.MigrationType.ToString());
					}
				}
				else if (jobItem.LocalMailbox.RecipientType != MigrationUserRecipientType.Mailbox)
				{
					throw new InvalidRecipientTypeException(MigrationUserRecipientType.Mailuser.ToString(), MigrationUserRecipientType.Mailbox.ToString());
				}
			}
			if (jobItem.MigrationJob.UseAdvancedValidation)
			{
				this.SubscriptionAccessor.TestCreateSubscription(jobItem);
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00043464 File Offset: 0x00041664
		public IStepSnapshot Inject(MigrationJobItem jobItem)
		{
			return this.SubscriptionAccessor.CreateSubscription(jobItem);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00043474 File Offset: 0x00041674
		public IStepSnapshot Process(ISnapshotId id, MigrationJobItem jobItem, out bool updated)
		{
			updated = false;
			MigrationEndpointBase migrationEndpointBase = null;
			if (jobItem.MigrationJob.JobDirection == MigrationBatchDirection.Onboarding)
			{
				migrationEndpointBase = jobItem.MigrationJob.SourceEndpoint;
			}
			else if (jobItem.MigrationJob.JobDirection == MigrationBatchDirection.Offboarding)
			{
				migrationEndpointBase = jobItem.MigrationJob.TargetEndpoint;
			}
			ExDateTime t = jobItem.SubscriptionSettingsLastUpdatedTime ?? ExDateTime.MinValue;
			bool flag = jobItem.SubscriptionSettings != null && jobItem.SubscriptionSettings.LastModifiedTime > t;
			bool flag2 = jobItem.MigrationJob.SubscriptionSettings != null && jobItem.MigrationJob.SubscriptionSettings.LastModifiedTime > t;
			if ((migrationEndpointBase != null && migrationEndpointBase.LastModifiedTime > t) || flag2 || flag)
			{
				updated = this.SubscriptionAccessor.UpdateSubscription((ISubscriptionId)id, migrationEndpointBase, jobItem, false);
			}
			return this.SubscriptionAccessor.RetrieveSubscriptionSnapshot((ISubscriptionId)id);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00043565 File Offset: 0x00041765
		public void Start(ISnapshotId id)
		{
			this.SubscriptionAccessor.ResumeSubscription((ISubscriptionId)id, false);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0004357A File Offset: 0x0004177A
		public IStepSnapshot Stop(ISnapshotId id)
		{
			if (!this.SubscriptionAccessor.SuspendSubscription((ISubscriptionId)id))
			{
				return this.SubscriptionAccessor.RetrieveSubscriptionSnapshot((ISubscriptionId)id);
			}
			return null;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000435A2 File Offset: 0x000417A2
		public void Delete(ISnapshotId id)
		{
			this.SubscriptionAccessor.RemoveSubscription((ISubscriptionId)id);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000435B6 File Offset: 0x000417B6
		public bool CanProcess(MigrationJobItem jobItem)
		{
			return true;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000435BC File Offset: 0x000417BC
		public MigrationUserStatus ResolvePresentationStatus(MigrationFlags flags, IStepSnapshot stepSnapshot = null)
		{
			MigrationUserStatus? migrationUserStatus = MigrationJobItem.ResolveFlagStatus(flags);
			if (migrationUserStatus != null)
			{
				return migrationUserStatus.Value;
			}
			SubscriptionSnapshot subscriptionSnapshot = stepSnapshot as SubscriptionSnapshot;
			if (subscriptionSnapshot != null && subscriptionSnapshot.IsInitialSyncComplete)
			{
				return MigrationUserStatus.Synced;
			}
			return MigrationUserStatus.Syncing;
		}

		// Token: 0x04000597 RID: 1431
		public static readonly MigrationStage[] AllowedStages = new MigrationStage[]
		{
			MigrationStage.Discovery,
			MigrationStage.Validation,
			MigrationStage.Injection,
			MigrationStage.Processing
		};
	}
}
