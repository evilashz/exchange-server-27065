using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004EF RID: 1263
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x06003CAA RID: 15530 RVA: 0x000E27B6 File Offset: 0x000E09B6
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000E27F8 File Offset: 0x000E09F8
		[SecurityCritical]
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem callback = null;
							if (this.workStealingQueue.LocalPop(out callback))
							{
								this.workQueue.Enqueue(callback, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000E2864 File Offset: 0x000E0A64
		[SecuritySafeCritical]
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04001950 RID: 6480
		[ThreadStatic]
		[SecurityCritical]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04001951 RID: 6481
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04001952 RID: 6482
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04001953 RID: 6483
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
