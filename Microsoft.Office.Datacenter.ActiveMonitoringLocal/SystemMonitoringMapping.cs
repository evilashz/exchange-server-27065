using System;
using System.Collections.Generic;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000061 RID: 97
	internal class SystemMonitoringMapping
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000197D4 File Offset: 0x000179D4
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x000197DC File Offset: 0x000179DC
		public string InstanceName { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000197E5 File Offset: 0x000179E5
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x000197ED File Offset: 0x000179ED
		public List<ScopeMapping> Scopes { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x000197F6 File Offset: 0x000179F6
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x000197FE File Offset: 0x000179FE
		public BatchingUploader<ScopeNotificationRawData> Uploader { get; set; }
	}
}
