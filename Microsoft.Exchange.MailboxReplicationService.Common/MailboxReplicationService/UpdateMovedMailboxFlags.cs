using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000025 RID: 37
	[Flags]
	internal enum UpdateMovedMailboxFlags
	{
		// Token: 0x04000150 RID: 336
		None = 0,
		// Token: 0x04000151 RID: 337
		SkipMailboxReleaseCheck = 1,
		// Token: 0x04000152 RID: 338
		SkipProvisioningCheck = 2,
		// Token: 0x04000153 RID: 339
		MakeExoPrimary = 4
	}
}
