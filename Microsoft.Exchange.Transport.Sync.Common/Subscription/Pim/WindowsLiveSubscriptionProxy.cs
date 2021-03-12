using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public abstract class WindowsLiveSubscriptionProxy : PimSubscriptionProxy
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x000209AF File Offset: 0x0001EBAF
		internal WindowsLiveSubscriptionProxy(WindowsLiveServiceAggregationSubscription subscription) : base(subscription)
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000209B8 File Offset: 0x0001EBB8
		public override ValidationError[] Validate()
		{
			ICollection<ValidationError> collection = PimSubscriptionValidator.Validate(this);
			ValidationError[] array = new ValidationError[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000209E1 File Offset: 0x0001EBE1
		public override void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000209E8 File Offset: 0x0001EBE8
		public void SetLiveAccountPuid(string puid)
		{
			((WindowsLiveServiceAggregationSubscription)base.Subscription).Puid = puid;
		}
	}
}
