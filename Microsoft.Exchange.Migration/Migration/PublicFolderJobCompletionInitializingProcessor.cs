using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000124 RID: 292
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PublicFolderJobCompletionInitializingProcessor : MigrationJobCompletionInitializingProcessor
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x00040C14 File Offset: 0x0003EE14
		internal override MigrationJobStatus GetNextStageStatus()
		{
			if (base.GetJobItemCount(MigrationJob.JobItemStatusForBatchCompletionErrors) == 0)
			{
				base.DataProvider.ADProvider.RemovePublicFolderMigrationLock();
				return MigrationJobStatus.CompletionStarting;
			}
			return MigrationJobStatus.SyncCompleting;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00040C44 File Offset: 0x0003EE44
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null);
			legacyMigrationJobProcessorResponse.NumItemsProcessed = new int?(0);
			legacyMigrationJobProcessorResponse.NumItemsTransitioned = new int?(0);
			int num = base.GetJobItemCount(PublicFolderJobCompletionInitializingProcessor.IncrementalSyncingStatusArray);
			int val = base.SlotProvider.AvailableInitialSeedingSlots.IsUnlimited ? base.Job.TotalItemCount : base.SlotProvider.AvailableInitialSeedingSlots.Value;
			int num2 = Math.Min(val, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize"));
			if (scheduleNewWork && num2 > 0)
			{
				JobItemOperationResult jobItemOperationResult = this.ResumeForFinalIncrementalSync(base.Job.LastFinalizationAttempt, num2);
				num += jobItemOperationResult.NumItemsProcessed;
				legacyMigrationJobProcessorResponse.NumItemsProcessed += jobItemOperationResult.NumItemsProcessed;
				legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult.NumItemsTransitioned;
			}
			if (num > 0)
			{
				ExDateTime cutoffTime = ExDateTime.UtcNow - ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationInitialSyncStartPollingTimeout");
				JobItemOperationResult jobItemOperationResult2 = base.SyncJobItems(PublicFolderJobCompletionInitializingProcessor.IncrementalSyncingStatusArray, MigrationUserStatus.IncrementalFailed, cutoffTime, Math.Min(num, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationPollingBatchSize")));
				num = base.GetJobItemCount(PublicFolderJobCompletionInitializingProcessor.IncrementalSyncingStatusArray);
				legacyMigrationJobProcessorResponse.NumItemsProcessed += jobItemOperationResult2.NumItemsProcessed;
				legacyMigrationJobProcessorResponse.NumItemsTransitioned += jobItemOperationResult2.NumItemsTransitioned;
				legacyMigrationJobProcessorResponse.NumItemsOutstanding = new int?(num);
			}
			else if (num2 > 0)
			{
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Completed;
				return legacyMigrationJobProcessorResponse;
			}
			if (num >= base.SlotProvider.MaximumConcurrentMigrations || base.SlotProvider.AvailableInitialSeedingSlots <= 0 || num >= base.Job.TotalItemCount)
			{
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Waiting;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00040ED4 File Offset: 0x0003F0D4
		private static IEnumerable<MigrationJobItem> GetMigratableByStatusLastFinalizationAttempt(IMigrationDataProvider provider, MigrationJob job, int lastJobFinalizationAttempt, MigrationUserStatus status, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(provider);
			migrationJobObjectCache.PreSeed(job);
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, job.JobId),
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationUserStatus, status),
				new ComparisonFilter(ComparisonOperator.LessThan, MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt, lastJobFinalizationAttempt)
			});
			SortBy[] sortBy = new SortBy[]
			{
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationUserStatus, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt, SortOrder.Ascending)
			};
			IEnumerable<StoreObjectId> messageIdList = provider.FindMessageIds(filter, PublicFolderJobCompletionInitializingProcessor.MigrationJobItemSubscriptionStateLastUpdated, sortBy, (IDictionary<PropertyDefinition, object> rowData) => MigrationJobItem.FilterJobItemsByColumnLastUpdated(rowData, MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated, new Guid?(job.JobId), null, new MigrationUserStatus?(status)), new int?(maxCount));
			return MigrationJobItem.LoadJobItemsWithStatus(provider, messageIdList, status, migrationJobObjectCache);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000410C8 File Offset: 0x0003F2C8
		private JobItemOperationResult ResumeForFinalIncrementalSync(int lastJobFinalizationAttempt, int slotsToUse)
		{
			return base.FindAndRunJobItemOperation(PublicFolderJobCompletionInitializingProcessor.IncrementalSyncPollingStatusArray, MigrationUserStatus.IncrementalFailed, slotsToUse, (MigrationUserStatus status, int itemCount) => PublicFolderJobCompletionInitializingProcessor.GetMigratableByStatusLastFinalizationAttempt(this.DataProvider, this.Job, lastJobFinalizationAttempt, status, itemCount), delegate(MigrationJobItem item)
			{
				bool result;
				using (BasicMigrationSlotProvider.SlotAcquisitionGuard slotAcquisitionGuard = base.SlotProvider.AcquireSlot(item, MigrationSlotType.InitialSeeding, base.DataProvider))
				{
					base.SubscriptionHandler.SyncSubscriptionSettings(item);
					int num = -base.Job.LastFinalizationAttempt;
					if (item.LastFinalizationAttempt != num)
					{
						item.PublicFolderCompletionFailures = 0;
					}
					item.LastFinalizationAttempt = num;
					base.SubscriptionHandler.ResumeUnderlyingSubscriptions(MigrationUserStatus.IncrementalSyncing, item);
					slotAcquisitionGuard.Success();
					result = true;
				}
				return result;
			});
		}

		// Token: 0x04000522 RID: 1314
		private static readonly PropertyDefinition[] MigrationJobItemSubscriptionStateLastUpdated = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MigrationBatchMessageSchema.MigrationJobId,
			MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated,
			MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
			MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt,
			MigrationBatchMessageSchema.MigrationUserStatus
		};

		// Token: 0x04000523 RID: 1315
		private static readonly MigrationUserStatus[] IncrementalSyncPollingStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Synced,
			MigrationUserStatus.IncrementalFailed
		};

		// Token: 0x04000524 RID: 1316
		private static readonly MigrationUserStatus[] IncrementalSyncingStatusArray = new MigrationUserStatus[]
		{
			MigrationUserStatus.Syncing,
			MigrationUserStatus.IncrementalSyncing
		};
	}
}
