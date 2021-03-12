using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C0 RID: 448
	internal interface ISubscriptionInfo
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000FE4 RID: 4068
		// (remove) Token: 0x06000FE5 RID: 4069
		event EventHandler onNoActiveSubscriptionEvent;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000FE6 RID: 4070
		// (remove) Token: 0x06000FE7 RID: 4071
		event EventHandler<RemoteSubscriptionEventArgs> noListenersForSubscriptionEvent;

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FE8 RID: 4072
		int SubscriptionCount { get; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FE9 RID: 4073
		string Mailbox { get; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FEA RID: 4074
		string ContextKey { get; }

		// Token: 0x06000FEB RID: 4075
		void Add(string subscriptionId, string channelId, string user, NotificationType notificationType, out bool subscriptionExists);

		// Token: 0x06000FEC RID: 4076
		void Add(string subscriptionId, string channelId, string user, NotificationType notificationType, string remoteEndpointOverride, out bool subscriptionExists);

		// Token: 0x06000FED RID: 4077
		void Remove(string subscriptionId, string channelId, string user);

		// Token: 0x06000FEE RID: 4078
		void NotifyAllChannelsRemoved();

		// Token: 0x06000FEF RID: 4079
		bool CleanUpChannel(string channelId);

		// Token: 0x06000FF0 RID: 4080
		RemoteChannelInfo[] GetChannels(string subscriptionId);
	}
}
