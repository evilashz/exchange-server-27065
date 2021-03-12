using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200040F RID: 1039
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MonthlyRegeneratingPattern : RegeneratingPattern
	{
		// Token: 0x06002F07 RID: 12039 RVA: 0x000C1519 File Offset: 0x000BF719
		public MonthlyRegeneratingPattern(int recurrenceInterval) : this(recurrenceInterval, CalendarType.Default)
		{
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000C1523 File Offset: 0x000BF723
		public MonthlyRegeneratingPattern(int recurrenceInterval, CalendarType calendarType)
		{
			EnumValidator.ThrowIfInvalid<CalendarType>(calendarType, "calendarType");
			base.RecurrenceInterval = recurrenceInterval;
			this.calendarType = calendarType;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000C1544 File Offset: 0x000BF744
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			MonthlyRegeneratingPattern monthlyRegeneratingPattern = value as MonthlyRegeneratingPattern;
			return monthlyRegeneratingPattern != null && (ignoreCalendarTypeAndIsLeapMonth || monthlyRegeneratingPattern.CalendarType == this.CalendarType) && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000C1578 File Offset: 0x000BF778
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000C1580 File Offset: 0x000BF780
		internal override LocalizedString When()
		{
			if (base.RecurrenceInterval == 1)
			{
				return ClientStrings.TaskWhenMonthlyRegeneratingPattern;
			}
			return ClientStrings.TaskWhenNMonthsRegeneratingPattern(base.RecurrenceInterval);
		}

		// Token: 0x040019A3 RID: 6563
		private CalendarType calendarType;
	}
}
