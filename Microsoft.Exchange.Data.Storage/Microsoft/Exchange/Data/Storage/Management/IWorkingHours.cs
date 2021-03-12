using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009F7 RID: 2551
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IWorkingHours
	{
		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06005D32 RID: 23858
		// (set) Token: 0x06005D33 RID: 23859
		DaysOfWeek WorkDays { get; set; }

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06005D34 RID: 23860
		// (set) Token: 0x06005D35 RID: 23861
		TimeSpan WorkingHoursStartTime { get; set; }

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06005D36 RID: 23862
		// (set) Token: 0x06005D37 RID: 23863
		TimeSpan WorkingHoursEndTime { get; set; }

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06005D38 RID: 23864
		// (set) Token: 0x06005D39 RID: 23865
		ExTimeZoneValue WorkingHoursTimeZone { get; set; }
	}
}
