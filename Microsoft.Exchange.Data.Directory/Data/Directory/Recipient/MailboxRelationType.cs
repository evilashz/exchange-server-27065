using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000218 RID: 536
	public enum MailboxRelationType
	{
		// Token: 0x04000C22 RID: 3106
		[LocDescription(DirectoryStrings.IDs.NoneMailboxRelationType)]
		None,
		// Token: 0x04000C23 RID: 3107
		[LocDescription(DirectoryStrings.IDs.PrimaryMailboxRelationType)]
		Primary,
		// Token: 0x04000C24 RID: 3108
		[LocDescription(DirectoryStrings.IDs.SecondaryMailboxRelationType)]
		Secondary
	}
}
