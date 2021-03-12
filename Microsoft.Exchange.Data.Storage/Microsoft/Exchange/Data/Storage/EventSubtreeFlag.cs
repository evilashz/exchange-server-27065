using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E8 RID: 488
	[Flags]
	internal enum EventSubtreeFlag
	{
		// Token: 0x04000D96 RID: 3478
		NonIPMSubtree = 1,
		// Token: 0x04000D97 RID: 3479
		IPMSubtree = 2,
		// Token: 0x04000D98 RID: 3480
		All = 3
	}
}
