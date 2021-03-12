using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C0 RID: 1472
	public enum CanRunRestoreState
	{
		// Token: 0x04002E12 RID: 11794
		[LocDescription(DirectoryStrings.IDs.CanRunRestoreState_Invalid)]
		Invalid,
		// Token: 0x04002E13 RID: 11795
		[LocDescription(DirectoryStrings.IDs.CanRunRestoreState_NotLocal)]
		NotLocal,
		// Token: 0x04002E14 RID: 11796
		[LocDescription(DirectoryStrings.IDs.CanRunRestoreState_Allowed)]
		Allowed
	}
}
