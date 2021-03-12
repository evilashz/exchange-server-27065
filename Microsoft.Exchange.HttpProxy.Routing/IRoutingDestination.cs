using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000002 RID: 2
	public interface IRoutingDestination : IEquatable<IRoutingDestination>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		RoutingItemType RoutingItemType { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		string Value { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		IList<string> Properties { get; }
	}
}
