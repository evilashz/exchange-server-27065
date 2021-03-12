using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A2E RID: 2606
	[Serializable]
	public enum MigrationMailboxPermission
	{
		// Token: 0x04003635 RID: 13877
		[LocDescription(ServerStrings.IDs.MigrationMailboxPermissionAdmin)]
		Admin,
		// Token: 0x04003636 RID: 13878
		[LocDescription(ServerStrings.IDs.MigrationMailboxPermissionFullAccess)]
		FullAccess
	}
}
