using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000277 RID: 631
	internal class AveragePerfCounter
	{
		// Token: 0x060029AB RID: 10667 RVA: 0x00083404 File Offset: 0x00081604
		public AveragePerfCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase)
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

		// Token: 0x060029AC RID: 10668 RVA: 0x00083436 File Offset: 0x00081636
		public void AddSample(long sample)
		{
			this.averageCount.IncrementBy(sample);
			this.averageBase.Increment();
		}

		// Token: 0x040020E3 RID: 8419
		private ExPerformanceCounter averageCount;

		// Token: 0x040020E4 RID: 8420
		private ExPerformanceCounter averageBase;
	}
}
