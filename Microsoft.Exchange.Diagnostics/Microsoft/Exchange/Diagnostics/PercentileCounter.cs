using System;
using System.Globalization;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	public class PercentileCounter : IPercentileCounter
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x00019B2C File Offset: 0x00017D2C
		public PercentileCounter(TimeSpan expiryInterval, TimeSpan granularityInterval, long valueGranularity, long valueMaximum, CurrentTimeProvider currentTimeProvider)
		{
			if (granularityInterval <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "granularityInterval: [{0}] must be greater than zero.", new object[]
				{
					granularityInterval
				}));
			}
			if (expiryInterval <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "expiryInterval: [{0}] must be greater than zero.", new object[]
				{
					expiryInterval
				}));
			}
			if (expiryInterval != TimeSpan.MaxValue && granularityInterval >= expiryInterval)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "granularityInterval: [{0}] must be less than or equal to expiryInterval: [{1}]", new object[]
				{
					granularityInterval,
					expiryInterval
				}));
			}
			if (valueGranularity <= 0L)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "valueGranularity: [{0}] must be greater than 0.", new object[]
				{
					valueGranularity
				}));
			}
			if (valueMaximum <= 0L)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "valueMaximum: [{0}] must be greater than 0.", new object[]
				{
					valueMaximum
				}));
			}
			if (valueGranularity >= valueMaximum)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "valueGranularity: [{0}] must be less than valueMaximum: [{1}]", new object[]
				{
					valueGranularity,
					valueMaximum
				}));
			}
			this.granularityInterval = granularityInterval;
			this.expiryInterval = expiryInterval;
			if (currentTimeProvider != null)
			{
				this.currentTimeProvider = currentTimeProvider;
			}
			else
			{
				this.currentTimeProvider = new CurrentTimeProvider(this.CurrentTime);
			}
			this.creationTime = this.currentTimeProvider();
			this.mainSummary = new PercentileCounter.PercentileCounterSummary(valueGranularity, valueMaximum);
			if (this.expiryInterval != TimeSpan.MaxValue)
			{
				int num = (int)((this.expiryInterval.Ticks + this.granularityInterval.Ticks - 1L) / this.granularityInterval.Ticks);
				this.summaryBuckets = new PercentileCounter.PercentileCounterSummary[num];
				for (int i = 0; i < num; i++)
				{
					this.summaryBuckets[i] = new PercentileCounter.PercentileCounterSummary(valueGranularity, valueMaximum);
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00019D3F File Offset: 0x00017F3F
		public PercentileCounter(TimeSpan expiryInterval, TimeSpan granularityInterval, long valueGranularity, long valueMaximum) : this(expiryInterval, granularityInterval, valueGranularity, valueMaximum, null)
		{
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00019D50 File Offset: 0x00017F50
		internal double InfiniteBucketPercentage
		{
			get
			{
				double infiniteBucketPercentage;
				lock (this.syncObject)
				{
					this.ExpireBucketsIfNecessary();
					infiniteBucketPercentage = this.mainSummary.InfiniteBucketPercentage;
				}
				return infiniteBucketPercentage;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public void AddValue(long value)
		{
			lock (this.syncObject)
			{
				this.ExpireBucketsIfNecessary();
				int index = this.mainSummary.FindValueIndex(value);
				this.mainSummary.IncrementValueCount(index);
				if (this.expiryInterval != TimeSpan.MaxValue)
				{
					this.summaryBuckets[this.currentBucket].IncrementValueCount(index);
				}
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00019E20 File Offset: 0x00018020
		public long PercentileQuery(double percentage)
		{
			long num;
			return this.PercentileQuery(percentage, out num);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00019E38 File Offset: 0x00018038
		public long PercentileQuery(double percentage, out long samples)
		{
			long result;
			lock (this.syncObject)
			{
				this.ExpireBucketsIfNecessary();
				samples = this.mainSummary.TotalNumberOfValues;
				result = this.mainSummary.PercentileQuery(percentage);
			}
			return result;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00019E94 File Offset: 0x00018094
		protected void RemoveValue(long value)
		{
			if (this.expiryInterval != TimeSpan.MaxValue)
			{
				throw new InvalidOperationException("Cannot remove values when counter has expiration set");
			}
			lock (this.syncObject)
			{
				int index = this.mainSummary.FindValueIndex(value);
				this.mainSummary.DecrementValueCount(index);
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00019F04 File Offset: 0x00018104
		private void ExpireBucketsIfNecessary()
		{
			if (this.expiryInterval != TimeSpan.MaxValue)
			{
				DateTime d = this.currentTimeProvider();
				long num = (d - this.creationTime).Ticks / this.granularityInterval.Ticks;
				if (this.totalBucketsExpired < num)
				{
					this.UpdatePercentileCounterSummary(num - this.totalBucketsExpired);
				}
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00019F6C File Offset: 0x0001816C
		private void UpdatePercentileCounterSummary(long numberOfBucketsExpired)
		{
			this.totalBucketsExpired += numberOfBucketsExpired;
			if (numberOfBucketsExpired > (long)this.summaryBuckets.Length)
			{
				numberOfBucketsExpired = (long)this.summaryBuckets.Length;
			}
			while (numberOfBucketsExpired > 0L)
			{
				if (this.mainSummary.TotalNumberOfValues == 0L)
				{
					return;
				}
				this.ExpireBucket();
				numberOfBucketsExpired -= 1L;
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00019FC1 File Offset: 0x000181C1
		private void ExpireBucket()
		{
			this.currentBucket = (this.currentBucket + 1) % this.summaryBuckets.Length;
			this.mainSummary.SubtractAndClear(this.summaryBuckets[this.currentBucket]);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00019FF8 File Offset: 0x000181F8
		private DateTime CurrentTime()
		{
			return DateTime.UtcNow;
		}

		// Token: 0x04000452 RID: 1106
		private readonly TimeSpan expiryInterval;

		// Token: 0x04000453 RID: 1107
		private readonly TimeSpan granularityInterval;

		// Token: 0x04000454 RID: 1108
		private readonly DateTime creationTime;

		// Token: 0x04000455 RID: 1109
		private long totalBucketsExpired;

		// Token: 0x04000456 RID: 1110
		private PercentileCounter.PercentileCounterSummary mainSummary;

		// Token: 0x04000457 RID: 1111
		private PercentileCounter.PercentileCounterSummary[] summaryBuckets;

		// Token: 0x04000458 RID: 1112
		private volatile int currentBucket;

		// Token: 0x04000459 RID: 1113
		protected object syncObject = new object();

		// Token: 0x0400045A RID: 1114
		private CurrentTimeProvider currentTimeProvider;

		// Token: 0x020000E1 RID: 225
		[Serializable]
		private class PercentileCounterSummary
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x0001A000 File Offset: 0x00018200
			internal PercentileCounterSummary(long valueGranularity, long valueMaximum)
			{
				if (valueGranularity <= 0L)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "valueGranularity: [{0}] must be greater than 0.", new object[]
					{
						valueGranularity
					}));
				}
				if (valueMaximum <= 0L)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "valueMaximum: [{0}] must be greater than 0.", new object[]
					{
						valueMaximum
					}));
				}
				if (valueGranularity >= valueMaximum)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "valueGranularity: [{0}] must be less than valueMaximum: [{1}]", new object[]
					{
						valueGranularity,
						valueMaximum
					}));
				}
				this.valueGranularity = valueGranularity;
				int num = (int)((valueMaximum + this.valueGranularity - 1L) / this.valueGranularity) + 1;
				this.data = new long[num];
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001A0C5 File Offset: 0x000182C5
			internal long TotalNumberOfValues
			{
				get
				{
					return this.totalNumberOfValues;
				}
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0001A0D0 File Offset: 0x000182D0
			internal void IncrementValueCount(int index)
			{
				if (index < 0 || index >= this.data.Length)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "Index {0} not in range [0,{1}]", new object[]
					{
						index,
						this.data.Length - 1
					}));
				}
				this.data[index] += 1L;
				this.totalNumberOfValues += 1L;
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0001A150 File Offset: 0x00018350
			internal void DecrementValueCount(int index)
			{
				if (index < 0 || index >= this.data.Length)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "Index {0} not in rage [0, {1}]", new object[]
					{
						index,
						this.data.Length - 1
					}));
				}
				if (this.data[index] == 0L)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Index {0} is not present, cannot be removed", new object[]
					{
						index
					}));
				}
				this.data[index] -= 1L;
				this.totalNumberOfValues -= 1L;
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x0001A200 File Offset: 0x00018400
			internal int FindValueIndex(long value)
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "value {0} must be non-negative", new object[]
					{
						value
					}));
				}
				long num = value / this.valueGranularity;
				if (num >= (long)this.data.Length)
				{
					num = (long)(this.data.Length - 1);
				}
				return (int)num;
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0001A25C File Offset: 0x0001845C
			internal void SubtractAndClear(PercentileCounter.PercentileCounterSummary bucketToRemove)
			{
				if (this.data.Length != bucketToRemove.data.Length)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Bucket to remove is not of required size.", new object[0]));
				}
				if (bucketToRemove.totalNumberOfValues > 0L)
				{
					for (int i = 0; i < this.data.Length; i++)
					{
						this.data[i] -= bucketToRemove.data[i];
						bucketToRemove.totalNumberOfValues -= bucketToRemove.data[i];
						this.totalNumberOfValues -= bucketToRemove.data[i];
						bucketToRemove.data[i] = 0L;
						if (bucketToRemove.totalNumberOfValues == 0L)
						{
							break;
						}
					}
				}
				if (bucketToRemove.totalNumberOfValues != 0L)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "bucketToRemove.totalNumberOfValues: [{0}] has non-zero value after values expire.", new object[]
					{
						bucketToRemove.totalNumberOfValues
					}));
				}
				if (this.totalNumberOfValues < 0L)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Number of values in summary: [{0}] can not be negative.", new object[]
					{
						this.totalNumberOfValues
					}));
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001A37B File Offset: 0x0001857B
			internal double InfiniteBucketPercentage
			{
				get
				{
					if (this.TotalNumberOfValues == 0L)
					{
						return 0.0;
					}
					return (double)this.data[this.data.Length - 1] / (double)this.TotalNumberOfValues * 100.0;
				}
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x0001A3B8 File Offset: 0x000185B8
			internal long PercentileQuery(double percentage)
			{
				if (percentage < 0.0 || percentage > 100.0)
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "percentage:[{0}] must be in range [0,100]", new object[]
					{
						percentage
					}));
				}
				long num = (long)(percentage * (double)this.totalNumberOfValues / 100.0);
				long num2 = 0L;
				int i;
				for (i = 0; i < this.data.Length; i++)
				{
					num2 += this.data[i];
					if (num <= num2)
					{
						return this.valueGranularity * (long)i;
					}
				}
				if (i == this.data.Length)
				{
					i--;
				}
				return this.valueGranularity * (long)i;
			}

			// Token: 0x0400045B RID: 1115
			private long[] data;

			// Token: 0x0400045C RID: 1116
			private long valueGranularity;

			// Token: 0x0400045D RID: 1117
			private long totalNumberOfValues;
		}
	}
}
