using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E6 RID: 230
	public class AverageCounter : IAverageCounter
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x0001A878 File Offset: 0x00018A78
		public AverageCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase)
		{
			if (averageCount == null)
			{
				throw new ArgumentNullException("averageCount");
			}
			if (averageBase == null)
			{
				throw new ArgumentNullException("averageBase");
			}
			this.averageCount = averageCount;
			this.averageBase = averageBase;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001A8AA File Offset: 0x00018AAA
		public void AddSample(long sample)
		{
			this.averageCount.IncrementBy(sample);
			this.averageBase.Increment();
		}

		// Token: 0x04000464 RID: 1124
		private ExPerformanceCounter averageCount;

		// Token: 0x04000465 RID: 1125
		private ExPerformanceCounter averageBase;
	}
}
