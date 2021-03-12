using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000151 RID: 337
	internal enum DeliveryStatus : byte
	{
		// Token: 0x04000744 RID: 1860
		Enqueued,
		// Token: 0x04000745 RID: 1861
		InDelivery,
		// Token: 0x04000746 RID: 1862
		DequeuedAndDeferred,
		// Token: 0x04000747 RID: 1863
		PendingResubmit,
		// Token: 0x04000748 RID: 1864
		Complete
	}
}
