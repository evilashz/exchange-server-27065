using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000132 RID: 306
	internal class HistoricalSlidingTotalCounter
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x000223D5 File Offset: 0x000205D5
		public HistoricalSlidingTotalCounter(TimeSpan slidingWindowLength, TimeSpan bucketLength, DateTime trackingStartTime)
		{
			this.eventTimeStamp = trackingStartTime;
			this.slidingTotalCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, new Func<DateTime>(this.CurrentTimeProvider));
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00022408 File Offset: 0x00020608
		public long AddValue(long value, DateTime eventTimeStamp)
		{
			long result;
			lock (this.syncObject)
			{
				this.eventTimeStamp = eventTimeStamp;
				result = this.slidingTotalCounter.AddValue(value);
			}
			return result;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00022458 File Offset: 0x00020658
		public long SumAt(DateTime timeStamp)
		{
			return this.AddValue(0L, timeStamp);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00022463 File Offset: 0x00020663
		private DateTime CurrentTimeProvider()
		{
			return this.eventTimeStamp;
		}

		// Token: 0x040005D4 RID: 1492
		private readonly SlidingTotalCounter slidingTotalCounter;

		// Token: 0x040005D5 RID: 1493
		private readonly object syncObject = new object();

		// Token: 0x040005D6 RID: 1494
		private DateTime eventTimeStamp;
	}
}
