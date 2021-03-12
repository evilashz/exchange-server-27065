using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000004 RID: 4
	public interface IRoutingEntry : IEquatable<IRoutingEntry>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11
		IRoutingDestination Destination { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12
		IRoutingKey Key { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13
		long Timestamp { get; }
	}
}
