using System;
using System.Collections.Generic;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200068B RID: 1675
	internal class FixedTimeAverage
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x000385A9 File Offset: 0x000367A9
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x000385B0 File Offset: 0x000367B0
		internal static Action OnCorrectiveActionForTest { get; set; }

		// Token: 0x06001E62 RID: 7778 RVA: 0x000385B8 File Offset: 0x000367B8
		public FixedTimeAverage(ushort windowBucketLength, ushort numberOfBuckets, int currentTicks) : this(windowBucketLength, numberOfBuckets, currentTicks, FixedTimeAverage.DefaultCorrectiveInterval)
		{
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000385C8 File Offset: 0x000367C8
		public FixedTimeAverage(ushort windowBucketLength, ushort numberOfBuckets, int currentTicks, TimeSpan correctiveInterval)
		{
			if (windowBucketLength == 0)
			{
				throw new ArgumentOutOfRangeException("windowBucketLength", windowBucketLength, "WindowBucketLength must be greater than zero.");
			}
			if (numberOfBuckets == 0)
			{
				throw new ArgumentOutOfRangeException("numberOfBuckets", numberOfBuckets, "NumberOfBuckets must be greater than zero.");
			}
			if (correctiveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("correctiveInterval", correctiveInterval, "CorrectiveInterval must be positive.");
			}
			this.waitingBuckets = new Queue<FixedTimeAverage.WindowBucket>((int)numberOfBuckets);
			this.windowBucketLength = windowBucketLength;
			this.lastCall = currentTicks;
			this.lastCorrectiveFix = currentTicks;
			this.correctiveInterval = correctiveInterval;
			this.totalWindowLength = TimeSpan.FromMilliseconds((double)(windowBucketLength * numberOfBuckets));
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00038675 File Offset: 0x00036875
		public bool TryGetValue(out float value)
		{
			return this.TryGetValue(0, out value);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00038680 File Offset: 0x00036880
		public bool TryGetValue(int minTrustedBucketCount, out float value)
		{
			bool result;
			lock (this.instanceLock)
			{
				value = this.GetValue();
				result = (!this.IsEmpty && this.BucketCount >= minTrustedBucketCount);
			}
			return result;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000386DC File Offset: 0x000368DC
		public float GetValue()
		{
			return this.GetValue(Environment.TickCount);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000386E9 File Offset: 0x000368E9
		public void Add(uint value)
		{
			this.Add(Environment.TickCount, value);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000386F7 File Offset: 0x000368F7
		public void Clear()
		{
			this.Clear(Environment.TickCount);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x00038704 File Offset: 0x00036904
		public void Clear(int currentTicks)
		{
			lock (this.instanceLock)
			{
				this.waitingBuckets.Clear();
				this.currentBucket = null;
				this.value = 0f;
				this.lastCall = currentTicks;
				this.lastCorrectiveFix = currentTicks;
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0003876C File Offset: 0x0003696C
		public void Add(int currentTicks, uint value)
		{
			lock (this.instanceLock)
			{
				this.VerifyTime(ref currentTicks);
				this.InternalUpdate(currentTicks);
				this.GetCurrentBucket(currentTicks).Add(currentTicks, value);
			}
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x000387C8 File Offset: 0x000369C8
		public void Update(int currentTicks)
		{
			lock (this.instanceLock)
			{
				this.VerifyTime(ref currentTicks);
				this.InternalUpdate(currentTicks);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00038814 File Offset: 0x00036A14
		public bool IsEmpty
		{
			get
			{
				return this.BucketCount == 0 && this.currentBucket == null;
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0003882C File Offset: 0x00036A2C
		private void InternalUpdate(int currentTicks)
		{
			if (this.currentBucket != null && TickDiffer.Elapsed(this.currentBucket.ExpireTicks, currentTicks) >= TimeSpan.Zero)
			{
				int count = this.waitingBuckets.Count;
				this.waitingBuckets.Enqueue(this.currentBucket);
				int num = count + 1;
				float num2 = 1f / (float)num;
				float num3 = 1f - num2;
				this.value = num2 * this.currentBucket.Average + num3 * this.value;
				this.currentBucket = null;
			}
			while (this.waitingBuckets.Count > 0)
			{
				FixedTimeAverage.WindowBucket windowBucket = this.waitingBuckets.Peek();
				if (!(TickDiffer.Elapsed(windowBucket.ExpireTicks, currentTicks) >= this.totalWindowLength))
				{
					break;
				}
				int count2 = this.waitingBuckets.Count;
				this.waitingBuckets.Dequeue();
				int num4 = count2 - 1;
				if (num4 > 0)
				{
					float num5 = 1f / (float)count2;
					float num6 = 1f - num5;
					this.value = (this.value - num5 * windowBucket.Average) / num6;
				}
				else
				{
					this.value = 0f;
				}
			}
			if (TickDiffer.Elapsed(this.lastCorrectiveFix, currentTicks) > this.correctiveInterval)
			{
				this.lastCorrectiveFix = currentTicks;
				float num7 = 0f;
				if (this.waitingBuckets.Count == 0)
				{
					this.value = 0f;
					return;
				}
				int count3 = this.waitingBuckets.Count;
				foreach (FixedTimeAverage.WindowBucket windowBucket2 in this.waitingBuckets)
				{
					if (windowBucket2 != null)
					{
						num7 += windowBucket2.Average / (float)count3;
					}
				}
				if (FixedTimeAverage.OnCorrectiveActionForTest != null)
				{
					FixedTimeAverage.OnCorrectiveActionForTest();
				}
				this.value = num7;
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00038A24 File Offset: 0x00036C24
		internal float GetValue(int now)
		{
			this.Update(now);
			return this.value;
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00038A35 File Offset: 0x00036C35
		private int BucketCount
		{
			get
			{
				return this.waitingBuckets.Count;
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00038A44 File Offset: 0x00036C44
		private FixedTimeAverage.WindowBucket GetCurrentBucket(int currentTicks)
		{
			FixedTimeAverage.WindowBucket result;
			lock (this.instanceLock)
			{
				this.VerifyTime(ref currentTicks);
				if (this.currentBucket == null)
				{
					this.currentBucket = new FixedTimeAverage.WindowBucket(currentTicks, this.windowBucketLength);
				}
				result = this.currentBucket;
			}
			return result;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00038AAC File Offset: 0x00036CAC
		private bool VerifyTime(ref int currentTicks)
		{
			if (TickDiffer.Elapsed(this.lastCall, currentTicks) < TimeSpan.Zero)
			{
				currentTicks = this.lastCall;
				return false;
			}
			this.lastCall = currentTicks;
			return true;
		}

		// Token: 0x04001E44 RID: 7748
		private Queue<FixedTimeAverage.WindowBucket> waitingBuckets;

		// Token: 0x04001E45 RID: 7749
		private FixedTimeAverage.WindowBucket currentBucket;

		// Token: 0x04001E46 RID: 7750
		private ushort windowBucketLength;

		// Token: 0x04001E47 RID: 7751
		private TimeSpan totalWindowLength;

		// Token: 0x04001E48 RID: 7752
		private volatile float value;

		// Token: 0x04001E49 RID: 7753
		private int lastCall;

		// Token: 0x04001E4A RID: 7754
		private TimeSpan correctiveInterval;

		// Token: 0x04001E4B RID: 7755
		private int lastCorrectiveFix;

		// Token: 0x04001E4C RID: 7756
		private object instanceLock = new object();

		// Token: 0x04001E4D RID: 7757
		private static readonly TimeSpan DefaultCorrectiveInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x0200068C RID: 1676
		private class WindowBucket
		{
			// Token: 0x06001E73 RID: 7795 RVA: 0x00038AEF File Offset: 0x00036CEF
			public WindowBucket(int currentTicks, ushort windowSizeMsec)
			{
				this.ExpireTicks = TickDiffer.Add(currentTicks, (int)windowSizeMsec);
				this.WindowTimeSpan = TimeSpan.FromMilliseconds((double)windowSizeMsec);
			}

			// Token: 0x17000812 RID: 2066
			// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00038B11 File Offset: 0x00036D11
			// (set) Token: 0x06001E75 RID: 7797 RVA: 0x00038B19 File Offset: 0x00036D19
			internal int ExpireTicks { get; private set; }

			// Token: 0x17000813 RID: 2067
			// (get) Token: 0x06001E76 RID: 7798 RVA: 0x00038B22 File Offset: 0x00036D22
			// (set) Token: 0x06001E77 RID: 7799 RVA: 0x00038B2A File Offset: 0x00036D2A
			internal TimeSpan WindowTimeSpan { get; private set; }

			// Token: 0x17000814 RID: 2068
			// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00038B33 File Offset: 0x00036D33
			internal int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x06001E79 RID: 7801 RVA: 0x00038B40 File Offset: 0x00036D40
			internal bool Add(int currentTicks, uint value)
			{
				if (TickDiffer.Elapsed(this.ExpireTicks, currentTicks) < TimeSpan.Zero)
				{
					this.value += value;
					this.count++;
					return true;
				}
				return false;
			}

			// Token: 0x17000815 RID: 2069
			// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00038B8E File Offset: 0x00036D8E
			internal float Average
			{
				get
				{
					if (this.count != 0)
					{
						return this.value / (float)this.count;
					}
					return 0f;
				}
			}

			// Token: 0x04001E4F RID: 7759
			private volatile float value;

			// Token: 0x04001E50 RID: 7760
			private volatile int count;
		}
	}
}
