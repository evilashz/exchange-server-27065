using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000060 RID: 96
	[ComVisible(true)]
	[Serializable]
	public struct EnhancedTimeSpan : IComparable, IComparable<EnhancedTimeSpan>, IEquatable<EnhancedTimeSpan>, IComparable<TimeSpan>, IEquatable<TimeSpan>
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000D150 File Offset: 0x0000B350
		private EnhancedTimeSpan(TimeSpan timeSpan)
		{
			this.timeSpan = timeSpan;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D159 File Offset: 0x0000B359
		public static implicit operator TimeSpan(EnhancedTimeSpan enhancedTimeSpan)
		{
			return enhancedTimeSpan.timeSpan;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D162 File Offset: 0x0000B362
		public static implicit operator EnhancedTimeSpan(TimeSpan timeSpan)
		{
			return new EnhancedTimeSpan(timeSpan);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D16A File Offset: 0x0000B36A
		public static EnhancedTimeSpan operator -(EnhancedTimeSpan t)
		{
			return t.Negate();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D173 File Offset: 0x0000B373
		public static EnhancedTimeSpan operator -(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D182 File Offset: 0x0000B382
		public static EnhancedTimeSpan operator -(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D18C File Offset: 0x0000B38C
		public static EnhancedTimeSpan operator -(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		public static bool operator !=(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return !t1.Equals(t2);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public static bool operator !=(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return !t1.Equals(t2);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		public static bool operator !=(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return !t1.Equals(t2);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public static EnhancedTimeSpan operator +(EnhancedTimeSpan t)
		{
			return t;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000D1CF File Offset: 0x0000B3CF
		public static EnhancedTimeSpan operator +(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000D1DE File Offset: 0x0000B3DE
		public static EnhancedTimeSpan operator +(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public static EnhancedTimeSpan operator +(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		public static bool operator <(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) < 0;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D209 File Offset: 0x0000B409
		public static bool operator <(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.CompareTo(t2) < 0;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D216 File Offset: 0x0000B416
		public static bool operator <(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) < 0;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D228 File Offset: 0x0000B428
		public static bool operator <=(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) <= 0;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000D238 File Offset: 0x0000B438
		public static bool operator <=(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.CompareTo(t2) <= 0;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000D248 File Offset: 0x0000B448
		public static bool operator <=(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) <= 0;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000D25D File Offset: 0x0000B45D
		public static bool operator ==(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Equals(t2);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000D267 File Offset: 0x0000B467
		public static bool operator ==(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.Equals(t2);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000D271 File Offset: 0x0000B471
		public static bool operator ==(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Equals(t2);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000D280 File Offset: 0x0000B480
		public static bool operator >(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) > 0;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000D28D File Offset: 0x0000B48D
		public static bool operator >(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.CompareTo(t2) > 0;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000D29A File Offset: 0x0000B49A
		public static bool operator >(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) > 0;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		public static bool operator >=(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) >= 0;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public static bool operator >=(EnhancedTimeSpan t1, TimeSpan t2)
		{
			return t1.CompareTo(t2) >= 0;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public static bool operator >=(TimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.CompareTo(t2) >= 0;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
		public int Days
		{
			get
			{
				return this.timeSpan.Days;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000D2EE File Offset: 0x0000B4EE
		public int Hours
		{
			get
			{
				return this.timeSpan.Hours;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000D2FB File Offset: 0x0000B4FB
		public int Milliseconds
		{
			get
			{
				return this.timeSpan.Milliseconds;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000D308 File Offset: 0x0000B508
		public int Minutes
		{
			get
			{
				return this.timeSpan.Minutes;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000D315 File Offset: 0x0000B515
		public int Seconds
		{
			get
			{
				return this.timeSpan.Seconds;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000D322 File Offset: 0x0000B522
		public long Ticks
		{
			get
			{
				return this.timeSpan.Ticks;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000D32F File Offset: 0x0000B52F
		public double TotalDays
		{
			get
			{
				return this.timeSpan.TotalDays;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000D33C File Offset: 0x0000B53C
		public double TotalHours
		{
			get
			{
				return this.timeSpan.TotalHours;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000D349 File Offset: 0x0000B549
		public double TotalMilliseconds
		{
			get
			{
				return this.timeSpan.TotalMilliseconds;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000D356 File Offset: 0x0000B556
		public double TotalMinutes
		{
			get
			{
				return this.timeSpan.TotalMinutes;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000D363 File Offset: 0x0000B563
		public double TotalSeconds
		{
			get
			{
				return this.timeSpan.TotalSeconds;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D370 File Offset: 0x0000B570
		public EnhancedTimeSpan Add(TimeSpan ts)
		{
			EnhancedTimeSpan result;
			try
			{
				result = this.timeSpan.Add(ts);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException(DataStrings.ExceptionDurationOverflow, innerException);
			}
			return result;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			return TimeSpan.Compare(t1, t2);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is TimeSpan)
			{
				return this.CompareTo((TimeSpan)value);
			}
			if (value is EnhancedTimeSpan)
			{
				return this.CompareTo((EnhancedTimeSpan)value);
			}
			throw new ArgumentException(DataStrings.ExceptionTypeNotEnhancedTimeSpanOrTimeSpan, "value");
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D410 File Offset: 0x0000B610
		public int CompareTo(EnhancedTimeSpan value)
		{
			return this.CompareTo(value.timeSpan);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D41F File Offset: 0x0000B61F
		public int CompareTo(TimeSpan value)
		{
			return this.timeSpan.CompareTo(value);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D42D File Offset: 0x0000B62D
		public EnhancedTimeSpan Duration()
		{
			return this.timeSpan.Duration();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D43F File Offset: 0x0000B63F
		public override bool Equals(object value)
		{
			if (value is EnhancedTimeSpan)
			{
				return this.Equals((EnhancedTimeSpan)value);
			}
			return value is TimeSpan && this.Equals((TimeSpan)value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D46C File Offset: 0x0000B66C
		public bool Equals(EnhancedTimeSpan obj)
		{
			return this.Equals(obj.timeSpan);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D47B File Offset: 0x0000B67B
		public bool Equals(TimeSpan obj)
		{
			return this.timeSpan.Equals(obj);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D489 File Offset: 0x0000B689
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return TimeSpan.Equals(t1, t2);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D492 File Offset: 0x0000B692
		public static EnhancedTimeSpan FromDays(double value)
		{
			return TimeSpan.FromDays(value);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000D49F File Offset: 0x0000B69F
		public static EnhancedTimeSpan FromHours(double value)
		{
			return TimeSpan.FromHours(value);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D4AC File Offset: 0x0000B6AC
		public static EnhancedTimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.FromMilliseconds(value);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D4B9 File Offset: 0x0000B6B9
		public static EnhancedTimeSpan FromMinutes(double value)
		{
			return TimeSpan.FromMinutes(value);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D4C6 File Offset: 0x0000B6C6
		public static EnhancedTimeSpan FromSeconds(double value)
		{
			return TimeSpan.FromSeconds(value);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D4D3 File Offset: 0x0000B6D3
		public static EnhancedTimeSpan FromTicks(long value)
		{
			return TimeSpan.FromTicks(value);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public override int GetHashCode()
		{
			return this.timeSpan.GetHashCode();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D4F3 File Offset: 0x0000B6F3
		public EnhancedTimeSpan Negate()
		{
			return this.timeSpan.Negate();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D508 File Offset: 0x0000B708
		public EnhancedTimeSpan Subtract(TimeSpan ts)
		{
			EnhancedTimeSpan result;
			try
			{
				result = this.timeSpan.Subtract(ts);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException(DataStrings.ExceptionDurationOverflow, innerException);
			}
			return result;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000D54C File Offset: 0x0000B74C
		public int Sign
		{
			get
			{
				if (0L == this.timeSpan.Ticks)
				{
					return 0;
				}
				if (0L >= this.timeSpan.Ticks)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D571 File Offset: 0x0000B771
		public static EnhancedTimeSpan operator *(EnhancedTimeSpan t, long n)
		{
			return t.Multiply(n);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D57B File Offset: 0x0000B77B
		public static EnhancedTimeSpan operator *(long n, EnhancedTimeSpan t)
		{
			return t.Multiply(n);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D585 File Offset: 0x0000B785
		public static long operator /(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Divide(t2);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D594 File Offset: 0x0000B794
		public static EnhancedTimeSpan operator %(EnhancedTimeSpan t1, EnhancedTimeSpan t2)
		{
			return t1.Mod(t2);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D5A3 File Offset: 0x0000B7A3
		public long Divide(TimeSpan ts)
		{
			return this.timeSpan.Ticks / ts.Ticks;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public EnhancedTimeSpan Mod(TimeSpan ts)
		{
			return EnhancedTimeSpan.FromTicks(this.timeSpan.Ticks % ts.Ticks);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public EnhancedTimeSpan Multiply(long n)
		{
			long value = 0L;
			try
			{
				value = this.timeSpan.Ticks * n;
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException(DataStrings.ExceptionDurationOverflow, innerException);
			}
			return EnhancedTimeSpan.FromTicks(value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D61C File Offset: 0x0000B81C
		public static EnhancedTimeSpan Parse(string s)
		{
			return TimeSpan.Parse(s);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D629 File Offset: 0x0000B829
		public override string ToString()
		{
			return this.timeSpan.ToString();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D63C File Offset: 0x0000B83C
		public static bool TryParse(string s, out EnhancedTimeSpan result)
		{
			TimeSpan timeSpan;
			bool flag = TimeSpan.TryParse(s, out timeSpan);
			result = (flag ? new EnhancedTimeSpan(timeSpan) : default(EnhancedTimeSpan));
			return flag;
		}

		// Token: 0x04000130 RID: 304
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04000131 RID: 305
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000132 RID: 306
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04000133 RID: 307
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000134 RID: 308
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04000135 RID: 309
		private TimeSpan timeSpan;

		// Token: 0x04000136 RID: 310
		public static readonly EnhancedTimeSpan MaxValue = TimeSpan.MaxValue;

		// Token: 0x04000137 RID: 311
		public static readonly EnhancedTimeSpan MinValue = TimeSpan.MinValue;

		// Token: 0x04000138 RID: 312
		public static readonly EnhancedTimeSpan Zero = TimeSpan.Zero;

		// Token: 0x04000139 RID: 313
		public static readonly EnhancedTimeSpan OneDay = TimeSpan.FromTicks(864000000000L);

		// Token: 0x0400013A RID: 314
		public static readonly EnhancedTimeSpan OneHour = TimeSpan.FromTicks(36000000000L);

		// Token: 0x0400013B RID: 315
		public static readonly EnhancedTimeSpan OneMillisecond = TimeSpan.FromTicks(10000L);

		// Token: 0x0400013C RID: 316
		public static readonly EnhancedTimeSpan OneMinute = TimeSpan.FromTicks(600000000L);

		// Token: 0x0400013D RID: 317
		public static readonly EnhancedTimeSpan OneSecond = TimeSpan.FromTicks(10000000L);

		// Token: 0x0400013E RID: 318
		public static readonly EnhancedTimeSpan OneTick = TimeSpan.FromTicks(1L);
	}
}
