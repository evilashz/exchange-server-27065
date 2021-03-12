using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000156 RID: 342
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MRSMergeRequestAccessor : MrsAccessorBase
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x00047492 File Offset: 0x00045692
		public MRSMergeRequestAccessor(IMigrationDataProvider dataProvider, string batchName, bool legacyManualSyncs = false) : base(dataProvider, batchName)
		{
			this.LegacyManualSyncs = legacyManualSyncs;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x000474A3 File Offset: 0x000456A3
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x000474AB File Offset: 0x000456AB
		private protected bool LegacyManualSyncs { protected get; private set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x000474B4 File Offset: 0x000456B4
		protected string JobName
		{
			get
			{
				return "Simple Migration Merge Request";
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000474BB File Offset: 0x000456BB
		protected MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.Merge;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000474C0 File Offset: 0x000456C0
		public override SubscriptionSnapshot CreateSubscription(MigrationJobItem jobItem)
		{
			return this.InternalCreateSubscription((ExchangeOutlookAnywhereEndpoint)jobItem.MigrationJob.SourceEndpoint, (ExchangeJobSubscriptionSettings)jobItem.MigrationJob.SubscriptionSettings, MRSMergeRequestAccessor.GetSettings(jobItem), jobItem.LocalMailbox, jobItem.MigrationJob.IncrementalSyncInterval.Value, false);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00047513 File Offset: 0x00045713
		public override SubscriptionSnapshot TestCreateSubscription(MigrationJobItem jobItem)
		{
			return this.TestCreateSubscription((ExchangeOutlookAnywhereEndpoint)jobItem.MigrationJob.SourceEndpoint, (ExchangeJobSubscriptionSettings)jobItem.MigrationJob.SubscriptionSettings, MRSMergeRequestAccessor.GetSettings(jobItem), jobItem.LocalMailbox, jobItem.MigrationJob.IncrementalSyncInterval, true);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00047554 File Offset: 0x00045754
		public SubscriptionSnapshot TestCreateSubscription(ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobSubscriptionSettings jobSettings, ExchangeJobItemSubscriptionSettings subscriptionSettings, IMailboxData localMailbox, TimeSpan? incrementalSyncInterval, bool forceNew)
		{
			return this.InternalCreateSubscription(endpoint, jobSettings, subscriptionSettings, localMailbox, incrementalSyncInterval ?? TimeSpan.FromDays(1.0), true);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00047590 File Offset: 0x00045790
		public override SnapshotStatus RetrieveSubscriptionStatus(ISubscriptionId subscriptionId)
		{
			MigrationUtil.ThrowOnNullArgument(subscriptionId, "subscriptionId");
			MRSAccessorIdCommand command = new MRSAccessorIdCommand("Get-MergeRequest", null, null, base.GetMRSIdentity(subscriptionId, false));
			RequestBase requestBase = this.Run(command);
			return MRSMergeRequestAccessor.GetSubscriptionStatus(requestBase.Status, requestBase.Suspend, null);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000475DF File Offset: 0x000457DF
		public override bool UpdateSubscription(ISubscriptionId subscriptionId, MigrationEndpointBase endpoint, MigrationJobItem jobItem, bool adoptingSubscription)
		{
			return this.UpdateSubscription(subscriptionId, endpoint, (ExchangeJobSubscriptionSettings)jobItem.MigrationJob.SubscriptionSettings, MRSMergeRequestAccessor.GetSettings(jobItem));
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00047600 File Offset: 0x00045800
		public bool UpdateSubscription(ISubscriptionId subscriptionId, MigrationEndpointBase endpoint, ExchangeJobSubscriptionSettings jobSubscriptionSettings, ExchangeJobItemSubscriptionSettings subscriptionSettings)
		{
			MigrationUtil.ThrowOnNullArgument(subscriptionId, "subscriptionId");
			MigrationUtil.ThrowOnNullArgument(endpoint, "endpoint");
			MigrationUtil.ThrowOnNullArgument(subscriptionSettings, "subscriptionSettings");
			MRSMergeRequestAccessor.UpdateMRSAccessorCommand updateMRSAccessorCommand = new MRSMergeRequestAccessor.UpdateMRSAccessorCommand("Set-MergeRequest", base.GetMRSIdentity(subscriptionId, false), (ExchangeOutlookAnywhereEndpoint)endpoint, jobSubscriptionSettings, subscriptionSettings);
			if (!string.IsNullOrWhiteSpace(base.BatchName))
			{
				updateMRSAccessorCommand.Command.AddParameter("BatchName", base.BatchName);
			}
			Type left;
			this.Run(updateMRSAccessorCommand, out left);
			return left == null;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00047680 File Offset: 0x00045880
		public override SubscriptionSnapshot RetrieveSubscriptionSnapshot(ISubscriptionId subscriptionId)
		{
			MigrationUtil.ThrowOnNullArgument(subscriptionId, "subscriptionId");
			MRSSubscriptionId mrsidentity = base.GetMRSIdentity(subscriptionId, false);
			MRSAccessorIdCommand mrsaccessorIdCommand = new MRSAccessorIdCommand("Get-MergeRequestStatistics", null, null, mrsidentity);
			if (base.IncludeReport)
			{
				mrsaccessorIdCommand.IncludeReport = true;
			}
			SubscriptionSnapshot subscriptionSnapshot = this.RetrieveSubscriptionSnapshot(mrsaccessorIdCommand, mrsidentity.MailboxData);
			if (subscriptionSnapshot == null)
			{
				subscriptionSnapshot = SubscriptionSnapshot.CreateRemoved();
			}
			return subscriptionSnapshot;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000476D8 File Offset: 0x000458D8
		public override bool ResumeSubscription(ISubscriptionId subscriptionId, bool finalize = false)
		{
			MRSSubscriptionId mrsidentity = base.GetMRSIdentity(subscriptionId, true);
			MrsAccessorCommand command = new MRSMergeRequestAccessor.ResumeMRSAccessorCommand("Resume-MergeRequest", mrsidentity, this.LegacyManualSyncs);
			Type left;
			this.Run(command, out left);
			return left == null;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00047714 File Offset: 0x00045914
		public override bool SuspendSubscription(ISubscriptionId subscriptionId)
		{
			MRSSubscriptionId mrsidentity = base.GetMRSIdentity(subscriptionId, true);
			MrsAccessorCommand command = new MRSAccessorIdCommand("Suspend-MergeRequest", new Type[]
			{
				typeof(CannotSetCompletedPermanentException)
			}, new Type[]
			{
				typeof(CannotSetCompletingPermanentException)
			}, mrsidentity);
			Type left;
			this.Run(command, out left);
			return left == null;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00047774 File Offset: 0x00045974
		public override bool RemoveSubscription(ISubscriptionId subscriptionId)
		{
			MRSSubscriptionId mrsidentity = base.GetMRSIdentity(subscriptionId, true);
			MrsAccessorCommand command = new MRSMergeRequestAccessor.MRSAccessorIdCommandIgnoreMissing("Remove-MergeRequest", new Type[]
			{
				typeof(CannotSetCompletingPermanentException)
			}, mrsidentity);
			Type left;
			this.Run(command, out left);
			return left == null;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000477BC File Offset: 0x000459BC
		public RequestBase Run(MrsAccessorCommand command)
		{
			Type type;
			return this.Run(command, out type);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000477D2 File Offset: 0x000459D2
		public RequestBase Run(MrsAccessorCommand command, out Type ignoredErrorType)
		{
			return base.Run<RequestBase>(command, out ignoredErrorType);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000477DC File Offset: 0x000459DC
		internal static ExchangeJobItemSubscriptionSettings GetSettings(MigrationJobItem jobItem)
		{
			if (jobItem.IsPAW || (((ExchangeOutlookAnywhereEndpoint)jobItem.MigrationJob.SourceEndpoint).UseAutoDiscover && jobItem.SubscriptionSettings != null))
			{
				return (ExchangeJobItemSubscriptionSettings)jobItem.SubscriptionSettings;
			}
			ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = (ExchangeProvisioningDataStorage)jobItem.ProvisioningData;
			string propertyValue = exchangeProvisioningDataStorage.ExchangeRecipient.GetPropertyValue<string>(PropTag.EmailAddress);
			string text = null;
			ExchangeMigrationRecipientWithHomeServer exchangeMigrationRecipientWithHomeServer = exchangeProvisioningDataStorage.ExchangeRecipient as ExchangeMigrationRecipientWithHomeServer;
			if (exchangeMigrationRecipientWithHomeServer != null)
			{
				text = exchangeMigrationRecipientWithHomeServer.MsExchHomeServerName;
			}
			return ExchangeJobItemSubscriptionSettings.CreateFromProperties(propertyValue, text, text, null);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0004785C File Offset: 0x00045A5C
		protected static SnapshotStatus GetSubscriptionStatus(RequestStatus requestStatus, bool suspendFlag, ExceptionSide? exceptionSide)
		{
			switch (requestStatus)
			{
			case RequestStatus.None:
			case RequestStatus.CompletionInProgress:
			case RequestStatus.Completed:
			case RequestStatus.CompletedWithWarning:
				MigrationLogger.Log(MigrationEventType.Error, "unsupported status for merge request: {0}", new object[]
				{
					requestStatus
				});
				return SnapshotStatus.Corrupted;
			case RequestStatus.Queued:
			case RequestStatus.InProgress:
				return SnapshotStatus.InProgress;
			case RequestStatus.AutoSuspended:
				if (suspendFlag)
				{
					return SnapshotStatus.AutoSuspended;
				}
				return SnapshotStatus.InProgress;
			case RequestStatus.Synced:
				return SnapshotStatus.Synced;
			case (RequestStatus)6:
			case (RequestStatus)7:
			case (RequestStatus)8:
			case (RequestStatus)9:
				break;
			default:
				switch (requestStatus)
				{
				case RequestStatus.Suspended:
					if (suspendFlag)
					{
						return SnapshotStatus.Suspended;
					}
					return SnapshotStatus.InProgress;
				case RequestStatus.Failed:
					if (!suspendFlag)
					{
						return SnapshotStatus.InProgress;
					}
					if (exceptionSide != null && exceptionSide.Value != ExceptionSide.Source)
					{
						MigrationLogger.Log(MigrationEventType.Error, "exception side {0} was not source, treating as corrupt", new object[]
						{
							exceptionSide.Value
						});
						return SnapshotStatus.Corrupted;
					}
					return SnapshotStatus.Failed;
				}
				break;
			}
			MigrationLogger.Log(MigrationEventType.Error, "unknown status for merge request: {0}", new object[]
			{
				requestStatus
			});
			return SnapshotStatus.Corrupted;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00047940 File Offset: 0x00045B40
		protected SubscriptionSnapshot RetrieveSubscriptionSnapshot(MrsAccessorCommand command, IMailboxData localMailbox)
		{
			MergeRequestStatistics mergeRequestStatistics = base.Run<MergeRequestStatistics>(command);
			if (mergeRequestStatistics == null)
			{
				return null;
			}
			MRSSubscriptionId id = new MRSSubscriptionId(mergeRequestStatistics.RequestGuid, MigrationType.ExchangeOutlookAnywhere, localMailbox);
			if (mergeRequestStatistics.Status == RequestStatus.None)
			{
				return null;
			}
			ExDateTime createTime;
			if (mergeRequestStatistics.QueuedTimestamp != null)
			{
				createTime = (ExDateTime)mergeRequestStatistics.QueuedTimestamp.Value;
			}
			else
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "subscription {0} hasn't been queued yet", new object[]
				{
					mergeRequestStatistics
				});
				createTime = ExDateTime.UtcNow;
			}
			LocalizedString? errorMessage = null;
			if (!mergeRequestStatistics.IsValid)
			{
				errorMessage = new LocalizedString?(mergeRequestStatistics.ValidationMessage);
			}
			else if (mergeRequestStatistics.Status == RequestStatus.Failed)
			{
				errorMessage = new LocalizedString?(mergeRequestStatistics.Message);
			}
			ExDateTime? lastUpdateTime = new ExDateTime?((ExDateTime)mergeRequestStatistics.LastUpdateTimestamp.Value);
			ExDateTime? lastSyncTime = null;
			if (mergeRequestStatistics.Status == RequestStatus.AutoSuspended)
			{
				lastSyncTime = (ExDateTime?)mergeRequestStatistics.SuspendedTimestamp;
			}
			SubscriptionSnapshot subscriptionSnapshot = new SubscriptionSnapshot(id, MRSMergeRequestAccessor.GetSubscriptionStatus(mergeRequestStatistics.Status, mergeRequestStatistics.Suspend, mergeRequestStatistics.FailureSide), mergeRequestStatistics.InitialSeedingCompletedTimestamp != null, createTime, lastUpdateTime, lastSyncTime, errorMessage, mergeRequestStatistics.BatchName);
			long numberItemsSynced = 0L;
			long numberItemsSkipped = 0L;
			if (mergeRequestStatistics.ItemsTransferred != null)
			{
				numberItemsSynced = MrsAccessorBase.HandleLongOverflow(mergeRequestStatistics.ItemsTransferred.Value, mergeRequestStatistics);
				numberItemsSkipped = (long)(mergeRequestStatistics.BadItemsEncountered + mergeRequestStatistics.LargeItemsEncountered);
			}
			long value = MrsAccessorBase.HandleLongOverflow(mergeRequestStatistics.TotalMailboxItemCount, mergeRequestStatistics);
			subscriptionSnapshot.SetStatistics(numberItemsSynced, numberItemsSkipped, new long?(value));
			subscriptionSnapshot.TotalQueuedDuration = mergeRequestStatistics.TotalQueuedDuration;
			subscriptionSnapshot.TotalInProgressDuration = mergeRequestStatistics.TimeTracker.GetDisplayDuration(new RequestState[]
			{
				RequestState.InitializingMove,
				RequestState.InitialSeeding,
				RequestState.Completion
			});
			subscriptionSnapshot.TotalSyncedDuration = mergeRequestStatistics.TimeTracker.GetDisplayDuration(new RequestState[]
			{
				RequestState.AutoSuspended
			});
			subscriptionSnapshot.TotalStalledDuration = SubscriptionSnapshot.Subtract(mergeRequestStatistics.TimeTracker.GetDisplayDuration(new RequestState[]
			{
				RequestState.Stalled,
				RequestState.TransientFailure,
				RequestState.Suspended,
				RequestState.Failed
			}), subscriptionSnapshot.TotalSyncedDuration);
			subscriptionSnapshot.EstimatedTotalTransferSize = new ByteQuantifiedSize?(mergeRequestStatistics.EstimatedTransferSize);
			subscriptionSnapshot.EstimatedTotalTransferCount = new ulong?(mergeRequestStatistics.EstimatedTransferItemCount);
			subscriptionSnapshot.BytesTransferred = mergeRequestStatistics.BytesTransferred;
			subscriptionSnapshot.CurrentBytesTransferredPerMinute = mergeRequestStatistics.BytesTransferredPerMinute;
			subscriptionSnapshot.AverageBytesTransferredPerHour = ((mergeRequestStatistics.BytesTransferred != null && mergeRequestStatistics.TotalInProgressDuration != null && mergeRequestStatistics.TotalInProgressDuration.Value.Ticks > 0L) ? new ByteQuantifiedSize?(mergeRequestStatistics.BytesTransferred.Value / (ulong)mergeRequestStatistics.TotalInProgressDuration.Value.Ticks * 36000000000UL) : null);
			subscriptionSnapshot.Report = mergeRequestStatistics.Report;
			subscriptionSnapshot.PercentageComplete = new int?(mergeRequestStatistics.PercentComplete);
			return subscriptionSnapshot;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00047C50 File Offset: 0x00045E50
		protected object GetMRSIdentity(IMailboxData targetMailbox)
		{
			MigrationUtil.ThrowOnNullArgument(targetMailbox, "jobItem");
			string mailboxIdentifier = targetMailbox.MailboxIdentifier;
			return string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
			{
				mailboxIdentifier,
				this.JobName
			});
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00047C94 File Offset: 0x00045E94
		private SubscriptionSnapshot InternalCreateSubscription(ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobSubscriptionSettings jobSettings, ExchangeJobItemSubscriptionSettings jobItemSettings, IMailboxData targetMailbox, TimeSpan incrementalSyncInterval, bool whatIf)
		{
			bool useAdmin = endpoint.MailboxPermission == MigrationMailboxPermission.Admin;
			NewMergeRequestCommand newMergeRequestCommand = new NewMergeRequestCommand(endpoint, jobItemSettings, targetMailbox.GetIdParameter<MailboxOrMailUserIdParameter>(), "Simple Migration Merge Request", whatIf, useAdmin);
			if (this.LegacyManualSyncs)
			{
				newMergeRequestCommand.SuspendWhenReadyToComplete = true;
			}
			else
			{
				newMergeRequestCommand.IncrementalSyncInterval = incrementalSyncInterval;
			}
			if (!string.IsNullOrWhiteSpace(base.BatchName))
			{
				newMergeRequestCommand.BatchName = base.BatchName;
			}
			if (jobSettings != null && jobSettings.StartAfter != null)
			{
				newMergeRequestCommand.StartAfter = (DateTime?)jobSettings.StartAfter;
			}
			RequestBase requestBase = this.Run(newMergeRequestCommand);
			if (whatIf)
			{
				return null;
			}
			if (requestBase != null)
			{
				ISubscriptionId id = new MRSSubscriptionId(requestBase.RequestGuid, MigrationType.ExchangeOutlookAnywhere, targetMailbox);
				return SubscriptionSnapshot.CreateId(id);
			}
			SubscriptionSnapshot subscriptionSnapshot = this.RetrieveSubscriptionSnapshot(new MRSAccessorIdCommand("Get-MergeRequestStatistics", null, null, this.GetMRSIdentity(targetMailbox)), targetMailbox);
			MigrationUtil.AssertOrThrow(subscriptionSnapshot != null && subscriptionSnapshot.Id != null, "job endpoint {0} and user {1}", new object[]
			{
				endpoint,
				jobItemSettings
			});
			MigrationLogger.Log(MigrationEventType.Information, "Updating MRS subscription in Create because it already exists: {0} with connection settings {1}", new object[]
			{
				subscriptionSnapshot,
				endpoint
			});
			this.UpdateSubscription((ISubscriptionId)subscriptionSnapshot.Id, endpoint, jobSettings, jobItemSettings);
			if (subscriptionSnapshot.Status != SnapshotStatus.InProgress)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Updating subscription {0} to be in progress, formerly was {1}", new object[]
				{
					subscriptionSnapshot,
					subscriptionSnapshot.Status
				});
				this.ResumeSubscription((ISubscriptionId)subscriptionSnapshot.Id, false);
			}
			return subscriptionSnapshot;
		}

		// Token: 0x040005E9 RID: 1513
		private const string MRSMergeJobName = "Simple Migration Merge Request";

		// Token: 0x040005EA RID: 1514
		private const string GetMergeRequestCommand = "Get-MergeRequest";

		// Token: 0x040005EB RID: 1515
		private const string GetMergeRequestStatisticsCommand = "Get-MergeRequestStatistics";

		// Token: 0x040005EC RID: 1516
		private const string SuspendMergeRequestCommand = "Suspend-MergeRequest";

		// Token: 0x040005ED RID: 1517
		private const string ResumeMergeRequestCommand = "Resume-MergeRequest";

		// Token: 0x040005EE RID: 1518
		private const string RemoveMergeRequestCommand = "Remove-MergeRequest";

		// Token: 0x040005EF RID: 1519
		private const string SetMergeRequestCommand = "Set-MergeRequest";

		// Token: 0x02000157 RID: 343
		protected class MRSAccessorIdCommandIgnoreMissing : MRSAccessorIdCommand
		{
			// Token: 0x0600110B RID: 4363 RVA: 0x00047E10 File Offset: 0x00046010
			public MRSAccessorIdCommandIgnoreMissing(string name, ICollection<Type> transientExceptions, MRSSubscriptionId identity) : base(name, MRSMergeRequestAccessor.MRSAccessorIdCommandIgnoreMissing.ExceptionsToIgnore, transientExceptions, identity)
			{
			}

			// Token: 0x040005F1 RID: 1521
			private static readonly Type[] ExceptionsToIgnore = new Type[]
			{
				typeof(ManagementObjectNotFoundException)
			};
		}

		// Token: 0x02000158 RID: 344
		protected class ResumeMRSAccessorCommand : MRSAccessorIdCommand
		{
			// Token: 0x0600110D RID: 4365 RVA: 0x00047E47 File Offset: 0x00046047
			public ResumeMRSAccessorCommand(string name, MRSSubscriptionId identity, bool useSWRTC = false) : base(name, MRSMergeRequestAccessor.ResumeMRSAccessorCommand.IgnoreExceptionTypes, null, identity)
			{
				if (useSWRTC)
				{
					base.AddParameter("SuspendWhenReadyToComplete", true);
				}
			}

			// Token: 0x040005F2 RID: 1522
			private static readonly Type[] IgnoreExceptionTypes = new Type[]
			{
				typeof(CannotSetCompletedPermanentException)
			};
		}

		// Token: 0x02000159 RID: 345
		protected class UpdateMRSAccessorCommand : MRSAccessorIdCommand
		{
			// Token: 0x0600110F RID: 4367 RVA: 0x00047E93 File Offset: 0x00046093
			public UpdateMRSAccessorCommand(string name, MRSSubscriptionId identity, ExchangeOutlookAnywhereEndpoint settings, ExchangeJobSubscriptionSettings jobSettings, ExchangeJobItemSubscriptionSettings jobItemSettings) : base(name, MRSMergeRequestAccessor.UpdateMRSAccessorCommand.IgnoreExceptionTypes, null, identity)
			{
				this.UpdateSubscriptionSettings(settings, jobSettings, jobItemSettings);
			}

			// Token: 0x06001110 RID: 4368 RVA: 0x00047EB0 File Offset: 0x000460B0
			protected override void UpdateSubscriptionSettings(ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobSubscriptionSettings jobSettings, ExchangeJobItemSubscriptionSettings jobItemSettings)
			{
				base.UpdateSubscriptionSettings(endpoint, jobSettings, jobItemSettings);
				if (jobSettings != null && jobSettings.StartAfter != null)
				{
					base.AddParameter("StartAfter", (DateTime?)jobSettings.StartAfter);
				}
			}

			// Token: 0x040005F3 RID: 1523
			private static readonly Type[] IgnoreExceptionTypes = new Type[]
			{
				typeof(CannotSetCompletedPermanentException)
			};
		}
	}
}
