using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000EC RID: 236
	public class AverageTimeCounter : AverageTimeCounterBase, ITimerCounter, IDisposable
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001B614 File Offset: 0x00019814
		public AverageTimeCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase) : base(averageCount, averageBase)
		{
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001B61E File Offset: 0x0001981E
		public AverageTimeCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase, bool autoStart) : base(averageCount, averageBase, autoStart)
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B629 File Offset: 0x00019829
		void IDisposable.Dispose()
		{
			if (base.IsStarted)
			{
				base.Stop();
			}
		}
	}
}
