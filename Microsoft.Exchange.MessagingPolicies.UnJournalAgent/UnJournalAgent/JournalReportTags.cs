using System;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000015 RID: 21
	[Flags]
	internal enum JournalReportTags
	{
		// Token: 0x0400009B RID: 155
		None = 0,
		// Token: 0x0400009C RID: 156
		Sender = 1,
		// Token: 0x0400009D RID: 157
		MessageId = 2,
		// Token: 0x0400009E RID: 158
		Recipients = 4
	}
}
