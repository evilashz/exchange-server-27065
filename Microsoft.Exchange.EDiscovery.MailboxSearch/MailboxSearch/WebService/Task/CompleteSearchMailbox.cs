using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x0200004D RID: 77
	internal class CompleteSearchMailbox : SearchTask<SearchMailboxesResults>
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00015C68 File Offset: 0x00013E68
		public CompleteSearchMailbox.CompleteSearchMailboxContext TaskContext
		{
			get
			{
				return (CompleteSearchMailbox.CompleteSearchMailboxContext)base.Context.TaskContext;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00015C7A File Offset: 0x00013E7A
		public override void Process(SearchMailboxesResults item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "CompleteSearchMailbox.Process Item:", item);
			this.TaskContext.Results.MergeResults(item);
			base.Executor.EnqueueNext(this.TaskContext.Results);
		}

		// Token: 0x0200004E RID: 78
		internal class CompleteSearchMailboxContext
		{
			// Token: 0x06000379 RID: 889 RVA: 0x00015CB9 File Offset: 0x00013EB9
			public CompleteSearchMailboxContext()
			{
				this.Results = new SearchMailboxesResults(null);
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x0600037A RID: 890 RVA: 0x00015CCD File Offset: 0x00013ECD
			// (set) Token: 0x0600037B RID: 891 RVA: 0x00015CD5 File Offset: 0x00013ED5
			public SearchMailboxesResults Results { get; private set; }
		}
	}
}
