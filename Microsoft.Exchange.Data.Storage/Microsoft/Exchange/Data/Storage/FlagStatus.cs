using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001F0 RID: 496
	public enum FlagStatus
	{
		// Token: 0x04000DD9 RID: 3545
		[LocDescription(ServerStrings.IDs.InboxRuleFlagStatusNotFlagged)]
		NotFlagged,
		// Token: 0x04000DDA RID: 3546
		[LocDescription(ServerStrings.IDs.InboxRuleFlagStatusComplete)]
		Complete,
		// Token: 0x04000DDB RID: 3547
		[LocDescription(ServerStrings.IDs.InboxRuleFlagStatusFlagged)]
		Flagged
	}
}
