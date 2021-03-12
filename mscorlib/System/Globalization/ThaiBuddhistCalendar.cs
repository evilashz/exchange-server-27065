using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003A8 RID: 936
	[ComVisible(true)]
	[Serializable]
	public class ThaiBuddhistCalendar : Calendar
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x000BA1D5 File Offset: 0x000B83D5
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000BA1DC File Offset: 0x000B83DC
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000BA1E3 File Offset: 0x000B83E3
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000BA1E6 File Offset: 0x000B83E6
		public ThaiBuddhistCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, ThaiBuddhistCalendar.thaiBuddhistEraInfo);
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000BA1FF File Offset: 0x000B83FF
		internal override int ID
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000BA202 File Offset: 0x000B8402
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000BA211 File Offset: 0x000B8411
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000BA220 File Offset: 0x000B8420
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000BA230 File Offset: 0x000B8430
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000BA23F File Offset: 0x000B843F
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000BA24D File Offset: 0x000B844D
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000BA25B File Offset: 0x000B845B
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000BA269 File Offset: 0x000B8469
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000BA278 File Offset: 0x000B8478
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000BA288 File Offset: 0x000B8488
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000BA296 File Offset: 0x000B8496
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000BA2A4 File Offset: 0x000B84A4
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000BA2B2 File Offset: 0x000B84B2
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000BA2C4 File Offset: 0x000B84C4
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000BA2D3 File Offset: 0x000B84D3
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000BA2E2 File Offset: 0x000B84E2
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000BA2F4 File Offset: 0x000B84F4
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000BA319 File Offset: 0x000B8519
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000BA326 File Offset: 0x000B8526
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x000BA350 File Offset: 0x000B8550
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2572);
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

		// Token: 0x060030CA RID: 12490 RVA: 0x000BA3B3 File Offset: 0x000B85B3
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x04001481 RID: 5249
		internal static EraInfo[] thaiBuddhistEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -543, 544, 10542)
		};

		// Token: 0x04001482 RID: 5250
		public const int ThaiBuddhistEra = 1;

		// Token: 0x04001483 RID: 5251
		internal GregorianCalendarHelper helper;

		// Token: 0x04001484 RID: 5252
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 2572;
	}
}
