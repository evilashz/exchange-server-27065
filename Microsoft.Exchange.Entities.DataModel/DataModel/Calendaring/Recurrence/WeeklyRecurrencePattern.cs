using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x02000076 RID: 118
	public sealed class WeeklyRecurrencePattern : RecurrencePattern
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000666A File Offset: 0x0000486A
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00006672 File Offset: 0x00004872
		public DayOfWeek FirstDayOfWeek { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000667B File Offset: 0x0000487B
		public override RecurrencePatternType Type
		{
			get
			{
				return RecurrencePatternType.Weekly;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000667E File Offset: 0x0000487E
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00006686 File Offset: 0x00004886
		public ISet<DayOfWeek> DaysOfWeek { get; set; }
	}
}
