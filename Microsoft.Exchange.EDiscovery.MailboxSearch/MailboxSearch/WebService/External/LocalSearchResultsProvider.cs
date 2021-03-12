using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.External
{
	// Token: 0x02000027 RID: 39
	internal class LocalSearchResultsProvider : ISearchResultProvider
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000F9AF File Offset: 0x0000DBAF
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000F9B7 File Offset: 0x0000DBB7
		public long FastTime { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public long StoreTime { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000F9D1 File Offset: 0x0000DBD1
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000F9D9 File Offset: 0x0000DBD9
		public long RestrictionTime { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000F9E2 File Offset: 0x0000DBE2
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000F9EA File Offset: 0x0000DBEA
		public long TotalItems { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000F9F3 File Offset: 0x0000DBF3
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000F9FB File Offset: 0x0000DBFB
		public long TotalSize { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000FA04 File Offset: 0x0000DC04
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		public long ReturnedFastItems { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000FA15 File Offset: 0x0000DC15
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000FA1D File Offset: 0x0000DC1D
		public long ReturnedStoreItems { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000FA26 File Offset: 0x0000DC26
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000FA2E File Offset: 0x0000DC2E
		public long ReturnedStoreSize { get; set; }

		// Token: 0x060001DE RID: 478 RVA: 0x0000FA50 File Offset: 0x0000DC50
		public SearchMailboxesResults Search(ISearchPolicy policy, SearchMailboxesInputs input)
		{
			Guid databaseGuid = Guid.Empty;
			List<SearchSource> list = new List<SearchSource>(input.Sources);
			ResultAggregator aggregator = new ResultAggregator();
			IEnumerable<List<string>> enumerable = null;
			Recorder.Record record = policy.Recorder.Start("SearchResultProvider", TraceType.InfoTrace, true);
			Recorder.Trace(5L, TraceType.InfoTrace, new object[]
			{
				"LocalSearchResultsProvider.Search Input:",
				input,
				"Type:",
				input.SearchType
			});
			try
			{
				if (input.SearchType == SearchType.Statistics)
				{
					enumerable = this.GenerateKeywordStatsQueryBatches(policy, aggregator, input.Criteria);
				}
				SearchCompletedCallback searchCallback = delegate(ISearchMailboxTask task, ISearchTaskResult result)
				{
					aggregator.MergeSearchResult(result);
				};
				record.Attributes["MBXCNT"] = list.Count;
				while (list.Count > 0)
				{
					Recorder.Trace(5L, TraceType.InfoTrace, "LocalSearchResultsProvider.Search UnsearchedSources:", list.Count);
					HashSet<Guid> hashSet = new HashSet<Guid>();
					MailboxInfoList mailboxInfoList = new MailboxInfoList();
					int i = 0;
					while (i < list.Count)
					{
						SearchSource searchSource = list[i];
						Guid item = searchSource.MailboxInfo.IsArchive ? searchSource.MailboxInfo.ArchiveGuid : searchSource.MailboxInfo.MailboxGuid;
						if (!hashSet.Contains(item))
						{
							mailboxInfoList.Add(searchSource.MailboxInfo);
							list.RemoveAt(i);
							hashSet.Add(item);
							databaseGuid = (searchSource.MailboxInfo.IsArchive ? searchSource.MailboxInfo.ArchiveDatabase : searchSource.MailboxInfo.MdbGuid);
						}
						else
						{
							i++;
						}
					}
					Recorder.Trace(5L, TraceType.InfoTrace, "LocalSearchResultsProvider.Search NonDuplicateSourcesToSearch:", mailboxInfoList.Count);
					AggregatedMailboxSearchTask aggregatedMailboxSearchTask;
					if (input.SearchType == SearchType.Statistics)
					{
						Recorder.Trace(5L, TraceType.InfoTrace, "LocalSearchResultsProvider.Search Statistics:", enumerable);
						using (IEnumerator<List<string>> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								List<string> keywordList = enumerator.Current;
								AggregatedMailboxSearchTask aggregatedMailboxSearchTask2;
								aggregatedMailboxSearchTask = (aggregatedMailboxSearchTask2 = new AggregatedMailboxSearchTask(databaseGuid, mailboxInfoList, input.Criteria, input.PagingInfo, keywordList, input.CallerInfo));
								try
								{
									aggregatedMailboxSearchTask.Execute(searchCallback);
									this.UpdateSearchStatistics(policy, record, aggregatedMailboxSearchTask);
								}
								finally
								{
									if (aggregatedMailboxSearchTask2 != null)
									{
										((IDisposable)aggregatedMailboxSearchTask2).Dispose();
									}
								}
							}
							continue;
						}
					}
					Recorder.Trace(5L, TraceType.InfoTrace, "LocalSearchResultsProvider.Search Regular");
					AggregatedMailboxSearchTask aggregatedMailboxSearchTask3;
					aggregatedMailboxSearchTask = (aggregatedMailboxSearchTask3 = new AggregatedMailboxSearchTask(databaseGuid, mailboxInfoList, input.SearchType, input.Criteria, input.PagingInfo, input.CallerInfo));
					try
					{
						aggregatedMailboxSearchTask.Execute(searchCallback);
						this.UpdateSearchStatistics(policy, record, aggregatedMailboxSearchTask);
					}
					finally
					{
						if (aggregatedMailboxSearchTask3 != null)
						{
							((IDisposable)aggregatedMailboxSearchTask3).Dispose();
						}
					}
				}
			}
			finally
			{
				policy.Recorder.End(record);
			}
			return new SearchMailboxesResults(input.Sources)
			{
				SearchResult = aggregator
			};
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000FD80 File Offset: 0x0000DF80
		private IEnumerable<List<string>> GenerateKeywordStatsQueryBatches(ISearchPolicy policy, ResultAggregator aggregator, SearchCriteria criteria)
		{
			List<string> list = new List<string>
			{
				criteria.QueryString
			};
			aggregator.KeywordStatistics.Add(criteria.QueryString, new KeywordHit(criteria.QueryString, 0UL, ByteQuantifiedSize.Zero));
			if (criteria.SubFilters != null)
			{
				foreach (string text in criteria.SubFilters.Keys)
				{
					aggregator.KeywordStatistics.Add(text, new KeywordHit(text, 0UL, ByteQuantifiedSize.Zero));
				}
				list.AddRange(criteria.SubFilters.Keys);
			}
			return Util.PartitionInSetsOf<string>(list, 5);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000FE44 File Offset: 0x0000E044
		private void UpdateSearchStatistics(ISearchPolicy policy, Recorder.Record currentRecord, AggregatedMailboxSearchTask task)
		{
			currentRecord.Attributes["FAST"] = (this.FastTime += task.FastTime);
			currentRecord.Attributes["STORE"] = (this.StoreTime += task.StoreTime);
			currentRecord.Attributes["REST"] = (this.RestrictionTime += task.RestrictionTime);
			currentRecord.Attributes["TOTALSIZE"] = (this.TotalSize += task.TotalSize);
			currentRecord.Attributes["TOTALCNT"] = (this.TotalItems += task.TotalItems);
			currentRecord.Attributes["RTNSIZE"] = (this.ReturnedStoreSize += task.ReturnedStoreSize);
			currentRecord.Attributes["RTNSTORE"] = (this.ReturnedStoreItems += task.ReturnedStoreItems);
			currentRecord.Attributes["RTNFAST"] = (this.ReturnedFastItems += task.ReturnedFastItems);
			if (task.SearchStatistics != null && task.SearchStatistics.Count > 0)
			{
				string description = string.Format("{0}Mailboxes", currentRecord.Description);
				Recorder.Record record = policy.Recorder.Start(description, TraceType.InfoTrace, false);
				foreach (string text in task.SearchStatistics.Keys)
				{
					Dictionary<string, string> dictionary = task.SearchStatistics[text];
					foreach (string text2 in dictionary.Keys)
					{
						record.Attributes[string.Format("{0}-{1}", text, text2)] = dictionary[text2];
					}
				}
				policy.Recorder.End(record);
			}
		}
	}
}
