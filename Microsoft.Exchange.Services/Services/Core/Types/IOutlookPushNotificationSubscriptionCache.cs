using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F71 RID: 3953
	internal abstract class IOutlookPushNotificationSubscriptionCache
	{
		// Token: 0x0600641E RID: 25630
		internal abstract bool QueryMailboxSubscriptions(string notificationContext, out string tenantId, out List<OutlookServiceNotificationSubscription> activeSubscriptions);
	}
}
