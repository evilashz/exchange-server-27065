using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000082 RID: 130
	internal enum RecipientType : byte
	{
		// Token: 0x040001B3 RID: 435
		To = 1,
		// Token: 0x040001B4 RID: 436
		Cc,
		// Token: 0x040001B5 RID: 437
		Bcc,
		// Token: 0x040001B6 RID: 438
		P1 = 16,
		// Token: 0x040001B7 RID: 439
		SubmittedTo = 129,
		// Token: 0x040001B8 RID: 440
		SubmittedCc,
		// Token: 0x040001B9 RID: 441
		SubmittedBcc
	}
}
