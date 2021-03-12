using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F9 RID: 505
	internal class MultiMailboxSearch : DisposeTrackableBase
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x00037910 File Offset: 0x00035B10
		public MultiMailboxSearch(SearchCriteria criteria, List<MailboxInfo> users, PagingInfo pagingInfo, CallerInfo callerInfo, OrganizationId orgId)
		{
			Util.ThrowOnNull(criteria, "criteria");
			Util.ThrowOnNull(users, "users");
			Util.ThrowOnNull(pagingInfo, "pagingInfo");
			Util.ThrowOnNull(callerInfo, "callerInfo");
			this.mailboxes = users;
			this.criteria = criteria;
			this.pagingInfo = pagingInfo;
			this.callerInfo = callerInfo;
			this.orgId = orgId;
			this.resultAggregator = new ResultAggregator(Factory.Current.GetMaxRefinerResults(this.criteria.RecipientSession));
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000379AC File Offset: 0x00035BAC
		public IAsyncResult BeginSearch(AsyncCallback callback, object state)
		{
			if (MultiMailboxSearch.SearchState.Running == this.searchState)
			{
				return this.asyncResult;
			}
			if (this.asyncResult != null)
			{
				this.asyncResult.Dispose();
				this.asyncResult = null;
			}
			this.asyncResult = new AsyncResult(callback, state);
			this.searchState = MultiMailboxSearch.SearchState.Running;
			this.IncrementPerfCounters();
			IEwsEndpointDiscovery ewsEndpointDiscovery = Factory.Current.GetEwsEndpointDiscovery(this.mailboxes, this.orgId, this.callerInfo);
			long num = 0L;
			long num2 = 0L;
			Dictionary<GroupId, List<MailboxInfo>> mailboxGroups = ewsEndpointDiscovery.FindEwsEndpoints(out num, out num2);
			this.resultAggregator.ProtocolLog.Add("LocalMailboxMappingTime", num);
			this.resultAggregator.ProtocolLog.Add("AutodiscoverTime", num2);
			this.StartGroupSearches(mailboxGroups);
			return this.asyncResult;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00037A70 File Offset: 0x00035C70
		public ISearchResult EndSearch(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			if (!this.asyncResult.Equals(result))
			{
				throw new ArgumentException("result doesn't match with the search's result");
			}
			this.asyncResult.AsyncWaitHandle.WaitOne();
			this.searchState = MultiMailboxSearch.SearchState.Completed;
			this.UpdatePerfCountersBasedOnResults();
			return this.resultAggregator;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00037AC8 File Offset: 0x00035CC8
		public void AbortSearch()
		{
			if (this.searchState != MultiMailboxSearch.SearchState.Running)
			{
				return;
			}
			foreach (MailboxSearchGroup mailboxSearchGroup in this.groups)
			{
				mailboxSearchGroup.Abort();
			}
			this.searchState = MultiMailboxSearch.SearchState.Stopped;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00037B2C File Offset: 0x00035D2C
		private static void IncrementGroupPerfCounters(int localSearchCount, int remoteSearchCount, int searchGroupCount)
		{
			PerformanceCounters.AverageSearchGroupCreated.IncrementBy((long)searchGroupCount);
			PerformanceCounters.AverageSearchGroupCreatedBase.IncrementBy((long)searchGroupCount);
			if (localSearchCount > 0)
			{
				PerformanceCounters.TotalLocalSearches.IncrementBy((long)localSearchCount);
				PerformanceCounters.AverageLocalSearchGroupCreated.IncrementBy((long)localSearchCount);
				PerformanceCounters.AverageLocalSearchGroupCreatedBase.IncrementBy((long)localSearchCount);
			}
			if (remoteSearchCount > 0)
			{
				PerformanceCounters.TotalFanOutSearches.IncrementBy((long)remoteSearchCount);
				PerformanceCounters.AverageFanOutSearchGroupCreated.IncrementBy((long)remoteSearchCount);
				PerformanceCounters.AverageFanOutSearchGroupCreatedBase.IncrementBy((long)remoteSearchCount);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00037BAC File Offset: 0x00035DAC
		private void StartGroupSearches(Dictionary<GroupId, List<MailboxInfo>> mailboxGroups)
		{
			int num = 0;
			int num2 = 0;
			this.groups = new List<MailboxSearchGroup>(mailboxGroups.Count);
			foreach (KeyValuePair<GroupId, List<MailboxInfo>> keyValuePair in mailboxGroups)
			{
				if (keyValuePair.Key.GroupType == GroupType.Local)
				{
					num++;
				}
				else if (keyValuePair.Key.GroupType == GroupType.CrossServer)
				{
					num2++;
				}
				if (keyValuePair.Key.GroupType != GroupType.SkippedError)
				{
					if (keyValuePair.Key.GroupType != GroupType.Local && Util.IsNestedFanoutCall(this.callerInfo))
					{
						this.AddNestedFanoutMailboxesToPreviewErrors(keyValuePair.Value);
					}
					else
					{
						this.groups.Add(Factory.Current.CreateMailboxSearchGroup(keyValuePair.Key, keyValuePair.Value, this.criteria, this.pagingInfo, this.callerInfo));
					}
				}
				else
				{
					this.AddSkippedMailboxesToPreviewErrors(keyValuePair.Value, keyValuePair.Key.Error);
				}
			}
			this.resultAggregator.ProtocolLog.Add("NumberOfRemoteSearch", num2);
			MultiMailboxSearch.IncrementGroupPerfCounters(num, num2, mailboxGroups.Count);
			try
			{
				foreach (MailboxSearchGroup mailboxSearchGroup in this.groups)
				{
					mailboxSearchGroup.BeginExecuteSearch(new AsyncCallback(this.MailboxSearchGroupCompleted), mailboxSearchGroup);
					this.scheduledGroups++;
				}
			}
			catch (MultiMailboxSearchException ex)
			{
				Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Hit an unexpected exception during search execution.", this.callerInfo.QueryCorrelationId, ex.ToString());
			}
			finally
			{
				if (this.groups.Count == 0)
				{
					this.asyncResult.ReportCompletion();
				}
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00037DAC File Offset: 0x00035FAC
		private void MailboxSearchGroupCompleted(IAsyncResult result)
		{
			MailboxSearchGroup mailboxSearchGroup = (MailboxSearchGroup)result.AsyncState;
			ISearchResult aggregator = mailboxSearchGroup.EndExecuteSearch(result);
			this.resultAggregator.MergeSearchResult(aggregator);
			lock (this.locker)
			{
				this.completedGroups++;
				if (this.completedGroups == this.scheduledGroups && this.asyncResult != null)
				{
					this.asyncResult.ReportCompletion();
				}
				mailboxSearchGroup.Dispose();
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00037E3C File Offset: 0x0003603C
		private void AddSkippedMailboxesToPreviewErrors(List<MailboxInfo> mailboxes, Exception exception)
		{
			for (int i = 0; i < mailboxes.Count; i++)
			{
				this.resultAggregator.PreviewErrors.Add(new Pair<MailboxInfo, Exception>(mailboxes[i], exception));
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00037E78 File Offset: 0x00036078
		private void AddNestedFanoutMailboxesToPreviewErrors(List<MailboxInfo> mailboxes)
		{
			for (int i = 0; i < mailboxes.Count; i++)
			{
				this.resultAggregator.PreviewErrors.Add(new Pair<MailboxInfo, Exception>(mailboxes[i], new NestedFanoutException(mailboxes[i].DisplayName)));
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00037EC4 File Offset: 0x000360C4
		private void IncrementPerfCounters()
		{
			PerformanceCounters.TotalSearches.Increment();
			PerformanceCounters.TotalSearchesInProgress.Increment();
			PerformanceCounters.TotalRequestsPerSecond.Increment();
			switch (this.criteria.SearchType)
			{
			case SearchType.Statistics:
				PerformanceCounters.StatisticsRequestsPerSecond.Increment();
				break;
			case SearchType.Preview:
				PerformanceCounters.PreviewRequestsPerSecond.Increment();
				break;
			case SearchType.ExpandSources:
				PerformanceCounters.PreviewAndStatisticsRequestsPerSecond.Increment();
				break;
			}
			PerformanceCounters.AverageMailboxCountPerQuery.IncrementBy((long)this.mailboxes.Count);
			this.GetMailboxBucketPerfCounter(this.mailboxes.Count).Increment();
			long incrementValue = (long)((this.criteria.SubFilters == null) ? 0 : this.criteria.SubFilters.Count);
			PerformanceCounters.AverageKeywordsCountPerQuery.IncrementBy(incrementValue);
			this.GetKeywordsBucketPerfCounter(this.criteria.Query.Keywords().Count<string>()).Increment();
			this.searchTimer.Restart();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00037FC4 File Offset: 0x000361C4
		private ExPerformanceCounter GetKeywordsBucketPerfCounter(int keywordCount)
		{
			ExPerformanceCounter result = PerformanceCounters.TotalSearchesGreaterThan300Keywords;
			if (keywordCount == MultiMailboxSearch.KeywordsBuckets[0])
			{
				result = PerformanceCounters.TotalSearchesWithNoKeywords;
			}
			else if (keywordCount > MultiMailboxSearch.KeywordsBuckets[0] && keywordCount < MultiMailboxSearch.KeywordsBuckets[1])
			{
				result = PerformanceCounters.TotalSearchesBetween1To10Keywords;
			}
			else if (keywordCount >= MultiMailboxSearch.KeywordsBuckets[1] && keywordCount < MultiMailboxSearch.KeywordsBuckets[2])
			{
				result = PerformanceCounters.TotalSearchesBetween10To20Keywords;
			}
			else if (keywordCount >= MultiMailboxSearch.KeywordsBuckets[2] && keywordCount < MultiMailboxSearch.KeywordsBuckets[3])
			{
				result = PerformanceCounters.TotalSearchesBetween20To50Keywords;
			}
			else if (keywordCount >= MultiMailboxSearch.KeywordsBuckets[3] && keywordCount < MultiMailboxSearch.KeywordsBuckets[4])
			{
				result = PerformanceCounters.TotalSearchesBetween50To100Keywords;
			}
			else if (keywordCount >= MultiMailboxSearch.KeywordsBuckets[4] && keywordCount < MultiMailboxSearch.KeywordsBuckets[5])
			{
				result = PerformanceCounters.TotalSearchesBetween100To300Keywords;
			}
			return result;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00038078 File Offset: 0x00036278
		private ExPerformanceCounter GetMailboxBucketPerfCounter(int mailboxCount)
		{
			ExPerformanceCounter result = PerformanceCounters.TotalSearchesGreaterThan700Mailboxes;
			if (mailboxCount < MultiMailboxSearch.MailboxBuckets[0])
			{
				result = PerformanceCounters.TotalSearchesBelow5Mailboxes;
			}
			else if (mailboxCount >= MultiMailboxSearch.MailboxBuckets[0] && mailboxCount < MultiMailboxSearch.MailboxBuckets[1])
			{
				result = PerformanceCounters.TotalSearchesBetween5To10Mailboxes;
			}
			else if (mailboxCount >= MultiMailboxSearch.MailboxBuckets[1] && mailboxCount < MultiMailboxSearch.MailboxBuckets[2])
			{
				result = PerformanceCounters.TotalSearchesBetween10To50Mailboxes;
			}
			else if (mailboxCount >= MultiMailboxSearch.MailboxBuckets[2] && mailboxCount < MultiMailboxSearch.MailboxBuckets[3])
			{
				result = PerformanceCounters.TotalSearchesBetween50To100Mailboxes;
			}
			else if (mailboxCount >= MultiMailboxSearch.MailboxBuckets[3] && mailboxCount < MultiMailboxSearch.MailboxBuckets[4])
			{
				result = PerformanceCounters.TotalSearchesBetween100To400Mailboxes;
			}
			else if (mailboxCount >= MultiMailboxSearch.MailboxBuckets[4] && mailboxCount < MultiMailboxSearch.MailboxBuckets[5])
			{
				result = PerformanceCounters.TotalSearchesBetween400To700Mailboxes;
			}
			return result;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003812C File Offset: 0x0003632C
		private void UpdatePerfCountersBasedOnResults()
		{
			this.searchTimer.Stop();
			PerformanceCounters.TotalSearchesInProgress.Decrement();
			MultiMailboxSearch.GetSearchLatencyPerfCounter(this.criteria.SearchType, (int)this.searchTimer.ElapsedMilliseconds).Increment();
			switch (this.criteria.SearchType)
			{
			case SearchType.Statistics:
				PerformanceCounters.AverageStatisticsRequestProcessingTime.IncrementBy(this.searchTimer.ElapsedTicks);
				PerformanceCounters.AverageStatisticsRequestProcessingTimeBase.Increment();
				return;
			case SearchType.Preview:
				PerformanceCounters.AveragePreviewRequestProcessingTime.IncrementBy(this.searchTimer.ElapsedTicks);
				PerformanceCounters.AveragePreviewRequestProcessingTimeBase.Increment();
				PerformanceCounters.AverageFailedMailboxesInPreview.IncrementBy((long)this.resultAggregator.PreviewErrors.Count);
				PerformanceCounters.AverageFailedMailboxesInPreviewBase.Increment();
				return;
			case SearchType.ExpandSources:
				PerformanceCounters.AveragePreviewAndStatisticsRequestProcessingTime.IncrementBy(this.searchTimer.ElapsedTicks);
				PerformanceCounters.AveragePreviewAndStatisticsRequestProcessingTimeBase.Increment();
				PerformanceCounters.AverageFailedMailboxesInPreview.IncrementBy((long)this.resultAggregator.PreviewErrors.Count);
				PerformanceCounters.AverageFailedMailboxesInPreviewBase.Increment();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00038248 File Offset: 0x00036448
		internal static ExPerformanceCounter GetSearchLatencyPerfCounter(SearchType searchType, int searchTimeInMilliSeconds)
		{
			ExPerformanceCounter result = (searchType == SearchType.Preview) ? PerformanceCounters.TotalPreviewSearchesGreaterThan120Seconds : PerformanceCounters.TotalStatsSearchesGreaterThan120Seconds;
			if (searchTimeInMilliSeconds < MultiMailboxSearch.SearchLatencyBuckets[0])
			{
				result = ((searchType == SearchType.Preview) ? PerformanceCounters.TotalPreviewSearchesBelow500msec : PerformanceCounters.TotalStatsSearchesBelow500msec);
			}
			else if (searchTimeInMilliSeconds >= MultiMailboxSearch.SearchLatencyBuckets[0] && searchTimeInMilliSeconds < MultiMailboxSearch.SearchLatencyBuckets[1])
			{
				result = ((searchType == SearchType.Preview) ? PerformanceCounters.TotalPreviewSearchesBetween500msecTo2sec : PerformanceCounters.TotalStatsSearchesBetween500msecTo2sec);
			}
			else if (searchTimeInMilliSeconds >= MultiMailboxSearch.SearchLatencyBuckets[1] && searchTimeInMilliSeconds < MultiMailboxSearch.SearchLatencyBuckets[2])
			{
				result = ((searchType == SearchType.Preview) ? PerformanceCounters.TotalPreviewSearchesBetween2To10sec : PerformanceCounters.TotalStatsSearchesBetween2To10sec);
			}
			else if (searchTimeInMilliSeconds >= MultiMailboxSearch.SearchLatencyBuckets[2] && searchTimeInMilliSeconds < MultiMailboxSearch.SearchLatencyBuckets[3])
			{
				result = ((searchType == SearchType.Preview) ? PerformanceCounters.TotalPreviewSearchesBetween10SecTo60Sec : PerformanceCounters.TotalStatsSearchesBetween10SecTo60Sec);
			}
			else if (searchTimeInMilliSeconds >= MultiMailboxSearch.SearchLatencyBuckets[3] && searchTimeInMilliSeconds < MultiMailboxSearch.SearchLatencyBuckets[4])
			{
				result = ((searchType == SearchType.Preview) ? PerformanceCounters.TotalPreviewSearchesBetween60SecTo120Sec : PerformanceCounters.TotalStatsSearchesBetween60SecTo120Sec);
			}
			return result;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00038324 File Offset: 0x00036524
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.searchState == MultiMailboxSearch.SearchState.Running)
				{
					if (this.groups != null)
					{
						foreach (MailboxSearchGroup mailboxSearchGroup in this.groups)
						{
							mailboxSearchGroup.Dispose();
						}
					}
					this.searchState = MultiMailboxSearch.SearchState.Stopped;
				}
				if (this.asyncResult != null)
				{
					this.asyncResult.Dispose();
					this.asyncResult = null;
				}
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000383AC File Offset: 0x000365AC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MultiMailboxSearch>(this);
		}

		// Token: 0x0400093B RID: 2363
		private readonly Stopwatch searchTimer = new Stopwatch();

		// Token: 0x0400093C RID: 2364
		private readonly List<MailboxInfo> mailboxes;

		// Token: 0x0400093D RID: 2365
		private readonly PagingInfo pagingInfo;

		// Token: 0x0400093E RID: 2366
		private readonly SearchCriteria criteria;

		// Token: 0x0400093F RID: 2367
		private readonly OrganizationId orgId;

		// Token: 0x04000940 RID: 2368
		private List<MailboxSearchGroup> groups;

		// Token: 0x04000941 RID: 2369
		private ISearchResult resultAggregator;

		// Token: 0x04000942 RID: 2370
		private int scheduledGroups;

		// Token: 0x04000943 RID: 2371
		private int completedGroups;

		// Token: 0x04000944 RID: 2372
		private CallerInfo callerInfo;

		// Token: 0x04000945 RID: 2373
		private object locker = new object();

		// Token: 0x04000946 RID: 2374
		private AsyncResult asyncResult;

		// Token: 0x04000947 RID: 2375
		private MultiMailboxSearch.SearchState searchState;

		// Token: 0x04000948 RID: 2376
		internal static readonly int[] SearchLatencyBuckets = new int[]
		{
			500,
			TimeSpan.FromSeconds(2.0).Milliseconds,
			TimeSpan.FromSeconds(10.0).Milliseconds,
			TimeSpan.FromMinutes(1.0).Milliseconds,
			TimeSpan.FromMinutes(2.0).Milliseconds
		};

		// Token: 0x04000949 RID: 2377
		private static readonly int[] KeywordsBuckets = new int[]
		{
			0,
			10,
			20,
			50,
			100,
			300
		};

		// Token: 0x0400094A RID: 2378
		private static readonly int[] MailboxBuckets = new int[]
		{
			5,
			10,
			50,
			100,
			400,
			700
		};

		// Token: 0x020001FA RID: 506
		private enum SearchState
		{
			// Token: 0x0400094C RID: 2380
			NotStarted,
			// Token: 0x0400094D RID: 2381
			Running,
			// Token: 0x0400094E RID: 2382
			Stopped,
			// Token: 0x0400094F RID: 2383
			Completed
		}
	}
}
