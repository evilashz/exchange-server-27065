using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000005 RID: 5
	public interface IRoutingKey : IEquatable<IRoutingKey>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14
		RoutingItemType RoutingItemType { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15
		string Value { get; }
	}
}
