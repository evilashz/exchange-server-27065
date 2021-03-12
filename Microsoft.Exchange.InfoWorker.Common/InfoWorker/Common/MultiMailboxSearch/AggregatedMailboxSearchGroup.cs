using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001B6 RID: 438
	internal class AggregatedMailboxSearchGroup : MailboxSearchGroup
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00032BB4 File Offset: 0x00030DB4
		public AggregatedMailboxSearchGroup(MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser) : base(mailboxes, searchCriteria, pagingInfo, executingUser)
		{
			Util.ThrowOnNull(mailboxes, "mailboxes");
			Util.ThrowOnNull(searchCriteria, "searchCriteria");
			Util.ThrowOnNull(pagingInfo, "pagingInfo");
			Util.ThrowOnNull(executingUser, "executingUser");
			this.totalTasks = 0;
			Factory.Current.LocalTaskTracer.TraceInformation<int, string>(this.GetHashCode(), 0L, "Grouping the mailboxes:{0} by database for the query:{1}.", base.Mailboxes.Length, base.SearchCriteria.QueryString);
			Dictionary<Guid, List<MailboxInfo>> dictionary = Util.GroupMailboxByDatabase(base.Mailboxes);
			base.ResultAggregator.ProtocolLog.Add("NumberOfLocalSearch", dictionary.Count);
			PerformanceCounters.AverageDatabaseSearchedPerServer.IncrementBy((long)dictionary.Count);
			PerformanceCounters.AverageDatabaseSearchedPerServerBase.Increment();
			IEnumerable<List<string>> keywordStatsEnumerator = base.SearchCriteria.IsStatisticsSearch ? this.GenerateKeywordStatsQueryBatches(dictionary) : null;
			Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Creating Search task for query:{1} on mailboxes:{2} on databases:{3}", new object[]
			{
				base.ExecutingUser.QueryCorrelationId,
				base.SearchCriteria.QueryString,
				base.Mailboxes.Length,
				dictionary.Count
			});
			this.totalTasks = this.CreateSearchTasks(dictionary, keywordStatsEnumerator);
			Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Queued {1} tasks for query:{2} on mailboxes:{3} spanning on databases:{4}", new object[]
			{
				base.ExecutingUser.QueryCorrelationId,
				this.totalTasks,
				base.SearchCriteria.QueryString,
				base.Mailboxes.Length,
				dictionary.Count
			});
			this.maxNumberOfTasks = Factory.Current.GetMaximumThreadsForLocalSearch(this.totalTasks, base.SearchCriteria.RecipientSession);
			PerformanceCounters.TotalLocalSearchesInProgress.Increment();
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00032DC0 File Offset: 0x00030FC0
		internal int TotalTasks
		{
			get
			{
				return this.totalTasks;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00032DC8 File Offset: 0x00030FC8
		internal int CompletedTasks
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00032DD0 File Offset: 0x00030FD0
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x00032DD8 File Offset: 0x00030FD8
		protected int MaxNumberOfTasks
		{
			get
			{
				return this.maxNumberOfTasks;
			}
			set
			{
				this.maxNumberOfTasks = value;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00032DE4 File Offset: 0x00030FE4
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (disposing)
			{
				if (this.scheduledTasks != null)
				{
					foreach (ISearchMailboxTask searchMailboxTask in this.scheduledTasks)
					{
						if (searchMailboxTask != null)
						{
							((IDisposeTrackable)searchMailboxTask).Dispose();
						}
					}
					this.scheduledTasks.Clear();
				}
				if (this.queue != null)
				{
					foreach (ISearchMailboxTask searchMailboxTask2 in this.queue)
					{
						if (searchMailboxTask2 != null)
						{
							((IDisposeTrackable)searchMailboxTask2).Dispose();
						}
					}
					this.queue.Clear();
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00032EBC File Offset: 0x000310BC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AggregatedMailboxSearchGroup>(this);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00032EC4 File Offset: 0x000310C4
		protected override void ExecuteSearch()
		{
			lock (this.locker)
			{
				if (this.totalTasks == 0)
				{
					this.ReportCompletion();
				}
				this.Dispatch();
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00032F14 File Offset: 0x00031114
		protected override void ReportCompletion()
		{
			PerformanceCounters.TotalLocalSearchesInProgress.Decrement();
			base.ReportCompletion();
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00032F28 File Offset: 0x00031128
		protected override void StopSearch()
		{
			Factory.Current.LocalTaskTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Correlation Id:{0}. Search was cancelled", base.ExecutingUser.QueryCorrelationId);
			lock (this.locker)
			{
				if (this.completed != this.totalTasks)
				{
					this.cancelled = true;
					foreach (ISearchMailboxTask searchMailboxTask in this.queue)
					{
						Factory.Current.LocalTaskTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. Removing {1} Search task on {2} mailboxes on {3} database from the queue", new object[]
						{
							base.ExecutingUser.QueryCorrelationId,
							(searchMailboxTask.Type == SearchType.Preview) ? "Preview" : "Statistics",
							((AggregatedMailboxSearchTask)searchMailboxTask).MailboxInfoList.Count,
							((AggregatedMailboxSearchTask)searchMailboxTask).MailboxDatabaseGuid
						});
						AggregatedMailboxSearchTask aggregatedMailboxSearchTask = searchMailboxTask as AggregatedMailboxSearchTask;
						base.ResultAggregator.MergeSearchResult((AggregatedSearchTaskResult)searchMailboxTask.GetErrorResult(new DiscoverySearchTaskCancelled(aggregatedMailboxSearchTask.MailboxInfoList, aggregatedMailboxSearchTask.MailboxDatabaseGuid)));
						this.completed++;
					}
					foreach (ISearchMailboxTask searchMailboxTask2 in this.scheduledTasks)
					{
						Factory.Current.LocalTaskTracer.TraceDebug<Guid, string, int>((long)this.GetHashCode(), "Correlation Id:{0}. Cancelling {1} Search task on {2} mailboxes. Merging error with the result.", base.ExecutingUser.QueryCorrelationId, (searchMailboxTask2.Type == SearchType.Preview) ? "Preview" : "Statistics", ((AggregatedMailboxSearchTask)searchMailboxTask2).MailboxInfoList.Count);
						AggregatedMailboxSearchTask aggregatedMailboxSearchTask2 = searchMailboxTask2 as AggregatedMailboxSearchTask;
						aggregatedMailboxSearchTask2.Abort();
						base.ResultAggregator.MergeSearchResult((AggregatedSearchTaskResult)searchMailboxTask2.GetErrorResult(new DiscoverySearchTaskCancelled(aggregatedMailboxSearchTask2.MailboxInfoList, aggregatedMailboxSearchTask2.MailboxDatabaseGuid)));
						this.completed++;
					}
					this.ReportCompletion();
				}
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003319C File Offset: 0x0003139C
		private int CreateSearchTasks(Dictionary<Guid, List<MailboxInfo>> mailboxGroup, IEnumerable<List<string>> keywordStatsEnumerator)
		{
			int num = 0;
			foreach (Guid guid in mailboxGroup.Keys)
			{
				MailboxInfoList mailboxInfoList = Factory.Current.CreateMailboxInfoList(mailboxGroup[guid].ToArray());
				PerformanceCounters.AverageMailboxSearchedPerDatabase.IncrementBy((long)mailboxInfoList.Count);
				PerformanceCounters.AverageMailboxSearchedPerDatabaseBase.Increment();
				Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Creating {1} search task for query:{2} on mailboxes:{3} on database:{4}", new object[]
				{
					base.ExecutingUser.QueryCorrelationId,
					base.SearchCriteria.IsPreviewSearch ? "preview" : "statistics",
					base.SearchCriteria.QueryString,
					mailboxInfoList.Count,
					guid
				});
				if (base.SearchCriteria.IsPreviewSearch)
				{
					num++;
					this.queue.Enqueue(Factory.Current.CreateAggregatedMailboxSearchTask(guid, mailboxInfoList, SearchType.Preview, base.SearchCriteria, base.PagingInfo, base.ExecutingUser));
				}
				if (base.SearchCriteria.IsStatisticsSearch && keywordStatsEnumerator != null)
				{
					int num2 = 0;
					foreach (List<string> list in keywordStatsEnumerator)
					{
						Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Create statistics search task batch:{1} with {2} keywords on database:{3}.", new object[]
						{
							base.ExecutingUser.QueryCorrelationId,
							++num2,
							list.Count,
							guid
						});
						num++;
						this.queue.Enqueue(Factory.Current.CreateAggregatedMailboxSearchTask(guid, mailboxInfoList, base.SearchCriteria, base.PagingInfo, list, base.ExecutingUser));
					}
				}
			}
			return num;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000333E0 File Offset: 0x000315E0
		private IEnumerable<List<string>> GenerateKeywordStatsQueryBatches(Dictionary<Guid, List<MailboxInfo>> mailboxGroup)
		{
			List<string> list = new List<string>
			{
				base.SearchCriteria.QueryString
			};
			base.ResultAggregator.KeywordStatistics.Add(base.SearchCriteria.QueryString, new KeywordHit(base.SearchCriteria.QueryString, 0UL, ByteQuantifiedSize.Zero));
			if (base.SearchCriteria.SubFilters != null)
			{
				foreach (string text in base.SearchCriteria.SubFilters.Keys)
				{
					if (!base.ResultAggregator.KeywordStatistics.ContainsKey(text))
					{
						base.ResultAggregator.KeywordStatistics.Add(text, new KeywordHit(text, 0UL, ByteQuantifiedSize.Zero));
						list.Add(text);
					}
				}
			}
			IEnumerable<List<string>> enumerable = Util.PartitionInSetsOf<string>(list, 5);
			Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Statistics search task for query:{1} on mailboxes:{2} across {3} databases, resulted in {4} batches of batch size {5}", new object[]
			{
				base.ExecutingUser.QueryCorrelationId,
				base.SearchCriteria.QueryString,
				base.Mailboxes.Length,
				mailboxGroup.Count,
				enumerable.Count<List<string>>(),
				5
			});
			return enumerable;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00033550 File Offset: 0x00031750
		private void Dispatch()
		{
			while (this.tasksInProgress < this.maxNumberOfTasks)
			{
				if (this.queue.Count == 0)
				{
					return;
				}
				ISearchMailboxTask searchMailboxTask = this.queue.Dequeue();
				AggregatedMailboxSearchTask aggregatedMailboxSearchTask = searchMailboxTask as AggregatedMailboxSearchTask;
				this.tasksInProgress++;
				this.scheduledTasks.Add(searchMailboxTask);
				Factory.Current.LocalTaskTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. Queuing {1} Search on {2} mailboxes for query of length {3} with {4} keywords.", new object[]
				{
					base.ExecutingUser.QueryCorrelationId,
					(searchMailboxTask.Type == SearchType.Preview) ? "Preview" : "Statistics",
					aggregatedMailboxSearchTask.MailboxInfoList.Count,
					string.IsNullOrEmpty(aggregatedMailboxSearchTask.SearchCriteria.QueryString) ? 0 : aggregatedMailboxSearchTask.SearchCriteria.QueryString.Trim().Length,
					(aggregatedMailboxSearchTask.SearchCriteria.SubFilters != null && aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count > 0) ? aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count : 0
				});
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Worker), aggregatedMailboxSearchTask);
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003369C File Offset: 0x0003189C
		private void Worker(object state)
		{
			AggregatedMailboxSearchTask aggregatedMailboxSearchTask = (AggregatedMailboxSearchTask)state;
			Factory.Current.LocalTaskTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. Initiating {1} Search on {2} mailboxes for query of length {3} with {4} keywords.", new object[]
			{
				base.ExecutingUser.QueryCorrelationId,
				(aggregatedMailboxSearchTask.Type == SearchType.Preview) ? "Preview" : "Statistics",
				aggregatedMailboxSearchTask.MailboxInfoList.Count,
				string.IsNullOrEmpty(aggregatedMailboxSearchTask.SearchCriteria.QueryString) ? 0 : aggregatedMailboxSearchTask.SearchCriteria.QueryString.Trim().Length,
				(aggregatedMailboxSearchTask.SearchCriteria.SubFilters != null && aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count > 0) ? aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count : 0
			});
			aggregatedMailboxSearchTask.Execute(new SearchCompletedCallback(this.SearchCompletedCallback));
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003379C File Offset: 0x0003199C
		private void SearchCompletedCallback(ISearchMailboxTask task, ISearchTaskResult result)
		{
			AggregatedMailboxSearchTask aggregatedMailboxSearchTask = task as AggregatedMailboxSearchTask;
			lock (this.locker)
			{
				if (!this.cancelled)
				{
					this.HandleSearchCompletion(task, result);
				}
				else
				{
					Factory.Current.LocalTaskTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. {1} Search completed on {2} mailboxes, with query of length {3} with {4} keywords. But not merging the results because search was cancelled.", new object[]
					{
						base.ExecutingUser.QueryCorrelationId,
						(task.Type == SearchType.Preview) ? "Preview" : "Statistics",
						aggregatedMailboxSearchTask.MailboxInfoList.Count,
						string.IsNullOrEmpty(aggregatedMailboxSearchTask.SearchCriteria.QueryString) ? 0 : aggregatedMailboxSearchTask.SearchCriteria.QueryString.Trim().Length,
						(aggregatedMailboxSearchTask.SearchCriteria.SubFilters != null && aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count > 0) ? aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count : 0
					});
				}
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000338CC File Offset: 0x00031ACC
		private void HandleSearchCompletion(ISearchMailboxTask task, ISearchTaskResult result)
		{
			AggregatedMailboxSearchTask aggregatedMailboxSearchTask = task as AggregatedMailboxSearchTask;
			lock (this.locker)
			{
				Factory.Current.LocalTaskTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. {1} Search completed on {2} mailboxes, with query of length {3} with {4} keywords.The result was {5}", new object[]
				{
					base.ExecutingUser.QueryCorrelationId,
					(task.Type == SearchType.Preview) ? "Preview" : "Statistics",
					aggregatedMailboxSearchTask.MailboxInfoList.Count,
					string.IsNullOrEmpty(aggregatedMailboxSearchTask.SearchCriteria.QueryString) ? 0 : aggregatedMailboxSearchTask.SearchCriteria.QueryString.Trim().Length,
					(aggregatedMailboxSearchTask.SearchCriteria.SubFilters != null && aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count > 0) ? aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count : 0,
					(result != null && result.Success) ? "Success" : "Failure"
				});
				this.tasksInProgress--;
				this.scheduledTasks.Remove(task);
				if (result != null && task.ShouldRetry(result))
				{
					Factory.Current.LocalTaskTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. Retrying {1} Search completed on {2} mailboxes, with query of length {3} with {4} keywords.", new object[]
					{
						base.ExecutingUser.QueryCorrelationId,
						(task.Type == SearchType.Preview) ? "Preview" : "Statistics",
						aggregatedMailboxSearchTask.MailboxInfoList.Count,
						string.IsNullOrEmpty(aggregatedMailboxSearchTask.SearchCriteria.QueryString) ? 0 : aggregatedMailboxSearchTask.SearchCriteria.QueryString.Trim().Length,
						(aggregatedMailboxSearchTask.SearchCriteria.SubFilters != null && aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count > 0) ? aggregatedMailboxSearchTask.SearchCriteria.SubFilters.Keys.Count : 0
					});
					this.queue.Enqueue(task);
				}
				else
				{
					this.completed++;
					if (result != null)
					{
						base.ResultAggregator.MergeSearchResult(result);
					}
					aggregatedMailboxSearchTask.Dispose();
					if (this.completed == this.totalTasks)
					{
						this.ReportCompletion();
					}
				}
				if (!this.cancelled)
				{
					this.Dispatch();
				}
			}
		}

		// Token: 0x040008BF RID: 2239
		private readonly int totalTasks;

		// Token: 0x040008C0 RID: 2240
		private readonly Queue<ISearchMailboxTask> queue = new Queue<ISearchMailboxTask>();

		// Token: 0x040008C1 RID: 2241
		private readonly List<ISearchMailboxTask> scheduledTasks = new List<ISearchMailboxTask>(4);

		// Token: 0x040008C2 RID: 2242
		private int tasksInProgress;

		// Token: 0x040008C3 RID: 2243
		private int maxNumberOfTasks;

		// Token: 0x040008C4 RID: 2244
		private bool cancelled;

		// Token: 0x040008C5 RID: 2245
		private int completed;

		// Token: 0x040008C6 RID: 2246
		private readonly object locker = new object();
	}
}
