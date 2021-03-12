using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x0200001D RID: 29
	internal class FailedDatabaseGuidRoutingEntry : DatabaseGuidRoutingEntry
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003209 File Offset: 0x00001409
		public FailedDatabaseGuidRoutingEntry(DatabaseGuidRoutingKey key, ErrorRoutingDestination destination, long timestamp) : base(key, timestamp)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.destination = destination;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000322E File Offset: 0x0000142E
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x0400002F RID: 47
		private readonly ErrorRoutingDestination destination;
	}
}
