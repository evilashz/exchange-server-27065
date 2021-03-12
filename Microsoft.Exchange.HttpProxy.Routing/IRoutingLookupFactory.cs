using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000007 RID: 7
	public interface IRoutingLookupFactory
	{
		// Token: 0x06000011 RID: 17
		IRoutingLookup GetLookupForType(RoutingItemType routingEntryType);
	}
}
