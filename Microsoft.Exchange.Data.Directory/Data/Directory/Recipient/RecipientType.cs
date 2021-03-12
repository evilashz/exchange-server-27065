using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000216 RID: 534
	public enum RecipientType
	{
		// Token: 0x04000BE3 RID: 3043
		[LocDescription(DirectoryStrings.IDs.InvalidRecipientType)]
		Invalid,
		// Token: 0x04000BE4 RID: 3044
		[LocDescription(DirectoryStrings.IDs.UserRecipientType)]
		User,
		// Token: 0x04000BE5 RID: 3045
		[LocDescription(DirectoryStrings.IDs.MailboxUserRecipientType)]
		UserMailbox,
		// Token: 0x04000BE6 RID: 3046
		[LocDescription(DirectoryStrings.IDs.MailEnabledUserRecipientType)]
		MailUser,
		// Token: 0x04000BE7 RID: 3047
		[LocDescription(DirectoryStrings.IDs.ContactRecipientType)]
		Contact,
		// Token: 0x04000BE8 RID: 3048
		[LocDescription(DirectoryStrings.IDs.MailEnabledContactRecipientType)]
		MailContact,
		// Token: 0x04000BE9 RID: 3049
		[LocDescription(DirectoryStrings.IDs.GroupRecipientType)]
		Group,
		// Token: 0x04000BEA RID: 3050
		[LocDescription(DirectoryStrings.IDs.MailEnabledUniversalDistributionGroupRecipientType)]
		MailUniversalDistributionGroup,
		// Token: 0x04000BEB RID: 3051
		[LocDescription(DirectoryStrings.IDs.MailEnabledUniversalSecurityGroupRecipientType)]
		MailUniversalSecurityGroup,
		// Token: 0x04000BEC RID: 3052
		[LocDescription(DirectoryStrings.IDs.MailEnabledNonUniversalGroupRecipientType)]
		MailNonUniversalGroup,
		// Token: 0x04000BED RID: 3053
		[LocDescription(DirectoryStrings.IDs.DynamicDLRecipientType)]
		DynamicDistributionGroup,
		// Token: 0x04000BEE RID: 3054
		[LocDescription(DirectoryStrings.IDs.PublicFolderRecipientType)]
		PublicFolder,
		// Token: 0x04000BEF RID: 3055
		[LocDescription(DirectoryStrings.IDs.PublicDatabaseRecipientType)]
		PublicDatabase,
		// Token: 0x04000BF0 RID: 3056
		[LocDescription(DirectoryStrings.IDs.SystemAttendantMailboxRecipientType)]
		SystemAttendantMailbox,
		// Token: 0x04000BF1 RID: 3057
		[LocDescription(DirectoryStrings.IDs.SystemMailboxRecipientType)]
		SystemMailbox,
		// Token: 0x04000BF2 RID: 3058
		[LocDescription(DirectoryStrings.IDs.MicrosoftExchangeRecipientType)]
		MicrosoftExchange,
		// Token: 0x04000BF3 RID: 3059
		[LocDescription(DirectoryStrings.IDs.ComputerRecipientType)]
		Computer
	}
}
