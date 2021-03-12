using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x020000AB RID: 171
	internal class CalendarItemData : CalendarItemBaseData
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00031AE7 File Offset: 0x0002FCE7
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x00031AEF File Offset: 0x0002FCEF
		public Recurrence Recurrence
		{
			get
			{
				return this.recurrence;
			}
			set
			{
				this.recurrence = value;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00031AF8 File Offset: 0x0002FCF8
		public CalendarItemData()
		{
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00031B00 File Offset: 0x0002FD00
		public CalendarItemData(CalendarItem calendarItem)
		{
			this.SetFrom(calendarItem);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00031B0F File Offset: 0x0002FD0F
		public CalendarItemData(CalendarItemBase calendarItemBase)
		{
			this.SetFrom(calendarItemBase);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00031B1E File Offset: 0x0002FD1E
		public CalendarItemData(CalendarItemData other) : base(other)
		{
			this.recurrence = CalendarItemData.CloneRecurrence(other.recurrence);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00031B38 File Offset: 0x0002FD38
		public static bool IsRecurrenceEqual(Recurrence r1, Recurrence r2)
		{
			if (r1 == null != (r2 == null))
			{
				return false;
			}
			if (r1 != null)
			{
				if (!CalendarItemData.IsRecurrencePatternEqual(r1.Pattern, r2.Pattern))
				{
					return false;
				}
				if (!CalendarItemData.IsRecurrenceRangeEqual(r1.Range, r2.Range))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00031B74 File Offset: 0x0002FD74
		public static bool IsRecurrencePatternEqual(RecurrencePattern p1, RecurrencePattern p2)
		{
			if (p1 == null != (p2 == null))
			{
				return false;
			}
			if (p1 != null)
			{
				if (!p1.GetType().Equals(p2.GetType()))
				{
					return false;
				}
				DailyRecurrencePattern dailyRecurrencePattern = p1 as DailyRecurrencePattern;
				if (dailyRecurrencePattern != null)
				{
					DailyRecurrencePattern dailyRecurrencePattern2 = p2 as DailyRecurrencePattern;
					return dailyRecurrencePattern.RecurrenceInterval == dailyRecurrencePattern2.RecurrenceInterval;
				}
				WeeklyRecurrencePattern weeklyRecurrencePattern = p1 as WeeklyRecurrencePattern;
				if (weeklyRecurrencePattern != null)
				{
					WeeklyRecurrencePattern weeklyRecurrencePattern2 = p2 as WeeklyRecurrencePattern;
					return weeklyRecurrencePattern.DaysOfWeek == weeklyRecurrencePattern2.DaysOfWeek && weeklyRecurrencePattern.FirstDayOfWeek == weeklyRecurrencePattern2.FirstDayOfWeek && weeklyRecurrencePattern.RecurrenceInterval == weeklyRecurrencePattern2.RecurrenceInterval;
				}
				MonthlyRecurrencePattern monthlyRecurrencePattern = p1 as MonthlyRecurrencePattern;
				if (monthlyRecurrencePattern != null)
				{
					MonthlyRecurrencePattern monthlyRecurrencePattern2 = p2 as MonthlyRecurrencePattern;
					return monthlyRecurrencePattern.CalendarType == monthlyRecurrencePattern2.CalendarType && monthlyRecurrencePattern.DayOfMonth == monthlyRecurrencePattern2.DayOfMonth && monthlyRecurrencePattern.RecurrenceInterval == monthlyRecurrencePattern2.RecurrenceInterval;
				}
				MonthlyThRecurrencePattern monthlyThRecurrencePattern = p1 as MonthlyThRecurrencePattern;
				if (monthlyThRecurrencePattern != null)
				{
					MonthlyThRecurrencePattern monthlyThRecurrencePattern2 = p2 as MonthlyThRecurrencePattern;
					return monthlyThRecurrencePattern.CalendarType == monthlyThRecurrencePattern2.CalendarType && monthlyThRecurrencePattern.DaysOfWeek == monthlyThRecurrencePattern2.DaysOfWeek && monthlyThRecurrencePattern.Order == monthlyThRecurrencePattern2.Order && monthlyThRecurrencePattern.RecurrenceInterval == monthlyThRecurrencePattern2.RecurrenceInterval;
				}
				YearlyRecurrencePattern yearlyRecurrencePattern = p1 as YearlyRecurrencePattern;
				if (yearlyRecurrencePattern != null)
				{
					YearlyRecurrencePattern yearlyRecurrencePattern2 = p2 as YearlyRecurrencePattern;
					return yearlyRecurrencePattern.CalendarType == yearlyRecurrencePattern2.CalendarType && yearlyRecurrencePattern.DayOfMonth == yearlyRecurrencePattern2.DayOfMonth && yearlyRecurrencePattern.IsLeapMonth == yearlyRecurrencePattern2.IsLeapMonth && yearlyRecurrencePattern.Month == yearlyRecurrencePattern2.Month;
				}
				YearlyThRecurrencePattern yearlyThRecurrencePattern = p1 as YearlyThRecurrencePattern;
				if (yearlyThRecurrencePattern != null)
				{
					YearlyThRecurrencePattern yearlyThRecurrencePattern2 = p2 as YearlyThRecurrencePattern;
					return yearlyThRecurrencePattern.CalendarType == yearlyThRecurrencePattern2.CalendarType && yearlyThRecurrencePattern.DaysOfWeek == yearlyThRecurrencePattern2.DaysOfWeek && yearlyThRecurrencePattern.IsLeapMonth == yearlyThRecurrencePattern2.IsLeapMonth && yearlyThRecurrencePattern.Month == yearlyThRecurrencePattern2.Month && yearlyThRecurrencePattern.Order == yearlyThRecurrencePattern2.Order;
				}
			}
			return true;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00031D88 File Offset: 0x0002FF88
		public static bool IsRecurrenceRangeEqual(RecurrenceRange r1, RecurrenceRange r2)
		{
			if (r1 == null != (r2 == null))
			{
				return false;
			}
			if (r1 != null)
			{
				if (!r1.GetType().Equals(r2.GetType()))
				{
					return false;
				}
				if (r1.StartDate != r2.StartDate)
				{
					return false;
				}
				EndDateRecurrenceRange endDateRecurrenceRange = r1 as EndDateRecurrenceRange;
				if (endDateRecurrenceRange != null)
				{
					EndDateRecurrenceRange endDateRecurrenceRange2 = r2 as EndDateRecurrenceRange;
					return !(endDateRecurrenceRange.EndDate != endDateRecurrenceRange2.EndDate);
				}
				NumberedRecurrenceRange numberedRecurrenceRange = r1 as NumberedRecurrenceRange;
				if (numberedRecurrenceRange != null)
				{
					NumberedRecurrenceRange numberedRecurrenceRange2 = r2 as NumberedRecurrenceRange;
					return numberedRecurrenceRange.NumberOfOccurrences == numberedRecurrenceRange2.NumberOfOccurrences;
				}
			}
			return true;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00031E1C File Offset: 0x0003001C
		public static Recurrence CloneRecurrence(Recurrence recurrence)
		{
			Recurrence result = null;
			if (recurrence != null)
			{
				if (recurrence.CreatedExTimeZone != ExTimeZone.UtcTimeZone && recurrence.ReadExTimeZone != ExTimeZone.UtcTimeZone)
				{
					result = new Recurrence(CalendarItemData.CloneRecurrencePattern(recurrence.Pattern), CalendarItemData.CloneRecurrenceRange(recurrence.Range), recurrence.CreatedExTimeZone, recurrence.ReadExTimeZone);
				}
				else
				{
					result = new Recurrence(CalendarItemData.CloneRecurrencePattern(recurrence.Pattern), CalendarItemData.CloneRecurrenceRange(recurrence.Range));
				}
			}
			return result;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00031E90 File Offset: 0x00030090
		public static RecurrenceRange CloneRecurrenceRange(RecurrenceRange range)
		{
			RecurrenceRange result = null;
			if (range == null)
			{
				return result;
			}
			EndDateRecurrenceRange endDateRecurrenceRange = range as EndDateRecurrenceRange;
			if (endDateRecurrenceRange != null)
			{
				return new EndDateRecurrenceRange(endDateRecurrenceRange.StartDate, endDateRecurrenceRange.EndDate);
			}
			NoEndRecurrenceRange noEndRecurrenceRange = range as NoEndRecurrenceRange;
			if (noEndRecurrenceRange != null)
			{
				return new NoEndRecurrenceRange(noEndRecurrenceRange.StartDate);
			}
			NumberedRecurrenceRange numberedRecurrenceRange = range as NumberedRecurrenceRange;
			if (numberedRecurrenceRange != null)
			{
				return new NumberedRecurrenceRange(numberedRecurrenceRange.StartDate, numberedRecurrenceRange.NumberOfOccurrences);
			}
			throw new ArgumentException("Unhandled RecurrenceRange type.");
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00031EFC File Offset: 0x000300FC
		public static RecurrencePattern CloneRecurrencePattern(RecurrencePattern pattern)
		{
			RecurrencePattern result = null;
			if (pattern == null)
			{
				return result;
			}
			DailyRecurrencePattern dailyRecurrencePattern = pattern as DailyRecurrencePattern;
			if (dailyRecurrencePattern != null)
			{
				return new DailyRecurrencePattern(dailyRecurrencePattern.RecurrenceInterval);
			}
			MonthlyRecurrencePattern monthlyRecurrencePattern = pattern as MonthlyRecurrencePattern;
			if (monthlyRecurrencePattern != null)
			{
				return new MonthlyRecurrencePattern(monthlyRecurrencePattern.DayOfMonth, monthlyRecurrencePattern.RecurrenceInterval, monthlyRecurrencePattern.CalendarType);
			}
			MonthlyThRecurrencePattern monthlyThRecurrencePattern = pattern as MonthlyThRecurrencePattern;
			if (monthlyThRecurrencePattern != null)
			{
				return new MonthlyThRecurrencePattern(monthlyThRecurrencePattern.DaysOfWeek, monthlyThRecurrencePattern.Order, monthlyThRecurrencePattern.RecurrenceInterval, monthlyThRecurrencePattern.CalendarType);
			}
			WeeklyRecurrencePattern weeklyRecurrencePattern = pattern as WeeklyRecurrencePattern;
			if (weeklyRecurrencePattern != null)
			{
				return new WeeklyRecurrencePattern(weeklyRecurrencePattern.DaysOfWeek, weeklyRecurrencePattern.RecurrenceInterval, weeklyRecurrencePattern.FirstDayOfWeek);
			}
			YearlyRecurrencePattern yearlyRecurrencePattern = pattern as YearlyRecurrencePattern;
			if (yearlyRecurrencePattern != null)
			{
				return new YearlyRecurrencePattern(yearlyRecurrencePattern.DayOfMonth, yearlyRecurrencePattern.Month, yearlyRecurrencePattern.IsLeapMonth, yearlyRecurrencePattern.CalendarType);
			}
			YearlyThRecurrencePattern yearlyThRecurrencePattern = pattern as YearlyThRecurrencePattern;
			if (yearlyThRecurrencePattern != null)
			{
				return new YearlyThRecurrencePattern(yearlyThRecurrencePattern.DaysOfWeek, yearlyThRecurrencePattern.Order, yearlyThRecurrencePattern.Month, yearlyThRecurrencePattern.IsLeapMonth, yearlyThRecurrencePattern.CalendarType);
			}
			throw new ArgumentException("Unhandled RecurrencePattern type.");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00032008 File Offset: 0x00030208
		public override void SetFrom(CalendarItemBase calendarItemBase)
		{
			base.SetFrom(calendarItemBase);
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			if (calendarItem != null)
			{
				this.recurrence = CalendarItemData.CloneRecurrence(calendarItem.Recurrence);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00032038 File Offset: 0x00030238
		public override EditCalendarItemHelper.CalendarItemUpdateFlags CopyTo(CalendarItemBase calendarItemBase)
		{
			EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags = base.CopyTo(calendarItemBase);
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			if (calendarItem != null && !CalendarItemData.IsRecurrenceEqual(calendarItem.Recurrence, this.recurrence))
			{
				calendarItem.Recurrence = CalendarItemData.CloneRecurrence(this.recurrence);
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			return calendarItemUpdateFlags;
		}

		// Token: 0x04000481 RID: 1153
		private Recurrence recurrence;
	}
}
