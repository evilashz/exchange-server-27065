using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000476 RID: 1142
	[Flags]
	public enum SpamFilteringTestModeActions
	{
		// Token: 0x04002346 RID: 9030
		[LocDescription(DirectoryStrings.IDs.SpamFilteringTestActionNone)]
		None = 0,
		// Token: 0x04002347 RID: 9031
		[LocDescription(DirectoryStrings.IDs.SpamFilteringTestActionAddXHeader)]
		AddXHeader = 1,
		// Token: 0x04002348 RID: 9032
		[LocDescription(DirectoryStrings.IDs.SpamFilteringTestActionBccMessage)]
		BccMessage = 2
	}
}
