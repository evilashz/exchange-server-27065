using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200015D RID: 349
	public class SlidingAverageCounter : SlidingWindow
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x000257B3 File Offset: 0x000239B3
		public SlidingAverageCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength) : this(slidingWindowLength, bucketLength, () => DateTime.UtcNow)
		{
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000257DA File Offset: 0x000239DA
		public SlidingAverageCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength, Func<DateTime> currentTimeProvider) : base(slidingWindowLength, bucketLength, currentTimeProvider)
		{
			this.valueBuckets = new SlidingAverageCounter.CounterData[this.NumberOfBuckets];
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00025801 File Offset: 0x00023A01
		protected override Array ValueBuckets
		{
			get
			{
				return this.valueBuckets;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00025809 File Offset: 0x00023A09
		public void AddValue(long value)
		{
			this.ExpireAndUpdate(value);
			base.SetLastUpdateTime();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00025818 File Offset: 0x00023A18
		public long CalculateAverage()
		{
			long result;
			lock (this.syncObject)
			{
				base.ExpireBucketsIfNecessary();
				if (this.bucketsFilled == 0)
				{
					result = 0L;
				}
				else
				{
					result = this.valueTotal / (long)this.bucketsFilled;
				}
			}
			return result;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00025878 File Offset: 0x00023A78
		public long CalculateAverageAcrossAllSamples(out long numberOfSamples)
		{
			long result;
			lock (this.syncObject)
			{
				base.ExpireBucketsIfNecessary();
				numberOfSamples = this.numberTotal;
				if (0L == this.numberTotal)
				{
					result = 0L;
				}
				else
				{
					result = this.valueTotal / this.numberTotal;
				}
			}
			return result;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000258E0 File Offset: 0x00023AE0
		private void ExpireAndUpdate(long value)
		{
			lock (this.syncObject)
			{
				if (this.bucketsFilled == 0)
				{
					this.bucketsFilled = 1;
				}
				base.ExpireBucketsIfNecessary();
				this.valueTotal += value;
				this.numberTotal += 1L;
				this.valueBuckets[this.currentBucket].AddValue(value);
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00025964 File Offset: 0x00023B64
		protected override void ExpireBucket(int bucket)
		{
			if (this.valueBuckets.Length > this.bucketsFilled)
			{
				this.bucketsFilled++;
			}
			this.valueTotal -= this.valueBuckets[this.currentBucket].TotalValue;
			this.numberTotal -= this.valueBuckets[this.currentBucket].TotalNumber;
			this.valueBuckets[this.currentBucket].Reset();
		}

		// Token: 0x040006D1 RID: 1745
		private long valueTotal;

		// Token: 0x040006D2 RID: 1746
		private long numberTotal;

		// Token: 0x040006D3 RID: 1747
		private SlidingAverageCounter.CounterData[] valueBuckets;

		// Token: 0x040006D4 RID: 1748
		private int bucketsFilled;

		// Token: 0x040006D5 RID: 1749
		private object syncObject = new object();

		// Token: 0x0200015E RID: 350
		private struct CounterData
		{
			// Token: 0x06000A07 RID: 2567 RVA: 0x000259EB File Offset: 0x00023BEB
			public void AddValue(long value)
			{
				this.totalValue += value;
				this.totalNumber += 1L;
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00025A0A File Offset: 0x00023C0A
			public long TotalValue
			{
				get
				{
					return this.totalValue;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00025A12 File Offset: 0x00023C12
			public long TotalNumber
			{
				get
				{
					return this.totalNumber;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00025A1C File Offset: 0x00023C1C
			public long AverageValue
			{
				get
				{
					if (this.totalNumber == 0L)
					{
						return 0L;
					}
					return this.totalValue / this.totalNumber;
				}
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x00025A45 File Offset: 0x00023C45
			public void Reset()
			{
				this.totalValue = 0L;
				this.totalNumber = 0L;
			}

			// Token: 0x040006D7 RID: 1751
			private long totalValue;

			// Token: 0x040006D8 RID: 1752
			private long totalNumber;
		}
	}
}
