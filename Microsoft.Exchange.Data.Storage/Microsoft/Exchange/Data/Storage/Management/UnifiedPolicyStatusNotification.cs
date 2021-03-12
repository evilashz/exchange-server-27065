using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A56 RID: 2646
	[Serializable]
	public sealed class UnifiedPolicyStatusNotification : UnifiedPolicyNotificationBase
	{
		// Token: 0x060060A5 RID: 24741 RVA: 0x00197575 File Offset: 0x00195775
		public UnifiedPolicyStatusNotification(SyncStatusUpdateWorkitem workItem, ADObjectId mailboxOwnerId) : base(workItem, mailboxOwnerId)
		{
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x060060A6 RID: 24742 RVA: 0x0019757F File Offset: 0x0019577F
		[Parameter]
		public string StatusUpdateSvcUrl
		{
			get
			{
				return ((SyncStatusUpdateWorkitem)this.workItem).StatusUpdateSvcUrl;
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x060060A7 RID: 24743 RVA: 0x00197591 File Offset: 0x00195791
		[Parameter]
		public IEnumerable<UnifiedPolicyStatus> StatusUpdates
		{
			get
			{
				return ((SyncStatusUpdateWorkitem)this.workItem).StatusUpdates;
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x060060A8 RID: 24744 RVA: 0x001975A3 File Offset: 0x001957A3
		[Parameter]
		public bool SyncNow
		{
			get
			{
				return ((SyncStatusUpdateWorkitem)this.workItem).ProcessNow;
			}
		}
	}
}
