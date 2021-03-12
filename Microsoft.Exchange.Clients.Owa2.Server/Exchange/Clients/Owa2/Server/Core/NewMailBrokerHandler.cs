using System;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200015B RID: 347
	internal sealed class NewMailBrokerHandler : BrokerHandler
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002F468 File Offset: 0x0002D668
		public NewMailBrokerHandler(string subscriptionId, SubscriptionParameters parameters, IMailboxContext userContext) : base(subscriptionId, parameters, userContext)
		{
			this.newMailNotifier = new NewMailNotifier(subscriptionId, userContext);
			this.newMailNotifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002F48C File Offset: 0x0002D68C
		protected override BaseSubscription GetSubscriptionParmeters()
		{
			return new NewMailSubscription
			{
				ConsumerSubscriptionId = base.SubscriptionId
			};
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		protected override void HandleNotificatonInternal(BrokerNotification notification)
		{
			NewMailNotification newMailNotification = notification.Payload as NewMailNotification;
			if (newMailNotification == null)
			{
				return;
			}
			this.newMailNotifier.Payload = new NewMailNotificationPayload
			{
				SubscriptionId = newMailNotification.ConsumerSubscriptionId,
				ConversationId = newMailNotification.ConversationId,
				ItemId = newMailNotification.ItemId,
				Sender = newMailNotification.Sender,
				Subject = newMailNotification.Subject,
				PreviewText = newMailNotification.PreviewText,
				EventType = newMailNotification.EventType
			};
			this.newMailNotifier.PickupData();
		}

		// Token: 0x040007D9 RID: 2009
		private readonly NewMailNotifier newMailNotifier;
	}
}
