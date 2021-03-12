using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200096A RID: 2410
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class TeamMailboxSyncJob : Job
	{
		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x0600596A RID: 22890 RVA: 0x00171EF3 File Offset: 0x001700F3
		// (set) Token: 0x0600596B RID: 22891 RVA: 0x00171EFB File Offset: 0x001700FB
		public TeamMailboxSyncInfo SyncInfoEntry { get; private set; }

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x0600596C RID: 22892 RVA: 0x00171F04 File Offset: 0x00170104
		// (set) Token: 0x0600596D RID: 22893 RVA: 0x00171F0C File Offset: 0x0017010C
		public SyncOption SyncOption { get; private set; }

		// Token: 0x0600596E RID: 22894 RVA: 0x00171F18 File Offset: 0x00170118
		public TeamMailboxSyncJob(JobQueue queue, Configuration config, TeamMailboxSyncInfo syncInfoEntry, string clientString, SyncOption syncOption) : base(queue, config, clientString)
		{
			this.SyncInfoEntry = syncInfoEntry;
			this.SyncOption = syncOption;
			if (((TeamMailboxSyncConfiguration)config).UseOAuth)
			{
				this.credentials = ((TeamMailboxSyncJobQueue)queue).OAuthCredentialFactory.Get(syncInfoEntry.MailboxPrincipal.MailboxInfo.OrganizationId);
			}
			else
			{
				this.credentials = CredentialCache.DefaultCredentials;
			}
			this.loggingContext = new LoggingContext(this.SyncInfoEntry.MailboxGuid, this.SyncInfoEntry.SiteUrl, base.ClientString, null);
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x00171FA8 File Offset: 0x001701A8
		protected override void End()
		{
			try
			{
				if (base.LastError != null)
				{
					ProtocolLog.LogCycleFailure(this.loggingComponent, this.loggingContext, "The sync cycle completed with error", base.LastError);
				}
				else
				{
					ProtocolLog.LogCycleSuccess(this.loggingComponent, this.loggingContext, "The sync cycle completed successfully");
				}
				this.SafeCloseLoggingStream();
			}
			finally
			{
				base.End();
			}
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x00172010 File Offset: 0x00170210
		protected void SafeInitializeLoggingStream()
		{
			try
			{
				if (this.SyncInfoEntry.Logger != null)
				{
					this.syncCycleLogStream = this.SyncInfoEntry.Logger.GetStream();
				}
				else
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeInitializeLoggingStream: Logger is null and possibly caused by corruption of log stream configuration", new ArgumentException());
				}
			}
			catch (IOException exception)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeInitializeLoggingStream: Failed with IOException", exception);
			}
			catch (StorageTransientException exception2)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeInitializeLoggingStream: Failed with StorageTransientException", exception2);
			}
			catch (StoragePermanentException exception3)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeInitializeLoggingStream: Failed with StoragePermanentException", exception3);
			}
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x001720D8 File Offset: 0x001702D8
		protected void SafeCloseLoggingStream()
		{
			if (this.syncCycleLogStream != null)
			{
				try
				{
					this.syncCycleLogStream.SetLength(this.syncCycleLogStream.Position);
					this.syncCycleLogStream.Close();
					this.syncCycleLogStream = null;
					this.SyncInfoEntry.Logger.Save();
				}
				catch (IOException exception)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeCloseLoggingStream: Failed with IOException", exception);
				}
				catch (StorageTransientException exception2)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeCloseLoggingStream: Failed with StorageTransientException", exception2);
				}
				catch (StoragePermanentException exception3)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "SafeCloseLoggingStream: Failed with StoragePermanentException", exception3);
				}
			}
		}

		// Token: 0x04003128 RID: 12584
		private readonly LoggingContext loggingContext;

		// Token: 0x04003129 RID: 12585
		protected ProtocolLog.Component loggingComponent;

		// Token: 0x0400312A RID: 12586
		protected ICredentials credentials;

		// Token: 0x0400312B RID: 12587
		protected Stream syncCycleLogStream;
	}
}
