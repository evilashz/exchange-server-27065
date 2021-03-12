using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000009 RID: 9
	internal class AveragePerfCounter
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000027C3 File Offset: 0x000009C3
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

		// Token: 0x06000027 RID: 39 RVA: 0x000027F5 File Offset: 0x000009F5
		public void AddSample(long sample)
		{
			this.averageCount.IncrementBy(sample);
			this.averageBase.Increment();
		}

		// Token: 0x0400002B RID: 43
		private ExPerformanceCounter averageCount;

		// Token: 0x0400002C RID: 44
		private ExPerformanceCounter averageBase;
	}
}
