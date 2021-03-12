using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200000C RID: 12
	internal sealed class FailedItemStorage : IDisposeTrackable, IFailedItemStorage, IDisposable
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000048CC File Offset: 0x00002ACC
		public FailedItemStorage(ISearchServiceConfig config, string indexSystemName, string hostName)
		{
			Util.ThrowOnNullArgument(config, "config");
			Util.ThrowOnNullArgument(indexSystemName, "indexSystemName");
			Util.ThrowOnNullArgument(hostName, "hostName");
			this.internalImsFlow = FlowDescriptor.GetImsInternalFlowDescriptor(config, indexSystemName).DisplayName;
			this.diagnosticSession = DiagnosticsSession.CreateComponentDiagnosticsSession("FailedItemStorage", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.FailedItemStorageTracer, (long)this.GetHashCode());
			this.fastQueryExecutor = new ExchangeQueryExecutor(hostName, config.QueryServicePort, this.internalImsFlow, false, 0, config.QueryOperationTimeout, config.QueryProxyCacheTimeout);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000496C File Offset: 0x00002B6C
		public void Dispose()
		{
			if (!this.isDisposedFlag)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.fastQueryExecutor != null)
				{
					this.fastQueryExecutor.Dispose();
					this.fastQueryExecutor = null;
				}
				this.isDisposedFlag = true;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000049BC File Offset: 0x00002BBC
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000049D1 File Offset: 0x00002BD1
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FailedItemStorage>(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000049DC File Offset: 0x00002BDC
		public long GetFailedItemsCount(FailedItemParameters parameters)
		{
			string failuresQuery = FailedItemStorage.GetFailuresQuery(parameters);
			return this.GetCount(failuresQuery, "FailedItemStorage: GetFailedItemsCount()");
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000049FC File Offset: 0x00002BFC
		public ICollection<IFailureEntry> GetFailedItems(FailedItemParameters parameters)
		{
			string failuresQuery = FailedItemStorage.GetFailuresQuery(parameters);
			return this.GetFailures(failuresQuery, parameters.Fields, parameters.StartingIndexId, parameters.ResultLimit);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A2C File Offset: 0x00002C2C
		public ICollection<IDocEntry> GetDeletionPendingItems(int deletedMailboxNumber)
		{
			this.CheckDisposed();
			long num = IndexId.CreateIndexId(deletedMailboxNumber, 1);
			long num2 = IndexId.CreateIndexId(deletedMailboxNumber, int.MaxValue);
			QueryFilter value = new BetweenFilter(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.DocumentId, num), new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.DocumentId, num2));
			string text = FqlQueryBuilder.ToFqlString(value, CultureInfo.InvariantCulture);
			this.diagnosticSession.TraceDebug<string>("GetDeletionPendingItems with query: {0}", text);
			PagedReader pagedReader = this.fastQueryExecutor.ExecutePagedQueryByDocumentId(this.internalImsFlow, text, Guid.Empty, DocEntry.Schema);
			return new FailedItemStorage.DocEntryCollection(pagedReader, this.diagnosticSession, this.internalImsFlow);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004ACC File Offset: 0x00002CCC
		public long GetItemsCount(Guid filterMailboxGuid)
		{
			QueryFilter value = (filterMailboxGuid == Guid.Empty) ? FailedItemStorage.ErrorCodeTrueFilter : new TextFilter(ItemSchema.MailboxGuid, filterMailboxGuid.ToString(), MatchOptions.ExactPhrase, MatchFlags.IgnoreCase);
			string query = FqlQueryBuilder.ToFqlString(value, CultureInfo.CurrentCulture);
			return this.GetCount(query, "FailedItemStorage: GetItemsCount()");
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004B1F File Offset: 0x00002D1F
		public long GetPermanentFailureCount()
		{
			return this.GetCount(FailedItemStorage.PermanentFailureQuery, "FailedItemStorage: GetPermanentFailureCount()");
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004B31 File Offset: 0x00002D31
		public ICollection<IFailureEntry> GetRetriableItems(FieldSet fields)
		{
			return this.GetFailures(FailedItemStorage.TransientFailureQuery, fields, 0L, int.MaxValue);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004C08 File Offset: 0x00002E08
		public ICollection<long> GetPoisonDocuments()
		{
			this.CheckDisposed();
			string text = string.Format("errorcode:{0}", EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.PoisonDocument));
			this.diagnosticSession.TraceDebug<string>("GetPoisonItems with query: {0}", text);
			PagedReader pagedReader = this.fastQueryExecutor.ExecutePagedQueryByDocumentId(this.internalImsFlow, text, Guid.Empty, null);
			return ExchangeQueryExecutor.RunUnderExceptionHandler<List<long>>(delegate()
			{
				List<long> list = new List<long>(pagedReader.Count);
				foreach (SearchResultItem searchResultItem in pagedReader)
				{
					foreach (IFieldHolder fieldHolder in searchResultItem.Fields)
					{
						if (fieldHolder.Name == "DocId")
						{
							list.Add((long)fieldHolder.Value);
						}
					}
				}
				return list;
			}, this.diagnosticSession, this.internalImsFlow);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004C84 File Offset: 0x00002E84
		internal static string GetFailuresQuery(FailedItemParameters parameters)
		{
			int evaluationError = parameters.ErrorCode ?? 200;
			List<QueryFilter> list = new List<QueryFilter>();
			QueryFilter item;
			switch (parameters.FailureMode)
			{
			case FailureMode.Transient:
				item = ((parameters.ErrorCode != null) ? new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakeRetriableError(evaluationError)) : new BetweenFilter(new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.IndexingErrorCode, 0), new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakeRetriableError(evaluationError))));
				break;
			case FailureMode.Permanent:
				item = ((parameters.ErrorCode != null) ? new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakePermanentError(evaluationError)) : new BetweenFilter(new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakePermanentError(evaluationError)), new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.IndexingErrorCode, 0)));
				break;
			case FailureMode.All:
				item = ((parameters.ErrorCode != null) ? new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakePermanentError(evaluationError)),
					new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakeRetriableError(evaluationError))
				}) : new OrFilter(new QueryFilter[]
				{
					new BetweenFilter(new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakePermanentError(evaluationError)), new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.IndexingErrorCode, 0)),
					new BetweenFilter(new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.IndexingErrorCode, 0), new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakeRetriableError(evaluationError)))
				}));
				break;
			default:
				throw new ArgumentException(string.Format("Unknown failure mode {0}", parameters.FailureMode));
			}
			list.Add(item);
			if (parameters.MailboxGuid != null)
			{
				list.Add(new TextFilter(ItemSchema.MailboxGuid, parameters.MailboxGuid.ToString(), MatchOptions.ExactPhrase, MatchFlags.Default));
			}
			if (parameters.StartDate != null && parameters.EndDate != null)
			{
				list.Add(new BetweenFilter(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.LastIndexingAttemptTime, parameters.StartDate), new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.LastIndexingAttemptTime, parameters.EndDate)));
			}
			else
			{
				if (parameters.StartDate != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.LastIndexingAttemptTime, parameters.StartDate));
				}
				if (parameters.EndDate != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.LastIndexingAttemptTime, parameters.EndDate));
				}
			}
			if (parameters.ParentEntryId != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ParentEntryId, parameters.ParentEntryId));
			}
			if (parameters.IsPartiallyProcessed != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IsPartiallyIndexed, parameters.IsPartiallyProcessed));
			}
			QueryFilter value = new AndFilter(list.ToArray());
			return FqlQueryBuilder.ToFqlString(value, parameters.Culture);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004FCA File Offset: 0x000031CA
		private void CheckDisposed()
		{
			if (this.isDisposedFlag)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004FE8 File Offset: 0x000031E8
		private ICollection<IFailureEntry> GetFailures(string query, FieldSet fields, long startingIndexId, int maxResults)
		{
			this.CheckDisposed();
			this.diagnosticSession.TraceDebug<string>("Getting failures with query: {0}", query);
			IndexSystemField[] schema = FailureEntry.GetSchema(fields);
			PagedReader pagedReader = this.fastQueryExecutor.ExecutePagedQueryByDocumentId(this.internalImsFlow, query, Guid.Empty, schema, startingIndexId, maxResults);
			return new FailedItemStorage.FailureCollection(pagedReader, this.diagnosticSession, this.internalImsFlow);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005044 File Offset: 0x00003244
		private long GetCount(string query, string traceDebugString)
		{
			this.CheckDisposed();
			this.diagnosticSession.TraceDebug<string, string>("{0} with query: {1}", traceDebugString, query);
			long hitCount = this.fastQueryExecutor.GetHitCount(this.internalImsFlow, query, Guid.Empty);
			this.diagnosticSession.TraceDebug<long>("count: {0}", hitCount);
			return hitCount;
		}

		// Token: 0x04000039 RID: 57
		internal const string MonitoringItemsQuery = "errorcode:{0}";

		// Token: 0x0400003A RID: 58
		private const string DocIdField = "DocId";

		// Token: 0x0400003B RID: 59
		internal static readonly ComparisonFilter ErrorCodeTrueFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ItemSchema.IndexingErrorCode, EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.NonExistentErrorCode));

		// Token: 0x0400003C RID: 60
		internal static readonly string PermanentFailureQuery = string.Format("errorcode:range({0},0)", EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.MaxFailureId));

		// Token: 0x0400003D RID: 61
		internal static readonly string TransientFailureQuery = string.Format("errorcode:range(1,{0})", EvaluationErrorsHelper.MakeRetriableError(EvaluationErrors.MaxFailureId));

		// Token: 0x0400003E RID: 62
		private readonly IDiagnosticsSession diagnosticSession;

		// Token: 0x0400003F RID: 63
		private readonly string internalImsFlow;

		// Token: 0x04000040 RID: 64
		private ExchangeQueryExecutor fastQueryExecutor;

		// Token: 0x04000041 RID: 65
		private DisposeTracker disposeTracker;

		// Token: 0x04000042 RID: 66
		private bool isDisposedFlag;

		// Token: 0x0200000D RID: 13
		private abstract class EntryCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : IDocEntry
		{
			// Token: 0x0600009C RID: 156 RVA: 0x000050FC File Offset: 0x000032FC
			protected EntryCollection(PagedReader pagedReader, IDiagnosticsSession diagnosticsSession, string flowName)
			{
				this.pagedReader = pagedReader;
				this.diagnosticsSession = diagnosticsSession;
				this.flowName = flowName;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00005126 File Offset: 0x00003326
			public int Count
			{
				get
				{
					return ExchangeQueryExecutor.RunUnderExceptionHandler<int>(() => this.pagedReader.Count, this.diagnosticsSession, this.flowName);
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00005145 File Offset: 0x00003345
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600009F RID: 159
			public abstract IEnumerator<T> GetEnumerator();

			// Token: 0x060000A0 RID: 160 RVA: 0x00005148 File Offset: 0x00003348
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x00005150 File Offset: 0x00003350
			public void Add(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00005157 File Offset: 0x00003357
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x0000515E File Offset: 0x0000335E
			public bool Contains(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x00005165 File Offset: 0x00003365
			public void CopyTo(T[] array, int arrayIndex)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x0000516C File Offset: 0x0000336C
			public bool Remove(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x000053B4 File Offset: 0x000035B4
			protected IEnumerable<ISearchResultItem> GetItemsSafe()
			{
				using (IEnumerator<ISearchResultItem> enumerator = ExchangeQueryExecutor.RunUnderExceptionHandler<IEnumerator<SearchResultItem>>(new Func<IEnumerator<SearchResultItem>>(this.pagedReader.GetEnumerator), this.diagnosticsSession, this.flowName))
				{
					while (ExchangeQueryExecutor.RunUnderExceptionHandler<bool>(new Func<bool>(enumerator.MoveNext), this.diagnosticsSession, this.flowName))
					{
						yield return ExchangeQueryExecutor.RunUnderExceptionHandler<ISearchResultItem>(() => enumerator.Current, this.diagnosticsSession, this.flowName);
					}
				}
				yield break;
			}

			// Token: 0x04000043 RID: 67
			private readonly PagedReader pagedReader;

			// Token: 0x04000044 RID: 68
			private readonly IDiagnosticsSession diagnosticsSession;

			// Token: 0x04000045 RID: 69
			private readonly string flowName;
		}

		// Token: 0x0200000E RID: 14
		private sealed class DocEntryCollection : FailedItemStorage.EntryCollection<IDocEntry>
		{
			// Token: 0x060000A8 RID: 168 RVA: 0x000053D1 File Offset: 0x000035D1
			public DocEntryCollection(PagedReader pagedReader, IDiagnosticsSession diagnosticsSession, string flowName) : base(pagedReader, diagnosticsSession, flowName)
			{
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x00005514 File Offset: 0x00003714
			public override IEnumerator<IDocEntry> GetEnumerator()
			{
				foreach (ISearchResultItem item in base.GetItemsSafe())
				{
					yield return new DocEntry(item);
				}
				yield break;
			}
		}

		// Token: 0x0200000F RID: 15
		private sealed class FailureCollection : FailedItemStorage.EntryCollection<IFailureEntry>
		{
			// Token: 0x060000AA RID: 170 RVA: 0x00005530 File Offset: 0x00003730
			public FailureCollection(PagedReader pagedReader, IDiagnosticsSession diagnosticsSession, string flowName) : base(pagedReader, diagnosticsSession, flowName)
			{
			}

			// Token: 0x060000AB RID: 171 RVA: 0x00005674 File Offset: 0x00003874
			public override IEnumerator<IFailureEntry> GetEnumerator()
			{
				foreach (ISearchResultItem item in base.GetItemsSafe())
				{
					yield return new FailureEntry(item);
				}
				yield break;
			}
		}
	}
}
