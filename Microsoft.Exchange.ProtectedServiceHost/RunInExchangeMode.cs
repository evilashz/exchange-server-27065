using System;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000004 RID: 4
	[Flags]
	internal enum RunInExchangeMode
	{
		// Token: 0x04000009 RID: 9
		None = 0,
		// Token: 0x0400000A RID: 10
		Enterprise = 1,
		// Token: 0x0400000B RID: 11
		ExchangeDatacenter = 2,
		// Token: 0x0400000C RID: 12
		DatacenterDedicated = 4
	}
}
