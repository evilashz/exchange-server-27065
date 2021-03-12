using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200011D RID: 285
	internal enum EvaluationResults
	{
		// Token: 0x04000724 RID: 1828
		Ignore,
		// Token: 0x04000725 RID: 1829
		Message,
		// Token: 0x04000726 RID: 1830
		Delete,
		// Token: 0x04000727 RID: 1831
		Accept,
		// Token: 0x04000728 RID: 1832
		Decline,
		// Token: 0x04000729 RID: 1833
		Tentative,
		// Token: 0x0400072A RID: 1834
		AcceptSomeDeclineSome,
		// Token: 0x0400072B RID: 1835
		Cancel,
		// Token: 0x0400072C RID: 1836
		IgnoreDelegate,
		// Token: 0x0400072D RID: 1837
		IgnoreOrganizer,
		// Token: 0x0400072E RID: 1838
		None
	}
}
