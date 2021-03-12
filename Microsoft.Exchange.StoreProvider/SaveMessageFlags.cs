using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001D8 RID: 472
	[Flags]
	internal enum SaveMessageFlags
	{
		// Token: 0x0400065A RID: 1626
		None = 0,
		// Token: 0x0400065B RID: 1627
		Unicode = -2147483648,
		// Token: 0x0400065C RID: 1628
		BestBody = 1,
		// Token: 0x0400065D RID: 1629
		PlainText = 2,
		// Token: 0x0400065E RID: 1630
		Html = 4,
		// Token: 0x0400065F RID: 1631
		Rtf = 8
	}
}
