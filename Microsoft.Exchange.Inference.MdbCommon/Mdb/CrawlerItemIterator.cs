using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000002 RID: 2
	internal class CrawlerItemIterator<TSort> where TSort : struct, IComparable<TSort>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CrawlerItemIterator(ICrawlerFolderIterator folderIterator, int maxRowCount, PropertyDefinition sortProperty, Predicate<object[]> filterPredicate, params PropertyDefinition[] predicateProperties)
		{
			Util.ThrowOnNullArgument(folderIterator, "folderIterator");
			Util.ThrowOnNullArgument(sortProperty, "sortProperty");
			if (filterPredicate != null)
			{
				Util.ThrowOnNullArgument(predicateProperties, "predicateProperties");
			}
			else if (predicateProperties != null && predicateProperties.Length > 0)
			{
				throw new ArgumentException("Predicate properties should not be specified when there's no filter supplied.");
			}
			this.folderIterator = folderIterator;
			this.maxRowCount = Math.Min(maxRowCount, 10000);
			this.sortProperty = sortProperty;
			this.filterPredicate = filterPredicate;
			this.predicateProperties = predicateProperties;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("CrawlerItemIterator", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.MdbCrawlerFeederTracer, (long)this.GetHashCode());
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002590 File Offset: 0x00000790
		public IEnumerable<MdbCompositeItemIdentity> GetItems(MailboxSession session, TSort intervalStart, TSort intervalStop)
		{
			Util.ThrowOnNullArgument(session, "session");
			List<CrawlerItemIterator<TSort>.QueryResultWrapper> resultsToDispose = new List<CrawlerItemIterator<TSort>.QueryResultWrapper>();
			SortedList<CrawlerItemIterator<TSort>.SortKey, CrawlerItemIterator<TSort>.QueryResultWrapper> mergeList = new SortedList<CrawlerItemIterator<TSort>.SortKey, CrawlerItemIterator<TSort>.QueryResultWrapper>();
			try
			{
				using (IEnumerator<StoreObjectId> enumerator = this.folderIterator.GetFolders(session).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StoreObjectId folderId = enumerator.Current;
						CrawlerItemIterator<TSort>.QueryResultWrapper queryResultWrapper = new CrawlerItemIterator<TSort>.QueryResultWrapper(this.diagnosticsSession, session, folderId, this.sortProperty, intervalStart, intervalStop, this.filterPredicate, this.predicateProperties, this.maxRowCount);
						resultsToDispose.Add(queryResultWrapper);
						queryResultWrapper.CanCacheQueryResult = (50 < mergeList.Count);
						if (queryResultWrapper.MoveNext())
						{
							mergeList.Add(queryResultWrapper.CurrentValue, queryResultWrapper);
						}
					}
					goto IL_217;
				}
				IL_11D:
				int cacheCounter = 0;
				using (IEnumerator<CrawlerItemIterator<TSort>.QueryResultWrapper> enumerator2 = mergeList.Values.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						CrawlerItemIterator<TSort>.QueryResultWrapper result = enumerator2.Current;
						yield return result.CurrentIdentity;
						mergeList.RemoveAt(0);
						if (result.MoveNext())
						{
							mergeList.Add(result.CurrentValue, result);
							if (cacheCounter > 0 && !result.CanCacheQueryResult)
							{
								result.CanCacheQueryResult = true;
								cacheCounter--;
							}
						}
						else if (result.CanCacheQueryResult)
						{
							cacheCounter++;
						}
					}
				}
				IL_217:
				if (mergeList.Count > 0)
				{
					goto IL_11D;
				}
			}
			finally
			{
				foreach (CrawlerItemIterator<TSort>.QueryResultWrapper queryResultWrapper2 in resultsToDispose)
				{
					queryResultWrapper2.Dispose();
				}
				resultsToDispose.Clear();
			}
			yield break;
		}

		// Token: 0x04000001 RID: 1
		private const int MaxOutstandingQueries = 50;

		// Token: 0x04000002 RID: 2
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000003 RID: 3
		private readonly ICrawlerFolderIterator folderIterator;

		// Token: 0x04000004 RID: 4
		private readonly int maxRowCount;

		// Token: 0x04000005 RID: 5
		private readonly PropertyDefinition sortProperty;

		// Token: 0x04000006 RID: 6
		private readonly Predicate<object[]> filterPredicate;

		// Token: 0x04000007 RID: 7
		private readonly PropertyDefinition[] predicateProperties;

		// Token: 0x02000003 RID: 3
		private struct SortKey : IComparable<CrawlerItemIterator<TSort>.SortKey>
		{
			// Token: 0x06000003 RID: 3 RVA: 0x000025C2 File Offset: 0x000007C2
			public SortKey(TSort value, int documentid, StoreObjectId folderId, bool isAscending)
			{
				this.value = value;
				this.documentId = documentid;
				this.folderId = folderId;
				this.isAscending = isAscending;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000004 RID: 4 RVA: 0x000025E1 File Offset: 0x000007E1
			public TSort Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x06000005 RID: 5 RVA: 0x000025EC File Offset: 0x000007EC
			public int CompareTo(CrawlerItemIterator<TSort>.SortKey other)
			{
				TSort tsort = this.value;
				int num = tsort.CompareTo(other.value);
				if (num == 0)
				{
					num = this.documentId.CompareTo(other.documentId);
				}
				if (num == 0)
				{
					num = this.folderId.CompareTo(other.folderId);
				}
				if (!this.isAscending)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x04000008 RID: 8
			private readonly TSort value;

			// Token: 0x04000009 RID: 9
			private readonly int documentId;

			// Token: 0x0400000A RID: 10
			private readonly bool isAscending;

			// Token: 0x0400000B RID: 11
			private readonly StoreObjectId folderId;
		}

		// Token: 0x02000004 RID: 4
		private class QueryResultWrapper : Disposable
		{
			// Token: 0x06000006 RID: 6 RVA: 0x00002650 File Offset: 0x00000850
			public QueryResultWrapper(IDiagnosticsSession tracer, MailboxSession session, StoreObjectId folderId, PropertyDefinition sortProperty, TSort intervalStart, TSort intervalStop, Predicate<object[]> filterPredicate, PropertyDefinition[] predicateProperties, int maxRowCount)
			{
				Util.ThrowOnNullArgument(session, "session");
				Util.ThrowOnNullArgument(folderId, "folderId");
				Util.ThrowOnNullArgument(sortProperty, "sortProperty");
				int num = 0;
				if (filterPredicate != null)
				{
					Util.ThrowOnNullArgument(predicateProperties, "predicateProperties");
					num = predicateProperties.Length;
				}
				else if (predicateProperties != null && predicateProperties.Length > 0)
				{
					throw new ArgumentException("Predicate properties should not be specified when there's no filter supplied.");
				}
				this.diagnosticsSession = tracer;
				this.maxRowCount = maxRowCount;
				this.session = session;
				this.folderId = folderId;
				this.sortProperty = sortProperty;
				this.intervalStart = intervalStart;
				this.intervalStop = intervalStop;
				this.filterPredicate = filterPredicate;
				this.propertiesToRequest = new PropertyDefinition[3 + num];
				this.propertiesToRequest[0] = ItemSchema.Id;
				this.propertiesToRequest[1] = sortProperty;
				this.propertiesToRequest[2] = ItemSchema.DocumentId;
				for (int i = 0; i < num; i++)
				{
					this.propertiesToRequest[i + 3] = predicateProperties[i];
				}
				this.isAscending = (this.intervalStart.CompareTo(this.intervalStop) < 0);
				if (this.sortProperty == ItemSchema.DocumentId)
				{
					this.sortOrder = new SortBy[]
					{
						new SortBy(ItemSchema.DocumentId, this.isAscending ? SortOrder.Ascending : SortOrder.Descending)
					};
					return;
				}
				this.sortOrder = new SortBy[]
				{
					new SortBy(sortProperty, this.isAscending ? SortOrder.Ascending : SortOrder.Descending),
					new SortBy(ItemSchema.DocumentId, SortOrder.Ascending)
				};
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000007 RID: 7 RVA: 0x000027CB File Offset: 0x000009CB
			// (set) Token: 0x06000008 RID: 8 RVA: 0x000027D3 File Offset: 0x000009D3
			public bool CanCacheQueryResult
			{
				get
				{
					return this.canCacheResult;
				}
				set
				{
					this.canCacheResult = value;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000009 RID: 9 RVA: 0x000027DC File Offset: 0x000009DC
			public CrawlerItemIterator<TSort>.SortKey CurrentValue
			{
				get
				{
					if (this.enumerator == null)
					{
						throw new InvalidOperationException();
					}
					return new CrawlerItemIterator<TSort>.SortKey((TSort)((object)this.enumerator.Current[1]), this.CurrentDocumentId, this.folderId, this.isAscending);
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000A RID: 10 RVA: 0x00002818 File Offset: 0x00000A18
			public MdbCompositeItemIdentity CurrentIdentity
			{
				get
				{
					if (this.enumerator == null)
					{
						throw new InvalidOperationException();
					}
					return new MdbCompositeItemIdentity(this.session.MdbGuid, this.session.MailboxGuid, (int)this.session.Mailbox.TryGetProperty(MailboxSchema.MailboxNumber), (this.enumerator.Current[0] as VersionedId).ObjectId, this.CurrentDocumentId);
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600000B RID: 11 RVA: 0x00002885 File Offset: 0x00000A85
			private int CurrentDocumentId
			{
				get
				{
					if (this.enumerator == null)
					{
						throw new InvalidOperationException();
					}
					return (int)this.enumerator.Current[2];
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600000C RID: 12 RVA: 0x00002B60 File Offset: 0x00000D60
			private IEnumerable<object[]> Entries
			{
				get
				{
					Folder folder = null;
					QueryResult result = null;
					try
					{
						TSort lastReturnedSortKey = this.intervalStart;
						int lastReturnedDocumentId = 0;
						object[][] entries = this.GetRowsFromQuery(ref folder, ref result, lastReturnedSortKey, lastReturnedDocumentId);
						while (entries.Length > 0)
						{
							foreach (object[] entry in entries)
							{
								yield return entry;
							}
							object[] lastEntry = entries[entries.Length - 1];
							lastReturnedSortKey = (TSort)((object)lastEntry[1]);
							lastReturnedDocumentId = (int)lastEntry[2];
							entries = this.GetRowsFromQuery(ref folder, ref result, lastReturnedSortKey, lastReturnedDocumentId);
						}
					}
					finally
					{
						if (result != null)
						{
							result.Dispose();
						}
						if (folder != null)
						{
							folder.Dispose();
						}
					}
					yield break;
				}
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002B80 File Offset: 0x00000D80
			public bool MoveNext()
			{
				if (this.isCompleted)
				{
					return false;
				}
				if (this.enumerator == null)
				{
					this.enumerator = this.Entries.GetEnumerator();
				}
				object[] array = (this.filterPredicate != null) ? new object[this.propertiesToRequest.Length - 3] : null;
				while (this.enumerator.MoveNext())
				{
					if (this.isAscending)
					{
						TSort tsort = this.intervalStop;
						if (tsort.CompareTo(this.CurrentValue.Value) < 0)
						{
							break;
						}
					}
					if (!this.isAscending)
					{
						TSort tsort2 = this.intervalStop;
						if (tsort2.CompareTo(this.CurrentValue.Value) > 0)
						{
							break;
						}
					}
					if (this.filterPredicate != null)
					{
						Array.Copy(this.enumerator.Current, 3, array, 0, array.Length);
						if (!this.filterPredicate(array))
						{
							continue;
						}
					}
					return true;
				}
				this.isCompleted = true;
				this.enumerator.Dispose();
				this.enumerator = null;
				return false;
			}

			// Token: 0x0600000E RID: 14 RVA: 0x00002C83 File Offset: 0x00000E83
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose && this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002CA2 File Offset: 0x00000EA2
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<CrawlerItemIterator<TSort>.QueryResultWrapper>(this);
			}

			// Token: 0x06000010 RID: 16 RVA: 0x00002E4C File Offset: 0x0000104C
			private object[][] GetRowsFromQuery(ref Folder folder, ref QueryResult result, TSort lastReturnedSortKey, int lastReturnedDocumentId)
			{
				QueryResult tempQueryResult = result;
				Folder tempFolder = folder;
				folder = null;
				result = null;
				object[][] result2;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					if (tempFolder == null)
					{
						tempFolder = XsoUtil.TranslateXsoExceptionsWithReturnValue<Folder>(this.diagnosticsSession, Strings.ConnectionToMailboxFailed(this.session.MailboxGuid), () => Folder.Bind(this.session, this.folderId));
						if (tempFolder == null)
						{
							return CrawlerItemIterator<TSort>.QueryResultWrapper.EmptyResult;
						}
						disposeGuard.Add<Folder>(tempFolder);
						tempQueryResult = XsoUtil.TranslateXsoExceptionsWithReturnValue<QueryResult>(this.diagnosticsSession, Strings.ConnectionToMailboxFailed(this.session.MailboxGuid), XsoUtil.XsoExceptionHandlingFlags.DoNotExpectObjectNotFound, () => tempFolder.ItemQuery(ItemQueryType.None, null, this.sortOrder, this.propertiesToRequest));
						if (tempQueryResult == null)
						{
							return CrawlerItemIterator<TSort>.QueryResultWrapper.EmptyResult;
						}
						disposeGuard.Add<QueryResult>(tempQueryResult);
						bool isSeekSuccessful = false;
						XsoUtil.TranslateXsoExceptions(this.diagnosticsSession, Strings.ConnectionToMailboxFailed(this.session.MailboxGuid), XsoUtil.XsoExceptionHandlingFlags.DoNotExpectObjectNotFound | XsoUtil.XsoExceptionHandlingFlags.DoNotExpectCorruptData, delegate()
						{
							if (this.sortProperty == ItemSchema.DocumentId)
							{
								tempQueryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(this.isAscending ? ComparisonOperator.GreaterThan : ComparisonOperator.LessThan, ItemSchema.DocumentId, lastReturnedSortKey), SeekToConditionFlags.None);
							}
							else
							{
								tempQueryResult.SeekToCondition(SeekReference.OriginBeginning, new OrFilter(new QueryFilter[]
								{
									new AndFilter(new QueryFilter[]
									{
										new ComparisonFilter(this.isAscending ? ComparisonOperator.GreaterThanOrEqual : ComparisonOperator.LessThanOrEqual, this.sortProperty, lastReturnedSortKey),
										new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.DocumentId, lastReturnedDocumentId)
									}),
									new ComparisonFilter(this.isAscending ? ComparisonOperator.GreaterThan : ComparisonOperator.LessThan, this.sortProperty, lastReturnedSortKey)
								}), SeekToConditionFlags.AllowExtendedFilters);
							}
							isSeekSuccessful = true;
						});
						if (!isSeekSuccessful)
						{
							return CrawlerItemIterator<TSort>.QueryResultWrapper.EmptyResult;
						}
					}
					else
					{
						disposeGuard.Add<Folder>(tempFolder);
						disposeGuard.Add<QueryResult>(tempQueryResult);
					}
					object[][] array = XsoUtil.TranslateXsoExceptionsWithReturnValue<object[][]>(this.diagnosticsSession, Strings.ConnectionToMailboxFailed(this.session.MailboxGuid), XsoUtil.XsoExceptionHandlingFlags.DoNotExpectObjectNotFound | XsoUtil.XsoExceptionHandlingFlags.DoNotExpectCorruptData, () => tempQueryResult.GetRows(this.maxRowCount));
					if (array == null)
					{
						result2 = CrawlerItemIterator<TSort>.QueryResultWrapper.EmptyResult;
					}
					else
					{
						if (this.canCacheResult)
						{
							folder = tempFolder;
							result = tempQueryResult;
							disposeGuard.Success();
						}
						result2 = array;
					}
				}
				return result2;
			}

			// Token: 0x0400000C RID: 12
			private static readonly object[][] EmptyResult = new object[0][];

			// Token: 0x0400000D RID: 13
			private readonly IDiagnosticsSession diagnosticsSession;

			// Token: 0x0400000E RID: 14
			private readonly int maxRowCount;

			// Token: 0x0400000F RID: 15
			private readonly MailboxSession session;

			// Token: 0x04000010 RID: 16
			private readonly StoreObjectId folderId;

			// Token: 0x04000011 RID: 17
			private readonly PropertyDefinition sortProperty;

			// Token: 0x04000012 RID: 18
			private readonly TSort intervalStart;

			// Token: 0x04000013 RID: 19
			private readonly TSort intervalStop;

			// Token: 0x04000014 RID: 20
			private readonly Predicate<object[]> filterPredicate;

			// Token: 0x04000015 RID: 21
			private readonly PropertyDefinition[] propertiesToRequest;

			// Token: 0x04000016 RID: 22
			private readonly SortBy[] sortOrder;

			// Token: 0x04000017 RID: 23
			private readonly bool isAscending;

			// Token: 0x04000018 RID: 24
			private IEnumerator<object[]> enumerator;

			// Token: 0x04000019 RID: 25
			private bool isCompleted;

			// Token: 0x0400001A RID: 26
			private bool canCacheResult = true;

			// Token: 0x02000005 RID: 5
			private enum PropertyValueIndex
			{
				// Token: 0x0400001C RID: 28
				Id,
				// Token: 0x0400001D RID: 29
				SortProperty,
				// Token: 0x0400001E RID: 30
				DocumentId,
				// Token: 0x0400001F RID: 31
				PredicatePropertiesBase
			}
		}
	}
}
