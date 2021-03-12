using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C9 RID: 2249
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06005D1F RID: 23839 RVA: 0x0014674B File Offset: 0x0014494B
		internal TaskAwaiter(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005D20 RID: 23840 RVA: 0x00146754 File Offset: 0x00144954
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x00146761 File Offset: 0x00144961
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00146771 File Offset: 0x00144971
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x00146781 File Offset: 0x00144981
		[__DynamicallyInvokable]
		public void GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x0014678E File Offset: 0x0014498E
		internal static void ValidateEnd(Task task)
		{
			if (task.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
			}
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x001467A0 File Offset: 0x001449A0
		private static void HandleNonSuccessAndDebuggerNotification(Task task)
		{
			if (!task.IsCompleted)
			{
				bool flag = task.InternalWait(-1, default(CancellationToken));
			}
			task.NotifyDebuggerOfWaitCompletionIfNecessary();
			if (!task.IsRanToCompletion)
			{
				TaskAwaiter.ThrowForNonSuccess(task);
			}
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x001467DC File Offset: 0x001449DC
		private static void ThrowForNonSuccess(Task task)
		{
			TaskStatus status = task.Status;
			if (status == TaskStatus.Canceled)
			{
				ExceptionDispatchInfo cancellationExceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
				if (cancellationExceptionDispatchInfo != null)
				{
					cancellationExceptionDispatchInfo.Throw();
				}
				throw new TaskCanceledException(task);
			}
			if (status != TaskStatus.Faulted)
			{
				return;
			}
			ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
			if (exceptionDispatchInfos.Count > 0)
			{
				exceptionDispatchInfos[0].Throw();
				return;
			}
			throw task.Exception;
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x00146834 File Offset: 0x00144A34
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (TplEtwProvider.Log.IsEnabled() || Task.s_asyncDebuggingEnabled)
			{
				continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
			}
			task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext, ref stackCrawlMark);
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x00146878 File Offset: 0x00144A78
		private static Action OutputWaitEtwEvents(Task task, Action continuation)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(task);
			}
			TplEtwProvider etwLog = TplEtwProvider.Log;
			if (etwLog.IsEnabled())
			{
				Task internalCurrent = Task.InternalCurrent;
				Task task2 = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				etwLog.TaskWaitBegin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, task.Id, TplEtwProvider.TaskWaitBehavior.Asynchronous, (task2 != null) ? task2.Id : 0, Thread.GetDomainID());
			}
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
			{
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(task.Id);
				}
				Guid currentThreadActivityId = default(Guid);
				bool flag = etwLog.IsEnabled();
				if (flag)
				{
					Task internalCurrent2 = Task.InternalCurrent;
					etwLog.TaskWaitEnd((internalCurrent2 != null) ? internalCurrent2.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent2 != null) ? internalCurrent2.Id : 0, task.Id);
					if (etwLog.TasksSetActivityIds && (task.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(task.Id), out currentThreadActivityId);
					}
				}
				continuation();
				if (flag)
				{
					etwLog.TaskWaitContinuationComplete(task.Id);
					if (etwLog.TasksSetActivityIds && (task.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
					}
				}
			}, null);
		}

		// Token: 0x0400299A RID: 10650
		private readonly Task m_task;
	}
}
