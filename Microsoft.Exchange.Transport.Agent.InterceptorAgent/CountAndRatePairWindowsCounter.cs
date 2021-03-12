using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000007 RID: 7
	internal class CountAndRatePairWindowsCounter : ICountAndRatePairCounter
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002BC8 File Offset: 0x00000DC8
		internal CountAndRatePairWindowsCounter(ExPerformanceCounter runningCount, ExPerformanceCounter average, TimeSpan trackingLength, TimeSpan rateDuration, ICountAndRatePairCounter totalPairForAutoUpdate)
		{
			ArgumentValidator.ThrowIfNull("runningCount", runningCount);
			ArgumentValidator.ThrowIfNull("average", average);
			this.runningCount = runningCount;
			this.average = average;
			this.slidingAverage = new SlidingAverageCounter(trackingLength, rateDuration);
			this.totalPairForAutoUpdate = totalPairForAutoUpdate;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C15 File Offset: 0x00000E15
		public void AddValue(long value)
		{
			this.runningCount.IncrementBy(value);
			this.slidingAverage.AddValue(value);
			if (this.totalPairForAutoUpdate != null)
			{
				this.totalPairForAutoUpdate.AddValue(value);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C44 File Offset: 0x00000E44
		public void UpdateAverage()
		{
			this.average.RawValue = this.slidingAverage.CalculateAverage();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void GetDiagnosticInfo(XElement parent)
		{
			parent.Add(new XElement(ExPerformanceCounter.GetEncodedName(this.runningCount.CounterName), this.runningCount.NextValue()));
			parent.Add(new XElement(ExPerformanceCounter.GetEncodedName(this.average.CounterName), this.average.NextValue()));
		}

		// Token: 0x04000019 RID: 25
		private readonly ExPerformanceCounter runningCount;

		// Token: 0x0400001A RID: 26
		private readonly ExPerformanceCounter average;

		// Token: 0x0400001B RID: 27
		private readonly SlidingAverageCounter slidingAverage;

		// Token: 0x0400001C RID: 28
		private readonly ICountAndRatePairCounter totalPairForAutoUpdate;
	}
}
