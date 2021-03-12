using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200015A RID: 346
	internal sealed class BrokerNotificationManager : DisposeTrackableBase, INotificationManager, IDisposable
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002EA1C File Offset: 0x0002CC1C
		internal BrokerNotificationManager(IMailboxContext userContext)
		{
			this.userContext = userContext;
			this.mapiNotificationManager = new OwaMapiNotificationManager(userContext);
			this.activeHandlers = new Dictionary<string, BrokerHandlerReferenceCounter>();
			this.userContext.PendingRequestManager.KeepAlive += this.KeepHandlersAlive;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000CA8 RID: 3240 RVA: 0x0002EA74 File Offset: 0x0002CC74
		// (remove) Token: 0x06000CA9 RID: 3241 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
		public event EventHandler<EventArgs> RemoteKeepAliveEvent
		{
			add
			{
				lock (this.syncRoot)
				{
					if (!this.isDisposed)
					{
						this.mapiNotificationManager.RemoteKeepAliveEvent += value;
					}
				}
			}
			remove
			{
				lock (this.syncRoot)
				{
					if (!this.isDisposed)
					{
						this.mapiNotificationManager.RemoteKeepAliveEvent -= value;
					}
				}
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002EB14 File Offset: 0x0002CD14
		public SearchNotificationHandler SearchNotificationHandler
		{
			get
			{
				return this.mapiNotificationManager.SearchNotificationHandler;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002EB24 File Offset: 0x0002CD24
		public void SubscribeToHierarchyNotification(string subscriptionId)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.SubscribeToHierarchyNotification(subscriptionId);
				}
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002EB74 File Offset: 0x0002CD74
		public void SubscribeToRowNotification(string subscriptionId, SubscriptionParameters parameters, ExTimeZone timeZone, CallContext callContext, bool remoteSubscription)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.SubscribeToRowNotification(subscriptionId, parameters, timeZone, callContext, remoteSubscription);
				}
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002EBCC File Offset: 0x0002CDCC
		public void SubscribeToReminderNotification(string subscriptionId)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.SubscribeToReminderNotification(subscriptionId);
				}
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002EC1C File Offset: 0x0002CE1C
		public void SubscribeToNewMailNotification(string subscriptionId, SubscriptionParameters parameters)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.SubscribeToNewMailNotification(subscriptionId, parameters);
				}
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002EC70 File Offset: 0x0002CE70
		public string SubscribeToUnseenItemNotification(string subscriptionId, UserMailboxLocator mailboxLocator, IRecipientSession adSession)
		{
			string result;
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					result = null;
				}
				else
				{
					result = this.mapiNotificationManager.SubscribeToUnseenItemNotification(subscriptionId, mailboxLocator, adSession);
				}
			}
			return result;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002ECF4 File Offset: 0x0002CEF4
		public void SubscribeToUnseenCountNotification(string subscriptionId, SubscriptionParameters parameters, IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNull("parameters", parameters);
			ArgumentValidator.ThrowIfNull("MailboxId", parameters.MailboxId);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			this.Subscribe(subscriptionId, parameters.ChannelId, () => new UnseenCountBrokerHandler(subscriptionId, parameters, this.userContext, adSession));
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002ED8B File Offset: 0x0002CF8B
		public void UnsubscribeToUnseenCountNotification(string subscriptionId, SubscriptionParameters parameters)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNull("parameters", parameters);
			ArgumentValidator.ThrowIfNull("MailboxId", parameters.MailboxId);
			this.Unsubscribe(subscriptionId, parameters);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		public void SubscribeToGroupAssociationNotification(string subscriptionId, IRecipientSession adSession)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.SubscribeToGroupAssociationNotification(subscriptionId, adSession);
				}
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002EE10 File Offset: 0x0002D010
		public void SubscribeToSearchNotification()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.SubscribeToSearchNotification();
				}
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002EE60 File Offset: 0x0002D060
		public void UnsubscribeForRowNotifications(string subscriptionId, SubscriptionParameters parameters)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.UnsubscribeForRowNotifications(subscriptionId, parameters);
				}
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002EEB4 File Offset: 0x0002D0B4
		public void ReleaseSubscription(string subscriptionId)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.ReleaseSubscription(subscriptionId);
				}
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002EF04 File Offset: 0x0002D104
		public void ReleaseSubscriptionsForChannelId(string channelId)
		{
			List<KeyValuePair<string, BrokerHandlerReferenceCounter>> list = new List<KeyValuePair<string, BrokerHandlerReferenceCounter>>();
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.mapiNotificationManager.ReleaseSubscriptionsForChannelId(channelId);
				foreach (KeyValuePair<string, BrokerHandlerReferenceCounter> item in this.activeHandlers)
				{
					item.Value.Remove(channelId);
					if (item.Value.Count == 0)
					{
						list.Add(item);
					}
				}
				foreach (KeyValuePair<string, BrokerHandlerReferenceCounter> keyValuePair in list)
				{
					this.activeHandlers.Remove(keyValuePair.Key);
				}
			}
			foreach (KeyValuePair<string, BrokerHandlerReferenceCounter> keyValuePair2 in list)
			{
				keyValuePair2.Value.Dispose();
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002F04C File Offset: 0x0002D24C
		public void RefreshSubscriptions(ExTimeZone timeZone)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.RefreshSubscriptions(timeZone);
				}
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002F09C File Offset: 0x0002D29C
		public void CleanupSubscriptions()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.CleanupSubscriptions();
				}
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		public void HandleConnectionDroppedNotification()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.HandleConnectionDroppedNotification();
				}
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002F13C File Offset: 0x0002D33C
		public void StartRemoteKeepAliveTimer()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					this.mapiNotificationManager.StartRemoteKeepAliveTimer();
				}
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002F270 File Offset: 0x0002D470
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "BrokerNotificationManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing)
			{
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						lock (this.syncRoot)
						{
							this.isDisposed = true;
							if (this.userContext != null && this.userContext.PendingRequestManager != null)
							{
								this.userContext.PendingRequestManager.KeepAlive -= this.KeepHandlersAlive;
							}
							foreach (BrokerHandlerReferenceCounter brokerHandlerReferenceCounter in this.activeHandlers.Values)
							{
								brokerHandlerReferenceCounter.Dispose();
							}
							this.activeHandlers.Clear();
							this.activeHandlers = null;
							if (this.mapiNotificationManager != null)
							{
								this.mapiNotificationManager.Dispose();
								this.mapiNotificationManager = null;
							}
						}
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "[BrokerNotificationManager.Dispose]. Unable to dispose object.  exception {0}", ex.Message);
				}
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BrokerNotificationManager>(this);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002F2E4 File Offset: 0x0002D4E4
		private void Subscribe(string subscriptionId, string channelId, Func<BrokerHandler> createHandlerDelegate)
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					BrokerHandlerReferenceCounter brokerHandlerReferenceCounter;
					if (!this.activeHandlers.TryGetValue(subscriptionId, out brokerHandlerReferenceCounter))
					{
						brokerHandlerReferenceCounter = new BrokerHandlerReferenceCounter(createHandlerDelegate);
						this.activeHandlers[subscriptionId] = brokerHandlerReferenceCounter;
					}
					brokerHandlerReferenceCounter.Add(channelId);
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002F354 File Offset: 0x0002D554
		private void Unsubscribe(string subscriptionId, SubscriptionParameters parameters)
		{
			BrokerHandlerReferenceCounter brokerHandlerReferenceCounter = null;
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					return;
				}
				BrokerHandlerReferenceCounter brokerHandlerReferenceCounter2;
				if (this.activeHandlers.TryGetValue(subscriptionId, out brokerHandlerReferenceCounter2))
				{
					brokerHandlerReferenceCounter2.Remove(parameters.ChannelId);
					if (brokerHandlerReferenceCounter2.Count == 0)
					{
						this.activeHandlers.Remove(subscriptionId);
						brokerHandlerReferenceCounter = brokerHandlerReferenceCounter2;
					}
				}
			}
			if (brokerHandlerReferenceCounter != null)
			{
				brokerHandlerReferenceCounter.Dispose();
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002F3D8 File Offset: 0x0002D5D8
		private void KeepHandlersAlive(object sender, EventArgs e)
		{
			ExDateTime now = ExDateTime.Now;
			foreach (BrokerHandlerReferenceCounter brokerHandlerReferenceCounter in this.GetActiveHandlers())
			{
				brokerHandlerReferenceCounter.KeepAlive(now);
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002F40C File Offset: 0x0002D60C
		private BrokerHandlerReferenceCounter[] GetActiveHandlers()
		{
			BrokerHandlerReferenceCounter[] result = Array<BrokerHandlerReferenceCounter>.Empty;
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					result = this.activeHandlers.Values.ToArray<BrokerHandlerReferenceCounter>();
				}
			}
			return result;
		}

		// Token: 0x040007D4 RID: 2004
		private OwaMapiNotificationManager mapiNotificationManager;

		// Token: 0x040007D5 RID: 2005
		private IMailboxContext userContext;

		// Token: 0x040007D6 RID: 2006
		private Dictionary<string, BrokerHandlerReferenceCounter> activeHandlers;

		// Token: 0x040007D7 RID: 2007
		private object syncRoot = new object();

		// Token: 0x040007D8 RID: 2008
		private bool isDisposed;
	}
}
