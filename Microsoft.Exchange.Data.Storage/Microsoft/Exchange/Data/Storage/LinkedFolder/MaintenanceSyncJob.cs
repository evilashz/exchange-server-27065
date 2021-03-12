using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000976 RID: 2422
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MaintenanceSyncJob : TeamMailboxSyncJob
	{
		// Token: 0x060059B7 RID: 22967 RVA: 0x00173560 File Offset: 0x00171760
		public MaintenanceSyncJob(JobQueue queue, Configuration config, TeamMailboxSyncInfo syncInfoEntry, string clientString, SyncOption syncOption) : base(queue, config, syncInfoEntry, clientString, syncOption)
		{
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x00173624 File Offset: 0x00171824
		public override void Begin(object state)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					base.SafeInitializeLoggingStream();
					MaintenanceSynchronizer maintenanceSynchronizer = new MaintenanceSynchronizer(this, base.SyncInfoEntry.MailboxSession, base.SyncInfoEntry.MailboxPrincipal.MailboxInfo.OrganizationId, base.SyncInfoEntry.WebCollectionUrl, base.SyncInfoEntry.WebId, base.SyncInfoEntry.SiteUrl, base.SyncInfoEntry.DisplayName, base.SyncInfoEntry.ResourceMonitor, this.credentials, ((TeamMailboxSyncConfiguration)base.Config).UseOAuth, ((TeamMailboxSyncConfiguration)base.Config).HttpDebugEnabled, this.syncCycleLogStream);
					maintenanceSynchronizer.BeginExecute(new AsyncCallback(this.OnMaintenanceSynchronizationCompleted), maintenanceSynchronizer);
				});
			}
			catch (GrayException lastError)
			{
				base.LastError = lastError;
				this.End();
			}
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x0017366C File Offset: 0x0017186C
		private void OnMaintenanceSynchronizationCompleted(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new InvalidOperationException("TeamMailboxSyncJob.OnMaintenanceSynchronizationCompleted: asyncResult cannot be null here.");
			}
			MaintenanceSynchronizer maintenanceSynchronizer = asyncResult.AsyncState as MaintenanceSynchronizer;
			if (maintenanceSynchronizer == null)
			{
				throw new InvalidOperationException("TeamMailboxSyncJob.OnMaintenanceSynchronizationCompleted: asyncResult.AsyncState is not MaintenanceSynchronizer");
			}
			maintenanceSynchronizer.EndExecute(asyncResult);
			if (maintenanceSynchronizer.LastError != null)
			{
				base.LastError = maintenanceSynchronizer.LastError;
			}
			this.End();
		}
	}
}
