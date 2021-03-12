using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200015F RID: 351
	public class SlidingPercentageCounter : SlidingWindow
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x00025A58 File Offset: 0x00023C58
		public SlidingPercentageCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength, bool isFailureReporting, Func<DateTime> currentTimeProvider) : base(slidingWindowLength, bucketLength, currentTimeProvider)
		{
			this.isFailureReporting = isFailureReporting;
			this.valuesExpires = true;
			this.numeratorBuckets = new long[this.NumberOfBuckets];
			this.denominatorBuckets = new long[this.NumberOfBuckets];
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00025AB1 File Offset: 0x00023CB1
		public SlidingPercentageCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength, bool isFailureReporting) : this(slidingWindowLength, bucketLength, isFailureReporting, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00025AD9 File Offset: 0x00023CD9
		public SlidingPercentageCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength) : this(slidingWindowLength, bucketLength, false)
		{
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00025AE4 File Offset: 0x00023CE4
		public SlidingPercentageCounter() : base(SlidingWindow.MaxSlidingWindowLength, SlidingWindow.MinBucketLength)
		{
			this.valuesExpires = false;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00025B08 File Offset: 0x00023D08
		public long Numerator
		{
			get
			{
				return this.numeratorTotal;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00025B10 File Offset: 0x00023D10
		public long Denominator
		{
			get
			{
				return this.denominatorTotal;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00025B18 File Offset: 0x00023D18
		public new bool IsEmpty
		{
			get
			{
				return this.valuesExpires && base.IsEmpty;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00025B2A File Offset: 0x00023D2A
		protected override Array ValueBuckets
		{
			get
			{
				return this.numeratorBuckets;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00025B32 File Offset: 0x00023D32
		public double AddNumerator(long value)
		{
			return this.Add(value, 0L);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00025B3D File Offset: 0x00023D3D
		public double AddDenominator(long value)
		{
			return this.Add(0L, value);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00025B48 File Offset: 0x00023D48
		public double Add(long numerator, long denominator)
		{
			base.SetLastUpdateTime();
			return this.ExpireAndUpdate(numerator, denominator);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00025B58 File Offset: 0x00023D58
		public double GetSlidingPercentage()
		{
			return this.ExpireAndUpdate(0L, 0L);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00025B64 File Offset: 0x00023D64
		private double ExpireAndUpdate(long numerator, long denominator)
		{
			double result;
			lock (this.syncObject)
			{
				if (this.valuesExpires)
				{
					base.ExpireBucketsIfNecessary();
				}
				this.numeratorTotal += numerator;
				this.denominatorTotal += denominator;
				if (this.valuesExpires)
				{
					this.denominatorBuckets[this.currentBucket] += denominator;
					this.numeratorBuckets[this.currentBucket] += numerator;
				}
				if (this.denominatorTotal == 0L)
				{
					if (this.numeratorTotal == 0L)
					{
						if (this.isFailureReporting)
						{
							result = 0.0;
						}
						else
						{
							result = 100.0;
						}
					}
					else if (this.numeratorTotal > 0L)
					{
						result = double.MaxValue;
					}
					else
					{
						result = double.MinValue;
					}
				}
				else
				{
					result = (double)this.numeratorTotal * 100.0 / (double)this.denominatorTotal;
				}
			}
			return result;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00025C80 File Offset: 0x00023E80
		protected override void ExpireBucket(int bucket)
		{
			this.numeratorTotal -= this.numeratorBuckets[this.currentBucket];
			this.denominatorTotal -= this.denominatorBuckets[this.currentBucket];
			this.numeratorBuckets[this.currentBucket] = 0L;
			this.denominatorBuckets[this.currentBucket] = 0L;
		}

		// Token: 0x040006D9 RID: 1753
		private readonly bool valuesExpires;

		// Token: 0x040006DA RID: 1754
		private readonly bool isFailureReporting;

		// Token: 0x040006DB RID: 1755
		private long numeratorTotal;

		// Token: 0x040006DC RID: 1756
		private long denominatorTotal;

		// Token: 0x040006DD RID: 1757
		private long[] numeratorBuckets;

		// Token: 0x040006DE RID: 1758
		private long[] denominatorBuckets;

		// Token: 0x040006DF RID: 1759
		private object syncObject = new object();
	}
}
