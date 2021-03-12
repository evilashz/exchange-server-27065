using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005BF RID: 1471
	public enum CanRunDefaultUpdateState
	{
		// Token: 0x04002E0D RID: 11789
		[LocDescription(DirectoryStrings.IDs.CanRunDefaultUpdateState_Invalid)]
		Invalid,
		// Token: 0x04002E0E RID: 11790
		[LocDescription(DirectoryStrings.IDs.CanRunDefaultUpdateState_NotLocal)]
		NotLocal,
		// Token: 0x04002E0F RID: 11791
		[LocDescription(DirectoryStrings.IDs.CanRunDefaultUpdateState_NotSuspended)]
		NotSuspended,
		// Token: 0x04002E10 RID: 11792
		[LocDescription(DirectoryStrings.IDs.CanRunDefaultUpdateState_Allowed)]
		Allowed
	}
}
