using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200038E RID: 910
	internal class EsoSessionPerformanceCounters : SessionPerformanceCounters
	{
		// Token: 0x0600308D RID: 12429 RVA: 0x00093CDB File Offset: 0x00091EDB
		public EsoSessionPerformanceCounters(PerfCounterGroup sessionsCounters, PerfCounterGroup requestsCounters, PerfCounterGroup esoSessionsCounters, PerfCounterGroup esoRequestsCounters) : base(sessionsCounters, requestsCounters)
		{
			this.esoSessionsCounter = esoSessionsCounters;
			this.esoRequestsCounter = esoRequestsCounters;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00093CF4 File Offset: 0x00091EF4
		public override void IncreaseSessionCounter()
		{
			base.IncreaseSessionCounter();
			this.esoSessionsCounter.Increment();
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x00093D07 File Offset: 0x00091F07
		public override void DecreaseSessionCounter()
		{
			base.DecreaseSessionCounter();
			this.esoSessionsCounter.Decrement();
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x00093D1A File Offset: 0x00091F1A
		public override void IncreaseRequestCounter()
		{
			base.IncreaseRequestCounter();
			this.esoRequestsCounter.Increment();
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x00093D2D File Offset: 0x00091F2D
		public override void DecreaseRequestCounter()
		{
			base.DecreaseRequestCounter();
			this.esoRequestsCounter.Decrement();
		}

		// Token: 0x0400237B RID: 9083
		private PerfCounterGroup esoSessionsCounter;

		// Token: 0x0400237C RID: 9084
		private PerfCounterGroup esoRequestsCounter;
	}
}
