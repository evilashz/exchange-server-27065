using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000F RID: 15
	[Flags]
	internal enum ExecutionFlags
	{
		// Token: 0x0400003A RID: 58
		Default = 0,
		// Token: 0x0400003B RID: 59
		ThrottlingNotRequired = 1,
		// Token: 0x0400003C RID: 60
		NoLock = 2
	}
}
