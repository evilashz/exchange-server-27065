using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000070 RID: 112
	[Flags]
	public enum MapiObjectTrackingScope : uint
	{
		// Token: 0x0400022F RID: 559
		Session = 1U,
		// Token: 0x04000230 RID: 560
		Service = 2U,
		// Token: 0x04000231 RID: 561
		User = 4U,
		// Token: 0x04000232 RID: 562
		All = 7U
	}
}
