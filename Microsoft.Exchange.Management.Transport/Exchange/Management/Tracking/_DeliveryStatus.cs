using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x02000097 RID: 151
	public enum _DeliveryStatus
	{
		// Token: 0x040001C6 RID: 454
		[TrackingStringsLocDescription(CoreStrings.IDs.StatusUnsuccessFul)]
		Unsuccessful,
		// Token: 0x040001C7 RID: 455
		[TrackingStringsLocDescription(CoreStrings.IDs.StatusPending)]
		Pending,
		// Token: 0x040001C8 RID: 456
		[TrackingStringsLocDescription(CoreStrings.IDs.StatusDelivered)]
		Delivered,
		// Token: 0x040001C9 RID: 457
		[TrackingStringsLocDescription(CoreStrings.IDs.StatusTransferred)]
		Transferred,
		// Token: 0x040001CA RID: 458
		[TrackingStringsLocDescription(CoreStrings.IDs.StatusRead)]
		Read
	}
}
