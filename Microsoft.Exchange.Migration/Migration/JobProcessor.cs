using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200010E RID: 270
	internal abstract class JobProcessor : DisposeTrackableBase
	{
		// Token: 0x06000E0E RID: 3598 RVA: 0x0003A064 File Offset: 0x00038264
		protected JobProcessor()
		{
			this.slotProvider = new Lazy<BasicMigrationSlotProvider>(new Func<BasicMigrationSlotProvider>(this.InitializeSlotProvider), LazyThreadSafetyMode.ExecutionAndPublication);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0003A084 File Offset: 0x00038284
		internal virtual bool SupportsInterrupting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003A087 File Offset: 0x00038287
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0003A08F File Offset: 0x0003828F
		private protected IMigrationDataProvider DataProvider { protected get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0003A098 File Offset: 0x00038298
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0003A0A0 File Offset: 0x000382A0
		private protected MigrationSession Session { protected get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0003A0A9 File Offset: 0x000382A9
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0003A0B1 File Offset: 0x000382B1
		private protected MigrationJob Job { protected get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003A0BA File Offset: 0x000382BA
		protected ILegacySubscriptionHandler SubscriptionHandler
		{
			get
			{
				if (this.subscriptionHandler == null)
				{
					this.subscriptionHandler = LegacySubscriptionHandlerBase.CreateSubscriptionHandler(this.DataProvider, this.Job);
				}
				return this.subscriptionHandler;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003A0E1 File Offset: 0x000382E1
		protected BasicMigrationSlotProvider SlotProvider
		{
			get
			{
				return this.slotProvider.Value;
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003A0F0 File Offset: 0x000382F0
		internal static JobProcessor CreateJobProcessor(MigrationJob job)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			switch (job.Status)
			{
			case MigrationJobStatus.SyncInitializing:
				return JobSyncInitializingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.SyncStarting:
				return MigrationJobSyncStartingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.SyncCompleting:
				return MigrationJobSyncCompletingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.SyncCompleted:
				return MigrationJobSyncCompletedProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.CompletionInitializing:
				return MigrationJobCompletionInitializingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.CompletionStarting:
				return MigrationJobCompletionStartingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.Completing:
				return MigrationJobCompletingProcessor.CreateProcessor(job.MigrationType, job.IsStaged);
			case MigrationJobStatus.Completed:
				return MigrationJobCompletedProcessor.CreateProcessor(job.MigrationType, job.SupportsMultiBatchFinalization);
			case MigrationJobStatus.Removing:
				return MigrationJobRemovingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.ProvisionStarting:
				return MigrationJobProvisionStartingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.Validating:
				return MigrationJobValidatingProcessor.CreateProcessor(job.MigrationType);
			case MigrationJobStatus.Stopped:
				return MigrationJobStoppedProcessor.CreateProcessor(job.MigrationType);
			}
			return null;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003A1F1 File Offset: 0x000383F1
		internal void Initialize(IMigrationDataProvider dataProvider, MigrationSession session, MigrationJob job)
		{
			this.DataProvider = dataProvider;
			this.Session = session;
			this.Job = job;
		}

		// Token: 0x06000E1A RID: 3610
		internal abstract bool Validate();

		// Token: 0x06000E1B RID: 3611
		internal abstract MigrationJobStatus GetNextStageStatus();

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003A208 File Offset: 0x00038408
		internal virtual void OnComplete()
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003A20C File Offset: 0x0003840C
		internal LegacyMigrationJobProcessorResponse Process()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse;
			if (this.ShouldStopProcessing())
			{
				legacyMigrationJobProcessorResponse = this.StopProcessing();
			}
			else
			{
				legacyMigrationJobProcessorResponse = this.Process(true);
			}
			this.RunFullScan(legacyMigrationJobProcessorResponse);
			if (this.slotProvider.IsValueCreated && this.SlotProvider != BasicMigrationSlotProvider.Unlimited)
			{
				MigrationEndpointBase migrationEndpointBase = this.Job.SourceEndpoint ?? this.Job.TargetEndpoint;
				MigrationUtil.AssertOrThrow(migrationEndpointBase != null, "Endpoint should be non-null or unlimited after processing.", new object[0]);
				using (IMigrationDataProvider providerForFolder = this.DataProvider.GetProviderForFolder(MigrationFolderName.Settings))
				{
					using (IMigrationMessageItem migrationMessageItem = providerForFolder.FindMessage(migrationEndpointBase.StoreObjectId, BasicMigrationSlotProvider.PropertyDefinition))
					{
						migrationMessageItem.OpenAsReadWrite();
						this.SlotProvider.WriteCachedCountsToMessageItem(migrationMessageItem);
						migrationMessageItem.Save(SaveMode.ResolveConflicts);
					}
				}
			}
			MigrationLogger.Log(MigrationEventType.Information, "Job type {0}, status {1}, result {2}, length {3}, job {4}", new object[]
			{
				this.Job.MigrationType,
				this.Job.Status,
				legacyMigrationJobProcessorResponse.Result,
				stopwatch.Elapsed.TotalSeconds,
				this.Job
			});
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E1E RID: 3614
		protected abstract LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork);

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003A374 File Offset: 0x00038574
		protected virtual void AutoCancelIfTooManyErrors()
		{
			if (this.Job.IsCancelled)
			{
				return;
			}
			int jobItemCount = this.GetJobItemCount(new MigrationUserStatus[]
			{
				MigrationUserStatus.Failed
			});
			if (jobItemCount < ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationMinimumFailureCountForAutoCancel"))
			{
				return;
			}
			if (jobItemCount >= ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationAbsoluteFailureCountForAutoCancel"))
			{
				this.Job.StopJob(this.DataProvider, this.Session.Config, JobCancellationStatus.CancelledDueToHighFailureCount);
				return;
			}
			int totalItemCount = this.Job.TotalItemCount;
			if (totalItemCount > 0)
			{
				double num = (double)(ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationFailureRatioForAutoCancel") / 100);
				double num2 = (double)jobItemCount / (double)totalItemCount;
				if (num2 > num)
				{
					this.Job.StopJob(this.DataProvider, this.Session.Config, JobCancellationStatus.CancelledDueToHighFailureCount);
				}
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003A424 File Offset: 0x00038624
		protected virtual bool ShouldStopProcessing()
		{
			return this.Job.IsCancelled;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003A431 File Offset: 0x00038631
		protected virtual LegacyMigrationJobProcessorResponse StopProcessing()
		{
			return this.Process(false);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003A43A File Offset: 0x0003863A
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.subscriptionHandler != null)
				{
					this.subscriptionHandler.Dispose();
				}
				this.subscriptionHandler = null;
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003A45C File Offset: 0x0003865C
		protected void CheckIfJobExceededThreshold(ExDateTime? beginTime, TimeSpan threshold)
		{
			if (beginTime == null)
			{
				beginTime = this.Job.StateLastUpdated;
				if (beginTime == null)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} has no beginning time to check if threshold reached", new object[]
					{
						this.Job
					});
					return;
				}
			}
			TimeSpan timeSpan = new TimeSpan(ExDateTime.UtcNow.UtcTicks - beginTime.Value.UtcTicks);
			if (timeSpan > threshold)
			{
				MigrationLogger.Log(MigrationEventType.Warning, "Job {0} has exceed threshold of {1} with {2}", new object[]
				{
					this.Job,
					threshold,
					timeSpan
				});
				return;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} has NOT exceed threshold of {1} with {2}", new object[]
			{
				this.Job,
				threshold,
				timeSpan
			});
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003A5E8 File Offset: 0x000387E8
		protected JobItemOperationResult ResumeJobItems(MigrationUserStatus[] sourceStatuses, MigrationUserStatus targetStatus, MigrationUserStatus targetFailedStatus, ExDateTime? cutoffTime, int count, MigrationSlotType slotType = MigrationSlotType.IncrementalSync)
		{
			MigrationUtil.ThrowOnNullArgument(sourceStatuses, "sourceStatuses");
			return this.FindAndRunJobItemOperation(sourceStatuses, targetFailedStatus, count, (MigrationUserStatus status, int itemCount) => MigrationJobItem.GetMigratableByStateLastUpdated(this.DataProvider, this.Job, cutoffTime, status, itemCount), delegate(MigrationJobItem item)
			{
				bool result;
				using (BasicMigrationSlotProvider.SlotAcquisitionGuard slotAcquisitionGuard = this.SlotProvider.AcquireSlot(item, slotType, this.DataProvider))
				{
					this.SubscriptionHandler.SyncSubscriptionSettings(item);
					this.SubscriptionHandler.ResumeUnderlyingSubscriptions(targetStatus, item);
					slotAcquisitionGuard.Success();
					result = true;
				}
				return result;
			});
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003A670 File Offset: 0x00038870
		protected JobItemOperationResult SyncJobItems(MigrationUserStatus[] findStatuses, MigrationUserStatus targetFailedStatus, ExDateTime cutoffTime, int count)
		{
			MigrationUtil.ThrowOnNullArgument(findStatuses, "findStatuses");
			return this.FindAndRunJobItemOperation(findStatuses, targetFailedStatus, count, (MigrationUserStatus jobItemStatus, int itemCount) => this.SubscriptionHandler.GetJobItemsForSubscriptionCheck(new ExDateTime?(cutoffTime), jobItemStatus, itemCount), new Func<MigrationJobItem, bool>(this.SyncJobItemToSubscription));
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003A6BE File Offset: 0x000388BE
		protected int GetJobItemCount(params MigrationUserStatus[] statuses)
		{
			return MigrationJobItem.GetCount(this.DataProvider, this.Job.JobId, statuses);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003A6D7 File Offset: 0x000388D7
		protected int GetJobItemCount(ExDateTime? lastRestartTime, params MigrationUserStatus[] statuses)
		{
			return MigrationJobItem.GetCount(this.DataProvider, this.Job.JobId, lastRestartTime, statuses);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003A6F4 File Offset: 0x000388F4
		protected void RunFullScan(LegacyMigrationJobProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Completed || (response.NumItemsTransitioned != null && response.NumItemsTransitioned > 0 && this.Job.ShouldLazyRescan))
			{
				this.Job.UpdateCachedItemCounts(this.DataProvider);
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003A755 File Offset: 0x00038955
		protected IEnumerable<MigrationJobItem> GetJobItemsByStatus(MigrationUserStatus status, int? maxCount)
		{
			return MigrationJobItem.GetByStatus(this.DataProvider, this.Job, status, maxCount);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003A76C File Offset: 0x0003896C
		protected JobItemOperationResult FindAndRunJobItemBatchOperation(MigrationUserStatus[] findStatuses, int count, Func<MigrationUserStatus, int, IEnumerable<MigrationJobItem>> jobItemFilter, Func<IEnumerable<MigrationJobItem>, JobItemOperationResult> jobItemBatchOperation)
		{
			MigrationUtil.ThrowOnNullArgument(findStatuses, "findStatuses");
			JobItemOperationResult jobItemOperationResult = default(JobItemOperationResult);
			if (count <= 0)
			{
				return jobItemOperationResult;
			}
			foreach (MigrationUserStatus migrationUserStatus in findStatuses)
			{
				IEnumerable<MigrationJobItem> arg = jobItemFilter(migrationUserStatus, count - jobItemOperationResult.NumItemsProcessed);
				JobItemOperationResult value = jobItemBatchOperation(arg);
				MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} operated on {1} job items for status {2}", new object[]
				{
					this.Job,
					value.NumItemsProcessed,
					migrationUserStatus
				});
				jobItemOperationResult += value;
				if (jobItemOperationResult.NumItemsProcessed >= count)
				{
					break;
				}
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} operated on {1} jobs hope to operate on max of {2}", new object[]
			{
				this.Job,
				jobItemOperationResult.NumItemsProcessed,
				count
			});
			return jobItemOperationResult;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		protected JobItemOperationResult FindAndRunJobItemOperation(MigrationUserStatus[] findStatuses, MigrationUserStatus targetFailedStatus, int count, Func<MigrationUserStatus, int, IEnumerable<MigrationJobItem>> jobItemFilter, Func<MigrationJobItem, bool> jobItemOperation)
		{
			MigrationUtil.ThrowOnNullArgument(findStatuses, "findStatuses");
			return this.FindAndRunJobItemBatchOperation(findStatuses, count, jobItemFilter, delegate(IEnumerable<MigrationJobItem> jobItems)
			{
				JobProcessor.<>c__DisplayClass9.<>c__DisplayClassb <>c__DisplayClassb = new JobProcessor.<>c__DisplayClass9.<>c__DisplayClassb();
				<>c__DisplayClassb.result = default(JobItemOperationResult);
				using (IEnumerator<MigrationJobItem> enumerator = jobItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MigrationJobItem item = enumerator.Current;
						MigrationProcessorResult migrationProcessorResult = ItemStateTransitionHelper.RunJobItemOperation(this.Job, item, this.DataProvider, targetFailedStatus, delegate
						{
							try
							{
								JobProcessor.<>c__DisplayClass9.<>c__DisplayClassb cs$<>8__localsc = <>c__DisplayClassb;
								cs$<>8__localsc.result.NumItemsProcessed = cs$<>8__localsc.result.NumItemsProcessed + 1;
								if (jobItemOperation(item))
								{
									JobProcessor.<>c__DisplayClass9.<>c__DisplayClassb cs$<>8__localsc2 = <>c__DisplayClassb;
									cs$<>8__localsc2.result.NumItemsTransitioned = cs$<>8__localsc2.result.NumItemsTransitioned + 1;
									JobProcessor.<>c__DisplayClass9.<>c__DisplayClassb cs$<>8__localsc3 = <>c__DisplayClassb;
									cs$<>8__localsc3.result.NumItemsSuccessful = cs$<>8__localsc3.result.NumItemsSuccessful + 1;
								}
							}
							catch (MigrationSlotCapacityExceededException exception)
							{
								MigrationLogger.Log(MigrationEventType.Verbose, exception, "Not enough capacity at the slot provider to perform the operation.", new object[0]);
							}
						});
						if (migrationProcessorResult == MigrationProcessorResult.Failed)
						{
							JobProcessor.<>c__DisplayClass9.<>c__DisplayClassb <>c__DisplayClassb2 = <>c__DisplayClassb;
							<>c__DisplayClassb2.result.NumItemsTransitioned = <>c__DisplayClassb2.result.NumItemsTransitioned + 1;
						}
					}
				}
				return <>c__DisplayClassb.result;
			});
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003AA14 File Offset: 0x00038C14
		protected bool SyncJobItemToSubscription(MigrationJobItem item)
		{
			this.SubscriptionHandler.SyncSubscriptionSettings(item);
			MigrationSlotType consumedSlotType = item.ConsumedSlotType;
			Guid migrationSlotProviderGuid = item.MigrationSlotProviderGuid;
			MigrationUserStatus status = item.Status;
			this.SubscriptionHandler.SyncToUnderlyingSubscriptions(item);
			if (consumedSlotType != MigrationSlotType.None && migrationSlotProviderGuid != Guid.Empty)
			{
				bool flag = consumedSlotType != item.ConsumedSlotType || migrationSlotProviderGuid != item.MigrationSlotProviderGuid;
				if (flag)
				{
					this.SlotProvider.ReleaseSlot(consumedSlotType);
				}
				else
				{
					bool flag2 = item.Status != MigrationUserStatus.Completing && item.Status != MigrationUserStatus.IncrementalSyncing && item.Status != MigrationUserStatus.Syncing;
					if (flag2 && item.ConsumedSlotType != MigrationSlotType.None && item.MigrationSlotProviderGuid != Guid.Empty)
					{
						this.SlotProvider.ReleaseSlot(item, this.DataProvider);
					}
				}
			}
			if (item.Status != MigrationUserStatus.Syncing && item.Status != MigrationUserStatus.IncrementalSyncing)
			{
				MigrationUserStatus status2 = item.Status;
			}
			return item.Status != status;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003AB08 File Offset: 0x00038D08
		protected LegacyMigrationJobProcessorResponse ProcessActions(bool scheduleNewWork, params Func<bool, LegacyMigrationJobProcessorResponse>[] processorActions)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (Func<bool, LegacyMigrationJobProcessorResponse> func in processorActions)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse2 = func(scheduleNewWork);
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(";");
				}
				stringBuilder.Append(string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
				{
					base.GetType().Name,
					num++,
					stopwatch.Elapsed.TotalSeconds,
					legacyMigrationJobProcessorResponse2.NumItemsProcessed,
					legacyMigrationJobProcessorResponse2.NumItemsOutstanding
				}));
				legacyMigrationJobProcessorResponse = MigrationProcessorResponse.MergeResponses<LegacyMigrationJobProcessorResponse>(legacyMigrationJobProcessorResponse, legacyMigrationJobProcessorResponse2);
				if (legacyMigrationJobProcessorResponse2.Result != MigrationProcessorResult.Completed)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} ran processor {1}, response {2}", new object[]
					{
						this.Job,
						func,
						legacyMigrationJobProcessorResponse
					});
					break;
				}
			}
			legacyMigrationJobProcessorResponse.DebugInfo = stringBuilder.ToString();
			if (legacyMigrationJobProcessorResponse.Result == MigrationProcessorResult.Failed)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Job {0} failed stage, marking it complete", new object[]
				{
					this.Job
				});
				legacyMigrationJobProcessorResponse.Result = MigrationProcessorResult.Completed;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003AC60 File Offset: 0x00038E60
		private BasicMigrationSlotProvider InitializeSlotProvider()
		{
			MigrationLogger.Log(MigrationEventType.Instrumentation, "Getting a slot provider for processor {0}", new object[]
			{
				base.GetType().Name
			});
			MigrationEndpointBase migrationEndpointBase = this.Job.SourceEndpoint ?? this.Job.TargetEndpoint;
			if (migrationEndpointBase != null)
			{
				migrationEndpointBase.SlotProvider.UpdateAllocationCounts(this.DataProvider);
			}
			if (migrationEndpointBase != null)
			{
				return migrationEndpointBase.SlotProvider;
			}
			return BasicMigrationSlotProvider.Unlimited;
		}

		// Token: 0x040004FF RID: 1279
		private readonly Lazy<BasicMigrationSlotProvider> slotProvider;

		// Token: 0x04000500 RID: 1280
		private ILegacySubscriptionHandler subscriptionHandler;
	}
}
