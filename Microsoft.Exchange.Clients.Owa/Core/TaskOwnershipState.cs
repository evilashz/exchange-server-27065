using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000261 RID: 609
	[Flags]
	public enum TaskOwnershipState
	{
		// Token: 0x04000DFD RID: 3581
		New = 0,
		// Token: 0x04000DFE RID: 3582
		Delegate = 1,
		// Token: 0x04000DFF RID: 3583
		Me = 2,
		// Token: 0x04000E00 RID: 3584
		Max = 3
	}
}
