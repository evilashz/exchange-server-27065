using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000002 RID: 2
	internal enum AccessCheckOperation
	{
		// Token: 0x04000002 RID: 2
		PropertyGet,
		// Token: 0x04000003 RID: 3
		PropertySet,
		// Token: 0x04000004 RID: 4
		PropertyDelete,
		// Token: 0x04000005 RID: 5
		PropertyGetList,
		// Token: 0x04000006 RID: 6
		StreamOpen,
		// Token: 0x04000007 RID: 7
		StreamRead,
		// Token: 0x04000008 RID: 8
		StreamWrite,
		// Token: 0x04000009 RID: 9
		StreamSeek,
		// Token: 0x0400000A RID: 10
		StreamGetSize,
		// Token: 0x0400000B RID: 11
		StreamSetSize,
		// Token: 0x0400000C RID: 12
		FolderOpen,
		// Token: 0x0400000D RID: 13
		FolderCreate,
		// Token: 0x0400000E RID: 14
		FolderMoveMessageSource,
		// Token: 0x0400000F RID: 15
		FolderMoveMessageDestination,
		// Token: 0x04000010 RID: 16
		FolderCopyMessageSource,
		// Token: 0x04000011 RID: 17
		FolderCopyMessageDestination,
		// Token: 0x04000012 RID: 18
		FolderDeleteMessage,
		// Token: 0x04000013 RID: 19
		FolderSetReadFlag,
		// Token: 0x04000014 RID: 20
		FolderImportMessageMoveSource,
		// Token: 0x04000015 RID: 21
		FolderImportMessageMoveDestination,
		// Token: 0x04000016 RID: 22
		FolderGetMessageStatus,
		// Token: 0x04000017 RID: 23
		FolderShallowCopySource,
		// Token: 0x04000018 RID: 24
		FolderShallowCopyDestination,
		// Token: 0x04000019 RID: 25
		FolderMoveSource,
		// Token: 0x0400001A RID: 26
		FolderMoveDestination,
		// Token: 0x0400001B RID: 27
		FolderDelete,
		// Token: 0x0400001C RID: 28
		FolderSetSearchCriteria,
		// Token: 0x0400001D RID: 29
		FolderGetSearchCriteria,
		// Token: 0x0400001E RID: 30
		FolderCopyPropsSource,
		// Token: 0x0400001F RID: 31
		FolderCopyPropsDestination,
		// Token: 0x04000020 RID: 32
		FolderSetMessageStatus,
		// Token: 0x04000021 RID: 33
		FolderViewMessage,
		// Token: 0x04000022 RID: 34
		FolderViewHierarchy,
		// Token: 0x04000023 RID: 35
		MessageCreate,
		// Token: 0x04000024 RID: 36
		MessageOpen,
		// Token: 0x04000025 RID: 37
		MessageCreateEmbedded,
		// Token: 0x04000026 RID: 38
		MessageOpenEmbedded,
		// Token: 0x04000027 RID: 39
		MessageSubmit,
		// Token: 0x04000028 RID: 40
		MessageSaveChanges,
		// Token: 0x04000029 RID: 41
		MessageSetReadFlag,
		// Token: 0x0400002A RID: 42
		MessageSetMessageFlags,
		// Token: 0x0400002B RID: 43
		AttachmentCreate,
		// Token: 0x0400002C RID: 44
		AttachmentOpen,
		// Token: 0x0400002D RID: 45
		AttachmentSaveChanges,
		// Token: 0x0400002E RID: 46
		AttachmentOpenEmbeddedMessage
	}
}
