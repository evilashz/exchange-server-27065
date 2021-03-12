using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000035 RID: 53
	internal interface IAutoDiscoverClient
	{
		// Token: 0x060001B3 RID: 435
		List<AutoDiscoverResult> GetUserEwsEndpoints(IEnumerable<string> mailboxes);
	}
}
