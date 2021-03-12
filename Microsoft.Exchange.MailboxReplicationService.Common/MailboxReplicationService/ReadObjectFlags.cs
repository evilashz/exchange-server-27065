using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000161 RID: 353
	[Flags]
	internal enum ReadObjectFlags
	{
		// Token: 0x0400073C RID: 1852
		None = 0,
		// Token: 0x0400073D RID: 1853
		DontThrowOnCorruptData = 1,
		// Token: 0x0400073E RID: 1854
		Refresh = 2,
		// Token: 0x0400073F RID: 1855
		LastChunkOnly = 4
	}
}
