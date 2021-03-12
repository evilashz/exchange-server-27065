using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x0200004F RID: 79
	internal class CompleteGetSearchableMailbox : SearchTask<SearchSource>
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00015CE0 File Offset: 0x00013EE0
		public override void Process(IList<SearchSource> item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "CompleteGetSearchableMailbox.Process Item:", item);
			GetSearchableMailboxesResults getSearchableMailboxesResults = new GetSearchableMailboxesResults();
			getSearchableMailboxesResults.Sources.AddRange(item);
			base.Executor.EnqueueNext(getSearchableMailboxesResults);
		}
	}
}
