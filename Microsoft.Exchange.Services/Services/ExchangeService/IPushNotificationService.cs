using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DDD RID: 3549
	internal interface IPushNotificationService : IDisposable
	{
		// Token: 0x06005B45 RID: 23365
		void EnqueuePendingOutlookPushNotification(ExchangeServiceTraceDelegate traceDelegate, IOutlookPushNotificationSubscriptionCache subscriptionCache, IOutlookPushNotificationSerializer serializer, string notificationContext, PendingOutlookPushNotification notification);
	}
}
