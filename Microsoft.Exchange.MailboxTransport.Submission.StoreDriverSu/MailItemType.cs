using System;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000024 RID: 36
	[Flags]
	internal enum MailItemType
	{
		// Token: 0x0400009C RID: 156
		ActualMessage = 1,
		// Token: 0x0400009D RID: 157
		MainMessage = 2,
		// Token: 0x0400009E RID: 158
		OtherMessage = 4
	}
}
