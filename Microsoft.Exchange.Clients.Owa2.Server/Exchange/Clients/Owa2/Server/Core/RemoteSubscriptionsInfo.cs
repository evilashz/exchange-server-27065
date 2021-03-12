using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D0 RID: 464
	internal class RemoteSubscriptionsInfo : ISubscriptionInfo
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600106C RID: 4204 RVA: 0x0003F080 File Offset: 0x0003D280
		// (remove) Token: 0x0600106D RID: 4205 RVA: 0x0003F0B8 File Offset: 0x0003D2B8
		public event EventHandler onNoActiveSubscriptionEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600106E RID: 4206 RVA: 0x0003F0F0 File Offset: 0x0003D2F0
		// (remove) Token: 0x0600106F RID: 4207 RVA: 0x0003F128 File Offset: 0x0003D328
		public event EventHandler<RemoteSubscriptionEventArgs> noListenersForSubscriptionEvent;

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0003F15D File Offset: 0x0003D35D
		public int SubscriptionCount
		{
			get
			{
				return this.remoteSubscriptions.Count;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0003F16A File Offset: 0x0003D36A
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x0003F172 File Offset: 0x0003D372
		public string Mailbox { get; private set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0003F17B File Offset: 0x0003D37B
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x0003F183 File Offset: 0x0003D383
		public string ContextKey { get; private set; }

		// Token: 0x06001075 RID: 4213 RVA: 0x0003F18C File Offset: 0x0003D38C
		public RemoteSubscriptionsInfo(string mailbox, string contextKey)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("mailbox", mailbox);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("contextKey", contextKey);
			this.Mailbox = mailbox;
			this.ContextKey = contextKey;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003F1D9 File Offset: 0x0003D3D9
		public void Add(string subscriptionId, string channelId, string user, NotificationType notificationType, out bool subscriptionExists)
		{
			this.Add(subscriptionId, channelId, user, notificationType, null, out subscriptionExists);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003F1EC File Offset: 0x0003D3EC
		public void Add(string subscriptionId, string channelId, string user, NotificationType notificationType, string remoteEndpointOverride, out bool subscriptionExists)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("channelId", channelId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("user", user);
			RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo notificationTypeAndChannelInfo;
			if (!this.remoteSubscriptions.TryGetValue(subscriptionId, out notificationTypeAndChannelInfo))
			{
				notificationTypeAndChannelInfo = (this.remoteSubscriptions[subscriptionId] = new RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo(notificationType));
			}
			HashSet<RemoteChannelInfo> channels = notificationTypeAndChannelInfo.Channels;
			RemoteChannelInfo remoteChannelInfo = new RemoteChannelInfo(channelId, user);
			if (!string.IsNullOrEmpty(remoteEndpointOverride) && AppConfigLoader.GetConfigBoolValue("Test_OwaAllowHeaderOverride", false))
			{
				remoteChannelInfo.EndpointTestOverride = remoteEndpointOverride;
			}
			subscriptionExists = !channels.Add(remoteChannelInfo);
			if (subscriptionExists)
			{
				SubscriberConcurrencyTracker.Instance.OnResubscribe(this.Mailbox, notificationType);
			}
			else
			{
				SubscriberConcurrencyTracker.Instance.OnSubscribe(this.Mailbox, notificationType);
			}
			this.AddSubscriptionToPerChannelList(channelId, subscriptionId);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003F2AE File Offset: 0x0003D4AE
		public void Remove(string subscriptionId, string channelId, string user)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("channelId", channelId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("user", user);
			this.Remove(subscriptionId, new RemoteChannelInfo(channelId, user));
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003F2E0 File Offset: 0x0003D4E0
		public void NotifyAllChannelsRemoved()
		{
			Dictionary<NotificationType, int> dictionary = new Dictionary<NotificationType, int>();
			foreach (RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo notificationTypeAndChannelInfo in this.remoteSubscriptions.Values)
			{
				int num;
				if (!dictionary.TryGetValue(notificationTypeAndChannelInfo.Type, out num))
				{
					dictionary.Add(notificationTypeAndChannelInfo.Type, notificationTypeAndChannelInfo.Channels.Count);
				}
				else
				{
					Dictionary<NotificationType, int> dictionary2;
					NotificationType type;
					(dictionary2 = dictionary)[type = notificationTypeAndChannelInfo.Type] = dictionary2[type] + notificationTypeAndChannelInfo.Channels.Count;
				}
			}
			foreach (KeyValuePair<NotificationType, int> keyValuePair in dictionary)
			{
				SubscriberConcurrencyTracker.Instance.OnUnsubscribe(this.Mailbox, keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003F3E0 File Offset: 0x0003D5E0
		public bool CleanUpChannel(string channelId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("channelId", channelId);
			List<string> list;
			if (this.perChannelSubscriptionLists.TryGetValue(channelId, out list))
			{
				foreach (string subscriptionId in list.ToArray())
				{
					this.Remove(subscriptionId, new RemoteChannelInfo(channelId, null));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003F434 File Offset: 0x0003D634
		public RemoteChannelInfo[] GetChannels(string subscriptionId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionId", subscriptionId);
			RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo notificationTypeAndChannelInfo;
			if (this.remoteSubscriptions.TryGetValue(subscriptionId, out notificationTypeAndChannelInfo))
			{
				return notificationTypeAndChannelInfo.Channels.ToArray<RemoteChannelInfo>();
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RemoteSubscriptionInfo.GetChannels - Subscription not active. SubscriptionId: {0}", subscriptionId);
			return Array<RemoteChannelInfo>.Empty;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003F484 File Offset: 0x0003D684
		private void Remove(string subscriptionId, RemoteChannelInfo channelInfo)
		{
			RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo notificationTypeAndChannelInfo;
			if (this.remoteSubscriptions.TryGetValue(subscriptionId, out notificationTypeAndChannelInfo))
			{
				if (notificationTypeAndChannelInfo.Channels.Remove(channelInfo))
				{
					SubscriberConcurrencyTracker.Instance.OnUnsubscribe(this.Mailbox, notificationTypeAndChannelInfo.Type);
				}
				this.RemoveSubscriptionFromPerChannelList(channelInfo.ChannelId, subscriptionId);
				if (notificationTypeAndChannelInfo.Channels.Count == 0)
				{
					this.remoteSubscriptions.Remove(subscriptionId);
					this.RaiseEventsIfNeeded(subscriptionId);
					return;
				}
			}
			else
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RemoteSubscriptionInfo.Remove - Subscription not active. SubscriptionId: {0}", subscriptionId);
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003F50C File Offset: 0x0003D70C
		private void RaiseEventsIfNeeded(string subscriptionId)
		{
			if (this.noListenersForSubscriptionEvent != null)
			{
				this.noListenersForSubscriptionEvent(this, new RemoteSubscriptionEventArgs(this.ContextKey, subscriptionId));
			}
			if (this.SubscriptionCount == 0 && this.onNoActiveSubscriptionEvent != null)
			{
				this.onNoActiveSubscriptionEvent(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003F55C File Offset: 0x0003D75C
		private void AddSubscriptionToPerChannelList(string channelId, string subscriptionId)
		{
			List<string> list;
			if (this.perChannelSubscriptionLists.TryGetValue(channelId, out list))
			{
				if (!list.Contains(subscriptionId))
				{
					list.Add(subscriptionId);
					return;
				}
			}
			else
			{
				this.perChannelSubscriptionLists[channelId] = new List<string>(5);
				this.perChannelSubscriptionLists[channelId].Add(subscriptionId);
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003F5B0 File Offset: 0x0003D7B0
		private void RemoveSubscriptionFromPerChannelList(string channelId, string subscriptionId)
		{
			List<string> list;
			if (this.perChannelSubscriptionLists.TryGetValue(channelId, out list))
			{
				list.Remove(subscriptionId);
				if (list.Count == 0)
				{
					this.perChannelSubscriptionLists.Remove(channelId);
				}
			}
		}

		// Token: 0x040009C0 RID: 2496
		private const int InitialSizeForChannelList = 5;

		// Token: 0x040009C3 RID: 2499
		private readonly Dictionary<string, RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo> remoteSubscriptions = new Dictionary<string, RemoteSubscriptionsInfo.NotificationTypeAndChannelInfo>();

		// Token: 0x040009C4 RID: 2500
		private readonly Dictionary<string, List<string>> perChannelSubscriptionLists = new Dictionary<string, List<string>>();

		// Token: 0x020001D1 RID: 465
		private class NotificationTypeAndChannelInfo
		{
			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x06001080 RID: 4224 RVA: 0x0003F5EA File Offset: 0x0003D7EA
			// (set) Token: 0x06001081 RID: 4225 RVA: 0x0003F5F2 File Offset: 0x0003D7F2
			public NotificationType Type { get; private set; }

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x06001082 RID: 4226 RVA: 0x0003F5FB File Offset: 0x0003D7FB
			// (set) Token: 0x06001083 RID: 4227 RVA: 0x0003F603 File Offset: 0x0003D803
			public HashSet<RemoteChannelInfo> Channels { get; private set; }

			// Token: 0x06001084 RID: 4228 RVA: 0x0003F60C File Offset: 0x0003D80C
			public NotificationTypeAndChannelInfo(NotificationType type)
			{
				this.Type = type;
				this.Channels = new HashSet<RemoteChannelInfo>();
			}
		}
	}
}
