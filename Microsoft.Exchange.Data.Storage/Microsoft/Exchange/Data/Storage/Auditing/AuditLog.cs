using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F3E RID: 3902
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AuditLog : IAuditLog
	{
		// Token: 0x06008620 RID: 34336 RVA: 0x0024B8A1 File Offset: 0x00249AA1
		public AuditLog(StoreSession storeSession, StoreId logFolderId, DateTime logRangeStart, DateTime logRangeEnd, int itemCount, Func<IAuditLogRecord, MessageItem, int> recordFormatter)
		{
			this.storeSession = storeSession;
			this.LogFolderId = logFolderId;
			this.EstimatedLogStartTime = logRangeStart;
			this.EstimatedLogEndTime = logRangeEnd;
			this.EstimatedItemCount = itemCount;
			this.recordFormatter = recordFormatter;
		}

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x06008621 RID: 34337 RVA: 0x0024B8D6 File Offset: 0x00249AD6
		// (set) Token: 0x06008622 RID: 34338 RVA: 0x0024B8DE File Offset: 0x00249ADE
		public DateTime EstimatedLogStartTime { get; private set; }

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x06008623 RID: 34339 RVA: 0x0024B8E7 File Offset: 0x00249AE7
		// (set) Token: 0x06008624 RID: 34340 RVA: 0x0024B8EF File Offset: 0x00249AEF
		public DateTime EstimatedLogEndTime { get; private set; }

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x06008625 RID: 34341 RVA: 0x0024B8F8 File Offset: 0x00249AF8
		public bool IsAsynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06008626 RID: 34342 RVA: 0x0024B8FB File Offset: 0x00249AFB
		// (set) Token: 0x06008627 RID: 34343 RVA: 0x0024B903 File Offset: 0x00249B03
		public StoreId LogFolderId { get; private set; }

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06008628 RID: 34344 RVA: 0x0024B90C File Offset: 0x00249B0C
		public StoreSession Session
		{
			get
			{
				return this.storeSession;
			}
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x06008629 RID: 34345 RVA: 0x0024B914 File Offset: 0x00249B14
		// (set) Token: 0x0600862A RID: 34346 RVA: 0x0024B91C File Offset: 0x00249B1C
		public int EstimatedItemCount { get; private set; }

		// Token: 0x0600862B RID: 34347 RVA: 0x0024BB3C File Offset: 0x00249D3C
		public IEnumerable<T> FindAuditRecords<T>(IAuditRecordStrategy<T> strategy)
		{
			using (Folder logFolder = Folder.Bind(this.storeSession, this.LogFolderId))
			{
				foreach (T result in AuditLog.InternalFindAuditRecords<T>(logFolder, strategy))
				{
					yield return result;
				}
			}
			yield break;
		}

		// Token: 0x0600862C RID: 34348 RVA: 0x0024BB60 File Offset: 0x00249D60
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			if (this.recordFormatter == null)
			{
				throw new InvalidOperationException("Audit log is not configured properly.");
			}
			int result;
			using (MessageItem messageItem = MessageItem.Create(this.Session, this.LogFolderId))
			{
				result = this.recordFormatter(auditRecord, messageItem);
				messageItem.Save(SaveMode.NoConflictResolutionForceSave);
			}
			return result;
		}

		// Token: 0x0600862D RID: 34349 RVA: 0x0024BBC8 File Offset: 0x00249DC8
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			if (typeof(TFilter) != typeof(QueryFilter))
			{
				throw new NotSupportedException();
			}
			return (IAuditQueryContext<TFilter>)new AuditLog.AuditLogQueryContext(this);
		}

		// Token: 0x0600862E RID: 34350 RVA: 0x0024BEB8 File Offset: 0x0024A0B8
		private static IEnumerable<T> InternalFindAuditRecords<T>(Folder logFolder, IAuditRecordStrategy<T> strategy)
		{
			using (QueryResult queryResults = logFolder.ItemQuery(ItemQueryType.None, null, strategy.QuerySortOrder, strategy.Columns))
			{
				queryResults.SeekToOffset(SeekReference.OriginBeginning, 0);
				bool theEnd = false;
				while (!theEnd)
				{
					object[][] rows = queryResults.GetRows(1000);
					if (rows.Length <= 0)
					{
						break;
					}
					foreach (object[] row in rows)
					{
						AuditLog.RowPropertyBag rowAsPropertyBag = new AuditLog.RowPropertyBag(strategy.Columns, row);
						bool stopNow;
						bool match = strategy.RecordFilter(rowAsPropertyBag, out stopNow);
						if (stopNow)
						{
							theEnd = true;
							break;
						}
						if (match)
						{
							yield return strategy.Convert(rowAsPropertyBag);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x040059C8 RID: 22984
		private const int ItemQueryBatchSize = 1000;

		// Token: 0x040059C9 RID: 22985
		private StoreSession storeSession;

		// Token: 0x040059CA RID: 22986
		private Func<IAuditLogRecord, MessageItem, int> recordFormatter;

		// Token: 0x02000F3F RID: 3903
		private class RowPropertyBag : IReadOnlyPropertyBag
		{
			// Token: 0x0600862F RID: 34351 RVA: 0x0024BEDC File Offset: 0x0024A0DC
			public RowPropertyBag(PropertyDefinition[] columns, object[] values)
			{
				this.columns = columns;
				this.row = values;
			}

			// Token: 0x17002396 RID: 9110
			public object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					StorePropertyDefinition other = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
					for (int i = 0; i < this.columns.Length; i++)
					{
						StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(this.columns[i]);
						if (storePropertyDefinition.CompareTo(other) == 0)
						{
							return this.row[i];
						}
					}
					return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
				}
			}

			// Token: 0x06008631 RID: 34353 RVA: 0x0024BF42 File Offset: 0x0024A142
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040059CF RID: 22991
			private PropertyDefinition[] columns;

			// Token: 0x040059D0 RID: 22992
			private object[] row;
		}

		// Token: 0x02000F41 RID: 3905
		private class AuditLogQueryContext : DisposableObject, IAuditQueryContext<QueryFilter>, IDisposable
		{
			// Token: 0x06008634 RID: 34356 RVA: 0x0024BF49 File Offset: 0x0024A149
			public AuditLogQueryContext(AuditLog auditLog)
			{
				this.auditLog = auditLog;
				this.pendingAsyncResult = null;
			}

			// Token: 0x06008635 RID: 34357 RVA: 0x0024BF60 File Offset: 0x0024A160
			public IAsyncResult BeginAuditLogQuery(QueryFilter queryFilter, int maximumResultsCount)
			{
				if (this.pendingAsyncResult != null)
				{
					throw new InvalidOperationException("Asynchronous query is already pending.");
				}
				StoreId storeId = null;
				IAsyncResult result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					Folder disposable;
					if (queryFilter != null)
					{
						SearchFolder searchFolder = SearchFolder.Create(this.auditLog.Session, this.auditLog.Session.GetDefaultFolderId(DefaultFolderType.SearchFolders), "SearchAuditMailboxFolder" + Guid.NewGuid().ToString(), CreateMode.OpenIfExists);
						disposeGuard.Add<SearchFolder>(searchFolder);
						searchFolder.Save();
						searchFolder.Load();
						storeId = searchFolder.Id;
						result = searchFolder.BeginApplyOneTimeSearch(new SearchFolderCriteria(queryFilter, new StoreId[]
						{
							this.auditLog.LogFolderId
						})
						{
							DeepTraversal = false,
							UseCiForComplexQueries = true,
							FailNonContentIndexedSearch = true,
							MaximumResultsCount = new int?(maximumResultsCount)
						}, null, null);
						disposable = searchFolder;
					}
					else
					{
						disposable = Folder.Bind(this.auditLog.Session, this.auditLog.LogFolderId);
						disposeGuard.Add<Folder>(disposable);
						result = new CompletedAsyncResult();
					}
					disposeGuard.Success();
					this.pendingAsyncResult = result;
					this.folder = disposable;
					this.folderIdToDelete = storeId;
				}
				return result;
			}

			// Token: 0x06008636 RID: 34358 RVA: 0x0024C0C4 File Offset: 0x0024A2C4
			public IEnumerable<T> EndAuditLogQuery<T>(IAsyncResult asyncResult, IAuditQueryStrategy<T> queryStrategy)
			{
				SearchFolder searchFolder = this.folder as SearchFolder;
				if (searchFolder != null)
				{
					SearchFolderCriteria searchCriteria = searchFolder.GetSearchCriteria();
					if ((searchCriteria.SearchState & SearchState.FailNonContentIndexedSearch) == SearchState.FailNonContentIndexedSearch && (searchCriteria.SearchState & SearchState.Failed) == SearchState.Failed)
					{
						throw queryStrategy.GetQueryFailedException();
					}
				}
				AuditLog.AuditLogQueryContext.QueryRecordStrategy<T> strategy = new AuditLog.AuditLogQueryContext.QueryRecordStrategy<T>(queryStrategy);
				return AuditLog.InternalFindAuditRecords<T>(this.folder, strategy);
			}

			// Token: 0x06008637 RID: 34359 RVA: 0x0024C127 File Offset: 0x0024A327
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<AuditLog.AuditLogQueryContext>(this);
			}

			// Token: 0x06008638 RID: 34360 RVA: 0x0024C130 File Offset: 0x0024A330
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					if (this.folder != null)
					{
						this.folder.Dispose();
						this.folder = null;
					}
					if (this.folderIdToDelete != null)
					{
						this.auditLog.Session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							this.folderIdToDelete
						});
					}
				}
				base.InternalDispose(disposing);
			}

			// Token: 0x040059D1 RID: 22993
			private static readonly SortBy[] SortBySentTime = new SortBy[]
			{
				new SortBy(ItemSchema.SentTime, SortOrder.Descending)
			};

			// Token: 0x040059D2 RID: 22994
			private static readonly PropertyDefinition[] QueryProperties = new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.SentTime
			};

			// Token: 0x040059D3 RID: 22995
			private AuditLog auditLog;

			// Token: 0x040059D4 RID: 22996
			private Folder folder;

			// Token: 0x040059D5 RID: 22997
			private StoreId folderIdToDelete;

			// Token: 0x040059D6 RID: 22998
			private IAsyncResult pendingAsyncResult;

			// Token: 0x02000F42 RID: 3906
			private class QueryRecordStrategy<T> : IAuditRecordStrategy<T>
			{
				// Token: 0x0600863A RID: 34362 RVA: 0x0024C1D1 File Offset: 0x0024A3D1
				public QueryRecordStrategy(IAuditQueryStrategy<T> queryStrategy)
				{
					this.queryStrategy = queryStrategy;
				}

				// Token: 0x17002397 RID: 9111
				// (get) Token: 0x0600863B RID: 34363 RVA: 0x0024C1E0 File Offset: 0x0024A3E0
				public SortBy[] QuerySortOrder
				{
					get
					{
						return AuditLog.AuditLogQueryContext.SortBySentTime;
					}
				}

				// Token: 0x17002398 RID: 9112
				// (get) Token: 0x0600863C RID: 34364 RVA: 0x0024C1E7 File Offset: 0x0024A3E7
				public PropertyDefinition[] Columns
				{
					get
					{
						return AuditLog.AuditLogQueryContext.QueryProperties;
					}
				}

				// Token: 0x0600863D RID: 34365 RVA: 0x0024C1F0 File Offset: 0x0024A3F0
				public bool RecordFilter(IReadOnlyPropertyBag propertyBag, out bool stopNow)
				{
					stopNow = false;
					return propertyBag[ItemSchema.Id] is StoreId && this.queryStrategy.RecordFilter(propertyBag, out stopNow);
				}

				// Token: 0x0600863E RID: 34366 RVA: 0x0024C223 File Offset: 0x0024A423
				public T Convert(IReadOnlyPropertyBag propertyBag)
				{
					return this.queryStrategy.Convert(propertyBag);
				}

				// Token: 0x040059D7 RID: 22999
				private IAuditQueryStrategy<T> queryStrategy;
			}
		}
	}
}
