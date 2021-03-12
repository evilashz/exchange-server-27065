using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000154 RID: 340
	public class SegmentedSlidingCounter
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x000241CF File Offset: 0x000223CF
		public SegmentedSlidingCounter(TimeSpan[] segments, TimeSpan bucketLength) : this(segments, bucketLength, null)
		{
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000241DC File Offset: 0x000223DC
		internal SegmentedSlidingCounter(TimeSpan[] segments, TimeSpan bucketLength, Func<DateTime> currentTimeProvider)
		{
			if (segments == null)
			{
				throw new ArgumentNullException("segments");
			}
			if (segments.Length == 0)
			{
				throw new ArgumentException("segments cannot be empty");
			}
			this.bucketLength = bucketLength;
			if (currentTimeProvider == null)
			{
				this.currentTimeProvider = new Func<DateTime>(this.GetCurrentTime);
				this.creationTime = this.RoundToBucketLength(DateTime.UtcNow);
				this.currentTime = this.creationTime;
			}
			else
			{
				this.currentTimeProvider = currentTimeProvider;
				this.creationTime = this.RoundToBucketLength(this.currentTimeProvider());
			}
			this.numSegments = (long)segments.Length;
			this.numBucketsPerSegment = new long[this.numSegments];
			this.segmentValues = new long[this.numSegments + 1L];
			this.numBuckets = 0L;
			for (long num = 0L; num < this.numSegments; num += 1L)
			{
				long num2;
				checked
				{
					TimeSpan slidingWindowLength = segments[(int)((IntPtr)num)];
					SlidingWindow.ValidateSlidingWindowAndBucketLength(slidingWindowLength, bucketLength);
					num2 = slidingWindowLength.Ticks / this.bucketLength.Ticks;
					this.numBucketsPerSegment[(int)((IntPtr)num)] = num2;
				}
				this.numBuckets += num2;
			}
			this.bucketValues = new long[this.numBuckets];
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00024308 File Offset: 0x00022508
		public DateTime AddEventsAt(DateTime timeStampUtc, long eventCount)
		{
			DateTime result;
			lock (this)
			{
				result = this.AddOrRemoveEventsAt(timeStampUtc, eventCount, false);
			}
			return result;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00024348 File Offset: 0x00022548
		public void RemoveEventsAt(DateTime timeStampUtc, long eventCount)
		{
			lock (this)
			{
				this.AddOrRemoveEventsAt(timeStampUtc, eventCount, true);
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00024388 File Offset: 0x00022588
		public long TimedUpdate()
		{
			return this.TimedUpdate(this.segmentValues);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00024398 File Offset: 0x00022598
		public long TimedUpdate(long[] segmentValues)
		{
			long result;
			lock (this)
			{
				this.ExpireBucketsIfNecessary(this.RoundToBucketLength(this.currentTimeProvider()));
				result = this.GetSegmentValues(segmentValues);
			}
			return result;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000243F0 File Offset: 0x000225F0
		internal long GetSegmentValues(long[] segmentValues)
		{
			if (segmentValues == null)
			{
				throw new ArgumentNullException("segmentValues");
			}
			if ((long)segmentValues.Length < this.numSegments + 1L)
			{
				throw new ArgumentException("segmentValues");
			}
			long num = 0L;
			long num2 = this.currentBucketIndex;
			for (long num3 = 0L; num3 < this.numSegments; num3 += 1L)
			{
				segmentValues[(int)(checked((IntPtr)num3))] = 0L;
				for (long num4 = 0L; num4 < this.numBucketsPerSegment[(int)(checked((IntPtr)num3))]; num4 += 1L)
				{
					segmentValues[(int)(checked((IntPtr)num3))] += this.bucketValues[(int)(checked((IntPtr)num2))];
					num2 = (num2 + 1L) % this.numBuckets;
				}
				num += segmentValues[(int)(checked((IntPtr)num3))];
			}
			segmentValues[(int)(checked((IntPtr)this.numSegments))] = this.excessBucket;
			return num;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000244A0 File Offset: 0x000226A0
		private DateTime AddOrRemoveEventsAt(DateTime timeStampUtc, long eventCount, bool isRemove)
		{
			if (eventCount <= 0L)
			{
				throw new ArgumentOutOfRangeException("eventCount: " + eventCount);
			}
			DateTime dateTime = this.currentTimeProvider();
			if (timeStampUtc > dateTime)
			{
				if (isRemove)
				{
					throw new ArgumentOutOfRangeException(string.Format("event timestamp [{0}] cannot be later than the current time [{1}] on Remove()", timeStampUtc, dateTime));
				}
				timeStampUtc = dateTime;
			}
			DateTime now = this.RoundToBucketLength(dateTime);
			this.ExpireBucketsIfNecessary(now);
			long num = isRemove ? (-1L * eventCount) : eventCount;
			long targetBucket = this.GetTargetBucket(now, this.RoundToBucketLength(timeStampUtc));
			long num2;
			if (targetBucket == -1L)
			{
				this.excessBucket += num;
				num2 = this.excessBucket;
			}
			else
			{
				this.bucketValues[(int)(checked((IntPtr)targetBucket))] += num;
				num2 = this.bucketValues[(int)(checked((IntPtr)targetBucket))];
			}
			if (num2 < 0L)
			{
				throw new InvalidOperationException("bucket value is negative: " + num2);
			}
			return timeStampUtc;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00024588 File Offset: 0x00022788
		private void ExpireBucketsIfNecessary(DateTime now)
		{
			long num = (now - this.creationTime).Ticks / this.bucketLength.Ticks;
			if (this.totalBucketsExpired < num)
			{
				long num2 = 0L;
				while (num2 < this.numBuckets && this.totalBucketsExpired < num)
				{
					this.currentBucketIndex = ((this.currentBucketIndex > 0L) ? (this.currentBucketIndex - 1L) : (this.numBuckets - 1L));
					this.excessBucket += this.bucketValues[(int)(checked((IntPtr)this.currentBucketIndex))];
					this.bucketValues[(int)(checked((IntPtr)this.currentBucketIndex))] = 0L;
					this.totalBucketsExpired += 1L;
					num2 += 1L;
				}
				this.totalBucketsExpired = num;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00024648 File Offset: 0x00022848
		private long GetTargetBucket(DateTime now, DateTime timeStampUtc)
		{
			long num = (now - timeStampUtc).Ticks / this.bucketLength.Ticks;
			if (num >= this.numBuckets)
			{
				return -1L;
			}
			return (this.currentBucketIndex + num) % this.numBuckets;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00024690 File Offset: 0x00022890
		private DateTime GetCurrentTime()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > this.currentTime)
			{
				this.currentTime = utcNow;
			}
			return this.currentTime;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000246C0 File Offset: 0x000228C0
		private DateTime RoundToBucketLength(DateTime dateTime)
		{
			return new DateTime(dateTime.Ticks / this.bucketLength.Ticks * this.bucketLength.Ticks);
		}

		// Token: 0x04000686 RID: 1670
		private readonly Func<DateTime> currentTimeProvider;

		// Token: 0x04000687 RID: 1671
		private readonly DateTime creationTime;

		// Token: 0x04000688 RID: 1672
		private readonly TimeSpan bucketLength;

		// Token: 0x04000689 RID: 1673
		private readonly long numBuckets;

		// Token: 0x0400068A RID: 1674
		private readonly long numSegments;

		// Token: 0x0400068B RID: 1675
		private readonly long[] segmentValues;

		// Token: 0x0400068C RID: 1676
		private DateTime currentTime;

		// Token: 0x0400068D RID: 1677
		private long totalBucketsExpired;

		// Token: 0x0400068E RID: 1678
		private long currentBucketIndex;

		// Token: 0x0400068F RID: 1679
		private long excessBucket;

		// Token: 0x04000690 RID: 1680
		private long[] bucketValues;

		// Token: 0x04000691 RID: 1681
		private long[] numBucketsPerSegment;
	}
}
