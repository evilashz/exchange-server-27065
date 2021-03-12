using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200011D RID: 285
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobSyncStartingProcessor : JobProcessor
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003EC7C File Offset: 0x0003CE7C
		internal static MigrationJobSyncStartingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type != MigrationType.IMAP && type != MigrationType.ExchangeOutlookAnywhere)
				{
					if (type != MigrationType.BulkProvisioning)
					{
						goto IL_37;
					}
					throw new NotSupportedException("Not a valid state for Bulk Provisioning");
				}
			}
			else if (type != MigrationType.ExchangeRemoteMove && type != MigrationType.ExchangeLocalMove && type != MigrationType.PublicFolder)
			{
				goto IL_37;
			}
			return new MigrationJobSyncStartingProcessor();
			IL_37:
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003ECD5 File Offset: 0x0003CED5
		internal override bool Validate()
		{
			return true;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003ECD8 File Offset: 0x0003CED8
		internal override MigrationJobStatus GetNextStageStatus()
		{
			if (base.Job.IsCancelled)
			{
				return MigrationJobStatus.SyncCompleted;
			}
			if (!base.Job.AutoComplete)
			{
				return MigrationJobStatus.SyncCompleting;
			}
			if (base.Job.CompleteAfterMoveSyncNotCompleted)
			{
				return MigrationJobStatus.SyncCompleted;
			}
			return MigrationJobStatus.CompletionStarting;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003EE38 File Offset: 0x0003D038
		protected sealed override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			int jobItemCount = base.GetJobItemCount(MigrationJobSyncStartingProcessor.SyncingJobItemStatusArray);
			int jobItemCount2 = base.GetJobItemCount(MigrationJobSyncStartingProcessor.QueuedJobItemStatusArray);
			LegacyMigrationJobProcessorResponse response = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			if (jobItemCount > 0 || (scheduleNewWork && jobItemCount2 > 0))
			{
				response.NumItemsOutstanding = new int?(0);
				response.NumItemsProcessed = new int?(0);
				response.NumItemsTransitioned = new int?(0);
				bool flag = jobItemCount > 0;
				if (scheduleNewWork)
				{
					base.SlotProvider.UpdateAllocationCounts(base.DataProvider);
					int num = Math.Min(base.SlotProvider.AvailableInitialSeedingSlots.IsUnlimited ? int.MaxValue : base.SlotProvider.AvailableInitialSeedingSlots.Value, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"));
					MigrationLogger.Log(MigrationEventType.Verbose, "Creating subscriptions. Slots available: {0}", new object[]
					{
						num
					});
					foreach (MigrationJobItem migrationJobItem in base.Job.GetItemsByStatus(base.DataProvider, MigrationUserStatus.Queued, new int?(num)))
					{
						flag = true;
						MigrationJobItem item = migrationJobItem;
						MigrationProcessorResult migrationProcessorResult = ItemStateTransitionHelper.RunJobItemOperation(base.Job, migrationJobItem, base.DataProvider, MigrationUserStatus.Failed, delegate
						{
							using (BasicMigrationSlotProvider.SlotAcquisitionGuard slotAcquisitionGuard = this.SlotProvider.AcquireSlot(item, MigrationSlotType.InitialSeeding, this.DataProvider))
							{
								MigrationLogger.Log(MigrationEventType.Verbose, "Acquired 1 slot to job item, {0} available.", new object[]
								{
									this.SlotProvider.AvailableInitialSeedingSlots
								});
								if (this.SubscriptionHandler.CreateUnderlyingSubscriptions(item))
								{
									response.NumItemsProcessed++;
									response.NumItemsTransitioned++;
									slotAcquisitionGuard.Success();
								}
							}
						});
						if (migrationProcessorResult == MigrationProcessorResult.Failed)
						{
							response.NumItemsTransitioned++;
						}
					}
				}
				if (flag)
				{
					ExDateTime cutoffTime = ExDateTime.UtcNow - ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationInitialSyncStartPollingTimeout");
					int count = Math.Min(base.SlotProvider.MaximumConcurrentMigrations.IsUnlimited ? base.Job.TotalItemCount : base.SlotProvider.MaximumConcurrentMigrations.Value, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"));
					JobItemOperationResult value = base.SyncJobItems(MigrationJobSyncStartingProcessor.SyncingJobItemStatusArray, MigrationUserStatus.Failed, cutoffTime, count);
					MigrationLogger.Log(MigrationEventType.Verbose, "Polled {0} initial active sync subscriptions", new object[]
					{
						value.NumItemsProcessed
					});
					int count2 = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize") - value.NumItemsProcessed;
					cutoffTime = ExDateTime.UtcNow - ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationPollingTimeout");
					value += base.SyncJobItems(MigrationJobSyncStartingProcessor.IncrementalSyncJobtemStatusArray, MigrationUserStatus.Failed, cutoffTime, count2);
					MigrationLogger.Log(MigrationEventType.Verbose, "Polled a total of {0} subscriptions", new object[]
					{
						value.NumItemsProcessed
					});
					response.NumItemsTransitioned += value.NumItemsTransitioned;
					jobItemCount = base.GetJobItemCount(MigrationJobSyncStartingProcessor.SyncingJobItemStatusArray);
				}
				response.NumItemsOutstanding = new int?(base.GetJobItemCount(MigrationJobSyncStartingProcessor.QueuedJobItemStatusArray) + jobItemCount);
				if (jobItemCount >= base.SlotProvider.MaximumConcurrentMigrations || base.SlotProvider.AvailableInitialSeedingSlots <= 0)
				{
					response.Result = MigrationProcessorResult.Waiting;
				}
				else if (response.NumItemsOutstanding.Value > 0)
				{
					response.Result = MigrationProcessorResult.Working;
				}
			}
			if (response.Result == MigrationProcessorResult.Completed && !base.Job.AutoComplete)
			{
				base.Job.ReportData.Append(Strings.MigrationReportJobInitialSyncComplete);
			}
			return response;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003F250 File Offset: 0x0003D450
		protected override LegacyMigrationJobProcessorResponse StopProcessing()
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(0);
			JobItemOperationResult jobItemOperationResult = base.FindAndRunJobItemOperation(MigrationJobSyncStartingProcessor.ProcessedJobItemStatusArray, MigrationUserStatus.Failed, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"), (MigrationUserStatus status, int itemCount) => base.Job.GetItemsByStatus(base.DataProvider, status, new int?(itemCount)), delegate(MigrationJobItem item)
			{
				base.SubscriptionHandler.CancelUnderlyingSubscriptions(item);
				return true;
			});
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(jobItemOperationResult.NumItemsProcessed);
			legacyMigrationJobProcessorResponse.NumItemsTransitioned = new int?(jobItemOperationResult.NumItemsTransitioned);
			legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(base.GetJobItemCount(MigrationJobSyncStartingProcessor.InitialSyncingJobItemStatusArray));
			if (legacyMigrationJobProcessorResponse.NumItemsProcessed > 0 || legacyMigrationJobProcessorResponse.NumItemsOutstanding > 0)
			{
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Working;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003F325 File Offset: 0x0003D525
		protected IEnumerable<MigrationJobItem> GetJobItemsForSubscriptionCreation(int maxCount)
		{
			if (maxCount > 0)
			{
				return base.Job.GetItemsByStatus(base.DataProvider, MigrationUserStatus.Queued, new int?(maxCount));
			}
			return new List<MigrationJobItem>(0);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003F34A File Offset: 0x0003D54A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobSyncStartingProcessor>(this);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003F354 File Offset: 0x0003D554
		// Note: this type is marked as 'beforefieldinit'.
		static MigrationJobSyncStartingProcessor()
		{
			MigrationUserStatus[] queuedJobItemStatusArray = new MigrationUserStatus[1];
			MigrationJobSyncStartingProcessor.QueuedJobItemStatusArray = queuedJobItemStatusArray;
			MigrationJobSyncStartingProcessor.IncrementalSyncJobtemStatusArray = new MigrationUserStatus[]
			{
				MigrationUserStatus.Synced,
				MigrationUserStatus.IncrementalFailed
			};
		}

		// Token: 0x0400051A RID: 1306
		private static readonly MigrationUserStatus[] SyncingJobItemStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Syncing,
			MigrationUserStatus.IncrementalSyncing
		};

		// Token: 0x0400051B RID: 1307
		private static readonly MigrationUserStatus[] InitialSyncingJobItemStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Syncing
		};

		// Token: 0x0400051C RID: 1308
		private static readonly MigrationUserStatus[] ProcessedJobItemStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Queued,
			MigrationUserStatus.Syncing,
			MigrationUserStatus.IncrementalSyncing
		};

		// Token: 0x0400051D RID: 1309
		private static readonly MigrationUserStatus[] QueuedJobItemStatusArray;

		// Token: 0x0400051E RID: 1310
		private static readonly MigrationUserStatus[] IncrementalSyncJobtemStatusArray;
	}
}
