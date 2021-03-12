using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001B9 RID: 441
	[Flags]
	internal enum CreateMessageFlags
	{
		// Token: 0x040005AA RID: 1450
		None = 0,
		// Token: 0x040005AB RID: 1451
		ContentAggregation = 1,
		// Token: 0x040005AC RID: 1452
		Associated = 64,
		// Token: 0x040005AD RID: 1453
		DeferredErrors = 8
	}
}
