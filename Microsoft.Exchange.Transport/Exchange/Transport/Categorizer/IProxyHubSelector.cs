using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B7 RID: 439
	internal interface IProxyHubSelector
	{
		// Token: 0x06001447 RID: 5191
		bool TrySelectHubServers(IReadOnlyMailItem mailItem, out IEnumerable<INextHopServer> hubServers);

		// Token: 0x06001448 RID: 5192
		bool TrySelectHubServersForClientProxy(MiniRecipient recipient, out IEnumerable<INextHopServer> hubServers);
	}
}
