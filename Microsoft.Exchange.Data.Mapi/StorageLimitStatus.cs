using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000010 RID: 16
	public enum StorageLimitStatus
	{
		// Token: 0x0400002B RID: 43
		BelowLimit = 1,
		// Token: 0x0400002C RID: 44
		IssueWarning,
		// Token: 0x0400002D RID: 45
		ProhibitSend = 4,
		// Token: 0x0400002E RID: 46
		NoChecking = 8,
		// Token: 0x0400002F RID: 47
		MailboxDisabled = 16
	}
}
