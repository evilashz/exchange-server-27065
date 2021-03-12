using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000264 RID: 612
	[Flags]
	public enum TaskLastUpdateType
	{
		// Token: 0x04000E0F RID: 3599
		None = 0,
		// Token: 0x04000E10 RID: 3600
		Accepted = 1,
		// Token: 0x04000E11 RID: 3601
		Declined = 2,
		// Token: 0x04000E12 RID: 3602
		Updated = 3,
		// Token: 0x04000E13 RID: 3603
		DueDateChanged = 4,
		// Token: 0x04000E14 RID: 3604
		Assigned = 5,
		// Token: 0x04000E15 RID: 3605
		Max = 6
	}
}
