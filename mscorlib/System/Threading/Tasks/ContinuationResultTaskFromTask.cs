using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200053E RID: 1342
	internal sealed class ContinuationResultTaskFromTask<TResult> : Task<TResult>
	{
		// Token: 0x06004048 RID: 16456 RVA: 0x000EF8F4 File Offset: 0x000EDAF4
		public ContinuationResultTaskFromTask(Task antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark) : base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x000EF930 File Offset: 0x000EDB30
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task, TResult> func = this.m_action as Func<Task, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task, object, TResult> func2 = this.m_action as Func<Task, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001A98 RID: 6808
		private Task m_antecedent;
	}
}
