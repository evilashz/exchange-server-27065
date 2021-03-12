using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000011 RID: 17
	internal class FullTextIndexQuery : IFullTextIndexQuery
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000050D4 File Offset: 0x000032D4
		internal FullTextIndexQuery() : this(FullTextIndexQuery.SearchConfig.FastQueryResultTrimHits)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000050E6 File Offset: 0x000032E6
		internal FullTextIndexQuery(int pageSize)
		{
			this.pageSize = pageSize;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000050F5 File Offset: 0x000032F5
		private static SearchConfig SearchConfig
		{
			get
			{
				return SearchConfig.Instance;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000050FC File Offset: 0x000032FC
		private static Hookable<PagingImsFlowExecutor> ServiceProxyWrapper
		{
			get
			{
				if (FullTextIndexQuery.serviceProxyWrapperInstance == null)
				{
					string hostName = FullTextIndexQuery.SearchConfig.HostName;
					int queryServicePort = FullTextIndexQuery.SearchConfig.QueryServicePort;
					int fastImsChannelPoolSize = FullTextIndexQuery.SearchConfig.FastImsChannelPoolSize;
					TimeSpan fastImsOpenTimeout = FullTextIndexQuery.SearchConfig.FastImsOpenTimeout;
					TimeSpan fastImsSendTimeout = FullTextIndexQuery.SearchConfig.FastImsSendTimeout;
					TimeSpan fastImsReceiveTimeout = FullTextIndexQuery.SearchConfig.FastImsReceiveTimeout;
					int fastSearchRetryCount = FullTextIndexQuery.SearchConfig.FastSearchRetryCount;
					long num = (long)FullTextIndexQuery.SearchConfig.FastIMSMaxReceivedMessageSize;
					int fastIMSMaxStringContentLength = FullTextIndexQuery.SearchConfig.FastIMSMaxStringContentLength;
					TimeSpan fastProxyCacheTimeout = FullTextIndexQuery.SearchConfig.FastProxyCacheTimeout;
					Hookable<PagingImsFlowExecutor> value = Hookable<PagingImsFlowExecutor>.Create(true, new PagingImsFlowExecutor(hostName, queryServicePort, fastImsChannelPoolSize, fastImsOpenTimeout, fastImsSendTimeout, fastImsReceiveTimeout, fastProxyCacheTimeout, num, fastIMSMaxStringContentLength, fastSearchRetryCount));
					Interlocked.CompareExchange<Hookable<PagingImsFlowExecutor>>(ref FullTextIndexQuery.serviceProxyWrapperInstance, value, null);
				}
				return FullTextIndexQuery.serviceProxyWrapperInstance;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000051B8 File Offset: 0x000033B8
		public List<FullTextIndexRow> ExecuteFullTextIndexQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, CultureInfo culture, Guid correlationId, PagingImsFlowExecutor.QueryLoggingContext loggingContext)
		{
			List<FullTextIndexRow> list = new List<FullTextIndexRow>(0);
			IEnumerable<SearchResultItem[]> enumerable = FullTextIndexQuery.ServiceProxyWrapper.Value.ExecuteSimple(FullTextIndexSchema.GetImsFlowName(databaseGuid), mailboxGuid, correlationId, query, 0L, culture, FullTextIndexQuery.AdditionalQueryParameters, this.pageSize, loggingContext);
			foreach (SearchResultItem[] page in enumerable)
			{
				FullTextIndexQuery.CacheOnePageOfResults(page, list, mailboxNumber);
			}
			return list;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005238 File Offset: 0x00003438
		public IEnumerable<FullTextDiagnosticRow> ExecuteDiagnosticQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, CultureInfo culture, Guid correlationId, string sortOrder, ICollection<string> additionalColumns, PagingImsFlowExecutor.QueryLoggingContext loggingContext)
		{
			AdditionalParameters additionalParameters = new AdditionalParameters
			{
				Sort = sortOrder,
				ExtraFields = new List<string>(additionalColumns)
			};
			IEnumerable<SearchResultItem[]> pagedResults = FullTextIndexQuery.ServiceProxyWrapper.Value.ExecuteSimple(FullTextIndexSchema.GetImsFlowName(databaseGuid), mailboxGuid, correlationId, query, 0L, culture, additionalParameters, this.pageSize, loggingContext);
			return FullTextDiagnosticRow.Parse(pagedResults);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005290 File Offset: 0x00003490
		public List<FullTextIndexRow> ExecutePagedFullTextIndexQuery(Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, string query, CultureInfo culture, Guid correlationId, bool needConversationId, PagingImsFlowExecutor.QueryLoggingContext loggingContext, PagedQueryResults pagedQueryResults)
		{
			List<FullTextIndexRow> list = new List<FullTextIndexRow>(0);
			if (!pagedQueryResults.IsInitialized)
			{
				pagedQueryResults.Initialize(FullTextIndexQuery.ServiceProxyWrapper.Value.ExecuteSimple(FullTextIndexSchema.GetImsFlowName(databaseGuid), mailboxGuid, correlationId, query, 0L, culture, needConversationId ? FullTextIndexQuery.AdditionalQueryParametersConversations : FullTextIndexQuery.AdditionalQueryParameters, this.pageSize, loggingContext).GetEnumerator());
			}
			if (pagedQueryResults.PagedResults.MoveNext())
			{
				SearchResultItem[] page = (SearchResultItem[])pagedQueryResults.PagedResults.Current;
				FullTextIndexQuery.CacheOnePageOfResults(page, list, mailboxNumber);
			}
			return list;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005318 File Offset: 0x00003518
		public int GetPageSize()
		{
			return this.pageSize;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005320 File Offset: 0x00003520
		internal static IDisposable SetPagingImsFlowExecutorTestHook(PagingImsFlowExecutor testHook)
		{
			return FullTextIndexQuery.ServiceProxyWrapper.SetTestHook(testHook);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005330 File Offset: 0x00003530
		private static void CacheOnePageOfResults(SearchResultItem[] page, List<FullTextIndexRow> rows, int mailboxNumber)
		{
			rows.Capacity = rows.Count + page.Length;
			foreach (SearchResultItem searchResultItem in page)
			{
				long num = (long)searchResultItem.Fields[1].Value;
				if (IndexId.GetMailboxNumber(num) == mailboxNumber)
				{
					SearchResultItem searchResultItem2 = searchResultItem.Fields[2].Value as SearchResultItem;
					if (searchResultItem2 != null && searchResultItem2.Fields.Count > 0 && searchResultItem2.Fields[0].Name == FastIndexSystemSchema.ConversationId.Name && searchResultItem2.Fields[0].Value != null)
					{
						int conversationId = (int)((long)searchResultItem2.Fields[0].Value);
						rows.Add(FullTextIndexRow.Parse(num, conversationId));
					}
					else
					{
						rows.Add(FullTextIndexRow.Parse(num));
					}
				}
			}
		}

		// Token: 0x04000056 RID: 86
		private const int FastSearchResultsDocumentIdFieldIndex = 1;

		// Token: 0x04000057 RID: 87
		private const int FastSearchResultsOtherFieldIndex = 2;

		// Token: 0x04000058 RID: 88
		private static readonly AdditionalParameters AdditionalQueryParameters = new AdditionalParameters
		{
			Sort = "-received"
		};

		// Token: 0x04000059 RID: 89
		private static readonly AdditionalParameters AdditionalQueryParametersConversations = new AdditionalParameters
		{
			Sort = "-received",
			ExtraFields = new List<string>
			{
				"conversationid"
			}
		};

		// Token: 0x0400005A RID: 90
		private static Hookable<PagingImsFlowExecutor> serviceProxyWrapperInstance;

		// Token: 0x0400005B RID: 91
		private readonly int pageSize;
	}
}
