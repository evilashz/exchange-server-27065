using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C3 RID: 2243
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder<TResult>
	{
		// Token: 0x06005D03 RID: 23811 RVA: 0x00145EA8 File Offset: 0x001440A8
		[__DynamicallyInvokable]
		public static AsyncTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncTaskMethodBuilder<TResult>);
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x00145EC0 File Offset: 0x001440C0
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[__DynamicallyInvokable]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.EstablishCopyOnWriteScope(ref executionContextSwitcher);
				stateMachine.MoveNext();
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x00145F20 File Offset: 0x00144120
		[__DynamicallyInvokable]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x00145F30 File Offset: 0x00144130
		[__DynamicallyInvokable]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner runner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref runner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, runner, task);
				}
				awaiter.OnCompleted(completionAction);
			}
			catch (Exception exception)
			{
				AsyncMethodBuilderCore.ThrowAsync(exception, null);
			}
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x00145FB4 File Offset: 0x001441B4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner runner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref runner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, runner, task);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception exception)
			{
				AsyncMethodBuilderCore.ThrowAsync(exception, null);
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005D08 RID: 23816 RVA: 0x00146038 File Offset: 0x00144238
		[__DynamicallyInvokable]
		public Task<TResult> Task
		{
			[__DynamicallyInvokable]
			get
			{
				Task<TResult> task = this.m_task;
				if (task == null)
				{
					task = (this.m_task = new Task<TResult>());
				}
				return task;
			}
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x00146060 File Offset: 0x00144260
		[__DynamicallyInvokable]
		public void SetResult(TResult result)
		{
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				this.m_task = this.GetTaskForResult(result);
				return;
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, task.Id, AsyncCausalityStatus.Completed);
			}
			if (System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
			{
				System.Threading.Tasks.Task.RemoveFromActiveTasks(task.Id);
			}
			if (!task.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x001460C4 File Offset: 0x001442C4
		internal void SetResult(Task<TResult> completedTask)
		{
			if (this.m_task == null)
			{
				this.m_task = completedTask;
				return;
			}
			this.SetResult(default(TResult));
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x001460F4 File Offset: 0x001442F4
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				task = this.Task;
			}
			OperationCanceledException ex = exception as OperationCanceledException;
			if (!((ex != null) ? task.TrySetCanceled(ex.CancellationToken, ex) : task.TrySetException(exception)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x00146154 File Offset: 0x00144354
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.Task.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005D0D RID: 23821 RVA: 0x00146162 File Offset: 0x00144362
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x0014616C File Offset: 0x0014436C
		[SecuritySafeCritical]
		private Task<TResult> GetTaskForResult(TResult result)
		{
			if (default(TResult) != null)
			{
				if (typeof(TResult) == typeof(bool))
				{
					Task<bool> o = ((bool)((object)result)) ? AsyncTaskCache.TrueTask : AsyncTaskCache.FalseTask;
					return JitHelpers.UnsafeCast<Task<TResult>>(o);
				}
				if (typeof(TResult) == typeof(int))
				{
					int num = (int)((object)result);
					if (num < 9 && num >= -1)
					{
						Task<int> o2 = AsyncTaskCache.Int32Tasks[num - -1];
						return JitHelpers.UnsafeCast<Task<TResult>>(o2);
					}
				}
				else if ((typeof(TResult) == typeof(uint) && (uint)((object)result) == 0U) || (typeof(TResult) == typeof(byte) && (byte)((object)result) == 0) || (typeof(TResult) == typeof(sbyte) && (sbyte)((object)result) == 0) || (typeof(TResult) == typeof(char) && (char)((object)result) == '\0') || (typeof(TResult) == typeof(decimal) && 0m == (decimal)((object)result)) || (typeof(TResult) == typeof(long) && (long)((object)result) == 0L) || (typeof(TResult) == typeof(ulong) && (ulong)((object)result) == 0UL) || (typeof(TResult) == typeof(short) && (short)((object)result) == 0) || (typeof(TResult) == typeof(ushort) && (ushort)((object)result) == 0) || (typeof(TResult) == typeof(IntPtr) && (IntPtr)0 == (IntPtr)((object)result)) || (typeof(TResult) == typeof(UIntPtr) && (UIntPtr)0 == (UIntPtr)((object)result)))
				{
					return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
				}
			}
			else if (result == null)
			{
				return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
			}
			return new Task<TResult>(result);
		}

		// Token: 0x04002990 RID: 10640
		internal static readonly Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default(TResult));

		// Token: 0x04002991 RID: 10641
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x04002992 RID: 10642
		private Task<TResult> m_task;
	}
}
