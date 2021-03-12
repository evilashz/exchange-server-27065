using System;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000008 RID: 8
	internal interface ISymphonyProxy
	{
		// Token: 0x0600001D RID: 29
		WorkItemQueryResult QueryWorkItems(string groupName, string tenantTier, string workItemType, WorkItemStatus status, int pageSize, byte[] bookmark);

		// Token: 0x0600001E RID: 30
		WorkItemInfo[] QueryTenantWorkItems(Guid tenantId);

		// Token: 0x0600001F RID: 31
		void UpdateWorkItem(string workItemId, WorkItemStatusInfo status);

		// Token: 0x06000020 RID: 32
		void UpdateTenantReadiness(TenantReadiness[] data);
	}
}
