using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000156 RID: 342
	internal class BrokerGateway : DisposeTrackableBase, IBrokerGateway
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x0002E366 File Offset: 0x0002C566
		protected BrokerGateway()
		{
			this.subscriptionHandlers = new ConcurrentDictionary<Guid, BrokerHandler>();
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002E37C File Offset: 0x0002C57C
		public static BrokerGateway Instance
		{
			get
			{
				if (BrokerGateway.instance == null)
				{
					lock (BrokerGateway.syncRoot)
					{
						if (BrokerGateway.instance == null)
						{
							using (DisposeGuard disposeGuard = default(DisposeGuard))
							{
								BrokerGateway brokerGateway = new BrokerGateway();
								disposeGuard.Add<BrokerGateway>(brokerGateway);
								brokerGateway.Initialize();
								disposeGuard.Success();
								BrokerGateway.instance = brokerGateway;
							}
						}
					}
				}
				return BrokerGateway.instance;
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002E410 File Offset: 0x0002C610
		public static void Shutdown()
		{
			BrokerGateway comparand = BrokerGateway.instance;
			BrokerGateway brokerGateway = Interlocked.CompareExchange<BrokerGateway>(ref BrokerGateway.instance, null, comparand);
			if (brokerGateway != null)
			{
				brokerGateway.Dispose();
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002E439 File Offset: 0x0002C639
		public void Subscribe(BrokerSubscription brokerSubscription, BrokerHandler handler)
		{
			ArgumentValidator.ThrowIfNull("brokerSubscription", brokerSubscription);
			ArgumentValidator.ThrowIfNull("handler", handler);
			this.broker.Subscribe(brokerSubscription);
			this.subscriptionHandlers[brokerSubscription.SubscriptionId] = handler;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002E470 File Offset: 0x0002C670
		public void Unsubscribe(BrokerSubscription brokerSubscription)
		{
			ArgumentValidator.ThrowIfNull("brokerSubscription", brokerSubscription);
			BrokerHandler brokerHandler;
			if (this.subscriptionHandlers.TryRemove(brokerSubscription.SubscriptionId, out brokerHandler))
			{
				this.broker.Unsubscribe(brokerSubscription);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002E4A9 File Offset: 0x0002C6A9
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "BrokerNotificationManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing && this.broker != null)
			{
				this.broker.Dispose();
				this.broker = null;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002E4DF File Offset: 0x0002C6DF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BrokerGateway>(this);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002E4E7 File Offset: 0x0002C6E7
		protected virtual INotificationBrokerClient GetBroker()
		{
			return new NotificationBrokerClient();
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002E4EE File Offset: 0x0002C6EE
		protected void Initialize()
		{
			this.broker = this.GetBroker();
			this.broker.StartNotificationCallbacks(new Action<BrokerNotification>(this.ProcessNotifications));
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002E514 File Offset: 0x0002C714
		private void ProcessNotifications(BrokerNotification notification)
		{
			BrokerHandler brokerHandler = null;
			if (this.subscriptionHandlers.TryGetValue(notification.SubscriptionId, out brokerHandler))
			{
				brokerHandler.HandleNotification(notification);
			}
		}

		// Token: 0x040007C5 RID: 1989
		private static BrokerGateway instance;

		// Token: 0x040007C6 RID: 1990
		private static readonly object syncRoot = new object();

		// Token: 0x040007C7 RID: 1991
		private INotificationBrokerClient broker;

		// Token: 0x040007C8 RID: 1992
		private ConcurrentDictionary<Guid, BrokerHandler> subscriptionHandlers;
	}
}
