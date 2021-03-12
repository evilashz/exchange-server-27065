using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003ED RID: 1005
	public enum MessageDeliveryStatus
	{
		// Token: 0x04001C46 RID: 7238
		[LocDescription(CoreStrings.IDs.DeliveryStatusAll)]
		All,
		// Token: 0x04001C47 RID: 7239
		[LocDescription(CoreStrings.IDs.DeliveryStatusDelivered)]
		Delivered,
		// Token: 0x04001C48 RID: 7240
		[LocDescription(CoreStrings.IDs.DeliveryStatusFailed)]
		Failed,
		// Token: 0x04001C49 RID: 7241
		[LocDescription(CoreStrings.IDs.DeliveryStatusExpanded)]
		Expanded
	}
}
