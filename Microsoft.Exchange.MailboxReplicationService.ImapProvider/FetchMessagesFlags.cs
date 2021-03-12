using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	[Flags]
	internal enum FetchMessagesFlags
	{
		// Token: 0x04000006 RID: 6
		None = 0,
		// Token: 0x04000007 RID: 7
		FetchByUid = 1,
		// Token: 0x04000008 RID: 8
		FetchBySeqNum = 2,
		// Token: 0x04000009 RID: 9
		IncludeExtendedData = 4
	}
}
