using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver.Agents
{
	// Token: 0x0200001E RID: 30
	internal class AveragePerformanceCounterWrapper
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00006BB8 File Offset: 0x00004DB8
		internal AveragePerformanceCounterWrapper(ExPerformanceCounter performanceCounter)
		{
			this.performanceCounter = performanceCounter;
			this.slidingAverage = new SlidingAverageCounter(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006C04 File Offset: 0x00004E04
		internal void Update(long milliseconds)
		{
			this.slidingAverage.AddValue(milliseconds);
			lock (this.syncObject)
			{
				this.performanceCounter.RawValue = this.slidingAverage.CalculateAverage();
			}
		}

		// Token: 0x04000069 RID: 105
		private SlidingAverageCounter slidingAverage;

		// Token: 0x0400006A RID: 106
		private ExPerformanceCounter performanceCounter;

		// Token: 0x0400006B RID: 107
		private object syncObject = new object();
	}
}
