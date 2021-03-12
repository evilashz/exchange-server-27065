using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000051 RID: 81
	internal abstract class AsyncTask
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00009782 File Offset: 0x00007982
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000978A File Offset: 0x0000798A
		public bool Aborted { get; private set; }

		// Token: 0x060001D0 RID: 464 RVA: 0x00009793 File Offset: 0x00007993
		public AsyncTask()
		{
			this.taskState = TaskState.NotStarted;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000097A2 File Offset: 0x000079A2
		public virtual void BeginInvoke(TaskCompleteCallback callback)
		{
			AsyncTask.Tracer.TraceDebug<object, AsyncTask>((long)this.GetHashCode(), "{0}: BeginInvoke: {1}", TraceContext.Get(), this);
			if (this.taskState != TaskState.NotStarted)
			{
				throw new InvalidOperationException();
			}
			this.taskState = TaskState.Running;
			this.callback = callback;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000097DC File Offset: 0x000079DC
		public virtual void Abort()
		{
			AsyncTask.Tracer.TraceDebug<object, AsyncTask>((long)this.GetHashCode(), "{0}: Abort: {1}", TraceContext.Get(), this);
			this.Aborted = true;
			if (this.done != null)
			{
				this.SetDone(null);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009810 File Offset: 0x00007A10
		protected void Complete()
		{
			AsyncTask.Tracer.TraceDebug<object, AsyncTask>((long)this.GetHashCode(), "{0}: Complete: {1}", TraceContext.Get(), this);
			try
			{
				this.callback(this);
			}
			finally
			{
				this.taskState = TaskState.Completed;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009860 File Offset: 0x00007A60
		internal bool Invoke(DateTime deadline)
		{
			this.done = new ManualResetEvent(false);
			bool result;
			try
			{
				this.BeginInvoke(new TaskCompleteCallback(this.SetDone));
				DateTime utcNow = DateTime.UtcNow;
				TimeSpan timeout = (deadline > utcNow) ? (deadline - utcNow) : TimeSpan.Zero;
				bool flag = this.done.WaitOne(timeout, false);
				if (!flag)
				{
					this.Abort();
				}
				result = flag;
			}
			finally
			{
				this.done.Close();
			}
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000098E4 File Offset: 0x00007AE4
		private void SetDone(AsyncTask task)
		{
			try
			{
				this.done.Set();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x0400012B RID: 299
		private static readonly Trace Tracer = ExTraceGlobals.RequestRoutingTracer;

		// Token: 0x0400012C RID: 300
		private TaskState taskState;

		// Token: 0x0400012D RID: 301
		private TaskCompleteCallback callback;

		// Token: 0x0400012E RID: 302
		private ManualResetEvent done;
	}
}
