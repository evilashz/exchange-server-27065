using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	public sealed class TenantInfo
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x0001F1B9 File Offset: 0x0001D3B9
		public TenantInfo(Guid tenantId, string syncSvcUrl, Dictionary<ConfigurationObjectType, SyncInfo> syncInfoTable)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("syncSvcUrl", syncSvcUrl);
			this.TenantId = tenantId;
			this.SyncSvcUrl = syncSvcUrl;
			this.SyncInfoTable = syncInfoTable;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001F1E1 File Offset: 0x0001D3E1
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0001F1E9 File Offset: 0x0001D3E9
		public Guid TenantId { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001F1F2 File Offset: 0x0001D3F2
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0001F1FA File Offset: 0x0001D3FA
		public string SyncSvcUrl { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001F203 File Offset: 0x0001D403
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0001F20B File Offset: 0x0001D40B
		public DateTime? LastAttemptedSyncUTC { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001F214 File Offset: 0x0001D414
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0001F21C File Offset: 0x0001D41C
		public DateTime? LastSuccessfulSyncUTC { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001F225 File Offset: 0x0001D425
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0001F22D File Offset: 0x0001D42D
		public DateTime? LastErrorTimeUTC { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001F236 File Offset: 0x0001D436
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0001F23E File Offset: 0x0001D43E
		public string[] LastErrors { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001F247 File Offset: 0x0001D447
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0001F24F File Offset: 0x0001D44F
		public Dictionary<ConfigurationObjectType, SyncInfo> SyncInfoTable { get; set; }
	}
}
