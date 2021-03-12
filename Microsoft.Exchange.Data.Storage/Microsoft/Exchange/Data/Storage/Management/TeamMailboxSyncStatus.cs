using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009D3 RID: 2515
	public enum TeamMailboxSyncStatus
	{
		// Token: 0x0400330C RID: 13068
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusNotAvailable)]
		NotAvailable,
		// Token: 0x0400330D RID: 13069
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusSucceeded)]
		Succeeded,
		// Token: 0x0400330E RID: 13070
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusFailed)]
		Failed,
		// Token: 0x0400330F RID: 13071
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusDocumentSyncFailureOnly)]
		DocumentSyncFailureOnly,
		// Token: 0x04003310 RID: 13072
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusMembershipSyncFailureOnly)]
		MembershipSyncFailureOnly,
		// Token: 0x04003311 RID: 13073
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusMaintenanceSyncFailureOnly)]
		MaintenanceSyncFailureOnly,
		// Token: 0x04003312 RID: 13074
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusDocumentAndMembershipSyncFailure)]
		DocumentAndMembershipSyncFailure,
		// Token: 0x04003313 RID: 13075
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusMembershipAndMaintenanceSyncFailure)]
		MembershipAndMaintenanceSyncFailure,
		// Token: 0x04003314 RID: 13076
		[LocDescription(ServerStrings.IDs.TeamMailboxSyncStatusDocumentAndMaintenanceSyncFailure)]
		DocumentAndMaintenanceSyncFailure
	}
}
