using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001D7 RID: 471
	[Flags]
	internal enum SetReadFlags
	{
		// Token: 0x04000652 RID: 1618
		None = 0,
		// Token: 0x04000653 RID: 1619
		SuppressReceipt = 1,
		// Token: 0x04000654 RID: 1620
		ClearRead = 4,
		// Token: 0x04000655 RID: 1621
		DeferredErrors = 8,
		// Token: 0x04000656 RID: 1622
		GenerateReceiptOnly = 16,
		// Token: 0x04000657 RID: 1623
		ClearRnPending = 32,
		// Token: 0x04000658 RID: 1624
		CleanNrnPending = 64
	}
}
