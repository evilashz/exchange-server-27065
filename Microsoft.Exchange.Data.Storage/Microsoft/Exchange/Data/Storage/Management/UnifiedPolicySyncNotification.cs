using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A57 RID: 2647
	[Serializable]
	public class UnifiedPolicySyncNotification : UnifiedPolicyNotificationBase
	{
		// Token: 0x060060A9 RID: 24745 RVA: 0x001975B5 File Offset: 0x001957B5
		public UnifiedPolicySyncNotification()
		{
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x001975C0 File Offset: 0x001957C0
		public UnifiedPolicySyncNotification(SyncWorkItem workItem, ADObjectId mailboxOwnerId) : base(workItem, mailboxOwnerId)
		{
			if (workItem.WorkItemInfo != null && workItem.WorkItemInfo.Count > 0)
			{
				this.syncChangeInfos = new MultiValuedProperty<string>();
				foreach (List<SyncChangeInfo> list in workItem.WorkItemInfo.Values)
				{
					foreach (SyncChangeInfo syncChangeInfo in list)
					{
						this.syncChangeInfos.Add(syncChangeInfo.ToString());
					}
				}
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x00197684 File Offset: 0x00195884
		[Parameter]
		public string SyncSvcUrl
		{
			get
			{
				return ((SyncWorkItem)this.workItem).SyncSvcUrl;
			}
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x060060AC RID: 24748 RVA: 0x00197696 File Offset: 0x00195896
		[Parameter]
		public bool FullSync
		{
			get
			{
				return ((SyncWorkItem)this.workItem).FullSyncForTenant;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x001976A8 File Offset: 0x001958A8
		[Parameter]
		public bool SyncNow
		{
			get
			{
				return ((SyncWorkItem)this.workItem).ProcessNow;
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x001976BA File Offset: 0x001958BA
		[Parameter]
		public MultiValuedProperty<string> SyncChangeInfos
		{
			get
			{
				return this.syncChangeInfos;
			}
		}

		// Token: 0x040036FB RID: 14075
		private readonly MultiValuedProperty<string> syncChangeInfos;
	}
}
