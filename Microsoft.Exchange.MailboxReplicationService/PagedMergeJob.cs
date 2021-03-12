using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000096 RID: 150
	internal class PagedMergeJob : MergeJob
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000334F0 File Offset: 0x000316F0
		private TimeSpan CrawlAndCopyFolderTimeout
		{
			get
			{
				return base.GetConfig<TimeSpan>("CrawlAndCopyFolderTimeout");
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00033500 File Offset: 0x00031700
		protected override void StartMerge()
		{
			MrsTracer.Service.Debug("WorkItem: StartMerge", new object[0]);
			if (base.MailboxMerger.SyncState != null && base.MailboxMerger.ICSSyncState != null)
			{
				MrsTracer.Service.Debug("Recovering an interrupted merge.", new object[0]);
				base.CheckServersHealth();
				base.MailboxMerger.SourceMailbox.SetMailboxSyncState(base.MailboxMerger.ICSSyncState.ProviderState);
			}
			base.ScheduleWorkItem(new Action(this.CatchupFolderHierarchy), WorkloadType.Unknown);
			base.StartMerge();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00033590 File Offset: 0x00031790
		protected override void CopyMessages(IReadOnlyCollection<FolderMapping> foldersToCopy)
		{
			base.RefreshRequestIfNeeded();
			base.SyncStage = SyncStage.CopyingMessages;
			base.TimeTracker.CurrentState = RequestState.CopyingMessages;
			base.CheckServersHealth();
			foreach (FolderMapping folderMapping in foldersToCopy)
			{
				if (folderMapping.FolderType != FolderType.Search)
				{
					base.MailboxMerger.MailboxSizeTracker.TrackFolder(folderMapping.FolderRec);
					base.Report.Append(MrsStrings.ReportMergingFolder(folderMapping.FullFolderName, folderMapping.TargetFolder.FullFolderName));
				}
			}
			this.ScheduleIncrementalSync();
			this.crawler = new MailboxContentsCrawler(base.MailboxMerger, foldersToCopy);
			this.ScheduleCrawlAndCopyFolder();
			base.MailboxMerger.MailboxSizeTracker.IsFinishedEstimating = true;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00033660 File Offset: 0x00031860
		protected override void CalculateCopyingProgress()
		{
			if (base.MessagesWritten < base.TotalMessages)
			{
				base.OverallProgress = this.CopyStartPercentage + (this.CopyEndPercentage - this.CopyStartPercentage) * base.MessagesWritten / base.TotalMessages;
				return;
			}
			base.OverallProgress = this.CopyEndPercentage;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000336B0 File Offset: 0x000318B0
		protected override void UnconfigureProviders()
		{
			if (this.crawler != null)
			{
				this.crawler.Dispose();
				this.crawler = null;
			}
			base.UnconfigureProviders();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000336D2 File Offset: 0x000318D2
		protected virtual void PerformFolderRecoverySync(MailboxChanges changes, MailboxContentsCrawler crawler)
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000336D4 File Offset: 0x000318D4
		protected virtual void ScheduleIncrementalSync()
		{
			if (this.IsInteractive)
			{
				base.ScheduleWorkItem(new PeriodicWorkItem(ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("ActivatedJobIncrementalSyncInterval"), new Action(this.IncrementalSync)));
				return;
			}
			base.ScheduleWorkItem(new Action(this.IncrementalSync), WorkloadType.Unknown);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00033713 File Offset: 0x00031913
		private void ScheduleCrawlAndCopyFolder()
		{
			base.ScheduleWorkItem(new Action(this.CrawlAndCopyFolder), this.IsInteractive ? WorkloadType.MailboxReplicationServiceInternalMaintenance : WorkloadType.MailboxReplicationService);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00033734 File Offset: 0x00031934
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

		// Token: 0x06000771 RID: 1905 RVA: 0x00033814 File Offset: 0x00031A14
		private void CopyFolderPropertiesAndContents(FolderMapping folder, FolderContentsCrawler sourceFolderCrawler, bool shouldCopyProperties, TimeSpan maxOperationDuration)
		{
			PagedMergeJob.<>c__DisplayClass1 CS$<>8__locals1 = new PagedMergeJob.<>c__DisplayClass1();
			CS$<>8__locals1.folder = folder;
			CS$<>8__locals1.sourceFolderCrawler = sourceFolderCrawler;
			CS$<>8__locals1.shouldCopyProperties = shouldCopyProperties;
			CS$<>8__locals1.maxOperationDuration = maxOperationDuration;
			CS$<>8__locals1.<>4__this = this;
			using (IDestinationFolder destFolder = base.MailboxMerger.DestMailbox.GetFolder(CS$<>8__locals1.folder.TargetFolder.EntryId))
			{
				if (destFolder == null)
				{
					base.Report.Append(MrsStrings.ReportTargetFolderDeleted(CS$<>8__locals1.folder.TargetFolder.FullFolderName, TraceUtils.DumpEntryId(CS$<>8__locals1.folder.TargetFolder.EntryId), CS$<>8__locals1.folder.FullFolderName));
					return;
				}
				ExecutionContext.Create(new DataContext[]
				{
					new FolderRecWrapperDataContext(CS$<>8__locals1.folder)
				}).Execute(delegate
				{
					CS$<>8__locals1.<>4__this.MailboxMerger.CopyFolderPropertiesAndContents(CS$<>8__locals1.folder, CS$<>8__locals1.sourceFolderCrawler, destFolder, CS$<>8__locals1.shouldCopyProperties, CS$<>8__locals1.maxOperationDuration);
					destFolder.Flush();
				});
			}
			base.SaveState(SaveStateFlags.Lazy, null);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0003392C File Offset: 0x00031B2C
		private void CrawlAndCopyFolder()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			MrsTracer.Service.Debug("WorkItem: crawling and copying messages.", new object[0]);
			FolderContentsCrawler sourceFolderCrawler;
			bool shouldCopyProperties;
			FolderMapping nextFolderToCopy = this.crawler.GetNextFolderToCopy(out sourceFolderCrawler, out shouldCopyProperties);
			if (nextFolderToCopy != null)
			{
				this.CopyFolderPropertiesAndContents(nextFolderToCopy, sourceFolderCrawler, shouldCopyProperties, this.CrawlAndCopyFolderTimeout);
				this.ScheduleCrawlAndCopyFolder();
				return;
			}
			base.ScheduleWorkItem(new Action(base.CopyMailboxProperties), WorkloadType.Unknown);
			base.ScheduleWorkItem(new Action(this.FinalizeMerge), WorkloadType.Unknown);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000339AC File Offset: 0x00031BAC
		private void IncrementalSync()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			base.TestIntegration.Barrier("PostponeSync", new Action(base.RefreshRequestIfNeeded));
			MrsTracer.Service.Debug("WorkItem: incremental synchronization.", new object[0]);
			SyncContext syncContext = base.MailboxMerger.CreateSyncContext();
			MailboxChanges mailboxChanges = new MailboxChanges(base.MailboxMerger.EnumerateHierarchyChanges(syncContext));
			base.Report.Append(MrsStrings.ReportIncrementalSyncHierarchyChanges(base.MailboxMerger.SourceTracingID, mailboxChanges.HierarchyChanges.ChangedFolders.Count, mailboxChanges.HierarchyChanges.DeletedFolders.Count));
			base.EnumerateAndApplyIncrementalChanges(base.MailboxMerger, syncContext, mailboxChanges.HierarchyChanges);
			if (mailboxChanges.HasFolderRecoverySync)
			{
				this.PerformFolderRecoverySync(mailboxChanges, this.crawler);
			}
		}

		// Token: 0x04000302 RID: 770
		private MailboxContentsCrawler crawler;
	}
}
