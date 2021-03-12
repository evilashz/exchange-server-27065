using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200096B RID: 2411
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentSyncJob : TeamMailboxSyncJob
	{
		// Token: 0x06005972 RID: 22898 RVA: 0x001721A0 File Offset: 0x001703A0
		public DocumentSyncJob(JobQueue queue, Configuration config, TeamMailboxSyncInfo syncInfoEntry, string clientString, SyncOption syncOption) : base(queue, config, syncInfoEntry, clientString, syncOption)
		{
			this.loggingComponent = ProtocolLog.Component.DocumentSync;
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00172234 File Offset: 0x00170434
		public override void Begin(object state)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					base.SafeInitializeLoggingStream();
					SiteSynchronizer siteSynchronizer = new SiteSynchronizer(this, base.SyncInfoEntry.MailboxSession, base.SyncInfoEntry.ResourceMonitor, base.SyncInfoEntry.SiteUrl, this.credentials, ((TeamMailboxSyncConfiguration)base.Config).UseOAuth, ((TeamMailboxSyncConfiguration)base.Config).HttpDebugEnabled, this.syncCycleLogStream);
					siteSynchronizer.BeginExecute(new AsyncCallback(this.OnSiteSynchronizationCompleted), siteSynchronizer);
				});
			}
			catch (GrayException lastError)
			{
				base.LastError = lastError;
				this.End();
			}
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x0017227C File Offset: 0x0017047C
		private void OnSiteSynchronizationCompleted(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new InvalidOperationException("DocumentSyncJob.OnSiteSynchronizationCompleted: asyncResult cannot be null here.");
			}
			SiteSynchronizer siteSynchronizer = asyncResult.AsyncState as SiteSynchronizer;
			if (siteSynchronizer == null)
			{
				throw new InvalidOperationException("DocumentSyncJob.OnSiteSynchronizationCompleted: asyncResult.AsyncState is not SiteSynchronizer");
			}
			siteSynchronizer.EndExecute(asyncResult);
			if (siteSynchronizer.LastError != null || base.IsShuttingdown)
			{
				base.LastError = siteSynchronizer.LastError;
				this.End();
				return;
			}
			this.documentLibraryInfos = (Queue<DocumentLibraryInfo>)siteSynchronizer.SyncResult;
			this.SynchronizeNextDocumentLibrary();
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x001723B4 File Offset: 0x001705B4
		private void SynchronizeNextDocumentLibrary()
		{
			DocumentLibraryInfo info = (this.documentLibraryInfos.Count > 0) ? this.documentLibraryInfos.Dequeue() : null;
			if (info != null)
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						DocumentLibSynchronizer documentLibSynchronizer = new DocumentLibSynchronizer(this, this.SyncInfoEntry.MailboxSession, this.SyncInfoEntry.ResourceMonitor, info.FolderId, this.SyncInfoEntry.SiteUrl, info.SharepointId, this.credentials, ((TeamMailboxSyncConfiguration)this.Config).UseOAuth, ((TeamMailboxSyncConfiguration)this.Config).HttpDebugEnabled, this.syncCycleLogStream);
						documentLibSynchronizer.BeginExecute(new AsyncCallback(this.OnDocumentLibSynchronizationCompleted), documentLibSynchronizer);
					});
					return;
				}
				catch (GrayException lastError)
				{
					base.LastError = lastError;
					this.End();
					return;
				}
			}
			this.End();
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x00172438 File Offset: 0x00170638
		private void OnDocumentLibSynchronizationCompleted(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new InvalidOperationException("DocumentSyncJob.OnDocumentLibSynchronizationCompleted: asyncResult cannot be null here.");
			}
			DocumentLibSynchronizer documentLibSynchronizer = asyncResult.AsyncState as DocumentLibSynchronizer;
			if (documentLibSynchronizer == null)
			{
				throw new InvalidOperationException("DocumentSyncJob.OnDocumentLibSynchronizationCompleted: asyncResult.AsyncState is not DocumentLibSynchronizer");
			}
			documentLibSynchronizer.EndExecute(asyncResult);
			if (documentLibSynchronizer.LastError != null || base.IsShuttingdown)
			{
				base.LastError = documentLibSynchronizer.LastError;
				this.End();
				return;
			}
			this.SynchronizeNextDocumentLibrary();
		}

		// Token: 0x0400312E RID: 12590
		private Queue<DocumentLibraryInfo> documentLibraryInfos;
	}
}
