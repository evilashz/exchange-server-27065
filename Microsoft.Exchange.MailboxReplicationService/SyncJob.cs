using System;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009A RID: 154
	internal class SyncJob : PagedMergeJob
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000367A0 File Offset: 0x000349A0
		protected override int CopyEndPercentage
		{
			get
			{
				return 95;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000367A4 File Offset: 0x000349A4
		private int GetActionsPageSize
		{
			get
			{
				return base.GetConfig<int>("GetActionsPageSize");
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000367B4 File Offset: 0x000349B4
		public override void Initialize(TransactionalRequestJob syncRequest)
		{
			base.Initialize(syncRequest);
			base.RequestJobIdentity = syncRequest.Identity.ToString();
			base.IncrementalSyncInterval = syncRequest.IncrementalSyncInterval;
			Guid targetExchangeGuid = syncRequest.TargetExchangeGuid;
			MailboxCopierFlags mailboxCopierFlags = MailboxCopierFlags.None;
			LocalizedString sourceTracingID = LocalizedString.Empty;
			switch (syncRequest.SyncProtocol)
			{
			case SyncProtocol.Imap:
				mailboxCopierFlags |= MailboxCopierFlags.Imap;
				sourceTracingID = MrsStrings.ImapTracingId(syncRequest.EmailAddress.ToString());
				break;
			case SyncProtocol.Eas:
				mailboxCopierFlags |= MailboxCopierFlags.Eas;
				sourceTracingID = MrsStrings.EasTracingId(syncRequest.EmailAddress.ToString());
				break;
			case SyncProtocol.Pop:
				mailboxCopierFlags |= MailboxCopierFlags.Pop;
				sourceTracingID = MrsStrings.PopTracingId(syncRequest.EmailAddress.ToString());
				break;
			}
			string orgID = (syncRequest.OrganizationId != null && syncRequest.OrganizationId.OrganizationalUnit != null) ? (syncRequest.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			base.MailboxMerger = new MailboxMerger(Guid.Empty, targetExchangeGuid, syncRequest, this, mailboxCopierFlags, sourceTracingID, MrsStrings.PrimaryMailboxTracingId(orgID, targetExchangeGuid));
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000368E4 File Offset: 0x00034AE4
		protected override void PerformFolderRecoverySync(MailboxChanges changes, MailboxContentsCrawler crawler)
		{
			foreach (FolderChangesManifest folderChangesManifest in from folderChanges in changes.FolderChanges.Values
			where folderChanges.FolderRecoverySync
			select folderChanges)
			{
				FolderMapping folder = base.MailboxMerger.SourceHierarchy[folderChangesManifest.FolderId] as FolderMapping;
				crawler.ResetFolder(folder);
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00036974 File Offset: 0x00034B74
		protected override void FinalizeMerge()
		{
			base.CheckBadItemCount(true);
			base.SyncStage = SyncStage.IncrementalSync;
			base.OverallProgress = this.CopyEndPercentage;
			base.ScheduleWorkItem(new Action(this.AutoSuspendJob), WorkloadType.Unknown);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000369A4 File Offset: 0x00034BA4
		protected override void ScheduleIncrementalSync()
		{
			if (base.MailboxMerger.CanReplay && this.GetActionsPageSize > 0)
			{
				if (this.IsInteractive)
				{
					base.ScheduleWorkItem(new PeriodicWorkItem(ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("ActivatedJobIncrementalSyncInterval"), new Action(this.ReplayActions)));
				}
				else
				{
					base.ScheduleWorkItem(new Action(this.ReplayActions), WorkloadType.Unknown);
				}
			}
			base.ScheduleIncrementalSync();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00036A94 File Offset: 0x00034C94
		private void AutoSuspendJob()
		{
			DateTime? timestamp = base.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter);
			DateTime? timestamp2 = base.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter);
			TimeSpan incrementalSyncInterval = base.CachedRequestJob.IncrementalSyncInterval;
			DateTime? nextSchedule = BaseJob.GetNextScheduledTime(timestamp2, timestamp, incrementalSyncInterval);
			if (nextSchedule != null)
			{
				base.Report.Append(MrsStrings.ReportSyncedJob(nextSchedule.Value.ToLocalTime()));
				base.SaveState(SaveStateFlags.Regular, delegate(TransactionalRequestJob mergeRequest)
				{
					mergeRequest.Status = RequestStatus.Synced;
					this.TimeTracker.CurrentState = RequestState.AutoSuspended;
					this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(nextSchedule.Value));
					mergeRequest.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenSynced);
				});
				return;
			}
			base.SaveState(SaveStateFlags.Regular, delegate(TransactionalRequestJob mergeRequest)
			{
				mergeRequest.Status = RequestStatus.Completed;
				base.TimeTracker.CurrentState = RequestState.Completed;
				mergeRequest.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.ReportRequestCompleted);
			});
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00036B54 File Offset: 0x00034D54
		private void ReplayActions()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			MrsTracer.Service.Debug("WorkItem: replay actions.", new object[0]);
			MergeSyncContext syncContext = (MergeSyncContext)base.MailboxMerger.CreateSyncContext();
			this.EnumerateAndReplayActions(base.MailboxMerger, syncContext);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00036BA0 File Offset: 0x00034DA0
		private void EnumerateAndReplayActions(MailboxMerger mbxContext, MergeSyncContext syncContext)
		{
			if (mbxContext.ReplaySyncState == null)
			{
				mbxContext.ReplaySyncState = new ReplaySyncState();
			}
			string text = mbxContext.ReplaySyncState.ProviderState;
			bool flag = true;
			int num = 0;
			while (flag)
			{
				MergeSyncContext mergeSyncContext = (MergeSyncContext)base.MailboxMerger.CreateSyncContext();
				string text2;
				ReplayActionsQueue andTranslateActions = mbxContext.GetAndTranslateActions(text, this.GetActionsPageSize, mergeSyncContext, out text2, out flag);
				base.Report.Append(MrsStrings.ReportReplayActionsEnumerated(mbxContext.TargetTracingID, andTranslateActions.Count, num));
				try
				{
					mbxContext.ReplayActions(andTranslateActions, mergeSyncContext);
					text = text2;
					syncContext.NumberOfActionsReplayed += mergeSyncContext.NumberOfActionsReplayed;
					syncContext.NumberOfActionsIgnored += mergeSyncContext.NumberOfActionsIgnored;
				}
				catch (MailboxReplicationTransientException)
				{
					if (mergeSyncContext.LastActionProcessed != null)
					{
						text = mergeSyncContext.LastActionProcessed.Watermark;
					}
				}
				finally
				{
					base.Report.Append(MrsStrings.ReportReplayActionsSynced(mbxContext.TargetTracingID, mergeSyncContext.NumberOfActionsReplayed, mergeSyncContext.NumberOfActionsIgnored));
					mbxContext.ReplaySyncState.ProviderState = text;
					mbxContext.SaveReplaySyncState();
				}
				num++;
			}
			base.Report.Append(MrsStrings.ReportReplayActionsCompleted(mbxContext.TargetTracingID, syncContext.NumberOfActionsReplayed, syncContext.NumberOfActionsIgnored));
		}
	}
}
