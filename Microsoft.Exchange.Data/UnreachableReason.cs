using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A7 RID: 167
	[Flags]
	internal enum UnreachableReason
	{
		// Token: 0x0400027E RID: 638
		None = 0,
		// Token: 0x0400027F RID: 639
		NoMdb = 1,
		// Token: 0x04000280 RID: 640
		NoRouteToMdb = 2,
		// Token: 0x04000281 RID: 641
		NoRouteToMta = 4,
		// Token: 0x04000282 RID: 642
		NonBHExpansionServer = 8,
		// Token: 0x04000283 RID: 643
		NoMatchingConnector = 16,
		// Token: 0x04000284 RID: 644
		IncompatibleDeliveryDomain = 32
	}
}
