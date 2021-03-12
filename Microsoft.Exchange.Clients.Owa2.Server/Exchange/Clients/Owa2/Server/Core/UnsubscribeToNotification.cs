using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000367 RID: 871
	internal class UnsubscribeToNotification : UnsubscribeToNotificationBase
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x0006C83E File Offset: 0x0006AA3E
		public UnsubscribeToNotification(CallContext callContext, SubscriptionData[] subscriptionData) : base(callContext, subscriptionData)
		{
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0006C848 File Offset: 0x0006AA48
		protected override void InternalUnsubscribeNotification(IMailboxContext userContext, SubscriptionData subscription)
		{
			if (subscription.Parameters == null)
			{
				throw new ArgumentNullException("Subscription data parameters cannot be null");
			}
			NotificationType notificationType = subscription.Parameters.NotificationType;
			if (notificationType == NotificationType.UnseenItemNotification)
			{
				userContext.NotificationManager.UnsubscribeToUnseenCountNotification(subscription.SubscriptionId, subscription.Parameters);
				return;
			}
			base.InternalUnsubscribeNotification(userContext, subscription);
		}
	}
}
