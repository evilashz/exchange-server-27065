using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A6 RID: 166
	public enum MailRecipientType
	{
		// Token: 0x04000279 RID: 633
		[LocDescription(DataStrings.IDs.MailRecipientTypeUnknown)]
		Unknown,
		// Token: 0x0400027A RID: 634
		[LocDescription(DataStrings.IDs.MailRecipientTypeDistributionGroup)]
		DistributionGroup,
		// Token: 0x0400027B RID: 635
		[LocDescription(DataStrings.IDs.MailRecipientTypeExternal)]
		External,
		// Token: 0x0400027C RID: 636
		[LocDescription(DataStrings.IDs.MailRecipientTypeMailbox)]
		Mailbox
	}
}
