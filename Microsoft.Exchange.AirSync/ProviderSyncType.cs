using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000104 RID: 260
	[Flags]
	internal enum ProviderSyncType
	{
		// Token: 0x040009A5 RID: 2469
		None = 0,
		// Token: 0x040009A6 RID: 2470
		N = 1,
		// Token: 0x040009A7 RID: 2471
		IQ = 2,
		// Token: 0x040009A8 RID: 2472
		ICS = 4,
		// Token: 0x040009A9 RID: 2473
		FCS = 8
	}
}
