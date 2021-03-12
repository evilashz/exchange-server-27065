using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F8 RID: 1016
	public enum ReportRecurrence
	{
		// Token: 0x04001CA6 RID: 7334
		[LocDescription(CoreStrings.IDs.RunOnce)]
		RunOnce,
		// Token: 0x04001CA7 RID: 7335
		[LocDescription(CoreStrings.IDs.RunWeekly)]
		Weekly,
		// Token: 0x04001CA8 RID: 7336
		[LocDescription(CoreStrings.IDs.RunMonthly)]
		Monthly
	}
}
