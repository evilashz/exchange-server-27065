using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200044E RID: 1102
	public enum RecipientWriteScopeType
	{
		// Token: 0x040021F6 RID: 8694
		None,
		// Token: 0x040021F7 RID: 8695
		NotApplicable,
		// Token: 0x040021F8 RID: 8696
		Organization,
		// Token: 0x040021F9 RID: 8697
		MyGAL,
		// Token: 0x040021FA RID: 8698
		Self,
		// Token: 0x040021FB RID: 8699
		MyDirectReports,
		// Token: 0x040021FC RID: 8700
		OU,
		// Token: 0x040021FD RID: 8701
		CustomRecipientScope,
		// Token: 0x040021FE RID: 8702
		MyDistributionGroups,
		// Token: 0x040021FF RID: 8703
		MyExecutive,
		// Token: 0x04002200 RID: 8704
		ExclusiveRecipientScope = 13,
		// Token: 0x04002201 RID: 8705
		MailboxICanDelegate = 15
	}
}
