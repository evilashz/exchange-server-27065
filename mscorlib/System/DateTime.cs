﻿using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x0200007D RID: 125
	[__DynamicallyInvokable]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct DateTime : IComparable, IFormattable, IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00014C65 File Offset: 0x00012E65
		[__DynamicallyInvokable]
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
			}
			this.dateData = (ulong)ticks;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00014C94 File Offset: 0x00012E94
		private DateTime(ulong dateData)
		{
			this.dateData = dateData;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00014CA0 File Offset: 0x00012EA0
		[__DynamicallyInvokable]
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			this.dateData = (ulong)(ticks | (long)kind << 62);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014D00 File Offset: 0x00012F00
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
			}
			this.dateData = (ulong)(ticks | (isAmbiguousDst ? -4611686018427387904L : long.MinValue));
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014D52 File Offset: 0x00012F52
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day)
		{
			this.dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00014D62 File Offset: 0x00012F62
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00014D72 File Offset: 0x00012F72
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
			{
				second = 59;
			}
			this.dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00014DB0 File Offset: 0x00012FB0
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, kind))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this.dateData = (ulong)(num | (long)kind << 62);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00014E24 File Offset: 0x00013024
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			this.dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this.dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, DateTimeKind.Unspecified))
				{
					throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00014EC4 File Offset: 0x000130C4
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					999
				}));
			}
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)num;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00014F7C File Offset: 0x0001317C
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					999
				}));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, kind))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)(num | (long)kind << 62);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001505C File Offset: 0x0001325C
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					999
				}));
			}
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			long num2 = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num2 += (long)millisecond * 10000L;
			if (num2 < 0L || num2 > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)num2;
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this.dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, DateTimeKind.Unspecified))
				{
					throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001516C File Offset: 0x0001336C
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					999
				}));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			long num2 = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num2 += (long)millisecond * 10000L;
			if (num2 < 0L || num2 > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)(num2 | (long)kind << 62);
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this.dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, kind))
				{
					throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000152A4 File Offset: 0x000134A4
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			ulong num2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						num2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					num = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this.dateData = num2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_MissingDateTimeData"));
				}
				this.dateData = (ulong)num;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_DateTimeTicksOutOfRange"));
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00015380 File Offset: 0x00013580
		internal long InternalTicks
		{
			get
			{
				return (long)(this.dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00015392 File Offset: 0x00013592
		private ulong InternalKind
		{
			get
			{
				return this.dateData & 13835058055282163712UL;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000153A4 File Offset: 0x000135A4
		[__DynamicallyInvokable]
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000153B4 File Offset: 0x000135B4
		private DateTime Add(double value, int scale)
		{
			long num = (long)(value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5));
			if (num <= -315537897600000L || num >= 315537897600000L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
			}
			return this.AddTicks(num * 10000L);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00015423 File Offset: 0x00013623
		[__DynamicallyInvokable]
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00015431 File Offset: 0x00013631
		[__DynamicallyInvokable]
		public DateTime AddHours(double value)
		{
			return this.Add(value, 3600000);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001543F File Offset: 0x0001363F
		[__DynamicallyInvokable]
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00015449 File Offset: 0x00013649
		[__DynamicallyInvokable]
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00015458 File Offset: 0x00013658
		[__DynamicallyInvokable]
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadMonths"));
			}
			int num;
			int num2;
			int num3;
			this.GetDatePart(out num, out num2, out num3);
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
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)(DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L | (long)this.InternalKind));
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001551B File Offset: 0x0001371B
		[__DynamicallyInvokable]
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001552C File Offset: 0x0001372C
		[__DynamicallyInvokable]
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)(internalTicks + value | (long)this.InternalKind));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00015579 File Offset: 0x00013779
		[__DynamicallyInvokable]
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("years", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadYears"));
			}
			return this.AddMonths(value * 12);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000155AC File Offset: 0x000137AC
		[__DynamicallyInvokable]
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000155D8 File Offset: 0x000137D8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTime"));
			}
			long internalTicks = ((DateTime)value).InternalTicks;
			long internalTicks2 = this.InternalTicks;
			if (internalTicks2 > internalTicks)
			{
				return 1;
			}
			if (internalTicks2 < internalTicks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00015628 File Offset: 0x00013828
		[__DynamicallyInvokable]
		public int CompareTo(DateTime value)
		{
			long internalTicks = value.InternalTicks;
			long internalTicks2 = this.InternalTicks;
			if (internalTicks2 > internalTicks)
			{
				return 1;
			}
			if (internalTicks2 < internalTicks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00015654 File Offset: 0x00013854
		private static long DateToTicks(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					return (long)num2 * 864000000000L;
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000156DF File Offset: 0x000138DF
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return TimeSpan.TimeToTicks(hour, minute, second);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00015718 File Offset: 0x00013918
		[__DynamicallyInvokable]
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
			return array[month] - array[month - 1];
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00015764 File Offset: 0x00013964
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : -0.5));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateScale"));
			}
			return num * 10000L;
		}

		// Token: 0x06000604 RID: 1540
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LegacyParseMode();

		// Token: 0x06000605 RID: 1541
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EnableAmPmParseAdjustment();

		// Token: 0x06000606 RID: 1542 RVA: 0x00015810 File Offset: 0x00013A10
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001583D File Offset: 0x00013A3D
		[__DynamicallyInvokable]
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001584E File Offset: 0x00013A4E
		[__DynamicallyInvokable]
		public static bool Equals(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks == t2.InternalTicks;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00015860 File Offset: 0x00013A60
		[__DynamicallyInvokable]
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool isAmbiguousDst = false;
			long ticks;
			if (num < 0L)
			{
				ticks = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				ticks = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime time = new DateTime(num, DateTimeKind.Utc);
				bool flag = false;
				ticks = TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
			}
			num += ticks;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00015950 File Offset: 0x00013B50
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00015998 File Offset: 0x00013B98
		[__DynamicallyInvokable]
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000159B4 File Offset: 0x00013BB4
		[__DynamicallyInvokable]
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
			}
			if (DateTime.s_isLeapSecondsSupportedSystem)
			{
				return DateTime.InternalFromFileTime(fileTime);
			}
			long ticks = fileTime + 504911232000000000L;
			return new DateTime(ticks, DateTimeKind.Utc);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00015A08 File Offset: 0x00013C08
		[__DynamicallyInvokable]
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00015A16 File Offset: 0x00013C16
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this.dateData);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00015A48 File Offset: 0x00013C48
		[__DynamicallyInvokable]
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00015A66 File Offset: 0x00013C66
		[__DynamicallyInvokable]
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00015A78 File Offset: 0x00013C78
		[__DynamicallyInvokable]
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long ticks = this.Ticks;
				long num = ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this.dateData;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00015ACF File Offset: 0x00013CCF
		internal long ToBinaryRaw()
		{
			return (long)this.dateData;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00015AD8 File Offset: 0x00013CD8
		[__DynamicallyInvokable]
		public DateTime Date
		{
			[__DynamicallyInvokable]
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)(internalTicks - internalTicks % 864000000000L | (long)this.InternalKind));
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00015B08 File Offset: 0x00013D08
		private int GetDatePart(int part)
		{
			long internalTicks = this.InternalTicks;
			int i = (int)(internalTicks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00015BF8 File Offset: 0x00013DF8
		internal void GetDatePart(out int year, out int month, out int day)
		{
			long internalTicks = this.InternalTicks;
			int i = (int)(internalTicks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00015CDC File Offset: 0x00013EDC
		[__DynamicallyInvokable]
		public int Day
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(3);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00015CE5 File Offset: 0x00013EE5
		[__DynamicallyInvokable]
		public DayOfWeek DayOfWeek
		{
			[__DynamicallyInvokable]
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00015CFE File Offset: 0x00013EFE
		[__DynamicallyInvokable]
		public int DayOfYear
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(1);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00015D08 File Offset: 0x00013F08
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00015D24 File Offset: 0x00013F24
		[__DynamicallyInvokable]
		public int Hour
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015D3B File Offset: 0x00013F3B
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00015D50 File Offset: 0x00013F50
		[__DynamicallyInvokable]
		public DateTimeKind Kind
		{
			[__DynamicallyInvokable]
			get
			{
				ulong internalKind = this.InternalKind;
				if (internalKind == 0UL)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != 4611686018427387904UL)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00015D7A File Offset: 0x00013F7A
		[__DynamicallyInvokable]
		public int Millisecond
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00015D91 File Offset: 0x00013F91
		[__DynamicallyInvokable]
		public int Minute
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00015DA5 File Offset: 0x00013FA5
		[__DynamicallyInvokable]
		public int Month
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(2);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00015DB0 File Offset: 0x00013FB0
		[__DynamicallyInvokable]
		public static DateTime Now
		{
			[__DynamicallyInvokable]
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool isAmbiguousDst = false;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out isAmbiguousDst).Ticks;
				long num = utcNow.Ticks + ticks;
				if (num > 3155378975999999999L)
				{
					return new DateTime(3155378975999999999L, DateTimeKind.Local);
				}
				if (num < 0L)
				{
					return new DateTime(0L, DateTimeKind.Local);
				}
				return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00015E14 File Offset: 0x00014014
		[__DynamicallyInvokable]
		public static DateTime UtcNow
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (DateTime.s_isLeapSecondsSupportedSystem)
				{
					DateTime.FullSystemTime fullSystemTime = default(DateTime.FullSystemTime);
					DateTime.GetSystemTimeWithLeapSecondsHandling(ref fullSystemTime);
					return DateTime.CreateDateTimeFromSystemTime(ref fullSystemTime);
				}
				long systemTimeAsFileTime = DateTime.GetSystemTimeAsFileTime();
				return new DateTime((ulong)(systemTimeAsFileTime + 504911232000000000L | 4611686018427387904L));
			}
		}

		// Token: 0x06000622 RID: 1570
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSystemTimeAsFileTime();

		// Token: 0x06000623 RID: 1571
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ValidateSystemTime(ref DateTime.FullSystemTime time, bool localTime);

		// Token: 0x06000624 RID: 1572
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetSystemTimeWithLeapSecondsHandling(ref DateTime.FullSystemTime time);

		// Token: 0x06000625 RID: 1573
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsLeapSecondsSupportedSystem();

		// Token: 0x06000626 RID: 1574
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SystemFileTimeToSystemTime(long fileTime, ref DateTime.FullSystemTime time);

		// Token: 0x06000627 RID: 1575
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SystemTimeToSystemFileTime(ref DateTime.FullSystemTime time, ref long fileTime);

		// Token: 0x06000628 RID: 1576 RVA: 0x00015E62 File Offset: 0x00014062
		[SecuritySafeCritical]
		internal static bool SystemSupportLeapSeconds()
		{
			return DateTime.IsLeapSecondsSupportedSystem();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00015E6C File Offset: 0x0001406C
		[SecuritySafeCritical]
		internal static DateTime InternalFromFileTime(long fileTime)
		{
			DateTime.FullSystemTime fullSystemTime = default(DateTime.FullSystemTime);
			if (DateTime.SystemFileTimeToSystemTime(fileTime, ref fullSystemTime))
			{
				fullSystemTime.hundredNanoSecond = fileTime % 10000L;
				return DateTime.CreateDateTimeFromSystemTime(ref fullSystemTime);
			}
			throw new ArgumentOutOfRangeException("fileTime", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00015EB8 File Offset: 0x000140B8
		[SecuritySafeCritical]
		internal static long InternalToFileTime(long ticks)
		{
			long num = 0L;
			DateTime.FullSystemTime fullSystemTime = new DateTime.FullSystemTime(ticks);
			if (DateTime.SystemTimeToSystemFileTime(ref fullSystemTime, ref num))
			{
				return num + ticks % 10000L;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00015EF8 File Offset: 0x000140F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static DateTime CreateDateTimeFromSystemTime(ref DateTime.FullSystemTime time)
		{
			long num = DateTime.DateToTicks((int)time.wYear, (int)time.wMonth, (int)time.wDay);
			num += DateTime.TimeToTicks((int)time.wHour, (int)time.wMinute, (int)time.wSecond);
			num += (long)((ulong)time.wMillisecond * 10000UL);
			num += time.hundredNanoSecond;
			return new DateTime((ulong)(num | 4611686018427387904L));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00015F64 File Offset: 0x00014164
		[SecuritySafeCritical]
		internal static bool IsValidTimeWithLeapSeconds(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			DateTime dateTime = new DateTime(year, month, day);
			DateTime.FullSystemTime fullSystemTime = new DateTime.FullSystemTime(year, month, dateTime.DayOfWeek, day, hour, minute, second);
			if (kind == DateTimeKind.Utc)
			{
				return DateTime.ValidateSystemTime(ref fullSystemTime, false);
			}
			if (kind == DateTimeKind.Local)
			{
				return DateTime.ValidateSystemTime(ref fullSystemTime, true);
			}
			return DateTime.ValidateSystemTime(ref fullSystemTime, true) || DateTime.ValidateSystemTime(ref fullSystemTime, false);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00015FC1 File Offset: 0x000141C1
		[__DynamicallyInvokable]
		public int Second
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00015FD5 File Offset: 0x000141D5
		[__DynamicallyInvokable]
		public long Ticks
		{
			[__DynamicallyInvokable]
			get
			{
				return this.InternalTicks;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00015FDD File Offset: 0x000141DD
		[__DynamicallyInvokable]
		public TimeSpan TimeOfDay
		{
			[__DynamicallyInvokable]
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00015FF4 File Offset: 0x000141F4
		[__DynamicallyInvokable]
		public static DateTime Today
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.Now.Date;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001600E File Offset: 0x0001420E
		[__DynamicallyInvokable]
		public int Year
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(0);
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00016017 File Offset: 0x00014217
		[__DynamicallyInvokable]
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Year"));
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00016053 File Offset: 0x00014253
		[__DynamicallyInvokable]
		public static DateTime Parse(string s)
		{
			return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00016061 File Offset: 0x00014261
		[__DynamicallyInvokable]
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00016070 File Offset: 0x00014270
		[__DynamicallyInvokable]
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001608A File Offset: 0x0001428A
		[__DynamicallyInvokable]
		public static DateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001609A File Offset: 0x0001429A
		[__DynamicallyInvokable]
		public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000160B5 File Offset: 0x000142B5
		[__DynamicallyInvokable]
		public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000160D0 File Offset: 0x000142D0
		[__DynamicallyInvokable]
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000160E8 File Offset: 0x000142E8
		[__DynamicallyInvokable]
		public DateTime Subtract(TimeSpan value)
		{
			long internalTicks = this.InternalTicks;
			long ticks = value._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)(internalTicks - ticks | (long)this.InternalKind));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001613C File Offset: 0x0001433C
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000161C9 File Offset: 0x000143C9
		[__DynamicallyInvokable]
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000161D8 File Offset: 0x000143D8
		[__DynamicallyInvokable]
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000161F4 File Offset: 0x000143F4
		[__DynamicallyInvokable]
		public long ToFileTimeUtc()
		{
			long num = ((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks;
			if (DateTime.s_isLeapSecondsSupportedSystem)
			{
				return DateTime.InternalToFileTime(num);
			}
			num -= 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
			}
			return num;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001625B File Offset: 0x0001445B
		[__DynamicallyInvokable]
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00016264 File Offset: 0x00014464
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag = false;
			bool isAmbiguousDst = false;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00016304 File Offset: 0x00014504
		public string ToLongDateString()
		{
			return DateTimeFormat.Format(this, "D", DateTimeFormatInfo.CurrentInfo);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001631B File Offset: 0x0001451B
		public string ToLongTimeString()
		{
			return DateTimeFormat.Format(this, "T", DateTimeFormatInfo.CurrentInfo);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00016332 File Offset: 0x00014532
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", DateTimeFormatInfo.CurrentInfo);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016349 File Offset: 0x00014549
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", DateTimeFormatInfo.CurrentInfo);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00016360 File Offset: 0x00014560
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, DateTimeFormatInfo.CurrentInfo);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00016373 File Offset: 0x00014573
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this, format, DateTimeFormatInfo.CurrentInfo);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00016386 File Offset: 0x00014586
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001639A File Offset: 0x0001459A
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000163AE File Offset: 0x000145AE
		[__DynamicallyInvokable]
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000163BC File Offset: 0x000145BC
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out DateTime result)
		{
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000163CB File Offset: 0x000145CB
		[__DynamicallyInvokable]
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000163E6 File Offset: 0x000145E6
		[__DynamicallyInvokable]
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00016403 File Offset: 0x00014603
		[__DynamicallyInvokable]
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00016420 File Offset: 0x00014620
		[__DynamicallyInvokable]
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)(internalTicks + ticks | (long)d.InternalKind));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00016478 File Offset: 0x00014678
		[__DynamicallyInvokable]
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)(internalTicks - ticks | (long)d.InternalKind));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000164CB File Offset: 0x000146CB
		[__DynamicallyInvokable]
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000164E1 File Offset: 0x000146E1
		[__DynamicallyInvokable]
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000164F3 File Offset: 0x000146F3
		[__DynamicallyInvokable]
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00016508 File Offset: 0x00014708
		[__DynamicallyInvokable]
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001651A File Offset: 0x0001471A
		[__DynamicallyInvokable]
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001652F File Offset: 0x0001472F
		[__DynamicallyInvokable]
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00016541 File Offset: 0x00014741
		[__DynamicallyInvokable]
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00016556 File Offset: 0x00014756
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats()
		{
			return this.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00016563 File Offset: 0x00014763
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats(IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00016576 File Offset: 0x00014776
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats(char format)
		{
			return this.GetDateTimeFormats(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00016584 File Offset: 0x00014784
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats(char format, IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00016598 File Offset: 0x00014798
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001659C File Offset: 0x0001479C
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Boolean"
			}));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000165C3 File Offset: 0x000147C3
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Char"
			}));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000165EA File Offset: 0x000147EA
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"SByte"
			}));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00016611 File Offset: 0x00014811
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Byte"
			}));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00016638 File Offset: 0x00014838
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Int16"
			}));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001665F File Offset: 0x0001485F
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"UInt16"
			}));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00016686 File Offset: 0x00014886
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Int32"
			}));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000166AD File Offset: 0x000148AD
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"UInt32"
			}));
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000166D4 File Offset: 0x000148D4
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Int64"
			}));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000166FB File Offset: 0x000148FB
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"UInt64"
			}));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00016722 File Offset: 0x00014922
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Single"
			}));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00016749 File Offset: 0x00014949
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Double"
			}));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00016770 File Offset: 0x00014970
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"DateTime",
				"Decimal"
			}));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00016797 File Offset: 0x00014997
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001679F File Offset: 0x0001499F
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000167B4 File Offset: 0x000149B4
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second > 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			if (second == 60)
			{
				if (!DateTime.s_isLeapSecondsSupportedSystem || !DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
				{
					return false;
				}
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		// Token: 0x040002B8 RID: 696
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x040002B9 RID: 697
		private const long TicksPerSecond = 10000000L;

		// Token: 0x040002BA RID: 698
		private const long TicksPerMinute = 600000000L;

		// Token: 0x040002BB RID: 699
		private const long TicksPerHour = 36000000000L;

		// Token: 0x040002BC RID: 700
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040002BD RID: 701
		private const int MillisPerSecond = 1000;

		// Token: 0x040002BE RID: 702
		private const int MillisPerMinute = 60000;

		// Token: 0x040002BF RID: 703
		private const int MillisPerHour = 3600000;

		// Token: 0x040002C0 RID: 704
		private const int MillisPerDay = 86400000;

		// Token: 0x040002C1 RID: 705
		private const int DaysPerYear = 365;

		// Token: 0x040002C2 RID: 706
		private const int DaysPer4Years = 1461;

		// Token: 0x040002C3 RID: 707
		private const int DaysPer100Years = 36524;

		// Token: 0x040002C4 RID: 708
		private const int DaysPer400Years = 146097;

		// Token: 0x040002C5 RID: 709
		private const int DaysTo1601 = 584388;

		// Token: 0x040002C6 RID: 710
		private const int DaysTo1899 = 693593;

		// Token: 0x040002C7 RID: 711
		internal const int DaysTo1970 = 719162;

		// Token: 0x040002C8 RID: 712
		private const int DaysTo10000 = 3652059;

		// Token: 0x040002C9 RID: 713
		internal const long MinTicks = 0L;

		// Token: 0x040002CA RID: 714
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x040002CB RID: 715
		private const long MaxMillis = 315537897600000L;

		// Token: 0x040002CC RID: 716
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x040002CD RID: 717
		private const long DoubleDateOffset = 599264352000000000L;

		// Token: 0x040002CE RID: 718
		private const long OADateMinAsTicks = 31241376000000000L;

		// Token: 0x040002CF RID: 719
		private const double OADateMinAsDouble = -657435.0;

		// Token: 0x040002D0 RID: 720
		private const double OADateMaxAsDouble = 2958466.0;

		// Token: 0x040002D1 RID: 721
		private const int DatePartYear = 0;

		// Token: 0x040002D2 RID: 722
		private const int DatePartDayOfYear = 1;

		// Token: 0x040002D3 RID: 723
		private const int DatePartMonth = 2;

		// Token: 0x040002D4 RID: 724
		private const int DatePartDay = 3;

		// Token: 0x040002D5 RID: 725
		internal static readonly bool s_isLeapSecondsSupportedSystem = DateTime.SystemSupportLeapSeconds();

		// Token: 0x040002D6 RID: 726
		private static readonly int[] DaysToMonth365 = new int[]
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
			334,
			365
		};

		// Token: 0x040002D7 RID: 727
		private static readonly int[] DaysToMonth366 = new int[]
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
			335,
			366
		};

		// Token: 0x040002D8 RID: 728
		[__DynamicallyInvokable]
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		// Token: 0x040002D9 RID: 729
		[__DynamicallyInvokable]
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x040002DA RID: 730
		private const ulong TicksMask = 4611686018427387903UL;

		// Token: 0x040002DB RID: 731
		private const ulong FlagsMask = 13835058055282163712UL;

		// Token: 0x040002DC RID: 732
		private const ulong LocalMask = 9223372036854775808UL;

		// Token: 0x040002DD RID: 733
		private const long TicksCeiling = 4611686018427387904L;

		// Token: 0x040002DE RID: 734
		private const ulong KindUnspecified = 0UL;

		// Token: 0x040002DF RID: 735
		private const ulong KindUtc = 4611686018427387904UL;

		// Token: 0x040002E0 RID: 736
		private const ulong KindLocal = 9223372036854775808UL;

		// Token: 0x040002E1 RID: 737
		private const ulong KindLocalAmbiguousDst = 13835058055282163712UL;

		// Token: 0x040002E2 RID: 738
		private const int KindShift = 62;

		// Token: 0x040002E3 RID: 739
		private const string TicksField = "ticks";

		// Token: 0x040002E4 RID: 740
		private const string DateDataField = "dateData";

		// Token: 0x040002E5 RID: 741
		private ulong dateData;

		// Token: 0x02000A9C RID: 2716
		internal struct FullSystemTime
		{
			// Token: 0x060068B1 RID: 26801 RVA: 0x001680A4 File Offset: 0x001662A4
			internal FullSystemTime(int year, int month, DayOfWeek dayOfWeek, int day, int hour, int minute, int second)
			{
				this.wYear = (ushort)year;
				this.wMonth = (ushort)month;
				this.wDayOfWeek = (ushort)dayOfWeek;
				this.wDay = (ushort)day;
				this.wHour = (ushort)hour;
				this.wMinute = (ushort)minute;
				this.wSecond = (ushort)second;
				this.wMillisecond = 0;
				this.hundredNanoSecond = 0L;
			}

			// Token: 0x060068B2 RID: 26802 RVA: 0x001680FC File Offset: 0x001662FC
			internal FullSystemTime(long ticks)
			{
				DateTime dateTime = new DateTime(ticks);
				int num;
				int num2;
				int num3;
				dateTime.GetDatePart(out num, out num2, out num3);
				this.wYear = (ushort)num;
				this.wMonth = (ushort)num2;
				this.wDayOfWeek = (ushort)dateTime.DayOfWeek;
				this.wDay = (ushort)num3;
				this.wHour = (ushort)dateTime.Hour;
				this.wMinute = (ushort)dateTime.Minute;
				this.wSecond = (ushort)dateTime.Second;
				this.wMillisecond = (ushort)dateTime.Millisecond;
				this.hundredNanoSecond = 0L;
			}

			// Token: 0x0400300E RID: 12302
			internal ushort wYear;

			// Token: 0x0400300F RID: 12303
			internal ushort wMonth;

			// Token: 0x04003010 RID: 12304
			internal ushort wDayOfWeek;

			// Token: 0x04003011 RID: 12305
			internal ushort wDay;

			// Token: 0x04003012 RID: 12306
			internal ushort wHour;

			// Token: 0x04003013 RID: 12307
			internal ushort wMinute;

			// Token: 0x04003014 RID: 12308
			internal ushort wSecond;

			// Token: 0x04003015 RID: 12309
			internal ushort wMillisecond;

			// Token: 0x04003016 RID: 12310
			internal long hundredNanoSecond;
		}
	}
}
