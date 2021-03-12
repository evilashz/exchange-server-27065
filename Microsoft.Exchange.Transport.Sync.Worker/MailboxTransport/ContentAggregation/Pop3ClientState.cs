using System;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001F0 RID: 496
	internal enum Pop3ClientState
	{
		// Token: 0x04000928 RID: 2344
		ProcessConnection,
		// Token: 0x04000929 RID: 2345
		ProcessCapaCommand,
		// Token: 0x0400092A RID: 2346
		ProcessTopCommand,
		// Token: 0x0400092B RID: 2347
		ProcessStlsCommand,
		// Token: 0x0400092C RID: 2348
		ProcessAuthNtlmCommand,
		// Token: 0x0400092D RID: 2349
		ProcessUserCommand,
		// Token: 0x0400092E RID: 2350
		ProcessPassCommand,
		// Token: 0x0400092F RID: 2351
		ProcessStatCommand,
		// Token: 0x04000930 RID: 2352
		ProcessUidlCommand,
		// Token: 0x04000931 RID: 2353
		ProcessListCommand,
		// Token: 0x04000932 RID: 2354
		ProcessRetrCommand,
		// Token: 0x04000933 RID: 2355
		ProcessDeleCommand,
		// Token: 0x04000934 RID: 2356
		ProcessQuitCommand,
		// Token: 0x04000935 RID: 2357
		ProcessEnd
	}
}
