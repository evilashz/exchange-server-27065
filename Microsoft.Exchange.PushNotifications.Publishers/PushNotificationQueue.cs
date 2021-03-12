using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000016 RID: 22
	internal class PushNotificationQueue<TNotif> where TNotif : PushNotification
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003FBA File Offset: 0x000021BA
		public PushNotificationQueue(int capacity)
		{
			this.notificationQueue = new BlockingCollection<PushNotificationQueueItem<TNotif>>(capacity);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FCE File Offset: 0x000021CE
		public virtual bool TryAdd(PushNotificationQueueItem<TNotif> item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return this.notificationQueue.TryAdd(item, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FDE File Offset: 0x000021DE
		public virtual bool TryTake(out PushNotificationQueueItem<TNotif> item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return this.notificationQueue.TryTake(out item, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003FEE File Offset: 0x000021EE
		public virtual IEnumerable<PushNotificationQueueItem<TNotif>> GetConsumingEnumerable(CancellationToken cancellationToken)
		{
			return this.notificationQueue.GetConsumingEnumerable(cancellationToken);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FFC File Offset: 0x000021FC
		public virtual void CompleteAdding()
		{
			this.notificationQueue.CompleteAdding();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004009 File Offset: 0x00002209
		public virtual void Dispose()
		{
			this.notificationQueue.Dispose();
		}

		// Token: 0x04000038 RID: 56
		private BlockingCollection<PushNotificationQueueItem<TNotif>> notificationQueue;
	}
}
