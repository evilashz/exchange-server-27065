using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000242 RID: 578
	public enum ArchiveState
	{
		// Token: 0x04000D87 RID: 3463
		[LocDescription(DirectoryStrings.IDs.ArchiveStateNone)]
		None,
		// Token: 0x04000D88 RID: 3464
		[LocDescription(DirectoryStrings.IDs.ArchiveStateLocal)]
		Local,
		// Token: 0x04000D89 RID: 3465
		[LocDescription(DirectoryStrings.IDs.ArchiveStateHostedProvisioned)]
		HostedProvisioned,
		// Token: 0x04000D8A RID: 3466
		[LocDescription(DirectoryStrings.IDs.ArchiveStateHostedPending)]
		HostedPending,
		// Token: 0x04000D8B RID: 3467
		[LocDescription(DirectoryStrings.IDs.ArchiveStateOnPremise)]
		OnPremise
	}
}
