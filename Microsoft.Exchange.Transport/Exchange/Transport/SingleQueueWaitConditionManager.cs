using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200033C RID: 828
	internal class SingleQueueWaitConditionManager : WaitConditionManager
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x00088AB8 File Offset: 0x00086CB8
		public SingleQueueWaitConditionManager(ILockableQueue queue, NextHopSolutionKey queueName, int maxExecutingThreadsLimit, IWaitConditionManagerConfig config, ICostFactory factory, IProcessingQuotaComponent processingQuotaComponent, Func<DateTime> timeProvider, Trace tracer) : base(maxExecutingThreadsLimit, config, factory, processingQuotaComponent, timeProvider, tracer)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this.queue = queue;
			this.queueName = queueName;
			this.timeProvider = timeProvider;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00088AF0 File Offset: 0x00086CF0
		public ILockableItem DequeueNext()
		{
			bool flag = false;
			ILockableItem lockableItem;
			Cost cost;
			for (;;)
			{
				lockableItem = this.queue.DequeueInternal();
				if (lockableItem != null)
				{
					WaitCondition condition = lockableItem.GetCondition();
					if (condition == null)
					{
						break;
					}
					if (base.AllowMessageOrLock(condition, this.queueName, lockableItem, lockableItem.AccessToken, out cost))
					{
						goto Block_3;
					}
				}
				else
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(2869308733U);
					if (flag || !base.TryActivate(this.queueName))
					{
						goto IL_96;
					}
					flag = true;
					ExTraceGlobals.FaultInjectionTracer.TraceTest(4211486013U);
				}
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "No condition could be determined for mail item");
			return lockableItem;
			Block_3:
			lockableItem.ThrottlingContext = new ThrottlingContext(this.GetCurrentTime(), cost);
			return lockableItem;
			IL_96:
			this.mapStateChanged = false;
			return lockableItem;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00088B9B File Offset: 0x00086D9B
		public void MessageCompleted(DateTime startTime, WaitCondition condition)
		{
			base.MessageCompleted(condition, this.queueName, startTime);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00088BAB File Offset: 0x00086DAB
		public void AddToWaitlist(WaitCondition condition)
		{
			base.AddToLocked(condition, this.queueName);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00088BBA File Offset: 0x00086DBA
		public void RevokeToken(WaitCondition condition)
		{
			base.RevokeToken(condition, this.queueName);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00088BC9 File Offset: 0x00086DC9
		public void CleanupQueue(WaitCondition condition)
		{
			base.CleanupQueue(condition, this.queueName);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00088BD8 File Offset: 0x00086DD8
		public void CleanupItem(WaitCondition condition)
		{
			base.CleanupItem(condition, this.queueName);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00088BE8 File Offset: 0x00086DE8
		protected override bool Activate(WaitCondition condition, NextHopSolutionKey queue)
		{
			AccessToken token = null;
			bool undoWaiting = false;
			QueueWaitList queueWaitList;
			if (!this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				base.ActivateFailed(condition, this.queueName, token, undoWaiting);
				return false;
			}
			if (queueWaitList.GetNextItem(this.queueName))
			{
				undoWaiting = true;
				token = new AccessToken(condition, this.queueName, this);
				if (this.queue.ActivateOne(condition, DeliveryPriority.Normal, token))
				{
					base.ActivateSucceeded(condition, this.queueName, queueWaitList);
					return true;
				}
			}
			base.ActivateFailed(condition, this.queueName, token, undoWaiting);
			return false;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00088C68 File Offset: 0x00086E68
		protected override void Lock(ILockableItem item, WaitCondition condition, NextHopSolutionKey queueName, string lockReason)
		{
			if (!this.queueName.Equals(queueName))
			{
				throw new InvalidOperationException(string.Format("Lock called on SingleQueueWaitConditionManager and the queue passed '{0}' did not match expected '{1}'", queueName, this.queueName));
			}
			this.queue.Lock(item, condition, lockReason, this.config.LockedMessageDehydrationThreshold);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00088CC4 File Offset: 0x00086EC4
		private DateTime GetCurrentTime()
		{
			DateTime result;
			if (this.timeProvider != null)
			{
				result = this.timeProvider();
			}
			else
			{
				result = DateTime.UtcNow;
			}
			return result;
		}

		// Token: 0x0400128F RID: 4751
		private readonly ILockableQueue queue;

		// Token: 0x04001290 RID: 4752
		private readonly NextHopSolutionKey queueName;

		// Token: 0x04001291 RID: 4753
		private Func<DateTime> timeProvider;
	}
}
