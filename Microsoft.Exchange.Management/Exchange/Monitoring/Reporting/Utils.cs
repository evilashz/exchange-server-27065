using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005F1 RID: 1521
	internal sealed class Utils
	{
		// Token: 0x06003632 RID: 13874 RVA: 0x000DF38C File Offset: 0x000DD58C
		public static void GetStartEndDateForReportingPeriod(ReportingPeriod reportingPeriod, out ExDateTime startDateTime, out ExDateTime endDateTime)
		{
			ExDateTime date = ExDateTime.UtcNow.Date;
			switch (reportingPeriod)
			{
			case ReportingPeriod.Today:
				startDateTime = date;
				endDateTime = startDateTime.Add(TimeSpan.FromDays(1.0)).Subtract(TimeSpan.FromSeconds(1.0));
				return;
			case ReportingPeriod.Yesterday:
				startDateTime = date.Subtract(TimeSpan.FromDays(1.0));
				endDateTime = startDateTime.AddDays(1.0).Subtract(TimeSpan.FromSeconds(1.0));
				return;
			case ReportingPeriod.LastWeek:
				startDateTime = date.Subtract(TimeSpan.FromDays((double)(7 + date.DayOfWeek)));
				endDateTime = date.Subtract(TimeSpan.FromDays((double)date.DayOfWeek)).Subtract(TimeSpan.FromSeconds(1.0));
				return;
			case ReportingPeriod.LastMonth:
				startDateTime = Utils.SubtractMonths(date, 1);
				endDateTime = Utils.GetLastMonthLastDate(date);
				return;
			case ReportingPeriod.Last3Months:
				startDateTime = Utils.SubtractMonths(date, 3);
				endDateTime = Utils.GetLastMonthLastDate(date);
				return;
			case ReportingPeriod.Last6Months:
				startDateTime = Utils.SubtractMonths(date, 6);
				endDateTime = Utils.GetLastMonthLastDate(date);
				return;
			case ReportingPeriod.Last12Months:
				startDateTime = Utils.SubtractMonths(date, 12);
				endDateTime = Utils.GetLastMonthLastDate(date);
				return;
			default:
				throw new ArgumentException(Strings.InvalidReportingPeriod);
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000DF518 File Offset: 0x000DD718
		public static IEnumerable<StartEndDateTimePair> GetAllDaysInGivenRange(ExDateTime startDate, ExDateTime endDate)
		{
			List<StartEndDateTimePair> list = new List<StartEndDateTimePair>();
			if (endDate.Date < startDate.Date)
			{
				throw new ArgumentException(Strings.InvalidTimeRange, "StartDate");
			}
			if (endDate.Date == startDate.Date)
			{
				list.Add(new StartEndDateTimePair(startDate.Date, startDate.Date.AddDays(1.0).Subtract(TimeSpan.FromSeconds(1.0))));
			}
			else if (endDate.Date > startDate.Date)
			{
				ExDateTime t = startDate.Date;
				while (t <= endDate.Date)
				{
					list.Add(new StartEndDateTimePair(t.Date, t.Date.AddDays(1.0).Subtract(TimeSpan.FromSeconds(1.0))));
					t = t.AddDays(1.0);
				}
			}
			return list;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000DF634 File Offset: 0x000DD834
		private static ExDateTime SubtractMonths(ExDateTime dateTime, int monthsToSubtract)
		{
			int num = dateTime.Year;
			int num2 = dateTime.Month;
			num2 -= monthsToSubtract;
			if (num2 <= 0)
			{
				num--;
				num2 += 12;
			}
			return new ExDateTime(ExTimeZone.UtcTimeZone, num, num2, 1);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000DF670 File Offset: 0x000DD870
		private static ExDateTime GetLastMonthLastDate(ExDateTime datetime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, datetime.Year, datetime.Month, 1).Subtract(TimeSpan.FromSeconds(1.0));
		}

		// Token: 0x04002506 RID: 9478
		private const int TotalDaysInWeek = 7;
	}
}
