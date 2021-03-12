using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000585 RID: 1413
	public enum DatabaseMountDialOverride
	{
		// Token: 0x04002CB4 RID: 11444
		[LocDescription(DirectoryStrings.IDs.MountDialOverrideNone)]
		None = -1,
		// Token: 0x04002CB5 RID: 11445
		[LocDescription(DirectoryStrings.IDs.MountDialOverrideLossless)]
		Lossless,
		// Token: 0x04002CB6 RID: 11446
		[LocDescription(DirectoryStrings.IDs.MountDialOverrideGoodAvailability)]
		GoodAvailability = 6,
		// Token: 0x04002CB7 RID: 11447
		[LocDescription(DirectoryStrings.IDs.MountDialOverrideBestAvailability)]
		BestAvailability = 12,
		// Token: 0x04002CB8 RID: 11448
		[LocDescription(DirectoryStrings.IDs.MountDialOverrideBestEffort)]
		BestEffort = 10
	}
}
