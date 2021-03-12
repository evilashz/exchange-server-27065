using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003B1 RID: 945
	internal class MultiQueueWaitConditionManager : WaitConditionManager
	{
		// Token: 0x06002A4C RID: 10828 RVA: 0x000A807C File Offset: 0x000A627C
		public MultiQueueWaitConditionManager(int maxExecutingThreadsLimit, IWaitConditionManagerConfig config, ICostFactory factory, IProcessingQuotaComponent processingQuotaComponent, Func<DateTime> timeProvider, Trace tracer, GetQueueDelegate getQueue) : base(maxExecutingThreadsLimit, config, factory, processingQuotaComponent, timeProvider, tracer)
		{
			this.getQueue = getQueue;
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000A8098 File Offset: 0x000A6298
		internal ILockableItem DequeueNext(ILockableQueue queue, DeliveryPriority priority)
		{
			int num = 0;
			ILockableItem lockableItem;
			Cost cost;
			for (;;)
			{
				lockableItem = queue.DequeueInternal(priority);
				if (lockableItem != null)
				{
					WaitCondition condition = lockableItem.GetCondition();
					if (condition == null)
					{
						break;
					}
					if (base.AllowMessageOrLock(condition, queue.Key, lockableItem, lockableItem.AccessToken, out cost))
					{
						goto Block_3;
					}
				}
				else
				{
					if (queue.LockedCount == 0)
					{
						goto IL_7E;
					}
					if (!base.TryActivate(queue.Key))
					{
						goto Block_5;
					}
					ExTraceGlobals.FaultInjectionTracer.TraceTest(2600873277U);
					num++;
					if (num > 5)
					{
						goto Block_6;
					}
				}
			}
			return lockableItem;
			Block_3:
			lockableItem.ThrottlingContext = new ThrottlingContext(cost);
			return lockableItem;
			Block_5:
			this.mapStateChanged = false;
			return null;
			Block_6:
			this.mapStateChanged = false;
			return null;
			IL_7E:
			return null;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000A8124 File Offset: 0x000A6324
		internal void MessageCompleted(WaitCondition condition, NextHopSolutionKey queue)
		{
			base.MessageCompleted(condition, queue, DateTime.MinValue);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000A8134 File Offset: 0x000A6334
		protected override bool Activate(WaitCondition condition, NextHopSolutionKey currentQueue)
		{
			bool flag = false;
			QueueWaitList queueWaitList;
			if (!this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				this.tracer.TraceInformation<NextHopSolutionKey, WaitCondition>(0, (long)this.GetHashCode(), "No wait list exists for condition {1}", currentQueue, condition);
				base.ActivateFailed(condition, currentQueue, null, false);
				return false;
			}
			ILockableQueue lockableQueue;
			AccessToken accessToken;
			if (queueWaitList.GetNextItem(currentQueue))
			{
				lockableQueue = this.getQueue(currentQueue);
				if (lockableQueue == null)
				{
					this.tracer.TraceInformation<NextHopSolutionKey>(0, (long)this.GetHashCode(), "Cannot find queue with key {0}; will remove it", currentQueue);
					base.CleanupQueue(currentQueue);
					base.ActivateFailed(condition, currentQueue, null, false);
					return false;
				}
				accessToken = new AccessToken(condition, currentQueue, this);
			}
			else
			{
				if (!queueWaitList.HasDisabledMessages(currentQueue))
				{
					this.tracer.TraceInformation<NextHopSolutionKey, WaitCondition>(0, (long)this.GetHashCode(), "No weight found for queue {0} for condition {1} or the weight is 0.", currentQueue, condition);
					base.ActivateFailed(condition, currentQueue, null, false);
					return false;
				}
				lockableQueue = this.getQueue(currentQueue);
				accessToken = new AccessToken(condition, currentQueue, this);
				flag = true;
			}
			if (lockableQueue.ActivateOne(condition, DeliveryPriority.Normal, accessToken))
			{
				base.ActivateSucceeded(condition, currentQueue, queueWaitList);
				return true;
			}
			if (flag)
			{
				base.DisabledMessagesCleared(condition, currentQueue, queueWaitList);
				accessToken.Return(false);
				return false;
			}
			this.tracer.TraceInformation<NextHopSolutionKey, WaitCondition>(0, (long)this.GetHashCode(), "Queue {0} has no messages for condition {1} though conditionManager believes there are locked messages. Cleaning up waiting messages listed in manager.", currentQueue, condition);
			base.ActivateFailed(condition, currentQueue, accessToken, true);
			return false;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000A8268 File Offset: 0x000A6468
		protected override void Lock(ILockableItem item, WaitCondition condition, NextHopSolutionKey queue, string lockReason)
		{
			ILockableQueue lockableQueue = this.getQueue(queue);
			if (lockableQueue == null)
			{
				throw new InvalidOperationException(string.Format("Cannot find queue '{0}' in RemoteDeliveryComponent", queue.ToString()));
			}
			lockableQueue.Lock(item, condition, lockReason, 0);
		}

		// Token: 0x0400156D RID: 5485
		private GetQueueDelegate getQueue;
	}
}
