using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200000A RID: 10
	internal class AverageTimePerfCounter : AveragePerfCounter, IDisposable
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002810 File Offset: 0x00000A10
		public AverageTimePerfCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase) : base(averageCount, averageBase)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000281A File Offset: 0x00000A1A
		public AverageTimePerfCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase, bool autoStart) : base(averageCount, averageBase)
		{
			if (autoStart)
			{
				this.Start();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000282D File Offset: 0x00000A2D
		public void Start()
		{
			this.stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000283A File Offset: 0x00000A3A
		public void Stop()
		{
			if (this.stopwatch != null)
			{
				base.AddSample(this.stopwatch.ElapsedTicks);
			}
			this.stopwatch = null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000285C File Offset: 0x00000A5C
		public void Dispose()
		{
			if (this.stopwatch != null)
			{
				this.Stop();
			}
		}

		// Token: 0x0400002D RID: 45
		private Stopwatch stopwatch;
	}
}
