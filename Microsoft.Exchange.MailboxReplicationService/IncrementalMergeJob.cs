using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000090 RID: 144
	internal class IncrementalMergeJob : MergeJob
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00031419 File Offset: 0x0002F619
		public override void ValidateAndPopulateRequestJob(List<ReportEntry> entries)
		{
			base.ValidateAndPopulateRequestJob(entries);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00031422 File Offset: 0x0002F622
		protected override int CopyStartPercentage
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00031426 File Offset: 0x0002F626
		protected override int CopyEndPercentage
		{
			get
			{
				return 95;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0003142C File Offset: 0x0002F62C
		protected override void StartMerge()
		{
			MrsTracer.Service.Debug("WorkItem: StartMerge", new object[0]);
			if (base.MailboxMerger.SyncState != null && base.MailboxMerger.ICSSyncState != null)
			{
				MrsTracer.Service.Debug("Recovering an interrupted merge.", new object[0]);
				base.CheckServersHealth();
				base.MailboxMerger.SourceMailbox.SetMailboxSyncState(base.MailboxMerger.ICSSyncState.ProviderState);
				if (base.SyncStage == SyncStage.IncrementalSync || base.SyncStage == SyncStage.FinalIncrementalSync)
				{
					base.OverallProgress = this.CopyEndPercentage;
					base.ScheduleWorkItem(new Action(this.InitializeIncrementalSync), WorkloadType.Unknown);
					base.Report.Append(MrsStrings.ReportRequestContinued(base.SyncStage.ToString()));
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestContinued, new object[]
					{
						base.RequestJobIdentity,
						base.RequestJobGuid.ToString(),
						base.SyncStage.ToString()
					});
					return;
				}
			}
			base.ScheduleWorkItem(new Action(this.CatchupFolderHierarchy), WorkloadType.Unknown);
			base.StartMerge();
			base.MailboxMerger.ICSSyncState = new MailboxMapiSyncState();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000315E4 File Offset: 0x0002F7E4
		protected override void BeforeDataCopy()
		{
			base.MailboxMerger.SourceHierarchy.EnumerateFolderHierarchy(EnumHierarchyFlags.NormalFolders | EnumHierarchyFlags.RootFolder, delegate(FolderRecWrapper folderRec, FolderMap.EnumFolderContext context)
			{
				FolderMapping folderMapping = (FolderMapping)folderRec;
				if (!folderMapping.IsIncluded)
				{
					return;
				}
				using (ISourceFolder folder = base.MailboxMerger.SourceMailbox.GetFolder(folderMapping.EntryId))
				{
					if (folder != null)
					{
						FolderRec folderRec2 = folder.GetFolderRec(base.MailboxMerger.GetAdditionalFolderPtags(), GetFolderRecFlags.None);
						base.MailboxMerger.CatchupFolder(folderRec2, folder);
					}
				}
			});
			base.MailboxMerger.ICSSyncState.ProviderState = base.MailboxMerger.SourceMailbox.GetMailboxSyncState();
			base.MailboxMerger.SaveICSSyncState(true);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0003163C File Offset: 0x0002F83C
		protected override void FinalizeMerge()
		{
			base.CheckBadItemCount(true);
			base.ReportProgress(true);
			base.SyncStage = SyncStage.IncrementalSync;
			base.OverallProgress = this.CopyEndPercentage;
			base.TimeTracker.SetTimestamp(RequestJobTimestamp.InitialSeedingCompleted, new DateTime?(DateTime.UtcNow));
			RequestJobLog.Write(base.CachedRequestJob, RequestState.InitialSeedingComplete);
			base.ScheduleWorkItem<bool>(new Action<bool>(this.IncrementalSync), true, WorkloadType.Unknown);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00031724 File Offset: 0x0002F924
		protected virtual void AutoSuspendJob()
		{
			if (base.CachedRequestJob.SuspendWhenReadyToComplete || base.CachedRequestJob.PreventCompletion)
			{
				base.Report.Append(MrsStrings.ReportAutoSuspendingJob);
				base.TimeTracker.CurrentState = RequestState.AutoSuspended;
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.Suspended, new DateTime?(DateTime.UtcNow));
				base.SaveState(SaveStateFlags.Regular, delegate(TransactionalRequestJob mergeRequest)
				{
					mergeRequest.Status = RequestStatus.AutoSuspended;
					mergeRequest.Suspend = true;
					mergeRequest.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenAutoSuspended);
				});
				return;
			}
			DateTime? timestamp = base.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter);
			DateTime? timestamp2 = base.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter);
			TimeSpan incrementalSyncInterval = base.CachedRequestJob.IncrementalSyncInterval;
			DateTime utcNow = DateTime.UtcNow;
			DateTime? nextSchedule = BaseJob.GetNextScheduledTime(timestamp2, timestamp, incrementalSyncInterval);
			base.Report.Append(MrsStrings.ReportSyncedJob(nextSchedule.Value.ToLocalTime()));
			base.SaveState(SaveStateFlags.Regular, delegate(TransactionalRequestJob mergeRequest)
			{
				mergeRequest.Status = RequestStatus.Synced;
				this.TimeTracker.CurrentState = RequestState.AutoSuspended;
				this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(nextSchedule.Value));
				mergeRequest.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenSynced);
			});
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00031824 File Offset: 0x0002FA24
		private void CatchupFolderHierarchy()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			if (string.IsNullOrEmpty(base.MailboxMerger.ICSSyncState.ProviderState))
			{
				MrsTracer.Service.Debug("WorkItem: catchup folder hierarchy.", new object[0]);
				base.MailboxMerger.SourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags.Catchup, 0);
				return;
			}
			MrsTracer.Service.Debug("Folder hierarchy sync state already exists, will not run hierarchy catchup", new object[0]);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00031894 File Offset: 0x0002FA94
		protected virtual void IncrementalSync(bool firstIncrementalSync)
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			base.TestIntegration.Barrier("PostponeSync", new Action(base.RefreshRequestIfNeeded));
			base.MailboxSnapshotTimestamp = DateTime.UtcNow;
			base.SyncStage = SyncStage.IncrementalSync;
			base.OverallProgress = this.CopyEndPercentage;
			base.TimeTracker.CurrentState = RequestState.IncrementalSync;
			MrsTracer.Service.Debug("WorkItem: incremental synchronization.", new object[0]);
			this.EnumerateAndApplyIncrementalChanges();
			base.CopyMailboxProperties();
			base.CheckBadItemCount(true);
			if (firstIncrementalSync && !base.SkipContentVerification)
			{
				base.ScheduleContentVerification();
			}
			base.ScheduleWorkItem(new Action(this.AutoSuspendJob), WorkloadType.Unknown);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00031944 File Offset: 0x0002FB44
		protected SyncContext EnumerateAndApplyIncrementalChanges()
		{
			SyncContext syncContext = base.MailboxMerger.CreateSyncContext();
			MailboxChanges mailboxChanges = new MailboxChanges(base.MailboxMerger.EnumerateHierarchyChanges(syncContext));
			base.Report.Append(MrsStrings.ReportIncrementalSyncHierarchyChanges(base.MailboxMerger.SourceTracingID, mailboxChanges.HierarchyChanges.ChangedFolders.Count, mailboxChanges.HierarchyChanges.DeletedFolders.Count));
			base.EnumerateAndApplyIncrementalChanges(base.MailboxMerger, syncContext, mailboxChanges.HierarchyChanges);
			return syncContext;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000319BE File Offset: 0x0002FBBE
		private void InitializeIncrementalSync()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			base.ComputeFolderMapping(false);
			base.ScheduleWorkItem<bool>(new Action<bool>(this.IncrementalSync), false, WorkloadType.Unknown);
		}
	}
}
