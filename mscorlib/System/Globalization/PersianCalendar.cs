using System;

namespace System.Globalization
{
	// Token: 0x0200039C RID: 924
	[Serializable]
	public class PersianCalendar : Calendar
	{
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000B5997 File Offset: 0x000B3B97
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return PersianCalendar.minDate;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000B599E File Offset: 0x000B3B9E
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return PersianCalendar.maxDate;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x000B59A5 File Offset: 0x000B3BA5
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000B59B0 File Offset: 0x000B3BB0
		internal override int BaseCalendarID
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000B59B3 File Offset: 0x000B3BB3
		internal override int ID
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000B59B8 File Offset: 0x000B3BB8
		private long GetAbsoluteDatePersian(int year, int month, int day)
		{
			if (year >= 1 && year <= 9378 && month >= 1 && month <= 12)
			{
				int num = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
				int num2 = (int)(365.242189 * (double)(year - 1));
				long num3 = CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long)num2 + 180L);
				return num3 + (long)num;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000B5A24 File Offset: 0x000B3C24
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), PersianCalendar.minDate, PersianCalendar.maxDate));
			}
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000B5A7E File Offset: 0x000B3C7E
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != PersianCalendar.PersianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000B5AA0 File Offset: 0x000B3CA0
		internal static void CheckYearRange(int year, int era)
		{
			PersianCalendar.CheckEraRange(era);
			if (year < 1 || year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 9378));
			}
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000B5AF0 File Offset: 0x000B3CF0
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378 && month > 10)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 10));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000B5B5C File Offset: 0x000B3D5C
		private static int MonthFromOrdinalDay(int ordinalDay)
		{
			int num = 0;
			while (ordinalDay > PersianCalendar.DaysToMonth[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000B5B7C File Offset: 0x000B3D7C
		private static int DaysInPreviousMonths(int month)
		{
			month--;
			return PersianCalendar.DaysToMonth[month];
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000B5B8C File Offset: 0x000B3D8C
		internal int GetDatePart(long ticks, int part)
		{
			PersianCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			long num2 = CalendricalCalculationsHelper.PersianNewYearOnOrBefore(num);
			int num3 = (int)Math.Floor((double)(num2 - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
			if (part == 0)
			{
				return num3;
			}
			int num4 = (int)(num - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(num3, 1, 1, 0, 0, 0, 0, 1)));
			if (part == 1)
			{
				return num4;
			}
			int num5 = PersianCalendar.MonthFromOrdinalDay(num4);
			if (part == 2)
			{
				return num5;
			}
			int result = num4 - PersianCalendar.DaysInPreviousMonths(num5);
			if (part == 3)
			{
				return result;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000B5C2C File Offset: 0x000B3E2C
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), -120000, 120000));
			}
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long ticks = this.GetAbsoluteDatePersian(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000B5D2A File Offset: 0x000B3F2A
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000B5D37 File Offset: 0x000B3F37
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000B5D47 File Offset: 0x000B3F47
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000B5D60 File Offset: 0x000B3F60
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000B5D70 File Offset: 0x000B3F70
		public override int GetDaysInMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			if (month == 10 && year == 9378)
			{
				return 13;
			}
			int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
			if (month == 12 && !this.IsLeapYear(year))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000B5DBA File Offset: 0x000B3FBA
		public override int GetDaysInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return PersianCalendar.DaysToMonth[9] + 13;
			}
			if (!this.IsLeapYear(year, 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000B5DEC File Offset: 0x000B3FEC
		public override int GetEra(DateTime time)
		{
			PersianCalendar.CheckTicksRange(time.Ticks);
			return PersianCalendar.PersianEra;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000B5DFF File Offset: 0x000B3FFF
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					PersianCalendar.PersianEra
				};
			}
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000B5E0F File Offset: 0x000B400F
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000B5E1F File Offset: 0x000B401F
		public override int GetMonthsInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return 10;
			}
			return 12;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000B5E35 File Offset: 0x000B4035
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000B5E48 File Offset: 0x000B4048
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000B5EAA File Offset: 0x000B40AA
		public override int GetLeapMonth(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return 0;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000B5EB4 File Offset: 0x000B40B4
		public override bool IsLeapMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000B5EBF File Offset: 0x000B40BF
		public override bool IsLeapYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return year != 9378 && this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000B5EF0 File Offset: 0x000B40F0
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), daysInMonth, month));
			}
			long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
			if (absoluteDatePersian >= 0L)
			{
				return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000B5F79 File Offset: 0x000B4179
		// (set) Token: 0x06002F8F RID: 12175 RVA: 0x000B5FA0 File Offset: 0x000B41A0
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9378)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, 9378));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000B5FF8 File Offset: 0x000B41F8
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (year < 100)
			{
				return base.ToFourDigitYear(year);
			}
			if (year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 9378));
			}
			return year;
		}

		// Token: 0x04001406 RID: 5126
		public static readonly int PersianEra = 1;

		// Token: 0x04001407 RID: 5127
		internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;

		// Token: 0x04001408 RID: 5128
		private const int ApproximateHalfYear = 180;

		// Token: 0x04001409 RID: 5129
		internal const int DatePartYear = 0;

		// Token: 0x0400140A RID: 5130
		internal const int DatePartDayOfYear = 1;

		// Token: 0x0400140B RID: 5131
		internal const int DatePartMonth = 2;

		// Token: 0x0400140C RID: 5132
		internal const int DatePartDay = 3;

		// Token: 0x0400140D RID: 5133
		internal const int MonthsPerYear = 12;

		// Token: 0x0400140E RID: 5134
		internal static int[] DaysToMonth = new int[]
		{
			0,
			31,
			62,
			93,
			124,
			155,
			186,
			216,
			246,
			276,
			306,
			336,
			366
		};

		// Token: 0x0400140F RID: 5135
		internal const int MaxCalendarYear = 9378;

		// Token: 0x04001410 RID: 5136
		internal const int MaxCalendarMonth = 10;

		// Token: 0x04001411 RID: 5137
		internal const int MaxCalendarDay = 13;

		// Token: 0x04001412 RID: 5138
		internal static DateTime minDate = new DateTime(622, 3, 22);

		// Token: 0x04001413 RID: 5139
		internal static DateTime maxDate = DateTime.MaxValue;

		// Token: 0x04001414 RID: 5140
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;
	}
}
