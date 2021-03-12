using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004EE RID: 1262
	internal sealed class ThreadPoolWorkQueue
	{
		// Token: 0x06003CA1 RID: 15521 RVA: 0x000E2428 File Offset: 0x000E0628
		public ThreadPoolWorkQueue()
		{
			this.queueTail = (this.queueHead = new ThreadPoolWorkQueue.QueueSegment());
			this.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)18L);
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x000E2467 File Offset: 0x000E0667
		[SecurityCritical]
		public ThreadPoolWorkQueueThreadLocals EnsureCurrentThreadHasQueue()
		{
			if (ThreadPoolWorkQueueThreadLocals.threadLocals == null)
			{
				ThreadPoolWorkQueueThreadLocals.threadLocals = new ThreadPoolWorkQueueThreadLocals(this);
			}
			return ThreadPoolWorkQueueThreadLocals.threadLocals;
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000E2480 File Offset: 0x000E0680
		[SecurityCritical]
		internal void EnsureThreadRequested()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i < ThreadPoolGlobals.processorCount; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i + 1, i);
				if (num == i)
				{
					ThreadPool.RequestWorkerThread();
					return;
				}
			}
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000E24C0 File Offset: 0x000E06C0
		[SecurityCritical]
		internal void MarkThreadRequestSatisfied()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i > 0; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i - 1, i);
				if (num == i)
				{
					break;
				}
			}
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000E24F4 File Offset: 0x000E06F4
		[SecurityCritical]
		public void Enqueue(IThreadPoolWorkItem callback, bool forceGlobal)
		{
			ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = null;
			if (!forceGlobal)
			{
				threadPoolWorkQueueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			}
			if (this.loggingEnabled)
			{
				FrameworkEventSource.Log.ThreadPoolEnqueueWorkObject(callback);
			}
			if (threadPoolWorkQueueThreadLocals != null)
			{
				threadPoolWorkQueueThreadLocals.workStealingQueue.LocalPush(callback);
			}
			else
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueHead;
				while (!queueSegment.TryEnqueue(callback))
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref queueSegment.Next, new ThreadPoolWorkQueue.QueueSegment(), null);
					while (queueSegment.Next != null)
					{
						Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueHead, queueSegment.Next, queueSegment);
						queueSegment = this.queueHead;
					}
				}
			}
			this.EnsureThreadRequested();
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000E2588 File Offset: 0x000E0788
		[SecurityCritical]
		internal bool LocalFindAndPop(IThreadPoolWorkItem callback)
		{
			ThreadPoolWorkQueueThreadLocals threadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			return threadLocals != null && threadLocals.workStealingQueue.LocalFindAndPop(callback);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000E25AC File Offset: 0x000E07AC
		[SecurityCritical]
		public void Dequeue(ThreadPoolWorkQueueThreadLocals tl, out IThreadPoolWorkItem callback, out bool missedSteal)
		{
			callback = null;
			missedSteal = false;
			ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = tl.workStealingQueue;
			workStealingQueue.LocalPop(out callback);
			if (callback == null)
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueTail;
				while (!queueSegment.TryDequeue(out callback) && queueSegment.Next != null && queueSegment.IsUsedUp())
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueTail, queueSegment.Next, queueSegment);
					queueSegment = this.queueTail;
				}
			}
			if (callback == null)
			{
				ThreadPoolWorkQueue.WorkStealingQueue[] array = ThreadPoolWorkQueue.allThreadQueues.Current;
				int num = tl.random.Next(array.Length);
				for (int i = array.Length; i > 0; i--)
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue2 = Volatile.Read<ThreadPoolWorkQueue.WorkStealingQueue>(ref array[num % array.Length]);
					if (workStealingQueue2 != null && workStealingQueue2 != workStealingQueue && workStealingQueue2.TrySteal(out callback, ref missedSteal))
					{
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000E2670 File Offset: 0x000E0870
		[SecurityCritical]
		internal static bool Dispatch()
		{
			ThreadPoolWorkQueue workQueue = ThreadPoolGlobals.workQueue;
			int tickCount = Environment.TickCount;
			workQueue.MarkThreadRequestSatisfied();
			workQueue.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)18L);
			bool flag = true;
			IThreadPoolWorkItem threadPoolWorkItem = null;
			try
			{
				ThreadPoolWorkQueueThreadLocals tl = workQueue.EnsureCurrentThreadHasQueue();
				while ((long)(Environment.TickCount - tickCount) < (long)((ulong)ThreadPoolGlobals.tpQuantum))
				{
					try
					{
					}
					finally
					{
						bool flag2 = false;
						workQueue.Dequeue(tl, out threadPoolWorkItem, out flag2);
						if (threadPoolWorkItem == null)
						{
							flag = flag2;
						}
						else
						{
							workQueue.EnsureThreadRequested();
						}
					}
					if (threadPoolWorkItem == null)
					{
						return true;
					}
					if (workQueue.loggingEnabled)
					{
						FrameworkEventSource.Log.ThreadPoolDequeueWorkObject(threadPoolWorkItem);
					}
					if (ThreadPoolGlobals.enableWorkerTracking)
					{
						bool flag3 = false;
						try
						{
							try
							{
							}
							finally
							{
								ThreadPool.ReportThreadStatus(true);
								flag3 = true;
							}
							threadPoolWorkItem.ExecuteWorkItem();
							threadPoolWorkItem = null;
							goto IL_A6;
						}
						finally
						{
							if (flag3)
							{
								ThreadPool.ReportThreadStatus(false);
							}
						}
						goto IL_9E;
					}
					goto IL_9E;
					IL_A6:
					if (!ThreadPool.NotifyWorkItemComplete())
					{
						return false;
					}
					continue;
					IL_9E:
					threadPoolWorkItem.ExecuteWorkItem();
					threadPoolWorkItem = null;
					goto IL_A6;
				}
				return true;
			}
			catch (ThreadAbortException tae)
			{
				if (threadPoolWorkItem != null)
				{
					threadPoolWorkItem.MarkAborted(tae);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					workQueue.EnsureThreadRequested();
				}
			}
			return true;
		}

		// Token: 0x0400194B RID: 6475
		internal volatile ThreadPoolWorkQueue.QueueSegment queueHead;

		// Token: 0x0400194C RID: 6476
		internal volatile ThreadPoolWorkQueue.QueueSegment queueTail;

		// Token: 0x0400194D RID: 6477
		internal bool loggingEnabled;

		// Token: 0x0400194E RID: 6478
		internal static ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue> allThreadQueues = new ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue>(16);

		// Token: 0x0400194F RID: 6479
		private volatile int numOutstandingThreadRequests;

		// Token: 0x02000BBE RID: 3006
		internal class SparseArray<T> where T : class
		{
			// Token: 0x06006E3E RID: 28222 RVA: 0x0017B082 File Offset: 0x00179282
			internal SparseArray(int initialSize)
			{
				this.m_array = new T[initialSize];
			}

			// Token: 0x17001307 RID: 4871
			// (get) Token: 0x06006E3F RID: 28223 RVA: 0x0017B098 File Offset: 0x00179298
			internal T[] Current
			{
				get
				{
					return this.m_array;
				}
			}

			// Token: 0x06006E40 RID: 28224 RVA: 0x0017B0A4 File Offset: 0x001792A4
			internal int Add(T e)
			{
				for (;;)
				{
					T[] array = this.m_array;
					T[] obj = array;
					lock (obj)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] == null)
							{
								Volatile.Write<T>(ref array[i], e);
								return i;
							}
							if (i == array.Length - 1 && array == this.m_array)
							{
								T[] array2 = new T[array.Length * 2];
								Array.Copy(array, array2, i + 1);
								array2[i + 1] = e;
								this.m_array = array2;
								return i + 1;
							}
						}
						continue;
					}
					break;
				}
				int result;
				return result;
			}

			// Token: 0x06006E41 RID: 28225 RVA: 0x0017B15C File Offset: 0x0017935C
			internal void Remove(T e)
			{
				T[] array = this.m_array;
				T[] obj = array;
				lock (obj)
				{
					for (int i = 0; i < this.m_array.Length; i++)
					{
						if (this.m_array[i] == e)
						{
							Volatile.Write<T>(ref this.m_array[i], default(T));
							break;
						}
					}
				}
			}

			// Token: 0x0400353B RID: 13627
			private volatile T[] m_array;
		}

		// Token: 0x02000BBF RID: 3007
		internal class WorkStealingQueue
		{
			// Token: 0x06006E42 RID: 28226 RVA: 0x0017B1EC File Offset: 0x001793EC
			public void LocalPush(IThreadPoolWorkItem obj)
			{
				int num = this.m_tailIndex;
				if (num == 2147483647)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.Enter(ref flag);
						if (this.m_tailIndex == 2147483647)
						{
							this.m_headIndex &= this.m_mask;
							num = (this.m_tailIndex &= this.m_mask);
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(true);
						}
					}
				}
				if (num < this.m_headIndex + this.m_mask)
				{
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
					return;
				}
				bool flag2 = false;
				try
				{
					this.m_foreignLock.Enter(ref flag2);
					int headIndex = this.m_headIndex;
					int num2 = this.m_tailIndex - this.m_headIndex;
					if (num2 >= this.m_mask)
					{
						IThreadPoolWorkItem[] array = new IThreadPoolWorkItem[this.m_array.Length << 1];
						for (int i = 0; i < this.m_array.Length; i++)
						{
							array[i] = this.m_array[i + headIndex & this.m_mask];
						}
						this.m_array = array;
						this.m_headIndex = 0;
						num = (this.m_tailIndex = num2);
						this.m_mask = (this.m_mask << 1 | 1);
					}
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
				}
				finally
				{
					if (flag2)
					{
						this.m_foreignLock.Exit(false);
					}
				}
			}

			// Token: 0x06006E43 RID: 28227 RVA: 0x0017B3B4 File Offset: 0x001795B4
			public bool LocalFindAndPop(IThreadPoolWorkItem obj)
			{
				if (this.m_array[this.m_tailIndex - 1 & this.m_mask] == obj)
				{
					IThreadPoolWorkItem threadPoolWorkItem;
					return this.LocalPop(out threadPoolWorkItem);
				}
				for (int i = this.m_tailIndex - 2; i >= this.m_headIndex; i--)
				{
					if (this.m_array[i & this.m_mask] == obj)
					{
						bool flag = false;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_array[i & this.m_mask] == null)
							{
								return false;
							}
							Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[i & this.m_mask], null);
							if (i == this.m_tailIndex)
							{
								this.m_tailIndex--;
							}
							else if (i == this.m_headIndex)
							{
								this.m_headIndex++;
							}
							return true;
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
					}
				}
				return false;
			}

			// Token: 0x06006E44 RID: 28228 RVA: 0x0017B4D4 File Offset: 0x001796D4
			public bool LocalPop(out IThreadPoolWorkItem obj)
			{
				int num3;
				for (;;)
				{
					int num = this.m_tailIndex;
					if (this.m_headIndex >= num)
					{
						break;
					}
					num--;
					Interlocked.Exchange(ref this.m_tailIndex, num);
					if (this.m_headIndex > num)
					{
						bool flag = false;
						bool result;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_headIndex <= num)
							{
								int num2 = num & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num2]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num2] = null;
								result = true;
							}
							else
							{
								this.m_tailIndex = num + 1;
								obj = null;
								result = false;
							}
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
						return result;
					}
					num3 = (num & this.m_mask);
					obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num3]);
					if (obj != null)
					{
						goto Block_2;
					}
				}
				obj = null;
				return false;
				Block_2:
				this.m_array[num3] = null;
				return true;
			}

			// Token: 0x06006E45 RID: 28229 RVA: 0x0017B5D0 File Offset: 0x001797D0
			public bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal)
			{
				return this.TrySteal(out obj, ref missedSteal, 0);
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x0017B5DC File Offset: 0x001797DC
			private bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal, int millisecondsTimeout)
			{
				obj = null;
				while (this.m_headIndex < this.m_tailIndex)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.TryEnter(millisecondsTimeout, ref flag);
						if (flag)
						{
							int headIndex = this.m_headIndex;
							Interlocked.Exchange(ref this.m_headIndex, headIndex + 1);
							if (headIndex < this.m_tailIndex)
							{
								int num = headIndex & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num] = null;
								return true;
							}
							else
							{
								this.m_headIndex = headIndex;
								obj = null;
								missedSteal = true;
							}
						}
						else
						{
							missedSteal = true;
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(false);
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x0400353C RID: 13628
			private const int INITIAL_SIZE = 32;

			// Token: 0x0400353D RID: 13629
			internal volatile IThreadPoolWorkItem[] m_array = new IThreadPoolWorkItem[32];

			// Token: 0x0400353E RID: 13630
			private volatile int m_mask = 31;

			// Token: 0x0400353F RID: 13631
			private const int START_INDEX = 0;

			// Token: 0x04003540 RID: 13632
			private volatile int m_headIndex;

			// Token: 0x04003541 RID: 13633
			private volatile int m_tailIndex;

			// Token: 0x04003542 RID: 13634
			private SpinLock m_foreignLock = new SpinLock(false);
		}

		// Token: 0x02000BC0 RID: 3008
		internal class QueueSegment
		{
			// Token: 0x06006E48 RID: 28232 RVA: 0x0017B6D4 File Offset: 0x001798D4
			private void GetIndexes(out int upper, out int lower)
			{
				int num = this.indexes;
				upper = (num >> 16 & 65535);
				lower = (num & 65535);
			}

			// Token: 0x06006E49 RID: 28233 RVA: 0x0017B700 File Offset: 0x00179900
			private bool CompareExchangeIndexes(ref int prevUpper, int newUpper, ref int prevLower, int newLower)
			{
				int num = prevUpper << 16 | (prevLower & 65535);
				int value = newUpper << 16 | (newLower & 65535);
				int num2 = Interlocked.CompareExchange(ref this.indexes, value, num);
				prevUpper = (num2 >> 16 & 65535);
				prevLower = (num2 & 65535);
				return num2 == num;
			}

			// Token: 0x06006E4A RID: 28234 RVA: 0x0017B751 File Offset: 0x00179951
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public QueueSegment()
			{
				this.nodes = new IThreadPoolWorkItem[256];
			}

			// Token: 0x06006E4B RID: 28235 RVA: 0x0017B76C File Offset: 0x0017996C
			public bool IsUsedUp()
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				return num == this.nodes.Length && num2 == this.nodes.Length;
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x0017B79C File Offset: 0x0017999C
			public bool TryEnqueue(IThreadPoolWorkItem node)
			{
				int num;
				int newLower;
				this.GetIndexes(out num, out newLower);
				while (num != this.nodes.Length)
				{
					if (this.CompareExchangeIndexes(ref num, num + 1, ref newLower, newLower))
					{
						Volatile.Write<IThreadPoolWorkItem>(ref this.nodes[num], node);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006E4D RID: 28237 RVA: 0x0017B7E4 File Offset: 0x001799E4
			public bool TryDequeue(out IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num2 != num)
				{
					if (this.CompareExchangeIndexes(ref num, num, ref num2, num2 + 1))
					{
						SpinWait spinWait = default(SpinWait);
						for (;;)
						{
							IThreadPoolWorkItem threadPoolWorkItem;
							node = (threadPoolWorkItem = Volatile.Read<IThreadPoolWorkItem>(ref this.nodes[num2]));
							if (threadPoolWorkItem != null)
							{
								break;
							}
							spinWait.SpinOnce();
						}
						this.nodes[num2] = null;
						return true;
					}
				}
				node = null;
				return false;
			}

			// Token: 0x04003543 RID: 13635
			internal readonly IThreadPoolWorkItem[] nodes;

			// Token: 0x04003544 RID: 13636
			private const int QueueSegmentLength = 256;

			// Token: 0x04003545 RID: 13637
			private volatile int indexes;

			// Token: 0x04003546 RID: 13638
			public volatile ThreadPoolWorkQueue.QueueSegment Next;

			// Token: 0x04003547 RID: 13639
			private const int SixteenBits = 65535;
		}
	}
}
