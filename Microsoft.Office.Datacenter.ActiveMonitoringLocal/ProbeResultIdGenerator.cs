using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000081 RID: 129
	internal class ProbeResultIdGenerator : ResultIdGenerator<ProbeResult>
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001CF7C File Offset: 0x0001B17C
		protected override ExPerformanceCounter Counter
		{
			get
			{
				return LocalDataAccessPerfCounters.LastProbeResultId;
			}
		}
	}
}
