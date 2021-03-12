using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001CD RID: 461
	internal class RemoteNotificationManager
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0003E77F File Offset: 0x0003C97F
		public static RemoteNotificationManager Instance
		{
			get
			{
				return RemoteNotificationManager.instance;
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003E786 File Offset: 0x0003C986
		protected RemoteNotificationManager()
		{
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003E7A4 File Offset: 0x0003C9A4
		public void Subscribe(string contextKey, string subscriptionMailbox, string subscriptionId, string channelId, string user, NotificationType notificationType, out bool subscriptionExists)
		{
			this.Subscribe(contextKey, subscriptionMailbox, subscriptionId, channelId, user, notificationType, null, out subscriptionExists);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
		public void Subscribe(string contextKey, string subscriptionMailbox, string subscriptionId, string channelId, string user, NotificationType notificationType, string remoteEndpointOverride, out bool subscriptionExists)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("contextKey", contextKey);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionMailbox", subscriptionMailbox);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("channelId", channelId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("user", user);
			if (this.contextKeySubscriptionsLock.LockWriterElastic(3000))
			{
				try
				{
					ISubscriptionInfo subscriptionInfo;
					if (!this.contextKeySubscriptions.TryGetValue(contextKey, out subscriptionInfo))
					{
						subscriptionInfo = (this.contextKeySubscriptions[contextKey] = this.CreateSubscriptionInfo(subscriptionMailbox, contextKey));
						subscriptionInfo.onNoActiveSubscriptionEvent += this.NoActiveSubscription;
						subscriptionInfo.noListenersForSubscriptionEvent += this.NoActiveListeners;
					}
					subscriptionInfo.Add(subscriptionId, channelId, user, notificationType, remoteEndpointOverride, out subscriptionExists);
					return;
				}
				finally
				{
					this.contextKeySubscriptionsLock.ReleaseWriterLock();
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "[RemoteNotificationManager::Subscribe] mailboxSubscriptionsLock timed");
			throw new OwaLockTimeoutException(string.Format("Could not acquire WriterLock on mailboxSubscriptions", new object[0]));
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003E8C4 File Offset: 0x0003CAC4
		public virtual ISubscriptionInfo CreateSubscriptionInfo(string mailbox, string contextKey)
		{
			return new RemoteSubscriptionsInfo(mailbox, contextKey);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		public void UnSubscribe(string contextKey, string subscriptionId, string channelId, string user)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("contextKey", contextKey);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("channelId", channelId);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("user", user);
			if (this.contextKeySubscriptionsLock.LockWriterElastic(3000))
			{
				try
				{
					ISubscriptionInfo subscriptionInfo;
					if (this.contextKeySubscriptions.TryGetValue(contextKey, out subscriptionInfo))
					{
						subscriptionInfo.Remove(subscriptionId, channelId, user);
					}
					else
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RemoteNotificationManager.UnSubscribe - No Subscriptions exists for this user context. UserContextKey: {0}", contextKey);
					}
					return;
				}
				finally
				{
					this.contextKeySubscriptionsLock.ReleaseWriterLock();
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "[RemoteNotificationManager::UnSubscribe] mailboxSubscriptionsLock timed");
			throw new OwaLockTimeoutException(string.Format("Could not acquire WriterLock on mailboxSubscriptions", new object[0]));
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0003E99C File Offset: 0x0003CB9C
		public void CleanUpSubscriptions(string contextKey)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("contextKey", contextKey);
			if (this.contextKeySubscriptionsLock.LockWriterElastic(3000))
			{
				try
				{
					ISubscriptionInfo subscriptions;
					if (this.contextKeySubscriptions.TryGetValue(contextKey, out subscriptions))
					{
						this.RemoveSubscriptions(subscriptions);
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RemoteNotificationManager.CleanUpSubscriptions - All subscriptions removed for this user context. UserContextKey: {0}", contextKey);
					}
					else
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RemoteNotificationManager.CleanUpSubscriptions - No Subscriptions exists for this user context. UserContextKey: {0}", contextKey);
					}
					return;
				}
				finally
				{
					this.contextKeySubscriptionsLock.ReleaseWriterLock();
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "[RemoteNotificationManager::CleanUpSubscriptions] mailboxSubscriptionsLock timed");
			throw new OwaLockTimeoutException(string.Format("Could not acquire WriterLock on mailboxSubscriptions", new object[0]));
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003EA58 File Offset: 0x0003CC58
		public virtual void CleanUpChannel(string channelId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("channelId", channelId);
			List<string> list = new List<string>();
			if (this.contextKeySubscriptionsLock.LockWriterElastic(3000))
			{
				try
				{
					foreach (ISubscriptionInfo subscriptionInfo in this.contextKeySubscriptions.Values.ToArray<ISubscriptionInfo>())
					{
						if (subscriptionInfo.CleanUpChannel(channelId))
						{
							list.Add(subscriptionInfo.ContextKey);
						}
					}
					goto IL_9D;
				}
				finally
				{
					this.contextKeySubscriptionsLock.ReleaseWriterLock();
				}
				goto IL_71;
				IL_9D:
				foreach (string contextKey in list)
				{
					UserContextKey userContextKey;
					if (this.TryParseUserContextKey(contextKey, out userContextKey))
					{
						IMailboxContext mailboxContextFromCache = UserContextManager.GetMailboxContextFromCache(userContextKey);
						if (mailboxContextFromCache != null)
						{
							mailboxContextFromCache.NotificationManager.ReleaseSubscriptionsForChannelId(channelId);
						}
					}
				}
				return;
			}
			IL_71:
			ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "[RemoteNotificationManager::CleanUpChannel] mailboxSubscriptionsLock timed");
			throw new OwaLockTimeoutException(string.Format("Could not acquire WriterLock on mailboxSubscriptions", new object[0]));
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003EB70 File Offset: 0x0003CD70
		public virtual IEnumerable<IDestinationInfo> GetDestinations(string contextKey, string subscriptionId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("contextKey", contextKey);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("subscriptionId", subscriptionId);
			Dictionary<Uri, IDestinationInfo> dictionary = new Dictionary<Uri, IDestinationInfo>();
			foreach (RemoteChannelInfo remoteChannelInfo in this.GetChannels(contextKey, subscriptionId))
			{
				Uri uri;
				if (string.IsNullOrEmpty(remoteChannelInfo.EndpointTestOverride))
				{
					uri = this.GetDestinationUri(remoteChannelInfo.User);
				}
				else
				{
					uri = new Uri(remoteChannelInfo.EndpointTestOverride);
				}
				IDestinationInfo destinationInfo;
				if (uri == null)
				{
					string message = string.Format("Could not resolve url. User - {0}, Subscription Id - {1}, UserContextKey - {2}, Channel - {3}.", new object[]
					{
						remoteChannelInfo.User,
						subscriptionId,
						contextKey,
						remoteChannelInfo.ChannelId
					});
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("RemoteNotificationManager", null, "GetDestinations", message));
					this.UnSubscribe(contextKey, subscriptionId, remoteChannelInfo.ChannelId, remoteChannelInfo.User);
				}
				else if (!dictionary.TryGetValue(uri, out destinationInfo))
				{
					dictionary[uri] = new RemoteDestinationInfo(uri, remoteChannelInfo.ChannelId);
				}
				else
				{
					destinationInfo.AddChannel(remoteChannelInfo.ChannelId);
				}
			}
			return dictionary.Values;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003EC98 File Offset: 0x0003CE98
		public virtual Uri GetDestinationUri(string user)
		{
			Uri result;
			if (HttpProxyBackEndHelper.TryGetBackEndWebServicesUrlFromSmtp(user, (SmtpAddress address) => UserContextUtilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, address.Domain, null), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003ECD0 File Offset: 0x0003CED0
		private RemoteChannelInfo[] GetChannels(string contextKey, string subscriptionId)
		{
			RemoteChannelInfo[] result = Array<RemoteChannelInfo>.Empty;
			if (this.contextKeySubscriptionsLock.LockReaderElastic(3000))
			{
				try
				{
					ISubscriptionInfo subscriptionInfo;
					this.contextKeySubscriptions.TryGetValue(contextKey, out subscriptionInfo);
					if (subscriptionInfo != null)
					{
						result = subscriptionInfo.GetChannels(subscriptionId);
					}
					else
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "RemoteNotificationManager.GetChannels - No Subscriptions exists for this user context. UserContextKey: {0}", contextKey);
					}
					return result;
				}
				finally
				{
					this.contextKeySubscriptionsLock.ReleaseReaderLock();
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "[RemoteNotificationManager::GetChannels] mailboxSubscriptionsLock timed");
			throw new OwaLockTimeoutException(string.Format("Could not acquire ReaderLock on mailboxSubscriptions", new object[0]));
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003ED74 File Offset: 0x0003CF74
		private void NoActiveListeners(object sender, RemoteSubscriptionEventArgs eventArgs)
		{
			UserContextKey userContextKey;
			if (this.TryParseUserContextKey(eventArgs.ContextKey, out userContextKey))
			{
				IMailboxContext mailboxContextFromCache = UserContextManager.GetMailboxContextFromCache(userContextKey);
				if (mailboxContextFromCache != null)
				{
					mailboxContextFromCache.NotificationManager.ReleaseSubscription(eventArgs.SubscriptionId);
				}
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003EDAC File Offset: 0x0003CFAC
		private void NoActiveSubscription(object sender, EventArgs e)
		{
			ISubscriptionInfo subscriptionInfo = sender as ISubscriptionInfo;
			if (subscriptionInfo != null)
			{
				this.RemoveSubscriptions(subscriptionInfo);
				UserContextKey userContextKey;
				if (this.TryParseUserContextKey(subscriptionInfo.ContextKey, out userContextKey))
				{
					UserContext userContext = UserContextManager.GetMailboxContextFromCache(userContextKey) as UserContext;
					if (userContext != null && userContext.IsGroupUserContext)
					{
						userContext.RetireMailboxSessionForGroupMailbox();
					}
				}
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003EDF8 File Offset: 0x0003CFF8
		private bool TryParseUserContextKey(string contextKey, out UserContextKey contextKeyObj)
		{
			bool flag = UserContextKey.TryParse(contextKey, out contextKeyObj);
			if (!flag)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string>((long)this.GetHashCode(), "RemoteNotificationManager.TryParseUserContextKey - couldn't parse UserContextKey {0}, skipping cleanup", contextKey);
				ExWatson.SendReport(new ArgumentException(string.Format("RemoteNotificationManager::TryParseUserContextKey - TryParse failed for UserContextKey string - '{0}'", contextKey)), ReportOptions.None, null);
			}
			return flag;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003EE40 File Offset: 0x0003D040
		private void RemoveSubscriptions(ISubscriptionInfo subscriptions)
		{
			if (subscriptions == null)
			{
				return;
			}
			if (this.contextKeySubscriptions.Remove(subscriptions.ContextKey))
			{
				subscriptions.NotifyAllChannelsRemoved();
			}
			subscriptions.onNoActiveSubscriptionEvent -= this.NoActiveSubscription;
			subscriptions.noListenersForSubscriptionEvent -= this.NoActiveListeners;
		}

		// Token: 0x040009B5 RID: 2485
		internal const int ContextKeySubscriptionsLockTimeout = 3000;

		// Token: 0x040009B6 RID: 2486
		private static readonly RemoteNotificationManager instance = new RemoteNotificationManager();

		// Token: 0x040009B7 RID: 2487
		private readonly OwaRWLockWrapper contextKeySubscriptionsLock = new OwaRWLockWrapper();

		// Token: 0x040009B8 RID: 2488
		protected Dictionary<string, ISubscriptionInfo> contextKeySubscriptions = new Dictionary<string, ISubscriptionInfo>();
	}
}
