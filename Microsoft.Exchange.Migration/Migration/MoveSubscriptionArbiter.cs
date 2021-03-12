using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000160 RID: 352
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MoveSubscriptionArbiter : SubscriptionArbiterBase
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x0004813F File Offset: 0x0004633F
		private MoveSubscriptionArbiter()
		{
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00048147 File Offset: 0x00046347
		public static MoveSubscriptionArbiter Instance
		{
			get
			{
				return MoveSubscriptionArbiter.soleInstance;
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00048150 File Offset: 0x00046350
		protected override void Initialize()
		{
			base.Initialize();
			base.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.Finalized, MigrationUserStatus.Completed);
			base.SetResolveStatus(MigrationUserStatus.Syncing, SnapshotStatus.CompletedWithWarning, MigrationUserStatus.CompletedWithWarnings);
			base.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.Finalized, MigrationUserStatus.Completed);
			base.SetResolveStatus(MigrationUserStatus.Synced, SnapshotStatus.CompletedWithWarning, MigrationUserStatus.CompletedWithWarnings);
			base.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.Finalized, MigrationUserStatus.Completed);
			base.SetResolveStatus(MigrationUserStatus.IncrementalSyncing, SnapshotStatus.CompletedWithWarning, MigrationUserStatus.CompletedWithWarnings);
			base.SetDefaultResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.InProgress);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.AutoSuspended, MigrationUserStatus.Synced);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.Synced, MigrationUserStatus.Synced);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.Suspended, MigrationUserStatus.CompletionFailed);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.Failed, MigrationUserStatus.CompletionFailed);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.Corrupted, MigrationUserStatus.CompletionFailed);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.Removed, MigrationUserStatus.Corrupted);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.Finalized, MigrationUserStatus.Completed);
			base.SetResolveStatus(MigrationUserStatus.Completing, SnapshotStatus.CompletedWithWarning, MigrationUserStatus.CompletedWithWarnings);
			base.SetResolveOperation(MigrationUserStatus.Completing, new SubscriptionArbiterBase.ResolveOperation(MoveSubscriptionArbiter.ResolveMoveItemCompleting));
			base.SetResolveOperation(MigrationUserStatus.CompletionFailed, new SubscriptionArbiterBase.ResolveOperation(MoveSubscriptionArbiter.ResolveJobItemCompletionFailed));
			base.SetResolveOperation(MigrationUserStatus.CompletedWithWarnings, new SubscriptionArbiterBase.ResolveOperation(MoveSubscriptionArbiter.ResolveJobItemCompletedWithWarnings));
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0004822C File Offset: 0x0004642C
		private static SubscriptionArbiterBase.StatusAndError ResolveMoveItemCompleting(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			if (job.FinalizeTime == null)
			{
				throw new MigrationDataCorruptionException("the job should have been finalized" + job);
			}
			TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationInitialSyncTimeOutForFailingSubscriptions");
			if (job.SupportsSyncTimeouts && subscription.IsTimedOut(config))
			{
				return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.CompletionFailed, new SyncTimeoutException(ItemStateTransitionHelper.LocalizeTimeSpan(config)));
			}
			return null;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000482AC File Offset: 0x000464AC
		private static SubscriptionArbiterBase.StatusAndError ResolveJobItemCompletionFailed(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			if (subscription.Status == SnapshotStatus.Suspended)
			{
				return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.CompletionFailed, new ExternallySuspendedException());
			}
			return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.CompletionFailed, SubscriptionArbiterBase.ResolveLocalizedError(jobItem, subscription, MigrationUserStatus.CompletionFailed));
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00048300 File Offset: 0x00046500
		private static SubscriptionArbiterBase.StatusAndError ResolveJobItemCompletedWithWarnings(MigrationJob job, MigrationJobItem jobItem, SubscriptionSnapshot subscription)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.ThrowOnNullArgument(subscription, "subscription");
			return new SubscriptionArbiterBase.StatusAndError(MigrationUserStatus.CompletedWithWarnings, new MigrationPermanentException(subscription.ErrorMessage ?? Strings.UnknownMigrationError));
		}

		// Token: 0x040005FB RID: 1531
		private static readonly MoveSubscriptionArbiter soleInstance = new MoveSubscriptionArbiter();
	}
}
