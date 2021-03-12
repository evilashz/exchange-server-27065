using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000292 RID: 658
	[Flags]
	internal enum CreateFolderFlags
	{
		// Token: 0x04000760 RID: 1888
		None = 0,
		// Token: 0x04000761 RID: 1889
		OpenIfExists = 1,
		// Token: 0x04000762 RID: 1890
		InstantSearch = 2,
		// Token: 0x04000763 RID: 1891
		OptimizedConversationSearch = 4,
		// Token: 0x04000764 RID: 1892
		CreatePublicFolderDumpster = 8,
		// Token: 0x04000765 RID: 1893
		InternalAccess = 16,
		// Token: 0x04000766 RID: 1894
		ReservedForLegacySupport = 128
	}
}
