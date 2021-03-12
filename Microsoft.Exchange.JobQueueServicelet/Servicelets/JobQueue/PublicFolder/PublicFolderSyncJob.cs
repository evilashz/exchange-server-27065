using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderSyncJob : Job
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00005999 File Offset: 0x00003B99
		public PublicFolderSyncJob(JobQueue queue, OrganizationId orgId, Guid contentMailboxGuid, bool executeReconcileFolders) : base(queue, queue.Configuration, string.Empty)
		{
			this.OrganizationId = orgId;
			this.ContentMailboxGuid = contentMailboxGuid;
			this.ExecuteReconcileFolders = executeReconcileFolders;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000059C3 File Offset: 0x00003BC3
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000059CB File Offset: 0x00003BCB
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000059D4 File Offset: 0x00003BD4
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000059DC File Offset: 0x00003BDC
		public Guid ContentMailboxGuid { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000059E5 File Offset: 0x00003BE5
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000059ED File Offset: 0x00003BED
		public bool ExecuteReconcileFolders { get; private set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00005A08 File Offset: 0x00003C08
		public override void Begin(object state)
		{
			if (this.ShouldSyncMailbox())
			{
				PublicFolderSynchronizer.Begin(this.OrganizationId, this.ContentMailboxGuid, this.ExecuteReconcileFolders, delegate(Exception exception)
				{
					base.LastError = exception;
					this.End();
				});
				return;
			}
			this.End();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005A50 File Offset: 0x00003C50
		private bool ShouldSyncMailbox()
		{
			bool result;
			try
			{
				ExchangePrincipal exchangePrincipal;
				if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(this.OrganizationId, this.ContentMailboxGuid, false, out exchangePrincipal) || exchangePrincipal == null)
				{
					PublicFolderSynchronizerLogger.LogOnServer(string.Format("Sync Cancelled for Mailbox {0} in Organization {1}. Could not get ExchangePrincipal. Mailbox could have been deleted.", this.ContentMailboxGuid, this.OrganizationId), LogEventType.Warning, null);
					result = false;
				}
				else if (!exchangePrincipal.MailboxInfo.IsRemote && LocalServerCache.LocalServer != null && exchangePrincipal.MailboxInfo.Location.ServerFqdn != LocalServerCache.LocalServer.Fqdn)
				{
					PublicFolderSynchronizerLogger.LogOnServer(string.Format("Sync Cancelled for Mailbox {0} in Organization {1}. Mailbox was moved to Server {2}.", this.ContentMailboxGuid, this.OrganizationId, exchangePrincipal.MailboxInfo.Location.ServerFqdn), LogEventType.Warning, null);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (LocalizedException e)
			{
				PublicFolderSynchronizerLogger.LogOnServer(string.Format("Sync Cancelled for Mailbox {0} in Organization {1}. Could not get ExchangePrincipal. Mailbox could have been deleted/relocated. Exception - {2}", this.ContentMailboxGuid, this.OrganizationId, PublicFolderMailboxLoggerBase.GetExceptionLogString(e, PublicFolderMailboxLoggerBase.ExceptionLogOption.All)), LogEventType.Warning, null);
				result = false;
			}
			return result;
		}
	}
}
