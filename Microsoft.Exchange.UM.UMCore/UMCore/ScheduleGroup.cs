using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001CB RID: 459
	internal class ScheduleGroup
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x0003BCDF File Offset: 0x00039EDF
		internal ScheduleGroup(List<ExDateTime> dayOfWeekList, List<TimeRange> scheduleIntervalList)
		{
			this.dayOfWeekList = dayOfWeekList;
			this.scheduleIntervalList = scheduleIntervalList;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0003BCF5 File Offset: 0x00039EF5
		internal List<ExDateTime> DaysOfWeek
		{
			get
			{
				return this.dayOfWeekList;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0003BCFD File Offset: 0x00039EFD
		internal List<TimeRange> ScheduleIntervals
		{
			get
			{
				return this.scheduleIntervalList;
			}
		}

		// Token: 0x04000A8F RID: 2703
		private List<ExDateTime> dayOfWeekList;

		// Token: 0x04000A90 RID: 2704
		private List<TimeRange> scheduleIntervalList;
	}
}
