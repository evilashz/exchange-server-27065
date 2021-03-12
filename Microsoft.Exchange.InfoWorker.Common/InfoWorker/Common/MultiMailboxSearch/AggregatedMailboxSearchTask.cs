using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001B8 RID: 440
	internal class AggregatedMailboxSearchTask : DisposeTrackableBase, ISearchMailboxTask
	{
		// Token: 0x06000BBF RID: 3007 RVA: 0x00033B6C File Offset: 0x00031D6C
		public AggregatedMailboxSearchTask(Guid databaseGuid, MailboxInfoList mailboxesToSearch, SearchCriteria criteria, PagingInfo pagingInfo, List<string> keywordList, CallerInfo executingUser) : this(databaseGuid, mailboxesToSearch, SearchType.Statistics, criteria, pagingInfo, executingUser)
		{
			Util.ThrowOnNull(keywordList, "keywordList");
			if (keywordList.Count == 0)
			{
				throw new ArgumentException("AggregatedMailboxSearchTask: The keyword list for Stats search cannot be an empty list.");
			}
			this.keywordList = keywordList;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00033BA4 File Offset: 0x00031DA4
		public AggregatedMailboxSearchTask(Guid databaseGuid, MailboxInfoList mailboxesToSearch, SearchType type, SearchCriteria criteria, PagingInfo pagingInfo, CallerInfo executingUser)
		{
			Util.ThrowOnNull(databaseGuid, "databaseGuid");
			if (databaseGuid.Equals(Guid.Empty))
			{
				throw new ArgumentNullException("databaseGuid");
			}
			Util.ThrowOnNull(mailboxesToSearch, "mailboxesToSearch");
			Util.ThrowOnNull(criteria, "criteria");
			Util.ThrowOnNull(pagingInfo, "pagingInfo");
			Util.ThrowOnNull(executingUser, "executingUser");
			if ((type & (SearchType)(-4)) != (SearchType)0 || (type & SearchType.ExpandSources) == SearchType.ExpandSources)
			{
				throw new ArgumentException("AggregatedMailboxSearchTask: the task can either be a preview task or a statistics task");
			}
			if ((type & SearchType.ExpandSources) == SearchType.ExpandSources)
			{
				throw new ArgumentException("AggregatedMailboxSearchTask: the task can either be a preview task or a statistics task");
			}
			this.mailboxesToSearch = mailboxesToSearch;
			this.mailboxDatabaseId = databaseGuid;
			this.type = type;
			this.criteria = criteria;
			this.pagingInfo = pagingInfo;
			this.executingUser = executingUser;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00033C64 File Offset: 0x00031E64
		internal PagingInfo PagingInfo
		{
			get
			{
				return this.pagingInfo;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00033C6C File Offset: 0x00031E6C
		internal CallerInfo ExecutingUserIdentity
		{
			get
			{
				return this.executingUser;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00033C74 File Offset: 0x00031E74
		internal SearchCriteria SearchCriteria
		{
			get
			{
				return this.criteria;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00033C7C File Offset: 0x00031E7C
		internal MailboxInfoList MailboxInfoList
		{
			get
			{
				return this.mailboxesToSearch;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00033C84 File Offset: 0x00031E84
		public MailboxInfo Mailbox
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00033C87 File Offset: 0x00031E87
		public SearchType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00033C8F File Offset: 0x00031E8F
		internal virtual MultiMailboxSearchClient RpcClient
		{
			get
			{
				return this.rpcSearchClient;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00033C97 File Offset: 0x00031E97
		internal Guid MailboxDatabaseGuid
		{
			get
			{
				return this.mailboxDatabaseId;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00033C9F File Offset: 0x00031E9F
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00033CA7 File Offset: 0x00031EA7
		public Dictionary<string, Dictionary<string, string>> SearchStatistics { get; private set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00033CB0 File Offset: 0x00031EB0
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x00033CB8 File Offset: 0x00031EB8
		public long FastTime { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00033CC1 File Offset: 0x00031EC1
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x00033CC9 File Offset: 0x00031EC9
		public long StoreTime { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00033CD2 File Offset: 0x00031ED2
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x00033CDA File Offset: 0x00031EDA
		public long RestrictionTime { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00033CE3 File Offset: 0x00031EE3
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00033CEB File Offset: 0x00031EEB
		public long TotalItems { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00033CF4 File Offset: 0x00031EF4
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x00033CFC File Offset: 0x00031EFC
		public long TotalSize { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00033D05 File Offset: 0x00031F05
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x00033D0D File Offset: 0x00031F0D
		public long ReturnedFastItems { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00033D16 File Offset: 0x00031F16
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00033D1E File Offset: 0x00031F1E
		public long ReturnedStoreItems { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00033D27 File Offset: 0x00031F27
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00033D2F File Offset: 0x00031F2F
		public long ReturnedStoreSize { get; set; }

		// Token: 0x06000BDB RID: 3035 RVA: 0x00033F30 File Offset: 0x00032130
		public void Execute(SearchCompletedCallback searchCallback)
		{
			if (searchCallback == null)
			{
				throw new ArgumentNullException("searchCallback");
			}
			this.InvokeMethodAndHandleExceptions(delegate
			{
				this.attempts++;
				this.callback = searchCallback;
				AggregatedSearchTaskResult result = null;
				this.InitRpcSearchClient();
				Factory.Current.LocalTaskTracer.TraceInformation(this.GetHashCode(), 0L, "Correlation Id:{0}. Executing {1} search for the queryFilter:{2} on {3} mailboxes in database:{4}", new object[]
				{
					this.ExecutingUserIdentity.QueryCorrelationId,
					(this.RpcClient.Criteria.SearchType == SearchType.Preview) ? "preview" : "keyword stats",
					this.RpcClient.Criteria.QueryString,
					this.MailboxInfoList.Count,
					this.RpcClient.MailboxDatabaseGuid
				});
				if ((this.type & SearchType.Preview) == SearchType.Preview)
				{
					result = this.RpcClient.Search(Factory.Current.GetMaxRefinerResults(this.SearchCriteria.RecipientSession));
				}
				else if ((this.type & SearchType.Statistics) == SearchType.Statistics)
				{
					List<IKeywordHit> keywordHits = this.RpcClient.GetKeywordHits(this.keywordList);
					IKeywordHit keywordHit;
					if (keywordHits == null)
					{
						keywordHit = null;
					}
					else
					{
						keywordHit = keywordHits.Find((IKeywordHit x) => x.Phrase.Equals(this.SearchCriteria.QueryString, StringComparison.OrdinalIgnoreCase));
					}
					IKeywordHit keywordHit2 = keywordHit;
					result = new AggregatedSearchTaskResult(this.MailboxInfoList, keywordHits, (keywordHit2 != null) ? keywordHit2.Count : 0UL, (keywordHit2 != null) ? keywordHit2.Size : ByteQuantifiedSize.Zero);
				}
				this.UpdateSearchStatistics();
				this.callback(this, result);
			});
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00033F78 File Offset: 0x00032178
		public bool ShouldRetry(ISearchTaskResult taskResult)
		{
			AggregatedSearchTaskResult aggregatedSearchTaskResult = taskResult as AggregatedSearchTaskResult;
			if (aggregatedSearchTaskResult == null)
			{
				throw new ArgumentNullException("result");
			}
			if (this.type != aggregatedSearchTaskResult.ResultType)
			{
				throw new ArgumentException("result types don't match");
			}
			if (aggregatedSearchTaskResult.Success)
			{
				return false;
			}
			Exception exception = aggregatedSearchTaskResult.Exception;
			return this.attempts <= 3 && exception != null && (exception is DiscoverySearchMaxSearchesExceeded || exception is DiscoveryKeywordStatsSearchTimedOut || exception is DiscoverySearchTimedOut || exception is StorageTransientException || exception is ADTransientException || exception is SearchTransientException);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00034004 File Offset: 0x00032204
		public ISearchTaskResult GetErrorResult(Exception ex)
		{
			if ((this.type & SearchType.Statistics) == SearchType.Statistics)
			{
				List<IKeywordHit> list = new List<IKeywordHit>(1);
				List<string> list2 = new List<string>(1);
				list2.Add(this.criteria.QueryString);
				if (this.criteria.SubFilters != null)
				{
					list2.AddRange(this.criteria.SubFilters.Keys);
				}
				foreach (string phrase in list2)
				{
					IKeywordHit keywordHit = null;
					foreach (MailboxInfo mailbox in this.MailboxInfoList)
					{
						if (keywordHit == null)
						{
							keywordHit = new KeywordHit(phrase, mailbox, ex);
						}
						else
						{
							keywordHit.Merge(new KeywordHit(phrase, mailbox, ex));
						}
					}
					list.Add(keywordHit);
				}
				return new AggregatedSearchTaskResult(this.MailboxInfoList, list);
			}
			return new AggregatedSearchTaskResult(this.MailboxInfoList, ex);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0003411C File Offset: 0x0003231C
		public void Abort()
		{
			if (this.rpcSearchClient != null)
			{
				this.rpcSearchClient.Abort();
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00034131 File Offset: 0x00032331
		internal virtual void InitRpcSearchClient()
		{
			if (this.rpcSearchClient == null)
			{
				this.rpcSearchClient = Factory.Current.CreateSearchRpcClient(this.mailboxDatabaseId, this.MailboxInfoList.ToArray(), this.criteria, this.executingUser, this.pagingInfo);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003416E File Offset: 0x0003236E
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.rpcSearchClient != null)
			{
				this.rpcSearchClient.Dispose();
				this.rpcSearchClient = null;
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003418D File Offset: 0x0003238D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AggregatedMailboxSearchTask>(this);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000343F0 File Offset: 0x000325F0
		protected void InvokeMethodAndHandleExceptions(Util.MethodDelegate method)
		{
			Util.HandleExceptions(delegate
			{
				Exception ex = null;
				try
				{
					method();
				}
				catch (DiscoverySearchPermanentException ex2)
				{
					Factory.Current.LocalTaskTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Aggregated Search task failed. DiscoverySearchPermanentException: {1}", this.ExecutingUserIdentity.QueryCorrelationId, ex2.ToString());
					ex = ex2;
				}
				catch (MultiMailboxSearchException ex3)
				{
					Factory.Current.LocalTaskTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Aggregated Search task failed. MultiMailboxSearchException: {1}", this.ExecutingUserIdentity.QueryCorrelationId, ex3.ToString());
					ex = ex3;
				}
				catch (StoragePermanentException ex4)
				{
					Factory.Current.LocalTaskTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Aggregated Search task failed. StorageTransientException: {1}", this.ExecutingUserIdentity.QueryCorrelationId, ex4.ToString());
					ex = ex4;
				}
				catch (StorageTransientException ex5)
				{
					Factory.Current.LocalTaskTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Aggregated Search task failed. StoragePermanentException: {1}", this.ExecutingUserIdentity.QueryCorrelationId, ex5.ToString());
					ex = ex5;
				}
				catch (ADTransientException ex6)
				{
					Factory.Current.LocalTaskTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Aggregated Search task failed. ADTransientException: {1}", this.ExecutingUserIdentity.QueryCorrelationId, ex6.ToString());
					ex = ex6;
				}
				finally
				{
					if (ex != null)
					{
						AggregatedSearchTaskResult result = (AggregatedSearchTaskResult)this.GetErrorResult(ex);
						this.callback(this, result);
					}
				}
			}, delegate(GrayException ex)
			{
				AggregatedSearchTaskResult result = (AggregatedSearchTaskResult)this.GetErrorResult((ex.InnerException != null) ? ex.InnerException : ex);
				this.callback(this, result);
			});
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00034430 File Offset: 0x00032630
		private void UpdateSearchStatistics()
		{
			MultiMailboxSearchClient rpcClient = this.RpcClient;
			if (rpcClient != null)
			{
				this.SearchStatistics = rpcClient.SearchStatistics;
				this.FastTime = rpcClient.FastTime;
				this.StoreTime = rpcClient.StoreTime;
				this.RestrictionTime = rpcClient.RestrictionTime;
				this.TotalItems = rpcClient.TotalItems;
				this.TotalSize = rpcClient.TotalSize;
				this.ReturnedFastItems = rpcClient.ReturnedFastItems;
				this.ReturnedStoreItems = rpcClient.ReturnedStoreItems;
				this.ReturnedStoreSize = rpcClient.ReturnedStoreSize;
			}
		}

		// Token: 0x040008C7 RID: 2247
		private const int MaxRetryCount = 3;

		// Token: 0x040008C8 RID: 2248
		private readonly MailboxInfoList mailboxesToSearch;

		// Token: 0x040008C9 RID: 2249
		private readonly SearchType type;

		// Token: 0x040008CA RID: 2250
		private readonly SearchCriteria criteria;

		// Token: 0x040008CB RID: 2251
		private readonly PagingInfo pagingInfo;

		// Token: 0x040008CC RID: 2252
		private readonly CallerInfo executingUser;

		// Token: 0x040008CD RID: 2253
		private readonly List<string> keywordList;

		// Token: 0x040008CE RID: 2254
		private int attempts;

		// Token: 0x040008CF RID: 2255
		private readonly Guid mailboxDatabaseId;

		// Token: 0x040008D0 RID: 2256
		private MultiMailboxSearchClient rpcSearchClient;

		// Token: 0x040008D1 RID: 2257
		private SearchCompletedCallback callback;
	}
}
