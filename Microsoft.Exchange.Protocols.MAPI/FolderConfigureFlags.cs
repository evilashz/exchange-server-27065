using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum FolderConfigureFlags
	{
		// Token: 0x0400010D RID: 269
		None = 0,
		// Token: 0x0400010E RID: 270
		CreateSearchFolder = 1,
		// Token: 0x0400010F RID: 271
		CreateIpmFolder = 2,
		// Token: 0x04000110 RID: 272
		CreateLastWriterWinsFolder = 4,
		// Token: 0x04000111 RID: 273
		InstantSearch = 8,
		// Token: 0x04000112 RID: 274
		OptimizedConversationSearch = 16,
		// Token: 0x04000113 RID: 275
		InternalAccess = 32
	}
}
