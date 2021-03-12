using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E7 RID: 231
	public abstract class SlidingWindow
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x0001A8CC File Offset: 0x00018ACC
		protected SlidingWindow(TimeSpan slidingWindowLength, TimeSpan bucketLength) : this(slidingWindowLength, bucketLength, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001A8F4 File Offset: 0x00018AF4
		protected SlidingWindow(TimeSpan slidingWindowLength, TimeSpan bucketLength, Func<DateTime> currentTimeProvider)
		{
			SlidingWindow.ValidateSlidingWindowAndBucketLength(slidingWindowLength, bucketLength);
			ExAssert.RetailAssert(currentTimeProvider != null, "Current time provider should not be null.");
			this.BucketLength = bucketLength;
			this.currentTimeProvider = currentTimeProvider;
			this.creationTime = this.currentTimeProvider();
			this.NumberOfBuckets = (int)(slidingWindowLength.Ticks / this.BucketLength.Ticks);
			this.filled = new bool[this.NumberOfBuckets];
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001A969 File Offset: 0x00018B69
		public bool IsEmpty
		{
			get
			{
				return this.GetTargetBucket(this.currentTimeProvider(), this.lastUpdateTime) == -1;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001A988 File Offset: 0x00018B88
		protected DateTime OldestDataTime
		{
			get
			{
				int oldestBucketIndex = this.OldestBucketIndex;
				int num = oldestBucketIndex;
				while (!this.filled[num])
				{
					num = (num + 1) % this.ValueBuckets.Length;
					if (num == oldestBucketIndex)
					{
						return DateTime.MaxValue;
					}
				}
				return this.GetTimeForBucket(num);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001A9CC File Offset: 0x00018BCC
		protected int OldestBucketIndex
		{
			get
			{
				this.ExpireBucketsIfNecessary();
				int num = 1;
				if (this.totalBucketsExpired < (long)this.NumberOfBuckets)
				{
					num = this.NumberOfBuckets - (int)this.totalBucketsExpired;
				}
				return (this.currentBucket + num) % this.ValueBuckets.Length;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000689 RID: 1673
		protected abstract Array ValueBuckets { get; }

		// Token: 0x0600068A RID: 1674 RVA: 0x0001AA14 File Offset: 0x00018C14
		internal static void ValidateSlidingWindowAndBucketLength(TimeSpan slidingWindowLength, TimeSpan bucketLength)
		{
			ExAssert.RetailAssert(bucketLength >= SlidingWindow.MinBucketLength, "bucketLength: [{0}] must be at least [{1}].", new object[]
			{
				bucketLength,
				SlidingWindow.MinBucketLength
			});
			ExAssert.RetailAssert(slidingWindowLength > TimeSpan.Zero, "slidingWindowLength: [{0}] must be greater than zero.", new object[]
			{
				slidingWindowLength
			});
			ExAssert.RetailAssert(bucketLength < slidingWindowLength, "bucketLength: [{0}] must be less than slidingWindowLength: [{1}]", new object[]
			{
				bucketLength,
				slidingWindowLength
			});
			ExAssert.RetailAssert(slidingWindowLength <= SlidingWindow.MaxSlidingWindowLength, "slidingWindowLength: [{0}] must be less than or equal to slidingWindowLength: [{1}]", new object[]
			{
				slidingWindowLength,
				SlidingWindow.MaxSlidingWindowLength
			});
			ExAssert.RetailAssert(slidingWindowLength.Ticks % bucketLength.Ticks == 0L, "slidingWindowLength: [{0}] must be a multiple of bucketLength: [{1}]", new object[]
			{
				slidingWindowLength,
				bucketLength
			});
		}

		// Token: 0x0600068B RID: 1675
		protected abstract void ExpireBucket(int bucket);

		// Token: 0x0600068C RID: 1676 RVA: 0x0001AB14 File Offset: 0x00018D14
		protected void ExpireBucketsIfNecessary()
		{
			ExAssert.RetailAssert(this.ValueBuckets != null, "Child class did not create a ValueBuckets array");
			if (this.ValueBuckets.Length != this.NumberOfBuckets)
			{
				ExAssert.RetailAssert(false, "The child class did not define the appropriate number of elements in the ValueBuckets array. Expected: {0}, Actual: {1}", new object[]
				{
					this.NumberOfBuckets,
					this.ValueBuckets.Length
				});
			}
			DateTime d = this.currentTimeProvider();
			long num = (d - this.creationTime).Ticks / this.BucketLength.Ticks;
			int num2 = 0;
			while (num2 < this.ValueBuckets.Length && this.totalBucketsExpired < num)
			{
				this.currentBucket = (this.currentBucket + 1) % this.ValueBuckets.Length;
				this.ExpireBucket(this.currentBucket);
				this.filled[this.currentBucket] = false;
				this.totalBucketsExpired += 1L;
				num2++;
			}
			this.totalBucketsExpired = num;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001AC19 File Offset: 0x00018E19
		protected void SetLastUpdateTime()
		{
			this.lastUpdateTime = this.currentTimeProvider();
			this.filled[this.currentBucket] = true;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001AC3C File Offset: 0x00018E3C
		protected DateTime RoundToBucketLength(DateTime dateTime)
		{
			return new DateTime(dateTime.Ticks / this.BucketLength.Ticks * this.BucketLength.Ticks);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001AC74 File Offset: 0x00018E74
		protected int GetTargetBucket(DateTime now, DateTime timeStampUtc)
		{
			long num = (now - timeStampUtc).Ticks / this.BucketLength.Ticks;
			if (num < (long)this.NumberOfBuckets)
			{
				long num2 = (long)this.currentBucket - num;
				if (num2 < 0L)
				{
					num2 = (long)this.NumberOfBuckets + num2;
				}
				return (int)num2;
			}
			return -1;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		private DateTime GetTimeForBucket(int index)
		{
			if (index < 0 || index >= this.NumberOfBuckets)
			{
				throw new InvalidOperationException(string.Format("index {0} is out of range", index));
			}
			long num = 0L;
			if (index < this.currentBucket)
			{
				num = (long)(this.currentBucket - index);
			}
			else if (index > this.currentBucket)
			{
				num = (long)(this.currentBucket + this.NumberOfBuckets - index);
			}
			return new DateTime(this.creationTime.Ticks + (this.totalBucketsExpired - num) * this.BucketLength.Ticks);
		}

		// Token: 0x04000466 RID: 1126
		internal static readonly TimeSpan MaxSlidingWindowLength = TimeSpan.FromHours(12.0);

		// Token: 0x04000467 RID: 1127
		internal static readonly TimeSpan MinBucketLength = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000468 RID: 1128
		protected readonly TimeSpan BucketLength;

		// Token: 0x04000469 RID: 1129
		protected readonly int NumberOfBuckets;

		// Token: 0x0400046A RID: 1130
		protected Func<DateTime> currentTimeProvider;

		// Token: 0x0400046B RID: 1131
		protected int currentBucket;

		// Token: 0x0400046C RID: 1132
		private readonly DateTime creationTime;

		// Token: 0x0400046D RID: 1133
		private long totalBucketsExpired;

		// Token: 0x0400046E RID: 1134
		private DateTime lastUpdateTime;

		// Token: 0x0400046F RID: 1135
		private bool[] filled;
	}
}
