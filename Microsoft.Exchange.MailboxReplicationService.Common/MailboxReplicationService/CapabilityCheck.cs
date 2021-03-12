using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200014A RID: 330
	[Flags]
	internal enum CapabilityCheck
	{
		// Token: 0x0400065F RID: 1631
		MRS = 1,
		// Token: 0x04000660 RID: 1632
		OtherProvider = 2,
		// Token: 0x04000661 RID: 1633
		BothMRSAndOtherProvider = 3
	}
}
