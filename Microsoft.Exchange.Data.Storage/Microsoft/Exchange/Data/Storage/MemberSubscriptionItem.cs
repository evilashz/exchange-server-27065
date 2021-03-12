using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007DD RID: 2013
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MemberSubscriptionItem : IMemberSubscriptionItem
	{
		// Token: 0x06004B78 RID: 19320 RVA: 0x0013AA5D File Offset: 0x00138C5D
		public MemberSubscriptionItem(string subscriptionId, ExDateTime lastUpdateTimeUTC)
		{
			ArgumentValidator.ThrowIfNull("subscriptionId", subscriptionId);
			this.SubscriptionId = subscriptionId;
			this.LastUpdateTimeUTC = lastUpdateTimeUTC;
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06004B79 RID: 19321 RVA: 0x0013AA7E File Offset: 0x00138C7E
		// (set) Token: 0x06004B7A RID: 19322 RVA: 0x0013AA86 File Offset: 0x00138C86
		public string SubscriptionId { get; private set; }

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x0013AA8F File Offset: 0x00138C8F
		// (set) Token: 0x06004B7C RID: 19324 RVA: 0x0013AA97 File Offset: 0x00138C97
		public ExDateTime LastUpdateTimeUTC { get; set; }
	}
}
