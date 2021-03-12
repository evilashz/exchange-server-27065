using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200000A RID: 10
	internal sealed class ExchangeQueryExecutor : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003BF1 File Offset: 0x00001DF1
		internal ExchangeQueryExecutor(ISearchServiceConfig config, string imsFlowName) : this(config.HostName, config.QueryServicePort, imsFlowName, false, 1, config.QueryOperationTimeout, config.QueryProxyCacheTimeout)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C14 File Offset: 0x00001E14
		internal ExchangeQueryExecutor(string hostName, int queryServicePort, bool readHitCount) : this(hostName, queryServicePort, null, readHitCount)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C20 File Offset: 0x00001E20
		internal ExchangeQueryExecutor(string hostName, int queryServicePort, string imsFlowName, bool readHitCount) : this(hostName, queryServicePort, imsFlowName, readHitCount, 1, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(5.0))
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal ExchangeQueryExecutor(string hostName, int queryServicePort, string imsFlowName, bool readHitCount, int retryCount, TimeSpan operationTimeout, TimeSpan proxyCacheTimeout)
		{
			this.imsFlowName = imsFlowName;
			this.readHitCount = readHitCount;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("ExchangeQueryExecutor", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.QueryExecutorTracer, (long)this.GetHashCode());
			this.flowExecutor = Factory.Current.CreatePagingImsFlowExecutor(hostName, queryServicePort, 2, operationTimeout, operationTimeout, operationTimeout, proxyCacheTimeout, 2147483647L, 65536, retryCount);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003CC8 File Offset: 0x00001EC8
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public long TotalHits { get; private set; }

		// Token: 0x0600006E RID: 110 RVA: 0x00003CDC File Offset: 0x00001EDC
		public static T RunUnderExceptionHandler<T>(Func<T> call, IDiagnosticsSession session, string flowName)
		{
			T result;
			try
			{
				result = call();
			}
			catch (CommunicationException ex)
			{
				session.TraceError<CommunicationException>("ExchangeQueryExecutor.CallImsFlow - operation returned exception: {0}.", ex);
				session.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_FastConnectionException, flowName, new object[]
				{
					ex
				});
				throw new OperationFailedException(ex);
			}
			catch (TimeoutException ex2)
			{
				session.TraceError<TimeoutException>("ExchangeQueryExecutor.CallImsFlow - operation returned exception: {0}.", ex2);
				session.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_FastConnectionException, flowName, new object[]
				{
					ex2
				});
				throw new OperationFailedException(ex2);
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F34 File Offset: 0x00002134
		public IEnumerable<long> ExecuteQuery(Guid mailboxGuid, string query)
		{
			foreach (SearchResultItem item in this.ExecuteQueryWithFields(mailboxGuid, query))
			{
				yield return (long)item.Fields[1].Value;
			}
			yield break;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003F5F File Offset: 0x0000215F
		public IEnumerable<SearchResultItem> ExecuteQueryWithFields(Guid mailboxGuid, string query)
		{
			return this.ExecuteQueryWithFields(mailboxGuid, query, 0L, 1000, false, null);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F72 File Offset: 0x00002172
		public IEnumerable<SearchResultItem> ExecuteQueryWithFields(Guid mailboxGuid, string query, List<string> extraFields)
		{
			return this.ExecuteQueryWithFields(mailboxGuid, query, 0L, 1000, false, extraFields);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F85 File Offset: 0x00002185
		public IEnumerable<SearchResultItem> ExecuteQueryWithFields(Guid mailboxGuid, string query, long offset, int trimHits, bool firstPageOnly)
		{
			return this.ExecuteQueryWithFields(mailboxGuid, query, offset, trimHits, firstPageOnly, null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000442C File Offset: 0x0000262C
		public IEnumerable<SearchResultItem> ExecuteQueryWithFields(Guid mailboxGuid, string query, long offset, int trimHits, bool firstPageOnly, List<string> extraFields)
		{
			ExchangeQueryExecutor.<>c__DisplayClassb CS$<>8__locals1 = new ExchangeQueryExecutor.<>c__DisplayClassb();
			CS$<>8__locals1.mailboxGuid = mailboxGuid;
			CS$<>8__locals1.query = query;
			CS$<>8__locals1.offset = offset;
			CS$<>8__locals1.trimHits = trimHits;
			CS$<>8__locals1.<>4__this = this;
			this.TotalHits = -1L;
			bool isFirstPage = true;
			CS$<>8__locals1.parameters = new AdditionalParameters
			{
				ExtraFields = extraFields
			};
			using (IEnumerator<KeyValuePair<PagingImsFlowExecutor.QueryExecutionContext, SearchResultItem[]>> pageEnumerator = this.RunUnderExceptionHandler<IEnumerable<KeyValuePair<PagingImsFlowExecutor.QueryExecutionContext, SearchResultItem[]>>>(() => CS$<>8__locals1.<>4__this.flowExecutor.Execute(CS$<>8__locals1.<>4__this.imsFlowName, CS$<>8__locals1.mailboxGuid, Guid.NewGuid(), CS$<>8__locals1.query, CS$<>8__locals1.offset, CultureInfo.InvariantCulture, CS$<>8__locals1.parameters, CS$<>8__locals1.trimHits, null)).GetEnumerator())
			{
				do
				{
					if (!this.RunUnderExceptionHandler<bool>(() => pageEnumerator.MoveNext()))
					{
						break;
					}
					if (isFirstPage)
					{
						if (this.readHitCount)
						{
							this.TotalHits = this.RunUnderExceptionHandler<long>(delegate()
							{
								PagingImsFlowExecutor pagingImsFlowExecutor = CS$<>8__locals1.<>4__this.flowExecutor;
								KeyValuePair<PagingImsFlowExecutor.QueryExecutionContext, SearchResultItem[]> keyValuePair2 = pageEnumerator.Current;
								return pagingImsFlowExecutor.ReadHitCount(keyValuePair2.Key);
							});
						}
						isFirstPage = false;
					}
					KeyValuePair<PagingImsFlowExecutor.QueryExecutionContext, SearchResultItem[]> keyValuePair = pageEnumerator.Current;
					foreach (SearchResultItem item in keyValuePair.Value)
					{
						yield return item;
					}
				}
				while (!firstPageOnly);
			}
			yield break;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004476 File Offset: 0x00002676
		public PagedReader ExecutePagedQueryByDocumentId(string flowName, string query, Guid tenantId, IndexSystemField[] extraFieldsToReturn)
		{
			return this.ExecutePagedQueryByDocumentId(flowName, query, tenantId, extraFieldsToReturn, 0L, int.MaxValue);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044D8 File Offset: 0x000026D8
		public PagedReader ExecutePagedQueryByDocumentId(string flowName, string query, Guid tenantId, IndexSystemField[] extraFieldsToReturn, long startingDocId, int maxResults)
		{
			AdditionalParameters parameters = new AdditionalParameters
			{
				ExtraFields = this.GetIndexSystemFieldNamesList(extraFieldsToReturn)
			};
			PagedReader result = this.RunUnderExceptionHandler<PagedReader>(() => this.flowExecutor.ExecutePagedQueryByDocumentId(flowName, query, tenantId, parameters, startingDocId, maxResults));
			this.diagnosticsSession.TraceDebug<int>("Rows returned: {0}", this.RunUnderExceptionHandler<int>(() => result.Count));
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000045C8 File Offset: 0x000027C8
		public long GetHitCount(string flowName, string query, Guid tenantId)
		{
			return this.RunUnderExceptionHandler<long>(delegate()
			{
				QueryParameters queryParameters = new QueryParameters(this.flowExecutor.GetLookupTimeout(), flowName, query, tenantId, Guid.NewGuid(), null);
				return this.flowExecutor.GetHitCount(queryParameters);
			});
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004609 File Offset: 0x00002809
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeQueryExecutor>(this);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004611 File Offset: 0x00002811
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004620 File Offset: 0x00002820
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004635 File Offset: 0x00002835
		private void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.flowExecutor != null)
				{
					this.flowExecutor.Dispose();
					this.flowExecutor = null;
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004670 File Offset: 0x00002870
		private List<string> GetIndexSystemFieldNamesList(IndexSystemField[] fields)
		{
			if (fields == null || fields.Length == 0)
			{
				return null;
			}
			List<string> list = new List<string>(fields.Length);
			foreach (IndexSystemField indexSystemField in fields)
			{
				if (!indexSystemField.Retrievable)
				{
					throw new ArgumentException(indexSystemField.Name + " is not Retrievable");
				}
				list.Add(indexSystemField.Name);
			}
			return list;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000046CF File Offset: 0x000028CF
		private T RunUnderExceptionHandler<T>(Func<T> call)
		{
			return ExchangeQueryExecutor.RunUnderExceptionHandler<T>(call, this.diagnosticsSession, this.imsFlowName);
		}

		// Token: 0x04000031 RID: 49
		private const int HitsPerQuery = 1000;

		// Token: 0x04000032 RID: 50
		private readonly string imsFlowName;

		// Token: 0x04000033 RID: 51
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000034 RID: 52
		private readonly bool readHitCount;

		// Token: 0x04000035 RID: 53
		private PagingImsFlowExecutor flowExecutor;

		// Token: 0x04000036 RID: 54
		private DisposeTracker disposeTracker;
	}
}
