using System;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006B6 RID: 1718
	internal enum ExchangeTopologyScope
	{
		// Token: 0x04003630 RID: 13872
		Complete,
		// Token: 0x04003631 RID: 13873
		ServerAndSiteTopology,
		// Token: 0x04003632 RID: 13874
		ADAndExchangeServerAndSiteTopology,
		// Token: 0x04003633 RID: 13875
		ADAndExchangeServerAndSiteAndVirtualDirectoryTopology,
		// Token: 0x04003634 RID: 13876
		Max = 3
	}
}
