using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000699 RID: 1689
	internal class AsyncServiceTask<T> : ServiceTask<T>
	{
		// Token: 0x060033DB RID: 13275 RVA: 0x000B9848 File Offset: 0x000B7A48
		internal AsyncServiceTask(BaseRequest request, CallContext callContext, ServiceAsyncResult<T> serviceAsyncResult) : base(request, callContext, serviceAsyncResult)
		{
			this.requestStart = ExDateTime.UtcNow;
			this.budgetKey = base.CallContext.Budget.Owner;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000B9874 File Offset: 0x000B7A74
		protected internal override void InternalComplete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			if (this.executionException == null)
			{
				this.queueAndDelayTime = queueAndDelayTime;
				return;
			}
			this.FinishRequest("[CWE]", queueAndDelayTime, ExDateTime.UtcNow - this.requestStart, this.executionException);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000B98A8 File Offset: 0x000B7AA8
		protected internal override TaskExecuteResult InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime)
		{
			IAsyncServiceCommand asyncServiceCommand = base.Request.ServiceCommand as IAsyncServiceCommand;
			asyncServiceCommand.CompleteRequestAsyncCallback = new CompleteRequestAsyncCallback(this.CompleteRequestCallback);
			TaskExecuteResult result = base.InternalExecute(queueAndDelay, totalTime);
			if (this.budget != null)
			{
				this.budget.LogEndStateToIIS();
				this.budget.Dispose();
				this.budget = null;
			}
			return result;
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000B9907 File Offset: 0x000B7B07
		protected internal override void InternalTimeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			base.InternalTimeout(queueAndDelayTime, totalTime);
			if (this.budget != null)
			{
				this.budget.LogEndStateToIIS();
				this.budget.Dispose();
				this.budget = null;
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000B9936 File Offset: 0x000B7B36
		protected internal override void InternalCancel()
		{
			base.InternalCancel();
			if (this.budget != null)
			{
				this.budget.LogEndStateToIIS();
				this.budget.Dispose();
				this.budget = null;
			}
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000B9963 File Offset: 0x000B7B63
		private void CompleteRequestCallback(Exception exception)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[AsyncServiceTask.CompleteRequestCallback] Hanging request completed for task [{0}]", base.Description);
			this.FinishRequest("[C]", this.queueAndDelayTime, ExDateTime.UtcNow - this.requestStart, exception);
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000B99A4 File Offset: 0x000B7BA4
		public override IBudget Budget
		{
			get
			{
				IBudget budget = null;
				try
				{
					budget = base.Budget;
				}
				catch (ObjectDisposedException)
				{
				}
				if (budget == null)
				{
					this.budget = EwsBudget.Acquire(this.budgetKey);
					budget = this.budget;
				}
				return budget;
			}
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000B99EC File Offset: 0x000B7BEC
		protected override void WriteThrottlingDiagnostics(string logType, TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
		}

		// Token: 0x04001D4F RID: 7503
		private ExDateTime requestStart;

		// Token: 0x04001D50 RID: 7504
		private TimeSpan queueAndDelayTime;

		// Token: 0x04001D51 RID: 7505
		private BudgetKey budgetKey;

		// Token: 0x04001D52 RID: 7506
		private IEwsBudget budget;
	}
}
