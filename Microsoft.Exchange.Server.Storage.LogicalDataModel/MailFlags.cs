using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200002B RID: 43
	[Flags]
	public enum MailFlags : ushort
	{
		// Token: 0x040002B1 RID: 689
		None = 0,
		// Token: 0x040002B2 RID: 690
		DeliveryCompleted = 1,
		// Token: 0x040002B3 RID: 691
		NeedsReadNotification = 2,
		// Token: 0x040002B4 RID: 692
		NeedsNotReadNotification = 4,
		// Token: 0x040002B5 RID: 693
		OOFCanBeSent = 8,
		// Token: 0x040002B6 RID: 694
		SentRepresentingAddedByTransport = 16,
		// Token: 0x040002B7 RID: 695
		ReadReceiptSent = 32
	}
}
