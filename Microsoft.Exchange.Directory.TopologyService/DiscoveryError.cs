using System;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000006 RID: 6
	internal enum DiscoveryError
	{
		// Token: 0x04000010 RID: 16
		None,
		// Token: 0x04000011 RID: 17
		NoServersInForest,
		// Token: 0x04000012 RID: 18
		NoSuitableServersInSite,
		// Token: 0x04000013 RID: 19
		NoSuitableServersInSiteAndConnectedSites,
		// Token: 0x04000014 RID: 20
		NoRequiredSuitableServersInSiteAndConnectedSites
	}
}
