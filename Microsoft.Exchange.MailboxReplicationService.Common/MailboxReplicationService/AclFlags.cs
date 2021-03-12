using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002E RID: 46
	[Flags]
	internal enum AclFlags
	{
		// Token: 0x0400018D RID: 397
		None = 0,
		// Token: 0x0400018E RID: 398
		FreeBusyAcl = 1,
		// Token: 0x0400018F RID: 399
		FolderAcl = 2
	}
}
