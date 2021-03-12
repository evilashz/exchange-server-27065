using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x02000074 RID: 116
	public sealed class RelativeYearlyRecurrencePattern : RecurrencePattern
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000662C File Offset: 0x0000482C
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00006634 File Offset: 0x00004834
		public ISet<DayOfWeek> DaysOfWeek { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000663D File Offset: 0x0000483D
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00006645 File Offset: 0x00004845
		public WeekIndex Index { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000664E File Offset: 0x0000484E
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00006656 File Offset: 0x00004856
		public int Month { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000665F File Offset: 0x0000485F
		public override RecurrencePatternType Type
		{
			get
			{
				return RecurrencePatternType.RelativeYearly;
			}
		}
	}
}
