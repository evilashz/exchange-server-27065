using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000026 RID: 38
	[Flags]
	internal enum MailboxConnectFlags
	{
		// Token: 0x04000155 RID: 341
		None = 0,
		// Token: 0x04000156 RID: 342
		DoNotOpenMapiSession = 1,
		// Token: 0x04000157 RID: 343
		ValidateOnly = 2,
		// Token: 0x04000158 RID: 344
		NonMrsLogon = 4,
		// Token: 0x04000159 RID: 345
		PublicFolderHierarchyReplication = 8,
		// Token: 0x0400015A RID: 346
		HighPriority = 16,
		// Token: 0x0400015B RID: 347
		AllowRestoreFromConnectedMailbox = 32
	}
}
