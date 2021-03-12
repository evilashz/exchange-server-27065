using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000410 RID: 1040
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class YearlyRegeneratingPattern : RegeneratingPattern
	{
		// Token: 0x06002F0C RID: 12044 RVA: 0x000C159C File Offset: 0x000BF79C
		public YearlyRegeneratingPattern(int recurrenceInterval) : this(recurrenceInterval, CalendarType.Default)
		{
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000C15A6 File Offset: 0x000BF7A6
		public YearlyRegeneratingPattern(int recurrenceInterval, CalendarType calendarType)
		{
			EnumValidator.ThrowIfInvalid<CalendarType>(calendarType, "calendarType");
			base.RecurrenceInterval = recurrenceInterval;
			this.calendarType = calendarType;
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000C15C8 File Offset: 0x000BF7C8
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			YearlyRegeneratingPattern yearlyRegeneratingPattern = value as YearlyRegeneratingPattern;
			return yearlyRegeneratingPattern != null && (ignoreCalendarTypeAndIsLeapMonth || yearlyRegeneratingPattern.calendarType == this.calendarType) && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000C15FC File Offset: 0x000BF7FC
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000C1604 File Offset: 0x000BF804
		internal override LocalizedString When()
		{
			if (base.RecurrenceInterval == 1)
			{
				return ClientStrings.TaskWhenYearlyRegeneratingPattern;
			}
			return ClientStrings.TaskWhenNYearsRegeneratingPattern(base.RecurrenceInterval);
		}

		// Token: 0x040019A4 RID: 6564
		private CalendarType calendarType;
	}
}
