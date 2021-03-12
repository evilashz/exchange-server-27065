using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003A RID: 58
	[Flags]
	public enum MRSErrorFlags
	{
		// Token: 0x0400022F RID: 559
		None = 0,
		// Token: 0x04000230 RID: 560
		Source = 1,
		// Token: 0x04000231 RID: 561
		Target = 2
	}
}
