using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003FA RID: 1018
	public enum ReportDeliveryStatus
	{
		// Token: 0x04001CAE RID: 7342
		[LocDescription(CoreStrings.IDs.DeliveryStatusAll)]
		All,
		// Token: 0x04001CAF RID: 7343
		[LocDescription(CoreStrings.IDs.DeliveryStatusDelivered)]
		Delivered,
		// Token: 0x04001CB0 RID: 7344
		[LocDescription(CoreStrings.IDs.DeliveryStatusFailed)]
		Failed,
		// Token: 0x04001CB1 RID: 7345
		[LocDescription(CoreStrings.IDs.DeliveryStatusExpanded)]
		Expanded
	}
}
