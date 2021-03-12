using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003A1 RID: 929
	[ComVisible(true)]
	[Serializable]
	public class KoreanCalendar : Calendar
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000B896B File Offset: 0x000B6B6B
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000B8972 File Offset: 0x000B6B72
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000B8979 File Offset: 0x000B6B79
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000B897C File Offset: 0x000B6B7C
		public KoreanCalendar()
		{
			try
			{
				new CultureInfo("ko-KR");
			}
			catch (ArgumentException innerException)
			{
				throw new TypeInitializationException(base.GetType().FullName, innerException);
			}
			this.helper = new GregorianCalendarHelper(this, KoreanCalendar.koreanEraInfo);
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000B89D0 File Offset: 0x000B6BD0
		internal override int ID
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000B89D3 File Offset: 0x000B6BD3
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000B89E2 File Offset: 0x000B6BE2
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000B89F1 File Offset: 0x000B6BF1
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000B8A01 File Offset: 0x000B6C01
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000B8A10 File Offset: 0x000B6C10
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000B8A1E File Offset: 0x000B6C1E
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000B8A2C File Offset: 0x000B6C2C
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000B8A3A File Offset: 0x000B6C3A
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000B8A49 File Offset: 0x000B6C49
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000B8A59 File Offset: 0x000B6C59
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000B8A67 File Offset: 0x000B6C67
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000B8A75 File Offset: 0x000B6C75
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000B8A83 File Offset: 0x000B6C83
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000B8A95 File Offset: 0x000B6C95
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000B8AA4 File Offset: 0x000B6CA4
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000B8AB3 File Offset: 0x000B6CB3
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000B8AC4 File Offset: 0x000B6CC4
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000B8AE9 File Offset: 0x000B6CE9
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000B8AF6 File Offset: 0x000B6CF6
		// (set) Token: 0x06003023 RID: 12323 RVA: 0x000B8B20 File Offset: 0x000B6D20
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 4362);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000B8B83 File Offset: 0x000B6D83
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x04001454 RID: 5204
		public const int KoreanEra = 1;

		// Token: 0x04001455 RID: 5205
		internal static EraInfo[] koreanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -2333, 2334, 12332)
		};

		// Token: 0x04001456 RID: 5206
		internal GregorianCalendarHelper helper;

		// Token: 0x04001457 RID: 5207
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 4362;
	}
}
