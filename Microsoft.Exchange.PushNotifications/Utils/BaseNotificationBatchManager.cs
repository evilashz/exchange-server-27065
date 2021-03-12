using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.PushNotifications.Utils
{
	// Token: 0x02000045 RID: 69
	internal abstract class BaseNotificationBatchManager<TKey, TNotification> : PushNotificationDisposable
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x000059B8 File Offset: 0x00003BB8
		internal BaseNotificationBatchManager(uint batchTimerInSeconds, uint batchSize)
		{
			this.batchSize = batchSize;
			this.activeSet = new ConcurrentDictionary<TKey, TNotification>();
			this.timer = new System.Timers.Timer(batchTimerInSeconds * 1000U);
			this.timer.Elapsed += this.OnTimedEvent;
			this.timer.Start();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005A28 File Offset: 0x00003C28
		internal virtual bool TryGetPushNotification(TKey key, out TNotification notification)
		{
			ConcurrentDictionary<TKey, TNotification> concurrentDictionary = this.activeSet;
			if (concurrentDictionary != null && concurrentDictionary.ContainsKey(key))
			{
				notification = concurrentDictionary[key];
				return true;
			}
			notification = default(TNotification);
			return false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005A94 File Offset: 0x00003C94
		internal virtual void Add(TKey key, TNotification notification)
		{
			using (ReaderLockSlimWrapper readerLockSlimWrapper = new ReaderLockSlimWrapper(this.objLock))
			{
				readerLockSlimWrapper.AcquireLock();
				this.activeSet.AddOrUpdate(key, notification, delegate(TKey originalKey, TNotification originalValue)
				{
					Interlocked.Increment(ref this.discardedNotificationCounter);
					this.Merge(notification, originalValue);
					return notification;
				});
			}
			this.CheckConditionAndDrainNotificationBatch(this.batchSize);
		}

		// Token: 0x060001B8 RID: 440
		protected abstract void Merge(TNotification notificationDst, TNotification notificationSrc);

		// Token: 0x060001B9 RID: 441
		protected abstract void DrainNotifications(ConcurrentDictionary<TKey, TNotification> notifications);

		// Token: 0x060001BA RID: 442 RVA: 0x00005B1C File Offset: 0x00003D1C
		protected virtual void ReportDiscardedNotifications(int discarded)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005B1E File Offset: 0x00003D1E
		protected virtual void ReportDrainNotificationsException(AggregateException error)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005B20 File Offset: 0x00003D20
		protected virtual void ReportBatchCancelled()
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005B22 File Offset: 0x00003D22
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BaseNotificationBatchManager<TKey, TNotification>>(this);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005B2C File Offset: 0x00003D2C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.running = false;
				if (this.timer != null)
				{
					this.timer.Stop();
					this.timer.Dispose();
					this.timer = null;
				}
				if (this.objLock != null)
				{
					this.objLock.Dispose();
					this.objLock = null;
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005B84 File Offset: 0x00003D84
		protected bool CheckCancellation()
		{
			if (!this.running)
			{
				this.ReportBatchCancelled();
				return true;
			}
			return false;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005B99 File Offset: 0x00003D99
		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			if (!this.running)
			{
				return;
			}
			this.CheckConditionAndDrainNotificationBatch(1U);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private void CheckConditionAndDrainNotificationBatch(uint checkValue)
		{
			ConcurrentDictionary<TKey, TNotification> concurrentDictionary = null;
			using (UpgradeableLockSlimWrapper upgradeableLockSlimWrapper = new UpgradeableLockSlimWrapper(this.objLock))
			{
				upgradeableLockSlimWrapper.AcquireLock();
				if ((long)this.activeSet.Count >= (long)((ulong)checkValue))
				{
					using (WriterLockSlimWrapper writerLockSlimWrapper = new WriterLockSlimWrapper(this.objLock))
					{
						writerLockSlimWrapper.AcquireLock();
						if ((long)this.activeSet.Count >= (long)((ulong)checkValue))
						{
							concurrentDictionary = this.activeSet;
							this.activeSet = new ConcurrentDictionary<TKey, TNotification>();
						}
					}
				}
			}
			if (concurrentDictionary != null)
			{
				int num = Interlocked.Exchange(ref this.discardedNotificationCounter, 0);
				if (num > 0)
				{
					this.ReportDiscardedNotifications(num);
				}
				this.StartDrainingNotifications(concurrentDictionary);
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005CAC File Offset: 0x00003EAC
		private void StartDrainingNotifications(ConcurrentDictionary<TKey, TNotification> notifications)
		{
			if (notifications != null)
			{
				if (this.batchSize == 0U)
				{
					this.DrainNotifications(notifications);
					return;
				}
				Task task = new Task(delegate()
				{
					this.DrainNotifications(notifications);
				});
				task.ContinueWith(delegate(Task t)
				{
					if (t.Exception != null)
					{
						this.ReportDrainNotificationsException(t.Exception);
					}
				}, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously);
				task.Start();
			}
		}

		// Token: 0x0400009A RID: 154
		private readonly uint batchSize;

		// Token: 0x0400009B RID: 155
		private int discardedNotificationCounter;

		// Token: 0x0400009C RID: 156
		private ReaderWriterLockSlim objLock = new ReaderWriterLockSlim();

		// Token: 0x0400009D RID: 157
		private volatile bool running = true;

		// Token: 0x0400009E RID: 158
		private System.Timers.Timer timer;

		// Token: 0x0400009F RID: 159
		private ConcurrentDictionary<TKey, TNotification> activeSet;
	}
}
