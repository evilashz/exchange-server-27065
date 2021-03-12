using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	// Token: 0x02000375 RID: 885
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000AA07B File Offset: 0x000A827B
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual DateTime MinSupportedDateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000AA082 File Offset: 0x000A8282
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual DateTime MaxSupportedDateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000AA089 File Offset: 0x000A8289
		[__DynamicallyInvokable]
		protected Calendar()
		{
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000AA09F File Offset: 0x000A829F
		internal virtual int ID
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000AA0A2 File Offset: 0x000A82A2
		internal virtual int BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000AA0AA File Offset: 0x000A82AA
		[ComVisible(false)]
		public virtual CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.Unknown;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000AA0AD File Offset: 0x000A82AD
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000AA0B8 File Offset: 0x000A82B8
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000AA0DC File Offset: 0x000A82DC
		[ComVisible(false)]
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000AA115 File Offset: 0x000A8315
		internal void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000AA12F File Offset: 0x000A832F
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000AA138 File Offset: 0x000A8338
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this.m_currentEraValue == -1)
				{
					this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
				}
				return this.m_currentEraValue;
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000AA15F File Offset: 0x000A835F
		internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
		{
			if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Argument_ResultCalendarRange"), minValue, maxValue));
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000AA19C File Offset: 0x000A839C
		internal DateTime Add(DateTime time, double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5);
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
			}
			long num2 = (long)num;
			long ticks = time.Ticks + num2 * 10000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000AA228 File Offset: 0x000A8428
		[__DynamicallyInvokable]
		public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
		{
			return this.Add(time, milliseconds, 1);
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000AA233 File Offset: 0x000A8433
		[__DynamicallyInvokable]
		public virtual DateTime AddDays(DateTime time, int days)
		{
			return this.Add(time, (double)days, 86400000);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000AA243 File Offset: 0x000A8443
		[__DynamicallyInvokable]
		public virtual DateTime AddHours(DateTime time, int hours)
		{
			return this.Add(time, (double)hours, 3600000);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000AA253 File Offset: 0x000A8453
		[__DynamicallyInvokable]
		public virtual DateTime AddMinutes(DateTime time, int minutes)
		{
			return this.Add(time, (double)minutes, 60000);
		}

		// Token: 0x06002C90 RID: 11408
		[__DynamicallyInvokable]
		public abstract DateTime AddMonths(DateTime time, int months);

		// Token: 0x06002C91 RID: 11409 RVA: 0x000AA263 File Offset: 0x000A8463
		[__DynamicallyInvokable]
		public virtual DateTime AddSeconds(DateTime time, int seconds)
		{
			return this.Add(time, (double)seconds, 1000);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000AA273 File Offset: 0x000A8473
		[__DynamicallyInvokable]
		public virtual DateTime AddWeeks(DateTime time, int weeks)
		{
			return this.AddDays(time, weeks * 7);
		}

		// Token: 0x06002C93 RID: 11411
		[__DynamicallyInvokable]
		public abstract DateTime AddYears(DateTime time, int years);

		// Token: 0x06002C94 RID: 11412
		[__DynamicallyInvokable]
		public abstract int GetDayOfMonth(DateTime time);

		// Token: 0x06002C95 RID: 11413
		[__DynamicallyInvokable]
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		// Token: 0x06002C96 RID: 11414
		[__DynamicallyInvokable]
		public abstract int GetDayOfYear(DateTime time);

		// Token: 0x06002C97 RID: 11415 RVA: 0x000AA27F File Offset: 0x000A847F
		[__DynamicallyInvokable]
		public virtual int GetDaysInMonth(int year, int month)
		{
			return this.GetDaysInMonth(year, month, 0);
		}

		// Token: 0x06002C98 RID: 11416
		[__DynamicallyInvokable]
		public abstract int GetDaysInMonth(int year, int month, int era);

		// Token: 0x06002C99 RID: 11417 RVA: 0x000AA28A File Offset: 0x000A848A
		[__DynamicallyInvokable]
		public virtual int GetDaysInYear(int year)
		{
			return this.GetDaysInYear(year, 0);
		}

		// Token: 0x06002C9A RID: 11418
		[__DynamicallyInvokable]
		public abstract int GetDaysInYear(int year, int era);

		// Token: 0x06002C9B RID: 11419
		[__DynamicallyInvokable]
		public abstract int GetEra(DateTime time);

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002C9C RID: 11420
		[__DynamicallyInvokable]
		public abstract int[] Eras { [__DynamicallyInvokable] get; }

		// Token: 0x06002C9D RID: 11421 RVA: 0x000AA294 File Offset: 0x000A8494
		[__DynamicallyInvokable]
		public virtual int GetHour(DateTime time)
		{
			return (int)(time.Ticks / 36000000000L % 24L);
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000AA2AC File Offset: 0x000A84AC
		[__DynamicallyInvokable]
		public virtual double GetMilliseconds(DateTime time)
		{
			return (double)(time.Ticks / 10000L % 1000L);
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000AA2C4 File Offset: 0x000A84C4
		[__DynamicallyInvokable]
		public virtual int GetMinute(DateTime time)
		{
			return (int)(time.Ticks / 600000000L % 60L);
		}

		// Token: 0x06002CA0 RID: 11424
		[__DynamicallyInvokable]
		public abstract int GetMonth(DateTime time);

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000AA2D9 File Offset: 0x000A84D9
		[__DynamicallyInvokable]
		public virtual int GetMonthsInYear(int year)
		{
			return this.GetMonthsInYear(year, 0);
		}

		// Token: 0x06002CA2 RID: 11426
		[__DynamicallyInvokable]
		public abstract int GetMonthsInYear(int year, int era);

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000AA2E3 File Offset: 0x000A84E3
		[__DynamicallyInvokable]
		public virtual int GetSecond(DateTime time)
		{
			return (int)(time.Ticks / 10000000L % 60L);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000AA2F8 File Offset: 0x000A84F8
		internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (num2 - firstDayOfWeek + 14) % 7;
			return (num + num3) / 7 + 1;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000AA32C File Offset: 0x000A852C
		private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek - num2 + 14) % 7;
			if (num3 != 0 && num3 >= fullDays)
			{
				num3 -= 7;
			}
			int num4 = num - num3;
			if (num4 >= 0)
			{
				return num4 / 7 + 1;
			}
			if (time <= this.MinSupportedDateTime.AddDays((double)num))
			{
				return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
			}
			return this.GetWeekOfYearFullDays(time.AddDays((double)(-(double)(num + 1))), firstDayOfWeek, fullDays);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000AA3A8 File Offset: 0x000A85A8
		private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
		{
			int num = this.GetDayOfYear(this.MinSupportedDateTime) - 1;
			int num2 = this.GetDayOfWeek(this.MinSupportedDateTime) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek + 7 - num2) % 7;
			if (num3 == 0 || num3 >= minimumDaysInFirstWeek)
			{
				return 1;
			}
			int num4 = this.DaysInYearBeforeMinSupportedYear - 1;
			int num5 = num2 - 1 - num4 % 7;
			int num6 = (firstDayOfWeek - num5 + 14) % 7;
			int num7 = num4 - num6;
			if (num6 >= minimumDaysInFirstWeek)
			{
				num7 += 7;
			}
			return num7 / 7 + 1;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000AA41A File Offset: 0x000A861A
		protected virtual int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 365;
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000AA424 File Offset: 0x000A8624
		[__DynamicallyInvokable]
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("firstDayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			switch (rule)
			{
			case CalendarWeekRule.FirstDay:
				return this.GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
			case CalendarWeekRule.FirstFullWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
			case CalendarWeekRule.FirstFourDayWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
			default:
				throw new ArgumentOutOfRangeException("rule", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					CalendarWeekRule.FirstDay,
					CalendarWeekRule.FirstFourDayWeek
				}));
			}
		}

		// Token: 0x06002CA9 RID: 11433
		[__DynamicallyInvokable]
		public abstract int GetYear(DateTime time);

		// Token: 0x06002CAA RID: 11434 RVA: 0x000AA4C3 File Offset: 0x000A86C3
		[__DynamicallyInvokable]
		public virtual bool IsLeapDay(int year, int month, int day)
		{
			return this.IsLeapDay(year, month, day, 0);
		}

		// Token: 0x06002CAB RID: 11435
		[__DynamicallyInvokable]
		public abstract bool IsLeapDay(int year, int month, int day, int era);

		// Token: 0x06002CAC RID: 11436 RVA: 0x000AA4CF File Offset: 0x000A86CF
		[__DynamicallyInvokable]
		public virtual bool IsLeapMonth(int year, int month)
		{
			return this.IsLeapMonth(year, month, 0);
		}

		// Token: 0x06002CAD RID: 11437
		[__DynamicallyInvokable]
		public abstract bool IsLeapMonth(int year, int month, int era);

		// Token: 0x06002CAE RID: 11438 RVA: 0x000AA4DA File Offset: 0x000A86DA
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year)
		{
			return this.GetLeapMonth(year, 0);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000AA4E4 File Offset: 0x000A86E4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int GetLeapMonth(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 0;
			}
			int monthsInYear = this.GetMonthsInYear(year, era);
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (this.IsLeapMonth(year, i, era))
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000AA520 File Offset: 0x000A8720
		[__DynamicallyInvokable]
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		// Token: 0x06002CB1 RID: 11441
		[__DynamicallyInvokable]
		public abstract bool IsLeapYear(int year, int era);

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000AA52C File Offset: 0x000A872C
		[__DynamicallyInvokable]
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		// Token: 0x06002CB3 RID: 11443
		[__DynamicallyInvokable]
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000AA54C File Offset: 0x000A874C
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool result2;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				result2 = true;
			}
			catch (ArgumentException)
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000AA59C File Offset: 0x000A879C
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000AA5C1 File Offset: 0x000A87C1
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000AA5E1 File Offset: 0x000A87E1
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000AA605 File Offset: 0x000A8805
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000AA60D File Offset: 0x000A880D
		[__DynamicallyInvokable]
		public virtual int TwoDigitYearMax
		{
			[__DynamicallyInvokable]
			get
			{
				return this.twoDigitYearMax;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000AA61C File Offset: 0x000A881C
		[__DynamicallyInvokable]
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000AA668 File Offset: 0x000A8868
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000AA6F0 File Offset: 0x000A88F0
		[SecuritySafeCritical]
		internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
		{
			int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				num = defaultYearValue;
			}
			return num;
		}

		// Token: 0x040011FC RID: 4604
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x040011FD RID: 4605
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x040011FE RID: 4606
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x040011FF RID: 4607
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x04001200 RID: 4608
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x04001201 RID: 4609
		internal const int MillisPerSecond = 1000;

		// Token: 0x04001202 RID: 4610
		internal const int MillisPerMinute = 60000;

		// Token: 0x04001203 RID: 4611
		internal const int MillisPerHour = 3600000;

		// Token: 0x04001204 RID: 4612
		internal const int MillisPerDay = 86400000;

		// Token: 0x04001205 RID: 4613
		internal const int DaysPerYear = 365;

		// Token: 0x04001206 RID: 4614
		internal const int DaysPer4Years = 1461;

		// Token: 0x04001207 RID: 4615
		internal const int DaysPer100Years = 36524;

		// Token: 0x04001208 RID: 4616
		internal const int DaysPer400Years = 146097;

		// Token: 0x04001209 RID: 4617
		internal const int DaysTo10000 = 3652059;

		// Token: 0x0400120A RID: 4618
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x0400120B RID: 4619
		internal const int CAL_GREGORIAN = 1;

		// Token: 0x0400120C RID: 4620
		internal const int CAL_GREGORIAN_US = 2;

		// Token: 0x0400120D RID: 4621
		internal const int CAL_JAPAN = 3;

		// Token: 0x0400120E RID: 4622
		internal const int CAL_TAIWAN = 4;

		// Token: 0x0400120F RID: 4623
		internal const int CAL_KOREA = 5;

		// Token: 0x04001210 RID: 4624
		internal const int CAL_HIJRI = 6;

		// Token: 0x04001211 RID: 4625
		internal const int CAL_THAI = 7;

		// Token: 0x04001212 RID: 4626
		internal const int CAL_HEBREW = 8;

		// Token: 0x04001213 RID: 4627
		internal const int CAL_GREGORIAN_ME_FRENCH = 9;

		// Token: 0x04001214 RID: 4628
		internal const int CAL_GREGORIAN_ARABIC = 10;

		// Token: 0x04001215 RID: 4629
		internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;

		// Token: 0x04001216 RID: 4630
		internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;

		// Token: 0x04001217 RID: 4631
		internal const int CAL_JULIAN = 13;

		// Token: 0x04001218 RID: 4632
		internal const int CAL_JAPANESELUNISOLAR = 14;

		// Token: 0x04001219 RID: 4633
		internal const int CAL_CHINESELUNISOLAR = 15;

		// Token: 0x0400121A RID: 4634
		internal const int CAL_SAKA = 16;

		// Token: 0x0400121B RID: 4635
		internal const int CAL_LUNAR_ETO_CHN = 17;

		// Token: 0x0400121C RID: 4636
		internal const int CAL_LUNAR_ETO_KOR = 18;

		// Token: 0x0400121D RID: 4637
		internal const int CAL_LUNAR_ETO_ROKUYOU = 19;

		// Token: 0x0400121E RID: 4638
		internal const int CAL_KOREANLUNISOLAR = 20;

		// Token: 0x0400121F RID: 4639
		internal const int CAL_TAIWANLUNISOLAR = 21;

		// Token: 0x04001220 RID: 4640
		internal const int CAL_PERSIAN = 22;

		// Token: 0x04001221 RID: 4641
		internal const int CAL_UMALQURA = 23;

		// Token: 0x04001222 RID: 4642
		internal int m_currentEraValue = -1;

		// Token: 0x04001223 RID: 4643
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001224 RID: 4644
		[__DynamicallyInvokable]
		public const int CurrentEra = 0;

		// Token: 0x04001225 RID: 4645
		internal int twoDigitYearMax = -1;
	}
}
