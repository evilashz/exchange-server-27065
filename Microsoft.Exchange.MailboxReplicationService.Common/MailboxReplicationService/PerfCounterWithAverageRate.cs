using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200015B RID: 347
	internal class PerfCounterWithAverageRate
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0001CF2F File Offset: 0x0001B12F
		public PerfCounterWithAverageRate(ExPerformanceCounter normalCounter, ExPerformanceCounter rateCounter, ExPerformanceCounter baseCounter, int counterUnit, TimeSpan timeUnit)
		{
			this.normalCounter = normalCounter;
			this.rateCounter = rateCounter;
			this.baseCounter = baseCounter;
			this.counterUnit = counterUnit;
			this.timeUnitSeconds = (int)timeUnit.TotalSeconds;
			this.lastTimestamp = ExDateTime.UtcNow;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001CF70 File Offset: 0x0001B170
		public void IncrementBy(long delta)
		{
			if (this.normalCounter != null)
			{
				this.normalCounter.IncrementBy(delta);
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			TimeSpan timeSpan = utcNow - this.lastTimestamp;
			this.lastTimestamp = utcNow;
			int num = (int)(timeSpan.TotalSeconds * (double)this.counterUnit);
			this.rateCounter.IncrementBy(delta * (long)this.timeUnitSeconds);
			this.baseCounter.IncrementBy((long)num);
		}

		// Token: 0x040006F2 RID: 1778
		private ExPerformanceCounter normalCounter;

		// Token: 0x040006F3 RID: 1779
		private ExPerformanceCounter rateCounter;

		// Token: 0x040006F4 RID: 1780
		private ExPerformanceCounter baseCounter;

		// Token: 0x040006F5 RID: 1781
		private int counterUnit;

		// Token: 0x040006F6 RID: 1782
		private int timeUnitSeconds;

		// Token: 0x040006F7 RID: 1783
		private ExDateTime lastTimestamp;
	}
}
