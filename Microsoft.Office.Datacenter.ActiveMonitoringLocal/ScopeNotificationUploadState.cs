using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000064 RID: 100
	internal sealed class ScopeNotificationUploadState
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001997E File Offset: 0x00017B7E
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00019986 File Offset: 0x00017B86
		internal ScopeNotificationRawData Data { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001998F File Offset: 0x00017B8F
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00019997 File Offset: 0x00017B97
		internal int LastUploadedHealthState { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x000199A0 File Offset: 0x00017BA0
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x000199A8 File Offset: 0x00017BA8
		internal DateTime LastSucceededUpload { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000199B1 File Offset: 0x00017BB1
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x000199B9 File Offset: 0x00017BB9
		internal DateTime LastFailedUpload { get; set; }
	}
}
