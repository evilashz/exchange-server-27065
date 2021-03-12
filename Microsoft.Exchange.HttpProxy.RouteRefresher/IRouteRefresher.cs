using System;
using Microsoft.Exchange.HttpProxy.RouteSelector;

namespace Microsoft.Exchange.HttpProxy.RouteRefresher
{
	// Token: 0x02000002 RID: 2
	internal interface IRouteRefresher
	{
		// Token: 0x06000001 RID: 1
		void Initialize(IRouteRefresherDiagnostics diagnostics, ISharedCacheClient anchorMailboxCacheClient, ISharedCacheClient mailboxServerCacheClient);

		// Token: 0x06000002 RID: 2
		void ProcessRoutingUpdates(string headerValue);
	}
}
