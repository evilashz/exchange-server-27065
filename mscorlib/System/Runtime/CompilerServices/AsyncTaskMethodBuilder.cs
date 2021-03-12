using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C2 RID: 2242
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder
	{
		// Token: 0x06005CF8 RID: 23800 RVA: 0x00145DB4 File Offset: 0x00143FB4
		[__DynamicallyInvokable]
		public static AsyncTaskMethodBuilder Create()
		{
			return default(AsyncTaskMethodBuilder);
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x00145DCC File Offset: 0x00143FCC
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

		// Token: 0x06005CFA RID: 23802 RVA: 0x00145E2C File Offset: 0x0014402C
		[__DynamicallyInvokable]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_builder.SetStateMachine(stateMachine);
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x00145E3A File Offset: 0x0014403A
		[__DynamicallyInvokable]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x00145E49 File Offset: 0x00144049
		[__DynamicallyInvokable]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06005CFD RID: 23805 RVA: 0x00145E58 File Offset: 0x00144058
		[__DynamicallyInvokable]
		public Task Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_builder.Task;
			}
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x00145E65 File Offset: 0x00144065
		[__DynamicallyInvokable]
		public void SetResult()
		{
			this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x00145E77 File Offset: 0x00144077
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			this.m_builder.SetException(exception);
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x00145E85 File Offset: 0x00144085
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.m_builder.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005D01 RID: 23809 RVA: 0x00145E93 File Offset: 0x00144093
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x0400298E RID: 10638
		private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;

		// Token: 0x0400298F RID: 10639
		private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;
	}
}
