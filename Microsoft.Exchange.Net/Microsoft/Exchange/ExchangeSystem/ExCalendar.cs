using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200005C RID: 92
	[ComVisible(true)]
	[Serializable]
	public class ExCalendar : ICloneable
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C665 File Offset: 0x0000A865
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000C66D File Offset: 0x0000A86D
		public Calendar InnerCalendar { get; private set; }

		// Token: 0x06000298 RID: 664 RVA: 0x0000C676 File Offset: 0x0000A876
		public ExCalendar(Calendar calendar)
		{
			this.InnerCalendar = calendar;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000C685 File Offset: 0x0000A885
		public int[] Eras
		{
			get
			{
				return this.InnerCalendar.Eras;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C692 File Offset: 0x0000A892
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.InnerCalendar.IsReadOnly;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C69F File Offset: 0x0000A89F
		[ComVisible(false)]
		public ExDateTime MaxSupportedDateTime
		{
			get
			{
				return (ExDateTime)this.InnerCalendar.MaxSupportedDateTime;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C6B1 File Offset: 0x0000A8B1
		[ComVisible(false)]
		public ExDateTime MinSupportedDateTime
		{
			get
			{
				return (ExDateTime)this.InnerCalendar.MinSupportedDateTime;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000C6C3 File Offset: 0x0000A8C3
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public int TwoDigitYearMax
		{
			get
			{
				return this.InnerCalendar.TwoDigitYearMax;
			}
			set
			{
				this.InnerCalendar.TwoDigitYearMax = value;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C6DE File Offset: 0x0000A8DE
		public ExDateTime AddDays(ExDateTime time, int days)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddDays(time.LocalTime, days));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		public ExDateTime AddHours(ExDateTime time, int hours)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddHours(time.LocalTime, hours));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C720 File Offset: 0x0000A920
		public ExDateTime AddMilliseconds(ExDateTime time, double milliseconds)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddMilliseconds(time.LocalTime, milliseconds));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C741 File Offset: 0x0000A941
		public ExDateTime AddMinutes(ExDateTime time, int minutes)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddMinutes(time.LocalTime, minutes));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C762 File Offset: 0x0000A962
		public ExDateTime AddMonths(ExDateTime time, int months)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddMonths(time.LocalTime, months));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C783 File Offset: 0x0000A983
		public ExDateTime AddSeconds(ExDateTime time, int seconds)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddSeconds(time.LocalTime, seconds));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public ExDateTime AddWeeks(ExDateTime time, int weeks)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddWeeks(time.LocalTime, weeks));
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C7C5 File Offset: 0x0000A9C5
		public ExDateTime AddYears(ExDateTime time, int years)
		{
			return new ExDateTime(time.TimeZone, this.InnerCalendar.AddYears(time.LocalTime, years));
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C7E6 File Offset: 0x0000A9E6
		public int GetDayOfMonth(ExDateTime time)
		{
			return this.InnerCalendar.GetDayOfMonth(time.LocalTime);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C7FA File Offset: 0x0000A9FA
		public DayOfWeek GetDayOfWeek(ExDateTime time)
		{
			return this.InnerCalendar.GetDayOfWeek(time.LocalTime);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C80E File Offset: 0x0000AA0E
		public int GetDayOfYear(ExDateTime time)
		{
			return this.InnerCalendar.GetDayOfYear(time.LocalTime);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C822 File Offset: 0x0000AA22
		public int GetDaysInMonth(int year, int month)
		{
			return this.InnerCalendar.GetDaysInMonth(year, month);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C831 File Offset: 0x0000AA31
		public int GetDaysInMonth(int year, int month, int era)
		{
			return this.InnerCalendar.GetDaysInMonth(year, month, era);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C841 File Offset: 0x0000AA41
		public int GetDaysInYear(int year)
		{
			return this.InnerCalendar.GetDaysInYear(year);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000C84F File Offset: 0x0000AA4F
		public int GetDaysInYear(int year, int era)
		{
			return this.InnerCalendar.GetDaysInYear(year, era);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000C85E File Offset: 0x0000AA5E
		public int GetEra(ExDateTime time)
		{
			return this.InnerCalendar.GetEra(time.LocalTime);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000C872 File Offset: 0x0000AA72
		public int GetHour(ExDateTime time)
		{
			return this.InnerCalendar.GetHour(time.LocalTime);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000C886 File Offset: 0x0000AA86
		[ComVisible(false)]
		public int GetLeapMonth(int year)
		{
			return this.InnerCalendar.GetLeapMonth(year, this.InnerCalendar.GetEra(DateTime.UtcNow));
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		[ComVisible(false)]
		public int GetLeapMonth(int year, int era)
		{
			return this.InnerCalendar.GetLeapMonth(year, era);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C8B3 File Offset: 0x0000AAB3
		public double GetMilliseconds(ExDateTime time)
		{
			return this.InnerCalendar.GetMilliseconds(time.LocalTime);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C8C7 File Offset: 0x0000AAC7
		public int GetMinute(ExDateTime time)
		{
			return this.InnerCalendar.GetMinute(time.LocalTime);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C8DB File Offset: 0x0000AADB
		public int GetMonth(ExDateTime time)
		{
			return this.InnerCalendar.GetMonth(time.LocalTime);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C8EF File Offset: 0x0000AAEF
		public int GetMonthsInYear(int year)
		{
			return this.InnerCalendar.GetMonthsInYear(year);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		public int GetMonthsInYear(int year, int era)
		{
			return this.InnerCalendar.GetMonthsInYear(year, era);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C90C File Offset: 0x0000AB0C
		public int GetSecond(ExDateTime time)
		{
			return this.InnerCalendar.GetSecond(time.LocalTime);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000C920 File Offset: 0x0000AB20
		public int GetWeekOfYear(ExDateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.InnerCalendar.GetWeekOfYear(time.LocalTime, rule, firstDayOfWeek);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C936 File Offset: 0x0000AB36
		public int GetYear(ExDateTime time)
		{
			return this.InnerCalendar.GetYear(time.LocalTime);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public bool IsLeapDay(int year, int month, int day)
		{
			return this.InnerCalendar.IsLeapDay(year, month, day);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C95A File Offset: 0x0000AB5A
		public bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.InnerCalendar.IsLeapDay(year, month, day, era);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C96C File Offset: 0x0000AB6C
		public bool IsLeapMonth(int year, int month)
		{
			return this.InnerCalendar.IsLeapMonth(year, month);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C97B File Offset: 0x0000AB7B
		public bool IsLeapMonth(int year, int month, int era)
		{
			return this.InnerCalendar.IsLeapMonth(year, month, era);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C98B File Offset: 0x0000AB8B
		public bool IsLeapYear(int year)
		{
			return this.InnerCalendar.IsLeapYear(year);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C999 File Offset: 0x0000AB99
		public bool IsLeapYear(int year, int era)
		{
			return this.IsLeapYear(year, era);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C9A3 File Offset: 0x0000ABA3
		public ExDateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return (ExDateTime)this.InnerCalendar.ToDateTime(year, month, day, hour, minute, second, millisecond);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public ExDateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return (ExDateTime)this.InnerCalendar.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		public int ToFourDigitYear(int year)
		{
			return this.InnerCalendar.ToFourDigitYear(year);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		[ComVisible(false)]
		public CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return this.InnerCalendar.AlgorithmType;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000CA05 File Offset: 0x0000AC05
		[ComVisible(false)]
		public object Clone()
		{
			return new ExCalendar((Calendar)this.InnerCalendar.Clone());
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		[ComVisible(false)]
		public static ExCalendar ReadOnly(ExCalendar calendar)
		{
			return new ExCalendar(Calendar.ReadOnly(calendar.InnerCalendar));
		}

		// Token: 0x0400018F RID: 399
		public const int CurrentEra = 0;
	}
}
