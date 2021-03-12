using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000041 RID: 65
	[Flags]
	internal enum MsgRecFlags
	{
		// Token: 0x0400026F RID: 623
		None = 0,
		// Token: 0x04000270 RID: 624
		Deleted = 1,
		// Token: 0x04000271 RID: 625
		Regular = 2,
		// Token: 0x04000272 RID: 626
		Associated = 4,
		// Token: 0x04000273 RID: 627
		New = 8,
		// Token: 0x04000274 RID: 628
		AllLegacy = 7
	}
}
