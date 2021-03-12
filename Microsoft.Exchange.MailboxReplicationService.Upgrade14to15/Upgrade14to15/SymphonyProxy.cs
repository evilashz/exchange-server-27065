using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SymphonyProxy : ISymphonyProxy
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000620D File Offset: 0x0000440D
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00006215 File Offset: 0x00004415
		public Uri WorkloadUri { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000621E File Offset: 0x0000441E
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x00006226 File Offset: 0x00004426
		public X509Certificate2 Cert { get; set; }

		// Token: 0x06000418 RID: 1048 RVA: 0x000062B4 File Offset: 0x000044B4
		public WorkItemQueryResult QueryWorkItems(string groupName, string tenantTier, string workItemType, WorkItemStatus status, int pageSize, byte[] bookmark)
		{
			WorkItemQueryResult result2;
			using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(this.WorkloadUri, this.Cert))
			{
				WorkItemQueryResult result = null;
				workloadClient.CallSymphony(delegate
				{
					result = workloadClient.Proxy.QueryWorkItems(groupName, tenantTier, workItemType, status, pageSize, bookmark);
				}, this.WorkloadUri.ToString());
				result2 = result;
			}
			return result2;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000063BC File Offset: 0x000045BC
		public WorkItemInfo[] QueryTenantWorkItems(Guid tenantId)
		{
			WorkItemInfo[] result2;
			using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(this.WorkloadUri, this.Cert))
			{
				WorkItemInfo[] result = null;
				workloadClient.CallSymphony(delegate
				{
					result = workloadClient.Proxy.QueryTenantWorkItems(tenantId);
				}, this.WorkloadUri.ToString());
				result2 = result;
			}
			return result2;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00006498 File Offset: 0x00004698
		public void UpdateWorkItem(string workItemId, WorkItemStatusInfo status)
		{
			SymphonyProxy.<>c__DisplayClassf CS$<>8__locals1 = new SymphonyProxy.<>c__DisplayClassf();
			CS$<>8__locals1.workItemId = workItemId;
			CS$<>8__locals1.status = status;
			using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(this.WorkloadUri, this.Cert))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateWorkItem(CS$<>8__locals1.workItemId, CS$<>8__locals1.status);
				}, this.WorkloadUri.ToString());
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000655C File Offset: 0x0000475C
		public void UpdateTenantReadiness(TenantReadiness[] data)
		{
			SymphonyProxy.<>c__DisplayClass15 CS$<>8__locals1 = new SymphonyProxy.<>c__DisplayClass15();
			CS$<>8__locals1.data = data;
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(this.WorkloadUri, this.Cert))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateTenantReadiness(CS$<>8__locals1.data);
				}, this.WorkloadUri.ToString());
			}
		}
	}
}
