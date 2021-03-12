using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200041B RID: 1051
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WeeklyRecurrencePattern : IntervalRecurrencePattern, IWeeklyPatternInfo
	{
		// Token: 0x06002F33 RID: 12083 RVA: 0x000C2AF4 File Offset: 0x000C0CF4
		public WeeklyRecurrencePattern() : this((DaysOfWeek)(1 << (int)ExDateTime.Today.DayOfWeek))
		{
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000C2B19 File Offset: 0x000C0D19
		public WeeklyRecurrencePattern(DaysOfWeek daysOfWeek) : this(daysOfWeek, 1)
		{
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000C2B23 File Offset: 0x000C0D23
		public WeeklyRecurrencePattern(DaysOfWeek daysOfWeek, int recurrenceInterval) : this(daysOfWeek, recurrenceInterval, DayOfWeek.Sunday)
		{
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000C2B2E File Offset: 0x000C0D2E
		public WeeklyRecurrencePattern(DaysOfWeek daysOfWeek, int recurrenceInterval, DayOfWeek firstDayOfWeek)
		{
			EnumValidator.ThrowIfInvalid<DayOfWeek>(firstDayOfWeek);
			this.DaysOfWeek = daysOfWeek;
			base.RecurrenceInterval = recurrenceInterval;
			this.firstDayOfWeek = firstDayOfWeek;
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000C2B51 File Offset: 0x000C0D51
		// (set) Token: 0x06002F38 RID: 12088 RVA: 0x000C2B59 File Offset: 0x000C0D59
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

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000C2B68 File Offset: 0x000C0D68
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				return this.firstDayOfWeek;
			}
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000C2B70 File Offset: 0x000C0D70
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			if (!(value is WeeklyRecurrencePattern))
			{
				return false;
			}
			WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)value;
			return weeklyRecurrencePattern.DaysOfWeek == this.daysOfWeek && weeklyRecurrencePattern.FirstDayOfWeek == this.FirstDayOfWeek && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000C2BB4 File Offset: 0x000C0DB4
		internal override LocalizedString When()
		{
			LocalizedString result;
			if (base.RecurrenceInterval == 1)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.CalendarWhenWeeklyEveryWeek(new LocalizedDaysOfWeek(this.DaysOfWeek, this.FirstDayOfWeek));
				}
				else
				{
					result = ClientStrings.TaskWhenWeeklyEveryWeek(new LocalizedDaysOfWeek(this.DaysOfWeek, this.FirstDayOfWeek));
				}
			}
			else if (base.RecurrenceInterval == 2)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.CalendarWhenWeeklyEveryAlterateWeek(new LocalizedDaysOfWeek(this.DaysOfWeek, this.FirstDayOfWeek));
				}
				else
				{
					result = ClientStrings.TaskWhenWeeklyEveryAlterateWeek(new LocalizedDaysOfWeek(this.DaysOfWeek, this.FirstDayOfWeek));
				}
			}
			else if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
			{
				result = ClientStrings.CalendarWhenWeeklyEveryNWeeks(base.RecurrenceInterval, new LocalizedDaysOfWeek(this.DaysOfWeek, this.FirstDayOfWeek));
			}
			else
			{
				result = ClientStrings.TaskWhenWeeklyEveryNWeeks(base.RecurrenceInterval, new LocalizedDaysOfWeek(this.DaysOfWeek, this.FirstDayOfWeek));
			}
			return result;
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000C2CB0 File Offset: 0x000C0EB0
		internal override RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.Weekly;
			}
		}

		// Token: 0x040019DC RID: 6620
		private DaysOfWeek daysOfWeek;

		// Token: 0x040019DD RID: 6621
		private DayOfWeek firstDayOfWeek;
	}
}
