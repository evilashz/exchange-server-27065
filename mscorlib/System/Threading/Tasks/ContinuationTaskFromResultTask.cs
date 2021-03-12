using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200053F RID: 1343
	internal sealed class ContinuationTaskFromResultTask<TAntecedentResult> : Task
	{
		// Token: 0x0600404A RID: 16458 RVA: 0x000EF994 File Offset: 0x000EDB94
		public ContinuationTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark) : base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000EF9D0 File Offset: 0x000EDBD0
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task<TAntecedentResult>> action = this.m_action as Action<Task<TAntecedentResult>>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task<TAntecedentResult>, object> action2 = this.m_action as Action<Task<TAntecedentResult>, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001A99 RID: 6809
		private Task<TAntecedentResult> m_antecedent;
	}
}
