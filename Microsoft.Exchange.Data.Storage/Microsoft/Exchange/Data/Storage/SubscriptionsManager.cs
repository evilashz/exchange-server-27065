using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200070E RID: 1806
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SubscriptionsManager : DisposableObject
	{
		// Token: 0x06004771 RID: 18289 RVA: 0x0012FB86 File Offset: 0x0012DD86
		internal SubscriptionsManager()
		{
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x0012FBA4 File Offset: 0x0012DDA4
		internal void RegisterSubscription(Subscription subscription)
		{
			lock (this.lockSubscriptionQueue)
			{
				this.subscriptionQueue.Add(subscription);
			}
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x0012FBEC File Offset: 0x0012DDEC
		internal void UnRegisterSubscription(Subscription subscription)
		{
			lock (this.lockSubscriptionQueue)
			{
				this.subscriptionQueue.Remove(subscription);
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x0012FC34 File Offset: 0x0012DE34
		internal int SubscriptionCount
		{
			get
			{
				int count;
				lock (this.lockSubscriptionQueue)
				{
					count = this.subscriptionQueue.Count;
				}
				return count;
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x0012FC88 File Offset: 0x0012DE88
		internal void SendNotificationAlert()
		{
			ExTraceGlobals.StorageTracer.TraceDebug<bool>((long)this.GetHashCode(), "SubscriptionManager::SendNotificationAlert. isDeliveryThreadAlive = {0}.", this.isDeliveryThreadAlive);
			lock (this.lockSubscriptionQueue)
			{
				if (!this.isDeliveryThreadAlive)
				{
					if (ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						this.DeliverNotifications(state);
					}))
					{
						this.isDeliveryThreadAlive = true;
					}
				}
			}
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x0012FD0C File Offset: 0x0012DF0C
		private void DeliverNotifications(object state)
		{
			if (base.IsDisposed)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			while (!flag2)
			{
				try
				{
					bool flag3 = true;
					while (flag3)
					{
						flag3 = false;
						Subscription subscription = null;
						while (this.RoundRobinSubscriptions(ref subscription))
						{
							if (base.IsDisposed)
							{
								return;
							}
							MapiNotification mapiNotification = subscription.Sink.Dequeue();
							if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionManager::DeliverNotifications. notification = {0}.", (mapiNotification == null) ? "<Null>" : mapiNotification.NotificationType.ToString());
							}
							if (mapiNotification != null)
							{
								flag3 = true;
								Notification notification;
								if (subscription.TryCreateXsoNotification(mapiNotification, out notification))
								{
									if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionManager::DeliverNotifications. notification = {0}.", notification.Type.ToString());
									}
									subscription.InvokeHandler(notification);
								}
								else if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "SubscriptionManager::DeliverNotifications. notification = <null>.");
								}
							}
						}
					}
					flag = true;
				}
				finally
				{
					bool flag4 = false;
					lock (this.lockSubscriptionQueue)
					{
						flag4 = this.HasAnyNotifications();
						if (!flag || !flag4)
						{
							this.isDeliveryThreadAlive = false;
							flag2 = true;
						}
					}
					if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.StorageTracer.TraceDebug<bool, bool, bool>((long)this.GetHashCode(), "SubscriptionManager::DeliverNotifications. Delivery thread is leaving. isOperationSuccess = {0}, hasAnyNotifications = {1}, workDone = {2}.", flag, flag4, flag2);
					}
				}
			}
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x0012FEA4 File Offset: 0x0012E0A4
		private bool RoundRobinSubscriptions(ref Subscription subscription)
		{
			bool result;
			lock (this.lockSubscriptionQueue)
			{
				if (this.subscriptionQueue.Count == 0)
				{
					result = false;
				}
				else
				{
					int num;
					if (subscription != null)
					{
						num = this.subscriptionQueue.IndexOf(subscription);
					}
					else
					{
						num = -1;
					}
					num++;
					if (num >= this.subscriptionQueue.Count)
					{
						result = false;
					}
					else
					{
						subscription = this.subscriptionQueue[num];
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x0012FF2C File Offset: 0x0012E12C
		private bool HasAnyNotifications()
		{
			foreach (Subscription subscription in this.subscriptionQueue)
			{
				if (subscription.Sink.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x0012FF90 File Offset: 0x0012E190
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SubscriptionsManager>(this);
		}

		// Token: 0x04002715 RID: 10005
		private readonly List<Subscription> subscriptionQueue = new List<Subscription>();

		// Token: 0x04002716 RID: 10006
		private readonly object lockSubscriptionQueue = new object();

		// Token: 0x04002717 RID: 10007
		private bool isDeliveryThreadAlive;
	}
}
