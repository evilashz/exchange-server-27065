using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200033B RID: 827
	internal abstract class WaitConditionManager
	{
		// Token: 0x060023C0 RID: 9152 RVA: 0x00087F10 File Offset: 0x00086110
		public WaitConditionManager(int maxExecutingThreadsLimit, IWaitConditionManagerConfig config, ICostFactory factory, IProcessingQuotaComponent processingQuotaComponent, Func<DateTime> timeProvider, Trace tracer)
		{
			if (maxExecutingThreadsLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("maxExecutingThreadsLimit");
			}
			this.config = config;
			CostConfiguration costConfig = new CostConfiguration(this.config, false, maxExecutingThreadsLimit, (long)((double)maxExecutingThreadsLimit * this.config.ThrottlingHistoryInterval.TotalMilliseconds), processingQuotaComponent, timeProvider);
			this.costMap = factory.CreateMap(costConfig, new IsLocked(this.IsLocked), new IsLockedOnQueue(this.IsLockedOnQueue), tracer);
			this.tracer = tracer;
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x00087FB4 File Offset: 0x000861B4
		public bool MapStateChanged
		{
			get
			{
				return this.mapStateChanged;
			}
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00087FBC File Offset: 0x000861BC
		public void AddToLocked(WaitCondition condition, NextHopSolutionKey queue)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.WaitlistNewItem);
			this.AddToWaitList(condition, queue);
			this.mapStateChanged = true;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00087FE8 File Offset: 0x000861E8
		public bool TryActivate(NextHopSolutionKey queue)
		{
			WaitCondition[] array = this.costMap.Unlock(queue);
			if (array == null || array.Length == 0)
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.ItemNotFoundToActivate);
				return false;
			}
			foreach (WaitCondition condition in array)
			{
				this.ActivateAndUpdate(condition, queue);
			}
			this.mapStateChanged = true;
			return true;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0008803E File Offset: 0x0008623E
		public void RevokeToken(WaitCondition condition, NextHopSolutionKey queue)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.ReturnTokenUnused);
			this.CompleteItem(condition, queue);
			this.costMap.FailToken(condition);
			this.mapStateChanged = true;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00088078 File Offset: 0x00086278
		public void MoveLockedToDisabled(WaitCondition condition, NextHopSolutionKey queue)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.AddDisabled);
			QueueWaitList queueWaitList;
			if (!this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				queueWaitList = this.AddToWaitList(condition, queue);
			}
			lock (queueWaitList)
			{
				if (!queueWaitList.MoveToDisabled(queue))
				{
					queueWaitList.Reset();
					QueueWaitList orAdd = this.WaitMap.GetOrAdd(condition, queueWaitList);
					orAdd.Add(queue);
					orAdd.MoveToDisabled(queue);
				}
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00088110 File Offset: 0x00086310
		public void CleanupQueue(NextHopSolutionKey queue)
		{
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.CleanupQueue);
			List<KeyValuePair<WaitCondition, QueueWaitList>> list = new List<KeyValuePair<WaitCondition, QueueWaitList>>();
			foreach (KeyValuePair<WaitCondition, QueueWaitList> item in this.WaitMap)
			{
				if (item.Value.Cleanup(queue))
				{
					this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.RemoveEmptyWaitlist);
					list.Add(item);
				}
			}
			foreach (KeyValuePair<WaitCondition, QueueWaitList> keyValuePair in list)
			{
				lock (keyValuePair.Value)
				{
					if (keyValuePair.Value.Cleanup(queue))
					{
						this.RemoveEmptyList(keyValuePair.Key);
					}
				}
				this.mapStateChanged = true;
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00088218 File Offset: 0x00086418
		public void CleanupQueue(WaitCondition condition, NextHopSolutionKey queue)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			QueueWaitList queueWaitList;
			if (this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.CleanupQueue);
				this.tracer.TraceDebug<NextHopSolutionKey, WaitCondition>((long)this.GetHashCode(), "Removing queue '{0}' from condition '{1}'", queue, condition);
				lock (queueWaitList)
				{
					if (queueWaitList.Cleanup(queue))
					{
						this.RemoveEmptyList(condition);
					}
				}
				this.mapStateChanged = true;
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000882A8 File Offset: 0x000864A8
		public void CleanupItem(WaitCondition condition, NextHopSolutionKey queue)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			QueueWaitList queueWaitList;
			if (this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.CleanupItem);
				this.tracer.TraceDebug<NextHopSolutionKey, WaitCondition>((long)this.GetHashCode(), "Removing message in queue '{0}' from condition '{1}'", queue, condition);
				lock (queueWaitList)
				{
					if (queueWaitList.CleanupItem(queue))
					{
						this.RemoveEmptyList(condition);
					}
				}
				this.mapStateChanged = true;
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00088338 File Offset: 0x00086538
		internal NextHopSolutionKey[] ActivateAll(WaitCondition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			QueueWaitList queueWaitList;
			if (this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				lock (queueWaitList)
				{
					this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.ActivateAll);
					this.tracer.TraceDebug<WaitCondition>((long)this.GetHashCode(), "Activating all queues for condition '{0}'", condition);
					NextHopSolutionKey[] result = queueWaitList.Clear();
					this.RemoveEmptyList(condition);
					this.mapStateChanged = true;
					return result;
				}
			}
			return null;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000883CC File Offset: 0x000865CC
		internal virtual XElement GetDiagnosticInfo(bool showVerbose)
		{
			XElement xelement = new XElement("conditionManager");
			xelement.Add(new XElement("MapStateChanged", this.mapStateChanged));
			xelement.Add(this.costMap.GetDiagnosticInfo(showVerbose));
			XElement xelement2 = new XElement("LockedConditions");
			if (!showVerbose)
			{
				xelement2.Add(new XElement("count", this.WaitMap.Count));
			}
			else
			{
				foreach (KeyValuePair<WaitCondition, QueueWaitList> keyValuePair in this.WaitMap)
				{
					XElement xelement3 = new XElement("condition");
					xelement3.Add(new XElement("name", keyValuePair.Key.ToString()));
					xelement3.Add(new XElement("lockedMessages", keyValuePair.Value.MessageCount));
					xelement3.Add(new XElement("tokens", keyValuePair.Value.PendingMessageCount));
					xelement2.Add(xelement3);
				}
			}
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00088528 File Offset: 0x00086728
		internal virtual void TimedUpdate()
		{
			if (this.config.ProcessingTimeThrottlingEnabled || this.config.ThrottlingMemoryMaxThreshold.ToBytes() > 0UL)
			{
				this.mapStateChanged = true;
			}
			this.costMap.TimedUpdate();
		}

		// Token: 0x060023CC RID: 9164
		protected abstract bool Activate(WaitCondition condition, NextHopSolutionKey queue);

		// Token: 0x060023CD RID: 9165
		protected abstract void Lock(ILockableItem item, WaitCondition condition, NextHopSolutionKey queue, string lockReason);

		// Token: 0x060023CE RID: 9166 RVA: 0x0008856C File Offset: 0x0008676C
		protected bool AllowMessageOrLock(WaitCondition condition, NextHopSolutionKey queue, ILockableItem item, out Cost cost)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			QueueWaitList queueWaitList;
			if (this.WaitMap.TryGetValue(condition, out queueWaitList) && (queueWaitList.GetPendingMessageCount(queue) > 0 || queueWaitList.GetMessageCount(queue) > 0))
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.OlderItemFound);
				string lockReason = WaitConditionManager.GetLockReason(queueWaitList, queue);
				this.AddToWaitList(condition, queueWaitList, queue);
				this.tracer.TraceDebug<WaitCondition, NextHopSolutionKey>((long)this.GetHashCode(), "Older locked items or tokens found for condition '{0}' on queue '{1}'", condition, queue);
				this.Lock(item, condition, queue, lockReason);
				cost = null;
				return false;
			}
			if (this.costMap.Allow(condition, out cost))
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.IncrementInUse);
				return true;
			}
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.ConditionExceedsQuota);
			this.AddToWaitList(condition, queue);
			string diagnosticString = this.costMap.GetDiagnosticString(condition);
			this.Lock(item, condition, queue, diagnosticString);
			cost = null;
			return false;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00088650 File Offset: 0x00086850
		protected bool AllowMessageOrLock(WaitCondition condition, NextHopSolutionKey queue, ILockableItem item, AccessToken token, out Cost cost)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (token != null && token.Validate(condition) && token.Return(false))
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.NewItemTokenUsed);
				this.costMap.ReturnToken(condition, out cost);
				this.CompleteItem(condition, queue);
				return true;
			}
			return this.AllowMessageOrLock(condition, queue, item, out cost);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000886C0 File Offset: 0x000868C0
		protected void MessageCompleted(WaitCondition condition, NextHopSolutionKey queue, DateTime startTime)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.DecrementInUse);
			this.costMap.CompleteProcessing(condition, startTime);
			this.costMap.RemoveThread(condition);
			this.tracer.TraceDebug<WaitCondition>((long)this.GetHashCode(), "Condition '{0}' completed processing", condition);
			this.mapStateChanged = true;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00088720 File Offset: 0x00086920
		protected void ActivateSucceeded(WaitCondition condition, NextHopSolutionKey queue, QueueWaitList waitList)
		{
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.UpdatePriority);
			lock (waitList)
			{
				if (waitList.ConfirmRemove(queue))
				{
					this.RemoveEmptyList(condition);
				}
			}
			this.tracer.TraceDebug<WaitCondition, NextHopSolutionKey>((long)this.GetHashCode(), "Updating condition '{0}' that queue '{1}' activated an item", condition, queue);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x0008878C File Offset: 0x0008698C
		protected void ActivateFailed(WaitCondition condition, NextHopSolutionKey queue, AccessToken token, bool undoWaiting)
		{
			this.tracer.TraceDebug<NextHopSolutionKey, WaitCondition>((long)this.GetHashCode(), "could not activate message in queue '{0}' for condition '{1}'.", queue, condition);
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.ItemNotFoundToActivate);
			this.costMap.FailToken(condition);
			if (undoWaiting)
			{
				QueueWaitList queueWaitList;
				if (this.WaitMap.TryGetValue(condition, out queueWaitList))
				{
					lock (queueWaitList)
					{
						if (queueWaitList.RemoveWaitingAndOneOutstanding(queue))
						{
							this.RemoveEmptyList(condition);
						}
						goto IL_79;
					}
				}
				throw new InvalidOperationException("Can't find waitList for a condition we were trying to unlock");
			}
			IL_79:
			if (token != null)
			{
				token.Return(false);
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00088830 File Offset: 0x00086A30
		protected void DisabledMessagesCleared(WaitCondition condition, NextHopSolutionKey queue, QueueWaitList waitList)
		{
			lock (waitList)
			{
				if (waitList.DisabledMessagesCleared(queue))
				{
					this.RemoveEmptyList(condition);
				}
			}
			this.tracer.TraceDebug<WaitCondition, NextHopSolutionKey>((long)this.GetHashCode(), "Updating condition '{0}' on queue '{1}' has cleared all disabled messages", condition, queue);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00088890 File Offset: 0x00086A90
		private static string GetLockReason(QueueWaitList waitList, NextHopSolutionKey queue)
		{
			return string.Format("W/Tk {0}/{1}", waitList.GetMessageCount(queue), waitList.GetPendingMessageCount(queue));
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000888B4 File Offset: 0x00086AB4
		private QueueWaitList AddToWaitList(WaitCondition condition, NextHopSolutionKey queue)
		{
			this.tracer.TraceDebug<WaitCondition>((long)this.GetHashCode(), "Adding item to waitlist for condition '{0}'", condition);
			QueueWaitList orAdd = this.WaitMap.GetOrAdd(condition, new QueueWaitList(this.tracer));
			this.AddToWaitList(condition, orAdd, queue);
			return orAdd;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000888FC File Offset: 0x00086AFC
		private void AddToWaitList(WaitCondition condition, QueueWaitList waitList, NextHopSolutionKey queue)
		{
			if (waitList == null)
			{
				throw new ArgumentNullException("waitList");
			}
			lock (waitList)
			{
				int messageCount;
				if (waitList.Add(queue))
				{
					messageCount = waitList.MessageCount;
				}
				else
				{
					waitList.Reset();
					QueueWaitList orAdd = this.WaitMap.GetOrAdd(condition, waitList);
					orAdd.Add(queue);
					messageCount = orAdd.MessageCount;
				}
				if (messageCount == 1)
				{
					this.costMap.AddToIndex(condition);
				}
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00088984 File Offset: 0x00086B84
		private bool ActivateAndUpdate(WaitCondition condition, NextHopSolutionKey queue)
		{
			if (this.Activate(condition, queue))
			{
				this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.ActivateWaitingItemFound);
				this.tracer.TraceDebug<WaitCondition, NextHopSolutionKey>((long)this.GetHashCode(), "Activation successful for condition '{0}' on queue '{1}'", condition, queue);
				return true;
			}
			this.tracer.TraceDebug<WaitCondition, NextHopSolutionKey>((long)this.GetHashCode(), "Activation failed for condition '{0}' on queue '{1}' ", condition, queue);
			return false;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000889E0 File Offset: 0x00086BE0
		private void CompleteItem(WaitCondition condition, NextHopSolutionKey queue)
		{
			QueueWaitList queueWaitList;
			if (this.WaitMap.TryGetValue(condition, out queueWaitList))
			{
				lock (queueWaitList)
				{
					if (queueWaitList.CompleteItem(queue))
					{
						this.RemoveEmptyList(condition);
					}
				}
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00088A38 File Offset: 0x00086C38
		private void RemoveEmptyList(WaitCondition condition)
		{
			this.breadcrumbs.Drop(WaitConditionManagerBreadcrumbs.RemoveEmptyWaitlist);
			QueueWaitList queueWaitList;
			this.WaitMap.TryRemove(condition, out queueWaitList);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00088A64 File Offset: 0x00086C64
		private bool IsLockedOnQueue(WaitCondition condition, NextHopSolutionKey queue)
		{
			QueueWaitList queueWaitList;
			return this.WaitMap.TryGetValue(condition, out queueWaitList) && queueWaitList.GetMessageCount(queue) > 0;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x00088A90 File Offset: 0x00086C90
		private bool IsLocked(WaitCondition condition)
		{
			QueueWaitList queueWaitList;
			return this.WaitMap.TryGetValue(condition, out queueWaitList) && queueWaitList.MessageCount > 0;
		}

		// Token: 0x04001287 RID: 4743
		private const int NumberOfBreadcrumbs = 32;

		// Token: 0x04001288 RID: 4744
		protected readonly CostMap costMap;

		// Token: 0x04001289 RID: 4745
		protected readonly ConcurrentDictionary<WaitCondition, QueueWaitList> WaitMap = new ConcurrentDictionary<WaitCondition, QueueWaitList>();

		// Token: 0x0400128A RID: 4746
		protected readonly object SyncRoot = new object();

		// Token: 0x0400128B RID: 4747
		protected readonly IWaitConditionManagerConfig config;

		// Token: 0x0400128C RID: 4748
		protected bool mapStateChanged;

		// Token: 0x0400128D RID: 4749
		protected Breadcrumbs<WaitConditionManagerBreadcrumbs> breadcrumbs = new Breadcrumbs<WaitConditionManagerBreadcrumbs>(32);

		// Token: 0x0400128E RID: 4750
		protected Trace tracer;
	}
}
