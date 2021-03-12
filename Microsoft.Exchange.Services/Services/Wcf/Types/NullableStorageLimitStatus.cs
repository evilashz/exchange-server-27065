using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF2 RID: 2802
	public enum NullableStorageLimitStatus
	{
		// Token: 0x04002CA7 RID: 11431
		NullStorageLimitStatus = -1,
		// Token: 0x04002CA8 RID: 11432
		BelowLimit = 1,
		// Token: 0x04002CA9 RID: 11433
		IssueWarning,
		// Token: 0x04002CAA RID: 11434
		ProhibitSend = 4,
		// Token: 0x04002CAB RID: 11435
		NoChecking = 8,
		// Token: 0x04002CAC RID: 11436
		MailboxDisabled = 16
	}
}
