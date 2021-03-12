using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000026 RID: 38
	internal class ThreadSafeQueue<T> where T : class
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00009806 File Offset: 0x00007A06
		public ThreadSafeQueue() : this(1000)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009814 File Offset: 0x00007A14
		public ThreadSafeQueue(int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentOutOfRangeException("capacity should be greater than 0.");
			}
			this.capacity = capacity;
			this.queue = new ConcurrentQueue<T>();
			this.consumerSemaphore = new Semaphore(0, this.capacity);
			this.producerSemaphore = new Semaphore(this.capacity, this.capacity);
			this.ConsumerSemaphoreCount = 0;
			this.ProducerSemaphoreCount = this.capacity;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009884 File Offset: 0x00007A84
		public int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009891 File Offset: 0x00007A91
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00009899 File Offset: 0x00007A99
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
			private set
			{
				this.capacity = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000098A2 File Offset: 0x00007AA2
		public bool IsFull
		{
			get
			{
				return this.capacity == this.Count;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000098B2 File Offset: 0x00007AB2
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000098BA File Offset: 0x00007ABA
		public int ConsumerSemaphoreCount { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000098C3 File Offset: 0x00007AC3
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000098CB File Offset: 0x00007ACB
		public int ProducerSemaphoreCount { get; set; }

		// Token: 0x060001E9 RID: 489 RVA: 0x000098D4 File Offset: 0x00007AD4
		public void Enqueue(T item, CancellationContext cancellationContext)
		{
			if (cancellationContext == null)
			{
				this.producerSemaphore.WaitOne();
			}
			else if (WaitHandle.WaitAny(new WaitHandle[]
			{
				cancellationContext.StopToken.WaitHandle,
				this.producerSemaphore
			}) != 1)
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3} ?---InProducerFullSkip ", new object[]
				{
					typeof(T),
					Thread.CurrentThread.ManagedThreadId,
					this.queue.Count,
					this.Capacity
				}), "", "");
				return;
			}
			this.Enqueue(item);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009990 File Offset: 0x00007B90
		public T Dequeue(CancellationContext cancellationContext)
		{
			if (cancellationContext == null)
			{
				this.consumerSemaphore.WaitOne();
			}
			else if (WaitHandle.WaitAny(new WaitHandle[]
			{
				cancellationContext.StopToken.WaitHandle,
				this.consumerSemaphore
			}) != 1)
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3} ?---InDequeueEmptySkip ", new object[]
				{
					typeof(T),
					Thread.CurrentThread.ManagedThreadId,
					this.queue.Count,
					this.Capacity
				}), "", "");
				return default(T);
			}
			return this.Dequeue();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009A53 File Offset: 0x00007C53
		public void Close()
		{
			this.consumerSemaphore.Close();
			this.producerSemaphore.Close();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009A6C File Offset: 0x00007C6C
		private T Dequeue()
		{
			T result = default(T);
			if (!this.queue.TryDequeue(out result))
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3} ?---InDequeuExp ", new object[]
				{
					typeof(T),
					Thread.CurrentThread.ManagedThreadId,
					this.queue.Count,
					this.Capacity
				}), "", "");
				throw new Exception("TryDequeue should always return true");
			}
			int num = this.producerSemaphore.Release();
			this.ProducerSemaphoreCount = num + 1;
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3}, consumerSemaphore={4} ?---InDequeuDone ", new object[]
			{
				typeof(T),
				Thread.CurrentThread.ManagedThreadId,
				this.queue.Count,
				this.Capacity,
				num
			}), "", "");
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009B8C File Offset: 0x00007D8C
		private void Enqueue(T item)
		{
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3} ?---In ", new object[]
			{
				typeof(T),
				Thread.CurrentThread.ManagedThreadId,
				this.queue.Count,
				this.Capacity
			}), "", "");
			if (this.queue.Count >= this.Capacity)
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3} ?---InEnqueuFull ", new object[]
				{
					typeof(T),
					Thread.CurrentThread.ManagedThreadId,
					this.queue.Count,
					this.Capacity
				}), "", "");
				throw new Exception("Queue should never be full here. The caller should have acquired writer quota");
			}
			this.queue.Enqueue(item);
			int num = this.consumerSemaphore.Release();
			this.ConsumerSemaphoreCount = num + 1;
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("Enqueue: called : Type={0}, thread={1}, queueSize={2}, capacity={3}, consumerSemaphore={4} ?---InDone ", new object[]
			{
				typeof(T),
				Thread.CurrentThread.ManagedThreadId,
				this.queue.Count,
				this.Capacity,
				num
			}), "", "");
		}

		// Token: 0x0400011A RID: 282
		private readonly ConcurrentQueue<T> queue;

		// Token: 0x0400011B RID: 283
		private int capacity;

		// Token: 0x0400011C RID: 284
		private Semaphore consumerSemaphore;

		// Token: 0x0400011D RID: 285
		private Semaphore producerSemaphore;
	}
}
