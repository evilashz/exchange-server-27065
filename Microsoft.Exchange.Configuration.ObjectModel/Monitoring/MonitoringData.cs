using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	public class MonitoringData
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x000359EA File Offset: 0x00033BEA
		public MonitoringData()
		{
			this.innerEvents = new MonitoringEventCollection();
			this.innerPerformanceCounters = new MonitoringPerformanceCounterCollection();
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00035A08 File Offset: 0x00033C08
		public MonitoringEventCollection Events
		{
			get
			{
				return this.innerEvents;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00035A10 File Offset: 0x00033C10
		public MonitoringPerformanceCounterCollection PerformanceCounters
		{
			get
			{
				return this.innerPerformanceCounters;
			}
		}

		// Token: 0x040003ED RID: 1005
		private MonitoringEventCollection innerEvents;

		// Token: 0x040003EE RID: 1006
		private MonitoringPerformanceCounterCollection innerPerformanceCounters;
	}
}
