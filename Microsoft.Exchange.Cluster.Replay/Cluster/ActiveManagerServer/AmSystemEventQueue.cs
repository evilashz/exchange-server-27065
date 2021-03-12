using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000074 RID: 116
	internal class AmSystemEventQueue
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0001A474 File Offset: 0x00018674
		internal AmSystemEventQueue()
		{
			this.m_queue = new Queue<AmEvtBase>();
			this.m_highPriorityQueue = new Queue<AmEvtBase>();
			this.ArrivalEvent = new AutoResetEvent(false);
			this.IsEnabled = true;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001A4B0 File Offset: 0x000186B0
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0001A4B8 File Offset: 0x000186B8
		internal AutoResetEvent ArrivalEvent { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001A4C1 File Offset: 0x000186C1
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0001A4C9 File Offset: 0x000186C9
		internal bool IsEnabled { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001A4D4 File Offset: 0x000186D4
		internal bool IsEmpty
		{
			get
			{
				bool result;
				lock (this.m_locker)
				{
					result = (this.m_highPriorityQueue.Count == 0 && this.m_queue.Count == 0);
				}
				return result;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001A530 File Offset: 0x00018730
		internal void Cancel(bool isStopAcceptingEvents, bool isClearHighPriority)
		{
			AmTrace.Debug("Cancelling all system events (stopAccceptingEvents={0}, clearhighprio={1})", new object[]
			{
				isStopAcceptingEvents,
				isClearHighPriority
			});
			lock (this.m_locker)
			{
				if (isStopAcceptingEvents)
				{
					this.IsEnabled = false;
				}
				if (isClearHighPriority)
				{
					this.m_highPriorityQueue.Clear();
				}
				this.m_queue.Clear();
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001A5B4 File Offset: 0x000187B4
		internal void Stop()
		{
			this.Cancel(true, true);
			this.ArrivalEvent.Set();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001A5CC File Offset: 0x000187CC
		internal bool Enqueue(AmEvtBase evt, bool isHighPriority)
		{
			if (!this.IsEnabled)
			{
				return false;
			}
			bool result;
			lock (this.m_locker)
			{
				if (isHighPriority)
				{
					this.m_highPriorityQueue.Enqueue(evt);
				}
				else
				{
					this.m_queue.Enqueue(evt);
				}
				this.ArrivalEvent.Set();
				result = true;
			}
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001A63C File Offset: 0x0001883C
		internal AmEvtBase Dequeue()
		{
			AmEvtBase result = null;
			lock (this.m_locker)
			{
				if (this.m_highPriorityQueue.Count > 0)
				{
					result = this.m_highPriorityQueue.Dequeue();
				}
				else if (this.m_queue.Count > 0)
				{
					result = this.m_queue.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x04000210 RID: 528
		private object m_locker = new object();

		// Token: 0x04000211 RID: 529
		private Queue<AmEvtBase> m_queue;

		// Token: 0x04000212 RID: 530
		private Queue<AmEvtBase> m_highPriorityQueue;
	}
}
