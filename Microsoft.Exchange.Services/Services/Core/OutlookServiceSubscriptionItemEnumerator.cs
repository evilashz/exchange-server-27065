using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F66 RID: 3942
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OutlookServiceSubscriptionItemEnumerator : OutlookServiceSubscriptionItemEnumeratorBase
	{
		// Token: 0x060063C8 RID: 25544 RVA: 0x00137253 File Offset: 0x00135453
		public OutlookServiceSubscriptionItemEnumerator(IFolder folder) : base(folder, null)
		{
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0013725D File Offset: 0x0013545D
		public OutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId) : base(folder, appId)
		{
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x00137267 File Offset: 0x00135467
		public OutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId, Unlimited<uint> resultSize) : base(folder, appId, resultSize)
		{
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x00137272 File Offset: 0x00135472
		protected override SortBy[] GetSortByConstraint()
		{
			return OutlookServiceSubscriptionItemEnumeratorBase.RefreshTimeUTCDescSortBy;
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x00137279 File Offset: 0x00135479
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			return false;
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0013727C File Offset: 0x0013547C
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			return false;
		}
	}
}
