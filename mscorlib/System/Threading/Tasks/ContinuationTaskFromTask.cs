using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200053D RID: 1341
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x06004046 RID: 16454 RVA: 0x000EF860 File Offset: 0x000EDA60
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark) : base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000EF89C File Offset: 0x000EDA9C
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001A97 RID: 6807
		private Task m_antecedent;
	}
}
