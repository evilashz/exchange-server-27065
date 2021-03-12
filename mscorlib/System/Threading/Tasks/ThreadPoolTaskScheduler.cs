using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200054D RID: 1357
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x06004104 RID: 16644 RVA: 0x000F1B64 File Offset: 0x000EFD64
		internal ThreadPoolTaskScheduler()
		{
			int id = base.Id;
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x000F1B80 File Offset: 0x000EFD80
		private static void LongRunningThreadWork(object obj)
		{
			Task task = obj as Task;
			task.ExecuteEntry(false);
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x000F1B9C File Offset: 0x000EFD9C
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork)
				{
					IsBackground = true
				}.Start(task);
				return;
			}
			bool forceGlobal = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, forceGlobal);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000F1BE0 File Offset: 0x000EFDE0
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool result = false;
			try
			{
				result = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return result;
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000F1C24 File Offset: 0x000EFE24
		[SecurityCritical]
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000F1C2C File Offset: 0x000EFE2C
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000F1C39 File Offset: 0x000EFE39
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x000F1C49 File Offset: 0x000EFE49
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x000F1C50 File Offset: 0x000EFE50
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001ABC RID: 6844
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = new ParameterizedThreadStart(ThreadPoolTaskScheduler.LongRunningThreadWork);
	}
}
