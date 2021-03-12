using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000057 RID: 87
	internal abstract class AsyncTaskWithChildTasks : AsyncTask
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009A10 File Offset: 0x00007C10
		protected IList<AsyncTask> Tasks
		{
			get
			{
				return this.tasks;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009A18 File Offset: 0x00007C18
		public AsyncTaskWithChildTasks(IList<AsyncTask> tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			this.tasks = tasks;
			this.isCompletedTask = new bool[tasks.Count];
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009A48 File Offset: 0x00007C48
		protected virtual void CompleteChildTask(AsyncTask childTask)
		{
			int num = this.tasks.IndexOf(childTask);
			if (num < 0 || num >= this.tasks.Count)
			{
				throw new InvalidOperationException();
			}
			if (this.isCompletedTask[num])
			{
				throw new InvalidOperationException();
			}
			this.isCompletedTask[num] = true;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009A94 File Offset: 0x00007C94
		public override void Abort()
		{
			base.Abort();
			foreach (AsyncTask asyncTask in this.tasks)
			{
				asyncTask.Abort();
			}
		}

		// Token: 0x04000142 RID: 322
		private IList<AsyncTask> tasks;

		// Token: 0x04000143 RID: 323
		private bool[] isCompletedTask;
	}
}
