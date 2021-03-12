using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderSynchronizerLogger : PublicFolderMailboxLogger
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00006C58 File Offset: 0x00004E58
		public PublicFolderSynchronizerLogger(PublicFolderSession publicFolderSession, FolderOperationCounter folderOperationCount, Guid correlationId) : base(publicFolderSession, "PublicFolderSyncInfo", "PublicFolderLastSyncCylceLog", new Guid?(correlationId))
		{
			ArgumentValidator.ThrowIfNull("folderOperationCount", folderOperationCount);
			this.logComponent = "PublicFolderSyncLog";
			this.logSuffixName = "PublicFolderSyncLog";
			this.folderOperationCount = folderOperationCount;
			using (DisposeGuard disposeGuard = this.Guard())
			{
				base.LogEvent(LogEventType.Entry, "Sync started");
				disposeGuard.Success();
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006CE0 File Offset: 0x00004EE0
		public static void LogOnServer(Exception exception)
		{
			PublicFolderMailboxLoggerBase.LogOnServer(exception, "PublicFolderSyncLog", "PublicFolderSyncLog");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006CF2 File Offset: 0x00004EF2
		public static void LogOnServer(string data, LogEventType eventType, Guid? transactionId = null)
		{
			PublicFolderMailboxLoggerBase.LogOnServer(data, eventType, "PublicFolderSyncLog", "PublicFolderSyncLog", transactionId);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006D06 File Offset: 0x00004F06
		public void LogFolderDeleted(byte[] entryId)
		{
			if (this.folderOperationCount.Deleted < 10)
			{
				base.LogEvent(LogEventType.Verbose, HexConverter.ByteArrayToHexString(entryId) + " is deleted");
			}
			this.folderOperationCount.Deleted++;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006D41 File Offset: 0x00004F41
		public void LogFolderUpdated(byte[] entryId)
		{
			if (this.folderOperationCount.Updated < 10)
			{
				base.LogEvent(LogEventType.Verbose, HexConverter.ByteArrayToHexString(entryId) + " is updated");
			}
			this.folderOperationCount.Updated++;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006D7C File Offset: 0x00004F7C
		public void LogFolderCreated(byte[] entryId)
		{
			if (this.folderOperationCount.Added < 10)
			{
				base.LogEvent(LogEventType.Verbose, HexConverter.ByteArrayToHexString(entryId) + " is added");
			}
			this.folderOperationCount.Added++;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006DB8 File Offset: 0x00004FB8
		protected override void LogFinalFoldersStats()
		{
			base.LogEvent(LogEventType.Statistics, this.folderOperationCount.Added + " folders have been added");
			base.LogEvent(LogEventType.Statistics, this.folderOperationCount.Updated + " folders have been updated");
			base.LogEvent(LogEventType.Statistics, this.folderOperationCount.Deleted + " folders have been deleted");
		}

		// Token: 0x0400008C RID: 140
		private const string LogComponent = "PublicFolderSyncLog";

		// Token: 0x0400008D RID: 141
		private const string LogSuffix = "PublicFolderSyncLog";

		// Token: 0x0400008E RID: 142
		private readonly FolderOperationCounter folderOperationCount;
	}
}
