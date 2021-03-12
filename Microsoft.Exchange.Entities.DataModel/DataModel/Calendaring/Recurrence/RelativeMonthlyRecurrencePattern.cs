using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x02000073 RID: 115
	public sealed class RelativeMonthlyRecurrencePattern : RecurrencePattern
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000065FF File Offset: 0x000047FF
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00006607 File Offset: 0x00004807
		public ISet<DayOfWeek> DaysOfWeek { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00006610 File Offset: 0x00004810
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00006618 File Offset: 0x00004818
		public WeekIndex Index { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00006621 File Offset: 0x00004821
		public override RecurrencePatternType Type
		{
			get
			{
				return RecurrencePatternType.RelativeMonthly;
			}
		}
	}
}
