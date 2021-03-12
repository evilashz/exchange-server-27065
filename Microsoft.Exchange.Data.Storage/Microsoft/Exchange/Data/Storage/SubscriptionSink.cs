using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200070F RID: 1807
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionSink
	{
		// Token: 0x0600477B RID: 18299 RVA: 0x0012FF98 File Offset: 0x0012E198
		internal SubscriptionSink(SubscriptionsManager manager, bool needsNotifyManager)
		{
			this.manager = manager;
			this.subscription = null;
			this.needsNotifyManager = needsNotifyManager;
			this.writer = new SubscriptionSink.NotificationNode(null);
			this.reader = this.writer;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x0012FFCD File Offset: 0x0012E1CD
		internal SubscriptionSink(Subscription subscription)
		{
			this.subscription = subscription;
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x0600477D RID: 18301 RVA: 0x0012FFDC File Offset: 0x0012E1DC
		internal int Count
		{
			get
			{
				return this.pendingNotificationCount;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x0012FFE4 File Offset: 0x0012E1E4
		internal bool HasDroppedNotification
		{
			get
			{
				return this.hasDroppedNotification;
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x0012FFEC File Offset: 0x0012E1EC
		private bool Enqueue(MapiNotification mapiNotification)
		{
			if (this.pendingNotificationCount > 256)
			{
				return false;
			}
			SubscriptionSink.NotificationNode next = new SubscriptionSink.NotificationNode(mapiNotification);
			this.writer.Next = next;
			this.writer = this.writer.Next;
			Interlocked.Increment(ref this.pendingNotificationCount);
			return true;
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x0013003C File Offset: 0x0012E23C
		internal MapiNotification Dequeue()
		{
			if (this.pendingNotificationCount <= 0)
			{
				return null;
			}
			Interlocked.Decrement(ref this.pendingNotificationCount);
			this.reader = this.reader.Next;
			return this.reader.Data;
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00130080 File Offset: 0x0012E280
		internal void OnNotify(MapiNotification notification)
		{
			if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "SubscriptionSink::OnNotify. Capacity = {0}, Count = {1}, Notification = {2}, needsNotifyManager = {3}.", new object[]
				{
					256,
					this.pendingNotificationCount,
					(notification == null) ? "<Null>" : notification.NotificationType.ToString(),
					this.needsNotifyManager
				});
			}
			Notification notification2;
			if (this.subscription == null)
			{
				if (!this.Enqueue(notification))
				{
					ExTraceGlobals.StorageTracer.TraceError<int, int>((long)this.GetHashCode(), "SubscriptionSink::OnNotify. The sink is full. Capacity = {0}, Count = {1}", 256, this.pendingNotificationCount);
					this.hasDroppedNotification = true;
				}
				if (this.needsNotifyManager)
				{
					this.manager.SendNotificationAlert();
					return;
				}
			}
			else if (this.subscription.TryCreateXsoNotification(notification, out notification2))
			{
				this.subscription.InvokeHandler(notification2);
			}
		}

		// Token: 0x04002718 RID: 10008
		private const int Capacity = 256;

		// Token: 0x04002719 RID: 10009
		private readonly SubscriptionsManager manager;

		// Token: 0x0400271A RID: 10010
		private readonly Subscription subscription;

		// Token: 0x0400271B RID: 10011
		private readonly bool needsNotifyManager;

		// Token: 0x0400271C RID: 10012
		private int pendingNotificationCount;

		// Token: 0x0400271D RID: 10013
		private bool hasDroppedNotification;

		// Token: 0x0400271E RID: 10014
		private SubscriptionSink.NotificationNode writer;

		// Token: 0x0400271F RID: 10015
		private SubscriptionSink.NotificationNode reader;

		// Token: 0x02000710 RID: 1808
		private class NotificationNode
		{
			// Token: 0x06004782 RID: 18306 RVA: 0x00130167 File Offset: 0x0012E367
			internal NotificationNode(MapiNotification data)
			{
				this.Data = data;
				this.Next = null;
			}

			// Token: 0x04002720 RID: 10016
			internal readonly MapiNotification Data;

			// Token: 0x04002721 RID: 10017
			internal SubscriptionSink.NotificationNode Next;
		}
	}
}
