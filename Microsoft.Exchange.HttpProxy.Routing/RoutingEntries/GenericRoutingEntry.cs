using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x02000022 RID: 34
	internal class GenericRoutingEntry : RoutingEntryBase
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000032FE File Offset: 0x000014FE
		public GenericRoutingEntry(IRoutingKey key, IRoutingDestination destination, long timestamp)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.key = key;
			this.destination = destination;
			this.timestamp = timestamp;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003337 File Offset: 0x00001537
		public override IRoutingDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000333F File Offset: 0x0000153F
		public override IRoutingKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003347 File Offset: 0x00001547
		public override long Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly IRoutingKey key;

		// Token: 0x04000037 RID: 55
		private readonly IRoutingDestination destination;

		// Token: 0x04000038 RID: 56
		private readonly long timestamp;
	}
}
