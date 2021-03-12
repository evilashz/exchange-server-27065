using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200005E RID: 94
	public struct ExDateTime : IComparable, IFormattable, IComparable<ExDateTime>, IEquatable<ExDateTime>
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public ExDateTime(ExTimeZone timeZone, long ticks)
		{
			this = new ExDateTime(timeZone, new DateTime(ticks));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public ExDateTime(ExTimeZone timeZone, int year, int month, int day)
		{
			this = new ExDateTime(timeZone, year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CE10 File Offset: 0x0000B010
		public ExDateTime(ExTimeZone timeZone, int year, int month, int day, int hour, int minute, int second)
		{
			this = new ExDateTime(timeZone, year, month, day, hour, minute, second, 0);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000CE30 File Offset: 0x0000B030
		public ExDateTime(ExTimeZone timeZone, int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			this = new ExDateTime(timeZone, new DateTime(year, month, day, hour, minute, second, millisecond, (timeZone == ExTimeZone.UtcTimeZone) ? DateTimeKind.Utc : DateTimeKind.Unspecified));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CE64 File Offset: 0x0000B064
		public ExDateTime(ExTimeZone desiredTimeZone, DateTime dateTime)
		{
			if (desiredTimeZone == null)
			{
				throw new ArgumentNullException("desiredTimeZone");
			}
			this.timeZone = desiredTimeZone;
			this.universalTime = TimeLibConsts.MinSystemDateTimeValue;
			this.localTime = null;
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (this.timeZone == ExTimeZone.UnspecifiedTimeZone)
				{
					this.timeZone = ExTimeZone.UtcTimeZone;
				}
				else
				{
					ExTimeZoneHelperForMigrationOnly.CheckValidationLevel<ExTimeZone>(this.timeZone.Id == ExTimeZone.UtcTimeZone.Id, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "ExDateTime. Invalid time zone for UTC date/time: {0}", this.timeZone);
				}
				this.localTime = null;
				this.UniversalTime = dateTime;
				return;
			}
			if (dateTime == DateTime.MinValue)
			{
				this.UniversalTime = TimeLibConsts.MinSystemDateTimeValue;
				return;
			}
			if (dateTime == DateTime.MaxValue)
			{
				this.UniversalTime = TimeLibConsts.MaxSystemDateTimeValue;
				return;
			}
			if (this.timeZone == ExTimeZone.UtcTimeZone)
			{
				this.UniversalTime = dateTime;
				this.LocalTime = this.UniversalTime;
				return;
			}
			TimeSpan value;
			if (ExDateTime.FindLeastBiasForLocalTime(this.timeZone, dateTime, out value))
			{
				this.LocalTime = dateTime;
				this.UniversalTime = dateTime.Subtract(value);
				return;
			}
			TimeSpan timeSpan = TimeSpan.MaxValue;
			int num = 0;
			while (num < 25 && timeSpan == TimeSpan.MaxValue)
			{
				dateTime = dateTime.AddHours(1.0);
				using (IEnumerator<TimeSpan> enumerator = this.timeZone.GetBiasesForLocalTime(dateTime).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						TimeSpan timeSpan2 = enumerator.Current;
						timeSpan = timeSpan2;
					}
				}
				num++;
			}
			if (timeSpan == TimeSpan.MaxValue)
			{
				throw new InvalidOperationException(string.Format("ExDateTime constructor failed to find a rule.\nTimeZone={0}\nDateTime={1}", this.timeZone, dateTime));
			}
			this.LocalTime = dateTime;
			this.UniversalTime = dateTime.Subtract(timeSpan);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000D034 File Offset: 0x0000B234
		internal ExDateTime(ExTimeZone timeZone, DateTime universalTime, DateTime? localTime)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			if (universalTime < TimeLibConsts.MinSystemDateTimeValue)
			{
				universalTime = TimeLibConsts.MinSystemDateTimeValue;
			}
			if (universalTime > TimeLibConsts.MaxSystemDateTimeValue)
			{
				universalTime = TimeLibConsts.MaxSystemDateTimeValue;
			}
			this.timeZone = timeZone;
			this.universalTime = universalTime;
			this.localTime = localTime;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000D08C File Offset: 0x0000B28C
		private ExDateTime(ExTimeZone timeZone, DateTime universalTime, TimeSpan bias)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			if (universalTime < TimeLibConsts.MinSystemDateTimeValue || universalTime > TimeLibConsts.MaxSystemDateTimeValue)
			{
				throw new ArgumentOutOfRangeException("universalTime");
			}
			this.localTime = null;
			this.universalTime = DateTime.MinValue;
			this.timeZone = timeZone;
			DateTime dateTime = universalTime + bias;
			TimeSpan biasForUtcTime = timeZone.GetBiasForUtcTime(universalTime);
			if (biasForUtcTime != bias)
			{
				if (ExDateTime.FindLeastBiasForLocalTime(timeZone, dateTime, out biasForUtcTime))
				{
					universalTime = dateTime - biasForUtcTime;
				}
				else
				{
					dateTime = universalTime + timeZone.GetBiasForUtcTime(universalTime);
				}
			}
			this.LocalTime = dateTime;
			this.UniversalTime = universalTime;
			this.timeZone = timeZone;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000D13C File Offset: 0x0000B33C
		public static ExDateTime UtcNow
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				return new ExDateTime(ExTimeZone.UtcTimeZone, utcNow, new DateTime?(utcNow));
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000D160 File Offset: 0x0000B360
		public static ExDateTime Now
		{
			get
			{
				if (ExTimeZone.CurrentTimeZone == null)
				{
					string message = string.Format("Current time zone is null, please check server registry at HKLM:{0}\\{1}", "SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation", "TimeZoneKeyName");
					ExTraceGlobals.CommonTracer.TraceError(0L, message);
					throw new InvalidTimeZoneException(message);
				}
				return ExTimeZone.CurrentTimeZone.ConvertDateTime(ExDateTime.UtcNow);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		public static ExDateTime Today
		{
			get
			{
				return ExDateTime.Now.Date;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000D1C6 File Offset: 0x0000B3C6
		public ExTimeZone TimeZone
		{
			get
			{
				if (this.timeZone == null)
				{
					this.Initialize();
				}
				return this.timeZone;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public bool HasTimeZone
		{
			get
			{
				return this.TimeZone != ExTimeZone.UnspecifiedTimeZone;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		public TimeSpan Bias
		{
			get
			{
				return this.LocalTime - this.UniversalTime;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000D204 File Offset: 0x0000B404
		public long UtcTicks
		{
			get
			{
				return this.UniversalTime.Ticks;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000D220 File Offset: 0x0000B420
		public ExDateTime Date
		{
			get
			{
				return new ExDateTime(this.TimeZone, this.LocalTime.Date);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000D248 File Offset: 0x0000B448
		public int Millisecond
		{
			get
			{
				return this.LocalTime.Millisecond;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000D264 File Offset: 0x0000B464
		public int Second
		{
			get
			{
				return this.LocalTime.Second;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000D280 File Offset: 0x0000B480
		public int Minute
		{
			get
			{
				return this.LocalTime.Minute;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000D29C File Offset: 0x0000B49C
		public int Hour
		{
			get
			{
				return this.LocalTime.Hour;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		public TimeSpan TimeOfDay
		{
			get
			{
				return this.LocalTime.TimeOfDay;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.LocalTime.DayOfWeek;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public int Day
		{
			get
			{
				return this.LocalTime.Day;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000D30C File Offset: 0x0000B50C
		public int DayOfYear
		{
			get
			{
				return this.LocalTime.DayOfYear;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000D328 File Offset: 0x0000B528
		public int Month
		{
			get
			{
				return this.LocalTime.Month;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000D344 File Offset: 0x0000B544
		public int Year
		{
			get
			{
				return this.LocalTime.Year;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000D35F File Offset: 0x0000B55F
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000D378 File Offset: 0x0000B578
		public DateTime UniversalTime
		{
			get
			{
				if (this.timeZone == null)
				{
					this.Initialize();
				}
				return this.universalTime;
			}
			private set
			{
				if (value == DateTime.MinValue)
				{
					value = TimeLibConsts.MinSystemDateTimeValue;
				}
				else if (value == DateTime.MaxValue)
				{
					value = TimeLibConsts.MaxSystemDateTimeValue;
				}
				else if (value < TimeLibConsts.MinSystemDateTimeValue)
				{
					ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.High, "ExDateTime.set_UniversalTime: DateTime less than TimeLibConsts.MinSystemDateTimeValue", new object[0]);
					value = TimeLibConsts.MinSystemDateTimeValue;
				}
				else if (value > TimeLibConsts.MaxSystemDateTimeValue)
				{
					ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.High, "ExDateTime.set_UniversalTime: DateTime greater than TimeLibConsts.MaxSystemDateTimeValue", new object[0]);
					value = TimeLibConsts.MaxSystemDateTimeValue;
				}
				this.universalTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000D40C File Offset: 0x0000B60C
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000D46E File Offset: 0x0000B66E
		internal DateTime LocalTime
		{
			get
			{
				if (this.localTime == null)
				{
					if (this.timeZone == null)
					{
						this.Initialize();
					}
					else
					{
						this.localTime = new DateTime?(DateTime.SpecifyKind(this.UniversalTime + this.timeZone.GetBiasForUtcTime(this.UniversalTime), DateTimeKind.Unspecified));
					}
				}
				return this.localTime.Value;
			}
			private set
			{
				this.localTime = new DateTime?(DateTime.SpecifyKind(value, DateTimeKind.Unspecified));
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000D484 File Offset: 0x0000B684
		private DateTime SystemDateTime
		{
			get
			{
				DateTime value = this.LocalTime;
				if (this.UniversalTime == TimeLibConsts.MaxSystemDateTimeValue)
				{
					value = DateTime.MaxValue;
				}
				else if (this.UniversalTime == TimeLibConsts.MinSystemDateTimeValue)
				{
					value = DateTime.MinValue;
				}
				DateTimeKind kind;
				if (this.TimeZone == ExTimeZone.UtcTimeZone)
				{
					kind = DateTimeKind.Utc;
				}
				else if (this.TimeZone == ExTimeZone.UnspecifiedTimeZone)
				{
					kind = DateTimeKind.Unspecified;
				}
				else
				{
					kind = DateTimeKind.Unspecified;
				}
				return DateTime.SpecifyKind(value, kind);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D4F5 File Offset: 0x0000B6F5
		public static explicit operator DateTime(ExDateTime exDateTime)
		{
			return exDateTime.SystemDateTime;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000D500 File Offset: 0x0000B700
		public static explicit operator DateTime?(ExDateTime? exDateTime)
		{
			if (exDateTime == null)
			{
				return null;
			}
			return new DateTime?((DateTime)exDateTime.Value);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D534 File Offset: 0x0000B734
		public static DateTime[] ToDateTimeArray(ExDateTime[] exDateTime)
		{
			if (exDateTime == null)
			{
				throw new ArgumentNullException("exDateTime");
			}
			DateTime[] array = new DateTime[exDateTime.Length];
			for (int i = 0; i < exDateTime.Length; i++)
			{
				array[i] = (DateTime)exDateTime[i];
			}
			return array;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D584 File Offset: 0x0000B784
		public static explicit operator ExDateTime(DateTime dateTime)
		{
			return new ExDateTime(ExTimeZone.TimeZoneFromKind(dateTime.Kind), dateTime);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D598 File Offset: 0x0000B798
		public static explicit operator ExDateTime?(DateTime? dateTime)
		{
			if (dateTime == null)
			{
				return null;
			}
			return new ExDateTime?((ExDateTime)dateTime.Value);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D5C9 File Offset: 0x0000B7C9
		public static bool IsValidDateTime(DateTime dateTime)
		{
			return dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue || (!(dateTime < TimeLibConsts.MinSystemDateTimeValue) && !(dateTime > TimeLibConsts.MaxSystemDateTimeValue));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D604 File Offset: 0x0000B804
		public static ExDateTime Parse(string s)
		{
			return (ExDateTime)DateTime.Parse(s);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D611 File Offset: 0x0000B811
		public static ExDateTime Parse(string s, IFormatProvider provider)
		{
			return (ExDateTime)DateTime.Parse(s, provider);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D61F File Offset: 0x0000B81F
		public static ExDateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			return (ExDateTime)DateTime.Parse(s, provider, styles);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D62E File Offset: 0x0000B82E
		public static ExDateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			return (ExDateTime)DateTime.ParseExact(s, format, provider);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D63D File Offset: 0x0000B83D
		public static ExDateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			return (ExDateTime)DateTime.ParseExact(s, format, provider, style);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D64D File Offset: 0x0000B84D
		public static ExDateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			return (ExDateTime)DateTime.ParseExact(s, formats, provider, style);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D65D File Offset: 0x0000B85D
		public static ExDateTime Parse(ExTimeZone exTimeZone, string s)
		{
			return new ExDateTime(exTimeZone, DateTime.Parse(s));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D66B File Offset: 0x0000B86B
		public static ExDateTime Parse(ExTimeZone exTimeZone, string s, IFormatProvider provider)
		{
			return new ExDateTime(exTimeZone, DateTime.Parse(s, provider));
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D67A File Offset: 0x0000B87A
		public static ExDateTime Parse(ExTimeZone exTimeZone, string s, IFormatProvider provider, DateTimeStyles styles)
		{
			return new ExDateTime(exTimeZone, DateTime.Parse(s, provider, styles));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D68A File Offset: 0x0000B88A
		public static ExDateTime ParseExact(ExTimeZone exTimeZone, string s, string format, IFormatProvider provider)
		{
			return new ExDateTime(exTimeZone, DateTime.ParseExact(s, format, provider));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D69A File Offset: 0x0000B89A
		public static ExDateTime ParseExact(ExTimeZone exTimeZone, string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			return new ExDateTime(exTimeZone, DateTime.ParseExact(s, format, provider, style));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		public static ExDateTime ParseExact(ExTimeZone exTimeZone, string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			return new ExDateTime(exTimeZone, DateTime.ParseExact(s, formats, provider, style));
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public static bool TryParse(string s, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(s, out dateTime);
			result = (flag ? ((ExDateTime)dateTime) : ExDateTime.MinValue);
			return flag;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(s, provider, styles, out dateTime);
			result = (flag ? ((ExDateTime)dateTime) : ExDateTime.MinValue);
			return flag;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D720 File Offset: 0x0000B920
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParseExact(s, format, provider, style, out dateTime);
			result = (flag ? ((ExDateTime)dateTime) : ExDateTime.MinValue);
			return flag;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D754 File Offset: 0x0000B954
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParseExact(s, formats, provider, style, out dateTime);
			result = (flag ? ((ExDateTime)dateTime) : ExDateTime.MinValue);
			return flag;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D788 File Offset: 0x0000B988
		public static bool TryParse(ExTimeZone exTimeZone, string s, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(s, out dateTime);
			result = (flag ? new ExDateTime(exTimeZone, dateTime) : exTimeZone.ConvertDateTime(ExDateTime.MinValue));
			return flag;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		public static bool TryParse(ExTimeZone exTimeZone, string s, IFormatProvider provider, DateTimeStyles styles, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(s, provider, styles, out dateTime);
			result = (flag ? new ExDateTime(exTimeZone, dateTime) : exTimeZone.ConvertDateTime(ExDateTime.MinValue));
			return flag;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public static bool TryParseExact(ExTimeZone exTimeZone, string s, string format, IFormatProvider provider, DateTimeStyles style, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParseExact(s, format, provider, style, out dateTime);
			result = (flag ? new ExDateTime(exTimeZone, dateTime) : exTimeZone.ConvertDateTime(ExDateTime.MinValue));
			return flag;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D830 File Offset: 0x0000BA30
		public static bool TryParseExact(ExTimeZone exTimeZone, string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParseExact(s, formats, provider, style, out dateTime);
			result = (flag ? new ExDateTime(exTimeZone, dateTime) : exTimeZone.ConvertDateTime(ExDateTime.MinValue));
			return flag;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D869 File Offset: 0x0000BA69
		public static ExDateTime FromBinary(long dateData)
		{
			return (ExDateTime)DateTime.FromBinary(dateData);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D876 File Offset: 0x0000BA76
		public static ExDateTime FromFileTimeUtc(long fileTime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, DateTime.FromFileTimeUtc(fileTime));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D888 File Offset: 0x0000BA88
		public static bool Equals(ExDateTime dt1, ExDateTime dt2)
		{
			return ExDateTime.Compare(dt1, dt2) == 0;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D894 File Offset: 0x0000BA94
		public static ExDateTime GetNow(ExTimeZone timeZone)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (timeZone != null)
			{
				return timeZone.ConvertDateTime(utcNow);
			}
			return utcNow;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		public static ExDateTime GetToday(ExTimeZone timeZone)
		{
			return ExDateTime.GetNow(timeZone).Date;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D8CF File Offset: 0x0000BACF
		public static int DaysInMonth(int year, int month)
		{
			return DateTime.DaysInMonth(year, month);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public static bool IsLeapYear(int year)
		{
			return DateTime.IsLeapYear(year);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		public static TimeSpan operator -(ExDateTime dt1, ExDateTime dt2)
		{
			return ExDateTime.TimeDiff(dt1, dt2);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D8E9 File Offset: 0x0000BAE9
		public static ExDateTime operator -(ExDateTime d, TimeSpan t)
		{
			return d.AddTicks(-t.Ticks);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D8FA File Offset: 0x0000BAFA
		public static bool operator !=(ExDateTime d1, ExDateTime d2)
		{
			return ExDateTime.Compare(d1, d2) != 0;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D909 File Offset: 0x0000BB09
		public static ExDateTime operator +(ExDateTime d, TimeSpan t)
		{
			return d.AddTicks(t.Ticks);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D919 File Offset: 0x0000BB19
		public static bool operator <(ExDateTime t1, ExDateTime t2)
		{
			return ExDateTime.Compare(t1, t2) < 0;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D925 File Offset: 0x0000BB25
		public static bool operator <=(ExDateTime t1, ExDateTime t2)
		{
			return ExDateTime.Compare(t1, t2) <= 0;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D934 File Offset: 0x0000BB34
		public static bool operator ==(ExDateTime d1, ExDateTime d2)
		{
			return ExDateTime.Compare(d1, d2) == 0;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D940 File Offset: 0x0000BB40
		public static bool operator >(ExDateTime t1, ExDateTime t2)
		{
			return ExDateTime.Compare(t1, t2) > 0;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public static bool operator >=(ExDateTime t1, ExDateTime t2)
		{
			return ExDateTime.Compare(t1, t2) >= 0;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D95B File Offset: 0x0000BB5B
		public static ExDateTime CreatedNormalizedExDateTime(ExTimeZone timeZone, DateTime universalTime, TimeSpan bias)
		{
			return new ExDateTime(timeZone, universalTime + bias);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public static IList<ExDateTime> Create(ExTimeZone timeZone, DateTime dateTime)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			List<ExDateTime> list = new List<ExDateTime>(2);
			foreach (TimeSpan value in timeZone.GetBiasesForLocalTime(dateTime))
			{
				list.Add(new ExDateTime(timeZone, dateTime.Subtract(value), new DateTime?(dateTime)));
			}
			return list;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		public static IList<ExDateTime> Create(ExTimeZone timeZone, int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			DateTime dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
			return ExDateTime.Create(timeZone, dateTime);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000DA0B File Offset: 0x0000BC0B
		public static int Compare(ExDateTime dt1, ExDateTime dt2)
		{
			return ExDateTime.Compare(dt1, dt2, TimeSpan.Zero);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		public static int Compare(ExDateTime dt1, ExDateTime dt2, TimeSpan threshold)
		{
			DateTime dateTime;
			DateTime dateTime2;
			if (dt1.TimeZone == ExTimeZone.UnspecifiedTimeZone || dt2.TimeZone == ExTimeZone.UnspecifiedTimeZone)
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Low, "ExDateTime.Compare: UnspecifiedTimeZone", new object[0]);
				dateTime = dt1.LocalTime;
				dateTime2 = dt2.LocalTime;
			}
			else
			{
				dateTime = dt1.UniversalTime;
				dateTime2 = dt2.UniversalTime;
			}
			int num = DateTime.Compare(dateTime, dateTime2);
			if (num != 0 && threshold != TimeSpan.Zero)
			{
				TimeSpan t = (num > 0) ? (dateTime - dateTime2) : (dateTime2 - dateTime);
				if (t <= threshold)
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public static ExDateTime ParseISO(string s)
		{
			return ExDateTime.Parse(s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		public static ExDateTime ParseISO(ExTimeZone exTimeZone, string s)
		{
			return ExDateTime.Parse(exTimeZone, s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public static bool TryParseISO(string s, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime);
			result = (flag ? ((ExDateTime)dateTime) : ExDateTime.MinValue);
			return flag;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public static bool TryParseISO(ExTimeZone exTimeZone, string s, out ExDateTime result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime);
			result = (flag ? new ExDateTime(exTimeZone, dateTime) : exTimeZone.ConvertDateTime(ExDateTime.MinValue));
			return flag;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000DB43 File Offset: 0x0000BD43
		public void CheckExpectedTimeZone(ExTimeZone timeZone)
		{
			this.CheckExpectedTimeZone(timeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public void CheckExpectedTimeZone(ExTimeZone timeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel level)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(this.TimeZone.Id == timeZone.Id, level, "CheckExpectedTimeZone. Expected: {0}. Actual: {1}", new object[]
			{
				timeZone,
				this.TimeZone
			});
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000DBA1 File Offset: 0x0000BDA1
		public ExDateTime ToUtc()
		{
			return ExTimeZone.UtcTimeZone.ConvertDateTime(this);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public ExDateTime AddTicks(long ticks)
		{
			long num = this.UniversalTime.Ticks + ticks;
			if (num < TimeLibConsts.MinSystemDateTimeValue.Ticks || num > TimeLibConsts.MaxSystemDateTimeValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("ticks");
			}
			return new ExDateTime(this.TimeZone, this.UniversalTime.AddTicks(ticks), null);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		public ExDateTime Add(TimeSpan value)
		{
			return this.AddTicks(value.Ticks);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000DC29 File Offset: 0x0000BE29
		public ExDateTime AddMilliseconds(double value)
		{
			return this.Add(TimeSpan.FromMilliseconds(value));
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000DC37 File Offset: 0x0000BE37
		public ExDateTime AddSeconds(double value)
		{
			return this.Add(TimeSpan.FromSeconds(value));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000DC45 File Offset: 0x0000BE45
		public ExDateTime AddMinutes(double value)
		{
			return this.Add(TimeSpan.FromMinutes(value));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000DC53 File Offset: 0x0000BE53
		public ExDateTime AddHours(double value)
		{
			return this.Add(TimeSpan.FromHours(value));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public ExDateTime IncrementDays(int value)
		{
			return new ExDateTime(this.TimeZone, this.LocalTime.AddDays((double)value));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public ExDateTime IncrementMonths(int value)
		{
			return new ExDateTime(this.TimeZone, this.LocalTime.AddMonths(value));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		public ExDateTime IncrementYears(int value)
		{
			return new ExDateTime(this.TimeZone, this.LocalTime.AddYears(value));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000DCDB File Offset: 0x0000BEDB
		public TimeSpan Subtract(ExDateTime value)
		{
			return this - value;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000DCE9 File Offset: 0x0000BEE9
		public ExDateTime Subtract(TimeSpan value)
		{
			return this - value;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000DCF7 File Offset: 0x0000BEF7
		public ExDateTime AddDays(double value)
		{
			return this.IncrementDays((int)value);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000DD01 File Offset: 0x0000BF01
		public ExDateTime AddMonths(int months)
		{
			return this.IncrementMonths(months);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000DD0A File Offset: 0x0000BF0A
		public ExDateTime AddYears(int value)
		{
			return this.IncrementYears(value);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public override string ToString()
		{
			return this.LocalTime.ToString();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public string ToString(IFormatProvider provider)
		{
			return this.LocalTime.ToString(provider);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public string ToString(string format)
		{
			return this.LocalTime.ToString(format);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public string ToString(string format, IFormatProvider provider)
		{
			return this.LocalTime.ToString(format, provider);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public string ToShortDateString()
		{
			return this.LocalTime.ToString("d");
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public string ToShortTimeString()
		{
			return this.LocalTime.ToString("t");
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		public string ToLongDateString()
		{
			return this.LocalTime.ToString("D");
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		public string ToLongTimeString()
		{
			return this.LocalTime.ToString("T");
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000DE10 File Offset: 0x0000C010
		public string ToISOString()
		{
			if (this.TimeZone == ExTimeZone.UtcTimeZone)
			{
				string format = (this.LocalTime.Millisecond == 0) ? "{0:yyyy-MM-ddTHH:mm:ss}Z" : "{0:yyyy-MM-ddTHH:mm:ss.fff}Z";
				return string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					this.LocalTime
				});
			}
			string format2 = (this.LocalTime.Millisecond == 0) ? "{0:yyyy-MM-ddTHH:mm:ss}{1}{2:00}:{3:00}" : "{0:yyyy-MM-ddTHH:mm:ss.fff}{1}{2:00}:{3:00}";
			TimeSpan bias = this.Bias;
			return string.Format(CultureInfo.InvariantCulture, format2, new object[]
			{
				this.LocalTime,
				(bias.Ticks < 0L) ? '-' : '+',
				Math.Abs(bias.Hours),
				Math.Abs(bias.Minutes)
			});
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public long ToBinary()
		{
			return this.LocalTime.ToBinary();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000DF18 File Offset: 0x0000C118
		public long ToFileTime()
		{
			return this.LocalTime.ToFileTime();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000DF34 File Offset: 0x0000C134
		public long ToFileTimeUtc()
		{
			return this.UniversalTime.ToFileTimeUtc();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000DF4F File Offset: 0x0000C14F
		public bool Equals(ExDateTime other)
		{
			return ExDateTime.Equals(this, other);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000DF5D File Offset: 0x0000C15D
		public override bool Equals(object other)
		{
			return other is ExDateTime && ExDateTime.Equals(this, (ExDateTime)other);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public override int GetHashCode()
		{
			return this.UtcTicks.GetHashCode() ^ this.Bias.GetHashCode();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		public int CompareTo(ExDateTime other, TimeSpan threshold)
		{
			return ExDateTime.Compare(this, other, threshold);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000DFBB File Offset: 0x0000C1BB
		public int CompareTo(ExDateTime other)
		{
			return ExDateTime.Compare(this, other);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000DFC9 File Offset: 0x0000C1C9
		public int CompareTo(object other)
		{
			if (other is ExDateTime)
			{
				return ExDateTime.Compare(this, (ExDateTime)other);
			}
			throw new ArgumentException("Invalid comparison of ExDateTime value to a different type");
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		internal static TimeSpan TimeDiff(ExDateTime t1, ExDateTime t2)
		{
			if (t1.TimeZone == ExTimeZone.UnspecifiedTimeZone || t2.TimeZone == ExTimeZone.UnspecifiedTimeZone)
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "ExDateTime.Compare: UnspecifiedTimeZone", new object[0]);
				return TimeSpan.FromTicks(t1.LocalTime.Ticks - t2.LocalTime.Ticks);
			}
			return TimeSpan.FromTicks(t1.UtcTicks - t2.UtcTicks);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000E064 File Offset: 0x0000C264
		private static bool FindLeastBiasForLocalTime(ExTimeZone timeZone, DateTime originalLocalTime, out TimeSpan bestBias)
		{
			return timeZone.TimeZoneInformation.FindLeastBiasForLocalTime(originalLocalTime, out bestBias);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000E074 File Offset: 0x0000C274
		private void Initialize()
		{
			this.timeZone = ExTimeZone.UtcTimeZone;
			this.universalTime = ExDateTime.MinValue.UniversalTime;
			this.localTime = new DateTime?(this.universalTime);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public ExDateTime(ExTimeZone timeZone, int year, int month, int day, Calendar calendar)
		{
			this = new ExDateTime(timeZone, new DateTime(year, month, day, calendar));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		public ExDateTime(ExTimeZone timeZone, int year, int month, int day, ExCalendar calendar)
		{
			this = new ExDateTime(timeZone, new DateTime(year, month, day, calendar.InnerCalendar));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public double ToOADate()
		{
			return this.UniversalTime.ToOADate();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000E0FB File Offset: 0x0000C2FB
		public static ExDateTime FromOADate(double d)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, DateTime.FromOADate(d));
		}

		// Token: 0x04000193 RID: 403
		public static readonly ExDateTime MaxValue = new ExDateTime(ExTimeZone.UtcTimeZone, TimeLibConsts.MaxSystemDateTimeValue, new DateTime?(TimeLibConsts.MaxSystemDateTimeValue));

		// Token: 0x04000194 RID: 404
		public static readonly ExDateTime MinValue = new ExDateTime(ExTimeZone.UtcTimeZone, TimeLibConsts.MinSystemDateTimeValue, new DateTime?(TimeLibConsts.MinSystemDateTimeValue));

		// Token: 0x04000195 RID: 405
		public static DateTime OutlookDateTimeMin = new DateTime(1601, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000196 RID: 406
		public static DateTime OutlookDateTimeMax = new DateTime(4501, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000197 RID: 407
		private ExTimeZone timeZone;

		// Token: 0x04000198 RID: 408
		private DateTime universalTime;

		// Token: 0x04000199 RID: 409
		private DateTime? localTime;
	}
}
