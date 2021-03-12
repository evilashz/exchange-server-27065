using System;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000005 RID: 5
	internal interface IServerLocatorFactory
	{
		// Token: 0x0600000D RID: 13
		IServerLocator GetServerLocator(ProtocolType protocolType);
	}
}
