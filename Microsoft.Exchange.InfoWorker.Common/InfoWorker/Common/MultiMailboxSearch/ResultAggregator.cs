using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MultiMailboxSearch;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200020E RID: 526
	internal class ResultAggregator : ISearchResult
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x0003E3FF File Offset: 0x0003C5FF
		public ResultAggregator() : this(0)
		{
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003E408 File Offset: 0x0003C608
		public ResultAggregator(int refinerResultTrim) : this(null, new Dictionary<string, List<IRefinerResult>>(0), 0UL, ByteQuantifiedSize.Zero, new List<Pair<MailboxInfo, Exception>>(0), new Dictionary<string, IKeywordHit>(0), new List<MailboxStatistics>(4))
		{
			this.refinerResultsTrimCount = refinerResultTrim;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003E437 File Offset: 0x0003C637
		public ResultAggregator(SortedResultPage resultPage, Dictionary<string, List<IRefinerResult>> refinerResults, ulong totalResultCount, ByteQuantifiedSize totalResultSize, List<Pair<MailboxInfo, Exception>> previewErrors) : this(resultPage, refinerResults, totalResultCount, totalResultSize, previewErrors, new Dictionary<string, IKeywordHit>(0), new List<MailboxStatistics>(4))
		{
			if (resultPage != null)
			{
				int resultCount = resultPage.ResultCount;
			}
			if (resultPage != null)
			{
				int resultCount2 = resultPage.ResultCount;
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003E46A File Offset: 0x0003C66A
		public ResultAggregator(Dictionary<string, IKeywordHit> keywordStatistics) : this(null, new Dictionary<string, List<IRefinerResult>>(0), 0UL, ByteQuantifiedSize.Zero, new List<Pair<MailboxInfo, Exception>>(0), keywordStatistics, new List<MailboxStatistics>(4))
		{
			Util.ThrowOnNull(keywordStatistics, "keywordStatistics");
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003E498 File Offset: 0x0003C698
		public ResultAggregator(SortedResultPage resultPage, Dictionary<string, List<IRefinerResult>> refinerResults, ulong totalResultCount, ByteQuantifiedSize totalResultSize, List<Pair<MailboxInfo, Exception>> previewErrors, Dictionary<string, IKeywordHit> keywordStatistics, List<MailboxStatistics> mailboxStatistics)
		{
			this.keywordStatistics = keywordStatistics;
			this.previewResult = resultPage;
			this.refinerResults = refinerResults;
			this.previewErrors = previewErrors;
			this.totalResultCount = totalResultCount;
			this.totalResultSize = totalResultSize;
			this.mailboxStatistics = mailboxStatistics;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0003E4F6 File Offset: 0x0003C6F6
		public SortedResultPage PreviewResult
		{
			get
			{
				return this.previewResult;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0003E4FE File Offset: 0x0003C6FE
		public IDictionary<string, IKeywordHit> KeywordStatistics
		{
			get
			{
				return this.keywordStatistics;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0003E506 File Offset: 0x0003C706
		public List<Pair<MailboxInfo, Exception>> PreviewErrors
		{
			get
			{
				return this.previewErrors;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003E50E File Offset: 0x0003C70E
		public Dictionary<string, List<IRefinerResult>> RefinersResult
		{
			get
			{
				return this.refinerResults;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0003E516 File Offset: 0x0003C716
		public IProtocolLog ProtocolLog
		{
			get
			{
				return this.protocolLog;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003E51E File Offset: 0x0003C71E
		public ulong TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003E526 File Offset: 0x0003C726
		public ByteQuantifiedSize TotalResultSize
		{
			get
			{
				return this.totalResultSize;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0003E52E File Offset: 0x0003C72E
		public List<MailboxStatistics> MailboxStats
		{
			get
			{
				return this.mailboxStatistics;
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003E538 File Offset: 0x0003C738
		public void MergeSearchResult(ISearchResult aggregator)
		{
			if (aggregator == null || (aggregator != null && aggregator.PreviewResult == null && aggregator.PreviewErrors == null && aggregator.RefinersResult == null && aggregator.MailboxStats == null && aggregator.KeywordStatistics == null))
			{
				return;
			}
			lock (this.locker)
			{
				if (this.previewResult == null)
				{
					this.previewResult = aggregator.PreviewResult;
				}
				else
				{
					this.previewResult.Merge(aggregator.PreviewResult);
				}
				this.MergeRefiners(aggregator.RefinersResult);
				this.MergeStatistics(aggregator.KeywordStatistics);
				this.totalResultCount += aggregator.TotalResultCount;
				this.totalResultSize += aggregator.TotalResultSize;
				if (aggregator.MailboxStats != null)
				{
					this.MergeMailboxStatistics(aggregator.MailboxStats);
				}
				if (aggregator.PreviewErrors != null)
				{
					this.previewErrors.AddRange(aggregator.PreviewErrors);
				}
				if (aggregator.ProtocolLog != null)
				{
					this.protocolLog.Merge(aggregator.ProtocolLog);
				}
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003E660 File Offset: 0x0003C860
		protected void MergeMailboxStatistics(List<MailboxStatistics> other)
		{
			if (other == null || other.Count == 0)
			{
				return;
			}
			Dictionary<string, MailboxStatistics> dictionary = this.MailboxStats.ToDictionary((MailboxStatistics x) => x.MailboxInfo.GetUniqueKey());
			foreach (MailboxStatistics mailboxStatistics in other)
			{
				string uniqueKey = mailboxStatistics.MailboxInfo.GetUniqueKey();
				if (!dictionary.ContainsKey(uniqueKey))
				{
					dictionary.Add(uniqueKey, mailboxStatistics);
				}
				else
				{
					dictionary[uniqueKey].Merge(mailboxStatistics);
				}
			}
			this.mailboxStatistics = dictionary.Values.ToList<MailboxStatistics>();
			this.mailboxStatistics.Sort(new MailboxStatsComparer(false));
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003E73C File Offset: 0x0003C93C
		protected virtual void MergeRefiners(Dictionary<string, List<IRefinerResult>> refinerToBeMerged)
		{
			if (refinerToBeMerged == null)
			{
				return;
			}
			foreach (string key in refinerToBeMerged.Keys)
			{
				List<IRefinerResult> source;
				if (!this.refinerResults.TryGetValue(key, out source))
				{
					this.refinerResults.Add(key, refinerToBeMerged[key]);
				}
				else
				{
					List<IRefinerResult> list = refinerToBeMerged[key];
					if (list != null && list.Count > 0)
					{
						Dictionary<string, IRefinerResult> dictionary = source.ToDictionary((IRefinerResult x) => x.Value);
						foreach (IRefinerResult refinerResult in list)
						{
							IRefinerResult refinerResult2;
							if (!dictionary.TryGetValue(refinerResult.Value, out refinerResult2))
							{
								dictionary.Add(refinerResult.Value, refinerResult);
							}
							else
							{
								refinerResult2.Merge(refinerResult);
							}
						}
						IEnumerable<IRefinerResult> source2 = from x in dictionary.Values
						orderby x.Count descending
						select x;
						int count = Math.Min(this.refinerResultsTrimCount, dictionary.Values.Count);
						List<IRefinerResult> value = source2.Take(count).ToList<IRefinerResult>();
						this.refinerResults[key] = value;
					}
				}
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003E8D4 File Offset: 0x0003CAD4
		private void MergeStatistics(IKeywordHit hit)
		{
			if (hit == null)
			{
				return;
			}
			IKeywordHit keywordHit = null;
			if (!this.keywordStatistics.TryGetValue(hit.Phrase, out keywordHit))
			{
				this.keywordStatistics.Add(hit.Phrase, hit);
				return;
			}
			keywordHit.Merge(hit);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003E918 File Offset: 0x0003CB18
		private void MergeStatistics(IDictionary<string, IKeywordHit> hits)
		{
			if (hits == null)
			{
				return;
			}
			foreach (KeyValuePair<string, IKeywordHit> keyValuePair in hits)
			{
				this.MergeStatistics(keyValuePair.Value);
			}
		}

		// Token: 0x040009C6 RID: 2502
		private static readonly Trace Tracer = ExTraceGlobals.LocalSearchTracer;

		// Token: 0x040009C7 RID: 2503
		private SortedResultPage previewResult;

		// Token: 0x040009C8 RID: 2504
		private readonly List<Pair<MailboxInfo, Exception>> previewErrors;

		// Token: 0x040009C9 RID: 2505
		private readonly Dictionary<string, IKeywordHit> keywordStatistics;

		// Token: 0x040009CA RID: 2506
		private readonly Dictionary<string, List<IRefinerResult>> refinerResults;

		// Token: 0x040009CB RID: 2507
		private readonly IProtocolLog protocolLog = new ProtocolLog();

		// Token: 0x040009CC RID: 2508
		private readonly object locker = new object();

		// Token: 0x040009CD RID: 2509
		private ulong totalResultCount;

		// Token: 0x040009CE RID: 2510
		private ByteQuantifiedSize totalResultSize;

		// Token: 0x040009CF RID: 2511
		private List<MailboxStatistics> mailboxStatistics;

		// Token: 0x040009D0 RID: 2512
		private readonly int refinerResultsTrimCount;
	}
}
