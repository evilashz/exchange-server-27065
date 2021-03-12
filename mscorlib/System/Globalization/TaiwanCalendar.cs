using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003A5 RID: 933
	[ComVisible(true)]
	[Serializable]
	public class TaiwanCalendar : Calendar
	{
		// Token: 0x06003059 RID: 12377 RVA: 0x000B951B File Offset: 0x000B771B
		internal static Calendar GetDefaultInstance()
		{
			if (TaiwanCalendar.s_defaultInstance == null)
			{
				TaiwanCalendar.s_defaultInstance = new TaiwanCalendar();
			}
			return TaiwanCalendar.s_defaultInstance;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000B9539 File Offset: 0x000B7739
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.calendarMinValue;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000B9540 File Offset: 0x000B7740
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000B9547 File Offset: 0x000B7747
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000B954C File Offset: 0x000B774C
		public TaiwanCalendar()
		{
			try
			{
				new CultureInfo("zh-TW");
			}
			catch (ArgumentException innerException)
			{
				throw new TypeInitializationException(base.GetType().FullName, innerException);
			}
			this.helper = new GregorianCalendarHelper(this, TaiwanCalendar.taiwanEraInfo);
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000B95A0 File Offset: 0x000B77A0
		internal override int ID
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000B95A3 File Offset: 0x000B77A3
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000B95B2 File Offset: 0x000B77B2
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000B95C1 File Offset: 0x000B77C1
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000B95D1 File Offset: 0x000B77D1
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000B95E0 File Offset: 0x000B77E0
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000B95EE File Offset: 0x000B77EE
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000B95FC File Offset: 0x000B77FC
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000B960A File Offset: 0x000B780A
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000B9619 File Offset: 0x000B7819
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000B9629 File Offset: 0x000B7829
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B9637 File Offset: 0x000B7837
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000B9645 File Offset: 0x000B7845
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000B9653 File Offset: 0x000B7853
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000B9665 File Offset: 0x000B7865
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000B9674 File Offset: 0x000B7874
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000B9683 File Offset: 0x000B7883
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000B9694 File Offset: 0x000B7894
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000B96B9 File Offset: 0x000B78B9
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000B96C6 File Offset: 0x000B78C6
		// (set) Token: 0x06003072 RID: 12402 RVA: 0x000B96EC File Offset: 0x000B78EC
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
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

		// Token: 0x06003073 RID: 12403 RVA: 0x000B9750 File Offset: 0x000B7950
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (year > this.helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, this.helper.MaxYear));
			}
			return year;
		}

		// Token: 0x04001465 RID: 5221
		internal static EraInfo[] taiwanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x04001466 RID: 5222
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x04001467 RID: 5223
		internal GregorianCalendarHelper helper;

		// Token: 0x04001468 RID: 5224
		internal static readonly DateTime calendarMinValue = new DateTime(1912, 1, 1);

		// Token: 0x04001469 RID: 5225
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
