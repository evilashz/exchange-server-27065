using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001FD RID: 509
	public enum Sensitivity
	{
		// Token: 0x04000E60 RID: 3680
		[LocDescription(ServerStrings.IDs.InboxRuleSensitivityNormal)]
		Normal,
		// Token: 0x04000E61 RID: 3681
		[LocDescription(ServerStrings.IDs.InboxRuleSensitivityPersonal)]
		Personal,
		// Token: 0x04000E62 RID: 3682
		[LocDescription(ServerStrings.IDs.InboxRuleSensitivityPrivate)]
		Private,
		// Token: 0x04000E63 RID: 3683
		[LocDescription(ServerStrings.IDs.InboxRuleSensitivityCompanyConfidential)]
		CompanyConfidential
	}
}
