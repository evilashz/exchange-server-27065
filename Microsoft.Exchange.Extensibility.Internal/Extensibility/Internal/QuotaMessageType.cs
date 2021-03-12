using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000067 RID: 103
	internal enum QuotaMessageType
	{
		// Token: 0x04000403 RID: 1027
		WarningMailboxUnlimitedSize,
		// Token: 0x04000404 RID: 1028
		WarningPublicFolderUnlimitedSize,
		// Token: 0x04000405 RID: 1029
		WarningMailbox,
		// Token: 0x04000406 RID: 1030
		WarningPublicFolder,
		// Token: 0x04000407 RID: 1031
		ProhibitSendMailbox,
		// Token: 0x04000408 RID: 1032
		ProhibitPostPublicFolder,
		// Token: 0x04000409 RID: 1033
		ProhibitSendReceiveMailBox,
		// Token: 0x0400040A RID: 1034
		WarningMailboxMessagesPerFolderCount,
		// Token: 0x0400040B RID: 1035
		ProhibitReceiveMailboxMessagesPerFolderCount,
		// Token: 0x0400040C RID: 1036
		WarningFolderHierarchyChildrenCount,
		// Token: 0x0400040D RID: 1037
		ProhibitReceiveFolderHierarchyChildrenCountCount,
		// Token: 0x0400040E RID: 1038
		WarningMailboxMessagesPerFolderUnlimitedCount,
		// Token: 0x0400040F RID: 1039
		WarningFolderHierarchyChildrenUnlimitedCount,
		// Token: 0x04000410 RID: 1040
		WarningFolderHierarchyDepth,
		// Token: 0x04000411 RID: 1041
		ProhibitReceiveFolderHierarchyDepth,
		// Token: 0x04000412 RID: 1042
		WarningFolderHierarchyDepthUnlimited,
		// Token: 0x04000413 RID: 1043
		WarningFoldersCount,
		// Token: 0x04000414 RID: 1044
		ProhibitReceiveFoldersCount,
		// Token: 0x04000415 RID: 1045
		WarningFoldersCountUnlimited
	}
}
