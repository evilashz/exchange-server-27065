using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003BD RID: 957
	internal sealed class UnreachableMessageQueue : RemoteMessageQueue
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000AF572 File Offset: 0x000AD772
		private UnreachableMessageQueue(RoutedQueueBase queueStorage) : base(queueStorage, PriorityBehaviour.IgnorePriority, null)
		{
			this.queuingPerfCountersInstance = QueueManager.GetTotalPerfCounters();
			this.currentRoutingTablesTimestamp = DateTime.MinValue;
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000AF593 File Offset: 0x000AD793
		public static UnreachableMessageQueue Instance
		{
			get
			{
				return UnreachableMessageQueue.instance;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x000AF59A File Offset: 0x000AD79A
		public override NextHopSolutionKey Key
		{
			get
			{
				return NextHopSolutionKey.Unreachable;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000AF5A1 File Offset: 0x000AD7A1
		protected override LatencyComponent LatencyComponent
		{
			get
			{
				return LatencyComponent.UnreachableQueue;
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000AF5A8 File Offset: 0x000AD7A8
		public static void CreateInstance()
		{
			if (UnreachableMessageQueue.instance != null)
			{
				throw new InvalidOperationException("Unreachable queue already created");
			}
			RoutedQueueBase orAddQueue = Components.MessagingDatabase.GetOrAddQueue(NextHopSolutionKey.Unreachable);
			UnreachableMessageQueue.instance = new UnreachableMessageQueue(orAddQueue);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000AF5E2 File Offset: 0x000AD7E2
		public static void LoadInstance(RoutedQueueBase queueStorage)
		{
			if (UnreachableMessageQueue.instance != null)
			{
				throw new InvalidOperationException("Unreachable queue already created");
			}
			UnreachableMessageQueue.instance = new UnreachableMessageQueue(queueStorage);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000AF604 File Offset: 0x000AD804
		public new void Enqueue(IQueueItem item)
		{
			if (this.queuingPerfCountersInstance != null)
			{
				this.queuingPerfCountersInstance.UnreachableQueueLength.Increment();
			}
			RoutedMailItem routedMailItem = (RoutedMailItem)item;
			LatencyTracker.BeginTrackLatency(this.LatencyComponent, routedMailItem.LatencyTracker);
			base.Enqueue(item);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000AF649 File Offset: 0x000AD849
		public void RoutingTablesChangedHandler(IMailRouter eventSource, DateTime newRoutingTablesTimestamp, bool routesChanged)
		{
			if (newRoutingTablesTimestamp < this.currentRoutingTablesTimestamp)
			{
				return;
			}
			this.currentRoutingTablesTimestamp = newRoutingTablesTimestamp;
			if (routesChanged)
			{
				this.Resubmit(ResubmitReason.ConfigUpdate, null);
			}
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000AF670 File Offset: 0x000AD870
		protected override SmtpResponse GetItemExpiredResponse(RoutedMailItem routedMailItem)
		{
			UnreachableReason unreachableReasons = routedMailItem.UnreachableReasons;
			return AckReason.UnreachableMessageExpired(StatusCodeConverter.UnreachableReasonToString(unreachableReasons, CultureInfo.InvariantCulture, ";"));
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000AF699 File Offset: 0x000AD899
		protected override void ItemRemoved(IQueueItem item)
		{
			base.ItemRemoved(item);
			if (this.queuingPerfCountersInstance != null)
			{
				this.queuingPerfCountersInstance.UnreachableQueueLength.Decrement();
			}
			LatencyTracker.EndTrackLatency(this.LatencyComponent, ((RoutedMailItem)item).LatencyTracker);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000AF6D4 File Offset: 0x000AD8D4
		protected override bool ShouldDequeueForResubmit(IQueueItem queueItem)
		{
			if (!base.ShouldDequeueForResubmit(queueItem))
			{
				return false;
			}
			RoutedMailItem routedMailItem = (RoutedMailItem)queueItem;
			DateTime routingTimeStamp = routedMailItem.RoutingTimeStamp;
			if (routingTimeStamp != DateTime.MinValue)
			{
				return this.resubmitReason == ResubmitReason.Admin || this.resubmitReason == ResubmitReason.Redirect || routingTimeStamp < this.currentRoutingTablesTimestamp;
			}
			throw new InvalidOperationException("Message does not have routing time-stamp");
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000AF75C File Offset: 0x000AD95C
		public override int Resubmit(ResubmitReason resubmitReason, Action<TransportMailItem> updateBeforeResubmit = null)
		{
			if (resubmitReason == ResubmitReason.Inactivity)
			{
				throw new ArgumentException("Invalid resubmit reason: " + resubmitReason);
			}
			if (this.Suspended)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<ResubmitReason>((long)this.GetHashCode(), "A resubmit request for the unreachable queue due to reason '{0}' was not performed because the queue is frozen.", resubmitReason);
				return 0;
			}
			ExTraceGlobals.QueuingTracer.TraceDebug<ResubmitReason>((long)this.GetHashCode(), "Performing resubmit request for the unreachable queue due to reason '{0}'", resubmitReason);
			int num = base.Resubmit(resubmitReason, updateBeforeResubmit);
			if (num > 0 && resubmitReason == ResubmitReason.ConfigUpdate)
			{
				QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ResubmitDueToConfigUpdate, null, new object[]
				{
					num,
					DataStrings.UnreachableQueueNextHopDomain
				});
			}
			return num;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000AF7FC File Offset: 0x000AD9FC
		public override bool IsInterestingQueueToLog()
		{
			return base.IsInterestingQueueToLog() || base.TotalCount > 0;
		}

		// Token: 0x04001607 RID: 5639
		private static UnreachableMessageQueue instance;

		// Token: 0x04001608 RID: 5640
		private QueuingPerfCountersInstance queuingPerfCountersInstance;

		// Token: 0x04001609 RID: 5641
		private DateTime currentRoutingTablesTimestamp;
	}
}
