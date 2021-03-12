using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderAssistantLogger : PublicFolderMailboxLogger
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x000586E0 File Offset: 0x000568E0
		public PublicFolderAssistantLogger(IPublicFolderSession publicFolderSession) : base(publicFolderSession, "PublicFolderAssistantInfo", "PublicFolderLastAssistantCycleLog", null)
		{
			this.logComponent = "PublicFolderAssistantLog";
			this.logSuffixName = "PublicFolderAssistantLog";
			using (DisposeGuard disposeGuard = this.Guard())
			{
				base.LogEvent(LogEventType.Entry, "Start");
				disposeGuard.Success();
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00058758 File Offset: 0x00056958
		protected override void LogFinalFoldersStats()
		{
			base.LogEvent(LogEventType.Entry, "Completed");
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00058766 File Offset: 0x00056966
		public static void LogOnServer(Exception exception)
		{
			PublicFolderMailboxLoggerBase.LogOnServer(exception, "PublicFolderAssistantLog", "PublicFolderAssistantLog");
		}

		// Token: 0x04000958 RID: 2392
		private const string LogComponent = "PublicFolderAssistantLog";

		// Token: 0x04000959 RID: 2393
		private const string LogSuffix = "PublicFolderAssistantLog";

		// Token: 0x0400095A RID: 2394
		internal const string PublicFolderDeletedItemExpiration = "PublicFolderDeletedItemExpiration";

		// Token: 0x0400095B RID: 2395
		internal const string PublicFolderContentsFromMove = "PublicFolderContentsFromMove";
	}
}
