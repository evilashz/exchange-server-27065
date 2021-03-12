using System;

namespace Microsoft.Exchange.Assistants.Logging
{
	// Token: 0x020000C1 RID: 193
	internal enum MailboxSlaEventType
	{
		// Token: 0x04000375 RID: 885
		StartProcessingMailbox,
		// Token: 0x04000376 RID: 886
		EndProcessingMailbox,
		// Token: 0x04000377 RID: 887
		MailboxInteresting,
		// Token: 0x04000378 RID: 888
		MailboxNotInteresting,
		// Token: 0x04000379 RID: 889
		FilterMailbox,
		// Token: 0x0400037A RID: 890
		SucceedOpenMailboxStoreSession,
		// Token: 0x0400037B RID: 891
		FailedOpenMailboxStoreSession,
		// Token: 0x0400037C RID: 892
		ErrorProcessingMailbox
	}
}
