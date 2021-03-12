using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000195 RID: 405
	internal sealed class OwaMapiNotificationManager : DisposeTrackableBase, INotificationManager, IDisposable
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x00036C88 File Offset: 0x00034E88
		internal OwaMapiNotificationManager(IMailboxContext userContext)
		{
			this.userContext = userContext;
			this.notificationHandlers = new List<MapiNotificationHandlerBase>();
			this.rowNotificationHandlerCache = new OwaMapiNotificationManager.RowNotificationHandlerCache(this.userContext);
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000E7E RID: 3710 RVA: 0x00036CC0 File Offset: 0x00034EC0
		// (remove) Token: 0x06000E7F RID: 3711 RVA: 0x00036CF8 File Offset: 0x00034EF8
		public event EventHandler<EventArgs> RemoteKeepAliveEvent;

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00036D2D File Offset: 0x00034F2D
		public SearchNotificationHandler SearchNotificationHandler
		{
			get
			{
				return this.searchHandlerLoggedUser;
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00036D38 File Offset: 0x00034F38
		public void SubscribeToHierarchyNotification(string subscriptionId)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					UserContext fullUserContext = this.GetFullUserContext("Hierarchy Notification");
					if (this.hierarchyHandlerLoggedUser == null)
					{
						try
						{
							this.userContext.LockAndReconnectMailboxSession(3000);
							this.hierarchyHandlerLoggedUser = HierachyNotificationHandlerFactory.CreateHandler(subscriptionId, fullUserContext);
							this.notificationHandlers.Add(this.hierarchyHandlerLoggedUser);
						}
						finally
						{
							if (this.userContext.MailboxSessionLockedByCurrentThread())
							{
								this.userContext.UnlockAndDisconnectMailboxSession();
							}
						}
						this.WireConnectionDroppedHandler(this.hierarchyHandlerLoggedUser);
					}
					this.hierarchyHandlerLoggedUser.Subscribe();
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<UserContextKey, string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::SubscribeToHierarchyNotification] START userContextKey: {0} SubscriptionId: {1} Setting this.userContext.HasActiveHierarchySubscription = true", this.userContext.Key, subscriptionId);
					fullUserContext.HasActiveHierarchySubscription = true;
				}
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00036E28 File Offset: 0x00035028
		public void SubscribeToRowNotification(string subscriptionId, SubscriptionParameters parameters, ExTimeZone timeZone, CallContext callContext, bool remoteSubscription)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (parameters.FolderId == null)
			{
				throw new OwaInvalidOperationException("Folder Id must be specified when subscribing to row notifications");
			}
			if (subscriptionId == null)
			{
				throw new ArgumentNullException("subscriptionId");
			}
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<UserContextKey, string, string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::SubscribeToRowNotification] START userContextKey: {0} SubscriptionId: {1} ChannelId: {2}", this.userContext.Key, subscriptionId, parameters.ChannelId);
					RowNotificationHandler rowNotificationHandler = null;
					this.rowNotificationHandlerCache.TryGetHandler(subscriptionId, out rowNotificationHandler);
					if (rowNotificationHandler == null)
					{
						StoreObjectId storeObjectId = StoreId.EwsIdToStoreObjectId(parameters.FolderId);
						if (storeObjectId == null)
						{
							throw new OwaInvalidOperationException("Invalid Folder Id. Could not be converted to a storeFolderId");
						}
						if (parameters.NotificationType == NotificationType.CalendarItemNotification)
						{
							rowNotificationHandler = new CalendarItemNotificationHandler(subscriptionId, parameters, storeObjectId, this.userContext, this.userContext.ExchangePrincipal.MailboxInfo.MailboxGuid, timeZone, remoteSubscription);
						}
						else if (parameters.NotificationType == NotificationType.PeopleIKnowNotification)
						{
							rowNotificationHandler = new PeopleIKnowRowNotificationHandler(subscriptionId, parameters, storeObjectId, this.userContext, this.userContext.ExchangePrincipal.MailboxInfo.MailboxGuid, timeZone, callContext.ClientCulture);
						}
						else if (parameters.IsConversation)
						{
							UserContext fullUserContext = this.GetFullUserContext("Conversation row notification");
							rowNotificationHandler = new ConversationRowNotificationHandler(subscriptionId, parameters, storeObjectId, this.userContext, this.userContext.ExchangePrincipal.MailboxInfo.MailboxGuid, timeZone, remoteSubscription, fullUserContext.FeaturesManager);
						}
						else
						{
							UserContext fullUserContext2 = this.GetFullUserContext("MessageItem row notification");
							rowNotificationHandler = new MessageItemRowNotificationHandler(subscriptionId, parameters, storeObjectId, this.userContext, this.userContext.ExchangePrincipal.MailboxInfo.MailboxGuid, timeZone, fullUserContext2.FeaturesManager);
						}
						try
						{
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<UserContextKey, string, string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::SubscribeToRowNotification] userContextKey: {0} New subscription for subscriptionId: {1} ChannelId: {2}", this.userContext.Key, subscriptionId, parameters.ChannelId);
							this.WireConnectionDroppedHandler(rowNotificationHandler);
							rowNotificationHandler.Subscribe();
							rowNotificationHandler.OnBeforeDisposed += this.BeforeDisposeRowNotificationHandler;
							this.rowNotificationHandlerCache.AddHandler(subscriptionId, rowNotificationHandler, parameters.ChannelId);
							rowNotificationHandler = null;
							goto IL_319;
						}
						finally
						{
							if (rowNotificationHandler != null)
							{
								try
								{
									this.userContext.LockAndReconnectMailboxSession(3000);
									rowNotificationHandler.Dispose();
									rowNotificationHandler = null;
								}
								catch (OwaLockTimeoutException ex)
								{
									ExTraceGlobals.NotificationsCallTracer.TraceError<string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::SubscribeToRowNotification] User context lock timed out in attempt to dispose handler. Exception: {0}", ex.Message);
								}
								finally
								{
									if (this.userContext.MailboxSessionLockedByCurrentThread())
									{
										this.userContext.UnlockAndDisconnectMailboxSession();
									}
								}
							}
						}
					}
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[OwaMapiNotificationManager::SubscribeToRowNotification] userContextKey: {0} Reusing existing notification handler subscriptionId: {1} ChannelId: {2} Current RefCount: {3}. Setting MissedNotifications = false", new object[]
					{
						this.userContext.Key,
						subscriptionId,
						parameters.ChannelId,
						rowNotificationHandler.RefCount
					});
					rowNotificationHandler.MissedNotifications = false;
					if (rowNotificationHandler.NeedToReinitSubscriptions)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[OwaMapiNotificationManager::SubscribeToRowNotification] userContextKey: {0} Need to re-init subscriptionId: {1} ChannelId: {2} Refcount: {3}", new object[]
						{
							this.userContext.Key,
							subscriptionId,
							parameters.ChannelId,
							rowNotificationHandler.RefCount
						});
						rowNotificationHandler.Subscribe();
					}
					this.rowNotificationHandlerCache.AddHandler(subscriptionId, rowNotificationHandler, parameters.ChannelId);
				}
				IL_319:;
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000371C0 File Offset: 0x000353C0
		public void SubscribeToReminderNotification(string subscriptionId)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					if (this.reminderHandlerLoggedUser == null)
					{
						this.reminderHandlerLoggedUser = new ReminderNotificationHandler(subscriptionId, this.userContext);
						this.notificationHandlers.Add(this.reminderHandlerLoggedUser);
						this.WireConnectionDroppedHandler(this.reminderHandlerLoggedUser);
					}
					this.reminderHandlerLoggedUser.Subscribe();
				}
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00037244 File Offset: 0x00035444
		public void SubscribeToNewMailNotification(string subscriptionId, SubscriptionParameters parameters)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					if (this.newMailHandlerLoggedUser == null)
					{
						this.newMailHandlerLoggedUser = NewMailNotificationHandlerFactory.Create(subscriptionId, this.userContext, parameters);
						this.notificationHandlers.Add(this.newMailHandlerLoggedUser);
						this.WireConnectionDroppedHandler(this.newMailHandlerLoggedUser);
					}
					this.newMailHandlerLoggedUser.Subscribe();
				}
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000372CC File Offset: 0x000354CC
		public string SubscribeToUnseenItemNotification(string subscriptionId, UserMailboxLocator mailboxLocator, IRecipientSession adSession)
		{
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					throw new OwaInvalidOperationException("[OwaMapiNotificationManager::SubscribeToUnseenItemNotification] Subscribe failed because object OwaMapiNotificationManager is disposed.");
				}
				if (this.unseenItemHandler == null)
				{
					this.unseenItemHandler = new UnseenItemNotificationHandler(this.userContext, adSession);
					this.unseenItemHandler.Subscribe();
					this.notificationHandlers.Add(this.unseenItemHandler);
					this.WireConnectionDroppedHandler(this.unseenItemHandler);
				}
			}
			string result;
			try
			{
				this.userContext.LockAndReconnectMailboxSession(3000);
				result = this.unseenItemHandler.AddMemberSubscription(subscriptionId, mailboxLocator);
			}
			finally
			{
				if (this.userContext.MailboxSessionLockedByCurrentThread())
				{
					this.userContext.UnlockAndDisconnectMailboxSession();
				}
			}
			return result;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000373A4 File Offset: 0x000355A4
		public void SubscribeToUnseenCountNotification(string subscriptionId, SubscriptionParameters parameters, IRecipientSession adSession)
		{
			throw new NotSupportedException("SubscribeToUnseenCountNotification is only supported through Broker not in OwaMapiNotificationManager.");
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000373B0 File Offset: 0x000355B0
		public void UnsubscribeToUnseenCountNotification(string subscriptionId, SubscriptionParameters parameters)
		{
			throw new NotSupportedException("UnSubscribeToUnseenCountNotification is only supported through Broker not in OwaMapiNotificationManager.");
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000373BC File Offset: 0x000355BC
		public void SubscribeToGroupAssociationNotification(string subscriptionId, IRecipientSession adSession)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed && this.groupAssociationHandlerLoggedUser == null)
				{
					this.groupAssociationHandlerLoggedUser = new GroupAssociationNotificationHandler(subscriptionId, this.userContext, adSession);
					this.notificationHandlers.Add(this.groupAssociationHandlerLoggedUser);
					this.WireConnectionDroppedHandler(this.groupAssociationHandlerLoggedUser);
					this.groupAssociationHandlerLoggedUser.Subscribe();
				}
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00037444 File Offset: 0x00035644
		public void SubscribeToSearchNotification()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed && this.searchHandlerLoggedUser == null)
				{
					this.searchHandlerLoggedUser = new SearchNotificationHandler(this.userContext);
					this.notificationHandlers.Add(this.searchHandlerLoggedUser);
					this.WireConnectionDroppedHandler(this.searchHandlerLoggedUser);
					this.searchHandlerLoggedUser.Subscribe();
				}
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000374C8 File Offset: 0x000356C8
		public void UnsubscribeForRowNotifications(string subscriptionId, SubscriptionParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::UnsubscribeForRowNotifications] SubscriptionId: {0} ChannelId: {1}", subscriptionId, parameters.ChannelId);
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.rowNotificationHandlerCache.ReleaseHandler(subscriptionId, parameters.ChannelId);
				}
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00037548 File Offset: 0x00035748
		public void ReleaseSubscription(string subscriptionId)
		{
			if (subscriptionId == null)
			{
				throw new ArgumentNullException("subscriptionId");
			}
			lock (this.syncRoot)
			{
				if (!this.isDisposed && this.unseenItemHandler != null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::ReleaseSubscription] Removing UnseenItem subscription for SubscriptionId: {0}", subscriptionId);
					this.unseenItemHandler.RemoveSubscription(subscriptionId);
					if (!this.unseenItemHandler.HasNotifiers())
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[OwaMapiNotificationManager::ReleaseSubscription] Disposing UnseenItem handler since there are no more notifiers active.");
						this.unseenItemHandler.Dispose();
						this.unseenItemHandler = null;
					}
				}
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000375F8 File Offset: 0x000357F8
		public void ReleaseSubscriptionsForChannelId(string channelId)
		{
			if (channelId == null)
			{
				throw new ArgumentNullException("channelId");
			}
			lock (this.syncRoot)
			{
				if (!this.isDisposed && this.rowNotificationHandlerCache != null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[OwaMapiNotificationManager::ReleaseSubscriptionsForChannelId] ChannelId: {0}", channelId);
					this.rowNotificationHandlerCache.ReleaseHandlersForChannelId(channelId);
				}
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00037674 File Offset: 0x00035874
		public void RefreshSubscriptions(ExTimeZone timeZone)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					foreach (MapiNotificationHandlerBase mapiNotificationHandlerBase in this.notificationHandlers)
					{
						mapiNotificationHandlerBase.NeedToReinitSubscriptions = true;
						mapiNotificationHandlerBase.Subscribe();
					}
					this.rowNotificationHandlerCache.RefreshSubscriptions(timeZone);
				}
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003770C File Offset: 0x0003590C
		public void CleanupSubscriptions()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					foreach (MapiNotificationHandlerBase mapiNotificationHandlerBase in this.notificationHandlers)
					{
						mapiNotificationHandlerBase.DisposeSubscriptions();
					}
					this.rowNotificationHandlerCache.DisposeSubscriptions();
				}
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003779C File Offset: 0x0003599C
		public void HandleConnectionDroppedNotification()
		{
			if (this.connectionDroppedNotificationHandler != null)
			{
				this.connectionDroppedNotificationHandler.HandleNotification(null);
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000377B4 File Offset: 0x000359B4
		public void StartRemoteKeepAliveTimer()
		{
			bool flag = this.isDisposed;
			bool flag2 = this.remoteKeepAliveTimer == null;
			if (flag2)
			{
				lock (this.syncRoot)
				{
					flag = this.isDisposed;
					if (!flag)
					{
						flag2 = (this.remoteKeepAliveTimer == null);
						if (flag2)
						{
							this.remoteKeepAliveTimer = new Timer(new TimerCallback(this.RemoteKeepAliveTimerCallback), null, 120000, 120000);
						}
					}
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "OwaMapiNotificationManager.StartRemoteKeepAliveTimer. isTimerNull: {0},  isDisposed: {1}.", flag2, flag);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00037854 File Offset: 0x00035A54
		private void RemoteKeepAliveTimerCallback(object state)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "OwaMapiNotificationManager.RemoteKeepAliveTimerCallback. Calling all registered handlers.");
			EventHandler<EventArgs> remoteKeepAliveEvent = this.RemoteKeepAliveEvent;
			if (remoteKeepAliveEvent != null)
			{
				remoteKeepAliveEvent(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000379F8 File Offset: 0x00035BF8
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaMapiNotificationManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing)
			{
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						lock (this.syncRoot)
						{
							if (this.hierarchyHandlerLoggedUser != null)
							{
								this.hierarchyHandlerLoggedUser.Dispose();
								this.hierarchyHandlerLoggedUser = null;
							}
							if (this.reminderHandlerLoggedUser != null)
							{
								this.reminderHandlerLoggedUser.Dispose();
								this.reminderHandlerLoggedUser = null;
							}
							if (this.newMailHandlerLoggedUser != null)
							{
								this.newMailHandlerLoggedUser.Dispose();
								this.newMailHandlerLoggedUser = null;
							}
							if (this.unseenItemHandler != null)
							{
								this.unseenItemHandler.Dispose();
								this.unseenItemHandler = null;
							}
							if (this.groupAssociationHandlerLoggedUser != null)
							{
								this.groupAssociationHandlerLoggedUser.Dispose();
								this.groupAssociationHandlerLoggedUser = null;
							}
							if (this.searchHandlerLoggedUser != null)
							{
								this.searchHandlerLoggedUser.Dispose();
								this.searchHandlerLoggedUser = null;
							}
							if (this.rowNotificationHandlerCache != null)
							{
								ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[OwaMapiNotificationManager.Dispose]. Calling this.rowNotificationHandlerCache.Clear()");
								this.rowNotificationHandlerCache.Clear();
								this.rowNotificationHandlerCache = null;
							}
							if (this.connectionDroppedNotificationHandler != null)
							{
								this.connectionDroppedNotificationHandler.Dispose();
								this.connectionDroppedNotificationHandler = null;
							}
							if (this.notificationHandlers != null)
							{
								this.notificationHandlers.Clear();
								this.notificationHandlers = null;
							}
							if (this.remoteKeepAliveTimer != null)
							{
								this.remoteKeepAliveTimer.Dispose();
								this.remoteKeepAliveTimer = null;
							}
							this.isDisposed = true;
						}
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "[OwaMapiNotificationManager.Dispose]. Unable to dispose object.  exception {0}", ex.Message);
				}
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00037A64 File Offset: 0x00035C64
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaMapiNotificationManager>(this);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00037A6C File Offset: 0x00035C6C
		private void InitializeConnectionDroppedHandler()
		{
			if (this.connectionDroppedNotificationHandler == null)
			{
				this.connectionDroppedNotificationHandler = new ConnectionDroppedNotificationHandler(this.userContext);
				this.notificationHandlers.Add(this.connectionDroppedNotificationHandler);
				this.connectionDroppedNotificationHandler.Subscribe();
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00037AA3 File Offset: 0x00035CA3
		private void WireConnectionDroppedHandler(MapiNotificationHandlerBase handler)
		{
			this.InitializeConnectionDroppedHandler();
			this.connectionDroppedNotificationHandler.OnConnectionDropped += handler.HandleConnectionDroppedNotification;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00037AC3 File Offset: 0x00035CC3
		private void BeforeDisposeRowNotificationHandler(ConnectionDroppedNotificationHandler.ConnectionDroppedEventHandler connectionDroppedEventHandler)
		{
			if (connectionDroppedEventHandler == null)
			{
				throw new ArgumentNullException("connectionDroppedEventHandler");
			}
			if (this.connectionDroppedNotificationHandler != null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "OwaMapiNotificationManager.BeforeDisposeRowNotificationHandler. Removing connection dropped event handler");
				this.connectionDroppedNotificationHandler.OnConnectionDropped -= connectionDroppedEventHandler;
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00037B00 File Offset: 0x00035D00
		private UserContext GetFullUserContext(string componentName)
		{
			UserContext userContext = this.userContext as UserContext;
			if (userContext == null)
			{
				throw new OwaInvalidOperationException(componentName + " must have a full user context to work");
			}
			return userContext;
		}

		// Token: 0x040008CC RID: 2252
		private const int RemoteKeepAliveTimerIntervalInMilliSeconds = 120000;

		// Token: 0x040008CD RID: 2253
		private IMailboxContext userContext;

		// Token: 0x040008CE RID: 2254
		private HierarchyNotificationHandler hierarchyHandlerLoggedUser;

		// Token: 0x040008CF RID: 2255
		private ReminderNotificationHandler reminderHandlerLoggedUser;

		// Token: 0x040008D0 RID: 2256
		private NewMailNotificationHandler newMailHandlerLoggedUser;

		// Token: 0x040008D1 RID: 2257
		private GroupAssociationNotificationHandler groupAssociationHandlerLoggedUser;

		// Token: 0x040008D2 RID: 2258
		private SearchNotificationHandler searchHandlerLoggedUser;

		// Token: 0x040008D3 RID: 2259
		private OwaMapiNotificationManager.RowNotificationHandlerCache rowNotificationHandlerCache;

		// Token: 0x040008D4 RID: 2260
		private ConnectionDroppedNotificationHandler connectionDroppedNotificationHandler;

		// Token: 0x040008D5 RID: 2261
		private UnseenItemNotificationHandler unseenItemHandler;

		// Token: 0x040008D6 RID: 2262
		private List<MapiNotificationHandlerBase> notificationHandlers;

		// Token: 0x040008D7 RID: 2263
		private object syncRoot = new object();

		// Token: 0x040008D8 RID: 2264
		private bool isDisposed;

		// Token: 0x040008D9 RID: 2265
		private Timer remoteKeepAliveTimer;

		// Token: 0x02000196 RID: 406
		internal class RowNotificationHandlerCache
		{
			// Token: 0x06000E99 RID: 3737 RVA: 0x00037B2E File Offset: 0x00035D2E
			internal RowNotificationHandlerCache(IMailboxContext userContext)
			{
				this.handlerCache = new Dictionary<string, RowNotificationHandler>();
				this.channelIdCache = new Dictionary<string, List<string>>();
				this.userContext = userContext;
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00037B53 File Offset: 0x00035D53
			internal Dictionary<string, RowNotificationHandler> HandlerCache
			{
				get
				{
					return this.handlerCache;
				}
			}

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00037B5B File Offset: 0x00035D5B
			internal Dictionary<string, List<string>> ChannelIdCache
			{
				get
				{
					return this.channelIdCache;
				}
			}

			// Token: 0x06000E9C RID: 3740 RVA: 0x00037B64 File Offset: 0x00035D64
			internal bool TryGetHandler(string subscriptionId, out RowNotificationHandler handler)
			{
				if (subscriptionId == null)
				{
					throw new ArgumentNullException("subscriptionId");
				}
				if (this.handlerCache == null)
				{
					throw new OwaInvalidOperationException("this.handlerCache may not be null");
				}
				if (this.channelIdCache == null)
				{
					throw new OwaInvalidOperationException("this.channelIdCache may not be null");
				}
				handler = null;
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::TryGetHandler] TryGetHandle for SubscriptionId: {0}", subscriptionId);
				if (this.handlerCache.ContainsKey(subscriptionId))
				{
					handler = this.handlerCache[subscriptionId];
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int>((long)this.GetHashCode(), "[RowNotificationHandlerCache::TryGetHandler] Found handler for SubscriptionId: {0}. Current RefCount: {1}", subscriptionId, handler.RefCount);
					return true;
				}
				return false;
			}

			// Token: 0x06000E9D RID: 3741 RVA: 0x00037BFC File Offset: 0x00035DFC
			internal void AddHandler(string subscriptionId, RowNotificationHandler handler, string channelId)
			{
				if (subscriptionId == null)
				{
					throw new ArgumentNullException("subscriptionId");
				}
				if (handler == null)
				{
					throw new ArgumentNullException("handler");
				}
				if (this.handlerCache == null)
				{
					throw new OwaInvalidOperationException("this.handlerCache may not be null");
				}
				if (this.channelIdCache == null)
				{
					throw new OwaInvalidOperationException("this.channelIdCache may not be null");
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "[RowNotificationHandlerCache::AddHandler] Adding handler for SubscriptionId: {0}. ChannelId: {1}. Current RefCount: {2}", subscriptionId, channelId, handler.RefCount);
				if (!this.handlerCache.ContainsKey(subscriptionId))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::AddHandler] Adding new handler for SubscriptionId: {0}. ChannelId: {1}. New RefCount: 1", subscriptionId, channelId);
					handler.RefCount = 1;
					this.handlerCache[subscriptionId] = handler;
					if (channelId != null)
					{
						this.TryAddSubscriptionIdToChannelIdCache(subscriptionId, channelId);
						return;
					}
				}
				else if (channelId != null)
				{
					bool flag = true;
					if (this.TryAddSubscriptionIdToChannelIdCache(subscriptionId, channelId))
					{
						handler.RefCount++;
						flag = false;
					}
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[RowNotificationHandlerCache::AddHandler] Is duplicate subscription request: '{0}' for existing unique view handler. SubscriptionId: {1}. ChannelId: {2}. RefCount: {3}", new object[]
					{
						flag,
						subscriptionId,
						channelId,
						handler.RefCount
					});
				}
			}

			// Token: 0x06000E9E RID: 3742 RVA: 0x00037D0C File Offset: 0x00035F0C
			internal void ReleaseHandler(string subscriptionId, string channelId)
			{
				if (subscriptionId == null)
				{
					throw new ArgumentNullException("subscriptionId");
				}
				if (this.handlerCache == null)
				{
					throw new OwaInvalidOperationException("this.handlerCache may not be null");
				}
				if (this.channelIdCache == null)
				{
					throw new OwaInvalidOperationException("this.channelIdCache may not be null");
				}
				RowNotificationHandler rowNotificationHandler = null;
				if (this.handlerCache.TryGetValue(subscriptionId, out rowNotificationHandler))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[RowNotificationHandlerCache::ReleaseHandler] releasing handler for SubscriptionId: {0} ChannelId: {1} Old RefCount: {2} New RefCount: {3}", new object[]
					{
						rowNotificationHandler.SubscriptionId,
						channelId,
						rowNotificationHandler.RefCount,
						rowNotificationHandler.RefCount - 1
					});
					if (--rowNotificationHandler.RefCount == 0)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::ReleaseHandler] Disposing handler for SubscriptionId: {0} ChannelId: {1}", subscriptionId, channelId);
						this.handlerCache.Remove(subscriptionId);
						if (!this.userContext.MailboxSessionLockedByCurrentThread())
						{
							try
							{
								try
								{
									this.userContext.LockAndReconnectMailboxSession(3000);
									rowNotificationHandler.Dispose();
									rowNotificationHandler = null;
								}
								catch (OwaLockTimeoutException)
								{
									ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::ReleaseHandler] Disposing handler for SubscriptionId: {0} ChannelId: {1} Failed to acquire mbx lock", subscriptionId, channelId);
								}
								catch (StoragePermanentException ex)
								{
									ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "[RowNotificationHandlerCache::ReleaseHandler]. Unable to dispose object.  exception {0}", ex.Message);
								}
								catch (StorageTransientException ex2)
								{
									ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "[RowNotificationHandlerCache::ReleaseHandler]. Unable to dispose object.  exception {0}", ex2.Message);
								}
								goto IL_15C;
							}
							finally
							{
								this.userContext.UnlockAndDisconnectMailboxSession();
							}
						}
						rowNotificationHandler.Dispose();
						rowNotificationHandler = null;
					}
				}
				IL_15C:
				if (channelId == null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::ReleaseHandler] Returning without updating channel id lookup cache for non-unique view handler for SubscriptionId: {0}. ChannelId is null.", subscriptionId);
					return;
				}
				this.RemoveSubscriptionIdFromChannelIdCache(subscriptionId, channelId);
			}

			// Token: 0x06000E9F RID: 3743 RVA: 0x00037ECC File Offset: 0x000360CC
			internal void ReleaseHandlersForChannelId(string channelId)
			{
				if (channelId == null)
				{
					throw new ArgumentNullException("channelId");
				}
				if (this.handlerCache == null)
				{
					throw new OwaInvalidOperationException("this.handlerCache may not be null");
				}
				if (this.channelIdCache == null)
				{
					throw new OwaInvalidOperationException("this.channelIdCache may not be null");
				}
				List<string> list = new List<string>();
				if (this.channelIdCache.ContainsKey(channelId))
				{
					list = this.channelIdCache[channelId];
					if (list == null)
					{
						throw new OwaInvalidOperationException("channelIdCache list of subscription ids for channel id: {0} may not be null");
					}
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<int, string>((long)this.GetHashCode(), "RowNotificationHandlerCache::ReleaseHandlersForChannelId - subscription list count is: {0} for channelId being removed: {1}", list.Count, channelId);
					this.channelIdCache.Remove(channelId);
					for (int i = 0; i < list.Count; i++)
					{
						this.ReleaseHandler(list[i], channelId);
					}
				}
			}

			// Token: 0x06000EA0 RID: 3744 RVA: 0x00037F88 File Offset: 0x00036188
			internal void RefreshSubscriptions(ExTimeZone timeZone)
			{
				if (timeZone == null)
				{
					throw new ArgumentNullException("timeZone");
				}
				if (this.handlerCache == null)
				{
					throw new OwaInvalidOperationException("this.handlerCache may not be null");
				}
				if (this.channelIdCache == null)
				{
					throw new OwaInvalidOperationException("this.channelIdCache may not be null");
				}
				foreach (RowNotificationHandler rowNotificationHandler in this.handlerCache.Values)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::RefreshSubscriptions] Calling Subscribe after Resetting timeZone and setting NeedToReinitSubscriptions = true for SubscriptionId: {0}", rowNotificationHandler.SubscriptionId);
					rowNotificationHandler.TimeZone = timeZone;
					rowNotificationHandler.NeedToReinitSubscriptions = true;
					if (!Globals.Owa2ServerUnitTestsHook)
					{
						rowNotificationHandler.Subscribe();
					}
				}
			}

			// Token: 0x06000EA1 RID: 3745 RVA: 0x00038044 File Offset: 0x00036244
			internal void DisposeSubscriptions()
			{
				if (this.handlerCache == null)
				{
					throw new OwaInvalidOperationException("this.handlerCache may not be null");
				}
				foreach (RowNotificationHandler rowNotificationHandler in this.handlerCache.Values)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::DisposeSubsriptions] Calling Dispose after Resetting timeZone", rowNotificationHandler.SubscriptionId);
					if (!Globals.Owa2ServerUnitTestsHook)
					{
						rowNotificationHandler.DisposeSubscriptions();
					}
				}
			}

			// Token: 0x06000EA2 RID: 3746 RVA: 0x00038194 File Offset: 0x00036394
			internal void Clear()
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[RowNotificationHandlerCache::Clear] disposing all row notification handlers");
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						if (this.handlerCache == null)
						{
							throw new OwaInvalidOperationException("this.handlerCache may not be null");
						}
						if (this.channelIdCache == null)
						{
							throw new OwaInvalidOperationException("this.channelIdCache may not be null");
						}
						foreach (string key in this.handlerCache.Keys)
						{
							RowNotificationHandler rowNotificationHandler = this.handlerCache[key];
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::Clear] Disposing handler for SubscriptionId: {0}", rowNotificationHandler.SubscriptionId);
							rowNotificationHandler.Dispose();
						}
						this.handlerCache = new Dictionary<string, RowNotificationHandler>();
						this.channelIdCache = new Dictionary<string, List<string>>();
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.Dispose Unable to dispose object.  exception {0}", ex.Message);
				}
			}

			// Token: 0x06000EA3 RID: 3747 RVA: 0x000381FC File Offset: 0x000363FC
			private bool TryAddSubscriptionIdToChannelIdCache(string subscriptionId, string channelId)
			{
				if (subscriptionId == null)
				{
					throw new OwaInvalidOperationException("subscriptionId may not be null");
				}
				if (channelId == null)
				{
					throw new OwaInvalidOperationException("channelId may not be null");
				}
				bool result = false;
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::TryAddSubscriptionIdToChannelIdCache] SubscriptionId: {0}. ChannelId: {1}.", subscriptionId, channelId);
				List<string> list = new List<string>();
				if (this.channelIdCache.ContainsKey(channelId))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::TryAddSubscriptionIdToChannelIdCache] for pre-existing entry for SubscriptionId: {0}. ChannelId: {1}.", subscriptionId, channelId);
					list = this.channelIdCache[channelId];
				}
				if (!list.Contains(subscriptionId))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::TryAddSubscriptionIdToChannelIdCache] Adding new subcription id: {0}. for channelId: {1}.", subscriptionId, channelId);
					list.Add(subscriptionId);
					result = true;
				}
				this.channelIdCache[channelId] = list;
				return result;
			}

			// Token: 0x06000EA4 RID: 3748 RVA: 0x000382B0 File Offset: 0x000364B0
			private void RemoveSubscriptionIdFromChannelIdCache(string subscriptionId, string channelId)
			{
				if (subscriptionId == null)
				{
					throw new OwaInvalidOperationException("subscriptionId may not be null");
				}
				if (channelId == null)
				{
					throw new OwaInvalidOperationException("channelId may not be null");
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::RemoveSubscriptionIdFromChannelIdCache] SubscriptionId: {0}. ChannelId: {1}.", subscriptionId, channelId);
				List<string> subIds = new List<string>();
				if (this.channelIdCache.ContainsKey(channelId))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::RemoveSubscriptionIdFromChannelIdCache] Found entry for ChannelId: {0}. SubscriptionId: {1}.", channelId, subscriptionId);
					subIds = this.channelIdCache[channelId];
					this.RemoveSubscriptionIdFromList(subscriptionId, subIds, channelId);
				}
			}

			// Token: 0x06000EA5 RID: 3749 RVA: 0x00038334 File Offset: 0x00036534
			private void RemoveSubscriptionIdFromList(string subscriptionId, List<string> subIds, string channelId)
			{
				if (subscriptionId == null)
				{
					throw new OwaInvalidOperationException("subscriptionId may not be null");
				}
				if (subIds == null)
				{
					throw new ArgumentNullException("subIds");
				}
				if (channelId == null)
				{
					throw new OwaInvalidOperationException("channelId may not be null");
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "[RowNotificationHandlerCache::RemoveSubscriptionIdFromList] SubscriptionId: {0}. channelId: {1}. subscriptionId list count: {2}", subscriptionId, channelId, subIds.Count);
				if (subIds.Contains(subscriptionId))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::RemoveSubscriptionIdFromList] Removing subscriptionId: {0} from channel id lookup cache list", subscriptionId);
					subIds.Remove(subscriptionId);
					if (subIds.Count == 0)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[RowNotificationHandlerCache::RemoveSubscriptionIdFromList] Removing channelId: {0} from channel id lookup cache", channelId);
						this.channelIdCache.Remove(channelId);
					}
				}
			}

			// Token: 0x040008DB RID: 2267
			private Dictionary<string, RowNotificationHandler> handlerCache;

			// Token: 0x040008DC RID: 2268
			private Dictionary<string, List<string>> channelIdCache;

			// Token: 0x040008DD RID: 2269
			private IMailboxContext userContext;
		}
	}
}
