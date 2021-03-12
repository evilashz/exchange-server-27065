using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001FC RID: 508
	public enum Importance
	{
		// Token: 0x04000E5C RID: 3676
		[LocDescription(ServerStrings.IDs.InboxRuleImportanceLow)]
		Low,
		// Token: 0x04000E5D RID: 3677
		[LocDescription(ServerStrings.IDs.InboxRuleImportanceNormal)]
		Normal,
		// Token: 0x04000E5E RID: 3678
		[LocDescription(ServerStrings.IDs.InboxRuleImportanceHigh)]
		High
	}
}
