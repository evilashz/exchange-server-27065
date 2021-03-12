using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000160 RID: 352
	public class SlidingTotalCounter : SlidingWindow
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x00025CE6 File Offset: 0x00023EE6
		public SlidingTotalCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength) : this(slidingWindowLength, bucketLength, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00025D0D File Offset: 0x00023F0D
		internal SlidingTotalCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength, Func<DateTime> currentTimeProvider) : base(slidingWindowLength, bucketLength, currentTimeProvider)
		{
			this.valueBuckets = new long[this.NumberOfBuckets];
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00025D34 File Offset: 0x00023F34
		public long Sum
		{
			get
			{
				long result;
				lock (this.syncObject)
				{
					base.ExpireBucketsIfNecessary();
					result = this.valueTotal;
				}
				return result;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00025D7C File Offset: 0x00023F7C
		public long LastTotalValue
		{
			get
			{
				return this.valueTotal;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00025FA4 File Offset: 0x000241A4
		public IEnumerable<long> PastTotalValues
		{
			get
			{
				List<long> buckets = new List<long>(this.valueBuckets);
				int startIndex = 0;
				lock (this.syncObject)
				{
					startIndex = base.OldestBucketIndex % buckets.Count;
				}
				int curr = startIndex;
				long sum = 0L;
				for (int i = curr; i < buckets.Count; i++)
				{
					sum += buckets[i];
					yield return sum;
				}
				for (int j = 0; j < startIndex; j++)
				{
					sum += buckets[j];
					yield return sum;
				}
				yield break;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00025FC1 File Offset: 0x000241C1
		protected override Array ValueBuckets
		{
			get
			{
				return this.valueBuckets;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00025FC9 File Offset: 0x000241C9
		public long AddValue(long value)
		{
			return this.ExpireAndUpdate(value);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00025FD4 File Offset: 0x000241D4
		private long ExpireAndUpdate(long value)
		{
			long result;
			lock (this.syncObject)
			{
				base.ExpireBucketsIfNecessary();
				this.valueTotal += value;
				this.valueBuckets[this.currentBucket] += value;
				base.SetLastUpdateTime();
				result = this.valueTotal;
			}
			return result;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00026050 File Offset: 0x00024250
		protected override void ExpireBucket(int bucket)
		{
			this.valueTotal -= this.valueBuckets[this.currentBucket];
			this.valueBuckets[this.currentBucket] = 0L;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002607C File Offset: 0x0002427C
		public SlidingTotalCounter Merge(SlidingTotalCounter other)
		{
			ArgumentValidator.ThrowIfNull("other", other);
			if (!this.BucketLength.Equals(other.BucketLength))
			{
				throw new InvalidOperationException("Cannot merge two slidingTotalCounters with different values for BucketLength");
			}
			if (this.NumberOfBuckets != other.NumberOfBuckets)
			{
				throw new InvalidOperationException("Cannot merge two slidingTotalCounters with different values for NumberOfBuckets");
			}
			lock (this.syncObject)
			{
				base.ExpireBucketsIfNecessary();
				other.ExpireBucketsIfNecessary();
				for (int i = 0; i < this.valueBuckets.Length; i++)
				{
					this.valueBuckets[i] += other.valueBuckets[i];
					this.valueTotal += other.valueBuckets[i];
				}
			}
			return this;
		}

		// Token: 0x040006E1 RID: 1761
		private long valueTotal;

		// Token: 0x040006E2 RID: 1762
		private readonly long[] valueBuckets;

		// Token: 0x040006E3 RID: 1763
		private readonly object syncObject = new object();
	}
}
