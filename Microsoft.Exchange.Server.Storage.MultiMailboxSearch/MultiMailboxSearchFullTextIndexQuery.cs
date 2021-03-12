using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MultiMailboxSearch;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.MultiMailboxSearch
{
	// Token: 0x02000006 RID: 6
	internal class MultiMailboxSearchFullTextIndexQuery : IMultiMailboxSearchFullTextIndexQuery
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000318B File Offset: 0x0000138B
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00003193 File Offset: 0x00001393
		public List<string> RefinersList { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000319C File Offset: 0x0000139C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000031A4 File Offset: 0x000013A4
		public List<string> ExtraFieldsList { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000031AD File Offset: 0x000013AD
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000031B5 File Offset: 0x000013B5
		public Guid QueryCorrelationId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000031C0 File Offset: 0x000013C0
		internal static string MailboxGuidFastPropertyName
		{
			get
			{
				if (string.IsNullOrEmpty(MultiMailboxSearchFullTextIndexQuery.mailboxGuidFastPropertyName))
				{
					ExTraceGlobals.FullTextIndexTracer.TraceInformation(60988, 0L, "Getting the Fast Property Name for the MailboxGuid Store Property.");
					FullTextIndexSchema.FullTextIndexInfo fullTextIndexInfo = null;
					FullTextIndexSchema.Current.IsPropertyInFullTextIndex(PropTag.Message.MailboxGuid.PropInfo.PropName, Guid.Empty, out fullTextIndexInfo);
					ExTraceGlobals.FullTextIndexTracer.TraceInformation<string>(36412, 0L, "The Fast Property Name for the MailboxGuid Store Property is {0}", fullTextIndexInfo.FastPropertyName);
					MultiMailboxSearchFullTextIndexQuery.mailboxGuidFastPropertyName = fullTextIndexInfo.FastPropertyName;
				}
				return MultiMailboxSearchFullTextIndexQuery.mailboxGuidFastPropertyName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003244 File Offset: 0x00001444
		internal static string SizeFastPropertyName
		{
			get
			{
				if (string.IsNullOrEmpty(MultiMailboxSearchFullTextIndexQuery.sizeFastPropertyName))
				{
					ExTraceGlobals.FullTextIndexTracer.TraceInformation(52112, 0L, "Getting the Fast Property Name for the Size Store Property.");
					FullTextIndexSchema.FullTextIndexInfo fullTextIndexInfo = null;
					FullTextIndexSchema.Current.IsPropertyInFullTextIndex(PropTag.Message.MessageSize.PropInfo.PropName, Guid.Empty, out fullTextIndexInfo);
					ExTraceGlobals.FullTextIndexTracer.TraceInformation<string>(45968, 0L, "The Fast Property Name for the Size Store Property is {0}", fullTextIndexInfo.FastPropertyName);
					MultiMailboxSearchFullTextIndexQuery.sizeFastPropertyName = fullTextIndexInfo.FastPropertyName;
				}
				return MultiMailboxSearchFullTextIndexQuery.sizeFastPropertyName;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000032C5 File Offset: 0x000014C5
		protected bool IsAborted
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isAborted, 0, 0) == 1;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000032D7 File Offset: 0x000014D7
		private static SearchConfig SearchConfig
		{
			get
			{
				return SearchConfig.Instance;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000032E0 File Offset: 0x000014E0
		private static Hookable<PagingImsFlowExecutor> ServiceProxyWrapper
		{
			get
			{
				if (MultiMailboxSearchFullTextIndexQuery.serviceProxyWrapperInstance == null)
				{
					string hostName = MultiMailboxSearchFullTextIndexQuery.SearchConfig.HostName;
					int queryServicePort = MultiMailboxSearchFullTextIndexQuery.SearchConfig.QueryServicePort;
					int fastMmsImsChannelPoolSize = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastMmsImsChannelPoolSize;
					TimeSpan fastImsMmsSendTimeout = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastImsMmsSendTimeout;
					TimeSpan fastImsMmsReceiveTimeout = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastImsMmsReceiveTimeout;
					int fastMmsImsRetryCount = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastMmsImsRetryCount;
					long num = (long)MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastIMSMaxReceivedMessageSize;
					int fastIMSMaxStringContentLength = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastIMSMaxStringContentLength;
					TimeSpan fastProxyCacheTimeout = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastProxyCacheTimeout;
					Hookable<PagingImsFlowExecutor> value = Hookable<PagingImsFlowExecutor>.Create(true, new PagingImsFlowExecutor(hostName, queryServicePort, fastMmsImsChannelPoolSize, fastImsMmsSendTimeout, fastImsMmsSendTimeout, fastImsMmsReceiveTimeout, fastProxyCacheTimeout, num, fastIMSMaxStringContentLength, fastMmsImsRetryCount));
					Interlocked.CompareExchange<Hookable<PagingImsFlowExecutor>>(ref MultiMailboxSearchFullTextIndexQuery.serviceProxyWrapperInstance, value, null);
				}
				return MultiMailboxSearchFullTextIndexQuery.serviceProxyWrapperInstance;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003390 File Offset: 0x00001590
		private static int PageSize
		{
			get
			{
				if (MultiMailboxSearchFullTextIndexQuery.pageSize == -1)
				{
					int fastQueryResultTrimHits = MultiMailboxSearchFullTextIndexQuery.SearchConfig.FastQueryResultTrimHits;
					Interlocked.CompareExchange(ref MultiMailboxSearchFullTextIndexQuery.pageSize, fastQueryResultTrimHits, -1);
				}
				return MultiMailboxSearchFullTextIndexQuery.pageSize;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000033C2 File Offset: 0x000015C2
		public static IDisposable SetPagingImsFlowExecutorTestHook(PagingImsFlowExecutor testHook)
		{
			return MultiMailboxSearchFullTextIndexQuery.ServiceProxyWrapper.SetTestHook(testHook);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000033D0 File Offset: 0x000015D0
		public virtual KeywordStatsResultRow ExecuteFullTextKeywordHitsQuery(Guid databaseGuid, Guid mailboxGuid, string query)
		{
			bool flag = ExTraceGlobals.FullTextIndexTracer.IsTraceEnabled(TraceType.PerformanceTrace);
			Stopwatch stopwatch = flag ? Stopwatch.StartNew() : null;
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.ExecuteFullTextKeywordHitsQuery");
			string imsFlowName = FullTextIndexSchema.GetImsFlowName(databaseGuid);
			ExTraceGlobals.FullTextIndexTracer.TraceInformation<Guid, string, Guid>(50748, 0L, "Correlation Id:{0}. Executing flow {1} on database {2}.", this.QueryCorrelationId, imsFlowName, databaseGuid);
			long count;
			Dictionary<string, List<RefinersResultRow>> refinersResult;
			try
			{
				using (PagingImsFlowExecutor.QueryExecutionContext queryExecutionContext = MultiMailboxSearchFullTextIndexQuery.ServiceProxyWrapper.Value.ExecuteWithNoResults(imsFlowName, mailboxGuid, this.QueryCorrelationId, query, CultureInfo.InvariantCulture, this.GenerateParameters(null)))
				{
					this.CheckIsAborted();
					count = MultiMailboxSearchFullTextIndexQuery.ServiceProxyWrapper.Value.ReadHitCount(queryExecutionContext);
					this.CheckIsAborted();
					refinersResult = MultiMailboxSearchFullTextIndexQuery.ReadRefinerInformation(queryExecutionContext);
				}
			}
			finally
			{
				if (flag)
				{
					stopwatch.Stop();
					ExTraceGlobals.FullTextIndexTracer.TracePerformance(0L, "Correlation Id:{0}. FullTextIndexQuery.ExecuteFullTextKeywordHitsQuery for mailbox {1} took {2}ms for query '{3}'", new object[]
					{
						this.QueryCorrelationId,
						mailboxGuid,
						stopwatch.ElapsedMilliseconds,
						query
					});
				}
			}
			double size = MultiMailboxSearchFullTextIndexQuery.FetchSizeFromRefiners(refinersResult, this.QueryCorrelationId);
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.ExecuteFullTextKeywordHitsQuery");
			return new KeywordStatsResultRow(query, count, size);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003514 File Offset: 0x00001714
		public virtual IList<FullTextIndexRow> ExecuteFullTextIndexQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, int maxResults, string sortSpec, out KeywordStatsResultRow keywordStatsResult, out Dictionary<string, List<RefinersResultRow>> refinersOutput)
		{
			bool flag = ExTraceGlobals.FullTextIndexTracer.IsTraceEnabled(TraceType.PerformanceTrace);
			Stopwatch stopwatch = flag ? Stopwatch.StartNew() : null;
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.ExecuteFullTextIndexQuery");
			ExTraceGlobals.FullTextIndexTracer.TraceInformation(62352, 0L, "Correlation Id:{0}. Invoking FullTextIndexQuery.ExecuteFullTextIndexQuery for the following query {1}, with PageSize: {2}, SortCriteria: {3}, MaxHits: {4}", new object[]
			{
				this.QueryCorrelationId,
				query,
				MultiMailboxSearchFullTextIndexQuery.PageSize,
				sortSpec,
				maxResults
			});
			string imsFlowName = FullTextIndexSchema.GetImsFlowName(databaseGuid);
			ExTraceGlobals.FullTextIndexTracer.TraceInformation(58256, 0L, "Correlation Id:{0}. Executing flow {1} on database {2} for mailbox {3}", new object[]
			{
				this.QueryCorrelationId,
				imsFlowName,
				databaseGuid,
				mailboxGuid
			});
			long num = 0L;
			refinersOutput = null;
			keywordStatsResult = null;
			List<FullTextIndexRow> list = new List<FullTextIndexRow>(0);
			bool flag2 = true;
			foreach (KeyValuePair<PagingImsFlowExecutor.QueryExecutionContext, SearchResultItem[]> keyValuePair in MultiMailboxSearchFullTextIndexQuery.ServiceProxyWrapper.Value.Execute(imsFlowName, mailboxGuid, this.QueryCorrelationId, query, 0L, CultureInfo.InvariantCulture, this.GenerateParameters(sortSpec), Math.Min(MultiMailboxSearchFullTextIndexQuery.PageSize, maxResults), null))
			{
				if (flag2)
				{
					flag2 = false;
					this.CheckIsAborted();
					num = MultiMailboxSearchFullTextIndexQuery.ServiceProxyWrapper.Value.ReadHitCount(keyValuePair.Key);
					this.CheckIsAborted();
					refinersOutput = MultiMailboxSearchFullTextIndexQuery.ReadRefinerInformation(keyValuePair.Key);
				}
				MultiMailboxSearchFullTextIndexQuery.ProcessSearchResults(list, keyValuePair.Value, imsFlowName, mailboxGuid, mailboxNumber, maxResults, this.QueryCorrelationId);
				if (list.Count >= maxResults)
				{
					break;
				}
				this.CheckIsAborted();
			}
			double num2 = MultiMailboxSearchFullTextIndexQuery.FetchSizeFromRefiners(refinersOutput, this.QueryCorrelationId);
			keywordStatsResult = new KeywordStatsResultRow(query, num, num2);
			if (flag)
			{
				stopwatch.Stop();
				ExTraceGlobals.FullTextIndexTracer.TracePerformance(0L, "Correlation Id:{0}. FullTextIndexQuery.ExecuteFullTextIndexQuery for mailbox {1} completed in {2}ms for query '{3}'", new object[]
				{
					this.QueryCorrelationId,
					mailboxGuid,
					stopwatch.ElapsedMilliseconds,
					query
				});
			}
			ExTraceGlobals.FullTextIndexTracer.TraceInformation(37776, 0L, "Correlation Id:{0}. FullTextIndexQuery.ExecuteFullTextIndexQuery for the following query {1} yielded {2} results, size: {3} bytes", new object[]
			{
				this.QueryCorrelationId,
				query,
				num,
				num2
			});
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.ExecuteFullTextIndexQuery");
			return list;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003794 File Offset: 0x00001994
		public virtual void Abort()
		{
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.Abort");
			Interlocked.Exchange(ref this.isAborted, 1);
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.Abort");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000037B8 File Offset: 0x000019B8
		private static Dictionary<string, List<RefinersResultRow>> ReadRefinerInformation(PagingImsFlowExecutor.QueryExecutionContext context)
		{
			IReadOnlyCollection<RefinerResult> readOnlyCollection = MultiMailboxSearchFullTextIndexQuery.ServiceProxyWrapper.Value.ReadRefiners(context);
			Dictionary<string, List<RefinersResultRow>> dictionary = new Dictionary<string, List<RefinersResultRow>>(readOnlyCollection.Count);
			foreach (RefinerResult refinerResult in readOnlyCollection)
			{
				string name = refinerResult.Name;
				List<RefinersResultRow> list = new List<RefinersResultRow>(4);
				dictionary.Add(name, list);
				foreach (RefinerEntry refinerEntry in refinerResult.Entries)
				{
					list.Add(RefinersResultRow.NewRow(refinerEntry.Name, refinerEntry.Count, refinerResult.Sum, refinerResult.Min, refinerResult.Max));
				}
			}
			return dictionary;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000038A0 File Offset: 0x00001AA0
		private static void ProcessSearchResults(List<FullTextIndexRow> rows, SearchResultItem[] items, string flowName, Guid mailboxGuid, int mailboxNumber, int maxResults, Guid queryCorrelationId)
		{
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.ProcessSearchResults");
			if (items == null || items.Length <= 0)
			{
				MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.ProcessSearchResults");
				return;
			}
			ExTraceGlobals.FullTextIndexTracer.TraceInformation(47248, 0L, "Correlation Id:{0}. Flow {1} for Mailbox {2}, returned {3} items. Processing them.", new object[]
			{
				queryCorrelationId,
				flowName,
				mailboxGuid,
				items.Length
			});
			ExTraceGlobals.FullTextIndexTracer.TraceInformation<Guid, string>(33936, 0L, "Correlation Id:{0}. Finding the mailboxguid field index in the results for flow {1}", queryCorrelationId, flowName);
			SearchResultItem searchResultItem = items[0];
			rows.Capacity = Math.Min(maxResults, rows.Count + items.Length);
			foreach (SearchResultItem searchResultItem2 in items)
			{
				long num = (long)searchResultItem2.Fields[1].Value;
				int mailboxNumber2 = IndexId.GetMailboxNumber(num);
				if (mailboxNumber == mailboxNumber2)
				{
					rows.Add(FullTextIndexRow.Parse(mailboxGuid, num));
				}
				if (rows.Count == maxResults)
				{
					return;
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003998 File Offset: 0x00001B98
		private static int GetMailboxGuidFieldIndexFromFastResults(SearchResultItem firstItem)
		{
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.GetMailboxGuidFieldIndexFromFastResults");
			int result = -1;
			if (firstItem != null && firstItem.Fields.Count == 3 && firstItem.Fields[2] != null && !string.IsNullOrEmpty(firstItem.Fields[2].Name) && firstItem.Fields[2].Name.Equals("other", StringComparison.OrdinalIgnoreCase))
			{
				SearchResultItem searchResultItem = firstItem.Fields[2].Value as SearchResultItem;
				if (searchResultItem != null && searchResultItem.Fields != null && searchResultItem.Fields.Count > 0)
				{
					for (int i = 0; i < searchResultItem.Fields.Count; i++)
					{
						if (searchResultItem.Fields[i].Name.Equals(MultiMailboxSearchFullTextIndexQuery.MailboxGuidFastPropertyName, StringComparison.OrdinalIgnoreCase))
						{
							result = i;
							break;
						}
					}
				}
			}
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.GetMailboxGuidFieldIndexFromFastResults");
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003A85 File Offset: 0x00001C85
		private static void TraceFunction(string message)
		{
			ExTraceGlobals.FullTextIndexTracer.TraceFunction(46608, 0L, message);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003A9C File Offset: 0x00001C9C
		private static double FetchSizeFromRefiners(Dictionary<string, List<RefinersResultRow>> refinersResult, Guid queryCorrelationId)
		{
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.FetchSizeFromRefiners");
			double result = 0.0;
			if (refinersResult == null || refinersResult.Count == 0)
			{
				ExTraceGlobals.FullTextIndexTracer.TraceInformation<Guid>(33680, 0L, "Correlation Id:{0}. Refiner result is null or empty", queryCorrelationId);
				MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.FetchSizeFromRefiners");
				return result;
			}
			List<RefinersResultRow> list = null;
			if (refinersResult.TryGetValue(MultiMailboxSearchFullTextIndexQuery.SizeFastPropertyName, out list) && list != null && list.Count > 0)
			{
				ExTraceGlobals.FullTextIndexTracer.TraceInformation<Guid>(50064, 0L, "Correlation Id:{0}. Found size refiner result in the refiner results.", queryCorrelationId);
				result = list[0].Sum;
				refinersResult.Remove(MultiMailboxSearchFullTextIndexQuery.SizeFastPropertyName);
			}
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.FetchSizeFromRefiners");
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003B43 File Offset: 0x00001D43
		private void CheckIsAborted()
		{
			if (this.IsAborted)
			{
				throw new TimeoutException("Search was aborted.");
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003B58 File Offset: 0x00001D58
		private AdditionalParameters GenerateParameters(string sortSpec)
		{
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Entering MultiMailboxSearchFullTextIndexQuery.GenerateParameters");
			AdditionalParameters additionalParameters = new AdditionalParameters();
			if (!string.IsNullOrEmpty(sortSpec))
			{
				additionalParameters.Sort = sortSpec;
			}
			if (this.RefinersList != null && this.RefinersList.Count > 0)
			{
				ExTraceGlobals.FullTextIndexTracer.TraceInformation<Guid>(54160, 0L, "Correlation Id:{0}. Found non empty refiner list adding it to the IMS input flow", this.QueryCorrelationId);
				additionalParameters.Refiners = this.RefinersList;
			}
			if (this.ExtraFieldsList != null && this.ExtraFieldsList.Count > 0)
			{
				ExTraceGlobals.FullTextIndexTracer.TraceInformation<Guid>(41872, 0L, "Correlation Id:{0}. Found non empty extra fields list adding it to the IMS input flow", this.QueryCorrelationId);
				additionalParameters.ExtraFields = this.ExtraFieldsList;
			}
			MultiMailboxSearchFullTextIndexQuery.TraceFunction("Exiting MultiMailboxSearchFullTextIndexQuery.GenerateParameters");
			return additionalParameters;
		}

		// Token: 0x04000007 RID: 7
		private const int FastSearchResultsDocumentIdFieldIndex = 1;

		// Token: 0x04000008 RID: 8
		private const int FastSearchResultsOtherFieldIndex = 2;

		// Token: 0x04000009 RID: 9
		private const string FastSearchResultsOtherFieldName = "other";

		// Token: 0x0400000A RID: 10
		private static int pageSize = -1;

		// Token: 0x0400000B RID: 11
		private static string sizeFastPropertyName;

		// Token: 0x0400000C RID: 12
		private static string mailboxGuidFastPropertyName;

		// Token: 0x0400000D RID: 13
		private static Hookable<PagingImsFlowExecutor> serviceProxyWrapperInstance;

		// Token: 0x0400000E RID: 14
		private int isAborted;
	}
}
