using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000263 RID: 611
	[Flags]
	public enum TaskType
	{
		// Token: 0x04000E08 RID: 3592
		NoMatch = 0,
		// Token: 0x04000E09 RID: 3593
		Undelegated = 1,
		// Token: 0x04000E0A RID: 3594
		Delegated = 2,
		// Token: 0x04000E0B RID: 3595
		DelegatedAccepted = 3,
		// Token: 0x04000E0C RID: 3596
		DelegatedDeclined = 4,
		// Token: 0x04000E0D RID: 3597
		Max = 5
	}
}
