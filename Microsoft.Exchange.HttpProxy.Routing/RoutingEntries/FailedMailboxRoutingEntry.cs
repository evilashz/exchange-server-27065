using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x0200001F RID: 31
	internal class FailedMailboxRoutingEntry : MailboxRoutingEntry
	{
		// Token: 0x06000087 RID: 135 RVA: 0x0000326A File Offset: 0x0000146A
		public FailedMailboxRoutingEntry(IRoutingKey key, ErrorRoutingDestination destination, long timestamp) : base(key, timestamp)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.destination = destination;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000328F File Offset: 0x0000148F
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x04000032 RID: 50
		private readonly ErrorRoutingDestination destination;
	}
}
