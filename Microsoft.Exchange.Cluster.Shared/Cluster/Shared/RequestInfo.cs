using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000A9 RID: 169
	public class RequestInfo
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x0001785C File Offset: 0x00015A5C
		public RequestInfo(OperationCategory operationCategory, OperationType operationType, string debugStr)
		{
			this.ClientRequestId = Guid.NewGuid();
			this.OperationCategory = operationCategory;
			this.OperationType = operationType;
			this.DebugStr = debugStr;
			this.InitiatedTime = DateTimeOffset.Now;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001788F File Offset: 0x00015A8F
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00017897 File Offset: 0x00015A97
		public Guid ClientRequestId { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000178A0 File Offset: 0x00015AA0
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000178A8 File Offset: 0x00015AA8
		public DateTimeOffset InitiatedTime { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000178B1 File Offset: 0x00015AB1
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x000178B9 File Offset: 0x00015AB9
		public OperationCategory OperationCategory { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000178C2 File Offset: 0x00015AC2
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x000178CA File Offset: 0x00015ACA
		public OperationType OperationType { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000178D3 File Offset: 0x00015AD3
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x000178DB File Offset: 0x00015ADB
		public string DebugStr { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000178E4 File Offset: 0x00015AE4
		public bool IsCloseKeyRequest
		{
			get
			{
				return this.OperationCategory == OperationCategory.CloseKey;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x000178EF File Offset: 0x00015AEF
		public bool IsGetBaseKeyRequest
		{
			get
			{
				return this.OperationCategory == OperationCategory.GetBaseKey;
			}
		}
	}
}
