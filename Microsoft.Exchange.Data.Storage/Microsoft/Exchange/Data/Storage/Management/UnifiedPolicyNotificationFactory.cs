using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A55 RID: 2645
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UnifiedPolicyNotificationFactory
	{
		// Token: 0x060060A4 RID: 24740 RVA: 0x00197518 File Offset: 0x00195718
		public static UnifiedPolicyNotificationBase Create(WorkItemBase workItem, ADObjectId mailboxOwnerId)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			if (workItem is SyncWorkItem)
			{
				return new UnifiedPolicySyncNotification(workItem as SyncWorkItem, mailboxOwnerId);
			}
			if (workItem is SyncStatusUpdateWorkitem)
			{
				return new UnifiedPolicyStatusNotification(workItem as SyncStatusUpdateWorkitem, mailboxOwnerId);
			}
			throw new NotImplementedException("not implemented yet");
		}
	}
}
