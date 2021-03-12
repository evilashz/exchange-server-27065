using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001FB RID: 507
	[Flags]
	public enum MailboxRestoreType
	{
		// Token: 0x04000A8A RID: 2698
		None = 0,
		// Token: 0x04000A8B RID: 2699
		Disabled = 1,
		// Token: 0x04000A8C RID: 2700
		SoftDeleted = 2,
		// Token: 0x04000A8D RID: 2701
		Recovery = 4,
		// Token: 0x04000A8E RID: 2702
		SoftDeletedRecipient = 8,
		// Token: 0x04000A8F RID: 2703
		PublicFolderMailbox = 16
	}
}
