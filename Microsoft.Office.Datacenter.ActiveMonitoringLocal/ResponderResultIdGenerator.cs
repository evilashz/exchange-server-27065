using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000083 RID: 131
	internal class ResponderResultIdGenerator : ResultIdGenerator<ResponderResult>
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001CF9A File Offset: 0x0001B19A
		protected override ExPerformanceCounter Counter
		{
			get
			{
				return LocalDataAccessPerfCounters.LastResponderResultId;
			}
		}
	}
}
