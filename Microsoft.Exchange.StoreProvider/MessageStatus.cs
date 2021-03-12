using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001C0 RID: 448
	[Flags]
	internal enum MessageStatus
	{
		// Token: 0x040005D0 RID: 1488
		None = 0,
		// Token: 0x040005D1 RID: 1489
		Highlighted = 1,
		// Token: 0x040005D2 RID: 1490
		Tagged = 2,
		// Token: 0x040005D3 RID: 1491
		Hidden = 4,
		// Token: 0x040005D4 RID: 1492
		DelMarked = 8,
		// Token: 0x040005D5 RID: 1493
		Draft = 256,
		// Token: 0x040005D6 RID: 1494
		Answered = 512,
		// Token: 0x040005D7 RID: 1495
		InConflict = 2048,
		// Token: 0x040005D8 RID: 1496
		RemoteDownload = 4096,
		// Token: 0x040005D9 RID: 1497
		RemoteDelete = 8192,
		// Token: 0x040005DA RID: 1498
		MessageDeliveryNotificationSent = 16384,
		// Token: 0x040005DB RID: 1499
		MimeConversionFailed = 32768
	}
}
