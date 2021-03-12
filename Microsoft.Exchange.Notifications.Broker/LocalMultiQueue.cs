using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000018 RID: 24
	internal class LocalMultiQueue
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00006474 File Offset: 0x00004674
		internal LocalMultiQueue()
		{
			this.queues = new LocalQueue[3];
			for (int i = 0; i < 3; i++)
			{
				this.queues[i] = new LocalQueue();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000064AC File Offset: 0x000046AC
		internal static LocalMultiQueue Singleton
		{
			get
			{
				if (LocalMultiQueue.singleton == null)
				{
					LocalMultiQueue.singleton = new LocalMultiQueue();
				}
				return LocalMultiQueue.singleton;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000064C4 File Offset: 0x000046C4
		internal void Put(ConsumerId consumerId, BrokerNotification notification)
		{
			this.queues[(int)consumerId].Put(notification);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000064D4 File Offset: 0x000046D4
		internal void Get(ConsumerId consumerId, Guid ackNotificationId, Action<BrokerNotification> consumerCallback)
		{
			this.queues[(int)consumerId].Get(ackNotificationId, consumerCallback);
		}

		// Token: 0x04000077 RID: 119
		private static LocalMultiQueue singleton;

		// Token: 0x04000078 RID: 120
		private LocalQueue[] queues;
	}
}
