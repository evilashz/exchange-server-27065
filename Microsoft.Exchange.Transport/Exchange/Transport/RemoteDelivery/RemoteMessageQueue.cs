using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003B7 RID: 951
	internal abstract class RemoteMessageQueue : TransportMessageQueue
	{
		// Token: 0x06002AB8 RID: 10936 RVA: 0x000AAA78 File Offset: 0x000A8C78
		protected RemoteMessageQueue(RoutedQueueBase queueStorage, PriorityBehaviour behaviour, MultiQueueWaitConditionManager conditionManager) : base(queueStorage, behaviour)
		{
			this.conditionManager = conditionManager;
			this.activeMessageCountsCollection = new ReadOnlyCollection<int>(this.activeMessageCounts);
			this.deferredMessageCountsCollection = new ReadOnlyCollection<int>(this.deferredMessageCounts);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000AAAEC File Offset: 0x000A8CEC
		protected RemoteMessageQueue(PriorityBehaviour behaviour) : base(behaviour)
		{
			this.activeMessageCountsCollection = new ReadOnlyCollection<int>(this.activeMessageCounts);
			this.deferredMessageCountsCollection = new ReadOnlyCollection<int>(this.deferredMessageCounts);
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06002ABA RID: 10938 RVA: 0x000AAB58 File Offset: 0x000A8D58
		// (remove) Token: 0x06002ABB RID: 10939 RVA: 0x000AAB90 File Offset: 0x000A8D90
		public event Action<RoutedMailItem> OnAcquire;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06002ABC RID: 10940 RVA: 0x000AABC8 File Offset: 0x000A8DC8
		// (remove) Token: 0x06002ABD RID: 10941 RVA: 0x000AAC00 File Offset: 0x000A8E00
		public event Action<RoutedMailItem> OnRelease;

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002ABE RID: 10942
		public abstract NextHopSolutionKey Key { get; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000AAC35 File Offset: 0x000A8E35
		public int CountNotDeleted
		{
			get
			{
				return Components.QueueManager.GetTotalFromInstance(this.ActiveMessageCounts) + Components.QueueManager.GetTotalFromInstance(this.DeferredMessageCounts);
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000AAC58 File Offset: 0x000A8E58
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x000AAC65 File Offset: 0x000A8E65
		public long LastResubmitTime
		{
			get
			{
				return Interlocked.Read(ref this.lastResubmitTime);
			}
			set
			{
				Interlocked.Exchange(ref this.lastResubmitTime, value);
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000AAC74 File Offset: 0x000A8E74
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x000AAC7C File Offset: 0x000A8E7C
		public LastError LastTransientError
		{
			get
			{
				return this.lastTransientError;
			}
			set
			{
				this.lastTransientError = value;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x000AAC85 File Offset: 0x000A8E85
		protected MultiQueueWaitConditionManager ConditionManager
		{
			get
			{
				return this.conditionManager;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000AAC8D File Offset: 0x000A8E8D
		protected virtual LatencyComponent LatencyComponent
		{
			get
			{
				return LatencyComponent.None;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x000AAC90 File Offset: 0x000A8E90
		protected ReadOnlyCollection<int> ActiveMessageCounts
		{
			get
			{
				return this.activeMessageCountsCollection;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000AAC98 File Offset: 0x000A8E98
		protected ReadOnlyCollection<int> DeferredMessageCounts
		{
			get
			{
				return this.deferredMessageCountsCollection;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x000AACA0 File Offset: 0x000A8EA0
		protected bool IsResubmitting
		{
			get
			{
				return this.resubmitReason != null;
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000AACB0 File Offset: 0x000A8EB0
		public override IQueueItem Dequeue(DeliveryPriority priority)
		{
			IQueueItem queueItem = base.Dequeue(priority);
			RoutedMailItem routedMailItem = (RoutedMailItem)queueItem;
			if (routedMailItem != null)
			{
				this.OnReleaseInternal(routedMailItem);
			}
			return queueItem;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000AACD7 File Offset: 0x000A8ED7
		public IQueueItem DequeueItem(DequeueMatch match)
		{
			return base.DequeueItem(match, false);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000AACE1 File Offset: 0x000A8EE1
		public void Lock(ILockableItem item, WaitCondition condition, string lockReason, int dehydrateThreshold)
		{
			item.LockExpirationTime = DateTimeOffset.UtcNow + Components.TransportAppConfig.ThrottlingConfig.LockExpirationInterval;
			base.Lock(item, condition, lockReason, dehydrateThreshold);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000AAD10 File Offset: 0x000A8F10
		public virtual int Resubmit(ResubmitReason resubmitReason, Action<TransportMailItem> updateBeforeResubmit = null)
		{
			int num = 0;
			long num2 = 0L;
			lock (this)
			{
				num2 = DateTime.UtcNow.Ticks;
				this.LastResubmitTime = num2;
				if (this.IsResubmitting)
				{
					ExTraceGlobals.QueuingTracer.TraceDebug<ResubmitReason, ResubmitReason?>((long)this.GetHashCode(), "Resubmit for reason '{0}' not performed; Resubmission is currently in progress for reason '{1}'.", resubmitReason, this.resubmitReason);
					this.nextResubmitReason = new ResubmitReason?(resubmitReason);
					this.nextUpdateBeforeResubmit = updateBeforeResubmit;
					return 0;
				}
				this.resubmitReason = new ResubmitReason?(resubmitReason);
				goto IL_200;
			}
			IL_86:
			ICollection<IQueueItem> collection = base.DequeueAll(new Predicate<IQueueItem>(this.ShouldDequeueForResubmit));
			int num3 = 0;
			bool routeForHighAvailability = this.resubmitReason != ResubmitReason.UnreachableSameVersionHubs;
			foreach (IQueueItem queueItem in collection)
			{
				RoutedMailItem routedMailItem = (RoutedMailItem)queueItem;
				if (this.resubmitReason == ResubmitReason.UnreachableSameVersionHubs)
				{
					MessageTrackingLog.TrackHighAvailabilityRedirect(MessageTrackingSource.QUEUE, routedMailItem, "ResubmitForDirectDelivery");
				}
				DeferReason resubmitDeferReason = DeferReason.None;
				TimeSpan value = TimeSpan.Zero;
				TimeSpan configUpdateResubmitDeferInterval = Components.TransportAppConfig.RemoteDelivery.ConfigUpdateResubmitDeferInterval;
				if (resubmitReason == ResubmitReason.ConfigUpdate && configUpdateResubmitDeferInterval > TimeSpan.Zero)
				{
					resubmitDeferReason = DeferReason.ConfigUpdate;
					value = configUpdateResubmitDeferInterval;
				}
				if (!routedMailItem.Resubmit(resubmitDeferReason, new TimeSpan?(value), routeForHighAvailability, updateBeforeResubmit))
				{
					num3++;
				}
			}
			num += collection.Count - num3;
			lock (this)
			{
				if (this.LastResubmitTime != num2)
				{
					num2 = this.LastResubmitTime;
					if (this.nextResubmitReason == null)
					{
						throw new InvalidOperationException("Invalid: a queued resubmit without a reason");
					}
					this.resubmitReason = this.nextResubmitReason;
					updateBeforeResubmit = this.nextUpdateBeforeResubmit;
					this.nextResubmitReason = null;
				}
				else
				{
					this.resubmitReason = null;
					this.nextResubmitReason = null;
					this.nextUpdateBeforeResubmit = null;
				}
			}
			IL_200:
			if (!this.IsResubmitting)
			{
				if (Components.QueueManager.PerfCountersTotal != null)
				{
					Components.QueueManager.PerfCountersTotal.ItemsResubmittedTotal.IncrementBy((long)num);
				}
				ExTraceGlobals.QueuingTracer.TraceDebug<int>((long)this.GetHashCode(), "{0} items were resubmitted", num);
				return num;
			}
			goto IL_86;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000AAFE0 File Offset: 0x000A91E0
		public void AttemptToGenerateDelayDSNAndDehydrateAll()
		{
			List<IQueueItem> dsnList = new List<IQueueItem>();
			DateTime now = DateTime.UtcNow;
			base.ForEach(delegate(IQueueItem item)
			{
				RoutedMailItem routedMailItem = (RoutedMailItem)item;
				if (routedMailItem.IsWorkNeeded(now))
				{
					routedMailItem.LastQueueLevelError = this.lastTransientError;
					dsnList.Add(item);
				}
			});
			dsnList.ForEach(delegate(IQueueItem item)
			{
				item.Update();
			});
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000AB04A File Offset: 0x000A924A
		protected override void DataAvailable()
		{
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000AB04C File Offset: 0x000A924C
		protected virtual SmtpResponse GetItemExpiredResponse(RoutedMailItem routedMailItem)
		{
			return AckReason.MessageExpired;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000AB064 File Offset: 0x000A9264
		protected override void ItemExpired(IQueueItem item, bool wasEnqueued)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			ExTraceGlobals.QueuingTracer.TraceDebug<long, NextHopSolutionKey>((long)this.GetHashCode(), "Message with ID {0} has expired in queue '{1}'.", routedMailItem.RecordId, this.Key);
			if (wasEnqueued)
			{
				this.ItemRemoved(item);
			}
			else
			{
				this.RemoveFromConditionManager(item);
			}
			SmtpResponse itemExpiredResponse = this.GetItemExpiredResponse(routedMailItem);
			routedMailItem.LastQueueLevelError = this.CreatePermanentError(itemExpiredResponse, this.lastTransientError);
			bool flag;
			RiskLevel riskLevel;
			DeliveryPriority priority;
			routedMailItem.Ack(AckStatus.Fail, itemExpiredResponse, MessageTrackingSource.QUEUE, "Queue=" + this.queueStorage.Id.ToString(), null, true, out flag, out riskLevel, out priority);
			if (!flag)
			{
				Components.QueueManager.UpateInstanceCounter(riskLevel, priority, delegate(QueuingPerfCountersInstance c)
				{
					c.ItemsQueuedForDeliveryExpiredTotal.Increment();
				});
			}
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000AB125 File Offset: 0x000A9325
		protected override void ItemLockExpired(IQueueItem item)
		{
			this.ItemRemoved(item);
			Components.QueueManager.UpdatePerfCountersOnLockExpiredInDeliveryQueue();
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000AB138 File Offset: 0x000A9338
		protected override bool ItemDeferred(IQueueItem item)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			if (routedMailItem.Defer())
			{
				this.OnAcquireInternal(routedMailItem);
				this.RemoveFromConditionManager(item);
				return true;
			}
			return false;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000AB165 File Offset: 0x000A9365
		protected override void ItemEnqueued(IQueueItem item)
		{
			this.OnAcquireInternal((RoutedMailItem)item);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000AB174 File Offset: 0x000A9374
		protected override bool ItemActivated(IQueueItem item)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			this.OnReleaseInternal(routedMailItem);
			return routedMailItem.Activate();
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000AB19C File Offset: 0x000A939C
		protected override bool ItemLocked(IQueueItem item, WaitCondition condition, string lockReason)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			LatencyTracker.BeginTrackLatency(LatencyComponent.DeliveryQueueLocking, routedMailItem.LatencyTracker);
			routedMailItem.Lock(lockReason);
			RemoteMessageQueue.ReturnTokenIfPresent(routedMailItem);
			this.OnAcquireInternal(routedMailItem);
			return true;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000AB1D4 File Offset: 0x000A93D4
		protected override bool ItemUnlocked(IQueueItem item, AccessToken token)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			routedMailItem.CurrentCondition = null;
			routedMailItem.LockReason = null;
			routedMailItem.LockExpirationTime = DateTimeOffset.MinValue;
			LatencyTracker.EndTrackLatency(LatencyComponent.DeliveryQueueLocking, routedMailItem.LatencyTracker);
			if (routedMailItem.IsSuspendedByAdmin && routedMailItem.PrepareForSuspension())
			{
				this.OnReleaseInternal(routedMailItem);
				this.Enqueue(routedMailItem);
				return false;
			}
			routedMailItem.AccessToken = token;
			foreach (MailRecipient mailRecipient in routedMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Locked)
				{
					mailRecipient.Status = Status.Ready;
				}
			}
			return true;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000AB280 File Offset: 0x000A9480
		protected override void ItemRelocked(IQueueItem item, string lockReason, out WaitCondition condition)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			LatencyTracker.BeginTrackLatency(LatencyComponent.DeliveryQueueLocking, routedMailItem.LatencyTracker);
			condition = routedMailItem.AccessToken.Condition;
			routedMailItem.LockExpirationTime = DateTimeOffset.UtcNow + Components.TransportAppConfig.ThrottlingConfig.LockExpirationInterval;
			RemoteMessageQueue.ReturnTokenIfPresent(routedMailItem);
			this.ConditionManager.AddToLocked(condition, this.Key);
			routedMailItem.Lock(lockReason);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000AB2F0 File Offset: 0x000A94F0
		protected override void ItemRemoved(IQueueItem item)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			this.OnReleaseInternal(routedMailItem);
			this.RemoveFromConditionManager(routedMailItem);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000AB314 File Offset: 0x000A9514
		protected override void ItemDehydrated(IQueueItem item)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			routedMailItem.Dehydrate(Breadcrumb.DehydrateOnMailItemLocked);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000AB334 File Offset: 0x000A9534
		protected virtual bool ShouldDequeueForResubmit(IQueueItem queueItem)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)queueItem;
			return routedMailItem.ShouldDequeueForResubmit();
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000AB353 File Offset: 0x000A9553
		private void OnAcquireInternal(RoutedMailItem mailItem)
		{
			this.IncrementInstanceCount(mailItem);
			Interlocked.Increment(ref this.lastIncomingMessageCount);
			if (this.OnAcquire != null)
			{
				this.OnAcquire(mailItem);
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000AB37C File Offset: 0x000A957C
		private void OnReleaseInternal(RoutedMailItem mailItem)
		{
			this.DecrementInstanceCount(mailItem);
			Interlocked.Increment(ref this.lastOutgoingMessageCount);
			if (this.OnRelease != null)
			{
				this.OnRelease(mailItem);
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000AB3A5 File Offset: 0x000A95A5
		private static void ReturnTokenIfPresent(RoutedMailItem mailItem)
		{
			if (mailItem.AccessToken != null)
			{
				mailItem.AccessToken.Return(true);
				mailItem.AccessToken = null;
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000AB3C4 File Offset: 0x000A95C4
		private void RemoveFromConditionManager(IQueueItem item)
		{
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			if (routedMailItem.AccessToken != null)
			{
				RemoteMessageQueue.ReturnTokenIfPresent(routedMailItem);
			}
			else if (routedMailItem.CurrentCondition != null)
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.DeliveryQueueLocking, routedMailItem.LatencyTracker);
				this.ConditionManager.CleanupItem(routedMailItem.CurrentCondition, this.Key);
			}
			foreach (MailRecipient mailRecipient in routedMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Locked)
				{
					mailRecipient.Status = Status.Ready;
				}
			}
			routedMailItem.CurrentCondition = null;
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000AB468 File Offset: 0x000A9668
		private void DecrementInstanceCount(RoutedMailItem routedMailItem)
		{
			this.DecrementInstanceCount(routedMailItem.RiskLevel, routedMailItem.Priority, routedMailItem.Deferred);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000AB484 File Offset: 0x000A9684
		private void DecrementInstanceCount(RiskLevel riskLevel, DeliveryPriority priority, bool deferred)
		{
			IEnumerable<int> instanceCounterIndex = QueueManager.GetInstanceCounterIndex(riskLevel, priority);
			List<int> list = new List<int>();
			if (deferred)
			{
				using (IEnumerator<int> enumerator = instanceCounterIndex.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						list.Add(Interlocked.Decrement(ref this.deferredMessageCounts[num]));
					}
					goto IL_91;
				}
			}
			foreach (int num2 in instanceCounterIndex)
			{
				list.Add(Interlocked.Decrement(ref this.activeMessageCounts[num2]));
			}
			IL_91:
			foreach (int num3 in list)
			{
				if (num3 < 0)
				{
					throw new InvalidOperationException(string.Format("Cannot decrement message count for {0}deferred messages below zero", deferred ? string.Empty : "non "));
				}
			}
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000AB59C File Offset: 0x000A979C
		private void IncrementInstanceCount(RoutedMailItem item)
		{
			this.IncrementInstanceCount(item.RiskLevel, item.Priority, item.Deferred);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000AB5B8 File Offset: 0x000A97B8
		private void IncrementInstanceCount(RiskLevel riskLevel, DeliveryPriority priority, bool deferred)
		{
			IEnumerable<int> instanceCounterIndex = QueueManager.GetInstanceCounterIndex(riskLevel, priority);
			if (deferred)
			{
				using (IEnumerator<int> enumerator = instanceCounterIndex.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						Interlocked.Increment(ref this.deferredMessageCounts[num]);
					}
					return;
				}
			}
			foreach (int num2 in instanceCounterIndex)
			{
				Interlocked.Increment(ref this.activeMessageCounts[num2]);
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000AB660 File Offset: 0x000A9860
		private LastError CreatePermanentError(SmtpResponse smtpResponse, LastError transientError)
		{
			if (transientError != null && smtpResponse.SmtpResponseType == SmtpResponseType.PermanentError)
			{
				return new LastError(transientError.LastAttemptFqdn, transientError.LastAttemptEndpoint, new DateTime?(DateTime.UtcNow), smtpResponse, transientError);
			}
			return null;
		}

		// Token: 0x040015B4 RID: 5556
		protected ResubmitReason? resubmitReason;

		// Token: 0x040015B5 RID: 5557
		private readonly MultiQueueWaitConditionManager conditionManager;

		// Token: 0x040015B6 RID: 5558
		private long lastResubmitTime = DateTime.MinValue.Ticks;

		// Token: 0x040015B7 RID: 5559
		private ResubmitReason? nextResubmitReason;

		// Token: 0x040015B8 RID: 5560
		private Action<TransportMailItem> nextUpdateBeforeResubmit;

		// Token: 0x040015B9 RID: 5561
		private int[] activeMessageCounts = new int[QueueManager.InstanceCountersLength];

		// Token: 0x040015BA RID: 5562
		private ReadOnlyCollection<int> activeMessageCountsCollection;

		// Token: 0x040015BB RID: 5563
		private int[] deferredMessageCounts = new int[QueueManager.InstanceCountersLength];

		// Token: 0x040015BC RID: 5564
		private ReadOnlyCollection<int> deferredMessageCountsCollection;

		// Token: 0x040015BD RID: 5565
		protected LastError lastTransientError;
	}
}
