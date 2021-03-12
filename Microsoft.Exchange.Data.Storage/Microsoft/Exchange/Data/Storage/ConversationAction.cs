using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000899 RID: 2201
	[Flags]
	internal enum ConversationAction : uint
	{
		// Token: 0x04002CCF RID: 11471
		None = 0U,
		// Token: 0x04002CD0 RID: 11472
		AlwaysMove = 1U,
		// Token: 0x04002CD1 RID: 11473
		AlwaysDelete = 2U,
		// Token: 0x04002CD2 RID: 11474
		AlwaysCategorize = 8U,
		// Token: 0x04002CD3 RID: 11475
		AlwaysClutterOrUnclutter = 16U,
		// Token: 0x04002CD4 RID: 11476
		AlwaysMoveOrDelete = 3U,
		// Token: 0x04002CD5 RID: 11477
		All = 27U
	}
}
