using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007C RID: 124
	internal struct FailureData
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001EFA5 File Offset: 0x0001D1A5
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0001EFAD File Offset: 0x0001D1AD
		public Guid RequestGuid { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001EFB6 File Offset: 0x0001D1B6
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001EFBE File Offset: 0x0001D1BE
		public bool IsFatal { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001EFC7 File Offset: 0x0001D1C7
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001EFCF File Offset: 0x0001D1CF
		public Exception Failure { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001EFE0 File Offset: 0x0001D1E0
		public RequestState RequestState { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001EFE9 File Offset: 0x0001D1E9
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0001EFF1 File Offset: 0x0001D1F1
		public SyncStage SyncStage { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001EFFA File Offset: 0x0001D1FA
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0001F002 File Offset: 0x0001D202
		public string FolderName { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001F00B File Offset: 0x0001D20B
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0001F013 File Offset: 0x0001D213
		public string OperationType { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001F01C File Offset: 0x0001D21C
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001F024 File Offset: 0x0001D224
		public string WatsonHash { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001F02D File Offset: 0x0001D22D
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001F035 File Offset: 0x0001D235
		public Guid FailureGuid { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001F03E File Offset: 0x0001D23E
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0001F046 File Offset: 0x0001D246
		public int FailureLevel { get; set; }
	}
}
