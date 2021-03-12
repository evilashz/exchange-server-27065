using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000041 RID: 65
	public class RecurringTask<T> : Task<T>
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x0000CA62 File Offset: 0x0000AC62
		public RecurringTask(Task<T>.TaskCallback callback, T context, TimeSpan interval) : this(callback, context, Task.NoDelay, interval, false)
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000CA73 File Offset: 0x0000AC73
		public RecurringTask(Task<T>.TaskCallback callback, T context, TimeSpan interval, bool autoStart) : this(callback, context, Task.NoDelay, interval, autoStart)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public RecurringTask(Task<T>.TaskCallback callback, T context, TimeSpan initialDelay, TimeSpan interval, bool autoStart) : base(callback, context, ThreadPriority.Normal, 0, TaskFlags.UseThreadPoolThread)
		{
			bool flag = false;
			try
			{
				this.timer = new Timer(delegate(object unused)
				{
					base.Worker();
				}, null, -1, -1);
				this.initialDelay = initialDelay;
				this.interval = interval;
				if (autoStart)
				{
					this.Start();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000CB04 File Offset: 0x0000AD04
		protected TimeSpan InitialDelay
		{
			set
			{
				this.initialDelay = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000CB0D File Offset: 0x0000AD0D
		protected TimeSpan Interval
		{
			get
			{
				return this.interval;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public override void Start()
		{
			base.CheckDisposed();
			using (LockManager.Lock(base.StateLock))
			{
				Task<T>.TaskState state = base.State;
				if (state == Task<T>.TaskState.Ready || state == Task<T>.TaskState.Complete)
				{
					base.State = Task<T>.TaskState.Starting;
					this.timer.Change(this.initialDelay, this.interval);
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public override void Stop()
		{
			base.CheckDisposed();
			using (LockManager.Lock(base.StateLock))
			{
				if (this.timer != null)
				{
					this.timer.Change(-1, -1);
				}
				if (base.State == Task<T>.TaskState.Starting)
				{
					base.State = Task<T>.TaskState.Ready;
				}
				if (base.State == Task<T>.TaskState.Running)
				{
					base.State = Task<T>.TaskState.StopRequested;
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		protected override void Invoke()
		{
			using (LockManager.Lock(base.StateLock))
			{
				if (base.State == Task<T>.TaskState.Complete)
				{
					base.State = Task<T>.TaskState.Starting;
				}
				if (base.State == Task<T>.TaskState.StopRequested)
				{
					base.State = Task<T>.TaskState.Complete;
					return;
				}
			}
			base.Invoke();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000CC60 File Offset: 0x0000AE60
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RecurringTask<T>>(this);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000CC68 File Offset: 0x0000AE68
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Stop();
				base.WaitForCompletion();
				if (this.timer != null)
				{
					using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
					{
						if (this.timer.Dispose(manualResetEvent))
						{
							manualResetEvent.WaitOne();
						}
					}
					this.timer = null;
				}
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x040004D9 RID: 1241
		protected static readonly TimeSpan RunOnce = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x040004DA RID: 1242
		private Timer timer;

		// Token: 0x040004DB RID: 1243
		private TimeSpan initialDelay;

		// Token: 0x040004DC RID: 1244
		private TimeSpan interval;
	}
}
