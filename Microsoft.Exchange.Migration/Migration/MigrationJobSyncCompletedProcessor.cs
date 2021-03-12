using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200011A RID: 282
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobSyncCompletedProcessor : JobProcessor
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003DCEF File Offset: 0x0003BEEF
		internal override bool SupportsInterrupting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003DCF4 File Offset: 0x0003BEF4
		internal static MigrationJobSyncCompletedProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type != MigrationType.IMAP && type != MigrationType.ExchangeOutlookAnywhere)
				{
					if (type != MigrationType.BulkProvisioning)
					{
						goto IL_37;
					}
					throw new NotSupportedException("Bulk Provisioning not supported in Synced state");
				}
			}
			else if (type != MigrationType.ExchangeRemoteMove && type != MigrationType.ExchangeLocalMove && type != MigrationType.PublicFolder)
			{
				goto IL_37;
			}
			return new MigrationJobSyncCompletedProcessor();
			IL_37:
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003DD4D File Offset: 0x0003BF4D
		internal override bool Validate()
		{
			return true;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003DD50 File Offset: 0x0003BF50
		internal override MigrationJobStatus GetNextStageStatus()
		{
			if (this.ShouldStopProcessing())
			{
				return MigrationJobStatus.Stopped;
			}
			if (!base.Job.AutoComplete || base.Job.CompleteAfterMoveSyncNotCompleted)
			{
				return MigrationJobStatus.SyncCompleted;
			}
			return MigrationJobStatus.CompletionStarting;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		protected override bool ShouldStopProcessing()
		{
			if (base.Job.AutoStop || base.Job.IsCancelled)
			{
				return true;
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (utcNow < base.Job.StateLastUpdated)
			{
				throw new MigrationItemLastUpdatedInTheFutureTransientException(base.Job.StateLastUpdated.Value.ToLongDateString());
			}
			if (utcNow - base.Job.StateLastUpdated > ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MigrationJobInactiveThreshold"))
			{
				MigrationLogger.Log(MigrationEventType.Information, "Job {0} has been inactive too long {1} ... autostopping", new object[]
				{
					base.Job,
					utcNow - base.Job.StateLastUpdated
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
		protected override LegacyMigrationJobProcessorResponse StopProcessing()
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			JobItemOperationResult jobItemOperationResult = base.FindAndRunJobItemOperation(MigrationJobSyncCompletedProcessor.StoppableStatusArray, MigrationUserStatus.IncrementalFailed, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"), (MigrationUserStatus status, int itemCount) => MigrationJobItem.GetMigratableByStateLastUpdated(base.DataProvider, base.Job, null, status, itemCount), delegate(MigrationJobItem item)
			{
				base.SubscriptionHandler.StopUnderlyingSubscriptions(item);
				return true;
			});
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(jobItemOperationResult.NumItemsProcessed);
			legacyMigrationJobProcessorResponse.NumItemsTransitioned = new int?(jobItemOperationResult.NumItemsTransitioned);
			if (legacyMigrationJobProcessorResponse.NumItemsProcessed > 0)
			{
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Working;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003DF8C File Offset: 0x0003C18C
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			ExDateTime exDateTime = ExDateTime.UtcNow - base.Job.IncrementalSyncInterval.Value;
			int num = base.GetJobItemCount(MigrationJobSyncCompletedProcessor.IncrementalSyncingStatusArray);
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(base.Job.CompleteAfterMoveSyncCompleted ? MigrationProcessorResult.Completed : MigrationProcessorResult.Waiting, null);
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(0);
			legacyMigrationJobProcessorResponse.NumItemsTransitioned = new int?(0);
			if (!base.SubscriptionHandler.SupportsActiveIncrementalSync && !base.Job.AutoComplete)
			{
				int num2 = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize");
				if (!base.SlotProvider.AvailableIncrementalSyncSlots.IsUnlimited)
				{
					num2 = base.SlotProvider.AvailableInitialSeedingSlots.Value;
				}
				if (scheduleNewWork && num2 > 0)
				{
					JobItemOperationResult jobItemOperationResult = base.ResumeJobItems(MigrationJobSyncCompletedProcessor.IncrementalSyncPollingStatusArray, MigrationUserStatus.IncrementalSyncing, MigrationUserStatus.IncrementalFailed, new ExDateTime?(exDateTime), num2, MigrationSlotType.IncrementalSync);
					num += jobItemOperationResult.NumItemsProcessed;
					legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult.NumItemsTransitioned;
				}
			}
			else
			{
				JobItemOperationResult jobItemOperationResult2 = base.SyncJobItems(MigrationJobSyncCompletedProcessor.IncrementalSyncPollingStatusArray, MigrationUserStatus.IncrementalFailed, exDateTime, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"));
				legacyMigrationJobProcessorResponse.NumItemsProcessed += jobItemOperationResult2.NumItemsProcessed;
				legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult2.NumItemsTransitioned;
			}
			if (num > 0)
			{
				JobItemOperationResult jobItemOperationResult3 = base.SyncJobItems(MigrationJobSyncCompletedProcessor.IncrementalSyncingStatusArray, MigrationUserStatus.IncrementalFailed, exDateTime, num);
				legacyMigrationJobProcessorResponse.NumItemsProcessed += jobItemOperationResult3.NumItemsProcessed;
				legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult3.NumItemsTransitioned;
				legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(base.GetJobItemCount(new MigrationUserStatus[]
				{
					MigrationUserStatus.IncrementalSyncing
				}));
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003E200 File Offset: 0x0003C400
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobSyncCompletedProcessor>(this);
		}

		// Token: 0x04000513 RID: 1299
		private static readonly MigrationUserStatus[] IncrementalSyncingStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Syncing,
			MigrationUserStatus.IncrementalSyncing
		};

		// Token: 0x04000514 RID: 1300
		private static readonly MigrationUserStatus[] IncrementalSyncPollingStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Synced,
			MigrationUserStatus.IncrementalFailed
		};

		// Token: 0x04000515 RID: 1301
		private static readonly MigrationUserStatus[] StoppableStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Queued,
			MigrationUserStatus.Syncing,
			MigrationUserStatus.Synced,
			MigrationUserStatus.IncrementalSyncing,
			MigrationUserStatus.IncrementalFailed
		};
	}
}
