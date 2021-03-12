using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200025B RID: 603
	internal class WorkingHoursAwareReminderTimeCalculator : IReminderTimeCalculator
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x0007E37F File Offset: 0x0007C57F
		internal WorkingHoursAwareReminderTimeCalculator(ReminderTimeCalculatorContext context, StorageWorkingHours workingHours)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			this.context = context;
			this.workingHours = workingHours;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0007E3A0 File Offset: 0x0007C5A0
		public ExDateTime CalculateReminderTime(IModernReminder reminder, ExDateTime nowInUTC)
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "WorkingHoursAwareReminderTimeCalculator.CalculateReminderTime");
			if (reminder.ReminderTimeHint == ReminderTimeHint.Custom)
			{
				return reminder.CustomReminderTime;
			}
			ExDateTime referenceTime = this.context.TimeZone.ConvertDateTime(reminder.ReferenceTime);
			ExDateTime exDateTime;
			if (reminder.ReminderTimeHint == ReminderTimeHint.Someday)
			{
				exDateTime = ExDateTime.MaxValue;
			}
			else
			{
				ExDateRange dateRange = WorkingHoursAwareReminderTimeCalculator.ConvertReminderTimeHintToExDateRange(reminder.ReminderTimeHint, referenceTime, this.context);
				List<ExDateRange> dateRanges = WorkingHoursAwareReminderTimeCalculator.ConvertHoursToDateRanges(referenceTime, reminder.Hours, this.workingHours, 14);
				exDateTime = WorkingHoursAwareReminderTimeCalculator.FindFirstIntersection(dateRange, dateRanges).Start;
			}
			ExTraceGlobals.HeuristicsTracer.TraceInformation<ExDateTime>(0, 0L, "WorkingHoursAwareReminderTimeCalculator calculated reminder time: {0}", exDateTime);
			return exDateTime;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0007E45C File Offset: 0x0007C65C
		internal static ExDateRange FindFirstIntersection(ExDateRange dateRange, List<ExDateRange> dateRanges)
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "WorkingHoursAwareReminderTimeCalculator.FindIntersection");
			dateRanges.Sort();
			foreach (ExDateRange b in dateRanges)
			{
				if (ExDateRange.AreOverlapping(dateRange, b))
				{
					return ExDateRange.Intersection(dateRange, b);
				}
			}
			ExDateRange exDateRange = dateRanges.FirstOrDefault((ExDateRange x) => x.CompareTo(dateRange) > 0);
			return exDateRange ?? dateRanges.Last<ExDateRange>();
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0007E508 File Offset: 0x0007C708
		internal static List<ExDateRange> ConvertHoursToDateRanges(ExDateTime referenceTime, Hours hours, StorageWorkingHours workingHours, int days)
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "WorkingHoursAwareReminderTimeCalculator.ConvertHoursToDateRanges");
			ExTraceGlobals.HeuristicsTracer.TraceInformation<Hours, StorageWorkingHours, int>(0, 0L, "Hours: '{0}'; WorkingHours: '{1}'; Days: '{2}'", hours, workingHours, days);
			List<ExDateRange> list = new List<ExDateRange>();
			ExDateRange exDateRange = new ExDateRange(referenceTime.ToUtc(), referenceTime.AddDays((double)days).ToUtc());
			if (workingHours == null)
			{
				ExTraceGlobals.HeuristicsTracer.TraceInformation(0, 0L, "User WorkingHours unavailable");
				hours = Hours.Any;
			}
			switch (hours)
			{
			case Hours.Personal:
				list = ExDateRange.SubtractRanges(exDateRange, WorkingHoursAwareReminderTimeCalculator.ConvertHoursToDateRanges(referenceTime, Hours.Working, workingHours, days));
				break;
			case Hours.Working:
				for (int i = 0; i < days; i++)
				{
					ExDateTime date = referenceTime.AddDays((double)i).Date;
					if (workingHours.IsWorkingDay(date.DayOfWeek))
					{
						list.Add(new ExDateRange(date.AddMinutes((double)workingHours.StartTimeInMinutes).ToUtc(), date.AddMinutes((double)workingHours.EndTimeInMinutes).ToUtc()));
					}
				}
				break;
			case Hours.Any:
				list.Add(exDateRange);
				break;
			}
			return list;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0007E618 File Offset: 0x0007C818
		internal static ExDateRange ConvertReminderTimeHintToExDateRange(ReminderTimeHint hint, ExDateTime referenceTime, ReminderTimeCalculatorContext context)
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "WorkingHoursAwareReminderTimeCalculator.ConvertReminderTimeHintToExDateRange");
			ExTraceGlobals.HeuristicsTracer.TraceInformation<ReminderTimeHint, ExDateTime, DayOfWeek>(0, 0L, "ReminderTimeHint : '{0}'; ReferenceTime : '{1}'; StartOfWeek: '{2}'", hint, referenceTime, context.StartOfWeek);
			ExDateRange exDateRange;
			switch (hint)
			{
			case ReminderTimeHint.LaterToday:
				exDateRange = new ExDateRange(referenceTime.Add(WorkingHoursAwareReminderTimeCalculator.RemindLaterMinOffsetTimeSpan).ToUtc(), referenceTime.Add(WorkingHoursAwareReminderTimeCalculator.RemindLaterMinOffsetTimeSpan).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				goto IL_496;
			case ReminderTimeHint.Tomorrow:
				exDateRange = new ExDateRange(referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayStartTimeSpan).ToUtc(), referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				goto IL_496;
			case ReminderTimeHint.TomorrowMorning:
				exDateRange = new ExDateRange(referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.MorningStartTimeSpan).ToUtc(), referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.MorningEndTimeSpan).ToUtc());
				goto IL_496;
			case ReminderTimeHint.TomorrowAfternoon:
				exDateRange = new ExDateRange(referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.AfternoonStartTimeSpan).ToUtc(), referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.AfternoonEndTimeSpan).ToUtc());
				goto IL_496;
			case ReminderTimeHint.TomorrowEvening:
				exDateRange = new ExDateRange(referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.EveningStartTimeSpan).ToUtc(), referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.EveningEndTimeSpan).ToUtc());
				goto IL_496;
			case ReminderTimeHint.ThisWeekend:
			{
				int num = WorkingHoursAwareReminderTimeCalculator.DaysTilNextOccurrence(referenceTime, DayOfWeek.Saturday);
				if (num == 7)
				{
					return new ExDateRange(referenceTime.ToUtc(), referenceTime.AddDays(1.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				}
				if (num == 6)
				{
					return new ExDateRange(referenceTime.ToUtc(), referenceTime.Date.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				}
				return new ExDateRange(referenceTime.AddDays((double)num).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayStartTimeSpan).ToUtc(), referenceTime.AddDays((double)(num + 1)).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
			}
			case ReminderTimeHint.NextWeek:
			{
				ExDateTime date = referenceTime.AddDays((double)WorkingHoursAwareReminderTimeCalculator.DaysTilNextOccurrence(referenceTime, context.StartOfWeek)).Date;
				exDateRange = new ExDateRange(date.ToUtc(), date.AddDays(6.0).Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				goto IL_496;
			}
			case ReminderTimeHint.NextMonth:
			{
				int num2 = ExDateTime.DaysInMonth(referenceTime.Year, referenceTime.Month) - referenceTime.Day + 1;
				ExDateTime date2 = referenceTime.AddDays((double)num2).Date;
				ExDateTime exDateTime = date2.AddDays((double)(ExDateTime.DaysInMonth(date2.Year, date2.Month) - 1));
				exDateRange = new ExDateRange(date2.ToUtc(), exDateTime.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				goto IL_496;
			}
			case ReminderTimeHint.Now:
				exDateRange = new ExDateRange(referenceTime.ToUtc(), referenceTime.ToUtc());
				goto IL_496;
			case ReminderTimeHint.InTwoDays:
				exDateRange = new ExDateRange(referenceTime.AddDays(2.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayStartTimeSpan).ToUtc(), referenceTime.AddDays(2.0).Date.Add(WorkingHoursAwareReminderTimeCalculator.DayEndTimeSpan).ToUtc());
				goto IL_496;
			}
			throw new InvalidOperationException("unsupported reminderTimeHint");
			IL_496:
			ExTraceGlobals.HeuristicsTracer.TraceInformation<ExDateTime, ExDateTime>(0, 0L, "Calculated reminder range : '{0}' - '{1}'", exDateRange.Start, exDateRange.End);
			return exDateRange;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0007EADC File Offset: 0x0007CCDC
		internal static int DaysTilNextOccurrence(ExDateTime dateTime, DayOfWeek dayOfWeek)
		{
			int num = dayOfWeek - dateTime.DayOfWeek;
			if (num > 0)
			{
				return num;
			}
			return num + 7;
		}

		// Token: 0x04000D24 RID: 3364
		internal const int DaysToCheck = 14;

		// Token: 0x04000D25 RID: 3365
		internal static TimeSpan DayStartTimeSpan = new TimeSpan(0, 0, 0);

		// Token: 0x04000D26 RID: 3366
		internal static TimeSpan DayEndTimeSpan = new TimeSpan(23, 59, 59);

		// Token: 0x04000D27 RID: 3367
		internal static TimeSpan MorningStartTimeSpan = new TimeSpan(8, 0, 0);

		// Token: 0x04000D28 RID: 3368
		internal static TimeSpan MorningEndTimeSpan = new TimeSpan(12, 0, 0);

		// Token: 0x04000D29 RID: 3369
		internal static TimeSpan AfternoonStartTimeSpan = new TimeSpan(13, 0, 0);

		// Token: 0x04000D2A RID: 3370
		internal static TimeSpan AfternoonEndTimeSpan = new TimeSpan(17, 0, 0);

		// Token: 0x04000D2B RID: 3371
		internal static TimeSpan EveningStartTimeSpan = new TimeSpan(18, 0, 0);

		// Token: 0x04000D2C RID: 3372
		internal static TimeSpan EveningEndTimeSpan = new TimeSpan(21, 0, 0);

		// Token: 0x04000D2D RID: 3373
		internal static TimeSpan RemindLaterMinOffsetTimeSpan = new TimeSpan(0, 0, 5);

		// Token: 0x04000D2E RID: 3374
		private readonly StorageWorkingHours workingHours;

		// Token: 0x04000D2F RID: 3375
		private readonly ReminderTimeCalculatorContext context;
	}
}
