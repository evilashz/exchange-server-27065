using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000082 RID: 130
	internal class MonitorResultIdGenerator : ResultIdGenerator<MonitorResult>
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001CF8B File Offset: 0x0001B18B
		protected override ExPerformanceCounter Counter
		{
			get
			{
				return LocalDataAccessPerfCounters.LastMonitorResultId;
			}
		}
	}
}
