using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Delivery;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003CB RID: 971
	internal class InternalOpenConnectionEventArgs : OpenConnectionEventArgs
	{
		// Token: 0x06002C7F RID: 11391 RVA: 0x000B1546 File Offset: 0x000AF746
		public InternalOpenConnectionEventArgs(DeliverableMailItem deliverableMailItem, string nextHopDomain)
		{
			this.deliverableMailItem = deliverableMailItem;
			this.nextHopDomain = new RoutingDomain(nextHopDomain);
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000B1561 File Offset: 0x000AF761
		public override DeliverableMailItem DeliverableMailItem
		{
			get
			{
				return this.deliverableMailItem;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000B1569 File Offset: 0x000AF769
		public override RoutingDomain NextHopDomain
		{
			get
			{
				return this.nextHopDomain;
			}
		}

		// Token: 0x04001641 RID: 5697
		private DeliverableMailItem deliverableMailItem;

		// Token: 0x04001642 RID: 5698
		private RoutingDomain nextHopDomain;
	}
}
