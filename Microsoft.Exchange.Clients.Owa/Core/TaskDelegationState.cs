using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000262 RID: 610
	[Flags]
	public enum TaskDelegationState
	{
		// Token: 0x04000E02 RID: 3586
		None = 0,
		// Token: 0x04000E03 RID: 3587
		Unknown = 1,
		// Token: 0x04000E04 RID: 3588
		Accepted = 2,
		// Token: 0x04000E05 RID: 3589
		Declined = 3,
		// Token: 0x04000E06 RID: 3590
		Max = 4
	}
}
