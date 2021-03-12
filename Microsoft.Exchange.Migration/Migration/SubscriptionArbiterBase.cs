using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013F RID: 319
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionArbiterBase
	{
		// Token: 0x06001009 RID: 4105 RVA: 0x000441C4 File Offset: 0x000423C4
		protected SubscriptionArbiterBase()
		{
			this.stateTable = new Dictionary<MigrationUserStatus, Dictionary<SnapshotStatus, MigrationUserStatus>>(Enum.GetValues(typeof(MigrationUserStatus)).Length);
			this.resolveOperations = new Dictionary<MigrationUserStatus, SubscriptionArbiterBase.ResolveOperation>(Enum.GetValues(typeof(MigrationUserStatus)).Length);
			this.Initialize();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0004421C File Offset: 0x0004241C
		public static MigrationUserStatus GetEffectiveJobItemStatus(MigrationJob job, MigrationUserStatus jobItemStatus)
		{
			switch (job.Status)
			{
			case MigrationJobStatus.Created:
				return jobItemStatus;
			case MigrationJobStatus.SyncInitializing:
			case MigrationJobStatus.ProvisionStarting:
				return jobItemStatus;
			case MigrationJobStatus.SyncStarting:
				if (jobItemStatus == MigrationUserStatus.Provisioning || jobItemStatus == MigrationUserStatus.ProvisionUpdating)
				{
					return MigrationUserStatus.Queued;
				}
				return jobItemStatus;
			case MigrationJobStatus.SyncCompleting:
			case MigrationJobStatus.SyncCompleted:
				if (jobItemStatus == MigrationUserStatus.Provisioning || jobItemStatus == MigrationUserStatus.ProvisionUpdating)
				{
					throw new MigrationDataCorruptionException(string.Format(CultureInfo.InvariantCulture, "job item status {0} doesn't mesh with job status {1}", new object[]
					{
						jobItemStatus,
						job.Status
					}));
				}
				return jobItemStatus;
			case MigrationJobStatus.CompletionInitializing:
				if (jobItemStatus == MigrationUserStatus.Provisioning || jobItemStatus == MigrationUserStatus.ProvisionUpdating)
				{
					throw new MigrationDataCorruptionException(string.Format(CultureInfo.InvariantCulture, "job item status {0} doesn't mesh with job status {1}", new object[]
					{
						jobItemStatus,
						job.Status
					}));
				}
				return jobItemStatus;
			case MigrationJobStatus.CompletionStarting:
				if (jobItemStatus == MigrationUserStatus.Provisioning || jobItemStatus == MigrationUserStatus.ProvisionUpdating)
				{
					throw new MigrationDataCorruptionException(string.Format(CultureInfo.InvariantCulture, "job item status {0} doesn't mesh with job status {1}", new object[]
					{
						jobItemStatus,
						job.Status
					}));
				}
				if (jobItemStatus == MigrationUserStatus.Syncing)
				{
					return MigrationUserStatus.Completing;
				}
				if (jobItemStatus == MigrationUserStatus.CompletionSynced && !job.UpdateSourceOnFinalization)
				{
					return MigrationUserStatus.Completed;
				}
				return jobItemStatus;
			case MigrationJobStatus.Completing:
			case MigrationJobStatus.Completed:
			case MigrationJobStatus.Removing:
				if (jobItemStatus == MigrationUserStatus.Provisioning || jobItemStatus == MigrationUserStatus.ProvisionUpdating || jobItemStatus == MigrationUserStatus.Syncing || jobItemStatus == MigrationUserStatus.Synced || jobItemStatus == MigrationUserStatus.Completing)
				{
					throw new MigrationDataCorruptionException(string.Format(CultureInfo.InvariantCulture, "job item status {0} doesn't mesh with job status {1}", new object[]
					{
						jobItemStatus,
						job.Status
					}));
				}
				return jobItemStatus;
			case MigrationJobStatus.Failed:
			case MigrationJobStatus.Corrupted:
				return jobItemStatus;
			}
			throw new MigrationDataCorruptionException("Unsupported job status " + job.Status);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000443D0 File Offset: 0x000425D0
		public bool TryResolveMigrationUserStatus(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription, out MigrationUserStatus resolvedStatus, out LocalizedException error, out SnapshotStatus? resolvedSubscriptionStatus)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			MigrationUserStatus effectiveJobItemStatus = SubscriptionArbiterBase.GetEffectiveJobItemStatus(job, jobItem.Status);
			Dictionary<SnapshotStatus, MigrationUserStatus> dictionary;
			if (!this.stateTable.TryGetValue(effectiveJobItemStatus, out dictionary))
			{
				throw new MigrationDataCorruptionException("job item status not supported " + effectiveJobItemStatus);
			}
			if (!dictionary.TryGetValue(subscription.Status, out resolvedStatus))
			{
				throw new MigrationDataCorruptionException(string.Format(CultureInfo.InvariantCulture, "SubscriptionArbiter. job item status {0} subscription status {1} not supported", new object[]
				{
					effectiveJobItemStatus,
					subscription.Status
				}));
			}
			error = null;
			resolvedSubscriptionStatus = null;
			SubscriptionArbiterBase.ResolveOperation resolveOperation;
			if (this.resolveOperations.TryGetValue(resolvedStatus, out resolveOperation))
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "for job item status {0} found resolve operation {1}", new object[]
				{
					effectiveJobItemStatus,
					resolveOperation
				});
				SubscriptionArbiterBase.StatusAndError statusAndError = resolveOperation(job, jobItem, subscription);
				if (statusAndError != null)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "for job item status {0} and subs status {1} default state status {2} is now {3}", new object[]
					{
						effectiveJobItemStatus,
						subscription.Status,
						resolvedStatus,
						statusAndError
					});
					resolvedStatus = statusAndError.Status;
					error = statusAndError.Error;
					resolvedSubscriptionStatus = statusAndError.SubscriptionStatus;
				}
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "for job item status {0} and subscription status {1} got new status {2}", new object[]
			{
				effectiveJobItemStatus,
				subscription.Status,
				resolvedStatus
			});
			return resolvedStatus != effectiveJobItemStatus || error != null;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00044574 File Offset: 0x00042774
		protected static LocalizedException ResolveLocalizedError(MigrationJobItem jobItem, SubscriptionSnapshot subscription, MigrationUserStatus resolvedStatus)
		{
			LocalizedString? localizedString = subscription.ErrorMessage;
			if ((localizedString == null || localizedString.Value.IsEmpty) && resolvedStatus == jobItem.Status)
			{
				localizedString = jobItem.LocalizedError;
			}
			if (localizedString == null || localizedString.Value.IsEmpty)
			{
				localizedString = new LocalizedString?(Strings.UnknownMigrationError);
			}
			MigrationLogger.Log(MigrationEventType.Information, "Arbiter: for job item {0} resolved status {1} subscription {2}, using error {3}", new object[]
			{
				jobItem,
				resolvedStatus,
				subscription,
				localizedString.Value
			});
			return new MigrationPermanentException(localizedString.Value);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00044618 File Offset: 0x00042818
		protected SubscriptionArbiterBase.StatusAndError ResolveJobItemCorrupted(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			if (subscription.ErrorMessage != null)
			{
				throw new MigrationPermanentException(subscription.ErrorMessage.Value);
			}
			throw new MigrationUnknownException();
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00044670 File Offset: 0x00042870
		protected SubscriptionArbiterBase.StatusAndError ResolveJobItemFailed(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			MigrationUserStatus migrationUserStatus = subscription.IsInitialSyncComplete ? MigrationUserStatus.IncrementalFailed : MigrationUserStatus.Failed;
			if (job.AutoComplete)
			{
				migrationUserStatus = MigrationUserStatus.CompletionFailed;
			}
			if (subscription.Status == SnapshotStatus.Suspended)
			{
				return new SubscriptionArbiterBase.StatusAndError(migrationUserStatus, new ExternallySuspendedException());
			}
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationIncrementalSyncFailureThreshold");
			if (jobItem.IncrementalSyncFailures >= config)
			{
				return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.Failed, new TooManyIncrementalSyncFailuresException(SubscriptionArbiterBase.ResolveLocalizedError(jobItem, subscription, migrationUserStatus)));
			}
			return new SubscriptionArbiterBase.StatusAndError(migrationUserStatus, SubscriptionArbiterBase.ResolveLocalizedError(jobItem, subscription, migrationUserStatus));
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00044700 File Offset: 0x00042900
		protected SubscriptionArbiterBase.StatusAndError ResolveJobItemSyncing(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			if (subscription.IsInitialSyncComplete && !job.AutoComplete)
			{
				TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationTimeOutForFailingSubscriptions");
				if (job.SupportsSyncTimeouts && subscription.IsTimedOut(config))
				{
					return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.IncrementalFailed, new SyncTimeoutException(ItemStateTransitionHelper.LocalizeTimeSpan(config)));
				}
				if (subscription.Status == SnapshotStatus.AutoSuspended)
				{
					return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.Synced);
				}
				return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.IncrementalSyncing);
			}
			else
			{
				MigrationUserStatus status = job.AutoComplete ? MigrationUserStatus.CompletionFailed : MigrationUserStatus.Failed;
				if (subscription.Status == SnapshotStatus.AutoSuspended)
				{
					return new SubscriptionArbiterBase.StatusAndError(status, new ExternallySuspendedException());
				}
				TimeSpan config2 = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MRSInitialSyncSubscriptionTimeout");
				if (job.SupportsSyncTimeouts && subscription.IsTimedOut(config2))
				{
					return new SubscriptionArbiterBase.StatusAndError(status, new SyncTimeoutException(ItemStateTransitionHelper.LocalizeTimeSpan(config2)))
					{
						SubscriptionStatus = new SnapshotStatus?(SnapshotStatus.Suspended)
					};
				}
				return null;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x000447E4 File Offset: 0x000429E4
		protected SubscriptionArbiterBase.StatusAndError ResolveJobItemIncrementalSyncing(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			if (!subscription.IsInitialSyncComplete)
			{
				return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.Syncing);
			}
			TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MRSInitialSyncSubscriptionTimeout");
			if (job.SupportsSyncTimeouts && subscription.IsTimedOut(config))
			{
				return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.IncrementalFailed, new SyncTimeoutException(ItemStateTransitionHelper.LocalizeTimeSpan(config)));
			}
			return null;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00044850 File Offset: 0x00042A50
		protected void SetResolveStatus(MigrationUserStatus status, SnapshotStatus subStatus, MigrationUserStatus resolvedStatus)
		{
			Dictionary<SnapshotStatus, MigrationUserStatus> dictionary;
			if (!this.stateTable.TryGetValue(status, out dictionary))
			{
				dictionary = new Dictionary<SnapshotStatus, MigrationUserStatus>(Enum.GetValues(typeof(SnapshotStatus)).Length);
				this.stateTable[status] = dictionary;
			}
			MigrationUserStatus migrationUserStatus;
			if (dictionary.TryGetValue(subStatus, out migrationUserStatus))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "SubscriptionArbiter. Cannot override resolved status for jobItemStatus {0} subscription status {1} of {2} with {3}", new object[]
				{
					status,
					subStatus,
					migrationUserStatus,
					resolvedStatus
				}));
			}
			dictionary[subStatus] = resolvedStatus;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000448E8 File Offset: 0x00042AE8
		protected void SetDefaultResolveStatus(MigrationUserStatus status, SnapshotStatus subStatus)
		{
			this.SetResolveStatus(status, subStatus, status);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000448F4 File Offset: 0x00042AF4
		protected void SetResolveOperation(MigrationUserStatus status, SubscriptionArbiterBase.ResolveOperation resolveOperation)
		{
			SubscriptionArbiterBase.ResolveOperation resolveOperation2;
			if (this.resolveOperations.TryGetValue(status, out resolveOperation2))
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "overriding default operation {0} with {1} for status {2}", new object[]
				{
					resolveOperation2,
					resolveOperation,
					status
				});
			}
			this.resolveOperations[status] = resolveOperation;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00044944 File Offset: 0x00042B44
		protected virtual void Initialize()
		{
			this.SetDefaultResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.InProgress);
			this.SetDefaultResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.AutoSuspended);
			this.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.Synced, MigrationUserStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.Suspended, MigrationUserStatus.Failed);
			this.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.Failed, MigrationUserStatus.Failed);
			this.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.Corrupted, MigrationUserStatus.Failed);
			this.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.Removed, MigrationUserStatus.Corrupted);
			this.SetResolveOperation(MigrationUserStatus.Syncing, new SubscriptionArbiterBase.ResolveOperation(this.ResolveJobItemSyncing));
			this.SetResolveOperation(MigrationUserStatus.Failed, new SubscriptionArbiterBase.ResolveOperation(this.ResolveJobItemFailed));
			this.SetResolveOperation(MigrationUserStatus.IncrementalFailed, new SubscriptionArbiterBase.ResolveOperation(this.ResolveJobItemFailed));
			this.SetResolveOperation(MigrationUserStatus.Corrupted, new SubscriptionArbiterBase.ResolveOperation(this.ResolveJobItemCorrupted));
			this.SetDefaultResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.AutoSuspended);
			this.SetDefaultResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.Suspended, MigrationUserStatus.Failed);
			this.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.InProgress, MigrationUserStatus.IncrementalSyncing);
			this.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.Failed, MigrationUserStatus.IncrementalFailed);
			this.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.Corrupted, MigrationUserStatus.IncrementalFailed);
			this.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.Removed, MigrationUserStatus.Corrupted);
			this.SetDefaultResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.InProgress);
			this.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.Synced, MigrationUserStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.AutoSuspended, MigrationUserStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.Suspended, MigrationUserStatus.Failed);
			this.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.Failed, MigrationUserStatus.IncrementalFailed);
			this.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.Corrupted, MigrationUserStatus.IncrementalFailed);
			this.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.Removed, MigrationUserStatus.Corrupted);
			this.SetResolveOperation(MigrationUserStatus.IncrementalSyncing, new SubscriptionArbiterBase.ResolveOperation(this.ResolveJobItemIncrementalSyncing));
			this.SetDefaultResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.Failed);
			this.SetDefaultResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.Corrupted);
			this.SetResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.InProgress, MigrationUserStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.Synced, MigrationUserStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.AutoSuspended, MigrationUserStatus.Synced);
			this.SetResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.Suspended, MigrationUserStatus.Failed);
			this.SetResolveStatus(MigrationUserStatus.IncrementalFailed, SnapshotStatus.Removed, MigrationUserStatus.Corrupted);
		}

		// Token: 0x040005A0 RID: 1440
		private readonly Dictionary<MigrationUserStatus, Dictionary<SnapshotStatus, MigrationUserStatus>> stateTable;

		// Token: 0x040005A1 RID: 1441
		private readonly Dictionary<MigrationUserStatus, SubscriptionArbiterBase.ResolveOperation> resolveOperations;

		// Token: 0x02000140 RID: 320
		// (Invoke) Token: 0x06001016 RID: 4118
		protected delegate SubscriptionArbiterBase.StatusAndError ResolveOperation(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription);

		// Token: 0x02000141 RID: 321
		protected class StatusAndError
		{
			// Token: 0x06001019 RID: 4121 RVA: 0x00044AAE File Offset: 0x00042CAE
			public StatusAndError(MigrationUserStatus status)
			{
				this.Status = status;
			}

			// Token: 0x0600101A RID: 4122 RVA: 0x00044ABD File Offset: 0x00042CBD
			public StatusAndError(MigrationUserStatus status, LocalizedException errorMessage)
			{
				this.Status = status;
				this.Error = errorMessage;
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x0600101B RID: 4123 RVA: 0x00044AD3 File Offset: 0x00042CD3
			// (set) Token: 0x0600101C RID: 4124 RVA: 0x00044ADB File Offset: 0x00042CDB
			public MigrationUserStatus Status { get; set; }

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x0600101D RID: 4125 RVA: 0x00044AE4 File Offset: 0x00042CE4
			// (set) Token: 0x0600101E RID: 4126 RVA: 0x00044AEC File Offset: 0x00042CEC
			public LocalizedException Error { get; set; }

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x0600101F RID: 4127 RVA: 0x00044AF5 File Offset: 0x00042CF5
			// (set) Token: 0x06001020 RID: 4128 RVA: 0x00044AFD File Offset: 0x00042CFD
			public SnapshotStatus? SubscriptionStatus { get; set; }

			// Token: 0x06001021 RID: 4129 RVA: 0x00044B08 File Offset: 0x00042D08
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					this.Status.ToString(),
					":",
					this.Error,
					":",
					this.SubscriptionStatus
				});
			}
		}
	}
}
