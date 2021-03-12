using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public struct ScheduleInterval : IComparable, IComparable<ScheduleInterval>
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002DA1 File Offset: 0x00000FA1
		public ScheduleInterval(DayOfWeek startDay, int startHour, int startMinute, DayOfWeek endDay, int endHour, int endMinute)
		{
			this.startTime = new WeekDayAndTime(startDay, startHour, startMinute / 15 * 15);
			this.endTime = new WeekDayAndTime(endDay, endHour, endMinute / 15 * 15);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DCE File Offset: 0x00000FCE
		public ScheduleInterval(WeekDayAndTime startTime, WeekDayAndTime endTime)
		{
			this.startTime = startTime.AlignToMinutes(15);
			this.endTime = endTime.AlignToMinutes(15);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DF0 File Offset: 0x00000FF0
		internal ScheduleInterval(DateTime start, DateTime end)
		{
			this.startTime = new WeekDayAndTime(start.DayOfWeek, start.Hour, start.Minute);
			this.endTime = new WeekDayAndTime(end.DayOfWeek, end.Hour, end.Minute);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E3D File Offset: 0x0000103D
		public WeekDayAndTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E45 File Offset: 0x00001045
		public WeekDayAndTime EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E4D File Offset: 0x0000104D
		public DayOfWeek StartDay
		{
			get
			{
				return this.startTime.DayOfWeek;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E5A File Offset: 0x0000105A
		public int StartHour
		{
			get
			{
				return this.startTime.Hour;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E67 File Offset: 0x00001067
		public int StartMinute
		{
			get
			{
				return this.startTime.Minute;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002E74 File Offset: 0x00001074
		public DayOfWeek EndDay
		{
			get
			{
				return this.endTime.DayOfWeek;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E81 File Offset: 0x00001081
		public int EndHour
		{
			get
			{
				return this.endTime.Hour;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002E8E File Offset: 0x0000108E
		public int EndMinute
		{
			get
			{
				return this.endTime.Minute;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E9B File Offset: 0x0000109B
		public TimeSpan Length
		{
			get
			{
				return this.endTime - this.startTime;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static ScheduleInterval Parse(string s)
		{
			ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(19305, 0L, "Parse called with string {0}", s);
			if (s.Length < 6)
			{
				throw new FormatException(CommonStrings.InvalidScheduleIntervalFormat);
			}
			DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
			if (!dateTimeFormat.Calendar.IsReadOnly)
			{
				dateTimeFormat.Calendar = ScheduleInterval.internalCalendar;
			}
			string[] formats = new string[]
			{
				"t",
				"HH:mm",
				"H:mm",
				"HH:mm tt",
				"H:mm tt",
				CultureInfo.InvariantCulture.DateTimeFormat.ShortTimePattern
			};
			DayOfWeek dayOfWeek = ScheduleInterval.ParseDayOfWeek(ref s, dateTimeFormat);
			string s2 = s;
			string text = null;
			int num = s.IndexOf('-');
			if (num > 0)
			{
				s2 = s.Substring(0, num);
				text = s.Substring(num + 1);
			}
			CultureInfo cultureInfo = null;
			DateTime dateTime;
			if (!DateTime.TryParseExact(s2, formats, null, DateTimeStyles.NoCurrentDateDefault, out dateTime))
			{
				cultureInfo = ScheduleInterval.GetCultureInfo("en-US");
				dateTime = DateTime.ParseExact(s2, formats, cultureInfo, DateTimeStyles.NoCurrentDateDefault);
			}
			DayOfWeek endDay = dayOfWeek;
			DateTime dateTime2 = dateTime;
			if (text != null)
			{
				try
				{
					endDay = ScheduleInterval.ParseDayOfWeek(ref text, dateTimeFormat);
				}
				catch (FormatException)
				{
					ExTraceGlobals.ScheduleIntervalTracer.TraceDebug(27497, 0L, "Schedule has no end date.");
				}
				if (!DateTime.TryParseExact(text, formats, null, DateTimeStyles.NoCurrentDateDefault, out dateTime2))
				{
					cultureInfo = (cultureInfo ?? ScheduleInterval.GetCultureInfo("en-US"));
					dateTime2 = DateTime.ParseExact(text, formats, cultureInfo, DateTimeStyles.NoCurrentDateDefault);
				}
			}
			return new ScheduleInterval(dayOfWeek, dateTime.Hour, dateTime.Minute, endDay, dateTime2.Hour, dateTime2.Minute);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003048 File Offset: 0x00001248
		public static byte[] GetWeekBitmapFromIntervals(ScheduleInterval[] intervals)
		{
			DateTime weekBitmapReference = ScheduleInterval.WeekBitmapReference;
			byte[] array = new byte[84];
			foreach (ScheduleInterval scheduleInterval in intervals)
			{
				WeekDayAndTime t = scheduleInterval.StartTime.ToUniversalTime(weekBitmapReference);
				WeekDayAndTime t2 = scheduleInterval.EndTime.ToUniversalTime(weekBitmapReference);
				if (t <= t2)
				{
					ScheduleInterval.UpdateBitmap((int)t.DayOfWeek, t.Hour, t.Minute, (int)t2.DayOfWeek, t2.Hour, t2.Minute, array);
				}
				else
				{
					ScheduleInterval.UpdateBitmap((int)t.DayOfWeek, t.Hour, t.Minute, 6, 23, 60, array);
					ScheduleInterval.UpdateBitmap(0, 0, 0, (int)t2.DayOfWeek, t2.Hour, t2.Minute, array);
				}
			}
			return array;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003130 File Offset: 0x00001330
		public static ScheduleInterval[] GetIntervalsFromWeekBitmap(byte[] week)
		{
			DateTime weekBitmapReference = ScheduleInterval.WeekBitmapReference;
			TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(weekBitmapReference);
			int num = utcOffset.Hours * 4 + utcOffset.Minutes / 15;
			List<ScheduleInterval> list = new List<ScheduleInterval>();
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < 672; i++)
			{
				int num3 = (i - num + 672) % 672;
				int num4 = num3 / 8;
				int num5 = num3 % 8;
				if ((week[num4] & (byte)(1 << 7 - num5)) > 0 && !flag)
				{
					flag = true;
					num2 = i;
				}
				else if ((week[num4] & (byte)(1 << 7 - num5)) == 0 && flag)
				{
					flag = false;
					int num6 = i;
					ScheduleInterval item = new ScheduleInterval((DayOfWeek)(num2 / 96), (num2 - num2 / 96 * 96) / 4, (num2 - num2 / 4 * 4) * 15, (DayOfWeek)(num6 / 96), (num6 - num6 / 96 * 96) / 4, (num6 - num6 / 4 * 4) * 15);
					list.Add(item);
				}
			}
			if (flag)
			{
				DayOfWeek endDay = DayOfWeek.Sunday;
				int endHour = 0;
				int endMinute = 0;
				int num7 = (672 - num) % 672;
				if (((int)week[num7 / 8] & 1 << 7 - num7 % 8) > 0)
				{
					if (list.Count > 0)
					{
						ScheduleInterval scheduleInterval = list[0];
						list.RemoveAt(0);
						endDay = scheduleInterval.EndDay;
						endHour = scheduleInterval.EndHour;
						endMinute = scheduleInterval.EndMinute;
					}
					else
					{
						list.Add(new ScheduleInterval(DayOfWeek.Sunday, 0, 0, DayOfWeek.Saturday, 23, 45));
						num2 = 671;
					}
				}
				list.Add(new ScheduleInterval((DayOfWeek)(num2 / 96), (num2 - num2 / 96 * 96) / 4, (num2 - num2 / 4 * 4) * 15, endDay, endHour, endMinute));
			}
			return list.ToArray();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032EA File Offset: 0x000014EA
		public override string ToString()
		{
			return this.startTime.ToString() + "-" + this.endTime.ToString();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003318 File Offset: 0x00001518
		public override bool Equals(object obj)
		{
			if (!(obj is ScheduleInterval))
			{
				return false;
			}
			ScheduleInterval scheduleInterval = (ScheduleInterval)obj;
			return this.StartTime == scheduleInterval.StartTime && this.EndTime == scheduleInterval.EndTime;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000335E File Offset: 0x0000155E
		public bool ConjointWith(ScheduleInterval other)
		{
			return this.startTime == other.endTime || this.endTime == other.startTime;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003388 File Offset: 0x00001588
		public bool Overlaps(ScheduleInterval other)
		{
			return this.Contains(other.startTime) || other.Contains(this.startTime);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033A8 File Offset: 0x000015A8
		public bool Contains(WeekDayAndTime dt)
		{
			if (this.startTime <= this.endTime)
			{
				return this.startTime <= dt && dt < this.endTime;
			}
			return this.endTime > dt || dt >= this.startTime;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003401 File Offset: 0x00001601
		public bool Contains(DateTime dt)
		{
			return this.Contains(new WeekDayAndTime(dt));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000340F File Offset: 0x0000160F
		public bool Contains(DayOfWeek day, int hour, int minute)
		{
			return this.Contains(new WeekDayAndTime(day, hour, minute));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000341F File Offset: 0x0000161F
		public override int GetHashCode()
		{
			return this.startTime.GetHashCode() ^ this.endTime.GetHashCode();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003444 File Offset: 0x00001644
		public int CompareTo(object value)
		{
			if (value is ScheduleInterval)
			{
				return this.CompareTo((ScheduleInterval)value);
			}
			throw new ArgumentException(CommonStrings.InvalidTypeToCompare);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000346C File Offset: 0x0000166C
		public int CompareTo(ScheduleInterval interval)
		{
			int num = this.StartTime.CompareTo(interval.StartTime);
			if (num == 0)
			{
				return this.Length.CompareTo(interval.Length);
			}
			return num;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034AC File Offset: 0x000016AC
		private static DayOfWeek ParseDayOfWeek(ref string s, DateTimeFormatInfo dtfi)
		{
			ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(23401, 0L, "ParseDayOfWeek called with string {0}", s);
			if (s.Length < 2)
			{
				throw new FormatException(CommonStrings.InvalidScheduleIntervalFormat);
			}
			if (char.IsDigit(s[0]) && s[1] == '.')
			{
				string text = s.Substring(0, 1);
				s = s.Substring(2);
				ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(31593, 0L, "ParseDayOfWeek matched {0}", text);
				return (DayOfWeek)int.Parse(text);
			}
			string[] abbreviatedDayNames = dtfi.AbbreviatedDayNames;
			string[] dayNames = dtfi.DayNames;
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek < (DayOfWeek)7; dayOfWeek++)
			{
				string text2 = abbreviatedDayNames[(int)dayOfWeek];
				if (s.Length > text2.Length && s[text2.Length] == '.' && s.StartsWith(text2, StringComparison.OrdinalIgnoreCase))
				{
					s = s.Substring(text2.Length + 1);
					ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(17257, 0L, "ParseDayOfWeek matched {0}", text2);
					return dayOfWeek;
				}
				text2 = dayNames[(int)dayOfWeek];
				if (s.Length > text2.Length && s[text2.Length] == '.' && s.StartsWith(text2, StringComparison.OrdinalIgnoreCase))
				{
					s = s.Substring(text2.Length + 1);
					ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(25449, 0L, "ParseDayOfWeek matched {0}", text2);
					return dayOfWeek;
				}
			}
			DateTimeFormatInfo dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
			abbreviatedDayNames = dateTimeFormat.AbbreviatedDayNames;
			dayNames = dateTimeFormat.DayNames;
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek < (DayOfWeek)7; dayOfWeek++)
			{
				string text3 = abbreviatedDayNames[(int)dayOfWeek];
				if (s.Length > text3.Length && s[text3.Length] == '.' && s.StartsWith(text3, StringComparison.OrdinalIgnoreCase))
				{
					s = s.Substring(text3.Length + 1);
					ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(21353, 0L, "ParseDayOfWeek matched {0}", text3);
					return dayOfWeek;
				}
				text3 = dayNames[(int)dayOfWeek];
				if (s.Length > text3.Length && s[text3.Length] == '.' && s.StartsWith(text3, StringComparison.OrdinalIgnoreCase))
				{
					s = s.Substring(text3.Length + 1);
					ExTraceGlobals.ScheduleIntervalTracer.TraceDebug<string>(29545, 0L, "ParseDayOfWeek matched {0}", text3);
					return dayOfWeek;
				}
			}
			throw new FormatException(CommonStrings.InvalidScheduleIntervalFormat);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003714 File Offset: 0x00001914
		private static void UpdateBitmap(int startDay, int startHour, int startMinute, int endDay, int endHour, int endMinute, byte[] week)
		{
			for (int i = startDay * 12 + startHour / 2 + 1; i < endDay * 12 + endHour / 2; i++)
			{
				week[i] = byte.MaxValue;
			}
			int num = (startHour / 2 + 1) * 2 * 4 - startHour * 4 - startMinute / 15;
			byte b = (byte)((1 << num) - 1);
			num = (endHour / 2 + 1) * 2 * 4 - endHour * 4 - endMinute / 15;
			byte b2 = (byte)(255 - ((1 << num) - 1));
			if (startDay == endDay && startHour / 2 == endHour / 2)
			{
				int num2 = startDay * 12 + startHour / 2;
				week[num2] |= (b & b2);
				return;
			}
			int num3 = startDay * 12 + startHour / 2;
			week[num3] |= b;
			int num4 = endDay * 12 + endHour / 2;
			week[num4] |= b2;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000037F3 File Offset: 0x000019F3
		private static CultureInfo GetCultureInfo(string culture)
		{
			return new CultureInfo(culture, false);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037FC File Offset: 0x000019FC
		public static Calendar GetLocalCalendar()
		{
			return new GregorianCalendar(GregorianCalendarTypes.Localized);
		}

		// Token: 0x04000067 RID: 103
		private const int BitmapLength = 84;

		// Token: 0x04000068 RID: 104
		public static readonly DateTime WeekBitmapReference = new DateTime(2006, 1, 1, 0, 0, 0, DateTimeKind.Local);

		// Token: 0x04000069 RID: 105
		private static readonly Calendar internalCalendar = ScheduleInterval.GetLocalCalendar();

		// Token: 0x0400006A RID: 106
		private WeekDayAndTime startTime;

		// Token: 0x0400006B RID: 107
		private WeekDayAndTime endTime;
	}
}
