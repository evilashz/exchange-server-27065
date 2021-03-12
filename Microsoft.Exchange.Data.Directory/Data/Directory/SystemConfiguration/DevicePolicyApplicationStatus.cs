using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004EF RID: 1263
	public enum DevicePolicyApplicationStatus
	{
		// Token: 0x04002632 RID: 9778
		[LocDescription(DirectoryStrings.IDs.Unknown)]
		Unknown,
		// Token: 0x04002633 RID: 9779
		[LocDescription(DirectoryStrings.IDs.NotApplied)]
		NotApplied,
		// Token: 0x04002634 RID: 9780
		[LocDescription(DirectoryStrings.IDs.AppliedInFull)]
		AppliedInFull,
		// Token: 0x04002635 RID: 9781
		[LocDescription(DirectoryStrings.IDs.PartiallyApplied)]
		PartiallyApplied,
		// Token: 0x04002636 RID: 9782
		[LocDescription(DirectoryStrings.IDs.ExternallyManaged)]
		ExternallyManaged
	}
}
