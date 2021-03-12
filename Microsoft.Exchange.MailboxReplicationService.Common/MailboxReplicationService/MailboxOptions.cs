using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000028 RID: 40
	[Flags]
	internal enum MailboxOptions
	{
		// Token: 0x04000160 RID: 352
		None = 0,
		// Token: 0x04000161 RID: 353
		IgnoreExtendedRuleFAIs = 1,
		// Token: 0x04000162 RID: 354
		Finalize = 2
	}
}
