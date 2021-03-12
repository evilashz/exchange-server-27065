using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x02000025 RID: 37
	internal class SuccessfulServerRoutingEntry : ServerRoutingEntry
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000033BA File Offset: 0x000015BA
		public SuccessfulServerRoutingEntry(ServerRoutingKey key, ServerRoutingDestination destination, long timestamp) : base(key, timestamp)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.destination = destination;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000033DF File Offset: 0x000015DF
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly ServerRoutingDestination destination;
	}
}
