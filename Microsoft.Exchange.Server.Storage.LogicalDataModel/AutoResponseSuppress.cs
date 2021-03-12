using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200002C RID: 44
	[Flags]
	internal enum AutoResponseSuppress
	{
		// Token: 0x040002B9 RID: 697
		None = 0,
		// Token: 0x040002BA RID: 698
		DeliveryReceipt = 1,
		// Token: 0x040002BB RID: 699
		NonDeliveryReceipt = 2,
		// Token: 0x040002BC RID: 700
		ReadNotification = 4,
		// Token: 0x040002BD RID: 701
		NotReadNotification = 8,
		// Token: 0x040002BE RID: 702
		OOF = 16,
		// Token: 0x040002BF RID: 703
		AutoReply = 32
	}
}
