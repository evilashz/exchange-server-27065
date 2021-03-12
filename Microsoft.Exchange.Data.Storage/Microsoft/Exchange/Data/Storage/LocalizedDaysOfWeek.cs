using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200041A RID: 1050
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct LocalizedDaysOfWeek : IFormattable
	{
		// Token: 0x06002F2F RID: 12079 RVA: 0x000C285B File Offset: 0x000C0A5B
		public LocalizedDaysOfWeek(DaysOfWeek daysOfWeek, DayOfWeek firstDayOfWeek)
		{
			this.daysOfWeek = daysOfWeek;
			this.firstDayOfWeek = firstDayOfWeek;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000C286B File Offset: 0x000C0A6B
		public LocalizedDaysOfWeek(DaysOfWeek daysOfWeek)
		{
			this.daysOfWeek = daysOfWeek;
			this.firstDayOfWeek = DayOfWeek.Sunday;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000C287C File Offset: 0x000C0A7C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			DateTimeFormatInfo dateTimeFormatInfo = LocalizedDayOfWeek.GetDateTimeFormatInfo(formatProvider);
			int num = (int)dateTimeFormatInfo.FirstDayOfWeek;
			DaysOfWeek daysOfWeek = (DaysOfWeek)(1 << num | 1 << (num + 6) % 7);
			DaysOfWeek daysOfWeek2 = DaysOfWeek.AllDays & ~daysOfWeek;
			if (this.daysOfWeek == daysOfWeek2)
			{
				if (format == "s")
				{
					return ClientStrings.WhenEveryWeekDay.ToString(formatProvider);
				}
				if (format == "p")
				{
					return ClientStrings.WhenOnWeekDays.ToString(formatProvider);
				}
				return ClientStrings.WhenWeekDays.ToString(formatProvider);
			}
			else
			{
				if (this.daysOfWeek == daysOfWeek)
				{
					return ClientStrings.WhenBothWeekendDays.ToString(formatProvider);
				}
				if (this.daysOfWeek != DaysOfWeek.AllDays)
				{
					return LocalizedDaysOfWeek.EnumerateDaysOfWeek(this.daysOfWeek).ToString(formatProvider);
				}
				if (format == "s")
				{
					return ClientStrings.WhenEveryDay.ToString(formatProvider);
				}
				if (format == "p")
				{
					return ClientStrings.WhenOnEveryDayOfTheWeek.ToString(formatProvider);
				}
				return ClientStrings.WhenAllDays.ToString(formatProvider);
			}
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000C2984 File Offset: 0x000C0B84
		internal static LocalizedString EnumerateDaysOfWeek(DaysOfWeek daysOfWeek)
		{
			List<IFormattable> list = new List<IFormattable>();
			int num = 0;
			for (DaysOfWeek daysOfWeek2 = DaysOfWeek.Sunday; daysOfWeek2 <= DaysOfWeek.Saturday; daysOfWeek2 <<= 1)
			{
				if ((daysOfWeek & daysOfWeek2) == daysOfWeek2)
				{
					list.Add(new LocalizedDayOfWeek((DayOfWeek)num));
				}
				num++;
			}
			switch (list.Count)
			{
			case 1:
				return ClientStrings.WhenOneDayOfWeek(list[0]);
			case 2:
				return ClientStrings.WhenTwoDaysOfWeek(list[0], list[1]);
			case 3:
				return ClientStrings.WhenThreeDaysOfWeek(list[0], list[1], list[2]);
			case 4:
				return ClientStrings.WhenFourDaysOfWeek(list[0], list[1], list[2], list[3]);
			case 5:
				return ClientStrings.WhenFiveDaysOfWeek(list[0], list[1], list[2], list[3], list[4]);
			case 6:
				return ClientStrings.WhenSixDaysOfWeek(list[0], list[1], list[2], list[3], list[4], list[5]);
			case 7:
				return ClientStrings.WhenSevenDaysOfWeek(list[0], list[1], list[2], list[3], list[4], list[5], list[6]);
			default:
				ExDiagnostics.FailFast("Wrong daysofweek", true);
				return default(LocalizedString);
			}
		}

		// Token: 0x040019DA RID: 6618
		private DaysOfWeek daysOfWeek;

		// Token: 0x040019DB RID: 6619
		private DayOfWeek firstDayOfWeek;
	}
}
