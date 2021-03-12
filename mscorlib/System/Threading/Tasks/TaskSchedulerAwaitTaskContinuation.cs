using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000544 RID: 1348
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x0600405B RID: 16475 RVA: 0x000EFE29 File Offset: 0x000EE029
		[SecurityCritical]
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark) : base(action, flowExecutionContext, ref stackMark)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x000EFE3C File Offset: 0x000EE03C
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || Thread.CurrentThread.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception exc)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(exc);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04001AA1 RID: 6817
		private readonly TaskScheduler m_scheduler;
	}
}
