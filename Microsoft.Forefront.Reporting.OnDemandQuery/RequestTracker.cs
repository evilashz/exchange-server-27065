using System;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x02000008 RID: 8
	public class RequestTracker
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000339B File Offset: 0x0000159B
		public RequestTracker()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000033A3 File Offset: 0x000015A3
		public RequestTracker(Guid tenantId, Guid onDemandQueryRequestId, int inBatchQueryId, OnDemandQueryType queryType)
		{
			this.TenantId = tenantId;
			this.RequestId = onDemandQueryRequestId;
			this.InBatchQueryId = inBatchQueryId;
			this.QueryType = queryType;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000033C8 File Offset: 0x000015C8
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000033D0 File Offset: 0x000015D0
		public Guid TenantId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000033D9 File Offset: 0x000015D9
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000033E1 File Offset: 0x000015E1
		public Guid RequestId { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000033EA File Offset: 0x000015EA
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000033F2 File Offset: 0x000015F2
		public int InBatchQueryId { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000033FB File Offset: 0x000015FB
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00003403 File Offset: 0x00001603
		public OnDemandQueryType QueryType { get; set; }
	}
}
