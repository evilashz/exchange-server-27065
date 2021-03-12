using System;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000006 RID: 6
	internal interface IServerLocator
	{
		// Token: 0x0600000E RID: 14
		ServerLocatorReturn LocateServer(IRoutingKey[] keys, IRouteSelectorDiagnostics logger);
	}
}
