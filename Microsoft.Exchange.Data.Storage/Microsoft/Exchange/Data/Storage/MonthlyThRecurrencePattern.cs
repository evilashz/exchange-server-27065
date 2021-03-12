using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E6 RID: 998
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonthlyThRecurrencePattern : IntervalRecurrencePattern, IMonthlyThPatternInfo, IMonthlyPatternInfo
	{
		// Token: 0x06002D82 RID: 11650 RVA: 0x000BB7E4 File Offset: 0x000B99E4
		public MonthlyThRecurrencePattern() : this((DaysOfWeek)(1 << (int)DateTime.Today.DayOfWeek), RecurrenceOrderType.First)
		{
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000BB80A File Offset: 0x000B9A0A
		public MonthlyThRecurrencePattern(DaysOfWeek daysOfWeek, RecurrenceOrderType order) : this(daysOfWeek, order, 1)
		{
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000BB815 File Offset: 0x000B9A15
		public MonthlyThRecurrencePattern(DaysOfWeek daysOfWeek, RecurrenceOrderType order, int recurrenceInterval) : this(daysOfWeek, order, recurrenceInterval, CalendarType.Default)
		{
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000BB821 File Offset: 0x000B9A21
		public MonthlyThRecurrencePattern(DaysOfWeek daysOfWeek, RecurrenceOrderType order, int recurrenceInterval, CalendarType calendarType)
		{
			EnumValidator.ThrowIfInvalid<CalendarType>(calendarType);
			this.DaysOfWeek = daysOfWeek;
			this.Order = order;
			base.RecurrenceInterval = recurrenceInterval;
			this.calendarType = calendarType;
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000BB84D File Offset: 0x000B9A4D
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x000BB855 File Offset: 0x000B9A55
		public DaysOfWeek DaysOfWeek
		{
			get
			{
				return this.daysOfWeek;
			}
			private set
			{
				EnumValidator.ThrowIfInvalid<DaysOfWeek>(value);
				this.daysOfWeek = value;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000BB864 File Offset: 0x000B9A64
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000BB86C File Offset: 0x000B9A6C
		public RecurrenceOrderType Order
		{
			get
			{
				return this.order;
			}
			private set
			{
				EnumValidator.ThrowIfInvalid<RecurrenceOrderType>(value);
				this.order = value;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000BB87B File Offset: 0x000B9A7B
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000BB883 File Offset: 0x000B9A83
		internal override RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.Monthly;
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000BB888 File Offset: 0x000B9A88
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			if (!(value is MonthlyThRecurrencePattern))
			{
				return false;
			}
			MonthlyThRecurrencePattern monthlyThRecurrencePattern = (MonthlyThRecurrencePattern)value;
			return monthlyThRecurrencePattern.DaysOfWeek == this.daysOfWeek && monthlyThRecurrencePattern.Order == this.order && (ignoreCalendarTypeAndIsLeapMonth || monthlyThRecurrencePattern.calendarType == this.calendarType) && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000BB8E0 File Offset: 0x000B9AE0
		internal static LocalizedString OrderAsString(RecurrenceOrderType order)
		{
			switch (order)
			{
			case RecurrenceOrderType.Last:
				return ClientStrings.WhenLast;
			case RecurrenceOrderType.First:
				return ClientStrings.WhenFirst;
			case RecurrenceOrderType.Second:
				return ClientStrings.WhenSecond;
			case RecurrenceOrderType.Third:
				return ClientStrings.WhenThird;
			case RecurrenceOrderType.Fourth:
				return ClientStrings.WhenFourth;
			}
			ExDiagnostics.FailFast("Invalid value for Order", false);
			throw new ArgumentOutOfRangeException("Order");
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000BB944 File Offset: 0x000B9B44
		internal override LocalizedString When()
		{
			if (Recurrence.IsGregorianCompatible(this.CalendarType))
			{
				if (base.RecurrenceInterval == 1)
				{
					if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
					{
						return ClientStrings.CalendarWhenMonthlyThEveryMonth(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
					}
					return ClientStrings.TaskWhenMonthlyThEveryMonth(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
				}
				else if (base.RecurrenceInterval == 2)
				{
					if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
					{
						return ClientStrings.CalendarWhenMonthlyThEveryOtherMonth(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
					}
					return ClientStrings.TaskWhenMonthlyThEveryOtherMonth(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
				}
				else
				{
					if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
					{
						return ClientStrings.CalendarWhenMonthlyThEveryNMonths(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), base.RecurrenceInterval);
					}
					return ClientStrings.TaskWhenMonthlyThEveryNMonths(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), base.RecurrenceInterval);
				}
			}
			else if (base.RecurrenceInterval == 1)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					return ClientStrings.AlternateCalendarWhenMonthlyThEveryMonth(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
				}
				return ClientStrings.AlternateCalendarTaskWhenMonthlyThEveryMonth(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
			}
			else if (base.RecurrenceInterval == 2)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					return ClientStrings.AlternateCalendarWhenMonthlyThEveryOtherMonth(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
				}
				return ClientStrings.AlternateCalendarTaskWhenMonthlyThEveryOtherMonth(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek));
			}
			else
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					return ClientStrings.AlternateCalendarWhenMonthlyThEveryNMonths(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), base.RecurrenceInterval);
				}
				return ClientStrings.AlternateCalendarTaskWhenMonthlyThEveryNMonths(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), base.RecurrenceInterval);
			}
		}

		// Token: 0x04001902 RID: 6402
		private RecurrenceOrderType order;

		// Token: 0x04001903 RID: 6403
		private DaysOfWeek daysOfWeek;

		// Token: 0x04001904 RID: 6404
		private CalendarType calendarType;
	}
}
