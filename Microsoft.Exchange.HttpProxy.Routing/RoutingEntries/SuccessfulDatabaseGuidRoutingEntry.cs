using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x02000023 RID: 35
	internal class SuccessfulDatabaseGuidRoutingEntry : DatabaseGuidRoutingEntry
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000334F File Offset: 0x0000154F
		public SuccessfulDatabaseGuidRoutingEntry(DatabaseGuidRoutingKey key, ServerRoutingDestination destination, long timestamp) : base(key, timestamp)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.destination = destination;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003374 File Offset: 0x00001574
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x04000039 RID: 57
		private readonly ServerRoutingDestination destination;
	}
}
