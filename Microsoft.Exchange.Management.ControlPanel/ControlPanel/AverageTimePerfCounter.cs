using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000278 RID: 632
	internal class AverageTimePerfCounter : AveragePerfCounter, IDisposable
	{
		// Token: 0x060029AD RID: 10669 RVA: 0x00083451 File Offset: 0x00081651
		public AverageTimePerfCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase) : base(averageCount, averageBase)
		{
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x0008345B File Offset: 0x0008165B
		public AverageTimePerfCounter(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase, bool autoStart) : base(averageCount, averageBase)
		{
			if (autoStart)
			{
				this.Start();
			}
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x0008346E File Offset: 0x0008166E
		public void Start()
		{
			this.stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x0008347B File Offset: 0x0008167B
		public void Stop()
		{
			base.AddSample(this.stopwatch.ElapsedTicks);
			this.stopwatch = null;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00083495 File Offset: 0x00081695
		void IDisposable.Dispose()
		{
			if (this.stopwatch != null)
			{
				this.Stop();
			}
		}

		// Token: 0x040020E5 RID: 8421
		private Stopwatch stopwatch;
	}
}
