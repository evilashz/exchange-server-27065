using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000043 RID: 67
	internal class SearchMailboxesResults
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00014A10 File Offset: 0x00012C10
		public SearchMailboxesResults(IEnumerable<SearchSource> sources = null)
		{
			this.failures = new ConcurrentQueue<Exception>();
			this.sources = ((sources != null) ? new ConcurrentQueue<SearchSource>(sources) : new ConcurrentQueue<SearchSource>());
			this.SearchResult = new ResultAggregator();
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00014A44 File Offset: 0x00012C44
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00014A4C File Offset: 0x00012C4C
		public ISearchResult SearchResult { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00014A55 File Offset: 0x00012C55
		public IEnumerable<Exception> Failures
		{
			get
			{
				return this.failures;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00014A5D File Offset: 0x00012C5D
		public IEnumerable<SearchSource> Sources
		{
			get
			{
				return this.sources;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00014A68 File Offset: 0x00012C68
		public void AddSources(IEnumerable<SearchSource> sources)
		{
			foreach (SearchSource item in sources)
			{
				this.sources.Enqueue(item);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00014AB8 File Offset: 0x00012CB8
		public void AddFailures(IEnumerable<Exception> failures)
		{
			foreach (Exception item in failures)
			{
				this.failures.Enqueue(item);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00014B08 File Offset: 0x00012D08
		public void MergeResults(SearchMailboxesResults result)
		{
			this.AddSources(result.Sources);
			this.SearchResult.MergeSearchResult(result.SearchResult);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00014B30 File Offset: 0x00012D30
		public void UpdateResults(IEnumerable<FanoutParameters> parameters, SearchMailboxesInputs input, SearchMailboxesResponse response, Exception exception)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"SearchMailboxesResults.UpdateResults Parameters:",
				parameters,
				"Input:",
				input,
				"Response:",
				response,
				"Exception:",
				exception
			});
			this.AddSources(from t in parameters
			select t.Source);
			using (WebServiceMailboxSearchGroup webServiceMailboxSearchGroup = new WebServiceMailboxSearchGroup(parameters.First<FanoutParameters>().GroupId, new WebServiceMailboxSearchGroup.FindMailboxInfoHandler(this.FindMailboxInfo), input.Criteria, input.PagingInfo, input.CallerInfo))
			{
				if (exception == null && (response.SearchResult == null || response.Result == 2))
				{
					exception = new ServiceResponseException(response);
				}
				if (exception != null)
				{
					using (IEnumerator<SearchSource> enumerator = this.Sources.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							SearchSource searchSource = enumerator.Current;
							if (searchSource.MailboxInfo != null)
							{
								webServiceMailboxSearchGroup.MergeMailboxResult(searchSource.MailboxInfo, exception);
							}
						}
						goto IL_10D;
					}
				}
				if (response.SearchResult != null)
				{
					webServiceMailboxSearchGroup.MergeSearchResults(response.SearchResult);
				}
				IL_10D:
				this.SearchResult = webServiceMailboxSearchGroup.GetResultAggregator();
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00014D30 File Offset: 0x00012F30
		private MailboxInfo FindMailboxInfo(object state)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "SearchMailboxesResults.FindMailboxInfo State:", state);
			if (this.sources != null && this.sources.Count > 0)
			{
				Recorder.Trace(4L, TraceType.InfoTrace, "SearchMailboxesResults.FindMailboxInfo Count:", this.sources.Count);
				SearchSource searchSource = null;
				SearchPreviewItem previewItem = state as SearchPreviewItem;
				if (previewItem != null)
				{
					searchSource = this.Sources.FirstOrDefault((SearchSource t) => string.Equals(t.OriginalReferenceId, previewItem.Mailbox.MailboxId, StringComparison.InvariantCultureIgnoreCase) || string.Equals(t.ReferenceId, previewItem.Mailbox.MailboxId, StringComparison.InvariantCultureIgnoreCase));
				}
				FailedSearchMailbox failedItem = state as FailedSearchMailbox;
				if (failedItem != null)
				{
					searchSource = this.Sources.FirstOrDefault((SearchSource t) => string.Equals(t.OriginalReferenceId, failedItem.Mailbox, StringComparison.InvariantCultureIgnoreCase) || string.Equals(t.ReferenceId, failedItem.Mailbox, StringComparison.InvariantCultureIgnoreCase));
				}
				MailboxStatisticsItem statisticsItem = state as MailboxStatisticsItem;
				if (statisticsItem != null)
				{
					searchSource = this.Sources.FirstOrDefault((SearchSource t) => string.Equals(t.OriginalReferenceId, statisticsItem.MailboxId, StringComparison.InvariantCultureIgnoreCase) || string.Equals(t.ReferenceId, statisticsItem.MailboxId, StringComparison.InvariantCultureIgnoreCase));
				}
				if (searchSource != null)
				{
					return searchSource.MailboxInfo;
				}
			}
			Recorder.Trace(4L, TraceType.WarningTrace, "SearchMailboxesResults.FindMailboxInfo Source Not Found State:", state);
			return null;
		}

		// Token: 0x04000161 RID: 353
		private ConcurrentQueue<SearchSource> sources;

		// Token: 0x04000162 RID: 354
		private ConcurrentQueue<Exception> failures;
	}
}
