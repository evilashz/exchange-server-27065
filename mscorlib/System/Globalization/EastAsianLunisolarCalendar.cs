using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000398 RID: 920
	[ComVisible(true)]
	[Serializable]
	public abstract class EastAsianLunisolarCalendar : Calendar
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000B443F File Offset: 0x000B263F
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000B4444 File Offset: 0x000B2644
		public virtual int GetSexagenaryYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return (num - 4) % 60 + 1;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000B447C File Offset: 0x000B267C
		public int GetCelestialStem(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					1,
					60
				}));
			}
			return (sexagenaryYear - 1) % 10 + 1;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000B44C8 File Offset: 0x000B26C8
		public int GetTerrestrialBranch(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					1,
					60
				}));
			}
			return (sexagenaryYear - 1) % 12 + 1;
		}

		// Token: 0x06002F03 RID: 12035
		internal abstract int GetYearInfo(int LunarYear, int Index);

		// Token: 0x06002F04 RID: 12036
		internal abstract int GetYear(int year, DateTime time);

		// Token: 0x06002F05 RID: 12037
		internal abstract int GetGregorianYear(int year, int era);

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002F06 RID: 12038
		internal abstract int MinCalendarYear { get; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002F07 RID: 12039
		internal abstract int MaxCalendarYear { get; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002F08 RID: 12040
		internal abstract EraInfo[] CalEraInfo { get; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002F09 RID: 12041
		internal abstract DateTime MinDate { get; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002F0A RID: 12042
		internal abstract DateTime MaxDate { get; }

		// Token: 0x06002F0B RID: 12043 RVA: 0x000B4514 File Offset: 0x000B2714
		internal int MinEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MinCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MinDate))
			{
				return this.GetYear(this.MinCalendarYear, this.MinDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].minEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000B4598 File Offset: 0x000B2798
		internal int MaxEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MaxCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MaxDate))
			{
				return this.GetYear(this.MaxCalendarYear, this.MaxDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].maxEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000B4619 File Offset: 0x000B2819
		internal EastAsianLunisolarCalendar()
		{
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000B4624 File Offset: 0x000B2824
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.MinSupportedDateTime.Ticks || ticks > this.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), this.MinSupportedDateTime, this.MaxSupportedDateTime));
			}
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000B4688 File Offset: 0x000B2888
		internal void CheckEraRange(int era)
		{
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era < this.GetEra(this.MinDate) || era > this.GetEra(this.MaxDate))
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000B46C8 File Offset: 0x000B28C8
		internal int CheckYearRange(int year, int era)
		{
			this.CheckEraRange(era);
			year = this.GetGregorianYear(year, era);
			if (year < this.MinCalendarYear || year > this.MaxCalendarYear)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					this.MinEraCalendarYear(era),
					this.MaxEraCalendarYear(era)
				}));
			}
			return year;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000B4734 File Offset: 0x000B2934
		internal int CheckYearMonthRange(int year, int month, int era)
		{
			year = this.CheckYearRange(year, era);
			if (month == 13 && this.GetYearInfo(year, 0) == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			return year;
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000B4790 File Offset: 0x000B2990
		internal int InternalGetDaysInMonth(int year, int month)
		{
			int num = 32768;
			num >>= month - 1;
			int result;
			if ((this.GetYearInfo(year, 3) & num) == 0)
			{
				result = 29;
			}
			else
			{
				result = 30;
			}
			return result;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000B47C1 File Offset: 0x000B29C1
		public override int GetDaysInMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			return this.InternalGetDaysInMonth(year, month);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000B47D6 File Offset: 0x000B29D6
		private static int GregorianIsLeapYear(int y)
		{
			if (y % 4 != 0)
			{
				return 0;
			}
			if (y % 100 != 0)
			{
				return 1;
			}
			if (y % 400 == 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000B47F4 File Offset: 0x000B29F4
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Day", new object[]
				{
					num,
					month
				}));
			}
			int year2 = 0;
			int month2 = 0;
			int day2 = 0;
			if (this.LunarToGregorian(year, month, day, ref year2, ref month2, ref day2))
			{
				return new DateTime(year2, month2, day2, hour, minute, second, millisecond);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000B4884 File Offset: 0x000B2A84
		internal void GregorianToLunar(int nSYear, int nSMonth, int nSDate, ref int nLYear, ref int nLMonth, ref int nLDate)
		{
			int num = EastAsianLunisolarCalendar.GregorianIsLeapYear(nSYear);
			int num2 = (num == 1) ? EastAsianLunisolarCalendar.DaysToMonth366[nSMonth - 1] : EastAsianLunisolarCalendar.DaysToMonth365[nSMonth - 1];
			num2 += nSDate;
			int i = num2;
			nLYear = nSYear;
			int yearInfo;
			int yearInfo2;
			if (nLYear == this.MaxCalendarYear + 1)
			{
				nLYear--;
				i += ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1) ? 366 : 365);
				yearInfo = this.GetYearInfo(nLYear, 1);
				yearInfo2 = this.GetYearInfo(nLYear, 2);
			}
			else
			{
				yearInfo = this.GetYearInfo(nLYear, 1);
				yearInfo2 = this.GetYearInfo(nLYear, 2);
				if (nSMonth < yearInfo || (nSMonth == yearInfo && nSDate < yearInfo2))
				{
					nLYear--;
					i += ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1) ? 366 : 365);
					yearInfo = this.GetYearInfo(nLYear, 1);
					yearInfo2 = this.GetYearInfo(nLYear, 2);
				}
			}
			i -= EastAsianLunisolarCalendar.DaysToMonth365[yearInfo - 1];
			i -= yearInfo2 - 1;
			int num3 = 32768;
			int yearInfo3 = this.GetYearInfo(nLYear, 3);
			int num4 = ((yearInfo3 & num3) != 0) ? 30 : 29;
			nLMonth = 1;
			while (i > num4)
			{
				i -= num4;
				nLMonth++;
				num3 >>= 1;
				num4 = (((yearInfo3 & num3) != 0) ? 30 : 29);
			}
			nLDate = i;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000B49CC File Offset: 0x000B2BCC
		internal bool LunarToGregorian(int nLYear, int nLMonth, int nLDate, ref int nSolarYear, ref int nSolarMonth, ref int nSolarDay)
		{
			if (nLDate < 1 || nLDate > 30)
			{
				return false;
			}
			int num = nLDate - 1;
			for (int i = 1; i < nLMonth; i++)
			{
				num += this.InternalGetDaysInMonth(nLYear, i);
			}
			int yearInfo = this.GetYearInfo(nLYear, 1);
			int yearInfo2 = this.GetYearInfo(nLYear, 2);
			int num2 = EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear);
			int[] array = (num2 == 1) ? EastAsianLunisolarCalendar.DaysToMonth366 : EastAsianLunisolarCalendar.DaysToMonth365;
			nSolarDay = yearInfo2;
			if (yearInfo > 1)
			{
				nSolarDay += array[yearInfo - 1];
			}
			nSolarDay += num;
			if (nSolarDay > num2 + 365)
			{
				nSolarYear = nLYear + 1;
				nSolarDay -= num2 + 365;
			}
			else
			{
				nSolarYear = nLYear;
			}
			nSolarMonth = 1;
			while (nSolarMonth < 12 && array[nSolarMonth] < nSolarDay)
			{
				nSolarMonth++;
			}
			nSolarDay -= array[nSolarMonth - 1];
			return true;
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000B4AA4 File Offset: 0x000B2CA4
		internal DateTime LunarToTime(DateTime time, int year, int month, int day)
		{
			int year2 = 0;
			int month2 = 0;
			int day2 = 0;
			this.LunarToGregorian(year, month, day, ref year2, ref month2, ref day2);
			return GregorianCalendar.GetDefaultInstance().ToDateTime(year2, month2, day2, time.Hour, time.Minute, time.Second, time.Millisecond);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000B4AF4 File Offset: 0x000B2CF4
		internal void TimeToLunar(DateTime time, ref int year, ref int month, ref int day)
		{
			Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
			int year2 = defaultInstance.GetYear(time);
			int month2 = defaultInstance.GetMonth(time);
			int dayOfMonth = defaultInstance.GetDayOfMonth(time);
			this.GregorianToLunar(year2, month2, dayOfMonth, ref year, ref month, ref day);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000B4B34 File Offset: 0x000B2D34
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					-120000,
					120000
				}));
			}
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			int i = num2 + months;
			if (i > 0)
			{
				int num4 = this.InternalIsLeapYear(num) ? 13 : 12;
				while (i - num4 > 0)
				{
					i -= num4;
					num++;
					num4 = (this.InternalIsLeapYear(num) ? 13 : 12);
				}
				num2 = i;
			}
			else
			{
				while (i <= 0)
				{
					int num5 = this.InternalIsLeapYear(num - 1) ? 13 : 12;
					i += num5;
					num--;
				}
				num2 = i;
			}
			int num6 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num6)
			{
				num3 = num6;
			}
			DateTime result = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(result.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return result;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000B4C40 File Offset: 0x000B2E40
		public override DateTime AddYears(DateTime time, int years)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			num += years;
			if (num2 == 13 && !this.InternalIsLeapYear(num))
			{
				num2 = 12;
				num3 = this.InternalGetDaysInMonth(num, num2);
			}
			int num4 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num4)
			{
				num3 = num4;
			}
			DateTime result = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(result.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return result;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000B4CC0 File Offset: 0x000B2EC0
		public override int GetDayOfYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int year = 0;
			int num = 0;
			int num2 = 0;
			this.TimeToLunar(time, ref year, ref num, ref num2);
			for (int i = 1; i < num; i++)
			{
				num2 += this.InternalGetDaysInMonth(year, i);
			}
			return num2;
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000B4D08 File Offset: 0x000B2F08
		public override int GetDayOfMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int result = 0;
			this.TimeToLunar(time, ref num, ref num2, ref result);
			return result;
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000B4D38 File Offset: 0x000B2F38
		public override int GetDaysInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int num = 0;
			int num2 = this.InternalIsLeapYear(year) ? 13 : 12;
			while (num2 != 0)
			{
				num += this.InternalGetDaysInMonth(year, num2--);
			}
			return num;
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000B4D78 File Offset: 0x000B2F78
		public override int GetMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int result = 0;
			int num2 = 0;
			this.TimeToLunar(time, ref num, ref result, ref num2);
			return result;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000B4DA8 File Offset: 0x000B2FA8
		public override int GetYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int year = 0;
			int num = 0;
			int num2 = 0;
			this.TimeToLunar(time, ref year, ref num, ref num2);
			return this.GetYear(year, time);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000B4DDD File Offset: 0x000B2FDD
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000B4E03 File Offset: 0x000B3003
		public override int GetMonthsInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			if (!this.InternalIsLeapYear(year))
			{
				return 12;
			}
			return 13;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000B4E20 File Offset: 0x000B3020
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Day", new object[]
				{
					num,
					month
				}));
			}
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000B4E8C File Offset: 0x000B308C
		public override bool IsLeapMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000B4EB8 File Offset: 0x000B30B8
		public override int GetLeapMonth(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int yearInfo = this.GetYearInfo(year, 0);
			if (yearInfo > 0)
			{
				return yearInfo + 1;
			}
			return 0;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000B4EE1 File Offset: 0x000B30E1
		internal bool InternalIsLeapYear(int year)
		{
			return this.GetYearInfo(year, 0) != 0;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000B4EEE File Offset: 0x000B30EE
		public override bool IsLeapYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			return this.InternalIsLeapYear(year);
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000B4F01 File Offset: 0x000B3101
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x000B4F38 File Offset: 0x000B3138
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.BaseCalendarID, this.GetYear(new DateTime(2029, 1, 1)));
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxCalendarYear)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
					{
						99,
						this.MaxCalendarYear
					}));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000B4F93 File Offset: 0x000B3193
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			year = base.ToFourDigitYear(year);
			this.CheckYearRange(year, 0);
			return year;
		}

		// Token: 0x040013D6 RID: 5078
		internal const int LeapMonth = 0;

		// Token: 0x040013D7 RID: 5079
		internal const int Jan1Month = 1;

		// Token: 0x040013D8 RID: 5080
		internal const int Jan1Date = 2;

		// Token: 0x040013D9 RID: 5081
		internal const int nDaysPerMonth = 3;

		// Token: 0x040013DA RID: 5082
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334
		};

		// Token: 0x040013DB RID: 5083
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335
		};

		// Token: 0x040013DC RID: 5084
		internal const int DatePartYear = 0;

		// Token: 0x040013DD RID: 5085
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040013DE RID: 5086
		internal const int DatePartMonth = 2;

		// Token: 0x040013DF RID: 5087
		internal const int DatePartDay = 3;

		// Token: 0x040013E0 RID: 5088
		internal const int MaxCalendarMonth = 13;

		// Token: 0x040013E1 RID: 5089
		internal const int MaxCalendarDay = 30;

		// Token: 0x040013E2 RID: 5090
		private const int DEFAULT_GREGORIAN_TWO_DIGIT_YEAR_MAX = 2029;
	}
}
