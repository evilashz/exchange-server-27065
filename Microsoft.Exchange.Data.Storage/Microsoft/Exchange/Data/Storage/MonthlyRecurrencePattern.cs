using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E4 RID: 996
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonthlyRecurrencePattern : IntervalRecurrencePattern, IMonthlyPatternInfo
	{
		// Token: 0x06002D77 RID: 11639 RVA: 0x000BB5AC File Offset: 0x000B97AC
		public MonthlyRecurrencePattern() : this(ExDateTime.GetNow(ExTimeZone.CurrentTimeZone).Day)
		{
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000BB5D1 File Offset: 0x000B97D1
		public MonthlyRecurrencePattern(int dayOfMonth) : this(dayOfMonth, 1)
		{
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000BB5DB File Offset: 0x000B97DB
		public MonthlyRecurrencePattern(int dayOfMonth, int recurrenceInterval) : this(dayOfMonth, recurrenceInterval, CalendarType.Default)
		{
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000BB5E6 File Offset: 0x000B97E6
		public MonthlyRecurrencePattern(int dayOfMonth, int recurrenceInterval, CalendarType calendarType)
		{
			EnumValidator.ThrowIfInvalid<CalendarType>(calendarType);
			this.DayOfMonth = dayOfMonth;
			base.RecurrenceInterval = recurrenceInterval;
			this.calendarType = calendarType;
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000BB609 File Offset: 0x000B9809
		// (set) Token: 0x06002D7C RID: 11644 RVA: 0x000BB611 File Offset: 0x000B9811
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

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000BB638 File Offset: 0x000B9838
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000BB640 File Offset: 0x000B9840
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			if (!(value is MonthlyRecurrencePattern))
			{
				return false;
			}
			MonthlyRecurrencePattern monthlyRecurrencePattern = (MonthlyRecurrencePattern)value;
			return monthlyRecurrencePattern.DayOfMonth == this.dayOfMonth && (ignoreCalendarTypeAndIsLeapMonth || monthlyRecurrencePattern.calendarType == this.calendarType) && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000BB688 File Offset: 0x000B9888
		internal override LocalizedString When()
		{
			if (Recurrence.IsGregorianCompatible(this.CalendarType))
			{
				if (base.RecurrenceInterval == 1)
				{
					if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
					{
						return ClientStrings.CalendarWhenMonthlyEveryMonth(this.DayOfMonth);
					}
					return ClientStrings.TaskWhenMonthlyEveryMonth(this.DayOfMonth);
				}
				else if (base.RecurrenceInterval == 2)
				{
					if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
					{
						return ClientStrings.CalendarWhenMonthlyEveryOtherMonth(this.DayOfMonth);
					}
					return ClientStrings.TaskWhenMonthlyEveryOtherMonth(this.DayOfMonth);
				}
				else
				{
					if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
					{
						return ClientStrings.CalendarWhenMonthlyEveryNMonths(this.DayOfMonth, base.RecurrenceInterval);
					}
					return ClientStrings.TaskWhenMonthlyEveryNMonths(this.DayOfMonth, base.RecurrenceInterval);
				}
			}
			else if (base.RecurrenceInterval == 1)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					return ClientStrings.AlternateCalendarWhenMonthlyEveryMonth(Recurrence.GetCalendarName(this.CalendarType), this.DayOfMonth);
				}
				return ClientStrings.AlternateCalendarTaskWhenMonthlyEveryMonth(Recurrence.GetCalendarName(this.CalendarType), this.DayOfMonth);
			}
			else if (base.RecurrenceInterval == 2)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					return ClientStrings.AlternateCalendarWhenMonthlyEveryOtherMonth(Recurrence.GetCalendarName(this.CalendarType), this.DayOfMonth);
				}
				return ClientStrings.AlternateCalendarTaskWhenMonthlyEveryOtherMonth(Recurrence.GetCalendarName(this.CalendarType), this.DayOfMonth);
			}
			else
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					return ClientStrings.AlternateCalendarWhenMonthlyEveryNMonths(Recurrence.GetCalendarName(this.CalendarType), this.DayOfMonth, base.RecurrenceInterval);
				}
				return ClientStrings.AlternateCalendarTaskWhenMonthlyEveryNMonths(Recurrence.GetCalendarName(this.CalendarType), this.DayOfMonth, base.RecurrenceInterval);
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000BB7DF File Offset: 0x000B99DF
		internal override RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.Monthly;
			}
		}

		// Token: 0x04001900 RID: 6400
		private int dayOfMonth;

		// Token: 0x04001901 RID: 6401
		private CalendarType calendarType;
	}
}
