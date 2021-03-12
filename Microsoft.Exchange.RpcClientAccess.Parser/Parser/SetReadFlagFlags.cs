using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200033F RID: 831
	[Flags]
	internal enum SetReadFlagFlags : byte
	{
		// Token: 0x04000A8E RID: 2702
		Read = 0,
		// Token: 0x04000A8F RID: 2703
		SuppressReceipt = 1,
		// Token: 0x04000A90 RID: 2704
		FolderMessageDialog = 2,
		// Token: 0x04000A91 RID: 2705
		ClearReadFlag = 4,
		// Token: 0x04000A92 RID: 2706
		DeferredErrors = 8,
		// Token: 0x04000A93 RID: 2707
		GenerateReceiptOnly = 16,
		// Token: 0x04000A94 RID: 2708
		ClearReadNotificationPending = 32,
		// Token: 0x04000A95 RID: 2709
		ClearNonReadNotificationPending = 64
	}
}
