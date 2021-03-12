using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x02000050 RID: 80
	internal sealed class AsyncTaskSequence : AsyncTaskWithChildTasks
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000030E9 File Offset: 0x000012E9
		internal AsyncTaskSequence(IList<AsyncTask> tasks) : base(tasks)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000030F2 File Offset: 0x000012F2
		public override string ToString()
		{
			return "AsyncTaskSequence for " + base.Tasks.Count + " tasks";
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003113 File Offset: 0x00001313
		internal void Cancel(EventWaitHandle notifyHandle)
		{
			base.Cancel();
			this.notifyCancelled = notifyHandle;
			if (notifyHandle != null && !base.Running && Interlocked.CompareExchange<EventWaitHandle>(ref this.notifyCancelled, null, notifyHandle) == notifyHandle)
			{
				notifyHandle.Set();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003144 File Offset: 0x00001344
		internal override void InternalExecute()
		{
			base.InternalExecute();
			this.currentTask = -1;
			this.ExecuteNextTask();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003159 File Offset: 0x00001359
		protected override void CompleteChildTask(AsyncTask childTask)
		{
			base.CompleteChildTask(childTask);
			if (childTask.Exception != null)
			{
				this.InternalComplete();
				return;
			}
			this.ExecuteNextTask();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003178 File Offset: 0x00001378
		private void ExecuteNextTask()
		{
			int num = Interlocked.Increment(ref this.currentTask);
			if (num < base.Tasks.Count)
			{
				if (!base.Cancelled)
				{
					AsyncTask asyncTask = base.Tasks[num];
					asyncTask.Execute(new TaskCompleteCallback(this.CompleteChildTask));
					return;
				}
				for (int i = num; i < base.Tasks.Count; i++)
				{
					base.Tasks[i].Cancel();
				}
			}
			this.InternalComplete();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000031F8 File Offset: 0x000013F8
		private void InternalComplete()
		{
			try
			{
				base.Complete();
			}
			finally
			{
				EventWaitHandle eventWaitHandle = this.notifyCancelled;
				if (eventWaitHandle != null)
				{
					ExAssert.RetailAssert(base.Cancelled, "Task must be marked cancelled");
					if (Interlocked.CompareExchange<EventWaitHandle>(ref this.notifyCancelled, null, eventWaitHandle) == eventWaitHandle)
					{
						eventWaitHandle.Set();
					}
				}
			}
		}

		// Token: 0x04000099 RID: 153
		private int currentTask;

		// Token: 0x0400009A RID: 154
		private EventWaitHandle notifyCancelled;
	}
}
