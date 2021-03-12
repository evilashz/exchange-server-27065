using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000532 RID: 1330
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x06004026 RID: 16422 RVA: 0x000EF335 File Offset: 0x000ED535
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000EF34B File Offset: 0x000ED54B
		[SecurityCritical]
		public void ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000EF35E File Offset: 0x000ED55E
		[SecurityCritical]
		public void MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x04001A64 RID: 6756
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04001A65 RID: 6757
		private readonly Task m_completingTask;
	}
}
