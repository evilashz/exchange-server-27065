using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000545 RID: 1349
	internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
	{
		// Token: 0x0600405D RID: 16477 RVA: 0x000EFEDC File Offset: 0x000EE0DC
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000EFEFB File Offset: 0x000EE0FB
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.FastCapture();
			}
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000EFF18 File Offset: 0x000EE118
		protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
		{
			return new Task(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler)
			{
				CapturedContext = this.m_capturedContext
			};
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000EFF4C File Offset: 0x000EE14C
		[SecuritySafeCritical]
		internal override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			ThreadPool.UnsafeQueueCustomWorkItem(this, false);
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x000EFFC0 File Offset: 0x000EE1C0
		internal static bool IsValidLocationForInlining
		{
			get
			{
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					return false;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent == null || internalCurrent == TaskScheduler.Default;
			}
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000F0004 File Offset: 0x000EE204
		[SecurityCritical]
		private void ExecuteWorkItemHelper()
		{
			TplEtwProvider log = TplEtwProvider.Log;
			Guid empty = Guid.Empty;
			if (log.TasksSetActivityIds && this.m_continuationId != 0)
			{
				Guid activityId = TplEtwProvider.CreateGuidForTaskID(this.m_continuationId);
				EventSource.SetCurrentThreadActivityId(activityId, out empty);
			}
			try
			{
				if (this.m_capturedContext == null)
				{
					this.m_action();
				}
				else
				{
					try
					{
						ExecutionContext.Run(this.m_capturedContext, AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, true);
					}
					finally
					{
						this.m_capturedContext.Dispose();
					}
				}
			}
			finally
			{
				if (log.TasksSetActivityIds && this.m_continuationId != 0)
				{
					EventSource.SetCurrentThreadActivityId(empty);
				}
			}
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000F00B0 File Offset: 0x000EE2B0
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.m_capturedContext == null && !TplEtwProvider.Log.IsEnabled())
			{
				this.m_action();
				return;
			}
			this.ExecuteWorkItemHelper();
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000F00D8 File Offset: 0x000EE2D8
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000F00DA File Offset: 0x000EE2DA
		[SecurityCritical]
		private static void InvokeAction(object state)
		{
			((Action)state)();
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000F00E8 File Offset: 0x000EE2E8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static ContextCallback GetInvokeActionCallback()
		{
			ContextCallback contextCallback = AwaitTaskContinuation.s_invokeActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (AwaitTaskContinuation.s_invokeActionCallback = new ContextCallback(AwaitTaskContinuation.InvokeAction));
			}
			return contextCallback;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000F0114 File Offset: 0x000EE314
		[SecurityCritical]
		protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
		{
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				if (this.m_capturedContext == null)
				{
					callback(state);
				}
				else
				{
					ExecutionContext.Run(this.m_capturedContext, callback, state, true);
				}
			}
			catch (Exception exc)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(exc);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
				if (this.m_capturedContext != null)
				{
					this.m_capturedContext.Dispose();
				}
			}
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000F018C File Offset: 0x000EE38C
		[SecurityCritical]
		internal static void RunOrScheduleAction(Action action, bool allowInlining, ref Task currentTask)
		{
			if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
			{
				AwaitTaskContinuation.UnsafeScheduleAction(action, currentTask);
				return;
			}
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				action();
			}
			catch (Exception exc)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(exc);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
			}
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x000F01EC File Offset: 0x000EE3EC
		[SecurityCritical]
		internal static void UnsafeScheduleAction(Action action, Task task)
		{
			AwaitTaskContinuation awaitTaskContinuation = new AwaitTaskContinuation(action, false);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled() && task != null)
			{
				awaitTaskContinuation.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, awaitTaskContinuation.m_continuationId);
			}
			ThreadPool.UnsafeQueueCustomWorkItem(awaitTaskContinuation, false);
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x000F024C File Offset: 0x000EE44C
		protected static void ThrowAsyncIfNecessary(Exception exc)
		{
			if (!(exc is ThreadAbortException) && !(exc is AppDomainUnloadedException) && !WindowsRuntimeMarshal.ReportUnhandledError(exc))
			{
				ExceptionDispatchInfo state = ExceptionDispatchInfo.Capture(exc);
				ThreadPool.QueueUserWorkItem(delegate(object s)
				{
					((ExceptionDispatchInfo)s).Throw();
				}, state);
			}
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x000F029E File Offset: 0x000EE49E
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			return new Delegate[]
			{
				AsyncMethodBuilderCore.TryGetStateMachineForDebugger(this.m_action)
			};
		}

		// Token: 0x04001AA2 RID: 6818
		private readonly ExecutionContext m_capturedContext;

		// Token: 0x04001AA3 RID: 6819
		protected readonly Action m_action;

		// Token: 0x04001AA4 RID: 6820
		protected int m_continuationId;

		// Token: 0x04001AA5 RID: 6821
		[SecurityCritical]
		private static ContextCallback s_invokeActionCallback;
	}
}
