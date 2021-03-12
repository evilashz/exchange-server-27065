using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000116 RID: 278
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobCompletionStartingProcessor : JobProcessor
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		protected MigrationJobCompletionStartingProcessor()
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003CC14 File Offset: 0x0003AE14
		internal static MigrationJobCompletionStartingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type == MigrationType.IMAP || type == MigrationType.ExchangeOutlookAnywhere)
				{
					throw new NotSupportedException("Exchange/IMAP not supported in CompletionStarting state in E15!");
				}
				if (type == MigrationType.BulkProvisioning)
				{
					throw new NotSupportedException("Bulk Provisioning not supported in CompletionStarting state");
				}
			}
			else if (type == MigrationType.ExchangeRemoteMove || type == MigrationType.ExchangeLocalMove || type == MigrationType.PublicFolder)
			{
				return new MigrationJobCompletionStartingProcessor();
			}
			throw new ArgumentException("MigrationJobCompletionStartingProcessor::CreateProcessor invoked with invalid migrationtype " + type);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003CC78 File Offset: 0x0003AE78
		internal override bool Validate()
		{
			return base.Job.Status == MigrationJobStatus.CompletionStarting;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003CC88 File Offset: 0x0003AE88
		internal override MigrationJobStatus GetNextStageStatus()
		{
			return MigrationJobStatus.Completing;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} found {1} slots available for syncs before processing", new object[]
			{
				base.Job,
				base.SlotProvider.AvailableInitialSeedingSlots
			});
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null);
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(0);
			legacyMigrationJobProcessorResponse.NumItemsTransitioned = new int?(0);
			MoveJobSubscriptionSettings moveJobSubscriptionSettings = base.Job.SubscriptionSettings as MoveJobSubscriptionSettings;
			MigrationUserStatus[] array = (moveJobSubscriptionSettings != null && moveJobSubscriptionSettings.CompleteAfter != null) ? MigrationJobCompletionStartingProcessor.PollingStatusesForCompleteAfter : MigrationJobCompletionStartingProcessor.PollingStatuses;
			int num = base.GetJobItemCount(array);
			if (scheduleNewWork)
			{
				if (!base.Job.CompleteAfterMoveSyncCompleted && base.SlotProvider.AvailableInitialSeedingSlots > 0)
				{
					JobItemOperationResult jobItemOperationResult = base.ResumeJobItems(new MigrationUserStatus[]
					{
						MigrationUserStatus.Synced
					}, MigrationUserStatus.Completing, MigrationUserStatus.IncrementalFailed, null, Math.Min((base.SlotProvider.AvailableInitialSeedingSlots.IsUnlimited || base.Job.AutoComplete) ? base.Job.TotalItemCount : base.SlotProvider.AvailableInitialSeedingSlots.Value, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize")), MigrationSlotType.InitialSeeding);
					if (!base.Job.AutoComplete)
					{
						num += jobItemOperationResult.NumItemsProcessed;
						legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult.NumItemsTransitioned;
					}
				}
				if (base.SlotProvider.AvailableInitialSeedingSlots > 0)
				{
					ExDateTime effectiveFinalizationTime = base.Job.GetEffectiveFinalizationTime();
					JobItemOperationResult jobItemOperationResult2 = base.ResumeJobItems(new MigrationUserStatus[]
					{
						MigrationUserStatus.IncrementalFailed,
						MigrationUserStatus.CompletionFailed
					}, MigrationUserStatus.Completing, MigrationUserStatus.IncrementalFailed, new ExDateTime?(effectiveFinalizationTime), Math.Min((base.SlotProvider.AvailableInitialSeedingSlots.IsUnlimited || base.Job.AutoComplete) ? base.Job.TotalItemCount : base.SlotProvider.AvailableInitialSeedingSlots.Value, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize")), MigrationSlotType.InitialSeeding);
					if (!base.Job.AutoComplete)
					{
						num += jobItemOperationResult2.NumItemsProcessed;
						legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult2.NumItemsTransitioned;
					}
				}
			}
			if (num > 0 || base.Job.AutoComplete)
			{
				ExDateTime cutoffTime = ExDateTime.UtcNow - ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationInitialSyncStartPollingTimeout");
				JobItemOperationResult jobItemOperationResult3 = base.SyncJobItems(array, MigrationUserStatus.IncrementalFailed, cutoffTime, Math.Min(num, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize")));
				legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(jobItemOperationResult3.NumItemsProcessed);
				legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult3.NumItemsTransitioned;
				legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(base.GetJobItemCount(array));
			}
			if (base.SlotProvider.AvailableInitialSeedingSlots <= 0)
			{
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Waiting;
			}
			else if (legacyMigrationJobProcessorResponse.NumItemsProcessed <= 0 && (legacyMigrationJobProcessorResponse.NumItemsOutstanding == null || legacyMigrationJobProcessorResponse.NumItemsOutstanding.Value <= 0))
			{
				base.Job.ReportData.Append(Strings.MigrationReportJobComplete);
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Completed;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003D04C File Offset: 0x0003B24C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobCompletionStartingProcessor>(this);
		}

		// Token: 0x04000510 RID: 1296
		private static readonly MigrationUserStatus[] PollingStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Syncing,
			MigrationUserStatus.IncrementalSyncing,
			MigrationUserStatus.Completing
		};

		// Token: 0x04000511 RID: 1297
		private static readonly MigrationUserStatus[] PollingStatusesForCompleteAfter = new MigrationUserStatus[]
		{
			MigrationUserStatus.Syncing,
			MigrationUserStatus.IncrementalSyncing,
			MigrationUserStatus.Completing,
			MigrationUserStatus.Synced
		};
	}
}
