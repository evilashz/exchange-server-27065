using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000006 RID: 6
	public interface IRoutingLookup
	{
		// Token: 0x06000010 RID: 16
		IRoutingEntry GetRoutingEntry(IRoutingKey routingKey, IRoutingDiagnostics diagnostics);
	}
}
