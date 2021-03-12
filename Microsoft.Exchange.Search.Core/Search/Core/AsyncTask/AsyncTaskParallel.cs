using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x0200004F RID: 79
	internal sealed class AsyncTaskParallel : AsyncTaskWithChildTasks
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00003039 File Offset: 0x00001239
		internal AsyncTaskParallel(IList<AsyncTask> tasks) : base(tasks)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003042 File Offset: 0x00001242
		public override string ToString()
		{
			return "AsyncTaskParallel for " + base.Tasks.Count + " tasks";
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003064 File Offset: 0x00001264
		internal override void InternalExecute()
		{
			base.InternalExecute();
			this.pendingTasksCount = base.Tasks.Count;
			for (int i = 0; i < base.Tasks.Count; i++)
			{
				AsyncTask asyncTask = base.Tasks[i];
				asyncTask.Execute(new TaskCompleteCallback(this.CompleteChildTask));
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000030C0 File Offset: 0x000012C0
		protected override void CompleteChildTask(AsyncTask childTask)
		{
			base.CompleteChildTask(childTask);
			if (Interlocked.Decrement(ref this.pendingTasksCount) == 0)
			{
				base.Complete();
			}
		}

		// Token: 0x04000098 RID: 152
		private int pendingTasksCount;
	}
}
