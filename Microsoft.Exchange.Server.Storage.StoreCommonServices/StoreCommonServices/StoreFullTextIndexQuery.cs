using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FullTextIndex;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200015D RID: 349
	public class StoreFullTextIndexQuery : ICustomParameter, IRefillableTableContents, IEnumerable<FullTextIndexRow>, IEnumerable, IHasCustomToStringImplementation
	{
		// Token: 0x06000D94 RID: 3476 RVA: 0x000444B9 File Offset: 0x000426B9
		internal StoreFullTextIndexQuery(SearchCriteria criteria, CultureInfo culture)
		{
			this.criteria = criteria;
			this.culture = culture;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x000444CF File Offset: 0x000426CF
		internal FqlQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = this.criteria.ToFql(FqlQueryGenerator.Options.Default, this.culture);
				}
				return this.query;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000444F7 File Offset: 0x000426F7
		bool IRefillableTableContents.CanRefill
		{
			get
			{
				return !this.done;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00044502 File Offset: 0x00042702
		void IRefillableTableContents.MarkChunkConsumed()
		{
			this.rows = null;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0004450B File Offset: 0x0004270B
		internal IList<FullTextIndexRow> Rows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00044513 File Offset: 0x00042713
		internal bool NeedsRefill
		{
			get
			{
				return !this.Executed && !this.done;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00044528 File Offset: 0x00042728
		internal bool Executed
		{
			get
			{
				return this.rows != null;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00044536 File Offset: 0x00042736
		internal bool Done
		{
			get
			{
				return this.done;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0004453E File Offset: 0x0004273E
		internal bool Failed
		{
			get
			{
				return this.failed;
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00044548 File Offset: 0x00042748
		private static void UpdateQueryResultStatistics(StorePerDatabasePerformanceCountersInstance perDatabasePerformanceCounters, TimeSpan totalTime, long numberOfResults)
		{
			perDatabasePerformanceCounters.TotalSuccessfulSearches.Increment();
			perDatabasePerformanceCounters.SearchQueryResultRate.IncrementBy(numberOfResults);
			perDatabasePerformanceCounters.AverageSearchResultSize.IncrementBy(numberOfResults);
			perDatabasePerformanceCounters.AverageSearchResultSizeBase.Increment();
			StoreFullTextIndexQuery.GetCorrespondingLatencyCounter(perDatabasePerformanceCounters, totalTime).Increment();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00044594 File Offset: 0x00042794
		private static ExPerformanceCounter GetCorrespondingLatencyCounter(StorePerDatabasePerformanceCountersInstance perDatabasePerformanceCounters, TimeSpan totalTime)
		{
			long num = (long)totalTime.TotalMilliseconds;
			if (num < 500L)
			{
				return perDatabasePerformanceCounters.TotalSearchesBelow500msec;
			}
			if (num < 2000L)
			{
				return perDatabasePerformanceCounters.TotalSearchesBetween500msecTo2sec;
			}
			if (num <= 10000L)
			{
				return perDatabasePerformanceCounters.TotalSearchesBetween2To10sec;
			}
			if (num <= 60000L)
			{
				return perDatabasePerformanceCounters.TotalSearchesBetween10SecTo60Sec;
			}
			return perDatabasePerformanceCounters.TotalSearchesQueriesGreaterThan60Seconds;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000447E0 File Offset: 0x000429E0
		private static PagingImsFlowExecutor.QueryLoggingContext CreateLoggingContext(Guid databaseGuid, int mailboxNumber, FqlQuery query, Guid correlationId, int clientType, string clientAction, int pageSize, StorePerDatabasePerformanceCountersInstance perfCounters, SearchExecutionDiagnostics diagnostics)
		{
			bool isTracingEnabled = ExTraceGlobals.FullTextIndexTracer.IsTraceEnabled(TraceType.DebugTrace);
			Stopwatch stopWatch = new Stopwatch();
			return new PagingImsFlowExecutor.QueryLoggingContext(delegate()
			{
				perfCounters.TotalSearchesInProgress.Increment();
				perfCounters.TotalSearches.Increment();
				diagnostics.OnIssueFastRequest(pageSize, query);
				if (isTracingEnabled)
				{
					ExTraceGlobals.FullTextIndexTracer.TraceDebug(0L, "Mailbox {0}, Client Type={1}, Action={2}: Calling out to FAST (query: '{3}').", new object[]
					{
						mailboxNumber,
						clientType,
						clientAction,
						query
					});
				}
				stopWatch.Reset();
				stopWatch.Start();
			}, delegate(int rowCount)
			{
				stopWatch.Stop();
				perfCounters.TotalSearchesInProgress.Decrement();
				StoreFullTextIndexQuery.UpdateQueryResultStatistics(perfCounters, stopWatch.ToTimeSpan(), (long)rowCount);
				diagnostics.OnReceiveFastResponse(rowCount, stopWatch.ToTimeSpan());
				if (isTracingEnabled)
				{
					ExTraceGlobals.FullTextIndexTracer.TraceDebug(0L, "Mailbox {0}: Call to FAST returned {1} hits in {2}ms (query: '{3}').", new object[]
					{
						mailboxNumber,
						rowCount,
						stopWatch.ElapsedMilliseconds,
						query
					});
				}
			}, delegate(string errorMessage)
			{
				stopWatch.Stop();
				perfCounters.TotalSearchesInProgress.Decrement();
				diagnostics.OnReceiveFastError(errorMessage, stopWatch.ToTimeSpan());
				if (isTracingEnabled)
				{
					ExTraceGlobals.FullTextIndexTracer.TraceDebug(0L, "Mailbox {0}: Exception raised calling out to FAST (query: '{1}') in {2} ms: {3}", new object[]
					{
						mailboxNumber,
						query,
						stopWatch.ElapsedMilliseconds,
						errorMessage
					});
				}
			});
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0004486E File Offset: 0x00042A6E
		public IEnumerator<FullTextIndexRow> GetEnumerator()
		{
			if (this.rows == null)
			{
				return null;
			}
			return this.rows.GetEnumerator();
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0004488A File Offset: 0x00042A8A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00044894 File Offset: 0x00042A94
		public void ExecuteAll(Context context, MailboxState mailboxState, SearchExecutionDiagnostics diagnostics)
		{
			try
			{
				IFullTextIndexQuery fullTextIndexQuery = StoreFullTextIndexHelper.FtiQueryCreator.Value();
				PagingImsFlowExecutor.QueryLoggingContext loggingContext = StoreFullTextIndexQuery.CreateLoggingContext(mailboxState.DatabaseGuid, mailboxState.MailboxNumber, this.Query, diagnostics.CorrelationId, (int)context.ClientType, context.Diagnostics.ClientActionString, fullTextIndexQuery.GetPageSize(), PerformanceCounterFactory.GetDatabaseInstance(mailboxState.Database), diagnostics);
				this.rows = fullTextIndexQuery.ExecuteFullTextIndexQuery(mailboxState.DatabaseGuid, mailboxState.MailboxGuid, mailboxState.MailboxNumber, this.Query.Value, context.Culture, diagnostics.CorrelationId, loggingContext);
			}
			catch (TimeoutException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_FullTextIndexException, new object[]
				{
					this.Query,
					ex.ToString()
				});
				this.rows = new List<FullTextIndexRow>(0);
				this.failed = true;
			}
			catch (CommunicationException ex2)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex2);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_FullTextIndexException, new object[]
				{
					this.Query,
					ex2.ToString()
				});
				this.rows = new List<FullTextIndexRow>(0);
				this.failed = true;
			}
			this.done = true;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000449E4 File Offset: 0x00042BE4
		public void ExecuteOnePage(Context context, MailboxState mailboxState, bool needConversationDocumentId, SearchExecutionDiagnostics diagnostics)
		{
			if (this.pagedQueryResults == null)
			{
				this.pagedQueryResults = new PagedQueryResults();
			}
			try
			{
				IFullTextIndexQuery fullTextIndexQuery = StoreFullTextIndexHelper.FtiQueryCreator.Value();
				PagingImsFlowExecutor.QueryLoggingContext loggingContext = StoreFullTextIndexQuery.CreateLoggingContext(mailboxState.DatabaseGuid, mailboxState.MailboxNumber, this.Query, diagnostics.CorrelationId, (int)context.ClientType, context.Diagnostics.ClientActionString, fullTextIndexQuery.GetPageSize(), PerformanceCounterFactory.GetDatabaseInstance(mailboxState.Database), diagnostics);
				this.rows = fullTextIndexQuery.ExecutePagedFullTextIndexQuery(mailboxState.DatabaseGuid, mailboxState.MailboxGuid, mailboxState.MailboxNumber, this.Query.Value, context.Culture, diagnostics.CorrelationId, needConversationDocumentId, loggingContext, this.pagedQueryResults);
			}
			catch (TimeoutException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_FullTextIndexException, new object[]
				{
					this.Query,
					ex.ToString()
				});
				this.rows = new List<FullTextIndexRow>(0);
				this.failed = true;
			}
			catch (CommunicationException ex2)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex2);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_FullTextIndexException, new object[]
				{
					this.Query,
					ex2.ToString()
				});
				this.rows = new List<FullTextIndexRow>(0);
				this.failed = true;
			}
			this.done = (this.rows.Count == 0);
			if (this.done)
			{
				this.pagedQueryResults.Dispose();
				this.pagedQueryResults = null;
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00044B78 File Offset: 0x00042D78
		public void Cleanup()
		{
			if (this.pagedQueryResults != null)
			{
				this.pagedQueryResults.Dispose();
				this.pagedQueryResults = null;
			}
			this.done = false;
			this.failed = false;
			this.rows = null;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00044BAC File Offset: 0x00042DAC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(120);
			this.AppendAsString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00044BD0 File Offset: 0x00042DD0
		public void AppendAsString(StringBuilder sb)
		{
			sb.Append("FQL:[");
			sb.Append(this.Query);
			sb.Append("], ");
			sb.Append("SearchCriteria:[");
			this.criteria.AppendToString(sb, StringFormatOptions.IncludeDetails);
			sb.Append("]");
		}

		// Token: 0x04000782 RID: 1922
		private readonly CultureInfo culture;

		// Token: 0x04000783 RID: 1923
		private readonly SearchCriteria criteria;

		// Token: 0x04000784 RID: 1924
		private FqlQuery query;

		// Token: 0x04000785 RID: 1925
		private List<FullTextIndexRow> rows;

		// Token: 0x04000786 RID: 1926
		private PagedQueryResults pagedQueryResults;

		// Token: 0x04000787 RID: 1927
		private bool done;

		// Token: 0x04000788 RID: 1928
		private bool failed;
	}
}
