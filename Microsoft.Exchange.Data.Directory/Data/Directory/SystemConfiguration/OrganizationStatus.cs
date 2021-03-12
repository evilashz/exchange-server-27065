using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002CA RID: 714
	public enum OrganizationStatus
	{
		// Token: 0x040013B9 RID: 5049
		Invalid,
		// Token: 0x040013BA RID: 5050
		Active,
		// Token: 0x040013BB RID: 5051
		PendingCompletion,
		// Token: 0x040013BC RID: 5052
		PendingRemoval,
		// Token: 0x040013BD RID: 5053
		PendingAcceptedDomainAddition,
		// Token: 0x040013BE RID: 5054
		PendingAcceptedDomainRemoval,
		// Token: 0x040013BF RID: 5055
		ReadyForRemoval = 8,
		// Token: 0x040013C0 RID: 5056
		ReadOnly = 10,
		// Token: 0x040013C1 RID: 5057
		PendingArrival,
		// Token: 0x040013C2 RID: 5058
		Suspended,
		// Token: 0x040013C3 RID: 5059
		LockedOut,
		// Token: 0x040013C4 RID: 5060
		Retired,
		// Token: 0x040013C5 RID: 5061
		SoftDeleted
	}
}
