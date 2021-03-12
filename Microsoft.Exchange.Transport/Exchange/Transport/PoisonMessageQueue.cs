using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000317 RID: 791
	internal sealed class PoisonMessageQueue
	{
		// Token: 0x06002231 RID: 8753 RVA: 0x00080FCF File Offset: 0x0007F1CF
		private PoisonMessageQueue()
		{
			this.queuingPerfCountersInstance = QueueManager.GetTotalPerfCounters();
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x00080FEF File Offset: 0x0007F1EF
		public static PoisonMessageQueue Instance
		{
			get
			{
				return PoisonMessageQueue.instance;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00080FF6 File Offset: 0x0007F1F6
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00081003 File Offset: 0x0007F203
		public bool IsEmpty
		{
			get
			{
				return this.items.Count == 0;
			}
		}

		// Token: 0x17000AEA RID: 2794
		public TransportMailItem this[long mailItemId]
		{
			get
			{
				TransportMailItem result;
				lock (this.items)
				{
					this.items.TryGetValue(mailItemId, out result);
				}
				return result;
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00081060 File Offset: 0x0007F260
		public void Enqueue(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				ExTraceGlobals.PoisonTracer.TraceError(0L, "Skipping Enqueue. MailItem is null.");
				return;
			}
			bool flag = false;
			lock (this.items)
			{
				if (!this.items.ContainsKey(mailItem.RecordId))
				{
					this.items.Add(mailItem.RecordId, mailItem);
					flag = true;
				}
				else
				{
					ExTraceGlobals.PoisonTracer.TraceError<long>(0L, "Queue already contains the mailitem with id {0}.", mailItem.RecordId);
				}
			}
			if (flag)
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.PoisonQueue, mailItem.LatencyTracker);
				if (this.queuingPerfCountersInstance != null)
				{
					this.queuingPerfCountersInstance.PoisonQueueLength.Increment();
				}
			}
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0008111C File Offset: 0x0007F31C
		public TransportMailItem Extract(long mailItemId)
		{
			TransportMailItem transportMailItem = null;
			lock (this.items)
			{
				if (this.items.TryGetValue(mailItemId, out transportMailItem))
				{
					this.items.Remove(mailItemId);
					if (this.queuingPerfCountersInstance != null)
					{
						this.queuingPerfCountersInstance.PoisonQueueLength.Decrement();
					}
				}
			}
			if (transportMailItem != null)
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.PoisonQueue, transportMailItem.LatencyTracker);
			}
			return transportMailItem;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000811A0 File Offset: 0x0007F3A0
		public void VisitMailItems(Func<TransportMailItem, bool> visitor)
		{
			if (visitor == null)
			{
				throw new ArgumentNullException("visitor");
			}
			lock (this.items)
			{
				foreach (TransportMailItem arg in this.items.Values)
				{
					if (!visitor(arg))
					{
						break;
					}
				}
			}
		}

		// Token: 0x040011DB RID: 4571
		private static PoisonMessageQueue instance = new PoisonMessageQueue();

		// Token: 0x040011DC RID: 4572
		private Dictionary<long, TransportMailItem> items = new Dictionary<long, TransportMailItem>(10);

		// Token: 0x040011DD RID: 4573
		private QueuingPerfCountersInstance queuingPerfCountersInstance;
	}
}
