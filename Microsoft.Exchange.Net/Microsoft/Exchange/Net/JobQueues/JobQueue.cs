using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.JobQueues
{
	// Token: 0x0200073C RID: 1852
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class JobQueue
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x0004A38A File Offset: 0x0004858A
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x0004A392 File Offset: 0x00048592
		public ManualResetEvent ShutdownEvent { get; private set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x0004A39B File Offset: 0x0004859B
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x0004A3A3 File Offset: 0x000485A3
		public QueueType Type { get; private set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x0004A3AC File Offset: 0x000485AC
		public int PendingJobCount
		{
			get
			{
				return this.pendingJobCount;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0004A3B4 File Offset: 0x000485B4
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0004A3BC File Offset: 0x000485BC
		public bool IsDipatcherActive { get; private set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x0004A3C8 File Offset: 0x000485C8
		public int Length
		{
			get
			{
				int count;
				lock (this.syncObject)
				{
					count = this.queue.Count;
				}
				return count;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0004A410 File Offset: 0x00048610
		public Configuration Configuration
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0004A418 File Offset: 0x00048618
		public bool IsShuttingdown
		{
			get
			{
				return this.shuttingdown;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0004A424 File Offset: 0x00048624
		public void SignalShutdown()
		{
			lock (this.syncObject)
			{
				this.shuttingdown = true;
				if (this.dispatchTimer != null)
				{
					this.dispatchTimer.Dispose();
					this.dispatchTimer = null;
				}
			}
			this.SignalShutdownEventIfNecessary();
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0004A488 File Offset: 0x00048688
		public virtual void Cleanup()
		{
		}

		// Token: 0x060023FD RID: 9213
		protected abstract bool TryCreateJob(byte[] data, out Job job, out EnqueueResult result);

		// Token: 0x060023FE RID: 9214 RVA: 0x0004A48A File Offset: 0x0004868A
		private void SignalShutdownEventIfNecessary()
		{
			if (this.shuttingdown && this.pendingJobCount == 0)
			{
				this.ShutdownEvent.Set();
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0004A4EC File Offset: 0x000486EC
		public EnqueueResult Enqueue(byte[] data)
		{
			bool flag = false;
			EnqueueResult result2;
			try
			{
				object obj;
				Monitor.Enter(obj = this.syncObject, ref flag);
				if (this.shuttingdown)
				{
					result2 = new EnqueueResult(EnqueueResultType.QueueServerShutDown);
				}
				else if (this.queue.Count + 1 > this.config.MaxAllowedQueueLength)
				{
					result2 = new EnqueueResult(EnqueueResultType.QueueLengthLimitReached);
				}
				else
				{
					Job job = null;
					EnqueueResult result = EnqueueResult.Success;
					bool jobCreationSuccessful = false;
					try
					{
						GrayException.MapAndReportGrayExceptions(delegate()
						{
							jobCreationSuccessful = this.TryCreateJob(data, out job, out result);
						});
					}
					catch (GrayException ex)
					{
						return new EnqueueResult(EnqueueResultType.UnknownError, ex.ToString());
					}
					if (!jobCreationSuccessful)
					{
						result2 = result;
					}
					else
					{
						this.queue.Enqueue(job);
						if (!this.IsDipatcherActive)
						{
							this.WakeUpDispatcher();
						}
						result2 = result;
					}
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
			return result2;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0004A618 File Offset: 0x00048818
		protected JobQueue(QueueType type, Configuration config)
		{
			this.Type = type;
			this.config = config;
			this.ShutdownEvent = new ManualResetEvent(false);
			this.dispatchTimer = new Timer(new TimerCallback(this.OnDispatch), null, TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0004A68F File Offset: 0x0004888F
		public virtual void OnJobCompletion(Job job)
		{
			Interlocked.Decrement(ref this.pendingJobCount);
			this.SignalShutdownEventIfNecessary();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0004A6A4 File Offset: 0x000488A4
		private void OnDispatch(object state)
		{
			lock (this.syncObject)
			{
				if (!this.shuttingdown)
				{
					int num = this.config.MaxAllowedPendingJobCount - this.PendingJobCount;
					int num2 = 0;
					while (num2 < num && this.queue.Count != 0)
					{
						Job job = this.queue.Dequeue();
						if (ThreadPool.QueueUserWorkItem(new WaitCallback(job.Begin), null))
						{
							Interlocked.Increment(ref this.pendingJobCount);
						}
						else
						{
							this.queue.Enqueue(job);
						}
						num2++;
					}
					if (this.queue.Count == 0)
					{
						this.DormantDispatcher();
					}
					else
					{
						this.WakeUpDispatcher();
					}
				}
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0004A774 File Offset: 0x00048974
		private void WakeUpDispatcher()
		{
			this.IsDipatcherActive = true;
			this.dispatchTimer.Change(this.config.DispatcherWakeUpInterval, TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0004A7A2 File Offset: 0x000489A2
		private void DormantDispatcher()
		{
			this.dispatchTimer.Change(-1, -1);
			this.IsDipatcherActive = false;
		}

		// Token: 0x040021C1 RID: 8641
		protected readonly object syncObject = new object();

		// Token: 0x040021C2 RID: 8642
		protected readonly Configuration config;

		// Token: 0x040021C3 RID: 8643
		private readonly Queue<Job> queue = new Queue<Job>();

		// Token: 0x040021C4 RID: 8644
		private Timer dispatchTimer;

		// Token: 0x040021C5 RID: 8645
		private int pendingJobCount;

		// Token: 0x040021C6 RID: 8646
		protected volatile bool shuttingdown;
	}
}
