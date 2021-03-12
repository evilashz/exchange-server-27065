using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000411 RID: 1041
	public enum QuotaMessageType
	{
		// Token: 0x04001F99 RID: 8089
		WarningMailboxUnlimitedSize,
		// Token: 0x04001F9A RID: 8090
		WarningPublicFolderUnlimitedSize,
		// Token: 0x04001F9B RID: 8091
		WarningMailbox,
		// Token: 0x04001F9C RID: 8092
		WarningPublicFolder,
		// Token: 0x04001F9D RID: 8093
		ProhibitSendMailbox,
		// Token: 0x04001F9E RID: 8094
		ProhibitPostPublicFolder,
		// Token: 0x04001F9F RID: 8095
		ProhibitSendReceiveMailBox,
		// Token: 0x04001FA0 RID: 8096
		WarningMailboxMessagesPerFolderCount,
		// Token: 0x04001FA1 RID: 8097
		ProhibitReceiveMailboxMessagesPerFolderCount,
		// Token: 0x04001FA2 RID: 8098
		WarningFolderHierarchyChildrenCount,
		// Token: 0x04001FA3 RID: 8099
		ProhibitReceiveFolderHierarchyChildrenCountCount,
		// Token: 0x04001FA4 RID: 8100
		WarningMailboxMessagesPerFolderUnlimitedCount,
		// Token: 0x04001FA5 RID: 8101
		WarningFolderHierarchyChildrenUnlimitedCount,
		// Token: 0x04001FA6 RID: 8102
		WarningFolderHierarchyDepth,
		// Token: 0x04001FA7 RID: 8103
		ProhibitReceiveFolderHierarchyDepth,
		// Token: 0x04001FA8 RID: 8104
		WarningFolderHierarchyDepthUnlimited,
		// Token: 0x04001FA9 RID: 8105
		WarningFoldersCount,
		// Token: 0x04001FAA RID: 8106
		ProhibitReceiveFoldersCount,
		// Token: 0x04001FAB RID: 8107
		WarningFoldersCountUnlimited
	}
}
