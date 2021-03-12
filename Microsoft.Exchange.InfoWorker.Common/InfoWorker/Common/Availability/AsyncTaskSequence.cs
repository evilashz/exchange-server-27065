using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000058 RID: 88
	internal sealed class AsyncTaskSequence : AsyncTaskWithChildTasks
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00009AE8 File Offset: 0x00007CE8
		public AsyncTaskSequence(IList<AsyncTask> tasks) : base(tasks)
		{
			this.currentTask = -1;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009AF8 File Offset: 0x00007CF8
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.BeginInvokeNextTask();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009B08 File Offset: 0x00007D08
		private void BeginInvokeNextTask()
		{
			this.currentTask++;
			if (this.currentTask < base.Tasks.Count && !base.Aborted)
			{
				AsyncTask asyncTask = base.Tasks[this.currentTask];
				asyncTask.BeginInvoke(new TaskCompleteCallback(this.CompleteChildTask));
				return;
			}
			base.Complete();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009B6A File Offset: 0x00007D6A
		protected override void CompleteChildTask(AsyncTask childTask)
		{
			base.CompleteChildTask(childTask);
			this.BeginInvokeNextTask();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009B79 File Offset: 0x00007D79
		public override string ToString()
		{
			return "AsyncTaskSequence for " + base.Tasks.Count + " tasks";
		}

		// Token: 0x04000144 RID: 324
		private int currentTask;
	}
}
