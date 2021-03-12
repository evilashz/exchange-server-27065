using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PerformanceData
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x00027A3F File Offset: 0x00025C3F
		public PerformanceData(int latency, uint count)
		{
			this = new PerformanceData(TimeSpan.FromMilliseconds((double)latency), count);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00027A4F File Offset: 0x00025C4F
		public PerformanceData(TimeSpan latency, uint count)
		{
			this = new PerformanceData(latency, count, 0, 0, 0);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00027A5C File Offset: 0x00025C5C
		public PerformanceData(TimeSpan latency, uint count, int counter1, int counter2, int counter3)
		{
			this.latency = latency;
			this.count = count;
			this.threadId = Environment.CurrentManagedThreadId;
			this.counter1 = counter1;
			this.counter2 = counter2;
			this.counter3 = counter3;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00027A8E File Offset: 0x00025C8E
		public static PerformanceData Zero
		{
			get
			{
				return PerformanceData.zero;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00027A95 File Offset: 0x00025C95
		public static PerformanceData Unknown
		{
			get
			{
				return PerformanceData.unknown;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00027A9C File Offset: 0x00025C9C
		public int ThreadId
		{
			get
			{
				return this.threadId;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00027AA4 File Offset: 0x00025CA4
		public int Milliseconds
		{
			get
			{
				return (int)this.latency.TotalMilliseconds;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00027AB2 File Offset: 0x00025CB2
		public uint Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00027ABA File Offset: 0x00025CBA
		public TimeSpan Latency
		{
			get
			{
				return this.latency;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00027AC2 File Offset: 0x00025CC2
		public int Counter1
		{
			get
			{
				return this.counter1;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00027ACA File Offset: 0x00025CCA
		public int Counter2
		{
			get
			{
				return this.counter2;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00027AD2 File Offset: 0x00025CD2
		public int Counter3
		{
			get
			{
				return this.counter3;
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00027ADC File Offset: 0x00025CDC
		public static bool operator ==(PerformanceData pd1, PerformanceData pd2)
		{
			return pd1.count == pd2.count && pd1.latency == pd2.latency && (pd1.Counter1 == pd2.Counter1 && pd1.Counter2 == pd2.Counter2) && pd1.Counter3 == pd2.Counter3;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00027B42 File Offset: 0x00025D42
		public static bool Equals(PerformanceData pd1, PerformanceData pd2)
		{
			return pd1 == pd2;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00027B4B File Offset: 0x00025D4B
		public static bool operator !=(PerformanceData pd1, PerformanceData pd2)
		{
			return !(pd1 == pd2);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00027B57 File Offset: 0x00025D57
		public static int Compare(PerformanceData pd1, PerformanceData pd2)
		{
			if (!(pd1 == pd2))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00027B68 File Offset: 0x00025D68
		public static PerformanceData operator -(PerformanceData pd1, PerformanceData pd2)
		{
			return new PerformanceData(pd1.latency - pd2.latency, pd1.count - pd2.count, pd1.Counter1 - pd2.Counter1, pd1.Counter2 - pd2.Counter2, pd1.Counter3 - pd2.Counter3);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00027BC9 File Offset: 0x00025DC9
		public static PerformanceData Subtract(PerformanceData pd1, PerformanceData pd2)
		{
			return pd1 - pd2;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00027BD4 File Offset: 0x00025DD4
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj != null && obj is PerformanceData)
			{
				result = ((PerformanceData)obj == this);
			}
			return result;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00027C01 File Offset: 0x00025E01
		public override int GetHashCode()
		{
			return this.count.GetHashCode() ^ this.latency.GetHashCode();
		}

		// Token: 0x0400074F RID: 1871
		private static readonly PerformanceData zero = default(PerformanceData);

		// Token: 0x04000750 RID: 1872
		private static readonly PerformanceData unknown = new PerformanceData(TimeSpan.FromMilliseconds(-1.0), 0U);

		// Token: 0x04000751 RID: 1873
		private TimeSpan latency;

		// Token: 0x04000752 RID: 1874
		private uint count;

		// Token: 0x04000753 RID: 1875
		private int threadId;

		// Token: 0x04000754 RID: 1876
		private int counter1;

		// Token: 0x04000755 RID: 1877
		private int counter2;

		// Token: 0x04000756 RID: 1878
		private int counter3;
	}
}
