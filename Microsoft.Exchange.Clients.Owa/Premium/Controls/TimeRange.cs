using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000428 RID: 1064
	internal sealed class TimeRange
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000DBE4D File Offset: 0x000DA04D
		public TimeRange.RangeId Range
		{
			get
			{
				return this.range;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000DBE55 File Offset: 0x000DA055
		public ExDateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000DBE5D File Offset: 0x000DA05D
		public ExDateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000DBE65 File Offset: 0x000DA065
		private TimeRange(TimeRange.RangeId range, ExDateTime start, ExDateTime end)
		{
			this.range = range;
			this.start = start;
			this.end = end;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000DBE84 File Offset: 0x000DA084
		public static List<TimeRange> GetTimeRanges(UserContext userContext)
		{
			TimeRange.RangeId rangeId = TimeRange.RangeId.All;
			ExDateTime normalizedDate = TimeRange.GetNormalizedDate(DateTimeUtilities.GetLocalTime());
			int num = (7 + (normalizedDate.DayOfWeek - userContext.UserOptions.WeekStartDay)) % 7;
			ExDateTime normalizedDate2 = TimeRange.GetNormalizedDate(normalizedDate.IncrementDays(1));
			ExDateTime normalizedDate3 = TimeRange.GetNormalizedDate(normalizedDate.IncrementDays(-1));
			ExDateTime normalizedDate4 = TimeRange.GetNormalizedDate(normalizedDate.IncrementDays(7 - num));
			ExDateTime normalizedDate5 = TimeRange.GetNormalizedDate(normalizedDate4.IncrementDays(7));
			ExDateTime normalizedDate6 = TimeRange.GetNormalizedDate(normalizedDate5.IncrementDays(7));
			ExDateTime normalizedDate7 = TimeRange.GetNormalizedDate(normalizedDate6.IncrementDays(7));
			ExDateTime normalizedDate8 = TimeRange.GetNormalizedDate(normalizedDate.IncrementDays(-1 * (7 + num)));
			ExDateTime normalizedDate9 = TimeRange.GetNormalizedDate(normalizedDate8.IncrementDays(-7));
			ExDateTime normalizedDate10 = TimeRange.GetNormalizedDate(normalizedDate9.IncrementDays(-7));
			if (normalizedDate7.Month != normalizedDate.Month)
			{
				rangeId &= ~TimeRange.RangeId.LaterThisMonth;
			}
			if (num != 6)
			{
				rangeId &= ~TimeRange.RangeId.Tomorrow;
			}
			if (num != 0)
			{
				rangeId &= ~TimeRange.RangeId.Yesterday;
			}
			if (normalizedDate10.Month != normalizedDate.Month || 1 >= normalizedDate10.Day)
			{
				rangeId &= ~TimeRange.RangeId.EarlierThisMonth;
			}
			List<TimeRange> list = new List<TimeRange>(18);
			ExDateTime exDateTime = new ExDateTime(userContext.TimeZone, normalizedDate.Year, normalizedDate.Month, 1);
			exDateTime = TimeRange.GetNormalizedDate(exDateTime.AddMonths(2));
			ExDateTime exDateTime2 = ExDateTime.MaxValue;
			list.Insert(0, new TimeRange(TimeRange.RangeId.BeyondNextMonth, exDateTime, exDateTime2));
			exDateTime2 = exDateTime;
			int num2 = ExDateTime.DaysInMonth(normalizedDate.Year, normalizedDate.Month) - normalizedDate.Day;
			if (21 < num2)
			{
				exDateTime = TimeRange.GetNormalizedDate(exDateTime2.AddMonths(-1));
			}
			else
			{
				exDateTime = TimeRange.GetNormalizedDate(normalizedDate6.IncrementDays(7));
			}
			list.Insert(0, new TimeRange(TimeRange.RangeId.NextMonth, exDateTime, exDateTime2));
			if ((TimeRange.RangeId)0 < (rangeId & TimeRange.RangeId.LaterThisMonth))
			{
				exDateTime2 = exDateTime;
				exDateTime = TimeRange.GetNormalizedDate(normalizedDate6.IncrementDays(7));
				list.Insert(0, new TimeRange(TimeRange.RangeId.LaterThisMonth, exDateTime, exDateTime2));
			}
			exDateTime2 = exDateTime;
			exDateTime = normalizedDate6;
			list.Insert(0, new TimeRange(TimeRange.RangeId.ThreeWeeksAway, exDateTime, exDateTime2));
			exDateTime2 = exDateTime;
			exDateTime = normalizedDate5;
			list.Insert(0, new TimeRange(TimeRange.RangeId.TwoWeeksAway, exDateTime, exDateTime2));
			exDateTime2 = exDateTime;
			if (num == 6)
			{
				exDateTime = TimeRange.GetNormalizedDate(normalizedDate4.IncrementDays(1));
			}
			else
			{
				exDateTime = normalizedDate4;
			}
			list.Insert(0, new TimeRange(TimeRange.RangeId.NextWeek, exDateTime, exDateTime2));
			if ((TimeRange.RangeId)0 < (rangeId & TimeRange.RangeId.Tomorrow))
			{
				exDateTime2 = exDateTime;
				exDateTime = TimeRange.GetNormalizedDate(exDateTime.IncrementDays(-1));
				list.Insert(0, new TimeRange(TimeRange.RangeId.Tomorrow, exDateTime, exDateTime2));
			}
			int num3 = 7;
			while (0 < num3)
			{
				exDateTime2 = exDateTime;
				exDateTime = TimeRange.GetNormalizedDate(exDateTime.IncrementDays(-1));
				TimeRange.RangeId rangeId2 = TimeRange.RangeId.None;
				if (normalizedDate2.Equals(exDateTime))
				{
					rangeId2 = TimeRange.RangeId.Tomorrow;
				}
				else if (normalizedDate.Equals(exDateTime))
				{
					rangeId2 = TimeRange.RangeId.Today;
				}
				else if (normalizedDate3.Equals(exDateTime))
				{
					rangeId2 = TimeRange.RangeId.Yesterday;
				}
				else
				{
					switch (exDateTime.DayOfWeek)
					{
					case DayOfWeek.Sunday:
						rangeId2 = TimeRange.RangeId.Sunday;
						break;
					case DayOfWeek.Monday:
						rangeId2 = TimeRange.RangeId.Monday;
						break;
					case DayOfWeek.Tuesday:
						rangeId2 = TimeRange.RangeId.Tuesday;
						break;
					case DayOfWeek.Wednesday:
						rangeId2 = TimeRange.RangeId.Wednesday;
						break;
					case DayOfWeek.Thursday:
						rangeId2 = TimeRange.RangeId.Thursday;
						break;
					case DayOfWeek.Friday:
						rangeId2 = TimeRange.RangeId.Friday;
						break;
					case DayOfWeek.Saturday:
						rangeId2 = TimeRange.RangeId.Saturday;
						break;
					}
				}
				list.Insert(0, new TimeRange(rangeId2, exDateTime, exDateTime2));
				num3--;
			}
			if ((TimeRange.RangeId)0 < (rangeId & TimeRange.RangeId.Yesterday))
			{
				exDateTime2 = exDateTime;
				exDateTime = TimeRange.GetNormalizedDate(exDateTime.IncrementDays(-1));
				list.Insert(0, new TimeRange(TimeRange.RangeId.Yesterday, exDateTime, exDateTime2));
			}
			exDateTime2 = exDateTime;
			exDateTime = normalizedDate8;
			list.Insert(0, new TimeRange(TimeRange.RangeId.LastWeek, exDateTime, exDateTime2));
			exDateTime2 = exDateTime;
			exDateTime = normalizedDate9;
			list.Insert(0, new TimeRange(TimeRange.RangeId.TwoWeeksAgo, exDateTime, exDateTime2));
			exDateTime2 = exDateTime;
			exDateTime = normalizedDate10;
			list.Insert(0, new TimeRange(TimeRange.RangeId.ThreeWeeksAgo, exDateTime, exDateTime2));
			if ((TimeRange.RangeId)0 < (rangeId & TimeRange.RangeId.EarlierThisMonth))
			{
				exDateTime2 = exDateTime;
				exDateTime = TimeRange.GetNormalizedDate(exDateTime.IncrementDays(-1 * (exDateTime.Day - 1)));
				list.Insert(0, new TimeRange(TimeRange.RangeId.EarlierThisMonth, exDateTime, exDateTime2));
			}
			exDateTime2 = exDateTime;
			if (exDateTime2.Day == 1)
			{
				exDateTime = TimeRange.GetNormalizedDate(exDateTime.IncrementDays(-1));
			}
			exDateTime = TimeRange.GetNormalizedDate(exDateTime.IncrementDays(-1 * (exDateTime.Day - 1)));
			list.Insert(0, new TimeRange(TimeRange.RangeId.LastMonth, exDateTime, exDateTime2));
			exDateTime2 = exDateTime;
			exDateTime = ExDateTime.MinValue.AddTicks(1L);
			list.Insert(0, new TimeRange(TimeRange.RangeId.Older, exDateTime, exDateTime2));
			list.Insert(0, new TimeRange(TimeRange.RangeId.None, ExDateTime.MinValue, ExDateTime.MinValue));
			return list;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000DC34D File Offset: 0x000DA54D
		private static ExDateTime GetNormalizedDate(ExDateTime dateTime)
		{
			return dateTime.Date;
		}

		// Token: 0x04001A2A RID: 6698
		private TimeRange.RangeId range;

		// Token: 0x04001A2B RID: 6699
		private ExDateTime start;

		// Token: 0x04001A2C RID: 6700
		private ExDateTime end;

		// Token: 0x02000429 RID: 1065
		[Flags]
		internal enum RangeId
		{
			// Token: 0x04001A2E RID: 6702
			Older = 1,
			// Token: 0x04001A2F RID: 6703
			LastMonth = 2,
			// Token: 0x04001A30 RID: 6704
			EarlierThisMonth = 4,
			// Token: 0x04001A31 RID: 6705
			ThreeWeeksAgo = 8,
			// Token: 0x04001A32 RID: 6706
			TwoWeeksAgo = 16,
			// Token: 0x04001A33 RID: 6707
			LastWeek = 32,
			// Token: 0x04001A34 RID: 6708
			Sunday = 64,
			// Token: 0x04001A35 RID: 6709
			Monday = 128,
			// Token: 0x04001A36 RID: 6710
			Tuesday = 256,
			// Token: 0x04001A37 RID: 6711
			Wednesday = 512,
			// Token: 0x04001A38 RID: 6712
			Thursday = 1024,
			// Token: 0x04001A39 RID: 6713
			Friday = 2048,
			// Token: 0x04001A3A RID: 6714
			Saturday = 4096,
			// Token: 0x04001A3B RID: 6715
			NextWeek = 8192,
			// Token: 0x04001A3C RID: 6716
			TwoWeeksAway = 16384,
			// Token: 0x04001A3D RID: 6717
			ThreeWeeksAway = 32768,
			// Token: 0x04001A3E RID: 6718
			LaterThisMonth = 65536,
			// Token: 0x04001A3F RID: 6719
			NextMonth = 131072,
			// Token: 0x04001A40 RID: 6720
			BeyondNextMonth = 262144,
			// Token: 0x04001A41 RID: 6721
			Yesterday = 524288,
			// Token: 0x04001A42 RID: 6722
			Today = 1048576,
			// Token: 0x04001A43 RID: 6723
			Tomorrow = 2097152,
			// Token: 0x04001A44 RID: 6724
			None = 4194304,
			// Token: 0x04001A45 RID: 6725
			All = 16777215
		}
	}
}
