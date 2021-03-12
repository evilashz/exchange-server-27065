using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000159 RID: 345
	internal interface INotificationManager : IDisposable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000C94 RID: 3220
		// (remove) Token: 0x06000C95 RID: 3221
		event EventHandler<EventArgs> RemoteKeepAliveEvent;

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000C96 RID: 3222
		SearchNotificationHandler SearchNotificationHandler { get; }

		// Token: 0x06000C97 RID: 3223
		void SubscribeToHierarchyNotification(string subscriptionId);

		// Token: 0x06000C98 RID: 3224
		void SubscribeToRowNotification(string subscriptionId, SubscriptionParameters parameters, ExTimeZone timeZone, CallContext callContext, bool remoteSubscription);

		// Token: 0x06000C99 RID: 3225
		void SubscribeToReminderNotification(string subscriptionId);

		// Token: 0x06000C9A RID: 3226
		void SubscribeToNewMailNotification(string subscriptionId, SubscriptionParameters parameters);

		// Token: 0x06000C9B RID: 3227
		string SubscribeToUnseenItemNotification(string subscriptionId, UserMailboxLocator mailboxLocator, IRecipientSession adSession);

		// Token: 0x06000C9C RID: 3228
		void SubscribeToGroupAssociationNotification(string subscriptionId, IRecipientSession adSession);

		// Token: 0x06000C9D RID: 3229
		void SubscribeToSearchNotification();

		// Token: 0x06000C9E RID: 3230
		void UnsubscribeForRowNotifications(string subscriptionId, SubscriptionParameters parameters);

		// Token: 0x06000C9F RID: 3231
		void CleanupSubscriptions();

		// Token: 0x06000CA0 RID: 3232
		void HandleConnectionDroppedNotification();

		// Token: 0x06000CA1 RID: 3233
		void RefreshSubscriptions(ExTimeZone timeZone);

		// Token: 0x06000CA2 RID: 3234
		void StartRemoteKeepAliveTimer();

		// Token: 0x06000CA3 RID: 3235
		void ReleaseSubscriptionsForChannelId(string channelId);

		// Token: 0x06000CA4 RID: 3236
		void ReleaseSubscription(string subscriptionId);

		// Token: 0x06000CA5 RID: 3237
		void SubscribeToUnseenCountNotification(string subscriptionId, SubscriptionParameters parameters, IRecipientSession adSession);

		// Token: 0x06000CA6 RID: 3238
		void UnsubscribeToUnseenCountNotification(string subscriptionId, SubscriptionParameters parameters);
	}
}
