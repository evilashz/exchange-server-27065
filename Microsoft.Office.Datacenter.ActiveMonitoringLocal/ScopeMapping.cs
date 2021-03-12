using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200005E RID: 94
	internal class ScopeMapping
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000192CB File Offset: 0x000174CB
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x000192D3 File Offset: 0x000174D3
		public string ScopeName { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000192DC File Offset: 0x000174DC
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x000192E4 File Offset: 0x000174E4
		public string ScopeType { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000192ED File Offset: 0x000174ED
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x000192F5 File Offset: 0x000174F5
		public List<SystemMonitoringMapping> SystemMonitoringInstances { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000192FE File Offset: 0x000174FE
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00019306 File Offset: 0x00017506
		public ScopeMapping Parent { get; set; }
	}
}
