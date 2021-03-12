using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x02000021 RID: 33
	internal class FailedServerRoutingEntry : ServerRoutingEntry
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000032D1 File Offset: 0x000014D1
		public FailedServerRoutingEntry(ServerRoutingKey key, ErrorRoutingDestination destination, long timestamp) : base(key, timestamp)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.destination = destination;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000032F6 File Offset: 0x000014F6
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x04000035 RID: 53
		private readonly ErrorRoutingDestination destination;
	}
}
