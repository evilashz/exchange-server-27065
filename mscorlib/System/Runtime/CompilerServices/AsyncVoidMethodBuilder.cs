using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C1 RID: 2241
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncVoidMethodBuilder
	{
		// Token: 0x06005CEE RID: 23790 RVA: 0x00145AC8 File Offset: 0x00143CC8
		[__DynamicallyInvokable]
		public static AsyncVoidMethodBuilder Create()
		{
			SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
			if (currentNoFlow != null)
			{
				currentNoFlow.OperationStarted();
			}
			return new AsyncVoidMethodBuilder
			{
				m_synchronizationContext = currentNoFlow
			};
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x00145AF8 File Offset: 0x00143CF8
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

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00145B58 File Offset: 0x00143D58
		[__DynamicallyInvokable]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x00145B68 File Offset: 0x00143D68
		[__DynamicallyInvokable]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner runner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref runner);
				if (this.m_coreState.m_stateMachine == null)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
					}
					this.m_coreState.PostBoxInitialization(stateMachine, runner, null);
				}
				awaiter.OnCompleted(completionAction);
			}
			catch (Exception exception)
			{
				AsyncMethodBuilderCore.ThrowAsync(exception, null);
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x00145C18 File Offset: 0x00143E18
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
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
					}
					this.m_coreState.PostBoxInitialization(stateMachine, runner, null);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception exception)
			{
				AsyncMethodBuilderCore.ThrowAsync(exception, null);
			}
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x00145CC8 File Offset: 0x00143EC8
		[__DynamicallyInvokable]
		public void SetResult()
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Completed);
			}
			if (this.m_synchronizationContext != null)
			{
				this.NotifySynchronizationContextOfCompletion();
			}
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00145CF4 File Offset: 0x00143EF4
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Error);
			}
			if (this.m_synchronizationContext != null)
			{
				try
				{
					AsyncMethodBuilderCore.ThrowAsync(exception, this.m_synchronizationContext);
					return;
				}
				finally
				{
					this.NotifySynchronizationContextOfCompletion();
				}
			}
			AsyncMethodBuilderCore.ThrowAsync(exception, null);
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x00145D5C File Offset: 0x00143F5C
		private void NotifySynchronizationContextOfCompletion()
		{
			try
			{
				this.m_synchronizationContext.OperationCompleted();
			}
			catch (Exception exception)
			{
				AsyncMethodBuilderCore.ThrowAsync(exception, null);
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x00145D90 File Offset: 0x00143F90
		private Task Task
		{
			get
			{
				if (this.m_task == null)
				{
					this.m_task = new Task();
				}
				return this.m_task;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x00145DAB File Offset: 0x00143FAB
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x0400298B RID: 10635
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x0400298C RID: 10636
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x0400298D RID: 10637
		private Task m_task;
	}
}
