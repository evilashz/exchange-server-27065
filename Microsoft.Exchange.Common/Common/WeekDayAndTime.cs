using System;
using System.Globalization;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public struct WeekDayAndTime : IComparable<WeekDayAndTime>, IComparable, IEquatable<WeekDayAndTime>
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002A9E File Offset: 0x00000C9E
		public WeekDayAndTime(DayOfWeek dayOfWeek, int hour, int minute)
		{
			if (hour < 0 || hour >= 24)
			{
				throw new ArgumentOutOfRangeException("Hour");
			}
			if (minute < 0 || minute >= 60)
			{
				throw new ArgumentOutOfRangeException("Minute");
			}
			this.dayOfWeek = dayOfWeek;
			this.hour = hour;
			this.minute = minute;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002ADD File Offset: 0x00000CDD
		public WeekDayAndTime(DateTime dt)
		{
			this.dayOfWeek = dt.DayOfWeek;
			this.hour = dt.Hour;
			this.minute = dt.Minute;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B06 File Offset: 0x00000D06
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.dayOfWeek;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002B0E File Offset: 0x00000D0E
		public int Hour
		{
			get
			{
				return this.hour;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002B16 File Offset: 0x00000D16
		public int Minute
		{
			get
			{
				return this.minute;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B1E File Offset: 0x00000D1E
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.Hour, this.Minute, 0);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B32 File Offset: 0x00000D32
		public static bool operator ==(WeekDayAndTime t1, WeekDayAndTime t2)
		{
			return t1.CompareTo(t2) == 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B3F File Offset: 0x00000D3F
		public static bool operator !=(WeekDayAndTime t1, WeekDayAndTime t2)
		{
			return t1.CompareTo(t2) != 0;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B50 File Offset: 0x00000D50
		public static TimeSpan operator -(WeekDayAndTime to, WeekDayAndTime from)
		{
			if (from <= to)
			{
				return TimeSpan.FromDays((double)(to.dayOfWeek - from.dayOfWeek)) + (to.TimeOfDay - from.TimeOfDay);
			}
			return TimeSpan.FromDays(7.0) - (from - to);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002BAE File Offset: 0x00000DAE
		public static bool operator <(WeekDayAndTime t1, WeekDayAndTime t2)
		{
			return t1.CompareTo(t2) < 0;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BBB File Offset: 0x00000DBB
		public static bool operator >(WeekDayAndTime t1, WeekDayAndTime t2)
		{
			return t1.CompareTo(t2) > 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static bool operator <=(WeekDayAndTime t1, WeekDayAndTime t2)
		{
			return t1.CompareTo(t2) <= 0;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public static bool operator >=(WeekDayAndTime t1, WeekDayAndTime t2)
		{
			return t1.CompareTo(t2) >= 0;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public int CompareTo(WeekDayAndTime t)
		{
			if (this.dayOfWeek < t.dayOfWeek)
			{
				return -1;
			}
			if (this.dayOfWeek > t.dayOfWeek)
			{
				return 1;
			}
			if (this.hour < t.hour)
			{
				return -1;
			}
			if (this.hour > t.hour)
			{
				return 1;
			}
			return this.minute - t.minute;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C47 File Offset: 0x00000E47
		public int CompareTo(object t)
		{
			if (t is WeekDayAndTime)
			{
				return this.CompareTo((WeekDayAndTime)t);
			}
			throw new ArgumentException(CommonStrings.InvalidTypeToCompare);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C6D File Offset: 0x00000E6D
		public override int GetHashCode()
		{
			return (int)((int)this.dayOfWeek << 16 | (DayOfWeek)(this.hour << 8) | (DayOfWeek)this.minute);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C88 File Offset: 0x00000E88
		public bool Equals(WeekDayAndTime t)
		{
			return this.CompareTo(t) == 0;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C94 File Offset: 0x00000E94
		public override bool Equals(object t)
		{
			if (t is WeekDayAndTime)
			{
				return this.Equals((WeekDayAndTime)t);
			}
			throw new ArgumentException(CommonStrings.InvalidTypeToCompare);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WeekDayAndTime ToUniversalTime(DateTime referenceTime)
		{
			TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(referenceTime);
			DateTime dt = new DateTime(2006, 1, (int)(1 + this.dayOfWeek), this.hour, this.minute, 0, DateTimeKind.Local).Subtract(utcOffset);
			return new WeekDayAndTime(dt);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D08 File Offset: 0x00000F08
		public override string ToString()
		{
			DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
			if (!dateTimeFormat.Calendar.IsReadOnly)
			{
				dateTimeFormat.Calendar = ScheduleInterval.GetLocalCalendar();
			}
			return new DateTime(2006, 1, (int)(1 + this.dayOfWeek), this.hour, this.minute, 0).ToString("ddd." + dateTimeFormat.ShortTimePattern);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D70 File Offset: 0x00000F70
		public WeekDayAndTime AlignToMinutes(int min)
		{
			if (min < 0 || min >= 60)
			{
				throw new ArgumentOutOfRangeException("Minute");
			}
			return new WeekDayAndTime(this.dayOfWeek, this.hour, this.minute / min * min);
		}

		// Token: 0x04000064 RID: 100
		private readonly DayOfWeek dayOfWeek;

		// Token: 0x04000065 RID: 101
		private readonly int hour;

		// Token: 0x04000066 RID: 102
		private readonly int minute;
	}
}
