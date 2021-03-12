using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200041E RID: 1054
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class YearlyThRecurrencePattern : IntervalRecurrencePattern, IYearlyPatternInfo, IMonthlyThPatternInfo, IMonthlyPatternInfo
	{
		// Token: 0x06002F50 RID: 12112 RVA: 0x000C2F68 File Offset: 0x000C1168
		public YearlyThRecurrencePattern() : this((DaysOfWeek)(1 << (int)DateTime.Today.DayOfWeek), RecurrenceOrderType.First, DateTime.Today.Month)
		{
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000C2F9B File Offset: 0x000C119B
		public YearlyThRecurrencePattern(DaysOfWeek daysOfWeek, RecurrenceOrderType order, int month) : this(daysOfWeek, order, month, false, CalendarType.Default)
		{
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000C2FA8 File Offset: 0x000C11A8
		public YearlyThRecurrencePattern(DaysOfWeek daysOfWeek, RecurrenceOrderType order, int month, bool isLeapMonth, CalendarType calendarType) : this(daysOfWeek, order, month, isLeapMonth, 1, calendarType)
		{
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000C2FB8 File Offset: 0x000C11B8
		public YearlyThRecurrencePattern(DaysOfWeek daysOfWeek, RecurrenceOrderType order, int month, bool isLeapMonth, int recurrenceInterval, CalendarType calendarType)
		{
			EnumValidator.ThrowIfInvalid<CalendarType>(calendarType);
			this.Month = month;
			this.DaysOfWeek = daysOfWeek;
			this.Order = order;
			this.isLeapMonth = isLeapMonth;
			this.calendarType = calendarType;
			base.RecurrenceInterval = recurrenceInterval;
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000C2FF4 File Offset: 0x000C11F4
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x000C2FFC File Offset: 0x000C11FC
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

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000C300B File Offset: 0x000C120B
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x000C3013 File Offset: 0x000C1213
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

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000C3022 File Offset: 0x000C1222
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000C302A File Offset: 0x000C122A
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

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000C3051 File Offset: 0x000C1251
		public bool IsLeapMonth
		{
			get
			{
				return this.isLeapMonth;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x000C3059 File Offset: 0x000C1259
		public CalendarType CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000C3061 File Offset: 0x000C1261
		internal override RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.Yearly;
			}
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000C3064 File Offset: 0x000C1264
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			if (!(value is YearlyThRecurrencePattern))
			{
				return false;
			}
			YearlyThRecurrencePattern yearlyThRecurrencePattern = (YearlyThRecurrencePattern)value;
			return yearlyThRecurrencePattern.DaysOfWeek == this.DaysOfWeek && yearlyThRecurrencePattern.Month == this.Month && yearlyThRecurrencePattern.Order == this.Order && (ignoreCalendarTypeAndIsLeapMonth || yearlyThRecurrencePattern.CalendarType == this.CalendarType) && (ignoreCalendarTypeAndIsLeapMonth || yearlyThRecurrencePattern.IsLeapMonth == this.IsLeapMonth);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000C30D4 File Offset: 0x000C12D4
		internal override LocalizedString When()
		{
			LocalizedString result;
			if (Recurrence.IsGregorianCompatible(this.CalendarType))
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.CalendarWhenYearlyTh(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), new LocalizedMonth(this.Month));
				}
				else
				{
					result = ClientStrings.TaskWhenYearlyTh(MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), new LocalizedMonth(this.Month));
				}
			}
			else if (this.IsLeapMonth)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.AlternateCalendarWhenYearlyThLeap(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), this.Month);
				}
				else
				{
					result = ClientStrings.AlternateCalendarTaskWhenYearlyThLeap(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), this.Month);
				}
			}
			else if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
			{
				result = ClientStrings.AlternateCalendarWhenYearlyTh(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), this.Month);
			}
			else
			{
				result = ClientStrings.AlternateCalendarTaskWhenYearlyTh(Recurrence.GetCalendarName(this.CalendarType), MonthlyThRecurrencePattern.OrderAsString(this.Order), new LocalizedDaysOfWeek(this.DaysOfWeek), this.Month);
			}
			return result;
		}

		// Token: 0x040019E4 RID: 6628
		private RecurrenceOrderType order;

		// Token: 0x040019E5 RID: 6629
		private DaysOfWeek daysOfWeek;

		// Token: 0x040019E6 RID: 6630
		private int month;

		// Token: 0x040019E7 RID: 6631
		private bool isLeapMonth;

		// Token: 0x040019E8 RID: 6632
		private CalendarType calendarType;
	}
}
