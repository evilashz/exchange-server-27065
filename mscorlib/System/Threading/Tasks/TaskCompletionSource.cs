using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x0200054E RID: 1358
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskCompletionSource<TResult>
	{
		// Token: 0x0600410E RID: 16654 RVA: 0x000F1C66 File Offset: 0x000EFE66
		[__DynamicallyInvokable]
		public TaskCompletionSource()
		{
			this.m_task = new Task<TResult>();
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x000F1C79 File Offset: 0x000EFE79
		[__DynamicallyInvokable]
		public TaskCompletionSource(TaskCreationOptions creationOptions) : this(null, creationOptions)
		{
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000F1C83 File Offset: 0x000EFE83
		[__DynamicallyInvokable]
		public TaskCompletionSource(object state) : this(state, TaskCreationOptions.None)
		{
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000F1C8D File Offset: 0x000EFE8D
		[__DynamicallyInvokable]
		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this.m_task = new Task<TResult>(state, creationOptions);
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x000F1CA2 File Offset: 0x000EFEA2
		[__DynamicallyInvokable]
		public Task<TResult> Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_task;
			}
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000F1CAC File Offset: 0x000EFEAC
		private void SpinUntilCompleted()
		{
			SpinWait spinWait = default(SpinWait);
			while (!this.m_task.IsCompleted)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000F1CD8 File Offset: 0x000EFED8
		[__DynamicallyInvokable]
		public bool TrySetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			bool flag = this.m_task.TrySetException(exception);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000F1D18 File Offset: 0x000EFF18
		[__DynamicallyInvokable]
		public bool TrySetException(IEnumerable<Exception> exceptions)
		{
			if (exceptions == null)
			{
				throw new ArgumentNullException("exceptions");
			}
			List<Exception> list = new List<Exception>();
			foreach (Exception ex in exceptions)
			{
				if (ex == null)
				{
					throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NullException"), "exceptions");
				}
				list.Add(ex);
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NoExceptions"), "exceptions");
			}
			bool flag = this.m_task.TrySetException(list);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000F1DD0 File Offset: 0x000EFFD0
		internal bool TrySetException(IEnumerable<ExceptionDispatchInfo> exceptions)
		{
			bool flag = this.m_task.TrySetException(exceptions);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000F1E01 File Offset: 0x000F0001
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (!this.TrySetException(exception))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x000F1E2A File Offset: 0x000F002A
		[__DynamicallyInvokable]
		public void SetException(IEnumerable<Exception> exceptions)
		{
			if (!this.TrySetException(exceptions))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000F1E48 File Offset: 0x000F0048
		[__DynamicallyInvokable]
		public bool TrySetResult(TResult result)
		{
			bool flag = this.m_task.TrySetResult(result);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000F1E79 File Offset: 0x000F0079
		[__DynamicallyInvokable]
		public void SetResult(TResult result)
		{
			if (!this.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000F1E94 File Offset: 0x000F0094
		[__DynamicallyInvokable]
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(default(CancellationToken));
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000F1EB0 File Offset: 0x000F00B0
		[__DynamicallyInvokable]
		public bool TrySetCanceled(CancellationToken cancellationToken)
		{
			bool flag = this.m_task.TrySetCanceled(cancellationToken);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x000F1EE1 File Offset: 0x000F00E1
		[__DynamicallyInvokable]
		public void SetCanceled()
		{
			if (!this.TrySetCanceled())
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x04001ABD RID: 6845
		private readonly Task<TResult> m_task;
	}
}
