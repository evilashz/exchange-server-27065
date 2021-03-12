using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure
{
	// Token: 0x02000032 RID: 50
	internal abstract class SearchTask<TIn> : ITask where TIn : class
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013118 File Offset: 0x00011318
		public IExecutor Executor
		{
			get
			{
				return this.Context.Executor;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00013125 File Offset: 0x00011325
		public ISearchPolicy Policy
		{
			get
			{
				return this.Executor.Policy;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00013132 File Offset: 0x00011332
		protected SearchTaskContext Context
		{
			get
			{
				return (SearchTaskContext)((ITask)this).State;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00013140 File Offset: 0x00011340
		public virtual void Process(IList<TIn> items)
		{
			foreach (TIn item in items)
			{
				this.Process(item);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00013188 File Offset: 0x00011388
		public virtual void Execute(object item)
		{
			if (item is IList<object>)
			{
				this.Process(((IList<object>)item).Cast<TIn>().ToList<TIn>());
				return;
			}
			this.Process((TIn)((object)item));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000131B5 File Offset: 0x000113B5
		public virtual void Process(TIn item)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000131B7 File Offset: 0x000113B7
		public virtual void Complete()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000131B9 File Offset: 0x000113B9
		public virtual void Cancel()
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000131BB File Offset: 0x000113BB
		public virtual ResourceKey[] GetResources()
		{
			return null;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000131BE File Offset: 0x000113BE
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000131CB File Offset: 0x000113CB
		string ITask.Description
		{
			get
			{
				return base.GetType().Name;
			}
			set
			{
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000131CD File Offset: 0x000113CD
		TimeSpan ITask.MaxExecutionTime
		{
			get
			{
				return this.Executor.Policy.ExecutionSettings.SearchTimeout;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000131E4 File Offset: 0x000113E4
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000131EC File Offset: 0x000113EC
		object ITask.State { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000131F5 File Offset: 0x000113F5
		WorkloadSettings ITask.WorkloadSettings
		{
			get
			{
				return new WorkloadSettings(WorkloadType.Unknown, true);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000131FE File Offset: 0x000113FE
		IBudget ITask.Budget
		{
			get
			{
				return this.Executor.Policy.Budget;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00013210 File Offset: 0x00011410
		TaskExecuteResult ITask.CancelStep(LocalizedException exception)
		{
			this.Cancel();
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00013219 File Offset: 0x00011419
		void ITask.Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.Complete();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00013221 File Offset: 0x00011421
		TaskExecuteResult ITask.Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.Execute(this.Context.Item);
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00013235 File Offset: 0x00011435
		IActivityScope ITask.GetActivityScope()
		{
			return this.Executor.Policy.GetActivityScope();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00013247 File Offset: 0x00011447
		void ITask.Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.Cancel();
		}
	}
}
