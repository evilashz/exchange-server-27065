using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001EE RID: 494
	internal interface IADDistributionList
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001913 RID: 6419
		// (set) Token: 0x06001914 RID: 6420
		ADObjectId ManagedBy { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001915 RID: 6421
		MultiValuedProperty<ADObjectId> Members { get; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001916 RID: 6422
		// (set) Token: 0x06001917 RID: 6423
		DeliveryReportsReceiver SendDeliveryReportsTo { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001918 RID: 6424
		// (set) Token: 0x06001919 RID: 6425
		bool SendOofMessageToOriginatorEnabled { get; set; }

		// Token: 0x0600191A RID: 6426
		ADPagedReader<ADRawEntry> Expand(int pageSize, params PropertyDefinition[] propertiesToReturn);

		// Token: 0x0600191B RID: 6427
		ADPagedReader<TEntry> Expand<TEntry>(int pageSize, params PropertyDefinition[] propertiesToReturn) where TEntry : MiniRecipient, new();

		// Token: 0x0600191C RID: 6428
		ADPagedReader<ADRecipient> Expand(int pageSize);
	}
}
