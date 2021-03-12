using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000066 RID: 102
	public enum QuotaType
	{
		// Token: 0x040003F2 RID: 1010
		Undefined,
		// Token: 0x040003F3 RID: 1011
		StorageWarningLimit,
		// Token: 0x040003F4 RID: 1012
		StorageOverQuotaLimit,
		// Token: 0x040003F5 RID: 1013
		StorageShutoff,
		// Token: 0x040003F6 RID: 1014
		DumpsterWarningLimit,
		// Token: 0x040003F7 RID: 1015
		DumpsterShutoff,
		// Token: 0x040003F8 RID: 1016
		MailboxMessagesPerFolderCountWarningQuota,
		// Token: 0x040003F9 RID: 1017
		MailboxMessagesPerFolderCountReceiveQuota,
		// Token: 0x040003FA RID: 1018
		DumpsterMessagesPerFolderCountWarningQuota,
		// Token: 0x040003FB RID: 1019
		DumpsterMessagesPerFolderCountReceiveQuota,
		// Token: 0x040003FC RID: 1020
		FolderHierarchyChildrenCountWarningQuota,
		// Token: 0x040003FD RID: 1021
		FolderHierarchyChildrenCountReceiveQuota,
		// Token: 0x040003FE RID: 1022
		FolderHierarchyDepthWarningQuota,
		// Token: 0x040003FF RID: 1023
		FolderHierarchyDepthReceiveQuota,
		// Token: 0x04000400 RID: 1024
		FoldersCountWarningQuota,
		// Token: 0x04000401 RID: 1025
		FoldersCountReceiveQuota
	}
}
