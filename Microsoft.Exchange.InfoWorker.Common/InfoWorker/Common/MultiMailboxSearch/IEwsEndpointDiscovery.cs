using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E3 RID: 483
	internal interface IEwsEndpointDiscovery
	{
		// Token: 0x06000C7B RID: 3195
		Dictionary<GroupId, List<MailboxInfo>> FindEwsEndpoints(out long localDiscoveryTime, out long autoDiscoveryTime);
	}
}
