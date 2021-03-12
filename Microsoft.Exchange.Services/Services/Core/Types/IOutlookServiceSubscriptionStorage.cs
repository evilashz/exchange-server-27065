using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F64 RID: 3940
	internal interface IOutlookServiceSubscriptionStorage : IDisposable
	{
		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060063B2 RID: 25522
		string TenantId { get; }

		// Token: 0x060063B3 RID: 25523
		List<OutlookServiceNotificationSubscription> GetActiveNotificationSubscriptions(string appId, uint deactivationInHours = 72U);

		// Token: 0x060063B4 RID: 25524
		List<OutlookServiceNotificationSubscription> GetActiveNotificationSubscriptionsForContext(string notificationConext);

		// Token: 0x060063B5 RID: 25525
		List<string> GetDeactivatedNotificationSubscriptions(string appId, uint deactivationInHours = 72U);

		// Token: 0x060063B6 RID: 25526
		List<string> GetExpiredNotificationSubscriptions(string appId);

		// Token: 0x060063B7 RID: 25527
		List<OutlookServiceNotificationSubscription> GetNotificationSubscriptions(string appId);

		// Token: 0x060063B8 RID: 25528
		OutlookServiceNotificationSubscription CreateOrUpdateSubscriptionItem(OutlookServiceNotificationSubscription subscription);

		// Token: 0x060063B9 RID: 25529
		void DeleteAllSubscriptions(string appId);

		// Token: 0x060063BA RID: 25530
		void DeleteExpiredSubscriptions(string appId);

		// Token: 0x060063BB RID: 25531
		void DeleteSubscription(string subscriptionId);
	}
}
