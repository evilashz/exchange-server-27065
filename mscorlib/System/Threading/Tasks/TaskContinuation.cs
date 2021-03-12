using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000541 RID: 1345
	internal abstract class TaskContinuation
	{
		// Token: 0x0600404E RID: 16462
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x0600404F RID: 16463 RVA: 0x000EFAC8 File Offset: 0x000EDCC8
		[SecuritySafeCritical]
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.InternalQueueTask(task);
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException) || (task.m_stateFlags & 134217728) == 0)
				{
					TaskSchedulerException exceptionObject = new TaskSchedulerException(ex);
					task.AddException(exceptionObject);
					task.Finish(false);
				}
			}
		}

		// Token: 0x06004050 RID: 16464
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();
	}
}
