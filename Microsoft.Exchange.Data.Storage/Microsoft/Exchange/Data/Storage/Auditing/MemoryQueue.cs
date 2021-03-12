using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F53 RID: 3923
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MemoryQueue<T> : DisposeTrackableBase, IQueue<T>, IDisposable
	{
		// Token: 0x0600868E RID: 34446 RVA: 0x0024EA0A File Offset: 0x0024CC0A
		internal MemoryQueue()
		{
			this.queue = new BlockingCollection<QueueMessage<T>>();
		}

		// Token: 0x0600868F RID: 34447 RVA: 0x0024EA20 File Offset: 0x0024CC20
		public void Send(T data)
		{
			base.CheckDisposed();
			QueueMessage<T> item = new QueueMessage<T>
			{
				Data = data
			};
			this.queue.Add(item);
		}

		// Token: 0x06008690 RID: 34448 RVA: 0x0024EA50 File Offset: 0x0024CC50
		public IQueueMessage<T> GetNext(int timeoutInMilliseconds, CancellationToken cancel)
		{
			base.CheckDisposed();
			QueueMessage<T> result = null;
			try
			{
				this.queue.TryTake(out result, timeoutInMilliseconds, cancel);
			}
			catch (OperationCanceledException)
			{
			}
			return result;
		}

		// Token: 0x06008691 RID: 34449 RVA: 0x0024EA8C File Offset: 0x0024CC8C
		public void Commit(IQueueMessage<T> message)
		{
			base.CheckDisposed();
		}

		// Token: 0x06008692 RID: 34450 RVA: 0x0024EA94 File Offset: 0x0024CC94
		public void Rollback(IQueueMessage<T> message)
		{
			base.CheckDisposed();
			this.Send(message.Data);
		}

		// Token: 0x06008693 RID: 34451 RVA: 0x0024EAA8 File Offset: 0x0024CCA8
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.queue.Dispose();
			}
			this.queue = null;
		}

		// Token: 0x06008694 RID: 34452 RVA: 0x0024EABF File Offset: 0x0024CCBF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MemoryQueue<T>>(this);
		}

		// Token: 0x04005A09 RID: 23049
		private BlockingCollection<QueueMessage<T>> queue;
	}
}
