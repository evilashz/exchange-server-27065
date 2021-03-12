using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A7 RID: 167
	[Flags]
	internal enum MailboxWrapperFlags
	{
		// Token: 0x0400033C RID: 828
		Source = 1,
		// Token: 0x0400033D RID: 829
		Target = 2,
		// Token: 0x0400033E RID: 830
		PST = 4,
		// Token: 0x0400033F RID: 831
		Archive = 16
	}
}
