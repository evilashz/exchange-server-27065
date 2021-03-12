using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200000C RID: 12
	internal sealed class DispatchPool : IDisposable
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public DispatchPool(string threadPoolDescription, int maxTaskQueueSize, int maxThreads, int minThreads, IExPerformanceCounter counterDispatchTaskQueueLength, IExPerformanceCounter counterDispatchTaskThreads, IExPerformanceCounter counterDispatchTaskActiveThreads, IExPerformanceCounter counterDispatchTaskRate)
		{
			if (maxTaskQueueSize < 1)
			{
				throw new ArgumentOutOfRangeException("maxTaskQueueSize");
			}
			if (maxThreads < 1)
			{
				throw new ArgumentOutOfRangeException("maxThreads");
			}
			if (minThreads < 0)
			{
				throw new ArgumentOutOfRangeException("minThreads");
			}
			if (minThreads > maxThreads)
			{
				minThreads = maxThreads;
			}
			this.threadPoolDescription = threadPoolDescription;
			this.counterDispatchTaskQueueLength = counterDispatchTaskQueueLength;
			DispatchPool.SetPerformanceCounter(this.counterDispatchTaskQueueLength, 0);
			this.counterDispatchTaskThreads = counterDispatchTaskThreads;
			DispatchPool.SetPerformanceCounter(this.counterDispatchTaskThreads, 0);
			this.counterDispatchTaskActiveThreads = counterDispatchTaskActiveThreads;
			DispatchPool.SetPerformanceCounter(this.counterDispatchTaskActiveThreads, 0);
			this.counterDispatchTaskRate = counterDispatchTaskRate;
			this.maxTaskQueueSize = maxTaskQueueSize;
			this.minThreads = minThreads;
			this.taskQueue = new Queue<DispatchTask>(this.maxTaskQueueSize);
			this.threadInfoArray = new DispatchPool.DispatchThreadInfo[maxThreads];
			for (int i = 0; i < maxThreads; i++)
			{
				this.threadInfoArray[i] = new DispatchPool.DispatchThreadInfo();
			}
			this.threadStateCount[0] = maxThreads;
			lock (this.poolLock)
			{
				for (int j = 0; j < this.minThreads; j++)
				{
					DispatchPool.UpdateState(this.threadInfoArray[j], this.threadStateCount, DispatchPool.DispatchThreadState.StartingUp);
					this.threadInfoArray[j].Wakeup();
					this.SpinUpThread(j);
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003C18 File Offset: 0x00001E18
		public int ActiveThreads
		{
			get
			{
				return this.threadStateCount[4];
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C24 File Offset: 0x00001E24
		public bool SubmitTask(DispatchTask task)
		{
			DispatchPool.IncrementPerformanceCounter(this.counterDispatchTaskQueueLength);
			bool flag = false;
			try
			{
				lock (this.poolLock)
				{
					if (this.isDisposed || this.taskQueue.Count >= this.maxTaskQueueSize)
					{
						return false;
					}
					this.taskQueue.Enqueue(task);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					DispatchPool.DecrementPerformanceCounter(this.counterDispatchTaskQueueLength);
				}
			}
			this.ProcessThreads();
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003CC0 File Offset: 0x00001EC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void IncrementPerformanceCounter(IExPerformanceCounter counter)
		{
			if (counter != null)
			{
				counter.Increment();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003CCC File Offset: 0x00001ECC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void DecrementPerformanceCounter(IExPerformanceCounter counter)
		{
			if (counter != null)
			{
				counter.Decrement();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003CD8 File Offset: 0x00001ED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void SetPerformanceCounter(IExPerformanceCounter counter, int value)
		{
			if (counter != null)
			{
				counter.RawValue = (long)value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003CE5 File Offset: 0x00001EE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void UpdateState(DispatchPool.DispatchThreadInfo threadInfo, int[] threadStateCount, DispatchPool.DispatchThreadState newState)
		{
			threadStateCount[(int)threadInfo.State]--;
			threadInfo.State = newState;
			threadStateCount[(int)newState] = threadStateCount[(int)newState] + 1;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003D1C File Offset: 0x00001F1C
		private bool TryGetTask(DispatchPool.DispatchThreadInfo threadInfo, out bool shutdown, out DispatchTask task)
		{
			task = null;
			shutdown = false;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			lock (this.poolLock)
			{
				if (this.isDisposed || threadInfo.State == DispatchPool.DispatchThreadState.ShuttingDown)
				{
					DispatchPool.UpdateState(threadInfo, this.threadStateCount, DispatchPool.DispatchThreadState.None);
					shutdown = true;
					return false;
				}
				if (this.taskQueue.Count == 0)
				{
					if (threadInfo.State != DispatchPool.DispatchThreadState.Inactive)
					{
						if (threadInfo.State == DispatchPool.DispatchThreadState.Active)
						{
							flag2 = true;
						}
						DispatchPool.UpdateState(threadInfo, this.threadStateCount, DispatchPool.DispatchThreadState.Inactive);
					}
				}
				else
				{
					if (threadInfo.State != DispatchPool.DispatchThreadState.Active)
					{
						DispatchPool.UpdateState(threadInfo, this.threadStateCount, DispatchPool.DispatchThreadState.Active);
						flag = true;
					}
					task = this.taskQueue.Dequeue();
					flag3 = true;
				}
			}
			if (flag3)
			{
				DispatchPool.DecrementPerformanceCounter(this.counterDispatchTaskQueueLength);
			}
			if (flag)
			{
				DispatchPool.IncrementPerformanceCounter(this.counterDispatchTaskActiveThreads);
			}
			else if (flag2)
			{
				DispatchPool.DecrementPerformanceCounter(this.counterDispatchTaskActiveThreads);
			}
			this.ProcessThreads();
			return task != null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E24 File Offset: 0x00002024
		private bool DispatchTasks(DispatchPool.DispatchThreadInfo threadInfo)
		{
			DispatchTask dispatchTask = null;
			bool flag = false;
			while (this.TryGetTask(threadInfo, out flag, out dispatchTask))
			{
				dispatchTask.Execute();
				dispatchTask = null;
				DispatchPool.IncrementPerformanceCounter(this.counterDispatchTaskRate);
			}
			return !flag;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003E5C File Offset: 0x0000205C
		private void DispatchWorkerThread(object obj)
		{
			DispatchPool.DispatchThreadInfo dispatchThreadInfo = (DispatchPool.DispatchThreadInfo)obj;
			DispatchPool.IncrementPerformanceCounter(this.counterDispatchTaskThreads);
			try
			{
				while (this.DispatchTasks(dispatchThreadInfo))
				{
					while (!dispatchThreadInfo.Wait())
					{
						this.ProcessThreads();
					}
				}
			}
			finally
			{
				DispatchPool.DecrementPerformanceCounter(this.counterDispatchTaskThreads);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003EB4 File Offset: 0x000020B4
		internal static void CheckThreads(int taskQueueSize, int[] threadStateCount, DispatchPool.DispatchThreadInfo[] threadInfoArray, int minThreads, out int startupThread, out int shutdownThread, out int activateThread)
		{
			startupThread = -1;
			shutdownThread = -1;
			activateThread = -1;
			int num = threadStateCount[3] + threadStateCount[1];
			if (threadStateCount[4] + num >= threadInfoArray.Length)
			{
				return;
			}
			if (taskQueueSize > num)
			{
				if (threadStateCount[2] + threadStateCount[5] > 0)
				{
					for (int i = 0; i < threadInfoArray.Length; i++)
					{
						DispatchPool.DispatchThreadInfo dispatchThreadInfo = threadInfoArray[i];
						if (dispatchThreadInfo.State == DispatchPool.DispatchThreadState.Inactive || dispatchThreadInfo.State == DispatchPool.DispatchThreadState.ShuttingDown)
						{
							DispatchPool.UpdateState(dispatchThreadInfo, threadStateCount, DispatchPool.DispatchThreadState.Activating);
							activateThread = i;
							break;
						}
					}
				}
				if (activateThread == -1 && threadStateCount[0] > 0)
				{
					for (int j = 0; j < threadInfoArray.Length; j++)
					{
						DispatchPool.DispatchThreadInfo dispatchThreadInfo = threadInfoArray[j];
						if (dispatchThreadInfo.State == DispatchPool.DispatchThreadState.None)
						{
							DispatchPool.UpdateState(dispatchThreadInfo, threadStateCount, DispatchPool.DispatchThreadState.StartingUp);
							startupThread = j;
							break;
						}
					}
				}
			}
			if (startupThread == -1 && threadStateCount[2] > 0)
			{
				DateTime utcNow = DateTime.UtcNow;
				for (int k = threadInfoArray.Length - 1; k >= minThreads; k--)
				{
					DispatchPool.DispatchThreadInfo dispatchThreadInfo = threadInfoArray[k];
					if (dispatchThreadInfo.State == DispatchPool.DispatchThreadState.Inactive && utcNow - dispatchThreadInfo.InactiveTime > DispatchPool.DispatchThreadInfo.InactiveThreadShutdownDelay)
					{
						DispatchPool.UpdateState(dispatchThreadInfo, threadStateCount, DispatchPool.DispatchThreadState.ShuttingDown);
						shutdownThread = k;
						return;
					}
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003FB0 File Offset: 0x000021B0
		private void ProcessThreads()
		{
			if (Interlocked.CompareExchange(ref this.processThreadsLock, 1, 0) == 1)
			{
				return;
			}
			bool flag = true;
			try
			{
				int num;
				int num2;
				int num3;
				do
				{
					lock (this.poolLock)
					{
						if (this.isDisposed)
						{
							break;
						}
						DispatchPool.CheckThreads(this.taskQueue.Count, this.threadStateCount, this.threadInfoArray, this.minThreads, out num, out num2, out num3);
						if (num == -1 && num2 == -1 && num3 == -1)
						{
							flag = false;
							this.processThreadsLock = 0;
						}
					}
					if (num != -1)
					{
						this.threadInfoArray[num].Wakeup();
						this.SpinUpThread(num);
					}
					if (num2 != -1)
					{
						this.threadInfoArray[num2].Wakeup();
					}
					if (num3 != -1)
					{
						this.threadInfoArray[num3].Wakeup();
					}
				}
				while (num != -1 || num2 != -1 || num3 != -1);
			}
			finally
			{
				if (flag)
				{
					this.processThreadsLock = 0;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000040B4 File Offset: 0x000022B4
		private void SpinUpThread(int threadId)
		{
			new Thread(new ParameterizedThreadStart(this.DispatchWorkerThread))
			{
				IsBackground = true,
				Name = string.Format("{0}{1}", this.threadPoolDescription, threadId)
			}.Start(this.threadInfoArray[threadId]);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004104 File Offset: 0x00002304
		public void Dispose()
		{
			if (this.isDisposed)
			{
				return;
			}
			Queue<DispatchTask> queue = null;
			lock (this.poolLock)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.isDisposed = true;
				queue = this.taskQueue;
				this.taskQueue.Clear();
			}
			if (this.threadInfoArray != null)
			{
				for (int i = 0; i < this.threadInfoArray.Length; i++)
				{
					this.threadInfoArray[i].Wakeup();
				}
			}
			if (queue != null)
			{
				while (queue.Count > 0)
				{
					DispatchTask dispatchTask = queue.Dequeue();
					dispatchTask.Cancel();
				}
			}
		}

		// Token: 0x04000041 RID: 65
		private readonly int maxTaskQueueSize;

		// Token: 0x04000042 RID: 66
		private readonly int minThreads;

		// Token: 0x04000043 RID: 67
		private readonly object poolLock = new object();

		// Token: 0x04000044 RID: 68
		private readonly Queue<DispatchTask> taskQueue;

		// Token: 0x04000045 RID: 69
		private readonly DispatchPool.DispatchThreadInfo[] threadInfoArray;

		// Token: 0x04000046 RID: 70
		private readonly int[] threadStateCount = new int[6];

		// Token: 0x04000047 RID: 71
		private IExPerformanceCounter counterDispatchTaskQueueLength;

		// Token: 0x04000048 RID: 72
		private IExPerformanceCounter counterDispatchTaskThreads;

		// Token: 0x04000049 RID: 73
		private IExPerformanceCounter counterDispatchTaskActiveThreads;

		// Token: 0x0400004A RID: 74
		private IExPerformanceCounter counterDispatchTaskRate;

		// Token: 0x0400004B RID: 75
		private string threadPoolDescription;

		// Token: 0x0400004C RID: 76
		private int processThreadsLock;

		// Token: 0x0400004D RID: 77
		private bool isDisposed;

		// Token: 0x0200000D RID: 13
		internal enum DispatchThreadState
		{
			// Token: 0x0400004F RID: 79
			None,
			// Token: 0x04000050 RID: 80
			StartingUp,
			// Token: 0x04000051 RID: 81
			Inactive,
			// Token: 0x04000052 RID: 82
			Activating,
			// Token: 0x04000053 RID: 83
			Active,
			// Token: 0x04000054 RID: 84
			ShuttingDown,
			// Token: 0x04000055 RID: 85
			Max
		}

		// Token: 0x0200000E RID: 14
		internal class DispatchThreadInfo
		{
			// Token: 0x06000076 RID: 118 RVA: 0x000041B4 File Offset: 0x000023B4
			internal DispatchThreadInfo()
			{
				this.state = DispatchPool.DispatchThreadState.None;
				this.inactiveTime = DateTime.MinValue;
			}

			// Token: 0x06000077 RID: 119 RVA: 0x000041DA File Offset: 0x000023DA
			internal DispatchThreadInfo(DispatchPool.DispatchThreadState state, DateTime inactiveTime)
			{
				this.state = state;
				this.inactiveTime = inactiveTime;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000078 RID: 120 RVA: 0x000041FC File Offset: 0x000023FC
			public AutoResetEvent WaitEvent
			{
				get
				{
					return this.waitEvent;
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00004204 File Offset: 0x00002404
			// (set) Token: 0x0600007A RID: 122 RVA: 0x0000420C File Offset: 0x0000240C
			public DispatchPool.DispatchThreadState State
			{
				get
				{
					return this.state;
				}
				set
				{
					if (value == DispatchPool.DispatchThreadState.Inactive && (this.state == DispatchPool.DispatchThreadState.Active || this.state == DispatchPool.DispatchThreadState.StartingUp))
					{
						this.inactiveTime = DateTime.UtcNow;
					}
					this.state = value;
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00004236 File Offset: 0x00002436
			public DateTime InactiveTime
			{
				get
				{
					return this.inactiveTime;
				}
			}

			// Token: 0x0600007C RID: 124 RVA: 0x0000423E File Offset: 0x0000243E
			public void Wakeup()
			{
				this.waitEvent.Set();
			}

			// Token: 0x0600007D RID: 125 RVA: 0x0000424C File Offset: 0x0000244C
			public bool Wait()
			{
				return this.waitEvent.WaitOne(DispatchPool.DispatchThreadInfo.periodicWakeupDelay);
			}

			// Token: 0x04000056 RID: 86
			private static readonly TimeSpan periodicWakeupDelay = TimeSpan.FromMinutes(1.0);

			// Token: 0x04000057 RID: 87
			public static readonly TimeSpan InactiveThreadShutdownDelay = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000058 RID: 88
			private readonly AutoResetEvent waitEvent = new AutoResetEvent(false);

			// Token: 0x04000059 RID: 89
			private DispatchPool.DispatchThreadState state;

			// Token: 0x0400005A RID: 90
			private DateTime inactiveTime;
		}
	}
}
