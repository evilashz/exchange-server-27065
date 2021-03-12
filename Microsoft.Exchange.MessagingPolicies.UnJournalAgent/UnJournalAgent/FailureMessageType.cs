using System;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x0200001A RID: 26
	internal enum FailureMessageType
	{
		// Token: 0x040000BF RID: 191
		Unknown,
		// Token: 0x040000C0 RID: 192
		DefectiveJournalNoRecipientsMsg,
		// Token: 0x040000C1 RID: 193
		DefectiveJournalWithRecipientsMsg,
		// Token: 0x040000C2 RID: 194
		UnProvisionedRecipientsMsg,
		// Token: 0x040000C3 RID: 195
		NoRecipientsResolvedMsg,
		// Token: 0x040000C4 RID: 196
		UnexpectedJournalMessageFormatMsg,
		// Token: 0x040000C5 RID: 197
		PermanentErrorOther
	}
}
