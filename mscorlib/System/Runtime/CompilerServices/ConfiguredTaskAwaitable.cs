using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008CB RID: 2251
	[__DynamicallyInvokable]
	public struct ConfiguredTaskAwaitable
	{
		// Token: 0x06005D2E RID: 23854 RVA: 0x0014698A File Offset: 0x00144B8A
		internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x00146999 File Offset: 0x00144B99
		[__DynamicallyInvokable]
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x0400299C RID: 10652
		private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x02000C5F RID: 3167
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06006FD3 RID: 28627 RVA: 0x001804E2 File Offset: 0x0017E6E2
			internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17001345 RID: 4933
			// (get) Token: 0x06006FD4 RID: 28628 RVA: 0x001804F2 File Offset: 0x0017E6F2
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x06006FD5 RID: 28629 RVA: 0x001804FF File Offset: 0x0017E6FF
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x06006FD6 RID: 28630 RVA: 0x00180514 File Offset: 0x0017E714
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x06006FD7 RID: 28631 RVA: 0x00180529 File Offset: 0x0017E729
			[__DynamicallyInvokable]
			public void GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
			}

			// Token: 0x04003768 RID: 14184
			private readonly Task m_task;

			// Token: 0x04003769 RID: 14185
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
