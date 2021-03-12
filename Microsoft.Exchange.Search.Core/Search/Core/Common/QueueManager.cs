using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000079 RID: 121
	internal class QueueManager<T> where T : class, IEquatable<T>
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000A22C File Offset: 0x0000842C
		internal QueueManager(int capacity, int outstandingSize, ExPerformanceCounter stallPerfCounter)
		{
			this.capacity = capacity;
			this.outstandingSize = outstandingSize;
			this.queue = new Queue<T>(this.capacity);
			this.outstandingSet = new HashSet<T>();
			this.stallPerfCounter = stallPerfCounter;
			this.diagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("QueueManager", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.QueueManagerTracer, (long)this.GetHashCode());
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000A2AC File Offset: 0x000084AC
		public int Length
		{
			get
			{
				int result;
				lock (this.locker)
				{
					if (this.overflowItem == null)
					{
						result = this.queue.Count;
					}
					else
					{
						result = this.queue.Count + 1;
					}
				}
				return result;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000A310 File Offset: 0x00008510
		public int OutstandingLength
		{
			get
			{
				int count;
				lock (this.locker)
				{
					count = this.outstandingSet.Count;
				}
				return count;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000A358 File Offset: 0x00008558
		protected IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A360 File Offset: 0x00008560
		public bool Enqueue(T item)
		{
			lock (this.locker)
			{
				if (this.queue.Count < this.capacity)
				{
					this.PreEnqueue(item);
					this.queue.Enqueue(item);
					this.diagnosticsSession.TraceDebug<T>("Enqueue successfully item {0}", item);
					return true;
				}
			}
			this.diagnosticsSession.TraceDebug<T>("Enqueue failed for item {0}", item);
			return false;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A3EC File Offset: 0x000085EC
		public bool Dequeue(out IEnumerable<T> items)
		{
			items = null;
			lock (this.locker)
			{
				if (this.queue.Count == 0 && this.overflowItem == null)
				{
					this.diagnosticsSession.TraceDebug("No items to dequeue: pending queue is empty", new object[0]);
					return false;
				}
				if (this.outstandingSet.Count == this.outstandingSize)
				{
					this.diagnosticsSession.TraceDebug<int>("No items to dequeue: outstanding set is full (size={0})", this.outstandingSet.Count);
					return false;
				}
				if (this.overflowItem != null && this.outstandingSet.Contains(this.overflowItem))
				{
					this.diagnosticsSession.TraceDebug<T>("Stalled: item {0} is still being processed.", this.overflowItem);
					return false;
				}
				int num = Math.Min(this.Length, this.outstandingSize - this.outstandingSet.Count);
				List<T> list = new List<T>(num);
				if (this.overflowItem != null)
				{
					list.Add(this.overflowItem);
					this.outstandingSet.Add(this.overflowItem);
					this.overflowItem = default(T);
					num--;
					this.stallTimer.Stop();
					if (this.stallPerfCounter != null)
					{
						this.stallPerfCounter.IncrementBy(this.stallTimer.ElapsedMilliseconds);
					}
					this.stallTimer.Reset();
				}
				for (int i = 0; i < num; i++)
				{
					T item = this.queue.Dequeue();
					if (this.PostDequeue(item))
					{
						if (this.outstandingSet.Contains(item))
						{
							this.overflowItem = item;
							this.stallTimer.Start();
							break;
						}
						this.outstandingSet.Add(item);
					}
					list.Add(item);
				}
				if (list.Count > 0)
				{
					this.diagnosticsSession.TraceDebug<int, int, int>("Dequeue successfully {0} items, pending queue = {1}, outstanding set = {2}", num, this.queue.Count, this.outstandingSet.Count);
					items = list;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A610 File Offset: 0x00008810
		public bool Remove(T item)
		{
			bool flag2;
			lock (this.locker)
			{
				flag2 = this.outstandingSet.Remove(item);
			}
			this.diagnosticsSession.TraceDebug("Remove item {0}: success = {1} pending queue = {2}, outstanding set = {3}", new object[]
			{
				item,
				flag2,
				this.queue.Count,
				this.outstandingSet.Count
			});
			return flag2;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A6A8 File Offset: 0x000088A8
		protected virtual void PreEnqueue(T item)
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A6AA File Offset: 0x000088AA
		protected virtual bool PostDequeue(T item)
		{
			return true;
		}

		// Token: 0x04000155 RID: 341
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000156 RID: 342
		private readonly Queue<T> queue;

		// Token: 0x04000157 RID: 343
		private readonly HashSet<T> outstandingSet;

		// Token: 0x04000158 RID: 344
		private readonly object locker = new object();

		// Token: 0x04000159 RID: 345
		private readonly int capacity;

		// Token: 0x0400015A RID: 346
		private readonly int outstandingSize;

		// Token: 0x0400015B RID: 347
		private T overflowItem;

		// Token: 0x0400015C RID: 348
		private ExPerformanceCounter stallPerfCounter;

		// Token: 0x0400015D RID: 349
		private Stopwatch stallTimer = new Stopwatch();
	}
}
