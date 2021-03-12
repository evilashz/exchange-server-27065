using System;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000005 RID: 5
	[Flags]
	internal enum DiscoveryFlags
	{
		// Token: 0x0400000B RID: 11
		None = 0,
		// Token: 0x0400000C RID: 12
		InitialDiscovery = 1,
		// Token: 0x0400000D RID: 13
		UrgentDiscovery = 2,
		// Token: 0x0400000E RID: 14
		FullDiscovery = 4
	}
}
