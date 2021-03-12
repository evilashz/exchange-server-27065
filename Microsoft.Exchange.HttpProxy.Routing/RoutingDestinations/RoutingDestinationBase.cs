using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations
{
	// Token: 0x02000016 RID: 22
	public abstract class RoutingDestinationBase : IRoutingDestination, IEquatable<IRoutingDestination>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82
		public abstract RoutingItemType RoutingItemType { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83
		public abstract string Value { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000054 RID: 84
		public abstract IList<string> Properties { get; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public static bool operator ==(RoutingDestinationBase firstRoutingDestination, RoutingDestinationBase secondRoutingDestination)
		{
			return object.ReferenceEquals(firstRoutingDestination, secondRoutingDestination) || (!object.ReferenceEquals(firstRoutingDestination, null) && !object.ReferenceEquals(secondRoutingDestination, null) && (firstRoutingDestination.RoutingItemType == secondRoutingDestination.RoutingItemType && firstRoutingDestination.Value.Equals(secondRoutingDestination.Value, StringComparison.OrdinalIgnoreCase)) && firstRoutingDestination.Properties.SequenceEqual(secondRoutingDestination.Properties));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E05 File Offset: 0x00001005
		public static bool operator !=(RoutingDestinationBase firstRoutingDestination, RoutingDestinationBase secondRoutingDestination)
		{
			return !(firstRoutingDestination == secondRoutingDestination);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E11 File Offset: 0x00001011
		public bool Equals(IRoutingDestination other)
		{
			return other as RoutingDestinationBase == this;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E1F File Offset: 0x0000101F
		public override bool Equals(object obj)
		{
			return this.Equals((IRoutingDestination)obj);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E2D File Offset: 0x0000102D
		public override int GetHashCode()
		{
			return this.RoutingItemType.GetHashCode() ^ this.Value.GetHashCode();
		}
	}
}
