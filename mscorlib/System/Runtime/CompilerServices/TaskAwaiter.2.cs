using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008CA RID: 2250
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06005D29 RID: 23849 RVA: 0x0014693C File Offset: 0x00144B3C
		internal TaskAwaiter(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005D2A RID: 23850 RVA: 0x00146945 File Offset: 0x00144B45
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x00146952 File Offset: 0x00144B52
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x00146962 File Offset: 0x00144B62
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x00146972 File Offset: 0x00144B72
		[__DynamicallyInvokable]
		public TResult GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
			return this.m_task.ResultOnSuccess;
		}

		// Token: 0x0400299B RID: 10651
		private readonly Task<TResult> m_task;
	}
}
