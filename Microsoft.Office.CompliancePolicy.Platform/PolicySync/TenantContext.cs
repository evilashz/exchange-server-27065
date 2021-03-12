using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	public sealed class TenantContext
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0001F129 File Offset: 0x0001D329
		public TenantContext(Guid tenantId, string tenantContextInfo)
		{
			this.TenantId = tenantId;
			this.TenantContextInfo = tenantContextInfo;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001F13F File Offset: 0x0001D33F
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001F147 File Offset: 0x0001D347
		public Guid TenantId { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001F150 File Offset: 0x0001D350
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001F158 File Offset: 0x0001D358
		public string TenantContextInfo { get; set; }
	}
}
