using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000366 RID: 870
	internal class UnsubscribeToGroupUnseenNotification : UnsubscribeToNotificationBase
	{
		// Token: 0x06001BEE RID: 7150 RVA: 0x0006C7CE File Offset: 0x0006A9CE
		public UnsubscribeToGroupUnseenNotification(CallContext callContext, SubscriptionData[] subscriptionData) : base(callContext, subscriptionData)
		{
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0006C7D8 File Offset: 0x0006A9D8
		public static bool RequestShouldUseSharedContext(string methodName)
		{
			return methodName == "UnsubscribeToGroupUnseenNotification";
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
		protected override void InternalUnsubscribeNotification(IMailboxContext userContext, SubscriptionData subscription)
		{
			if (subscription.Parameters == null)
			{
				throw new ArgumentNullException("Subscription data parameters cannot be null");
			}
			if (subscription.Parameters.NotificationType != NotificationType.UnseenItemNotification)
			{
				throw new OwaInvalidOperationException("Invalid Notification type specified when calling UnsubscribeToGroupUnseenNotification");
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[UnsubscribeToGroupUnseenNotification::InternalUnsubscribeNotification] Unsubscribe for unseen notifications for subscription {0}", subscription.SubscriptionId);
		}
	}
}
