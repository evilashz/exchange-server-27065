using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x0200020B RID: 523
	public class PerformanceCounterCheckSetting
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x00061D38 File Offset: 0x0005FF38
		public PerformanceCounterCheckSetting()
		{
			this.MinThreshold = int.MinValue;
			this.MaxThreshold = int.MaxValue;
		}

		// Token: 0x04000AFD RID: 2813
		public string ReasonToSkip;

		// Token: 0x04000AFE RID: 2814
		public int MinThreshold;

		// Token: 0x04000AFF RID: 2815
		public int MaxThreshold;

		// Token: 0x04000B00 RID: 2816
		public string CategoryName;

		// Token: 0x04000B01 RID: 2817
		public string CounterName;

		// Token: 0x04000B02 RID: 2818
		public string InstanceName;
	}
}
