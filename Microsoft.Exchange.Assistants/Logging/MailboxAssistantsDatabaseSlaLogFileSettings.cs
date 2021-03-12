using System;

namespace Microsoft.Exchange.Assistants.Logging
{
	// Token: 0x020000BE RID: 190
	internal class MailboxAssistantsDatabaseSlaLogFileSettings : MailboxAssistantsSlaReportLogFileSettings
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001B937 File Offset: 0x00019B37
		protected override string LogSubFolderName
		{
			get
			{
				return "MailboxAssistantsDatabaseSlaLog";
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001B93E File Offset: 0x00019B3E
		protected override string LogTypeName
		{
			get
			{
				return "MailboxAssistantsDatabaseSlaLog";
			}
		}

		// Token: 0x04000364 RID: 868
		internal const string DatabaseSlaLogName = "MailboxAssistantsDatabaseSlaLog";
	}
}
