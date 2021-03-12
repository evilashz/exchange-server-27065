using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000EA RID: 234
	public class AverageTimeCounterBase : AverageCounter
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x0001B5A0 File Offset: 0x000197A0
		public AverageTimeCounterBase(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase) : base(averageCount, averageBase)
		{
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001B5AA File Offset: 0x000197AA
		public AverageTimeCounterBase(ExPerformanceCounter averageCount, ExPerformanceCounter averageBase, bool autoStart) : base(averageCount, averageBase)
		{
			if (autoStart)
			{
				this.Start();
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001B5BD File Offset: 0x000197BD
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0001B5C5 File Offset: 0x000197C5
		private Stopwatch Stopwatch { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001B5CE File Offset: 0x000197CE
		protected bool IsStarted
		{
			get
			{
				return this.Stopwatch != null;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001B5DC File Offset: 0x000197DC
		public void Start()
		{
			this.Stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001B5EC File Offset: 0x000197EC
		public long Stop()
		{
			long elapsedTicks = this.Stopwatch.ElapsedTicks;
			base.AddSample(elapsedTicks);
			this.Stopwatch = null;
			return elapsedTicks;
		}
	}
}
