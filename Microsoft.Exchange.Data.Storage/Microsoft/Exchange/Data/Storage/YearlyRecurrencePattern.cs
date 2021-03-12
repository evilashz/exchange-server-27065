using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200041C RID: 1052
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class YearlyRecurrencePattern : IntervalRecurrencePattern, IYearlyPatternInfo, IMonthlyPatternInfo
	{
		// Token: 0x06002F3D RID: 12093 RVA: 0x000C2CB4 File Offset: 0x000C0EB4
		public YearlyRecurrencePattern() : this(ExDateTime.Today.Day, ExDateTime.Today.Month)
		{
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000C2CE1 File Offset: 0x000C0EE1
		public YearlyRecurrencePattern(int dayOfMonth, int month) : this(dayOfMonth, month, false, 1, CalendarType.Default)
		{
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000C2CEE File Offset: 0x000C0EEE
		public YearlyRecurrencePattern(int dayOfMonth, int month, int months) : this(dayOfMonth, month, false, CalendarType.Default, months)
		{
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000C2CFB File Offset: 0x000C0EFB
		public YearlyRecurrencePattern(int dayOfMonth, int month, bool isLeapMonth, CalendarType calendarType) : this(dayOfMonth, month, isLeapMonth, 1, calendarType)
		{
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000C2D09 File Offset: 0x000C0F09
		public YearlyRecurrencePattern(int dayOfMonth, int month, bool isLeapMonth, int recurrenceInterval, CalendarType calendarType) : this(dayOfMonth, month, isLeapMonth, calendarType, 12 * recurrenceInterval)
		{
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000C2D1B File Offset: 0x000C0F1B
		internal YearlyRecurrencePattern(int dayOfMonth, int month, bool isLeapMonth, CalendarType calendarType, int months)
		{
			EnumValidator.ThrowIfInvalid<CalendarType>(calendarType);
			this.isLeapMonth = isLeapMonth;
			this.DayOfMonth = dayOfMonth;
			this.Month = month;
			this.calendarType = calendarType;
			this.Months = months;
			base.RecurrenceInterval = months / 12;
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000C2D5A File Offset: 0x000C0F5A
		// (set) Token: 0x06002F44 RID: 12100 RVA: 0x000C2D62 File Offset: 0x000C0F62
		public int DayOfMonth
		{
			get
			{
				return this.dayOfMonth;
			}
			private set
			{
				if (value < 1 || value > 31)
				{
					throw new ArgumentOutOfRangeException(ServerStrings.ExInvalidDayOfMonth, "DayOfMonth");
				}
				this.dayOfMonth = value;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x000C2D89 File Offset: 0x000C0F89
		// (set) Token: 0x06002F46 RID: 12102 RVA: 0x000C2D91 File Offset: 0x000C0F91
		public int Month
		{
			get
			{
				return this.month;
			}
			private set
			{
				if (value < 1 || value > 12)
				{
					throw new ArgumentOutOfRangeException(ServerStrings.ExInvalidMonth, "Month");
				}
				this.month = value;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000C2DB8 File Offset: 0x000C0FB8
		public bool IsLeapMonth
		{
			get
			{
				return this.isLeapMonth;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000C2DC0 File Offset: 0x000C0FC0
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000C2DC8 File Offset: 0x000C0FC8
		internal override RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.Yearly;
			}
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000C2DCC File Offset: 0x000C0FCC
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			if (!(value is YearlyRecurrencePattern))
			{
				return false;
			}
			YearlyRecurrencePattern yearlyRecurrencePattern = (YearlyRecurrencePattern)value;
			return yearlyRecurrencePattern.DayOfMonth == this.DayOfMonth && yearlyRecurrencePattern.Month == this.Month && (ignoreCalendarTypeAndIsLeapMonth || yearlyRecurrencePattern.CalendarType == this.CalendarType) && (ignoreCalendarTypeAndIsLeapMonth || yearlyRecurrencePattern.IsLeapMonth == this.IsLeapMonth);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000C2E2C File Offset: 0x000C102C
		internal override LocalizedString When()
		{
			LocalizedString result;
			if (Recurrence.IsGregorianCompatible(this.CalendarType))
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.CalendarWhenYearly(new LocalizedMonth(this.month), this.DayOfMonth);
				}
				else
				{
					result = ClientStrings.TaskWhenYearly(new LocalizedMonth(this.month), this.DayOfMonth);
				}
			}
			else if (this.IsLeapMonth)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.AlternateCalendarWhenYearlyLeap(Recurrence.GetCalendarName(this.CalendarType), this.month, this.DayOfMonth);
				}
				else
				{
					result = ClientStrings.AlternateCalendarTaskWhenYearlyLeap(Recurrence.GetCalendarName(this.CalendarType), this.month, this.DayOfMonth);
				}
			}
			else if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
			{
				result = ClientStrings.AlternateCalendarWhenYearly(Recurrence.GetCalendarName(this.CalendarType), this.month, this.DayOfMonth);
			}
			else
			{
				result = ClientStrings.AlternateCalendarTaskWhenYearly(Recurrence.GetCalendarName(this.CalendarType), this.month, this.DayOfMonth);
			}
			return result;
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000C2F37 File Offset: 0x000C1137
		// (set) Token: 0x06002F4D RID: 12109 RVA: 0x000C2F3F File Offset: 0x000C113F
		internal int Months
		{
			get
			{
				return this.months;
			}
			set
			{
				this.months = value;
			}
		}

		// Token: 0x040019DE RID: 6622
		private int dayOfMonth;

		// Token: 0x040019DF RID: 6623
		private int month;

		// Token: 0x040019E0 RID: 6624
		private bool isLeapMonth;

		// Token: 0x040019E1 RID: 6625
		private CalendarType calendarType;

		// Token: 0x040019E2 RID: 6626
		private int months;
	}
}
