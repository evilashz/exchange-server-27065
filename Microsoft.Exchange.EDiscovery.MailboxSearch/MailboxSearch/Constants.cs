using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000003 RID: 3
	internal static class Constants
	{
		// Token: 0x04000008 RID: 8
		internal static readonly int ReadWriteBatchSize = 100;

		// Token: 0x04000009 RID: 9
		internal static readonly string MailboxItemIdPropertyIdSet = "2B55BDD4-255C-4C04-84B7-5B15DCCB9B15";

		// Token: 0x0400000A RID: 10
		internal static readonly string MailboxSearchRecycleFolderName = "MailboxSearchRecycleBin";

		// Token: 0x0400000B RID: 11
		internal static readonly string WorkingFolderSuffix = ".working";

		// Token: 0x0400000C RID: 12
		internal static readonly string ResultPathSeparator = "###";

		// Token: 0x0400000D RID: 13
		internal static readonly string SourceConfigurationLogItemSubjectPrefix = "configuration-";

		// Token: 0x0400000E RID: 14
		internal static readonly string SourceStatusLogItemSubjectPrefix = "status-";

		// Token: 0x0400000F RID: 15
		internal static readonly string SourceOperationStatusLogItemSubject = "operation-status";
	}
}
