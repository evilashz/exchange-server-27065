using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200038D RID: 909
	internal class SessionPerformanceCounters
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x00093C91 File Offset: 0x00091E91
		public SessionPerformanceCounters(PerfCounterGroup sessionsCounters, PerfCounterGroup requestsCounters)
		{
			this.sessionsCounter = sessionsCounters;
			this.requestsCounter = requestsCounters;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x00093CA7 File Offset: 0x00091EA7
		public virtual void IncreaseSessionCounter()
		{
			this.sessionsCounter.Increment();
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x00093CB4 File Offset: 0x00091EB4
		public virtual void DecreaseSessionCounter()
		{
			this.sessionsCounter.Decrement();
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x00093CC1 File Offset: 0x00091EC1
		public virtual void IncreaseRequestCounter()
		{
			this.requestsCounter.Increment();
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x00093CCE File Offset: 0x00091ECE
		public virtual void DecreaseRequestCounter()
		{
			this.requestsCounter.Decrement();
		}

		// Token: 0x04002379 RID: 9081
		private PerfCounterGroup sessionsCounter;

		// Token: 0x0400237A RID: 9082
		private PerfCounterGroup requestsCounter;
	}
}
