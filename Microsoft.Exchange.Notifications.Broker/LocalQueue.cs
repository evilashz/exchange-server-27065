using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000019 RID: 25
	public class LocalQueue
	{
		// Token: 0x06000107 RID: 263 RVA: 0x000064E5 File Offset: 0x000046E5
		internal LocalQueue()
		{
			this.notifications = new Queue<BrokerNotification>();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006504 File Offset: 0x00004704
		public int Count
		{
			get
			{
				int count;
				lock (this.mutex)
				{
					count = this.notifications.Count;
				}
				return count;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000654C File Offset: 0x0000474C
		internal void Put(BrokerNotification notification)
		{
			Action<BrokerNotification> action = null;
			lock (this.mutex)
			{
				if (this.notifications.Count >= 2000)
				{
					this.notifications.Clear();
				}
				this.notifications.Enqueue(notification);
				if (this.waitingConsumerCallback != null)
				{
					action = this.waitingConsumerCallback;
					this.waitingConsumerCallback = null;
				}
			}
			if (action != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Consumer is already waiting for this notification, delivering right away");
				action(notification);
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000065E8 File Offset: 0x000047E8
		internal void Get(Guid ackNotificationId, Action<BrokerNotification> consumerCallback)
		{
			Action<BrokerNotification> action = null;
			BrokerNotification brokerNotification = null;
			lock (this.mutex)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "There are {0} notifications in the queue", this.notifications.Count);
				if (this.waitingConsumerCallback != null)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Overwriting previously waiting consumer callback");
					this.waitingConsumerCallback = null;
				}
				if (this.notifications.Count > 0)
				{
					brokerNotification = this.notifications.Peek();
					if (brokerNotification.NotificationId == ackNotificationId)
					{
						ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Previous notification acknowledged");
						this.notifications.Dequeue();
						this.Get(Guid.Empty, consumerCallback);
						return;
					}
					action = consumerCallback;
				}
				else
				{
					this.waitingConsumerCallback = consumerCallback;
				}
			}
			if (action != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "There is already a notification in the queue, delivering right away");
				action(brokerNotification);
			}
		}

		// Token: 0x04000079 RID: 121
		internal const int MaximumQueueLength = 2000;

		// Token: 0x0400007A RID: 122
		private object mutex = new object();

		// Token: 0x0400007B RID: 123
		private Queue<BrokerNotification> notifications;

		// Token: 0x0400007C RID: 124
		private Action<BrokerNotification> waitingConsumerCallback;
	}
}
