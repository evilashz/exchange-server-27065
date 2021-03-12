using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001B RID: 27
	[Flags]
	public enum FolderState
	{
		// Token: 0x04000068 RID: 104
		None = 0,
		// Token: 0x04000069 RID: 105
		Created = 1,
		// Token: 0x0400006A RID: 106
		SearchFolderCopied = 2,
		// Token: 0x0400006B RID: 107
		CatchupFolderComplete = 4,
		// Token: 0x0400006C RID: 108
		CopyMessagesComplete = 8,
		// Token: 0x0400006D RID: 109
		IsGhosted = 16,
		// Token: 0x0400006E RID: 110
		PropertiesNotCopied = 32
	}
}
