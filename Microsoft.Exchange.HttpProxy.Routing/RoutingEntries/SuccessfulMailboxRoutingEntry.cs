using System;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x02000024 RID: 36
	internal class SuccessfulMailboxRoutingEntry : MailboxRoutingEntry
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000337C File Offset: 0x0000157C
		public SuccessfulMailboxRoutingEntry(IRoutingKey key, DatabaseGuidRoutingDestination destination, long timestamp) : base(key, timestamp)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.destination = destination;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000033A1 File Offset: 0x000015A1
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000033A9 File Offset: 0x000015A9
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033B2 File Offset: 0x000015B2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400003A RID: 58
		private readonly DatabaseGuidRoutingDestination destination;
	}
}
