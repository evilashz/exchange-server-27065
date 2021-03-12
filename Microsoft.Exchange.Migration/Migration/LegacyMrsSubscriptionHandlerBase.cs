using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000152 RID: 338
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class LegacyMrsSubscriptionHandlerBase : LegacySubscriptionHandlerBase, ILegacySubscriptionHandler, IForceReportDisposeTrackable, IDisposeTrackable, IDisposable
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x000467AF File Offset: 0x000449AF
		protected LegacyMrsSubscriptionHandlerBase(IMigrationDataProvider dataProvider, MigrationJob migrationJob, SubscriptionArbiterBase arbiter) : base(dataProvider, migrationJob)
		{
			this.SubscriptionArbiter = arbiter;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x000467C0 File Offset: 0x000449C0
		public virtual bool SupportsDupeDetection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060010C7 RID: 4295
		public abstract MigrationType SupportedMigrationType { get; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000467C3 File Offset: 0x000449C3
		public override bool SupportsActiveIncrementalSync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060010C9 RID: 4297
		protected abstract MigrationUserStatus PostTestStatus { get; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x000467C6 File Offset: 0x000449C6
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x000467CE File Offset: 0x000449CE
		private protected SubscriptionArbiterBase SubscriptionArbiter { protected get; private set; }

		// Token: 0x060010CC RID: 4300
		public abstract bool CreateUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x060010CD RID: 4301
		public abstract bool TestCreateUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x060010CE RID: 4302 RVA: 0x000467D8 File Offset: 0x000449D8
		public virtual MigrationProcessorResult SyncToUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			if (jobItem.SubscriptionId == null)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Didn't find a subscription to synchronize with for the job item {0}.", new object[]
				{
					jobItem
				});
				throw new SubscriptionNotFoundPermanentException();
			}
			SubscriptionSnapshot subscriptionSnapshot = base.SubscriptionAccessor.RetrieveSubscriptionSnapshot(jobItem.SubscriptionId);
			MigrationUserStatus value;
			LocalizedException localizedError;
			SnapshotStatus? snapshotStatus;
			if (this.SubscriptionArbiter.TryResolveMigrationUserStatus(base.Job, jobItem, subscriptionSnapshot, out value, out localizedError, out snapshotStatus))
			{
				jobItem.SetStatusAndSubscriptionLastChecked(base.DataProvider, new MigrationUserStatus?(value), localizedError, new ExDateTime?(ExDateTime.UtcNow), base.Job.SupportsIncrementalSyncs, subscriptionSnapshot);
				if (snapshotStatus != null)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "updating subscription. job-item {0} with status {1}", new object[]
					{
						jobItem,
						snapshotStatus.Value
					});
					SnapshotStatus value2 = snapshotStatus.Value;
					if (value2 != SnapshotStatus.InProgress)
					{
						if (value2 != SnapshotStatus.Removed)
						{
							if (value2 != SnapshotStatus.Suspended)
							{
								throw new InvalidOperationException("status " + snapshotStatus.Value + " not expected");
							}
							base.SubscriptionAccessor.SuspendSubscription(jobItem.SubscriptionId);
						}
						else
						{
							base.SubscriptionAccessor.RemoveSubscription(jobItem.SubscriptionId);
						}
					}
					else
					{
						base.SubscriptionAccessor.ResumeSubscription(jobItem.SubscriptionId, false);
					}
				}
			}
			else
			{
				jobItem.SetStatusAndSubscriptionLastChecked(base.DataProvider, null, null, new ExDateTime?(ExDateTime.UtcNow), true, subscriptionSnapshot);
			}
			return MigrationProcessorResult.Working;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00046944 File Offset: 0x00044B44
		public virtual void ResumeUnderlyingSubscriptions(MigrationUserStatus targetStatus, MigrationJobItem jobItem)
		{
			MigrationLogger.Log(MigrationEventType.Information, "Resuming subscription. job-item {0}", new object[]
			{
				jobItem
			});
			if (jobItem.SubscriptionId == null)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Didn't find a subscription to resume for the job item {0}.", new object[]
				{
					jobItem
				});
				throw new SubscriptionNotFoundPermanentException();
			}
			base.SubscriptionAccessor.ResumeSubscription(jobItem.SubscriptionId, targetStatus == MigrationUserStatus.Completing);
			LocalizedException localizedError = null;
			LocalizedString? localizedError2 = jobItem.LocalizedError;
			if (!string.IsNullOrEmpty((localizedError2 != null) ? localizedError2.GetValueOrDefault() : null))
			{
				LocalizedString? localizedError3 = jobItem.LocalizedError;
				localizedError = new MigrationPermanentException(new LocalizedString((localizedError3 != null) ? localizedError3.GetValueOrDefault() : null));
			}
			jobItem.SetStatusAndSubscriptionLastChecked(base.DataProvider, new MigrationUserStatus?(targetStatus), localizedError, new ExDateTime?(ExDateTime.UtcNow), true, null);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00046A18 File Offset: 0x00044C18
		public virtual void DeleteUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			if (jobItem.SubscriptionId == null)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "JobItem '{0}' has no subscription, ignoring deletion.", new object[]
				{
					jobItem
				});
				return;
			}
			base.SubscriptionAccessor.RemoveSubscription(jobItem.SubscriptionId);
			jobItem.SetSubscriptionId(base.DataProvider, null, null);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00046A78 File Offset: 0x00044C78
		public override void DisableSubscriptions(MigrationJobItem jobItem)
		{
			MigrationLogger.Log(MigrationEventType.Information, "Disabling subscription. job-item {0}", new object[]
			{
				jobItem
			});
			if (jobItem.SubscriptionId == null)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "JobItem '{0}' has no subscription ID, so not disabling it.", new object[]
				{
					jobItem
				});
				return;
			}
			base.SubscriptionAccessor.SuspendSubscription(jobItem.SubscriptionId);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00046AD0 File Offset: 0x00044CD0
		public override void SyncSubscriptionSettings(MigrationJobItem jobItem)
		{
			if (jobItem.MigrationJob == null)
			{
				throw new CouldNotUpdateSubscriptionWithoutBatchTransientException();
			}
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
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (base.SubscriptionAccessor.UpdateSubscription(jobItem.SubscriptionId, migrationEndpointBase, jobItem, false))
				{
					jobItem.SetSubscriptionLastUpdatedTime(base.DataProvider, new ExDateTime?(utcNow));
				}
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00046E7C File Offset: 0x0004507C
		protected override IEnumerable<MigrationJobItem> GetJobItemsForIncrementalSubscriptionCheck(ExDateTime? cutoffTime, MigrationUserStatus status, int maxItemsToCheck)
		{
			foreach (MigrationJobItem jobItem in MigrationJobItem.GetBySubscriptionLastChecked(base.DataProvider, base.Job, cutoffTime, status, maxItemsToCheck))
			{
				MigrationJobItem item = jobItem;
				this.RunJobItemOperation(jobItem, delegate
				{
					MigrationUtil.AssertOrThrow(this.Job.SupportsIncrementalSyncs, "Job item {0} should not be allowed for incremental sync because job does not support them!", new object[]
					{
						item
					});
					MigrationUtil.AssertOrThrow(item.SubscriptionId != null, "Job item {0} doesn't have a valid subscription ID but one is required.", new object[]
					{
						item
					});
				});
				yield return item;
			}
			yield break;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00046EAE File Offset: 0x000450AE
		protected override SnapshotStatus GetJobItemSubscriptionStatus(ISubscriptionId subscriptionId, MigrationJobItem migrationJobItem)
		{
			return base.SubscriptionAccessor.RetrieveSubscriptionStatus(subscriptionId);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00046EBC File Offset: 0x000450BC
		protected override MigrationProcessorResult RunJobItemOperation(MigrationJobItem jobItem, Action itemOperation)
		{
			MigrationUserStatus failedStatus = MigrationUserStatus.Failed;
			if (base.Job.Status == MigrationJobStatus.SyncCompleted)
			{
				failedStatus = MigrationUserStatus.IncrementalFailed;
			}
			else if (MigrationJobStage.Completion.IsStatusSupported(base.Job.Status))
			{
				failedStatus = MigrationUserStatus.CompletionFailed;
			}
			return ItemStateTransitionHelper.RunJobItemOperation(base.Job, jobItem, base.DataProvider, failedStatus, itemOperation);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00046F0C File Offset: 0x0004510C
		protected bool InternalCreate(MigrationJobItem jobItem, bool isTest)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			if (!this.DiscoverAndSetSubscriptionSettings(jobItem))
			{
				return false;
			}
			if (isTest)
			{
				base.SubscriptionAccessor.TestCreateSubscription(jobItem);
				jobItem.SetStatusAndSubscriptionLastChecked(base.DataProvider, new MigrationUserStatus?(this.PostTestStatus), null, new ExDateTime?(ExDateTime.UtcNow), true, null);
				return true;
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			SubscriptionSnapshot subscriptionSnapshot = base.SubscriptionAccessor.CreateSubscription(jobItem);
			if (subscriptionSnapshot.Id != null)
			{
				jobItem.SetSubscriptionId(base.DataProvider, (ISubscriptionId)subscriptionSnapshot.Id, new MigrationUserStatus?(MigrationUserStatus.Syncing));
				jobItem.SetSubscriptionLastUpdatedTime(base.DataProvider, new ExDateTime?(utcNow));
				return true;
			}
			MigrationUserStatus status;
			LocalizedException ex;
			SnapshotStatus? snapshotStatus;
			this.SubscriptionArbiter.TryResolveMigrationUserStatus(base.Job, jobItem, subscriptionSnapshot, out status, out ex, out snapshotStatus);
			if (ex == null)
			{
				throw MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationUnknownException>(string.Format(CultureInfo.InvariantCulture, "Snapshot {0} has no id but doesn't have an error message", new object[]
				{
					subscriptionSnapshot
				}));
			}
			jobItem.SetSubscriptionFailed(base.DataProvider, status, ex);
			return false;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00047001 File Offset: 0x00045201
		protected virtual bool DiscoverAndSetSubscriptionSettings(MigrationJobItem jobItem)
		{
			return true;
		}
	}
}
