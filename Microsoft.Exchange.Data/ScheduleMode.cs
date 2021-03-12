using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000199 RID: 409
	public enum ScheduleMode
	{
		// Token: 0x040007FA RID: 2042
		[LocDescription(DataStrings.IDs.ScheduleModeNever)]
		Never,
		// Token: 0x040007FB RID: 2043
		[LocDescription(DataStrings.IDs.ScheduleModeScheduledTimes)]
		ScheduledTimes,
		// Token: 0x040007FC RID: 2044
		[LocDescription(DataStrings.IDs.ScheduleModeAlways)]
		Always
	}
}
