using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000365 RID: 869
	internal class UnsubscribeToGroupNotification : UnsubscribeToNotificationBase
	{
		// Token: 0x06001BEC RID: 7148 RVA: 0x0006C7A7 File Offset: 0x0006A9A7
		public UnsubscribeToGroupNotification(CallContext callContext, SubscriptionData[] subscriptionData) : base(callContext, subscriptionData)
		{
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0006C7B1 File Offset: 0x0006A9B1
		protected override void InternalUnsubscribeNotification(IMailboxContext userContext, SubscriptionData subscription)
		{
			if (subscription.Parameters == null)
			{
				throw new ArgumentNullException("Subscription data parameters cannot be null");
			}
			base.InternalUnsubscribeNotification(userContext, subscription);
		}
	}
}
