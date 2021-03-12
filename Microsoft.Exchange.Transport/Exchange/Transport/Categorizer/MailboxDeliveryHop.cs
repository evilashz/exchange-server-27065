using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000241 RID: 577
	internal class MailboxDeliveryHop : RoutingNextHop
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x00065CD0 File Offset: 0x00063ED0
		public MailboxDeliveryHop(ADObjectId databaseId, DeliveryType deliveryType)
		{
			RoutingUtils.ThrowIfNullOrEmpty(databaseId, "databaseId");
			if (!NextHopType.IsMailboxDeliveryType(deliveryType))
			{
				throw new ArgumentOutOfRangeException("deliveryType", deliveryType, "Non-mailbox delivery type provided: " + deliveryType);
			}
			this.databaseId = databaseId;
			this.deliveryType = deliveryType;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00065D25 File Offset: 0x00063F25
		public override DeliveryType DeliveryType
		{
			get
			{
				return this.deliveryType;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00065D2D File Offset: 0x00063F2D
		public override Guid NextHopGuid
		{
			get
			{
				return this.databaseId.ObjectGuid;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00065D3A File Offset: 0x00063F3A
		public override string GetNextHopDomain(RoutingContext context)
		{
			return this.databaseId.Name;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00065D48 File Offset: 0x00063F48
		public override bool Match(RoutingNextHop other)
		{
			return other is MailboxDeliveryHop;
		}

		// Token: 0x04000C10 RID: 3088
		private ADObjectId databaseId;

		// Token: 0x04000C11 RID: 3089
		private DeliveryType deliveryType;
	}
}
