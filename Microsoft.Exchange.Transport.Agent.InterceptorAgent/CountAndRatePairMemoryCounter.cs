using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000006 RID: 6
	internal class CountAndRatePairMemoryCounter : ICountAndRatePairCounter
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public CountAndRatePairMemoryCounter(string counterName, string averageCounterName, TimeSpan trackingLength, TimeSpan rateDuration, ICountAndRatePairCounter totalPairCounters = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty(counterName, counterName);
			ArgumentValidator.ThrowIfNullOrEmpty(averageCounterName, averageCounterName);
			this.averageCounterName = averageCounterName;
			this.runningCount = new MemoryCounter(counterName);
			this.slidingAverage = new SlidingAverageCounter(trackingLength, rateDuration);
			this.totalPairForAutoUpdate = totalPairCounters;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B2F File Offset: 0x00000D2F
		public void AddValue(long value)
		{
			this.runningCount.IncrementBy(value);
			this.slidingAverage.AddValue(value);
			if (this.totalPairForAutoUpdate != null)
			{
				this.totalPairForAutoUpdate.AddValue(value);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B5E File Offset: 0x00000D5E
		public void UpdateAverage()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B60 File Offset: 0x00000D60
		public void GetDiagnosticInfo(XElement parent)
		{
			parent.Add(new XElement(ExPerformanceCounter.GetEncodedName(this.runningCount.CounterName), this.runningCount.RawValue));
			parent.Add(new XElement(ExPerformanceCounter.GetEncodedName(this.averageCounterName), this.slidingAverage.CalculateAverage()));
		}

		// Token: 0x04000015 RID: 21
		private readonly SlidingAverageCounter slidingAverage;

		// Token: 0x04000016 RID: 22
		private readonly ICountAndRatePairCounter totalPairForAutoUpdate;

		// Token: 0x04000017 RID: 23
		private readonly MemoryCounter runningCount;

		// Token: 0x04000018 RID: 24
		private readonly string averageCounterName;
	}
}
