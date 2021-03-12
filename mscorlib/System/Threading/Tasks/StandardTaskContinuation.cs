using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000542 RID: 1346
	internal class StandardTaskContinuation : TaskContinuation
	{
		// Token: 0x06004052 RID: 16466 RVA: 0x000EFB60 File Offset: 0x000EDD60
		internal StandardTaskContinuation(Task task, TaskContinuationOptions options, TaskScheduler scheduler)
		{
			this.m_task = task;
			this.m_options = options;
			this.m_taskScheduler = scheduler;
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.m_task.Id, "Task.ContinueWith: " + ((Delegate)task.m_action).Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this.m_task);
			}
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000EFBD4 File Offset: 0x000EDDD4
		internal override void Run(Task completedTask, bool bCanInlineContinuationTask)
		{
			TaskContinuationOptions options = this.m_options;
			bool flag = completedTask.IsRanToCompletion ? ((options & TaskContinuationOptions.NotOnRanToCompletion) == TaskContinuationOptions.None) : (completedTask.IsCanceled ? ((options & TaskContinuationOptions.NotOnCanceled) == TaskContinuationOptions.None) : ((options & TaskContinuationOptions.NotOnFaulted) == TaskContinuationOptions.None));
			Task task = this.m_task;
			if (flag)
			{
				if (!task.IsCanceled && AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, task.Id, CausalityRelation.AssignDelegate);
				}
				task.m_taskScheduler = this.m_taskScheduler;
				if (bCanInlineContinuationTask && (options & TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)
				{
					TaskContinuation.InlineIfPossibleOrElseQueue(task, true);
					return;
				}
				try
				{
					task.ScheduleAndStart(true);
					return;
				}
				catch (TaskSchedulerException)
				{
					return;
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x000EFC88 File Offset: 0x000EDE88
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_task.m_action == null)
			{
				return this.m_task.GetDelegateContinuationsForDebugger();
			}
			return new Delegate[]
			{
				this.m_task.m_action as Delegate
			};
		}

		// Token: 0x04001A9B RID: 6811
		internal readonly Task m_task;

		// Token: 0x04001A9C RID: 6812
		internal readonly TaskContinuationOptions m_options;

		// Token: 0x04001A9D RID: 6813
		private readonly TaskScheduler m_taskScheduler;
	}
}
