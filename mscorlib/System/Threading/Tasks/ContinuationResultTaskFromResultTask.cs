using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000540 RID: 1344
	internal sealed class ContinuationResultTaskFromResultTask<TAntecedentResult, TResult> : Task<TResult>
	{
		// Token: 0x0600404C RID: 16460 RVA: 0x000EFA28 File Offset: 0x000EDC28
		public ContinuationResultTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark) : base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000EFA64 File Offset: 0x000EDC64
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>, TResult> func = this.m_action as Func<Task<TAntecedentResult>, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task<TAntecedentResult>, object, TResult> func2 = this.m_action as Func<Task<TAntecedentResult>, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001A9A RID: 6810
		private Task<TAntecedentResult> m_antecedent;
	}
}
