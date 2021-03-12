using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200015D RID: 349
	internal sealed class UnseenCountBrokerHandler : BrokerHandler
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x0002F804 File Offset: 0x0002DA04
		public UnseenCountBrokerHandler(string subscriptionId, SubscriptionParameters parameters, IMailboxContext userContext, IRecipientSession adSession) : base(subscriptionId, parameters, userContext)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.adSession = adSession;
				this.unseenItemNotifier = new UnseenItemNotifier(subscriptionId, userContext, null, null);
				this.unseenItemNotifier.RegisterWithPendingRequestNotifier();
				disposeGuard.Success();
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0002F86C File Offset: 0x0002DA6C
		protected override ExchangePrincipal SenderPrincipal
		{
			get
			{
				return ExchangePrincipal.FromProxyAddress(this.adSession, base.Parameters.MailboxId, RemotingOptions.AllowCrossSite);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002F888 File Offset: 0x0002DA88
		protected override BaseSubscription GetSubscriptionParmeters()
		{
			return new UnseenCountSubscription
			{
				ConsumerSubscriptionId = base.SubscriptionId,
				UserExternalDirectoryObjectId = base.UserContext.ExchangePrincipal.ExternalDirectoryObjectId,
				UserLegacyDN = base.UserContext.ExchangePrincipal.LegacyDn
			};
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002F8D4 File Offset: 0x0002DAD4
		protected override void HandleNotificatonInternal(BrokerNotification notification)
		{
			UnseenCountNotification unseenCountNotification = notification.Payload as UnseenCountNotification;
			if (unseenCountNotification == null)
			{
				return;
			}
			this.unseenItemNotifier.AddGroupNotificationPayload(new UnseenItemNotificationPayload
			{
				SubscriptionId = unseenCountNotification.ConsumerSubscriptionId,
				UnseenData = unseenCountNotification.UnseenData
			});
			this.unseenItemNotifier.PickupData();
		}

		// Token: 0x040007DE RID: 2014
		private readonly UnseenItemNotifier unseenItemNotifier;

		// Token: 0x040007DF RID: 2015
		private readonly IRecipientSession adSession;
	}
}
