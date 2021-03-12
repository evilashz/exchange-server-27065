using System;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001E9 RID: 489
	[Flags]
	internal enum RootPathProcessingFlags
	{
		// Token: 0x040008C2 RID: 2242
		None = 0,
		// Token: 0x040008C3 RID: 2243
		FlagsInitialized = 1,
		// Token: 0x040008C4 RID: 2244
		FlagsDetermined = 2,
		// Token: 0x040008C5 RID: 2245
		ResponseIncludesRootPathPrefix = 4,
		// Token: 0x040008C6 RID: 2246
		FolderPathPrefixIsInbox = 8,
		// Token: 0x040008C7 RID: 2247
		UnableToProcess = 16
	}
}
