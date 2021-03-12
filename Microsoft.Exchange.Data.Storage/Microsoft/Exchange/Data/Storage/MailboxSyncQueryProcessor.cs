using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E63 RID: 3683
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxSyncQueryProcessor
	{
		// Token: 0x06007FA3 RID: 32675 RVA: 0x0022EB04 File Offset: 0x0022CD04
		public static MailboxSyncQueryProcessor.IQueryResult ItemQuery(Folder folder, ItemQueryType itemQueryType, QueryFilter filter, QueryFilter optimizationFilter, SortBy[] sortBy, PropertyDefinition[] queryColumns, bool allowTableRestrict, bool useSortOrder)
		{
			if (!(optimizationFilter is FalseFilter))
			{
				return new MailboxSyncQueryProcessor.InMemoryFilterQueryResult(folder, itemQueryType, filter, optimizationFilter, sortBy, queryColumns, useSortOrder);
			}
			if (MailboxSyncQueryProcessor.InMemoryFilterQueryResult.Match(filter, sortBy))
			{
				return new MailboxSyncQueryProcessor.InMemoryFilterQueryResult(folder, itemQueryType, filter, sortBy, queryColumns, useSortOrder);
			}
			if (allowTableRestrict)
			{
				return new MailboxSyncQueryProcessor.SlowQueryResult(folder, itemQueryType, filter, sortBy, queryColumns);
			}
			throw new InvalidOperationException(ServerStrings.ExNoOptimizedCodePath);
		}

		// Token: 0x02000E64 RID: 3684
		// (Invoke) Token: 0x06007FA6 RID: 32678
		public delegate int ComparisonDelegate(object x, object y);

		// Token: 0x02000E65 RID: 3685
		internal interface IQueryResult : ITableView, IPagedView, IDisposeTrackable, IDisposable
		{
		}

		// Token: 0x02000E66 RID: 3686
		internal class InMemoryFilterQueryResult : MailboxSyncQueryProcessor.IQueryResult, ITableView, IPagedView, IDisposeTrackable, IDisposable
		{
			// Token: 0x06007FA9 RID: 32681 RVA: 0x0022EB6C File Offset: 0x0022CD6C
			public InMemoryFilterQueryResult(Folder folder, ItemQueryType itemQueryType, QueryFilter filter, SortBy[] sortBy, PropertyDefinition[] queryColumns, bool useSortOrder)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter == null)
				{
					AndFilter andFilter = filter as AndFilter;
					if (andFilter != null)
					{
						comparisonFilter = (andFilter.Filters[0] as ComparisonFilter);
						if (comparisonFilter == null)
						{
							comparisonFilter = (ComparisonFilter)andFilter.Filters[1];
						}
					}
				}
				this.BuildResultSet(folder, itemQueryType, filter, comparisonFilter, sortBy, queryColumns, useSortOrder);
			}

			// Token: 0x06007FAA RID: 32682 RVA: 0x0022EBCA File Offset: 0x0022CDCA
			public InMemoryFilterQueryResult(Folder folder, ItemQueryType itemQueryType, QueryFilter filter, QueryFilter optimizationFilter, SortBy[] sortBy, PropertyDefinition[] queryColumns, bool useSortOrder)
			{
				this.BuildResultSet(folder, itemQueryType, filter, optimizationFilter, sortBy, queryColumns, useSortOrder);
			}

			// Token: 0x17002206 RID: 8710
			// (get) Token: 0x06007FAB RID: 32683 RVA: 0x0022EBE3 File Offset: 0x0022CDE3
			public int CurrentRow
			{
				get
				{
					return this.idxCurrentRow;
				}
			}

			// Token: 0x17002207 RID: 8711
			// (get) Token: 0x06007FAC RID: 32684 RVA: 0x0022EBEB File Offset: 0x0022CDEB
			public int EstimatedRowCount
			{
				get
				{
					if (this.result != null)
					{
						return this.result.Count;
					}
					return 0;
				}
			}

			// Token: 0x06007FAD RID: 32685 RVA: 0x0022EC04 File Offset: 0x0022CE04
			public static bool Match(QueryFilter queryFilter, SortBy[] sortBy)
			{
				if (!(queryFilter is ComparisonFilter))
				{
					if (queryFilter is OrFilter)
					{
						if (!MailboxSyncQueryProcessor.InMemoryFilterQueryResult.MatchOrFilter((OrFilter)queryFilter))
						{
							return false;
						}
					}
					else if (queryFilter is TextFilter)
					{
						if (!MailboxSyncQueryProcessor.InMemoryFilterQueryResult.MatchItemClassTextFilter((TextFilter)queryFilter))
						{
							return false;
						}
					}
					else
					{
						if (!(queryFilter is AndFilter))
						{
							return false;
						}
						AndFilter andFilter = (AndFilter)queryFilter;
						if (andFilter.FilterCount != 2)
						{
							return false;
						}
						int index;
						if (andFilter.Filters[0] is ComparisonFilter)
						{
							index = 1;
						}
						else
						{
							if (!(andFilter.Filters[1] is ComparisonFilter))
							{
								return false;
							}
							index = 0;
						}
						OrFilter orFilter = andFilter.Filters[index] as OrFilter;
						if (orFilter != null)
						{
							if (!MailboxSyncQueryProcessor.InMemoryFilterQueryResult.MatchOrFilter(orFilter))
							{
								return false;
							}
						}
						else
						{
							if (!(andFilter.Filters[index] is TextFilter))
							{
								return false;
							}
							if (!MailboxSyncQueryProcessor.InMemoryFilterQueryResult.MatchItemClassTextFilter((TextFilter)andFilter.Filters[index]))
							{
								return false;
							}
						}
					}
				}
				return sortBy != null && 1 == sortBy.Length && sortBy[0].ColumnDefinition.Type == typeof(int);
			}

			// Token: 0x06007FAE RID: 32686 RVA: 0x0022ED14 File Offset: 0x0022CF14
			public void Dispose()
			{
			}

			// Token: 0x06007FAF RID: 32687 RVA: 0x0022ED16 File Offset: 0x0022CF16
			public virtual DisposeTracker GetDisposeTracker()
			{
				return null;
			}

			// Token: 0x06007FB0 RID: 32688 RVA: 0x0022ED19 File Offset: 0x0022CF19
			public void SuppressDisposeTracker()
			{
			}

			// Token: 0x06007FB1 RID: 32689 RVA: 0x0022ED1C File Offset: 0x0022CF1C
			public object[][] GetRows(int numRows)
			{
				if (this.result == null || this.idxCurrentRow >= this.result.Count)
				{
					return MailboxSyncQueryProcessor.InMemoryFilterQueryResult.EmptyResultSet;
				}
				int num = this.result.Count - this.idxCurrentRow;
				if (num > numRows)
				{
					num = numRows;
				}
				object[][] array = new object[num][];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.result[this.idxCurrentRow + i];
				}
				this.idxCurrentRow += num;
				return array;
			}

			// Token: 0x06007FB2 RID: 32690 RVA: 0x0022EDC8 File Offset: 0x0022CFC8
			public bool SeekToCondition(SeekReference seekReference, QueryFilter seekCondition)
			{
				ComparisonFilter comparisonFilter = seekCondition as ComparisonFilter;
				if (comparisonFilter == null || seekReference != SeekReference.OriginBeginning)
				{
					throw new InvalidOperationException(ServerStrings.ExCannotSeekRow);
				}
				this.idxCurrentRow = 0;
				if (this.result == null || this.result.Count == 0)
				{
					return false;
				}
				MailboxSyncQueryProcessor.InMemoryFilterQueryResult.FoundResultDelegate foundResultDelegate;
				switch (comparisonFilter.ComparisonOperator)
				{
				case ComparisonOperator.Equal:
					foundResultDelegate = ((int result) => result == 0);
					break;
				case ComparisonOperator.NotEqual:
					foundResultDelegate = ((int result) => result != 0);
					break;
				case ComparisonOperator.LessThan:
					foundResultDelegate = ((int result) => result < 0);
					break;
				case ComparisonOperator.LessThanOrEqual:
					foundResultDelegate = ((int result) => result <= 0);
					break;
				case ComparisonOperator.GreaterThan:
					foundResultDelegate = ((int result) => result > 0);
					break;
				case ComparisonOperator.GreaterThanOrEqual:
					foundResultDelegate = ((int result) => result >= 0);
					break;
				default:
					throw new InvalidOperationException(ServerStrings.ExInvalidComparisonOperatorInComparisonFilter);
				}
				while (!foundResultDelegate(this.compareDelegate(this.result[this.idxCurrentRow][this.idxSortColumn], comparisonFilter.PropertyValue)))
				{
					this.idxCurrentRow++;
					if (this.idxCurrentRow >= this.result.Count)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007FB3 RID: 32691 RVA: 0x0022EF68 File Offset: 0x0022D168
			public int SeekToOffset(SeekReference seekReference, int offset)
			{
				if (seekReference != SeekReference.OriginBeginning)
				{
					throw new InvalidOperationException(ServerStrings.ExUnsupportedSeekReference);
				}
				if (this.result == null)
				{
					this.idxCurrentRow = 0;
				}
				else if (offset >= this.result.Count)
				{
					offset = this.result.Count;
				}
				else
				{
					this.idxCurrentRow = offset;
				}
				return this.CurrentRow;
			}

			// Token: 0x06007FB4 RID: 32692 RVA: 0x0022EFC4 File Offset: 0x0022D1C4
			private static QueryFilter CreateNegatedComparisonFilter(ComparisonFilter comparisonFilter)
			{
				switch (comparisonFilter.ComparisonOperator)
				{
				case ComparisonOperator.Equal:
					return new ComparisonFilter(ComparisonOperator.NotEqual, comparisonFilter.Property, comparisonFilter.PropertyValue);
				case ComparisonOperator.NotEqual:
					return new ComparisonFilter(ComparisonOperator.Equal, comparisonFilter.Property, comparisonFilter.PropertyValue);
				case ComparisonOperator.LessThan:
					return new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, comparisonFilter.Property, comparisonFilter.PropertyValue);
				case ComparisonOperator.LessThanOrEqual:
					return new ComparisonFilter(ComparisonOperator.GreaterThan, comparisonFilter.Property, comparisonFilter.PropertyValue);
				case ComparisonOperator.GreaterThan:
					return new ComparisonFilter(ComparisonOperator.LessThanOrEqual, comparisonFilter.Property, comparisonFilter.PropertyValue);
				case ComparisonOperator.GreaterThanOrEqual:
					return new ComparisonFilter(ComparisonOperator.LessThan, comparisonFilter.Property, comparisonFilter.PropertyValue);
				default:
					throw new InvalidOperationException("Invalid comparison operator");
				}
			}

			// Token: 0x06007FB5 RID: 32693 RVA: 0x0022F074 File Offset: 0x0022D274
			private static bool MatchItemClassTextFilter(TextFilter textFilter)
			{
				return textFilter.MatchFlags == MatchFlags.IgnoreCase && (textFilter.MatchOptions == MatchOptions.Prefix || textFilter.MatchOptions == MatchOptions.PrefixOnWords) && textFilter.Property == StoreObjectSchema.ItemClass;
			}

			// Token: 0x06007FB6 RID: 32694 RVA: 0x0022F0A8 File Offset: 0x0022D2A8
			private static bool MatchOrFilter(OrFilter comparisonOrFilter)
			{
				foreach (QueryFilter queryFilter in comparisonOrFilter.Filters)
				{
					TextFilter textFilter = queryFilter as TextFilter;
					if (textFilter == null)
					{
						return false;
					}
					if (!MailboxSyncQueryProcessor.InMemoryFilterQueryResult.MatchItemClassTextFilter(textFilter))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007FB7 RID: 32695 RVA: 0x0022F114 File Offset: 0x0022D314
			private void BuildResultSet(Folder folder, ItemQueryType itemQueryType, QueryFilter filter, QueryFilter optimizationFilter, SortBy[] sortBy, PropertyDefinition[] queryColumns, bool useSortOrder)
			{
				MailboxSyncPropertyBag mailboxSyncPropertyBag = new MailboxSyncPropertyBag(queryColumns);
				ComparisonFilter comparisonFilter = optimizationFilter as ComparisonFilter;
				mailboxSyncPropertyBag.AddColumnsFromFilter(filter);
				this.idxSortColumn = mailboxSyncPropertyBag.AddColumn(sortBy[0].ColumnDefinition);
				SortBy[] sortColumns = null;
				if (comparisonFilter != null && useSortOrder)
				{
					SortOrder sortOrder;
					switch (comparisonFilter.ComparisonOperator)
					{
					case ComparisonOperator.LessThan:
					case ComparisonOperator.LessThanOrEqual:
						sortOrder = SortOrder.Descending;
						break;
					default:
						sortOrder = SortOrder.Ascending;
						break;
					}
					sortColumns = new SortBy[]
					{
						new SortBy(comparisonFilter.Property, sortOrder)
					};
				}
				QueryResult queryResult2;
				QueryResult queryResult = queryResult2 = folder.ItemQuery(itemQueryType, null, sortColumns, mailboxSyncPropertyBag.Columns);
				try
				{
					bool flag = 0 != queryResult.EstimatedRowCount;
					if (comparisonFilter != null)
					{
						flag = queryResult.SeekToCondition(SeekReference.OriginBeginning, comparisonFilter);
						int currentRow = queryResult.CurrentRow;
					}
					if (flag)
					{
						int currentRow = queryResult.CurrentRow;
						int num = -1;
						if (comparisonFilter != null)
						{
							QueryFilter seekFilter = MailboxSyncQueryProcessor.InMemoryFilterQueryResult.CreateNegatedComparisonFilter(comparisonFilter);
							if (queryResult.SeekToCondition(SeekReference.OriginCurrent, seekFilter))
							{
								num = queryResult.CurrentRow;
							}
							queryResult.SeekToOffset(SeekReference.OriginBeginning, currentRow);
						}
						this.result = new List<object[]>(queryResult.EstimatedRowCount);
						do
						{
							int num2 = 10000;
							if (-1 != num)
							{
								num2 = num - currentRow;
							}
							if (num2 > 10000)
							{
								num2 = 10000;
							}
							object[][] rows = queryResult.GetRows(num2);
							if (rows.Length == 0)
							{
								break;
							}
							for (int i = 0; i < rows.Length; i++)
							{
								mailboxSyncPropertyBag.Bind(rows[i]);
								if (EvaluatableFilter.Evaluate(filter, mailboxSyncPropertyBag))
								{
									this.result.Add(rows[i]);
								}
							}
						}
						while (-1 == num || queryResult.CurrentRow < num);
					}
				}
				finally
				{
					if (queryResult2 != null)
					{
						((IDisposable)queryResult2).Dispose();
					}
				}
				if (this.result != null)
				{
					this.result.Sort(this.GetSortByComparer(sortBy[0]));
				}
			}

			// Token: 0x06007FB8 RID: 32696 RVA: 0x0022F300 File Offset: 0x0022D500
			private IComparer<object[]> GetSortByComparer(SortBy sortBy)
			{
				if (sortBy.ColumnDefinition.Type == typeof(int))
				{
					this.compareDelegate = delegate(object x, object y)
					{
						if (x == null)
						{
							return -1;
						}
						if (y == null)
						{
							return 1;
						}
						int num = (int)x;
						int num2 = (int)y;
						if (num < num2)
						{
							return -1;
						}
						if (num > num2)
						{
							return 1;
						}
						return 0;
					};
					return new MailboxSyncQueryProcessor.CompareBySortColumn<int>(sortBy.SortOrder, this.idxSortColumn, this.compareDelegate);
				}
				throw new InvalidOperationException(ServerStrings.ExMatchShouldHaveBeenCalled);
			}

			// Token: 0x04005653 RID: 22099
			private static readonly object[][] EmptyResultSet = Array<object[]>.Empty;

			// Token: 0x04005654 RID: 22100
			private MailboxSyncQueryProcessor.ComparisonDelegate compareDelegate;

			// Token: 0x04005655 RID: 22101
			private int idxCurrentRow;

			// Token: 0x04005656 RID: 22102
			private int idxSortColumn;

			// Token: 0x04005657 RID: 22103
			private List<object[]> result;

			// Token: 0x02000E67 RID: 3687
			// (Invoke) Token: 0x06007FC2 RID: 32706
			private delegate bool FoundResultDelegate(int result);
		}

		// Token: 0x02000E68 RID: 3688
		internal class SlowQueryResult : MailboxSyncQueryProcessor.IQueryResult, ITableView, IPagedView, IDisposeTrackable, IDisposable
		{
			// Token: 0x06007FC5 RID: 32709 RVA: 0x0022F37F File Offset: 0x0022D57F
			public SlowQueryResult(Folder folder, ItemQueryType itemQueryType, QueryFilter filter, SortBy[] sortBy, PropertyDefinition[] queryColumns)
			{
				this.queryResult = folder.ItemQuery(itemQueryType, filter, sortBy, queryColumns);
				this.disposeTracker = this.GetDisposeTracker();
			}

			// Token: 0x17002208 RID: 8712
			// (get) Token: 0x06007FC6 RID: 32710 RVA: 0x0022F3A5 File Offset: 0x0022D5A5
			public int CurrentRow
			{
				get
				{
					return this.queryResult.CurrentRow;
				}
			}

			// Token: 0x17002209 RID: 8713
			// (get) Token: 0x06007FC7 RID: 32711 RVA: 0x0022F3B2 File Offset: 0x0022D5B2
			public int EstimatedRowCount
			{
				get
				{
					return this.queryResult.EstimatedRowCount;
				}
			}

			// Token: 0x06007FC8 RID: 32712 RVA: 0x0022F3BF File Offset: 0x0022D5BF
			public virtual DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<MailboxSyncQueryProcessor.SlowQueryResult>(this);
			}

			// Token: 0x06007FC9 RID: 32713 RVA: 0x0022F3C7 File Offset: 0x0022D5C7
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x06007FCA RID: 32714 RVA: 0x0022F3DC File Offset: 0x0022D5DC
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06007FCB RID: 32715 RVA: 0x0022F3EB File Offset: 0x0022D5EB
			public object[][] GetRows(int numRows)
			{
				return this.queryResult.GetRows(numRows);
			}

			// Token: 0x06007FCC RID: 32716 RVA: 0x0022F3F9 File Offset: 0x0022D5F9
			public bool SeekToCondition(SeekReference seekReference, QueryFilter seekCondition)
			{
				return this.queryResult.SeekToCondition(seekReference, seekCondition);
			}

			// Token: 0x06007FCD RID: 32717 RVA: 0x0022F408 File Offset: 0x0022D608
			public int SeekToOffset(SeekReference seekReference, int offset)
			{
				return this.queryResult.SeekToOffset(seekReference, offset);
			}

			// Token: 0x06007FCE RID: 32718 RVA: 0x0022F417 File Offset: 0x0022D617
			private void Dispose(bool disposing)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					this.InternalDispose(disposing);
				}
			}

			// Token: 0x06007FCF RID: 32719 RVA: 0x0022F42F File Offset: 0x0022D62F
			private void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					if (this.queryResult != null)
					{
						this.queryResult.Dispose();
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
			}

			// Token: 0x0400565F RID: 22111
			private readonly DisposeTracker disposeTracker;

			// Token: 0x04005660 RID: 22112
			private bool disposed;

			// Token: 0x04005661 RID: 22113
			private QueryResult queryResult;
		}

		// Token: 0x02000E69 RID: 3689
		private class CompareBySortColumn<elementType> : IComparer<object[]>
		{
			// Token: 0x06007FD0 RID: 32720 RVA: 0x0022F45A File Offset: 0x0022D65A
			public CompareBySortColumn(SortOrder sortOrder, int idxSortColumn, MailboxSyncQueryProcessor.ComparisonDelegate compareDelegate)
			{
				this.sortOrder = sortOrder;
				this.idxSortColumn = idxSortColumn;
				this.compareDelegate = compareDelegate;
			}

			// Token: 0x06007FD1 RID: 32721 RVA: 0x0022F478 File Offset: 0x0022D678
			int IComparer<object[]>.Compare(object[] x, object[] y)
			{
				int num = this.compareDelegate(x[this.idxSortColumn], y[this.idxSortColumn]);
				if (this.sortOrder != SortOrder.Ascending)
				{
					return -num;
				}
				return num;
			}

			// Token: 0x04005662 RID: 22114
			private MailboxSyncQueryProcessor.ComparisonDelegate compareDelegate;

			// Token: 0x04005663 RID: 22115
			private int idxSortColumn;

			// Token: 0x04005664 RID: 22116
			private SortOrder sortOrder;
		}
	}
}
