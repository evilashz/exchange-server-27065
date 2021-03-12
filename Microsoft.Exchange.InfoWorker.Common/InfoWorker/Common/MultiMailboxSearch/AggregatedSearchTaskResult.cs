using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001BB RID: 443
	internal class AggregatedSearchTaskResult : ISearchTaskResult, ISearchResult
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x000344B4 File Offset: 0x000326B4
		internal AggregatedSearchTaskResult(MailboxInfoList mailboxInfoList, SortedResultPage resultPage, Dictionary<string, List<IRefinerResult>> refinerResults, ulong totalResultCount, ByteQuantifiedSize totalResultSize, List<Pair<MailboxInfo, Exception>> previewFailures, List<MailboxStatistics> mailboxStatistics, IProtocolLog protocolLog) : this(SearchType.Preview, true, mailboxInfoList, resultPage, refinerResults, mailboxStatistics, protocolLog, totalResultCount, totalResultSize, previewFailures, null, null)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000344D8 File Offset: 0x000326D8
		internal AggregatedSearchTaskResult(MailboxInfoList mailboxInfoList, List<Pair<MailboxInfo, Exception>> previewFailures) : this(SearchType.Preview, false, mailboxInfoList, null, null, null, null, 0UL, ByteQuantifiedSize.Zero, previewFailures, null, null)
		{
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000344FC File Offset: 0x000326FC
		internal AggregatedSearchTaskResult(MailboxInfoList mailboxInfoList, Exception searchError) : this(SearchType.Preview, false, mailboxInfoList, null, null, null, null, 0UL, ByteQuantifiedSize.Zero, null, null, searchError)
		{
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00034520 File Offset: 0x00032720
		internal AggregatedSearchTaskResult(MailboxInfoList mailboxInfoList, List<IKeywordHit> keywordHitList, ulong totalResultCount, ByteQuantifiedSize totalResultSize) : this(SearchType.Statistics, true, mailboxInfoList, null, null, null, null, totalResultCount, totalResultSize, null, keywordHitList, null)
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00034540 File Offset: 0x00032740
		internal AggregatedSearchTaskResult(MailboxInfoList mailboxInfoList, List<IKeywordHit> keywordHitList) : this(mailboxInfoList, keywordHitList, 0UL, ByteQuantifiedSize.Zero)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00034554 File Offset: 0x00032754
		internal AggregatedSearchTaskResult(MailboxInfoList mailboxInfoList, List<string> keywords, Exception exception) : this(SearchType.Statistics, false, mailboxInfoList, null, null, null, null, 0UL, ByteQuantifiedSize.Zero, null, null, exception)
		{
			Util.ThrowOnNull(keywords, "keywords");
			Util.ThrowOnNull(exception, "exception");
			this.keywordStatisticsResult = new Dictionary<string, IKeywordHit>(keywords.Count, StringComparer.InvariantCultureIgnoreCase);
			foreach (string text in keywords)
			{
				IKeywordHit keywordHit = new KeywordHit(text, this.totalResultCount, this.totalResultSize);
				foreach (MailboxInfo first in this.mailboxInfoList)
				{
					keywordHit.Errors.Add(new Pair<MailboxInfo, Exception>(first, exception));
				}
				this.keywordStatisticsResult.Add(text, keywordHit);
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003464C File Offset: 0x0003284C
		private AggregatedSearchTaskResult(SearchType searchType, bool isSuccess, MailboxInfoList mailboxInfoList, SortedResultPage previewResultPage, Dictionary<string, List<IRefinerResult>> refinerResult, List<MailboxStatistics> mailboxStatistics, IProtocolLog protocolLog, ulong totalResultCount, ByteQuantifiedSize totalResultSize, List<Pair<MailboxInfo, Exception>> previewFailures, List<IKeywordHit> keywordStatsResults, Exception error)
		{
			Util.ThrowOnNull(mailboxInfoList, "mailboxInfoList");
			if (mailboxInfoList.Count == 0)
			{
				throw new ArgumentException("Invalid or empty mailboxInfoList");
			}
			this.resultType = searchType;
			this.mailboxInfoList = mailboxInfoList;
			this.success = isSuccess;
			this.exception = error;
			this.totalResultCount = totalResultCount;
			this.totalResultSize = totalResultSize;
			this.protocolLog = protocolLog;
			if (searchType == SearchType.Preview)
			{
				if (this.success)
				{
					if (totalResultCount > 0UL)
					{
						Util.ThrowOnNull(previewResultPage, "resultPage");
					}
					if (previewResultPage != null && previewResultPage.ResultCount > 0)
					{
						if (totalResultCount < (ulong)((long)previewResultPage.ResultCount))
						{
							Factory.Current.LocalTaskTracer.TraceError<string, Guid>((long)this.GetHashCode(), "The total result count was less than the current page result count for the mailbox:{0} on database:{1}", this.mailboxInfoList[0].MailboxGuid.ToString(), this.mailboxInfoList[0].MdbGuid);
							throw new ArgumentException("The totalResultCount must be greater than or equal to the current page result count");
						}
						if (totalResultSize == ByteQuantifiedSize.Zero)
						{
							Factory.Current.LocalTaskTracer.TraceError<string, string>((long)this.GetHashCode(), "There are results from FAST but the size information was not returned from FAST for the mailbox:{0} on database:{1}", this.mailboxInfoList[0].MailboxGuid.ToString(), this.mailboxInfoList[0].MdbGuid.ToString());
							Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryFailedToFetchSizeInformation, null, new object[]
							{
								this.mailboxInfoList[0].MailboxGuid.ToString(),
								this.mailboxInfoList[0].MdbGuid.ToString()
							});
						}
					}
					this.previewSearchResultPage = previewResultPage;
					this.refinersResults = refinerResult;
					Util.ThrowOnNull(mailboxStatistics, "mailboxStatistics");
					if (this.previewSearchResultPage != null && this.previewSearchResultPage.ResultCount > 0 && mailboxStatistics.Count == 0)
					{
						throw new ArgumentException("The MailboxStats cannot be an empty list when the results are present.");
					}
					this.mailboxStatistics = mailboxStatistics;
				}
				this.previewErrors = new List<Pair<MailboxInfo, Exception>>(this.mailboxInfoList.Count);
				if (this.exception != null)
				{
					foreach (MailboxInfo first in this.mailboxInfoList)
					{
						this.previewErrors.Add(new Pair<MailboxInfo, Exception>(first, this.exception));
					}
				}
				if (previewFailures != null)
				{
					this.previewErrors.AddRange(previewFailures);
					return;
				}
			}
			else if (searchType == SearchType.Statistics && isSuccess)
			{
				Util.ThrowOnNull(keywordStatsResults, null);
				this.keywordStatisticsResult = new Dictionary<string, IKeywordHit>(keywordStatsResults.Count, StringComparer.InvariantCultureIgnoreCase);
				foreach (IKeywordHit keywordHit in keywordStatsResults)
				{
					IKeywordHit keywordHit2 = null;
					if (!this.keywordStatisticsResult.TryGetValue(keywordHit.Phrase, out keywordHit2))
					{
						this.keywordStatisticsResult.Add(keywordHit.Phrase, keywordHit);
					}
				}
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00034980 File Offset: 0x00032B80
		public SearchType ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00034988 File Offset: 0x00032B88
		public SortedResultPage PreviewResult
		{
			get
			{
				return this.previewSearchResultPage;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00034990 File Offset: 0x00032B90
		public List<Pair<MailboxInfo, Exception>> PreviewErrors
		{
			get
			{
				return this.previewErrors;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00034998 File Offset: 0x00032B98
		public bool Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000349A0 File Offset: 0x00032BA0
		public IProtocolLog ProtocolLog
		{
			get
			{
				return this.protocolLog;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000349A8 File Offset: 0x00032BA8
		public Dictionary<string, List<IRefinerResult>> RefinersResult
		{
			get
			{
				return this.refinersResults;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000349B0 File Offset: 0x00032BB0
		public IDictionary<string, IKeywordHit> KeywordStatistics
		{
			get
			{
				return this.keywordStatisticsResult;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000349B8 File Offset: 0x00032BB8
		public ulong TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x000349C0 File Offset: 0x00032BC0
		public ByteQuantifiedSize TotalResultSize
		{
			get
			{
				return this.totalResultSize;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000349C8 File Offset: 0x00032BC8
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x000349D0 File Offset: 0x00032BD0
		public List<MailboxStatistics> MailboxStats
		{
			get
			{
				return this.mailboxStatistics;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000349D8 File Offset: 0x00032BD8
		internal MailboxInfoList MailboxInfoList
		{
			get
			{
				return this.mailboxInfoList;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000349E0 File Offset: 0x00032BE0
		public void MergeSearchResult(ISearchResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040008DB RID: 2267
		private readonly SortedResultPage previewSearchResultPage;

		// Token: 0x040008DC RID: 2268
		private readonly MailboxInfoList mailboxInfoList;

		// Token: 0x040008DD RID: 2269
		private readonly Exception exception;

		// Token: 0x040008DE RID: 2270
		private readonly bool success;

		// Token: 0x040008DF RID: 2271
		private readonly SearchType resultType;

		// Token: 0x040008E0 RID: 2272
		private readonly Dictionary<string, List<IRefinerResult>> refinersResults;

		// Token: 0x040008E1 RID: 2273
		private readonly ulong totalResultCount;

		// Token: 0x040008E2 RID: 2274
		private readonly ByteQuantifiedSize totalResultSize;

		// Token: 0x040008E3 RID: 2275
		private readonly List<MailboxStatistics> mailboxStatistics;

		// Token: 0x040008E4 RID: 2276
		private readonly List<Pair<MailboxInfo, Exception>> previewErrors;

		// Token: 0x040008E5 RID: 2277
		private readonly Dictionary<string, IKeywordHit> keywordStatisticsResult;

		// Token: 0x040008E6 RID: 2278
		private readonly IProtocolLog protocolLog;
	}
}
