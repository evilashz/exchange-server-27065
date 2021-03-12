using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200070E RID: 1806
	public enum DomainState
	{
		// Token: 0x04003907 RID: 14599
		[LocDescription(DirectoryStrings.IDs.DomainStateUnknown)]
		Unknown,
		// Token: 0x04003908 RID: 14600
		[LocDescription(DirectoryStrings.IDs.DomainStateCustomProvision)]
		CustomProvision,
		// Token: 0x04003909 RID: 14601
		[LocDescription(DirectoryStrings.IDs.DomainStatePendingActivation)]
		PendingActivation,
		// Token: 0x0400390A RID: 14602
		[LocDescription(DirectoryStrings.IDs.DomainStatePendingRelease)]
		PendingRelease,
		// Token: 0x0400390B RID: 14603
		[LocDescription(DirectoryStrings.IDs.DomainStateActive)]
		Active
	}
}
