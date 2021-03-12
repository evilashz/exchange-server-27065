using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x0200004E RID: 78
	internal abstract class AsyncTaskWithChildTasks : AsyncTask
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00002EBB File Offset: 0x000010BB
		internal AsyncTaskWithChildTasks(IList<AsyncTask> tasks)
		{
			Util.ThrowOnNullOrEmptyArgument<AsyncTask>(tasks, "tasks");
			this.tasks = tasks;
			this.isCompletedTask = new bool[tasks.Count];
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00002EE6 File Offset: 0x000010E6
		protected IList<AsyncTask> Tasks
		{
			get
			{
				return this.tasks;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00002EF0 File Offset: 0x000010F0
		internal override void InternalExecute()
		{
			for (int i = 0; i < this.isCompletedTask.Length; i++)
			{
				this.isCompletedTask[i] = false;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002F1C File Offset: 0x0000111C
		protected virtual void CompleteChildTask(AsyncTask childTask)
		{
			int num = this.Tasks.IndexOf(childTask);
			if (num < 0 || num >= this.Tasks.Count)
			{
				throw new InvalidOperationException("The completed task is not from any known child task.");
			}
			lock (this.isCompletedTask)
			{
				if (this.isCompletedTask[num])
				{
					throw new InvalidOperationException(string.Format("The task {0} has been completed before.", num));
				}
				this.isCompletedTask[num] = true;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00002FAC File Offset: 0x000011AC
		protected void Complete()
		{
			List<ComponentException> list = new List<ComponentException>();
			foreach (AsyncTask asyncTask in this.Tasks)
			{
				if (asyncTask.Exception != null)
				{
					list.Add(asyncTask.Exception);
				}
			}
			if (list.Count > 0)
			{
				this.Complete(new AggregateException(list.ToArray()));
				return;
			}
			this.Complete(null);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003030 File Offset: 0x00001230
		private new void Complete(ComponentException exception)
		{
			base.Complete(exception);
		}

		// Token: 0x04000096 RID: 150
		private readonly IList<AsyncTask> tasks;

		// Token: 0x04000097 RID: 151
		private bool[] isCompletedTask;
	}
}
