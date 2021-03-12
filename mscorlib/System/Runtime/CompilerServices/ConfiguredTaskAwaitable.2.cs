using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008CC RID: 2252
	[__DynamicallyInvokable]
	public struct ConfiguredTaskAwaitable<TResult>
	{
		// Token: 0x06005D30 RID: 23856 RVA: 0x001469A1 File Offset: 0x00144BA1
		internal ConfiguredTaskAwaitable(Task<TResult> task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x001469B0 File Offset: 0x00144BB0
		[__DynamicallyInvokable]
		public ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x0400299D RID: 10653
		private readonly ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x02000C60 RID: 3168
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06006FD8 RID: 28632 RVA: 0x00180536 File Offset: 0x0017E736
			internal ConfiguredTaskAwaiter(Task<TResult> task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17001346 RID: 4934
			// (get) Token: 0x06006FD9 RID: 28633 RVA: 0x00180546 File Offset: 0x0017E746
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x06006FDA RID: 28634 RVA: 0x00180553 File Offset: 0x0017E753
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x06006FDB RID: 28635 RVA: 0x00180568 File Offset: 0x0017E768
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x06006FDC RID: 28636 RVA: 0x0018057D File Offset: 0x0017E77D
			[__DynamicallyInvokable]
			public TResult GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
				return this.m_task.ResultOnSuccess;
			}

			// Token: 0x0400376A RID: 14186
			private readonly Task<TResult> m_task;

			// Token: 0x0400376B RID: 14187
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
