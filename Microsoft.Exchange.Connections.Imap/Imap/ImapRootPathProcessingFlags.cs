using System;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200001A RID: 26
	[Flags]
	internal enum ImapRootPathProcessingFlags
	{
		// Token: 0x04000121 RID: 289
		None = 0,
		// Token: 0x04000122 RID: 290
		FlagsInitialized = 1,
		// Token: 0x04000123 RID: 291
		FlagsDetermined = 2,
		// Token: 0x04000124 RID: 292
		ResponseIncludesRootPathPrefix = 4,
		// Token: 0x04000125 RID: 293
		FolderPathPrefixIsInbox = 8,
		// Token: 0x04000126 RID: 294
		UnableToProcess = 16
	}
}
