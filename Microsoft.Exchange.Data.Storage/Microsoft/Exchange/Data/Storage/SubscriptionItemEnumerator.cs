using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B08 RID: 2824
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionItemEnumerator : SubscriptionItemEnumeratorBase
	{
		// Token: 0x0600669D RID: 26269 RVA: 0x001B33F7 File Offset: 0x001B15F7
		public SubscriptionItemEnumerator(IFolder folder) : base(folder)
		{
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x001B3400 File Offset: 0x001B1600
		public SubscriptionItemEnumerator(IFolder folder, Unlimited<uint> resultSize) : base(folder, resultSize)
		{
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x001B340A File Offset: 0x001B160A
		protected override SortBy[] GetSortByConstraint()
		{
			return SubscriptionItemEnumeratorBase.RefreshTimeUTCDescSortBy;
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x001B3411 File Offset: 0x001B1611
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			return false;
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x001B3414 File Offset: 0x001B1614
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			return false;
		}
	}
}
