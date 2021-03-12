using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.SendMail
{
	// Token: 0x02000064 RID: 100
	[Flags]
	public enum SendMailStatus
	{
		// Token: 0x04000197 RID: 407
		Success = 0,
		// Token: 0x04000198 RID: 408
		InvalidMIME = 4203,
		// Token: 0x04000199 RID: 409
		SendQuotaExceeded = 4211,
		// Token: 0x0400019A RID: 410
		MessagePreviouslySent = 4214,
		// Token: 0x0400019B RID: 411
		MessageHasNoRecipient = 4215,
		// Token: 0x0400019C RID: 412
		MailSubmissionFailed = 376
	}
}
