using System;
using System.Threading;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000005 RID: 5
	internal abstract class Job
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000034FC File Offset: 0x000016FC
		public Job(TimeSpan interval)
		{
			this.interval = interval;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000350B File Offset: 0x0000170B
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00003513 File Offset: 0x00001713
		public bool StartNow
		{
			get
			{
				return this.startNow;
			}
			set
			{
				this.startNow = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000351C File Offset: 0x0000171C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00003524 File Offset: 0x00001724
		public TimeSpan Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				this.interval = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000352D File Offset: 0x0000172D
		public bool HasShutdown
		{
			get
			{
				return this.hasShutdown;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003538 File Offset: 0x00001738
		public virtual void BeginExecute()
		{
			lock (this)
			{
				if (!this.shuttingDown)
				{
					if (this.timer == null)
					{
						this.timer = new Timer(new TimerCallback(this.TimerCallback), null, 0, -1);
					}
					else if (this.startNow)
					{
						this.timer.Change(0, -1);
					}
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000035B4 File Offset: 0x000017B4
		public virtual void InitiateShutdown()
		{
			lock (this)
			{
				this.shuttingDown = true;
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				if (this.running == 0)
				{
					this.hasShutdown = true;
				}
			}
		}

		// Token: 0x06000033 RID: 51
		protected abstract JobBackgroundResult BackgroundExecute();

		// Token: 0x06000034 RID: 52 RVA: 0x00003620 File Offset: 0x00001820
		private void TimerCallback(object state)
		{
			lock (this)
			{
				if (!this.shuttingDown)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.BackgroundExecuteWrapper));
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003674 File Offset: 0x00001874
		private void BackgroundExecuteWrapper(object ignoredArgument)
		{
			Thread.CurrentThread.Priority = ThreadPriority.Lowest;
			JobBackgroundResult jobBackgroundResult = JobBackgroundResult.Completed;
			try
			{
				if (Interlocked.Increment(ref this.running) < 2 && !this.shuttingDown)
				{
					DateTime.UtcNow + this.interval;
					jobBackgroundResult = this.BackgroundExecute();
				}
			}
			finally
			{
				if (Interlocked.Decrement(ref this.running) == 0)
				{
					GC.Collect();
					lock (this)
					{
						if (this.timer != null)
						{
							this.timer.Change((int)this.interval.TotalMilliseconds, -1);
						}
						else if (jobBackgroundResult == JobBackgroundResult.ReSchedule && this.shuttingDown)
						{
							this.hasShutdown = true;
						}
					}
				}
			}
		}

		// Token: 0x04000018 RID: 24
		protected volatile bool shuttingDown;

		// Token: 0x04000019 RID: 25
		private volatile bool hasShutdown;

		// Token: 0x0400001A RID: 26
		private TimeSpan interval;

		// Token: 0x0400001B RID: 27
		private int running;

		// Token: 0x0400001C RID: 28
		protected bool startNow;

		// Token: 0x0400001D RID: 29
		private Timer timer;
	}
}
