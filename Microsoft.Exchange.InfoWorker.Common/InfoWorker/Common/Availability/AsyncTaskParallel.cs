using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000059 RID: 89
	internal sealed class AsyncTaskParallel : AsyncTaskWithChildTasks
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00009B9A File Offset: 0x00007D9A
		public AsyncTaskParallel(IList<AsyncTask> tasks) : base(tasks)
		{
			this.pendintTasksCount = tasks.Count;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009BB0 File Offset: 0x00007DB0
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			for (int i = 0; i < base.Tasks.Count; i++)
			{
				AsyncTask asyncTask = base.Tasks[i];
				asyncTask.BeginInvoke(new TaskCompleteCallback(this.CompleteChildTask));
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009BFC File Offset: 0x00007DFC
		protected override void CompleteChildTask(AsyncTask childTask)
		{
			base.CompleteChildTask(childTask);
			if (Interlocked.Decrement(ref this.pendintTasksCount) == 0)
			{
				base.Complete();
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009C25 File Offset: 0x00007E25
		public override string ToString()
		{
			return "AsyncTaskParallel for " + base.Tasks.Count + " tasks";
		}

		// Token: 0x04000145 RID: 325
		private int pendintTasksCount;
	}
}
