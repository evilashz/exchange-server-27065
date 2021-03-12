using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000084 RID: 132
	internal class MaintenanceResultIdGenerator : ResultIdGenerator<MaintenanceResult>
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001CFA9 File Offset: 0x0001B1A9
		protected override ExPerformanceCounter Counter
		{
			get
			{
				return LocalDataAccessPerfCounters.LastMaintenanceResultId;
			}
		}
	}
}
