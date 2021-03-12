using System;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x0200000A RID: 10
	[Flags]
	internal enum RunInExchangeMode
	{
		// Token: 0x04000026 RID: 38
		None = 0,
		// Token: 0x04000027 RID: 39
		Enterprise = 1,
		// Token: 0x04000028 RID: 40
		ExchangeDatacenter = 2,
		// Token: 0x04000029 RID: 41
		DatacenterDedicated = 4
	}
}
