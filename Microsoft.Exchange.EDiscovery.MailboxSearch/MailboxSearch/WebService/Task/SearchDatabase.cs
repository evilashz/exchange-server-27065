using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x02000059 RID: 89
	internal class SearchDatabase : SearchTask<SearchSource>
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0001843C File Offset: 0x0001663C
		public override void Process(IList<SearchSource> item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "SearchDatabase.Process Item:", item);
			SearchMailboxesInputs searchMailboxesInputs = ((SearchMailboxesInputs)base.Executor.Context.Input).Clone();
			searchMailboxesInputs.Sources = item.ToList<SearchSource>();
			ISearchResultProvider searchResultProvider = SearchFactory.Current.GetSearchResultProvider(base.Policy, searchMailboxesInputs.SearchType);
			SearchMailboxesResults item2 = searchResultProvider.Search(base.Policy, searchMailboxesInputs);
			base.Executor.EnqueueNext(item2);
		}
	}
}
