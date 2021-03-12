using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200021A RID: 538
	[Flags]
	public enum WellKnownRecipientType
	{
		// Token: 0x04000C4A RID: 3146
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeNone)]
		None = 0,
		// Token: 0x04000C4B RID: 3147
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeMailboxUsers)]
		MailboxUsers = 1,
		// Token: 0x04000C4C RID: 3148
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeResources)]
		Resources = 2,
		// Token: 0x04000C4D RID: 3149
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeMailContacts)]
		MailContacts = 4,
		// Token: 0x04000C4E RID: 3150
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeMailGroups)]
		MailGroups = 8,
		// Token: 0x04000C4F RID: 3151
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeMailUsers)]
		MailUsers = 16,
		// Token: 0x04000C50 RID: 3152
		[LocDescription(DirectoryStrings.IDs.WellKnownRecipientTypeAllRecipients)]
		AllRecipients = -1
	}
}
