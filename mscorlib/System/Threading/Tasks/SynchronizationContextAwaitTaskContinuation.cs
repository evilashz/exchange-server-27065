using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000543 RID: 1347
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06004055 RID: 16469 RVA: 0x000EFCBC File Offset: 0x000EDEBC
		[SecurityCritical]
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark) : base(action, flowExecutionContext, ref stackMark)
		{
			this.m_syncContext = context;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000EFCD0 File Offset: 0x000EDED0
		[SecuritySafeCritical]
		internal sealed override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.CurrentNoFlow)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x000EFD54 File Offset: 0x000EDF54
		[SecurityCritical]
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.TasksSetActivityIds && synchronizationContextAwaitTaskContinuation.m_continuationId != 0)
			{
				synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, SynchronizationContextAwaitTaskContinuation.GetActionLogDelegate(synchronizationContextAwaitTaskContinuation.m_continuationId, synchronizationContextAwaitTaskContinuation.m_action));
				return;
			}
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000EFDB8 File Offset: 0x000EDFB8
		private static Action GetActionLogDelegate(int continuationId, Action action)
		{
			return delegate()
			{
				Guid activityId = TplEtwProvider.CreateGuidForTaskID(continuationId);
				Guid currentThreadActivityId;
				EventSource.SetCurrentThreadActivityId(activityId, out currentThreadActivityId);
				try
				{
					action();
				}
				finally
				{
					EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
				}
			};
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000EFDE8 File Offset: 0x000EDFE8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x04001A9E RID: 6814
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001A9F RID: 6815
		[SecurityCritical]
		private static ContextCallback s_postActionCallback;

		// Token: 0x04001AA0 RID: 6816
		private readonly SynchronizationContext m_syncContext;
	}
}
