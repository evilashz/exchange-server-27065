using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001B2 RID: 434
	[Flags]
	internal enum TaskStatus
	{
		// Token: 0x04000AA9 RID: 2729
		NoError = 0,
		// Token: 0x04000AAA RID: 2730
		DLADCrawlerFailed = 1,
		// Token: 0x04000AAB RID: 2731
		UserADCrawlerFailed = 2
	}
}
