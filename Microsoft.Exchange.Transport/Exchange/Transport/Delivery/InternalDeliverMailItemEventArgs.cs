using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Delivery;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003C8 RID: 968
	internal class InternalDeliverMailItemEventArgs : DeliverMailItemEventArgs
	{
		// Token: 0x06002C48 RID: 11336 RVA: 0x000B0C12 File Offset: 0x000AEE12
		public InternalDeliverMailItemEventArgs(DeliverableMailItem deliverableMailItem)
		{
			this.deliverableMailItem = deliverableMailItem;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000B0C21 File Offset: 0x000AEE21
		public override DeliverableMailItem DeliverableMailItem
		{
			get
			{
				return this.deliverableMailItem;
			}
		}

		// Token: 0x04001634 RID: 5684
		private DeliverableMailItem deliverableMailItem;
	}
}
