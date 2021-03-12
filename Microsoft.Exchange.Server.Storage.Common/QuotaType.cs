using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000078 RID: 120
	public enum QuotaType
	{
		// Token: 0x04000628 RID: 1576
		Undefined,
		// Token: 0x04000629 RID: 1577
		StorageWarningLimit,
		// Token: 0x0400062A RID: 1578
		StorageOverQuotaLimit,
		// Token: 0x0400062B RID: 1579
		StorageShutoff,
		// Token: 0x0400062C RID: 1580
		DumpsterWarningLimit,
		// Token: 0x0400062D RID: 1581
		DumpsterShutoff,
		// Token: 0x0400062E RID: 1582
		MailboxMessagesPerFolderCountWarningQuota,
		// Token: 0x0400062F RID: 1583
		MailboxMessagesPerFolderCountReceiveQuota,
		// Token: 0x04000630 RID: 1584
		DumpsterMessagesPerFolderCountWarningQuota,
		// Token: 0x04000631 RID: 1585
		DumpsterMessagesPerFolderCountReceiveQuota,
		// Token: 0x04000632 RID: 1586
		FolderHierarchyChildrenCountWarningQuota,
		// Token: 0x04000633 RID: 1587
		FolderHierarchyChildrenCountReceiveQuota,
		// Token: 0x04000634 RID: 1588
		FolderHierarchyDepthWarningQuota,
		// Token: 0x04000635 RID: 1589
		FolderHierarchyDepthReceiveQuota,
		// Token: 0x04000636 RID: 1590
		FoldersCountWarningQuota,
		// Token: 0x04000637 RID: 1591
		FoldersCountReceiveQuota,
		// Token: 0x04000638 RID: 1592
		NamedPropertiesCountQuota,
		// Token: 0x04000639 RID: 1593
		MaxQuotaType
	}
}
