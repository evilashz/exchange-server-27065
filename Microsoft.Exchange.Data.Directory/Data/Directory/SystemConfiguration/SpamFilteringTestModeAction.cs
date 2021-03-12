using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200046F RID: 1135
	public enum SpamFilteringTestModeAction
	{
		// Token: 0x040022B1 RID: 8881
		[LocDescription(DirectoryStrings.IDs.SpamFilteringTestActionNone)]
		None,
		// Token: 0x040022B2 RID: 8882
		[LocDescription(DirectoryStrings.IDs.SpamFilteringTestActionAddXHeader)]
		AddXHeader,
		// Token: 0x040022B3 RID: 8883
		[LocDescription(DirectoryStrings.IDs.SpamFilteringTestActionBccMessage)]
		BccMessage
	}
}
