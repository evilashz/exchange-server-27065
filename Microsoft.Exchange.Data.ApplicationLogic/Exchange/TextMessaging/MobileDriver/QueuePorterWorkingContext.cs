using System;
using System.Threading;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x020001AE RID: 430
	internal class QueuePorterWorkingContext<T>
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x000439A4 File Offset: 0x00041BA4
		public QueuePorterWorkingContext(ThreadSafeQueue<T> queue, QueueDataAvailableEventHandler<T> handler, int outstandingWorkersLimitHint)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (1 > outstandingWorkersLimitHint)
			{
				throw new ArgumentOutOfRangeException("outstandingWorkersLimitHint");
			}
			this.Queue = queue;
			this.QueueDataAvailableEventHandler = handler;
			this.OutstandingWorkersLimitHint = outstandingWorkersLimitHint;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000439F7 File Offset: 0x00041BF7
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x000439FF File Offset: 0x00041BFF
		public ThreadSafeQueue<T> Queue { get; private set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00043A08 File Offset: 0x00041C08
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x00043A10 File Offset: 0x00041C10
		public int OutstandingWorkersLimitHint { get; private set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00043A19 File Offset: 0x00041C19
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x00043A21 File Offset: 0x00041C21
		private QueueDataAvailableEventHandler<T> QueueDataAvailableEventHandler { get; set; }

		// Token: 0x0600107A RID: 4218 RVA: 0x00043A2C File Offset: 0x00041C2C
		public void OnDataAvailable(QueueDataAvailableEventSource<T> src, QueueDataAvailableEventArgs<T> e)
		{
			try
			{
				if (Interlocked.Increment(ref this.countOfOutstandingWorkers) == (long)this.OutstandingWorkersLimitHint)
				{
					this.Queue.PauseEvent();
				}
				this.QueueDataAvailableEventHandler(src, e);
			}
			finally
			{
				if (Interlocked.Decrement(ref this.countOfOutstandingWorkers) == (long)(this.OutstandingWorkersLimitHint - 1))
				{
					this.Queue.ResumeEvent();
				}
			}
		}

		// Token: 0x040008C0 RID: 2240
		private long countOfOutstandingWorkers;

		// Token: 0x040008C1 RID: 2241
		public QueueDataAvailableEventHandler<T> onDataAvailable;
	}
}
