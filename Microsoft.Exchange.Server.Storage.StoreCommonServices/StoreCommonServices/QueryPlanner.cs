using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000FD RID: 253
	public sealed class QueryPlanner
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x0002F084 File Offset: 0x0002D284
		public QueryPlanner(Context context, Table table, object[] tableFunctionParameters, SearchCriteria allCriteria, SearchCriteria finalCriteria, SearchCriteria findRowCriteria, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, IReadOnlyDictionary<Column, Column> renameDictionary, CategorizedQueryParams categorizedQueryParams, IList<IIndex> lazyIndexesOnTable, SortOrder sortOrder, Bookmark bookmark, int skipTo, int maxRows, bool backwards, bool mustUseLazyIndex, bool canUseFullTextIndex, bool simplePlanOnly, bool allowUnrestrictedComplexPlans, QueryPlanner.Hints hints) : this(context, table, tableFunctionParameters, allCriteria, finalCriteria, findRowCriteria, columnsToFetch, longValueColumnsToPreread, renameDictionary, categorizedQueryParams, lazyIndexesOnTable, null, sortOrder, bookmark, skipTo, maxRows, backwards, mustUseLazyIndex, canUseFullTextIndex, simplePlanOnly, allowUnrestrictedComplexPlans, true, hints)
		{
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		public QueryPlanner(Context context, Table table, object[] tableFunctionParameters, SearchCriteria allCriteria, SearchCriteria finalCriteria, SearchCriteria findRowCriteria, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, IReadOnlyDictionary<Column, Column> renameDictionary, CategorizedQueryParams categorizedQueryParams, IList<IIndex> lazyIndexesOnTable, IList<IIndex> masterIndexesOnTable, SortOrder sortOrder, Bookmark bookmark, int skipTo, int maxRows, bool backwards, bool mustUseLazyIndex, bool canUseFullTextIndex, bool simplePlanOnly, bool allowUnrestrictedComplexPlans, bool canJoinToBaseTable, QueryPlanner.Hints hints) : this(context, table, tableFunctionParameters, QueryPlanner.AndCriteria(allCriteria, findRowCriteria, finalCriteria, (context.Culture == null) ? null : context.Culture.CompareInfo, false), null, columnsToFetch, longValueColumnsToPreread, renameDictionary, categorizedQueryParams, lazyIndexesOnTable, masterIndexesOnTable, sortOrder, bookmark, skipTo, maxRows, backwards, mustUseLazyIndex, canUseFullTextIndex, simplePlanOnly, allowUnrestrictedComplexPlans, canJoinToBaseTable, hints, null, 0)
		{
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002F120 File Offset: 0x0002D320
		private QueryPlanner(Context context, Table table, object[] tableFunctionParameters, SearchCriteria criteria, SearchCriteria outerCriteria, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, IReadOnlyDictionary<Column, Column> renameDictionary, CategorizedQueryParams categorizedQueryParams, IList<IIndex> lazyIndexesOnTable, IList<IIndex> masterIndexesOnTable, SortOrder sortOrder, Bookmark bookmark, int skipTo, int maxRows, bool backwards, bool mustUseLazyIndex, bool canUseFullTextIndex, bool simplePlanOnly, bool allowUnrestrictedComplexPlans, bool canJoinToBaseTable, QueryPlanner.Hints hints, IList<string> diagnosticTrace, int diagnosticTraceIndent)
		{
			this.context = context;
			this.table = table;
			this.lazyIndexes = (lazyIndexesOnTable ?? new List<IIndex>(0));
			this.masterIndexes = masterIndexesOnTable;
			this.eagerIndexes = table.Indexes;
			this.fullTextIndex = ((QueryPlanner.CanUseFullTextIndex && canUseFullTextIndex) ? ((FullTextIndex)table.FullTextIndex) : null);
			this.parameters = tableFunctionParameters;
			this.renameDictionary = renameDictionary;
			this.categorizedQueryParams = categorizedQueryParams;
			this.sortOrder = sortOrder;
			this.bookmark = bookmark;
			this.skipTo = skipTo;
			this.maxRows = maxRows;
			this.backwards = backwards;
			this.mustUseLazyIndex = mustUseLazyIndex;
			this.simplePlanOnly = simplePlanOnly;
			this.allowUnrestrictedComplexPlans = allowUnrestrictedComplexPlans;
			this.canJoinToBaseTable = canJoinToBaseTable;
			this.hints = hints;
			this.lowestCost = int.MaxValue;
			this.lowestCardinality = int.MaxValue;
			this.longValueColumnsToPreread = longValueColumnsToPreread;
			this.fullColumnsToFetch = ((columnsToFetch == null) ? new List<Column>(0) : new List<Column>(columnsToFetch));
			this.truncatedColumnsToFetch = new List<Column>(sortOrder.Count);
			if (sortOrder.Count > 0)
			{
				for (int i = 0; i < sortOrder.Columns.Count; i++)
				{
					Column item = sortOrder.Columns[i];
					if (!this.fullColumnsToFetch.Contains(item))
					{
						this.truncatedColumnsToFetch.Add(item);
					}
				}
			}
			if (this.fullColumnsToFetch.Count == 0 && this.truncatedColumnsToFetch.Count == 0)
			{
				this.truncatedColumnsToFetch.Add(QueryPlanner.FakeConstantColumn);
			}
			this.criteria = criteria;
			if (this.GetFullTextFlavor(criteria) <= FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
			{
				this.fullTextIndex = null;
			}
			this.outerCriteria = outerCriteria;
			if (!bookmark.IsBOT && !bookmark.IsEOT)
			{
				this.bookmarkCriteria = QueryPlanner.GetBookmarkCriteria(sortOrder, backwards, bookmark);
				ColumnRanges columnRanges = new ColumnRanges(this.criteria, this.CompareInfo);
				this.bookmarkCriteria = columnRanges.SimplifyCriteria(this.bookmarkCriteria, this.CompareInfo);
				if (this.bookmarkCriteria is SearchCriteriaTrue)
				{
					this.bookmark = (backwards ? Bookmark.EOT : Bookmark.BOT);
				}
				else if (this.bookmarkCriteria is SearchCriteriaFalse)
				{
					this.criteria = Factory.CreateSearchCriteriaFalse();
				}
			}
			QueryPlanner.PlanStackFrame item2 = new QueryPlanner.PlanStackFrame(default(SortOrder), null, this.skipTo == 0, this.maxRows == 0, this.criteria, false, this.sortOrder.IsEmpty || this.bookmark.IsBOT || this.bookmark.IsEOT, 0, false);
			this.planStack = new List<QueryPlanner.PlanStackFrame>(30);
			this.planStack.Add(item2);
			this.topOfStackIndex = 0;
			this.diagnosticTrace = diagnosticTrace;
			this.traceIndent = diagnosticTraceIndent;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002F3E2 File Offset: 0x0002D5E2
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0002F3E9 File Offset: 0x0002D5E9
		internal static bool CanUseFullTextIndex
		{
			get
			{
				return QueryPlanner.canUseFullTextIndex;
			}
			set
			{
				QueryPlanner.canUseFullTextIndex = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002F3F1 File Offset: 0x0002D5F1
		internal int PlanAlternativesConsidered
		{
			get
			{
				return this.planAlternativesConsidered;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002F3F9 File Offset: 0x0002D5F9
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0002F411 File Offset: 0x0002D611
		internal SimpleQueryOperator.SimpleQueryOperatorDefinition CurrentPlan
		{
			get
			{
				return this.planStack[this.topOfStackIndex].CurrentPlan;
			}
			set
			{
				this.planStack[this.topOfStackIndex].CurrentPlan = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002F42A File Offset: 0x0002D62A
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0002F442 File Offset: 0x0002D642
		internal int CurrentPlanCost
		{
			get
			{
				return this.planStack[this.topOfStackIndex].CurrentPlanCost;
			}
			set
			{
				this.planStack[this.topOfStackIndex].CurrentPlanCost = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002F45B File Offset: 0x0002D65B
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0002F473 File Offset: 0x0002D673
		internal SortOrder CurrentSortOrder
		{
			get
			{
				return this.planStack[this.topOfStackIndex].CurrentSortOrder;
			}
			set
			{
				this.planStack[this.topOfStackIndex].CurrentSortOrder = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002F48C File Offset: 0x0002D68C
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		internal ISet<Column> CurrentConstantColumns
		{
			get
			{
				return this.planStack[this.topOfStackIndex].CurrentConstantColumns;
			}
			set
			{
				this.planStack[this.topOfStackIndex].CurrentConstantColumns = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002F4BD File Offset: 0x0002D6BD
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0002F4D5 File Offset: 0x0002D6D5
		internal bool SkipToSatisfied
		{
			get
			{
				return this.planStack[this.topOfStackIndex].SkipToSatisfied;
			}
			set
			{
				this.planStack[this.topOfStackIndex].SkipToSatisfied = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002F4EE File Offset: 0x0002D6EE
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0002F506 File Offset: 0x0002D706
		internal bool MaxRowsSatisfied
		{
			get
			{
				return this.planStack[this.topOfStackIndex].MaxRowsSatisfied;
			}
			set
			{
				this.planStack[this.topOfStackIndex].MaxRowsSatisfied = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002F51F File Offset: 0x0002D71F
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0002F537 File Offset: 0x0002D737
		internal SearchCriteria RemainingCriteria
		{
			get
			{
				return this.planStack[this.topOfStackIndex].RemainingCriteria;
			}
			set
			{
				this.planStack[this.topOfStackIndex].RemainingCriteria = ((value is SearchCriteriaTrue) ? null : value);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002F55B File Offset: 0x0002D75B
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0002F573 File Offset: 0x0002D773
		internal bool ColumnsToFetchSatisfied
		{
			get
			{
				return this.planStack[this.topOfStackIndex].ColumnsToFetchSatisfied;
			}
			set
			{
				this.planStack[this.topOfStackIndex].ColumnsToFetchSatisfied = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0002F58C File Offset: 0x0002D78C
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0002F5A4 File Offset: 0x0002D7A4
		internal bool BookmarkSatisfied
		{
			get
			{
				return this.planStack[this.topOfStackIndex].BookmarkSatisfied;
			}
			set
			{
				this.planStack[this.topOfStackIndex].BookmarkSatisfied = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002F5BD File Offset: 0x0002D7BD
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0002F5D5 File Offset: 0x0002D7D5
		internal int CurrentCardinality
		{
			get
			{
				return this.planStack[this.topOfStackIndex].CurrentCardinality;
			}
			set
			{
				this.planStack[this.topOfStackIndex].CurrentCardinality = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002F5EE File Offset: 0x0002D7EE
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0002F606 File Offset: 0x0002D806
		internal bool IndexMayBeStale
		{
			get
			{
				return this.planStack[this.topOfStackIndex].IndexMayBeStale;
			}
			set
			{
				this.planStack[this.topOfStackIndex].IndexMayBeStale = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002F61F File Offset: 0x0002D81F
		private static Column FakeConstantColumn
		{
			get
			{
				return QueryPlanner.fakeConstantColumn;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002F626 File Offset: 0x0002D826
		private Dictionary<Column, FilterFactorHint> FilterFactorHints
		{
			get
			{
				if (this.hints != null)
				{
					return this.hints.FilterFactorHints;
				}
				return null;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002F63D File Offset: 0x0002D83D
		private CompareInfo CompareInfo
		{
			get
			{
				if (this.context.Culture != null)
				{
					return this.context.Culture.CompareInfo;
				}
				return null;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002F65E File Offset: 0x0002D85E
		private bool WantToTryMoreComplexPlans
		{
			get
			{
				return this.allowUnrestrictedComplexPlans || this.lowestCostPlan == null;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002F673 File Offset: 0x0002D873
		private int MaxNumberOfFullTextSupplementaryRestrictions
		{
			get
			{
				if (this.fullTextIndex != null)
				{
					return FullTextIndexSchema.Current.MaxNumberOfSupplementaryRestrictions;
				}
				return 0;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002F689 File Offset: 0x0002D889
		private bool IsDiagnosticTraceEnabled
		{
			get
			{
				return this.diagnosticTrace != null;
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002F874 File Offset: 0x0002DA74
		public static bool IsRegularOrCriteria(IList<object> keyPrefixValues, IIndex index, SearchCriteria criteria, CultureInfo culture)
		{
			SortOrder sortOrder = index.SortOrder;
			int num = (keyPrefixValues == null) ? 0 : keyPrefixValues.Count;
			if (!(criteria is SearchCriteriaOr) || sortOrder.Count <= num)
			{
				return false;
			}
			Column keyColumn = sortOrder[num].Column;
			bool isRegularOrCriteria = true;
			criteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaOr)
				{
					return criterion;
				}
				if (criterion is SearchCriteriaAnd)
				{
					SearchCriteriaAnd searchCriteriaAnd = criterion as SearchCriteriaAnd;
					if (searchCriteriaAnd.NestedCriteria.Length != 2)
					{
						isRegularOrCriteria = false;
					}
					else
					{
						SearchCriteria searchCriteria = searchCriteriaAnd.NestedCriteria[0];
						SearchCriteriaCompare searchCriteriaCompare = searchCriteria as SearchCriteriaCompare;
						searchCriteria = searchCriteriaAnd.NestedCriteria[1];
						SearchCriteriaCompare searchCriteriaCompare2 = searchCriteria as SearchCriteriaCompare;
						Column col = null;
						Column col2 = null;
						if (searchCriteriaCompare == null || searchCriteriaCompare2 == null || !(searchCriteriaCompare.Rhs is ConstantColumn) || !(searchCriteriaCompare2.Rhs is ConstantColumn) || !index.GetIndexColumn(searchCriteriaCompare.Lhs, true, out col) || !index.GetIndexColumn(searchCriteriaCompare2.Lhs, true, out col2) || col != keyColumn || col2 != keyColumn)
						{
							isRegularOrCriteria = false;
						}
						else if (((searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.GreaterThanEqual && searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.GreaterThan) || (searchCriteriaCompare2.RelOp != SearchCriteriaCompare.SearchRelOp.LessThanEqual && searchCriteriaCompare2.RelOp != SearchCriteriaCompare.SearchRelOp.LessThan)) && ((searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.LessThanEqual && searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.LessThan) || (searchCriteriaCompare2.RelOp != SearchCriteriaCompare.SearchRelOp.GreaterThanEqual && searchCriteriaCompare2.RelOp != SearchCriteriaCompare.SearchRelOp.GreaterThan)))
						{
							isRegularOrCriteria = false;
						}
					}
					return null;
				}
				if (criterion is SearchCriteriaCompare)
				{
					SearchCriteriaCompare searchCriteriaCompare3 = criterion as SearchCriteriaCompare;
					if (searchCriteriaCompare3 == null || !(searchCriteriaCompare3.Rhs is ConstantColumn) || (searchCriteriaCompare3.RelOp != SearchCriteriaCompare.SearchRelOp.LessThanEqual && searchCriteriaCompare3.RelOp != SearchCriteriaCompare.SearchRelOp.LessThan && searchCriteriaCompare3.RelOp != SearchCriteriaCompare.SearchRelOp.GreaterThanEqual && searchCriteriaCompare3.RelOp != SearchCriteriaCompare.SearchRelOp.GreaterThan && searchCriteriaCompare3.RelOp != SearchCriteriaCompare.SearchRelOp.Equal))
					{
						isRegularOrCriteria = false;
					}
					else
					{
						Column lhs = searchCriteriaCompare3.Lhs;
						if (!index.GetIndexColumn(searchCriteriaCompare3.Lhs, true, out lhs))
						{
							lhs = searchCriteriaCompare3.Lhs;
						}
						if (lhs != keyColumn)
						{
							isRegularOrCriteria = false;
						}
					}
					return null;
				}
				isRegularOrCriteria = false;
				return null;
			}, (culture == null) ? null : culture.CompareInfo, true);
			return isRegularOrCriteria;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002F978 File Offset: 0x0002DB78
		public static List<KeyRange> BuildKeyRangesFromOrCriteria(IList<object> keyPrefixValues, IIndex index, ref SearchCriteria criteria, bool descending, CultureInfo culture)
		{
			List<KeyRange> ranges = null;
			criteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (ranges != null)
				{
					return null;
				}
				if (criterion is SearchCriteriaAnd)
				{
					return criterion;
				}
				if (criterion is SearchCriteriaOr && QueryPlanner.IsRegularOrCriteria(keyPrefixValues, index, criterion, culture))
				{
					ranges = QueryPlanner.BuildKeyRangesFromRegularOrCriteria(keyPrefixValues, index, criterion, descending, culture);
					return Factory.CreateSearchCriteriaTrue();
				}
				return null;
			}, (culture == null) ? null : culture.CompareInfo, true);
			return ranges;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002FCBC File Offset: 0x0002DEBC
		public static List<KeyRange> BuildKeyRangesFromRegularOrCriteria(IList<object> keyPrefixValues, IIndex index, SearchCriteria criteria, bool descending, CultureInfo culture)
		{
			List<KeyRange> ranges = new List<KeyRange>(((SearchCriteriaOr)criteria).NestedCriteria.Length);
			int keyPrefixLength = (keyPrefixValues == null) ? 0 : keyPrefixValues.Count;
			criteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaOr)
				{
					return criterion;
				}
				KeyRange item;
				if (criterion is SearchCriteriaAnd)
				{
					SearchCriteriaAnd searchCriteriaAnd = criterion as SearchCriteriaAnd;
					SearchCriteriaCompare searchCriteriaCompare = searchCriteriaAnd.NestedCriteria[0] as SearchCriteriaCompare;
					ConstantColumn constantColumn = (ConstantColumn)searchCriteriaCompare.Rhs;
					bool inclusive = searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThanEqual || searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual;
					object[] array = new object[keyPrefixLength + 1];
					if (keyPrefixLength != 0)
					{
						keyPrefixValues.CopyTo(array, 0);
					}
					array[keyPrefixLength] = constantColumn.Value;
					SearchCriteriaCompare searchCriteriaCompare2 = searchCriteriaAnd.NestedCriteria[1] as SearchCriteriaCompare;
					ConstantColumn constantColumn2 = (ConstantColumn)searchCriteriaCompare2.Rhs;
					bool inclusive2 = searchCriteriaCompare2.RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThanEqual || searchCriteriaCompare2.RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual;
					object[] array2 = new object[keyPrefixLength + 1];
					if (keyPrefixLength != 0)
					{
						keyPrefixValues.CopyTo(array2, 0);
					}
					array2[keyPrefixLength] = constantColumn2.Value;
					if (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThan || searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThanEqual)
					{
						if (descending)
						{
							item = new KeyRange(new StartStopKey(inclusive2, array2), new StartStopKey(inclusive, array));
						}
						else
						{
							item = new KeyRange(new StartStopKey(inclusive, array), new StartStopKey(inclusive2, array2));
						}
					}
					else if (descending)
					{
						item = new KeyRange(new StartStopKey(inclusive, array), new StartStopKey(inclusive2, array2));
					}
					else
					{
						item = new KeyRange(new StartStopKey(inclusive2, array2), new StartStopKey(inclusive, array));
					}
				}
				else
				{
					if (!(criterion is SearchCriteriaCompare))
					{
						return null;
					}
					SearchCriteriaCompare searchCriteriaCompare3 = criterion as SearchCriteriaCompare;
					bool inclusive3 = searchCriteriaCompare3.RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThanEqual || searchCriteriaCompare3.RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual;
					object[] array3 = new object[keyPrefixLength + 1];
					if (keyPrefixLength != 0)
					{
						keyPrefixValues.CopyTo(array3, 0);
					}
					array3[keyPrefixLength] = ((ConstantColumn)searchCriteriaCompare3.Rhs).Value;
					if (searchCriteriaCompare3.RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
					{
						item = new KeyRange(new StartStopKey(true, array3), new StartStopKey(true, array3));
					}
					else if (searchCriteriaCompare3.RelOp == SearchCriteriaCompare.SearchRelOp.LessThan || searchCriteriaCompare3.RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual)
					{
						if (descending)
						{
							item = new KeyRange(new StartStopKey(inclusive3, array3), new StartStopKey(true, keyPrefixValues));
						}
						else
						{
							item = new KeyRange(new StartStopKey(true, keyPrefixValues), new StartStopKey(inclusive3, array3));
						}
					}
					else if (descending)
					{
						item = new KeyRange(new StartStopKey(true, keyPrefixValues), new StartStopKey(inclusive3, array3));
					}
					else
					{
						item = new KeyRange(new StartStopKey(inclusive3, array3), new StartStopKey(true, keyPrefixValues));
					}
				}
				ranges.Add(item);
				return null;
			}, (culture == null) ? null : culture.CompareInfo, true);
			return ranges;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002FD3C File Offset: 0x0002DF3C
		public CountOperator CreateCountPlan()
		{
			CountOperator.CountOperatorDefinition countOperatorDefinition = this.CreateCountPlanDefinition();
			if (countOperatorDefinition == null)
			{
				return null;
			}
			return countOperatorDefinition.CreateOperator(this.context);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002FD64 File Offset: 0x0002DF64
		public OrdinalPositionOperator CreateOrdinalPositionPlan()
		{
			OrdinalPositionOperator.OrdinalPositionOperatorDefinition ordinalPositionOperatorDefinition = this.CreateOrdinalPositionPlanDefinition();
			if (ordinalPositionOperatorDefinition == null)
			{
				return null;
			}
			return ordinalPositionOperatorDefinition.CreateOperator(this.context);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002FD8C File Offset: 0x0002DF8C
		public SimpleQueryOperator CreatePlan()
		{
			SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = this.CreatePlanDefinition();
			if (simpleQueryOperatorDefinition == null)
			{
				return null;
			}
			return simpleQueryOperatorDefinition.CreateOperator(this.context);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002FDB4 File Offset: 0x0002DFB4
		public SimpleQueryOperator CreatePlan(out int planCost, out int planCardinality)
		{
			SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = this.CreatePlanDefinition(out planCost, out planCardinality);
			if (simpleQueryOperatorDefinition == null)
			{
				return null;
			}
			return simpleQueryOperatorDefinition.CreateOperator(this.context);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		public override string ToString()
		{
			StringFormatOptions formatOptions = this.IsDiagnosticTraceEnabled ? StringFormatOptions.SkipParametersData : StringFormatOptions.IncludeDetails;
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("QueryPlanner: [");
			stringBuilder.Append("Table:[");
			stringBuilder.Append(this.table);
			if (this.parameters != null)
			{
				stringBuilder.Append("] parameters:[");
				stringBuilder.Append(this.parameters);
			}
			if (this.criteria != null)
			{
				stringBuilder.Append("] criteria:[");
				this.criteria.AppendToString(stringBuilder, formatOptions);
			}
			if (this.outerCriteria != null)
			{
				stringBuilder.Append("] outerCriteria:[");
				this.outerCriteria.AppendToString(stringBuilder, formatOptions);
			}
			stringBuilder.Append("] sortOrder:[");
			stringBuilder.Append(this.sortOrder);
			if (this.skipTo != 0)
			{
				stringBuilder.Append("] skipTo:[");
				stringBuilder.Append(this.skipTo);
			}
			if (this.maxRows != 0)
			{
				stringBuilder.Append("] maxRows:[");
				stringBuilder.Append(this.maxRows);
			}
			stringBuilder.Append("] backwards:[");
			stringBuilder.Append(this.backwards);
			stringBuilder.Append("] simplePlanOnly:[");
			stringBuilder.Append(this.simplePlanOnly);
			stringBuilder.Append("] mustUseLazyIndex:[");
			stringBuilder.Append(this.mustUseLazyIndex);
			if (this.lazyIndexes != null)
			{
				stringBuilder.Append("] lazyIndexes:[");
				for (int i = 0; i < this.lazyIndexes.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.lazyIndexes[i]);
				}
			}
			if (this.masterIndexes != null && this.masterIndexes != this.lazyIndexes)
			{
				stringBuilder.Append("] masterIndexes:[");
				for (int j = 0; j < this.masterIndexes.Count; j++)
				{
					if (j != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.masterIndexes[j]);
				}
			}
			stringBuilder.Append("] eagerIndexes:[");
			for (int k = 0; k < this.eagerIndexes.Count; k++)
			{
				if (k != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(this.eagerIndexes[k]);
			}
			if (this.fullTextIndex != null)
			{
				stringBuilder.Append("] fullTextIndex:[");
				stringBuilder.Append(this.fullTextIndex);
			}
			stringBuilder.Append("] bookmark:[");
			stringBuilder.Append(this.bookmark);
			stringBuilder.Append("] fullColumnsToFetch:[");
			for (int l = 0; l < this.fullColumnsToFetch.Count; l++)
			{
				if (l != 0)
				{
					stringBuilder.Append(", ");
				}
				this.fullColumnsToFetch[l].AppendToString(stringBuilder, formatOptions);
			}
			stringBuilder.Append("] truncatedColumnsToFetch:[");
			for (int m = 0; m < this.truncatedColumnsToFetch.Count; m++)
			{
				if (m != 0)
				{
					stringBuilder.Append(", ");
				}
				this.truncatedColumnsToFetch[m].AppendToString(stringBuilder, formatOptions);
			}
			stringBuilder.Append("]]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00030108 File Offset: 0x0002E308
		public SimpleQueryOperator.SimpleQueryOperatorDefinition CreatePlanDefinition()
		{
			int num;
			int num2;
			return this.CreatePlanDefinition(out num, out num2);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00030120 File Offset: 0x0002E320
		public CountOperator.CountOperatorDefinition CreateCountPlanDefinition()
		{
			List<Column> list = this.fullColumnsToFetch;
			List<Column> list2 = this.truncatedColumnsToFetch;
			CountOperator.CountOperatorDefinition countOperatorDefinition = null;
			try
			{
				Column item = (this.categorizedQueryParams != null) ? PropertySchema.MapToColumn(this.context.Database, ObjectType.Message, PropTag.Message.ContentCount) : QueryPlanner.FakeConstantColumn;
				this.fullColumnsToFetch = new List<Column>(0);
				this.truncatedColumnsToFetch = new List<Column>(1);
				this.truncatedColumnsToFetch.Add(item);
				countOperatorDefinition = new CountOperator.CountOperatorDefinition(this.context.Culture, this.CreatePlanDefinition(), true);
				countOperatorDefinition.AttachPlanner(QueryPlanner.QueryPlanTrackerCount.Create(this));
			}
			finally
			{
				this.fullColumnsToFetch = list;
				this.truncatedColumnsToFetch = list2;
			}
			return countOperatorDefinition;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000301D0 File Offset: 0x0002E3D0
		public void EnableDiagnosticTrace()
		{
			this.diagnosticTrace = new List<string>(25);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000301DF File Offset: 0x0002E3DF
		public void AddDiagnosticTrace(string step)
		{
			if (this.IsDiagnosticTraceEnabled && !string.IsNullOrEmpty(step))
			{
				this.diagnosticTrace.Add(step);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000301FD File Offset: 0x0002E3FD
		public IList<string> GetDiagnosticTrace()
		{
			return this.diagnosticTrace;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00030205 File Offset: 0x0002E405
		internal static void Initialize()
		{
			QueryPlanner.fakeConstantColumn = Factory.CreateConstantColumn("fake", typeof(int), 4, 0, 1);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00030228 File Offset: 0x0002E428
		internal static IReadOnlyDictionary<Column, Column> GetEffectiveRenameDictionary(IIndex index, IReadOnlyDictionary<Column, Column> additionalRenameDictionary)
		{
			int num = 0;
			if (index.RenameDictionary == null || index.RenameDictionary.Count == 0)
			{
				return additionalRenameDictionary;
			}
			if (additionalRenameDictionary == null || additionalRenameDictionary.Count == 0)
			{
				return index.RenameDictionary;
			}
			num += index.RenameDictionary.Count;
			num += additionalRenameDictionary.Count;
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(num);
			foreach (KeyValuePair<Column, Column> keyValuePair in index.RenameDictionary)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			Dictionary<Column, Column> dictionary2 = additionalRenameDictionary as Dictionary<Column, Column>;
			if (dictionary2 != null)
			{
				using (Dictionary<Column, Column>.Enumerator enumerator2 = dictionary2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<Column, Column> keyValuePair2 = enumerator2.Current;
						dictionary[keyValuePair2.Key] = keyValuePair2.Value;
					}
					return dictionary;
				}
			}
			foreach (KeyValuePair<Column, Column> keyValuePair3 in additionalRenameDictionary)
			{
				dictionary[keyValuePair3.Key] = keyValuePair3.Value;
			}
			return dictionary;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00030374 File Offset: 0x0002E574
		internal static SearchCriteria GetBookmarkCriteria(SortOrder sortOrder, bool backwards, Bookmark bookmark)
		{
			SearchCriteria result = null;
			if (bookmark.KeyValues != null)
			{
				if (bookmark.KeyValues.Count > 1)
				{
					SearchCriteria[] array = new SearchCriteria[bookmark.KeyValues.Count];
					for (int i = 0; i < bookmark.KeyValues.Count; i++)
					{
						SearchCriteria searchCriteria = Factory.CreateSearchCriteriaCompare(sortOrder.Columns[i], QueryPlanner.GetCompareRelOp(sortOrder.Ascending[i], backwards, i == bookmark.KeyValues.Count - 1, bookmark.PositionedOn), Factory.CreateConstantColumn(bookmark.KeyValues[i], sortOrder.Columns[i]));
						if (i == 0)
						{
							array[i] = searchCriteria;
						}
						else
						{
							SearchCriteria[] array2 = new SearchCriteria[i + 1];
							for (int j = 0; j < i; j++)
							{
								array2[j] = Factory.CreateSearchCriteriaCompare(sortOrder.Columns[j], SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(bookmark.KeyValues[j], sortOrder.Columns[j]));
							}
							array2[i] = searchCriteria;
							array[i] = Factory.CreateSearchCriteriaAnd(array2);
						}
					}
					result = Factory.CreateSearchCriteriaOr(array);
				}
				else if (bookmark.KeyValues.Count == 1)
				{
					result = Factory.CreateSearchCriteriaCompare(sortOrder.Columns[0], QueryPlanner.GetCompareRelOp(sortOrder.Ascending[0], backwards, true, bookmark.PositionedOn), Factory.CreateConstantColumn(bookmark.KeyValues[0], sortOrder.Columns[0]));
				}
			}
			return result;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00030500 File Offset: 0x0002E700
		internal SimpleQueryOperator.SimpleQueryOperatorDefinition CreatePlanDefinition(out int planCost, out int planCardinality)
		{
			int maxValue = int.MaxValue;
			SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition;
			if (!this.TryCreatePlanDefinition(true, maxValue, out simpleQueryOperatorDefinition, out planCost, out planCardinality))
			{
				throw new StoreException((LID)43605U, ErrorCodeValue.TooComplex, "Failed to build a query plan");
			}
			if (simpleQueryOperatorDefinition != null)
			{
				simpleQueryOperatorDefinition.AttachPlanner(QueryPlanner.QueryPlanTrackerCost.Create(this));
			}
			return simpleQueryOperatorDefinition;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0003054C File Offset: 0x0002E74C
		internal OrdinalPositionOperator.OrdinalPositionOperatorDefinition CreateOrdinalPositionPlanDefinition()
		{
			Bookmark bookmark = this.bookmark;
			List<Column> list = this.fullColumnsToFetch;
			List<Column> list2 = this.truncatedColumnsToFetch;
			OrdinalPositionOperator.OrdinalPositionOperatorDefinition ordinalPositionOperatorDefinition = null;
			try
			{
				bool flag = this.categorizedQueryParams != null;
				this.bookmark = Bookmark.BOT;
				this.fullColumnsToFetch = new List<Column>(0);
				this.truncatedColumnsToFetch = new List<Column>(this.sortOrder.Count + (flag ? 1 : 0));
				for (int i = 0; i < this.sortOrder.Columns.Count; i++)
				{
					this.truncatedColumnsToFetch.Add(this.sortOrder.Columns[i]);
				}
				if (flag)
				{
					Column item = PropertySchema.MapToColumn(this.context.Database, ObjectType.Message, PropTag.Message.ContentCount);
					if (!this.truncatedColumnsToFetch.Contains(item))
					{
						this.truncatedColumnsToFetch.Add(item);
					}
				}
				ordinalPositionOperatorDefinition = new OrdinalPositionOperator.OrdinalPositionOperatorDefinition(this.context.Culture, this.CreatePlanDefinition(), this.sortOrder, new StartStopKey(bookmark.PositionedOn, bookmark.KeyValues), true);
				ordinalPositionOperatorDefinition.AttachPlanner(QueryPlanner.QueryPlanTrackerOrdinal.Create(this));
			}
			finally
			{
				this.bookmark = bookmark;
				this.fullColumnsToFetch = list;
				this.truncatedColumnsToFetch = list2;
			}
			return ordinalPositionOperatorDefinition;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000306A8 File Offset: 0x0002E8A8
		private static bool ColumnsContained(IList<Column> firstColumns, IList<Column> secondColumns)
		{
			if (secondColumns == null || secondColumns.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < secondColumns.Count; i++)
			{
				if (!firstColumns.Contains(secondColumns[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000306E8 File Offset: 0x0002E8E8
		private static List<Column> MergeColumns(List<Column> columns, List<Column> additionalColumns)
		{
			if (additionalColumns == null || additionalColumns.Count == 0)
			{
				return columns;
			}
			if (columns == null || columns.Count == 0)
			{
				return additionalColumns;
			}
			bool flag = false;
			foreach (Column item in additionalColumns)
			{
				if (!columns.Contains(item))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return columns;
			}
			List<Column> list = new List<Column>(columns.Count + additionalColumns.Count);
			list.AddRange(columns);
			foreach (Column item2 in additionalColumns)
			{
				if (!list.Contains(item2))
				{
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000307C0 File Offset: 0x0002E9C0
		private static bool CanAcceptTruncatedLhsColumnForCompare(Column rhsColumn)
		{
			if (rhsColumn is ConstantColumn)
			{
				ConstantColumn constantColumn = (ConstantColumn)rhsColumn;
				if ((constantColumn.Type == typeof(string) && constantColumn.Value != null && ((string)constantColumn.Value).Length < 127) || (constantColumn.Type == typeof(byte[]) && constantColumn.Value != null && ((byte[])constantColumn.Value).Length < 255))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00030848 File Offset: 0x0002EA48
		private static int EstimateSize(IList<Column> columns)
		{
			int num = 0;
			for (int i = 0; i < columns.Count; i++)
			{
				Column column = columns[i];
				int num2 = Math.Max(column.MaxLength, column.Size);
				num2 = Math.Min(num2, 500);
				num += num2;
			}
			return Math.Min(num, 32768);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000308A0 File Offset: 0x0002EAA0
		private static int EstimateCost(int avgRecordSize, int numberOfRows, bool probe)
		{
			int result;
			if (probe)
			{
				result = 1 + numberOfRows;
			}
			else
			{
				double num = (double)(32768 / avgRecordSize);
				result = 1 + (int)((double)numberOfRows / num);
			}
			return result;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000308CC File Offset: 0x0002EACC
		private static int ComputeRowsRemaining(int predicates, int cardinality)
		{
			int num = cardinality;
			int num2 = 0;
			while ((double)num2 < Math.Min((double)predicates, 4.5) && num > 1)
			{
				num = (int)(0.8 * (double)num);
				num2++;
			}
			return num;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00030B60 File Offset: 0x0002ED60
		private static int ComputeRowsRemaining(SearchCriteria criteria, CompareInfo compareInfo, Dictionary<Column, FilterFactorHint> filterFactorHints, int cardinality)
		{
			if (criteria != null)
			{
				criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo innerCompareInfo)
				{
					if (cardinality <= 1 || (double)cardinality < 4.5)
					{
						return null;
					}
					if (criterion is SearchCriteriaOr)
					{
						SearchCriteriaOr searchCriteriaOr = (SearchCriteriaOr)criterion;
						if (searchCriteriaOr.NestedCriteria.Length != 0)
						{
							int num = 0;
							foreach (SearchCriteria searchCriteria in searchCriteriaOr.NestedCriteria)
							{
								num = Math.Max(num, QueryPlanner.ComputeRowsRemaining(searchCriteria, compareInfo, filterFactorHints, cardinality));
							}
							cardinality = num;
						}
						else
						{
							cardinality = 1;
						}
						return null;
					}
					if (criterion is SearchCriteriaTrue)
					{
						return null;
					}
					if (criterion is SearchCriteriaFalse)
					{
						cardinality = 1;
						return null;
					}
					if (criterion is SearchCriteriaNot)
					{
						int num2 = QueryPlanner.ComputeRowsRemaining(((SearchCriteriaNot)criterion).Criteria, compareInfo, filterFactorHints, cardinality);
						if (num2 > (int)((double)cardinality * 0.8) || num2 < (int)((double)cardinality * 0.2))
						{
							cardinality = Math.Max(1, cardinality - num2);
						}
						else
						{
							cardinality = Math.Max(1, (int)(0.8 * (double)cardinality));
						}
						return null;
					}
					if (criterion is SearchCriteriaAnd || criterion is SearchCriteriaNear)
					{
						return criterion;
					}
					if (filterFactorHints != null && criterion is SearchCriteriaCompare)
					{
						SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
						if (searchCriteriaCompare.Rhs is ConstantColumn)
						{
							Column lhs = searchCriteriaCompare.Lhs;
							ConstantColumn constantColumn = (ConstantColumn)searchCriteriaCompare.Rhs;
							FilterFactorHint filterFactorHint;
							if (filterFactorHints.TryGetValue(lhs, out filterFactorHint) && filterFactorHint.IsEquality && (filterFactorHint.AnyValue || ValueHelper.ValuesEqual(filterFactorHint.Value, constantColumn.Value)))
							{
								if (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
								{
									cardinality = Math.Max(1, (int)((1.0 - filterFactorHint.FilterFactor) * (double)cardinality));
									return criterion;
								}
								if (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.NotEqual)
								{
									cardinality = Math.Max(1, (int)(filterFactorHint.FilterFactor * (double)cardinality));
									return criterion;
								}
							}
						}
					}
					cardinality = Math.Max(1, (int)(0.8 * (double)cardinality));
					return null;
				}, compareInfo, false);
			}
			return cardinality;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00030BB4 File Offset: 0x0002EDB4
		private static int TotalCost(List<QueryPlanner.AndOrLeg> legs)
		{
			int num = 0;
			for (int i = 0; i < legs.Count; i++)
			{
				num += legs[i].PlanCost;
			}
			return num;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00030BE4 File Offset: 0x0002EDE4
		private static SearchCriteria AndCriteria(SearchCriteria criteria1, SearchCriteria criteria2, SearchCriteria criteria3, CompareInfo compareInfo, bool simplifyNegation)
		{
			SearchCriteria searchCriteria = criteria1;
			if (criteria2 != null)
			{
				searchCriteria = ((searchCriteria == null) ? criteria2 : Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					searchCriteria,
					criteria2
				}));
			}
			if (criteria3 != null)
			{
				searchCriteria = ((searchCriteria == null) ? criteria3 : Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					searchCriteria,
					criteria3
				}));
			}
			if (searchCriteria != null)
			{
				return searchCriteria.InspectAndFix(null, compareInfo, simplifyNegation);
			}
			return null;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00030C40 File Offset: 0x0002EE40
		private static SearchCriteriaCompare.SearchRelOp GetCompareRelOp(bool sortOrderAscending, bool backwards, bool lastLeg, bool bookmarkPositionedOn)
		{
			bool flag = sortOrderAscending ^ backwards;
			bool flag2 = lastLeg && (bookmarkPositionedOn ^ backwards);
			if (!flag)
			{
				if (!flag2)
				{
					return SearchCriteriaCompare.SearchRelOp.LessThan;
				}
				return SearchCriteriaCompare.SearchRelOp.LessThanEqual;
			}
			else
			{
				if (!flag2)
				{
					return SearchCriteriaCompare.SearchRelOp.GreaterThan;
				}
				return SearchCriteriaCompare.SearchRelOp.GreaterThanEqual;
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00030C6C File Offset: 0x0002EE6C
		private static bool IsColumnMaterializedInIndex(IIndex index, Column column, bool acceptTruncated)
		{
			Column column2;
			return index.GetIndexColumn(column, acceptTruncated, out column2);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00030C84 File Offset: 0x0002EE84
		private static bool AreColumnsSatisfiableByIndexes(IReadOnlyDictionary<Column, Column> renameDictionary, IIndex index, IIndex secondIndex, bool acceptTruncated, IList<Column> columns)
		{
			for (int i = 0; i < columns.Count; i++)
			{
				if (!QueryPlanner.IsColumnSatisfiableByIndexes(renameDictionary, index, secondIndex, acceptTruncated, columns[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00030CBC File Offset: 0x0002EEBC
		private static bool IsColumnSatisfiableByIndexes(IReadOnlyDictionary<Column, Column> renameDictionary, IIndex index, IIndex secondIndex, bool acceptTruncated, Column column)
		{
			Column column2 = null;
			if (renameDictionary == null || !renameDictionary.TryGetValue(column, out column2))
			{
				column2 = column;
			}
			if (!QueryPlanner.IsColumnMaterializedInIndex(index, column2, acceptTruncated) && (secondIndex == null || !QueryPlanner.IsColumnMaterializedInIndex(secondIndex, column2, acceptTruncated)) && !(column2.ActualColumn is ConstantColumn))
			{
				Column[] argumentColumns = column2.ActualColumn.ArgumentColumns;
				if (argumentColumns == null)
				{
					return false;
				}
				if (!QueryPlanner.AreColumnsSatisfiableByIndexes(renameDictionary, index, secondIndex, acceptTruncated, argumentColumns))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00030D21 File Offset: 0x0002EF21
		private static bool IsColumnSatisfiableByIndex(IIndex index, Column column, bool acceptTruncated)
		{
			return QueryPlanner.IsColumnSatisfiableByIndexes(null, index, null, acceptTruncated, column);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00030D84 File Offset: 0x0002EF84
		private static int CountFilteringLegs(SearchCriteria criteria, CompareInfo compareInfo)
		{
			int legs = 0;
			if (criteria != null)
			{
				criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo innerCompareInfo)
				{
					if (!(criterion is SearchCriteriaTrue) && !(criterion is SearchCriteriaFalse) && !(criterion is SearchCriteriaOr) && !(criterion is SearchCriteriaAnd) && !(criterion is SearchCriteriaNear) && !(criterion is SearchCriteriaNot))
					{
						legs++;
					}
					return criterion;
				}, compareInfo, false);
			}
			return legs;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		private FullTextIndexSchema.CriteriaFullTextFlavor GetFullTextFlavor(SearchCriteria criteria)
		{
			bool flag;
			return this.GetFullTextFlavor(criteria, out flag);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00030DDA File Offset: 0x0002EFDA
		private FullTextIndexSchema.CriteriaFullTextFlavor GetFullTextFlavor(SearchCriteria criteria, out bool needsResidual)
		{
			if (this.fullTextIndex != null)
			{
				return FullTextIndexSchema.Current.GetCriteriaFullTextFlavor(criteria, this.context.Database.MdbGuid, out needsResidual);
			}
			needsResidual = true;
			return FullTextIndexSchema.CriteriaFullTextFlavor.CannotBeServiced;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00030E08 File Offset: 0x0002F008
		private QueryPlanner.AndOrLeg CreateFullTextIndexAccessLeg(SearchCriteria indexCriteria, IList<Column> indexColumnsToFetch)
		{
			if (this.outerCriteria != null)
			{
				SearchCriteria searchCriteria;
				SearchCriteria searchCriteria2;
				this.ExtractAndSimplifyIndexCriteriaForFullText(this.outerCriteria, out searchCriteria, out searchCriteria2);
				if (searchCriteria != null)
				{
					indexCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
					{
						indexCriteria,
						searchCriteria
					});
					indexCriteria = indexCriteria.InspectAndFix(null, this.CompareInfo, true);
				}
			}
			StoreFullTextIndexQuery storeFullTextIndexQuery = new StoreFullTextIndexQuery(indexCriteria, this.context.Culture);
			SimpleQueryOperator.SimpleQueryOperatorDefinition planOperator = new TableFunctionOperator.TableFunctionOperatorDefinition(this.context.Culture, (TableFunction)this.fullTextIndex.IndexTable, new object[]
			{
				storeFullTextIndexQuery
			}, indexColumnsToFetch, null, QueryPlanner.GetEffectiveRenameDictionary(this.fullTextIndex, this.renameDictionary), 0, 0, KeyRange.AllRows, false, true);
			int planCost = QueryPlanner.CountFilteringLegs(indexCriteria, this.CompareInfo);
			int planCardinality = QueryPlanner.ComputeRowsRemaining(indexCriteria, this.CompareInfo, this.FilterFactorHints, 90000);
			return new QueryPlanner.AndOrLeg(planOperator, planCost, planCardinality);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00030EEC File Offset: 0x0002F0EC
		private bool TryCreatePlanDefinition(bool topLevel, int planCostFloor, out SimpleQueryOperator.SimpleQueryOperatorDefinition plan, out int planCost, out int planCardinality)
		{
			if (ExTraceGlobals.QueryPlannerSummaryTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
			{
				this.TraceSummary("Starting to plan for " + this.ToString());
			}
			this.traceIndent++;
			plan = null;
			planCost = 0;
			planCardinality = 0;
			this.lowestCost = planCostFloor;
			if (this.criteria is SearchCriteriaFalse)
			{
				this.TraceSummary("Trivially empty - criteria is false");
				return true;
			}
			if (this.bookmark.IsBOT)
			{
				if (this.backwards)
				{
					this.TraceSummary("Trivially empty - BOT with backward query");
					return true;
				}
			}
			else if (this.bookmark.IsEOT && !this.backwards)
			{
				this.TraceSummary("Trivially empty - EOT with forward query");
				return true;
			}
			if (this.categorizedQueryParams != null)
			{
				this.CreatePlanForCategorizedView();
			}
			else
			{
				for (int i = 0; i < this.lazyIndexes.Count; i++)
				{
					IPseudoIndex pseudoIndex = (IPseudoIndex)this.lazyIndexes[i];
					bool flag;
					this.CreatePlanWithJustLazyIndex(pseudoIndex, null, topLevel, out flag);
					if (flag)
					{
						this.TraceSummary("CreatePlanWithJustLazyIndex determined that the plan is empty");
						return true;
					}
					if (this.perfectLazyIndexPlan)
					{
						break;
					}
					if (this.masterIndexes != null && this.context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
					{
						for (int j = 0; j < this.masterIndexes.Count; j++)
						{
							IPseudoIndex pseudoIndex2 = (IPseudoIndex)this.masterIndexes[j];
							if (pseudoIndex2 != pseudoIndex && pseudoIndex2.ShouldBeCurrent && pseudoIndex2.LogicalSortOrder.Count <= pseudoIndex.Columns.Count)
							{
								for (int k = 0; k < pseudoIndex2.LogicalSortOrder.Columns.Count; k++)
								{
									if (!QueryPlanner.IsColumnSatisfiableByIndex(pseudoIndex, pseudoIndex2.LogicalSortOrder.Columns[k], true))
									{
										pseudoIndex2 = null;
										break;
									}
								}
								if (pseudoIndex2 != null)
								{
									this.CreatePlanWithJustLazyIndex(pseudoIndex, pseudoIndex2, topLevel, out flag);
								}
							}
						}
					}
				}
				if (!this.mustUseLazyIndex)
				{
					for (int l = 0; l < this.eagerIndexes.Count; l++)
					{
						bool flag2;
						this.CreatePlanWithJustEagerIndex(this.eagerIndexes[l], out flag2);
						if (flag2)
						{
							this.TraceSummary("CreatePlanWithJustEagerIndex determined that the plan is empty");
							return true;
						}
					}
				}
				if (this.fullTextIndex != null)
				{
					this.CreatePlanWithJustFullTextIndex();
				}
				if (!this.simplePlanOnly && this.WantToTryMoreComplexPlans && !this.perfectLazyIndexPlan && !this.perfectEagerIndexPlan)
				{
					if (this.RemainingCriteria is SearchCriteriaAnd)
					{
						this.CreateANDPlan(new List<QueryPlanner.AndOrLeg>(0), null);
					}
					else if (this.RemainingCriteria is SearchCriteriaOr)
					{
						this.CreateORPlan(new List<QueryPlanner.AndOrLeg>(0), null);
					}
					else if (this.RemainingCriteria is SearchCriteriaNot)
					{
						this.CreateNOTPlan();
					}
					if (!this.allowUnrestrictedComplexPlans && this.lowestCost > 270000)
					{
						this.TraceSummary("Resulting plan cost is too high");
						DiagnosticContext.TraceLocation((LID)35872U);
						return false;
					}
				}
			}
			this.traceIndent--;
			if (this.lowestCostPlan == null)
			{
				this.TraceSummary("Failed to build a plan.");
				DiagnosticContext.TraceLocation((LID)52256U);
				return false;
			}
			this.TraceSummary("Final plan", null, this.lowestCost, this.lowestCardinality, this.lowestCostPlan, this.planAlternativesConsidered);
			plan = this.lowestCostPlan;
			planCost = this.lowestCost;
			planCardinality = this.lowestCardinality;
			this.lowestCostPlan = null;
			return true;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00031258 File Offset: 0x0002F458
		private void BuildAndTestIndexORPlan(List<QueryPlanner.AndOrLeg> legs, List<Column> indexColumnsToFetch)
		{
			if (legs.Count > 1 && this.RemainingCriteria == null)
			{
				this.TraceDetail("Testing OR plan tree that is only residual join/sort atop these legs", legs, null, null, 0, 0, null, 0);
				this.traceIndent++;
				this.planAlternativesConsidered++;
				int num = 0;
				int num2 = 0;
				SimpleQueryOperator.SimpleQueryOperatorDefinition[] array = new SimpleQueryOperator.SimpleQueryOperatorDefinition[legs.Count];
				for (int i = 0; i < legs.Count; i++)
				{
					array[i] = legs[i].PlanOperator;
					num = Math.Max(num, legs[i].PlanCardinality);
					num2 += legs[i].PlanCost;
				}
				int startTopOfStackIndex = this.PushPlanStack();
				SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan = new IndexOrOperator.IndexOrOperatorDefinition(this.context.Culture, indexColumnsToFetch, array, true);
				this.CurrentPlan = currentPlan;
				this.CurrentPlanCost = num2;
				this.CurrentCardinality = num;
				this.ColumnsToFetchSatisfied = (QueryPlanner.ColumnsContained(indexColumnsToFetch, this.fullColumnsToFetch) && QueryPlanner.ColumnsContained(indexColumnsToFetch, this.truncatedColumnsToFetch));
				this.CurrentSortOrder = default(SortOrder);
				this.CurrentConstantColumns = null;
				this.TestPlanWithAndWithoutResidualJoinAndSort(false);
				this.PopPlanStack(startTopOfStackIndex);
				this.traceIndent--;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00031390 File Offset: 0x0002F590
		private void BuildAndTestIndexANDPlan(List<QueryPlanner.AndOrLeg> legs, List<Column> indexColumnsToFetch)
		{
			if (legs.Count == 1)
			{
				this.TraceDetail("Testing AND plan tree that is only residual join/sort atop this leg", legs, null, null, 0, 0, null, 0);
				this.traceIndent++;
				this.planAlternativesConsidered++;
				int startTopOfStackIndex = this.PushPlanStack();
				this.CurrentPlan = legs[0].PlanOperator;
				this.CurrentPlanCost = legs[0].PlanCost;
				this.CurrentCardinality = legs[0].PlanCardinality;
				this.ColumnsToFetchSatisfied = (QueryPlanner.ColumnsContained(indexColumnsToFetch, this.fullColumnsToFetch) && QueryPlanner.ColumnsContained(indexColumnsToFetch, this.truncatedColumnsToFetch));
				this.CurrentSortOrder = default(SortOrder);
				this.CurrentConstantColumns = null;
				this.TestPlanWithAndWithoutResidualJoinAndSort(false);
				this.PopPlanStack(startTopOfStackIndex);
				this.traceIndent--;
				return;
			}
			this.TraceDetail("Testing AND plan tree that is only residual join/sort atop these legs", legs, null, null, 0, 0, null, 0);
			this.traceIndent++;
			this.planAlternativesConsidered++;
			int num = 90000;
			int num2 = 0;
			SimpleQueryOperator.SimpleQueryOperatorDefinition[] array = new SimpleQueryOperator.SimpleQueryOperatorDefinition[legs.Count];
			int num3 = 0;
			foreach (QueryPlanner.AndOrLeg andOrLeg in from lg in legs
			orderby lg.PlanCardinality
			select lg)
			{
				array[num3++] = andOrLeg.PlanOperator;
				num = Math.Min(num, andOrLeg.PlanCardinality);
				num2 += andOrLeg.PlanCost;
			}
			int startTopOfStackIndex2 = this.PushPlanStack();
			SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan = new IndexAndOperator.IndexAndOperatorDefinition(this.context.Culture, indexColumnsToFetch, array, true);
			this.CurrentPlan = currentPlan;
			this.CurrentPlanCost = num2;
			this.CurrentCardinality = num;
			this.ColumnsToFetchSatisfied = (QueryPlanner.ColumnsContained(indexColumnsToFetch, this.fullColumnsToFetch) && QueryPlanner.ColumnsContained(indexColumnsToFetch, this.truncatedColumnsToFetch));
			this.CurrentSortOrder = default(SortOrder);
			this.CurrentConstantColumns = null;
			this.TestPlanWithAndWithoutResidualJoinAndSort(false);
			this.PopPlanStack(startTopOfStackIndex2);
			this.traceIndent--;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000315C0 File Offset: 0x0002F7C0
		private void CreateANDPlan(List<QueryPlanner.AndOrLeg> currentLegs, List<Column> indexColumnsToFetch)
		{
			this.TraceDetail("Testing AND plan tree rooted with ", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
			this.traceIndent++;
			this.planAlternativesConsidered++;
			if (this.fullTextIndex != null && currentLegs.Count == 0)
			{
				this.TraceDetail("Testing full text index as next leg for AND plan tree");
				this.traceIndent++;
				this.planAlternativesConsidered++;
				if (indexColumnsToFetch == null)
				{
					indexColumnsToFetch = this.GetBaseTableIndexColumns(this.fullTextIndex);
				}
				SearchCriteria remainingCriteria;
				QueryPlanner.AndOrLeg andOrLeg = this.CreateFullTextIndexAndLeg(indexColumnsToFetch, out remainingCriteria);
				if (andOrLeg != null)
				{
					if (QueryPlanner.TotalCost(currentLegs) + andOrLeg.PlanCost < this.lowestCost)
					{
						List<QueryPlanner.AndOrLeg> list = new List<QueryPlanner.AndOrLeg>(currentLegs);
						list.Add(andOrLeg);
						int startTopOfStackIndex = this.PushPlanStack();
						this.RemainingCriteria = remainingCriteria;
						this.BuildAndTestIndexANDPlan(list, indexColumnsToFetch);
						if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
						{
							this.CreateANDPlan(list, indexColumnsToFetch);
						}
						this.PopPlanStack(startTopOfStackIndex);
					}
					else
					{
						this.TraceDetail("Discarding (too costly) AND plan tree rooted with ", currentLegs, andOrLeg, null, QueryPlanner.TotalCost(currentLegs) + andOrLeg.PlanCost, 0, null, 0);
					}
				}
				else
				{
					this.TraceDetail("Discarding (index does not help)");
				}
				this.traceIndent--;
			}
			for (int i = 0; i < this.lazyIndexes.Count; i++)
			{
				IPseudoIndex pseudoIndex = (IPseudoIndex)this.lazyIndexes[i];
				this.TraceDetail("Testing lazy index " + pseudoIndex.IndexTable.Name + " as next leg for AND plan tree");
				this.traceIndent++;
				this.planAlternativesConsidered++;
				if (indexColumnsToFetch == null)
				{
					indexColumnsToFetch = this.GetBaseTableIndexColumns(pseudoIndex);
				}
				SearchCriteria remainingCriteria2;
				QueryPlanner.AndOrLeg andOrLeg2 = this.CreateIndexAndLeg(pseudoIndex, indexColumnsToFetch, out remainingCriteria2);
				if (andOrLeg2 != null)
				{
					if (QueryPlanner.TotalCost(currentLegs) + andOrLeg2.PlanCost < this.lowestCost)
					{
						List<QueryPlanner.AndOrLeg> list2 = new List<QueryPlanner.AndOrLeg>(currentLegs);
						list2.Add(andOrLeg2);
						int startTopOfStackIndex2 = this.PushPlanStack();
						this.RemainingCriteria = remainingCriteria2;
						if (!pseudoIndex.ShouldBeCurrent)
						{
							this.IndexMayBeStale = true;
						}
						this.BuildAndTestIndexANDPlan(list2, indexColumnsToFetch);
						if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
						{
							this.CreateANDPlan(list2, indexColumnsToFetch);
						}
						this.PopPlanStack(startTopOfStackIndex2);
					}
					else
					{
						this.TraceDetail("Discarding (too costly) AND plan tree rooted with ", currentLegs, andOrLeg2, null, QueryPlanner.TotalCost(currentLegs) + andOrLeg2.PlanCost, 0, null, 0);
					}
				}
				else
				{
					this.TraceDetail("Discarding (index does not help)");
				}
				this.traceIndent--;
			}
			if ((this.lazyIndexes.Count > 0 || this.fullTextIndex != null) && this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
			{
				this.ComplexNextLegForAND(currentLegs, indexColumnsToFetch);
			}
			this.traceIndent--;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00031874 File Offset: 0x0002FA74
		private void ComplexNextLegForAND(List<QueryPlanner.AndOrLeg> currentLegs, List<Column> indexColumnsToFetch)
		{
			this.TraceDetail("Starting complex plan as next leg for AND ", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
			this.traceIndent++;
			this.planAlternativesConsidered++;
			SearchCriteria searchCriteria = null;
			SearchCriteriaAnd searchCriteriaAnd = this.RemainingCriteria as SearchCriteriaAnd;
			if (searchCriteriaAnd != null && this.GetFullTextFlavor(searchCriteriaAnd) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
			{
				foreach (SearchCriteria searchCriteria2 in searchCriteriaAnd.NestedCriteria)
				{
					if (this.GetFullTextFlavor(searchCriteria2) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
					{
						searchCriteria = searchCriteria2;
						break;
					}
				}
			}
			else
			{
				if (currentLegs.Count == 0)
				{
					this.TraceDetail("Discarding (criteria not complex enough for a first AND leg)", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
					this.traceIndent--;
					return;
				}
				searchCriteria = this.RemainingCriteria;
			}
			if (!this.CriteriaComplexEnoughForRecursion(searchCriteria) && this.lowestCostPlan != null)
			{
				this.TraceDetail("Discarding (criteria not complex enough for recursion)", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
				this.traceIndent--;
				return;
			}
			if (searchCriteria is SearchCriteriaNot && currentLegs.Count > 0)
			{
				this.TraceDetail("Next leg is a NOT and this is not the first leg of AND, so test it recursively");
				SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition = null;
				QueryPlanner queryPlanner = new QueryPlanner(this.context, this.table, this.parameters, ((SearchCriteriaNot)searchCriteria).Criteria, (this.outerCriteria == null) ? this.criteria : this.outerCriteria, indexColumnsToFetch, null, this.renameDictionary, this.categorizedQueryParams, this.lazyIndexes, this.masterIndexes, SortOrder.Empty, Bookmark.BOT, 0, 0, false, this.mustUseLazyIndex, this.fullTextIndex != null, false, this.allowUnrestrictedComplexPlans, this.canJoinToBaseTable, this.hints, this.diagnosticTrace, this.traceIndent);
				int planCostFloor = this.lowestCost - this.CurrentPlanCost - QueryPlanner.TotalCost(currentLegs);
				int num;
				int num2;
				if (queryPlanner.TryCreatePlanDefinition(false, planCostFloor, out simpleQueryOperatorDefinition, out num, out num2))
				{
				}
				this.planAlternativesConsidered += queryPlanner.PlanAlternativesConsidered;
				if (simpleQueryOperatorDefinition != null)
				{
					int num3;
					int num4;
					SimpleQueryOperator.SimpleQueryOperatorDefinition queryOperatorDefinition;
					if (currentLegs.Count > 1)
					{
						num3 = 90000;
						num4 = 0;
						SimpleQueryOperator.SimpleQueryOperatorDefinition[] array = new SimpleQueryOperator.SimpleQueryOperatorDefinition[currentLegs.Count];
						int num5 = 0;
						foreach (QueryPlanner.AndOrLeg andOrLeg in from lg in currentLegs
						orderby lg.PlanCardinality
						select lg)
						{
							array[num5++] = andOrLeg.PlanOperator;
							num3 = Math.Min(num3, andOrLeg.PlanCardinality);
							num4 += andOrLeg.PlanCost;
						}
						queryOperatorDefinition = new IndexAndOperator.IndexAndOperatorDefinition(this.context.Culture, indexColumnsToFetch, array, true);
					}
					else
					{
						num3 = currentLegs[0].PlanCardinality;
						num4 = currentLegs[0].PlanCost;
						queryOperatorDefinition = currentLegs[0].PlanOperator;
					}
					int startTopOfStackIndex = this.PushPlanStack();
					this.RemainingCriteria = this.RemoveCriteria(searchCriteriaAnd, searchCriteria);
					this.CurrentPlanCost += num4 + num;
					this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(searchCriteria, this.CompareInfo, this.FilterFactorHints, num3);
					this.ColumnsToFetchSatisfied = (QueryPlanner.ColumnsContained(indexColumnsToFetch, this.fullColumnsToFetch) && QueryPlanner.ColumnsContained(indexColumnsToFetch, this.truncatedColumnsToFetch));
					this.CurrentSortOrder = default(SortOrder);
					this.CurrentConstantColumns = null;
					SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition2 = new IndexNotOperator.IndexNotOperatorDefinition(this.context.Culture, indexColumnsToFetch, queryOperatorDefinition, simpleQueryOperatorDefinition, true);
					if (this.RemainingCriteria == null)
					{
						this.CurrentPlan = simpleQueryOperatorDefinition2;
						this.TestPlanWithAndWithoutResidualJoinAndSort(false);
					}
					else
					{
						QueryPlanner.AndOrLeg item = new QueryPlanner.AndOrLeg(simpleQueryOperatorDefinition2, this.CurrentPlanCost, this.CurrentCardinality);
						List<QueryPlanner.AndOrLeg> list = new List<QueryPlanner.AndOrLeg>(1);
						list.Add(item);
						this.CurrentPlanCost = 0;
						this.CurrentCardinality = 0;
						if (QueryPlanner.TotalCost(list) < this.lowestCost)
						{
							this.BuildAndTestIndexANDPlan(list, indexColumnsToFetch);
							if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
							{
								this.CreateANDPlan(list, indexColumnsToFetch);
							}
						}
						else
						{
							this.TraceDetail("Discarding (too costly)", list, null, null, QueryPlanner.TotalCost(list), 0, null, 0);
						}
					}
					this.PopPlanStack(startTopOfStackIndex);
				}
				else
				{
					this.TraceDetail("Discarding (did not get a valid plan)");
				}
			}
			else
			{
				this.TraceDetail("Next leg is not a NOT or this is the first leg of AND, so test it recursively");
				QueryPlanner queryPlanner2 = new QueryPlanner(this.context, this.table, this.parameters, searchCriteria, (this.outerCriteria == null) ? this.criteria : this.outerCriteria, indexColumnsToFetch, null, this.renameDictionary, this.categorizedQueryParams, this.lazyIndexes, this.masterIndexes, SortOrder.Empty, Bookmark.BOT, 0, 0, false, this.mustUseLazyIndex, this.fullTextIndex != null, false, this.allowUnrestrictedComplexPlans, this.canJoinToBaseTable, this.hints, this.diagnosticTrace, this.traceIndent);
				int planCostFloor2 = this.lowestCost - this.CurrentPlanCost - QueryPlanner.TotalCost(currentLegs);
				SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition3;
				int planCost;
				int planCardinality;
				if (queryPlanner2.TryCreatePlanDefinition(false, planCostFloor2, out simpleQueryOperatorDefinition3, out planCost, out planCardinality))
				{
				}
				this.planAlternativesConsidered += queryPlanner2.PlanAlternativesConsidered;
				if (simpleQueryOperatorDefinition3 != null)
				{
					QueryPlanner.AndOrLeg andOrLeg2 = new QueryPlanner.AndOrLeg(simpleQueryOperatorDefinition3, planCost, planCardinality);
					if (this.CurrentPlanCost + QueryPlanner.TotalCost(currentLegs) + andOrLeg2.PlanCost < this.lowestCost)
					{
						List<QueryPlanner.AndOrLeg> list2 = new List<QueryPlanner.AndOrLeg>(currentLegs);
						list2.Add(andOrLeg2);
						int startTopOfStackIndex2 = this.PushPlanStack();
						this.RemainingCriteria = this.RemoveCriteria(searchCriteriaAnd, searchCriteria);
						this.BuildAndTestIndexANDPlan(list2, indexColumnsToFetch);
						if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
						{
							this.CreateANDPlan(list2, indexColumnsToFetch);
						}
						this.PopPlanStack(startTopOfStackIndex2);
					}
					else
					{
						this.TraceDetail("Discarding (too costly)", currentLegs, andOrLeg2, null, this.CurrentPlanCost + QueryPlanner.TotalCost(currentLegs) + andOrLeg2.PlanCost, 0, null, 0);
					}
				}
				else
				{
					this.TraceDetail("Discarding (did not get a valid plan)");
				}
			}
			this.traceIndent--;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00031E3C File Offset: 0x0003003C
		private QueryPlanner.AndOrLeg GetCheapestOuterLegForNot(IList<Column> indexColumnsToFetch)
		{
			int num = int.MaxValue;
			IPseudoIndex pseudoIndex = null;
			for (int i = 0; i < this.lazyIndexes.Count; i++)
			{
				IPseudoIndex pseudoIndex2 = (IPseudoIndex)this.lazyIndexes[i];
				int num2 = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(pseudoIndex2.Columns), 90000, false);
				if (num2 < num)
				{
					pseudoIndex = pseudoIndex2;
					num = num2;
				}
			}
			if (pseudoIndex != null)
			{
				return this.CreateIndexAccessLeg(pseudoIndex, Factory.CreateSearchCriteriaTrue(), indexColumnsToFetch);
			}
			Index index = null;
			for (int j = 0; j < this.eagerIndexes.Count; j++)
			{
				Index index2 = this.eagerIndexes[j];
				int num3 = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(index2.Columns), 90000, false);
				if (num3 < num)
				{
					index = index2;
					num = num3;
				}
			}
			return this.CreateBaseTableAccessLeg(index, indexColumnsToFetch);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00031F08 File Offset: 0x00030108
		private void CreateNOTPlan()
		{
			this.TraceDetail("Testing top level NOT plan");
			this.traceIndent++;
			this.planAlternativesConsidered++;
			List<Column> list = QueryPlanner.MergeColumns(this.fullColumnsToFetch, this.truncatedColumnsToFetch);
			QueryPlanner.AndOrLeg cheapestOuterLegForNot = this.GetCheapestOuterLegForNot(list);
			if (cheapestOuterLegForNot == null)
			{
				this.TraceDetail("Discarding (cannot generate outer step for not)", null, null, null, 0, 0, null, 0);
				this.traceIndent--;
				return;
			}
			if (cheapestOuterLegForNot.PlanCost >= this.lowestCost)
			{
				this.TraceDetail("Discarding (cheapest driving leg is too costly)", null, null, null, cheapestOuterLegForNot.PlanCost, 0, null, 0);
				this.traceIndent--;
				return;
			}
			int num = 0;
			int num2 = 0;
			SearchCriteriaNot searchCriteriaNot = (SearchCriteriaNot)this.RemainingCriteria;
			SearchCriteria searchCriteria = searchCriteriaNot.Criteria;
			QueryPlanner queryPlanner = new QueryPlanner(this.context, this.table, this.parameters, searchCriteria, (this.outerCriteria == null) ? this.criteria : this.outerCriteria, list, null, this.renameDictionary, this.categorizedQueryParams, this.lazyIndexes, this.masterIndexes, SortOrder.Empty, Bookmark.BOT, 0, 0, false, this.mustUseLazyIndex, this.fullTextIndex != null, false, this.allowUnrestrictedComplexPlans, this.canJoinToBaseTable, this.hints, this.diagnosticTrace, this.traceIndent);
			int planCostFloor = this.lowestCost - cheapestOuterLegForNot.PlanCost;
			SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition;
			if (queryPlanner.TryCreatePlanDefinition(false, planCostFloor, out simpleQueryOperatorDefinition, out num, out num2))
			{
			}
			this.planAlternativesConsidered += queryPlanner.PlanAlternativesConsidered;
			if (simpleQueryOperatorDefinition != null)
			{
				SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan = new IndexNotOperator.IndexNotOperatorDefinition(this.context.Culture, list, cheapestOuterLegForNot.PlanOperator, simpleQueryOperatorDefinition, true);
				int startTopOfStackIndex = this.PushPlanStack();
				this.RemainingCriteria = null;
				this.CurrentPlan = currentPlan;
				this.CurrentSortOrder = default(SortOrder);
				this.CurrentConstantColumns = null;
				this.CurrentPlanCost = cheapestOuterLegForNot.PlanCost + num;
				this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(searchCriteriaNot, this.CompareInfo, this.FilterFactorHints, cheapestOuterLegForNot.PlanCardinality);
				this.ColumnsToFetchSatisfied = (QueryPlanner.ColumnsContained(list, this.fullColumnsToFetch) && QueryPlanner.ColumnsContained(list, this.truncatedColumnsToFetch));
				this.TestPlanWithAndWithoutResidualJoinAndSort(false);
				this.PopPlanStack(startTopOfStackIndex);
			}
			else
			{
				this.TraceDetail("Discarding (could not build cheap enough NOT leg)");
			}
			this.traceIndent--;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00032158 File Offset: 0x00030358
		private void CreateORPlan(List<QueryPlanner.AndOrLeg> currentLegs, List<Column> indexColumnsToFetch)
		{
			this.TraceDetail("Testing OR plan tree rooted with ", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
			this.traceIndent++;
			this.planAlternativesConsidered++;
			if (this.fullTextIndex != null && currentLegs.Count == 0)
			{
				this.TraceDetail("Testing full text index as next leg for OR plan tree");
				this.traceIndent++;
				if (indexColumnsToFetch == null)
				{
					indexColumnsToFetch = this.GetBaseTableIndexColumns(this.fullTextIndex);
				}
				SearchCriteria remainingCriteria;
				QueryPlanner.AndOrLeg andOrLeg = this.CreateFullTextIndexOrLeg(indexColumnsToFetch, out remainingCriteria);
				if (andOrLeg != null)
				{
					if (QueryPlanner.TotalCost(currentLegs) + andOrLeg.PlanCost < this.lowestCost)
					{
						List<QueryPlanner.AndOrLeg> list = new List<QueryPlanner.AndOrLeg>(currentLegs);
						list.Add(andOrLeg);
						int startTopOfStackIndex = this.PushPlanStack();
						this.RemainingCriteria = remainingCriteria;
						this.BuildAndTestIndexORPlan(list, indexColumnsToFetch);
						if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
						{
							this.CreateORPlan(list, indexColumnsToFetch);
						}
						this.PopPlanStack(startTopOfStackIndex);
					}
					else
					{
						this.TraceDetail("Discarding (too costly) OR plan tree rooted with ", currentLegs, andOrLeg, null, QueryPlanner.TotalCost(currentLegs) + andOrLeg.PlanCost, 0, null, 0);
					}
				}
				else
				{
					this.TraceDetail("Discarding (index does not help)");
				}
				this.traceIndent--;
			}
			for (int i = 0; i < this.lazyIndexes.Count; i++)
			{
				IPseudoIndex pseudoIndex = (IPseudoIndex)this.lazyIndexes[i];
				if (ExTraceGlobals.QueryPlannerDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
				{
					this.TraceDetail("Testing lazy index " + pseudoIndex.IndexTable.Name + " as next leg for OR plan tree");
				}
				this.traceIndent++;
				this.planAlternativesConsidered++;
				if (indexColumnsToFetch == null)
				{
					indexColumnsToFetch = this.GetBaseTableIndexColumns(pseudoIndex);
				}
				SearchCriteria remainingCriteria2;
				QueryPlanner.AndOrLeg andOrLeg2 = this.CreateIndexOrLeg(pseudoIndex, indexColumnsToFetch, out remainingCriteria2);
				if (andOrLeg2 != null)
				{
					if (QueryPlanner.TotalCost(currentLegs) + andOrLeg2.PlanCost < this.lowestCost)
					{
						List<QueryPlanner.AndOrLeg> list2 = new List<QueryPlanner.AndOrLeg>(currentLegs);
						list2.Add(andOrLeg2);
						int startTopOfStackIndex2 = this.PushPlanStack();
						if (!pseudoIndex.ShouldBeCurrent)
						{
							this.IndexMayBeStale = true;
						}
						this.RemainingCriteria = remainingCriteria2;
						this.BuildAndTestIndexORPlan(list2, indexColumnsToFetch);
						if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
						{
							this.CreateORPlan(list2, indexColumnsToFetch);
						}
						this.PopPlanStack(startTopOfStackIndex2);
					}
					else
					{
						this.TraceDetail("Discarding (too costly) OR plan tree rooted with ", currentLegs, andOrLeg2, null, QueryPlanner.TotalCost(currentLegs) + andOrLeg2.PlanCost, 0, null, 0);
					}
				}
				else
				{
					this.TraceDetail("Discarding (index does not help)");
				}
				this.traceIndent--;
			}
			if ((this.lazyIndexes.Count > 0 || this.fullTextIndex != null) && this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
			{
				this.ComplexNextLegForOR(currentLegs, indexColumnsToFetch);
			}
			this.traceIndent--;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0003240C File Offset: 0x0003060C
		private void ComplexNextLegForOR(List<QueryPlanner.AndOrLeg> currentLegs, List<Column> indexColumnsToFetch)
		{
			this.TraceDetail("Starting complex plan as next leg for OR plan tree rooted with ", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
			this.traceIndent++;
			this.planAlternativesConsidered++;
			SearchCriteria searchCriteria = null;
			SearchCriteriaOr searchCriteriaOr = this.RemainingCriteria as SearchCriteriaOr;
			if (searchCriteriaOr != null && this.GetFullTextFlavor(searchCriteriaOr) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
			{
				foreach (SearchCriteria searchCriteria2 in searchCriteriaOr.NestedCriteria)
				{
					if (this.GetFullTextFlavor(searchCriteria2) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
					{
						searchCriteria = searchCriteria2;
						break;
					}
				}
			}
			else
			{
				if (currentLegs.Count == 0)
				{
					this.TraceDetail("Discarding (criteria not complex enough for a first OR leg)", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
					this.traceIndent--;
					return;
				}
				searchCriteria = this.RemainingCriteria;
			}
			if (!this.CriteriaComplexEnoughForRecursion(searchCriteria) && currentLegs.Count == 0)
			{
				this.TraceDetail("Discarding (criteria not complex enough for recursion)", currentLegs, null, this.RemainingCriteria, 0, 0, null, 0);
				this.traceIndent--;
				return;
			}
			QueryPlanner queryPlanner = new QueryPlanner(this.context, this.table, this.parameters, searchCriteria, (this.outerCriteria == null) ? this.criteria : this.outerCriteria, indexColumnsToFetch, null, this.renameDictionary, this.categorizedQueryParams, this.lazyIndexes, this.masterIndexes, SortOrder.Empty, Bookmark.BOT, 0, 0, false, this.mustUseLazyIndex, this.fullTextIndex != null, false, this.allowUnrestrictedComplexPlans, this.canJoinToBaseTable, this.hints, this.diagnosticTrace, this.traceIndent);
			int planCostFloor = this.lowestCost - this.CurrentPlanCost - QueryPlanner.TotalCost(currentLegs);
			SimpleQueryOperator.SimpleQueryOperatorDefinition simpleQueryOperatorDefinition;
			int planCost;
			int planCardinality;
			if (queryPlanner.TryCreatePlanDefinition(false, planCostFloor, out simpleQueryOperatorDefinition, out planCost, out planCardinality))
			{
			}
			this.planAlternativesConsidered += queryPlanner.PlanAlternativesConsidered;
			if (simpleQueryOperatorDefinition != null)
			{
				QueryPlanner.AndOrLeg andOrLeg = new QueryPlanner.AndOrLeg(simpleQueryOperatorDefinition, planCost, planCardinality);
				if (QueryPlanner.TotalCost(currentLegs) + andOrLeg.PlanCost < this.lowestCost)
				{
					List<QueryPlanner.AndOrLeg> list = new List<QueryPlanner.AndOrLeg>(currentLegs);
					list.Add(andOrLeg);
					int startTopOfStackIndex = this.PushPlanStack();
					this.RemainingCriteria = this.RemoveCriteria(searchCriteriaOr, searchCriteria);
					this.BuildAndTestIndexORPlan(list, indexColumnsToFetch);
					if (this.RemainingCriteria != null && this.WantToTryMoreComplexPlans)
					{
						this.CreateORPlan(list, indexColumnsToFetch);
					}
					this.PopPlanStack(startTopOfStackIndex);
				}
				else
				{
					this.TraceDetail("Discarding (too costly) OR plan tree that is rooted with these legs", currentLegs, andOrLeg, null, QueryPlanner.TotalCost(currentLegs) + andOrLeg.PlanCost, 0, null, 0);
				}
			}
			else
			{
				this.TraceDetail("Discarding (did not get valid next leg)");
			}
			this.traceIndent--;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00032680 File Offset: 0x00030880
		private void CreatePlanWithJustEagerIndex(Index index, out bool emptyResultSet)
		{
			emptyResultSet = false;
			if (ExTraceGlobals.QueryPlannerDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
			{
				this.TraceDetail("Testing with just eager index " + index.Name);
			}
			this.traceIndent++;
			this.planAlternativesConsidered++;
			bool flag2;
			bool flag = !SortOrder.IsMatch(this.sortOrder, index.LogicalSortOrder, index.ConstantColumns, out flag2);
			bool flag3 = !flag && (flag2 ^ this.backwards);
			SearchCriteria searchCriteria = this.RemainingCriteria;
			bool flag4 = false;
			IList<KeyRange> list;
			this.ComputeKeyRanges(index, this.sortOrder, this.bookmark, flag3, ref searchCriteria, out list, ref flag4);
			if (searchCriteria is SearchCriteriaFalse)
			{
				this.TraceDetail("Stopping (result set is empty)");
				emptyResultSet = true;
				return;
			}
			if (index.IndexTable.IsPartitioned && !this.KeyRangesValidForPartitionedTable(list))
			{
				this.TraceDetail("Discarding (eager index plan cannot be used because the table is partitioned and we cannot create partitioning key)");
				this.traceIndent--;
				DiagnosticContext.TraceLocation((LID)35413U);
				return;
			}
			searchCriteria = ((searchCriteria is SearchCriteriaTrue) ? null : searchCriteria);
			if (this.GetFullTextFlavor(searchCriteria) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
			{
				this.TraceDetail("Discarding (criteria cannot be done residually)");
				this.traceIndent--;
				return;
			}
			if (!flag4)
			{
				searchCriteria = QueryPlanner.AndCriteria(searchCriteria, this.bookmarkCriteria, null, this.CompareInfo, true);
			}
			int startTopOfStackIndex = this.PushPlanStack();
			this.BookmarkSatisfied = true;
			List<Column> columnsToFetch = QueryPlanner.MergeColumns(this.fullColumnsToFetch, this.truncatedColumnsToFetch);
			SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan;
			if (index.IndexTable is TableFunction)
			{
				currentPlan = new TableFunctionOperator.TableFunctionOperatorDefinition(this.context.Culture, (TableFunction)index.IndexTable, this.parameters, columnsToFetch, searchCriteria, index.RenameDictionary, flag ? 0 : this.skipTo, flag ? 0 : this.maxRows, list, flag3, true);
			}
			else
			{
				currentPlan = new TableOperator.TableOperatorDefinition(this.context.Culture, index.IndexTable, index, columnsToFetch, this.longValueColumnsToPreread, searchCriteria, this.renameDictionary, flag ? 0 : this.skipTo, flag ? 0 : this.maxRows, list, flag3, true, true);
			}
			this.CurrentPlan = currentPlan;
			this.RemainingCriteria = null;
			this.ColumnsToFetchSatisfied = true;
			if (!flag)
			{
				this.SkipToSatisfied = true;
				this.MaxRowsSatisfied = true;
			}
			bool sortIsUnnecessary = false;
			int num;
			if (index.Unique && list.Count == 1)
			{
				StartStopKey startKey = list[0].StartKey;
				if (startKey.Count == index.ColumnCount)
				{
					StartStopKey stopKey = list[0].StopKey;
					if (stopKey.Count == index.ColumnCount)
					{
						StartStopKey startKey2 = list[0].StartKey;
						IList<object> values = startKey2.Values;
						StartStopKey stopKey2 = list[0].StopKey;
						if (ValueHelper.ListsEqual<object>(values, stopKey2.Values))
						{
							num = 1;
							sortIsUnnecessary = true;
							if (!this.SkipToSatisfied)
							{
								emptyResultSet = true;
								return;
							}
							this.MaxRowsSatisfied = true;
							goto IL_4E7;
						}
					}
				}
			}
			int num2 = int.MaxValue;
			for (int i = 0; i < list.Count; i++)
			{
				StartStopKey startKey3 = list[i].StartKey;
				int count = startKey3.Count;
				StartStopKey stopKey3 = list[i].StopKey;
				int val = Math.Max(count, stopKey3.Count);
				num2 = Math.Min(num2, val);
			}
			num = QueryPlanner.ComputeRowsRemaining(num2, 90000);
			if (!flag && this.maxRows > 0 && this.skipTo + this.maxRows < 500)
			{
				if (searchCriteria == null || searchCriteria is SearchCriteriaTrue)
				{
					num = Math.Min(num, this.skipTo + this.maxRows);
				}
				else
				{
					num = QueryPlanner.ComputeRowsRemaining(1, num);
				}
			}
			if (this.FilterFactorHints != null)
			{
				int num3 = 0;
				for (int j = 0; j < list.Count; j++)
				{
					int num4 = 90000;
					int num5 = 0;
					for (;;)
					{
						int num6 = num5;
						StartStopKey startKey4 = list[j].StartKey;
						int count2 = startKey4.Count;
						StartStopKey stopKey4 = list[j].StopKey;
						if (num6 >= Math.Min(count2, stopKey4.Count))
						{
							break;
						}
						StartStopKey startKey5 = list[j].StartKey;
						object x = startKey5.Values[num5];
						StartStopKey stopKey5 = list[j].StopKey;
						if (!ValueHelper.ValuesEqual(x, stopKey5.Values[num5]))
						{
							break;
						}
						Column key = index.LogicalSortOrder.Columns[num5];
						FilterFactorHint filterFactorHint;
						if (this.FilterFactorHints.TryGetValue(key, out filterFactorHint) && filterFactorHint.IsEquality)
						{
							if (!filterFactorHint.AnyValue)
							{
								object value = filterFactorHint.Value;
								StartStopKey startKey6 = list[j].StartKey;
								if (!ValueHelper.ValuesEqual(value, startKey6.Values[num5]))
								{
									goto IL_47D;
								}
							}
							num4 = Math.Max(1, Math.Min(num4, (int)((1.0 - filterFactorHint.FilterFactor) * 90000.0)));
						}
						IL_47D:
						num5++;
					}
					num3 = Math.Max(num3, num4);
				}
				num = Math.Min(num, num3);
			}
			IL_4E7:
			if (index.PrimaryKey)
			{
				this.CurrentPlanCost = QueryPlanner.EstimateCost(24576, num, false);
				this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(searchCriteria, this.CompareInfo, this.FilterFactorHints, num);
			}
			else
			{
				int num7 = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(index.Columns), num, false);
				int num8 = QueryPlanner.EstimateCost(24576, num, true);
				int currentCardinality = QueryPlanner.ComputeRowsRemaining(searchCriteria, this.CompareInfo, this.FilterFactorHints, num);
				this.CurrentCardinality = currentCardinality;
				this.CurrentPlanCost = num7 + num8;
			}
			this.CurrentSortOrder = (flag2 ? index.LogicalSortOrder.Reverse() : index.LogicalSortOrder);
			this.CurrentConstantColumns = index.ConstantColumns;
			bool flag5 = this.TestPlanWithAndWithoutResidualJoinAndSort(sortIsUnnecessary);
			if (flag5)
			{
				this.perfectLazyIndexPlan = false;
				if (num == 1)
				{
					this.perfectEagerIndexPlan = true;
				}
			}
			this.PopPlanStack(startTopOfStackIndex);
			this.traceIndent--;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00032C5C File Offset: 0x00030E5C
		private void CreatePlanWithJustLazyIndex(IPseudoIndex index, IPseudoIndex probeMasterIndex, bool topLevel, out bool emptyResultSet)
		{
			emptyResultSet = false;
			if (ExTraceGlobals.QueryPlannerDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
			{
				if (probeMasterIndex == null)
				{
					this.TraceDetail(string.Format("Testing with just lazy index {0}", index.IndexTable.Name));
				}
				else
				{
					this.TraceDetail(string.Format("Testing join of lazy index {0} with lazy index {1}", index.IndexTable.Name, probeMasterIndex.IndexTable.Name));
				}
			}
			this.traceIndent++;
			this.planAlternativesConsidered++;
			int num = (index.ConstantColumns == null) ? 0 : index.ConstantColumns.Count;
			bool flag2;
			bool flag = !SortOrder.IsMatch(this.sortOrder, index.LogicalSortOrder, index.ConstantColumns, out flag2);
			bool flag3 = !flag && (flag2 ^ this.backwards);
			SearchCriteria searchCriteria = this.RemainingCriteria;
			bool flag4 = false;
			IList<KeyRange> list;
			this.ComputeKeyRanges(index, this.sortOrder, this.bookmark, flag3, ref searchCriteria, out list, ref flag4);
			if (searchCriteria is SearchCriteriaFalse)
			{
				this.TraceDetail("Stopping (result set is empty)");
				this.traceIndent--;
				emptyResultSet = true;
				return;
			}
			searchCriteria = ((searchCriteria is SearchCriteriaTrue) ? null : searchCriteria);
			if (this.GetFullTextFlavor(searchCriteria) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
			{
				this.TraceDetail("Discarding (criteria cannot be done residually)");
				this.traceIndent--;
				return;
			}
			if (!flag4)
			{
				searchCriteria = QueryPlanner.AndCriteria(searchCriteria, this.bookmarkCriteria, null, this.CompareInfo, true);
			}
			int startTopOfStackIndex = this.PushPlanStack();
			this.BookmarkSatisfied = true;
			SearchCriteria searchCriteria2;
			this.ExtractIndexCriteria(searchCriteria, index, out searchCriteria2, out searchCriteria);
			List<Column> list2 = new List<Column>(this.fullColumnsToFetch.Count + this.truncatedColumnsToFetch.Count);
			bool flag5 = probeMasterIndex == null && this.IndexOnly(index, searchCriteria);
			if (!flag5)
			{
				if (probeMasterIndex != null)
				{
					if (!this.TwoIndexOnly(index, probeMasterIndex, searchCriteria))
					{
						this.TraceDetail("Discarding (joined index can't satisfy query)");
						this.traceIndent--;
						this.PopPlanStack(startTopOfStackIndex);
						return;
					}
					for (int i = 0; i < probeMasterIndex.IndexKeyPrefix.Count; i++)
					{
						list2.Add(Factory.CreateConstantColumn(probeMasterIndex.IndexKeyPrefix[i], probeMasterIndex.SortOrder.Columns[i]));
					}
					list2.AddRange(probeMasterIndex.LogicalSortOrder.Columns);
				}
				else
				{
					if (!this.canJoinToBaseTable)
					{
						this.TraceDetail("Discarding (lazy index cant satisfy query, no master index, join to base table disallowed)");
						this.traceIndent--;
						this.PopPlanStack(startTopOfStackIndex);
						return;
					}
					list2.AddRange(this.GetBaseTableIndexColumns(index));
				}
				foreach (Column column in this.fullColumnsToFetch)
				{
					if (QueryPlanner.IsColumnMaterializedInIndex(index, column, false) && !list2.Contains(column))
					{
						list2.Add(column);
					}
				}
				using (List<Column>.Enumerator enumerator2 = this.truncatedColumnsToFetch.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Column column2 = enumerator2.Current;
						if (QueryPlanner.IsColumnMaterializedInIndex(index, column2, true) && !list2.Contains(column2))
						{
							list2.Add(column2);
						}
					}
					goto IL_3A6;
				}
			}
			foreach (Column item in this.fullColumnsToFetch)
			{
				if (!list2.Contains(item))
				{
					list2.Add(item);
				}
			}
			foreach (Column item2 in this.truncatedColumnsToFetch)
			{
				if (!list2.Contains(item2))
				{
					list2.Add(item2);
				}
			}
			IL_3A6:
			bool flag6 = false;
			if (!flag && searchCriteria == null)
			{
				flag6 = true;
			}
			if (searchCriteria2 is SearchCriteriaTrue)
			{
				searchCriteria2 = null;
			}
			SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan;
			if (index.IndexTable is TableFunction)
			{
				currentPlan = new TableFunctionOperator.TableFunctionOperatorDefinition(this.context.Culture, (TableFunction)index.IndexTable, index.IndexTableFunctionParameters, list2, searchCriteria2, index.RenameDictionary, flag6 ? this.skipTo : 0, flag6 ? this.maxRows : 0, list, flag3, true);
			}
			else
			{
				currentPlan = new TableOperator.TableOperatorDefinition(this.context.Culture, index.IndexTable, index.IndexTable.PrimaryKeyIndex, list2, null, searchCriteria2, QueryPlanner.GetEffectiveRenameDictionary(index, this.renameDictionary), flag6 ? this.skipTo : 0, flag6 ? this.maxRows : 0, list, flag3, true, true);
			}
			this.CurrentPlan = currentPlan;
			this.RemainingCriteria = searchCriteria;
			this.ColumnsToFetchSatisfied = flag5;
			if (!index.ShouldBeCurrent)
			{
				this.IndexMayBeStale = true;
			}
			if (flag6)
			{
				this.SkipToSatisfied = true;
				this.MaxRowsSatisfied = true;
			}
			if (index.IndexTable is TableFunction)
			{
				this.CurrentCardinality = 50;
			}
			else
			{
				this.CurrentCardinality = 90000;
			}
			bool sortIsUnnecessary = false;
			if (list.Count == 1)
			{
				StartStopKey startKey = list[0].StartKey;
				if (startKey.Count == index.IndexTable.PrimaryKeyIndex.ColumnCount - index.RedundantKeyColumnsCount)
				{
					StartStopKey stopKey = list[0].StopKey;
					if (stopKey.Count == index.IndexTable.PrimaryKeyIndex.ColumnCount - index.RedundantKeyColumnsCount)
					{
						StartStopKey startKey2 = list[0].StartKey;
						IList<object> values = startKey2.Values;
						StartStopKey stopKey2 = list[0].StopKey;
						if (ValueHelper.ListsEqual<object>(values, stopKey2.Values))
						{
							this.CurrentCardinality = 1;
							sortIsUnnecessary = true;
							if (!this.SkipToSatisfied)
							{
								emptyResultSet = true;
								return;
							}
							this.MaxRowsSatisfied = true;
							goto IL_7F3;
						}
					}
				}
			}
			int currentCardinality = this.CurrentCardinality;
			int num2 = (index.IndexKeyPrefix == null) ? 0 : index.IndexKeyPrefix.Count;
			int num3 = int.MaxValue;
			for (int j = 0; j < list.Count; j++)
			{
				StartStopKey startKey3 = list[j].StartKey;
				int val = startKey3.Count - num2;
				StartStopKey stopKey3 = list[j].StopKey;
				int val2 = Math.Max(val, stopKey3.Count - num2);
				num3 = Math.Min(num3, val2);
			}
			this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(num3 + num, this.CurrentCardinality);
			if (!flag && this.maxRows > 0 && this.skipTo + this.maxRows < 500)
			{
				if ((searchCriteria == null || searchCriteria is SearchCriteriaTrue) && searchCriteria2 == null)
				{
					this.CurrentCardinality = Math.Min(this.CurrentCardinality, this.skipTo + this.maxRows);
				}
				else
				{
					this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(1, this.CurrentCardinality);
				}
			}
			if (this.FilterFactorHints != null)
			{
				int num4 = 0;
				for (int k = 0; k < list.Count; k++)
				{
					int num5 = currentCardinality;
					int num6 = num2;
					for (;;)
					{
						int num7 = num6;
						StartStopKey startKey4 = list[k].StartKey;
						int count = startKey4.Count;
						StartStopKey stopKey4 = list[k].StopKey;
						if (num7 >= Math.Min(count, stopKey4.Count))
						{
							break;
						}
						StartStopKey startKey5 = list[k].StartKey;
						object x = startKey5.Values[num6];
						StartStopKey stopKey5 = list[k].StopKey;
						if (!ValueHelper.ValuesEqual(x, stopKey5.Values[num6]))
						{
							break;
						}
						Column key = index.LogicalSortOrder.Columns[num6 - num2];
						FilterFactorHint filterFactorHint;
						if (this.FilterFactorHints.TryGetValue(key, out filterFactorHint) && filterFactorHint.IsEquality)
						{
							if (!filterFactorHint.AnyValue)
							{
								object value = filterFactorHint.Value;
								StartStopKey startKey6 = list[k].StartKey;
								if (!ValueHelper.ValuesEqual(value, startKey6.Values[num6]))
								{
									goto IL_781;
								}
							}
							num5 = Math.Max(1, Math.Min(num5, (int)((1.0 - filterFactorHint.FilterFactor) * (double)currentCardinality)));
						}
						IL_781:
						num6++;
					}
					num4 = Math.Max(num4, num5);
				}
				this.CurrentCardinality = Math.Min(this.CurrentCardinality, num4);
			}
			IL_7F3:
			this.CurrentPlanCost = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(index.Columns), this.CurrentCardinality, false);
			this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(searchCriteria2, this.CompareInfo, this.FilterFactorHints, this.CurrentCardinality);
			this.CurrentSortOrder = (flag2 ? index.LogicalSortOrder.Reverse() : index.LogicalSortOrder);
			this.CurrentConstantColumns = index.ConstantColumns;
			bool flag7 = this.TestPlanWithAndWithoutResidualJoinAndSort(sortIsUnnecessary, probeMasterIndex);
			if (flag7 && this.lowestCostPlan is TableOperator.TableOperatorDefinition)
			{
				this.perfectLazyIndexPlan = true;
			}
			this.PopPlanStack(startTopOfStackIndex);
			this.traceIndent--;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00033534 File Offset: 0x00031734
		private void CreatePlanForCategorizedView()
		{
			IPseudoIndex pseudoIndex = (IPseudoIndex)this.lazyIndexes[0];
			CategorizedTableParams categorizedTableParams = pseudoIndex.GetCategorizedTableParams(this.context);
			if (ExTraceGlobals.QueryPlannerDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
			{
				this.TraceDetail("Testing with just category headers lazy index " + pseudoIndex.IndexTable.Name);
			}
			this.traceIndent++;
			this.planAlternativesConsidered++;
			SearchCriteria remainingCriteria = this.RemainingCriteria;
			this.SimplifyCriteria(pseudoIndex, ref remainingCriteria);
			int startTopOfStackIndex = this.PushPlanStack();
			StartStopKey empty = StartStopKey.Empty;
			if (this.bookmark.IsBOT || this.bookmark.IsEOT)
			{
				this.BookmarkSatisfied = true;
			}
			else
			{
				SortOrder logicalSortOrder = this.sortOrder;
				List<object> list = new List<object>(categorizedTableParams.HeaderLogicalSortOrder.Count + categorizedTableParams.LeafLogicalSortOrder.Count);
				for (int i = 0; i < categorizedTableParams.HeaderLogicalSortOrder.Count; i++)
				{
					list.Add(this.bookmark.KeyValues[i]);
				}
				list.AddRange(this.RemoveConstantColumnsFromKeyValues(this.bookmark.KeyValues, logicalSortOrder, categorizedTableParams.HeaderLogicalSortOrder.Count, categorizedTableParams.LeafRenameDictionary));
				empty = new StartStopKey(this.bookmark.PositionedOn ^ this.backwards, list);
				this.BookmarkSatisfied = true;
			}
			List<Column> columnsToFetch = QueryPlanner.MergeColumns(this.fullColumnsToFetch, this.truncatedColumnsToFetch);
			KeyRange keyRange = new KeyRange(empty, StartStopKey.Empty);
			SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan = new CategorizedTableOperator.CategorizedTableOperatorDefinition(this.context.Culture, this.table, categorizedTableParams, this.categorizedQueryParams.CollapseState, columnsToFetch, this.categorizedQueryParams.HeaderRenameDictionary, this.categorizedQueryParams.LeafRenameDictionary, remainingCriteria, this.skipTo, this.maxRows, keyRange, this.backwards, true);
			this.CurrentPlan = currentPlan;
			this.RemainingCriteria = null;
			this.ColumnsToFetchSatisfied = true;
			this.SkipToSatisfied = true;
			this.MaxRowsSatisfied = true;
			this.CurrentCardinality = 1;
			this.CurrentPlanCost = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(pseudoIndex.Columns), this.CurrentCardinality, false);
			this.CurrentSortOrder = pseudoIndex.LogicalSortOrder;
			this.CurrentConstantColumns = pseudoIndex.ConstantColumns;
			this.perfectLazyIndexPlan = true;
			this.lowestCost = this.CurrentPlanCost;
			this.lowestCardinality = this.CurrentCardinality;
			this.lowestCostPlan = this.CurrentPlan;
			this.PopPlanStack(startTopOfStackIndex);
			this.traceIndent--;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000337BC File Offset: 0x000319BC
		private List<object> RemoveConstantColumnsFromKeyValues(IList<object> keyValues, SortOrder logicalSortOrder, int startingOffset, IReadOnlyDictionary<Column, Column> renameDictionary)
		{
			List<object> list = new List<object>(logicalSortOrder.Count - startingOffset);
			for (int i = startingOffset; i < logicalSortOrder.Count; i++)
			{
				Column column = logicalSortOrder.Columns[i];
				Column column2;
				if (!renameDictionary.TryGetValue(column, out column2))
				{
					column2 = column;
				}
				if (!(column2.ActualColumn is ConstantColumn))
				{
					list.Add(this.bookmark.KeyValues[i]);
				}
			}
			return list;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0003382C File Offset: 0x00031A2C
		private void CreatePlanWithJustFullTextIndex()
		{
			this.TraceDetail("Testing with just full text index plan");
			this.traceIndent++;
			this.planAlternativesConsidered++;
			SearchCriteria searchCriteria;
			SearchCriteria searchCriteria2;
			this.ExtractAndSimplifyIndexCriteriaForFullText(this.RemainingCriteria, out searchCriteria, out searchCriteria2);
			if (searchCriteria == null || this.GetFullTextFlavor(searchCriteria) <= FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
			{
				this.TraceDetail("Discarding (index does not help)");
				this.traceIndent--;
				return;
			}
			if (!this.BookmarkSatisfied)
			{
				searchCriteria2 = QueryPlanner.AndCriteria(searchCriteria2, this.bookmarkCriteria, null, this.CompareInfo, true);
			}
			int startTopOfStackIndex = this.PushPlanStack();
			List<Column> baseTableIndexColumns = this.GetBaseTableIndexColumns(this.fullTextIndex);
			QueryPlanner.AndOrLeg andOrLeg = this.CreateFullTextIndexAccessLeg(searchCriteria, baseTableIndexColumns);
			this.RemainingCriteria = searchCriteria2;
			this.CurrentPlan = andOrLeg.PlanOperator;
			this.CurrentPlanCost = andOrLeg.PlanCost;
			this.CurrentCardinality = andOrLeg.PlanCardinality;
			if (this.sortOrder.IsEmpty && this.maxRows > 0 && this.skipTo + this.maxRows < 500)
			{
				if (searchCriteria2 == null || searchCriteria2 is SearchCriteriaTrue)
				{
					this.CurrentCardinality = Math.Min(this.CurrentCardinality, this.skipTo + this.maxRows);
				}
				else
				{
					this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(1, this.CurrentCardinality);
				}
			}
			this.BookmarkSatisfied = true;
			this.ColumnsToFetchSatisfied = (QueryPlanner.ColumnsContained(baseTableIndexColumns, this.fullColumnsToFetch) && QueryPlanner.ColumnsContained(baseTableIndexColumns, this.truncatedColumnsToFetch));
			this.TestPlanWithAndWithoutResidualJoinAndSort(false);
			this.PopPlanStack(startTopOfStackIndex);
			this.traceIndent--;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000339B2 File Offset: 0x00031BB2
		private bool TestPlanWithAndWithoutResidualJoinAndSort(bool sortIsUnnecessary)
		{
			return this.TestPlanWithAndWithoutResidualJoinAndSort(sortIsUnnecessary, null);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000339BC File Offset: 0x00031BBC
		private bool TestPlanWithAndWithoutResidualJoinAndSort(bool sortIsUnnecessary, IPseudoIndex probeMasterIndex)
		{
			if (this.QueryIsComplete(sortIsUnnecessary))
			{
				int currentPlanCost = this.CurrentPlanCost;
				if (currentPlanCost < this.lowestCost)
				{
					this.lowestCost = currentPlanCost;
					this.lowestCardinality = this.CurrentCardinality;
					this.lowestCostPlan = this.CurrentPlan;
					this.TraceDetail("Saving (current lowest cost)", null, null, null, this.CurrentPlanCost, this.CurrentCardinality, this.CurrentPlan, 0);
					return true;
				}
				this.TraceDetail("Discarding (too costly)", null, null, null, this.CurrentPlanCost, this.CurrentCardinality, this.CurrentPlan, 0);
				return false;
			}
			else
			{
				if (this.GetFullTextFlavor(this.RemainingCriteria) < FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
				{
					this.TraceDetail("Not complete as is. Testing with residual join and sort", null, null, null, this.CurrentPlanCost, this.CurrentCardinality, this.CurrentPlan, 0);
					this.traceIndent++;
					this.planAlternativesConsidered++;
					int startTopOfStackIndex = this.PushPlanStack();
					bool result = false;
					if (probeMasterIndex != null || this.canJoinToBaseTable)
					{
						if (this.AddJoinToBaseTableAndRestrictionIfNecessary(probeMasterIndex))
						{
							bool flag;
							if (!sortIsUnnecessary && !SortOrder.IsMatch(this.sortOrder, this.CurrentSortOrder, this.CurrentConstantColumns, out flag))
							{
								this.AddResidualSort();
							}
							if (this.QueryIsComplete(sortIsUnnecessary))
							{
								int currentPlanCost2 = this.CurrentPlanCost;
								if (currentPlanCost2 < this.lowestCost)
								{
									this.lowestCost = currentPlanCost2;
									this.lowestCardinality = this.CurrentCardinality;
									this.lowestCostPlan = this.CurrentPlan;
									this.TraceDetail("Saving (current lowest cost)", null, null, null, this.CurrentPlanCost, this.CurrentCardinality, this.CurrentPlan, 0);
									result = true;
								}
								else
								{
									this.TraceDetail("Discarding (too costly)", null, null, null, this.CurrentPlanCost, this.CurrentCardinality, this.CurrentPlan, 0);
								}
							}
							else
							{
								this.TraceDetail("Discarding (incomplete)", null, null, null, 0, 0, this.CurrentPlan, 0);
							}
						}
					}
					else
					{
						this.TraceDetail("Discarding (join with base table not allowed)", null, null, null, 0, 0, this.CurrentPlan, 0);
					}
					this.PopPlanStack(startTopOfStackIndex);
					this.traceIndent--;
					return result;
				}
				this.TraceDetail("Discarding (criteria cannot be done residually)");
				return false;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00033BB4 File Offset: 0x00031DB4
		private bool QueryIsComplete(bool sortIsUnnecessary)
		{
			bool flag;
			return this.RemainingCriteria == null && !this.IndexMayBeStale && (sortIsUnnecessary || SortOrder.IsMatch(this.sortOrder, this.CurrentSortOrder, this.CurrentConstantColumns, out flag)) && this.ColumnsToFetchSatisfied && this.BookmarkSatisfied && this.SkipToSatisfied && this.MaxRowsSatisfied;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00033C14 File Offset: 0x00031E14
		private bool AddJoinToBaseTableAndRestrictionIfNecessary(IPseudoIndex probeIndex)
		{
			if (this.IndexMayBeStale)
			{
				this.ColumnsToFetchSatisfied = false;
				this.IndexMayBeStale = false;
			}
			if (!this.ColumnsToFetchSatisfied || this.RemainingCriteria != null)
			{
				TableOperator.TableOperatorDefinition tableOperatorDefinition = this.CurrentPlan as TableOperator.TableOperatorDefinition;
				if (probeIndex == null && tableOperatorDefinition != null && this.table == tableOperatorDefinition.Index.IndexTable)
				{
					return false;
				}
				List<Column> columnsToFetch = QueryPlanner.MergeColumns(this.fullColumnsToFetch, this.truncatedColumnsToFetch);
				bool flag2;
				bool flag = !SortOrder.IsMatch(this.sortOrder, this.CurrentSortOrder, this.CurrentConstantColumns, out flag2);
				bool flag3 = false;
				if (!flag)
				{
					flag3 = true;
				}
				SearchCriteria remainingCriteria = this.RemainingCriteria;
				Table indexTable;
				IReadOnlyDictionary<Column, Column> effectiveRenameDictionary;
				IList<Column> columns;
				int num;
				if (probeIndex != null)
				{
					indexTable = probeIndex.IndexTable;
					effectiveRenameDictionary = QueryPlanner.GetEffectiveRenameDictionary(probeIndex, this.renameDictionary);
					columns = probeIndex.SortOrder.Columns;
					num = QueryPlanner.EstimateCost(24576, this.CurrentCardinality, true) / 2;
				}
				else
				{
					indexTable = this.table;
					effectiveRenameDictionary = this.renameDictionary;
					columns = this.GetForeignKeyIndex().Columns;
					num = QueryPlanner.EstimateCost(24576, this.CurrentCardinality, true);
				}
				SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan = new JoinOperator.JoinOperatorDefinition(this.context.Culture, indexTable, columnsToFetch, this.longValueColumnsToPreread, remainingCriteria, effectiveRenameDictionary, flag3 ? (this.SkipToSatisfied ? 0 : this.skipTo) : 0, flag3 ? (this.MaxRowsSatisfied ? 0 : this.maxRows) : 0, columns, this.CurrentPlan, true);
				this.ColumnsToFetchSatisfied = true;
				this.RemainingCriteria = null;
				this.CurrentPlan = currentPlan;
				if (flag3)
				{
					this.SkipToSatisfied = true;
					this.MaxRowsSatisfied = true;
				}
				this.CurrentPlanCost += num;
				this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(remainingCriteria, this.CompareInfo, this.FilterFactorHints, this.CurrentCardinality);
			}
			return true;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00033DD8 File Offset: 0x00031FD8
		private void AddResidualSort()
		{
			Bookmark bookmark = this.BookmarkSatisfied ? Bookmark.BOT : this.bookmark;
			IList<KeyRange> list = this.ComputeResidualKeyRanges(this.sortOrder, bookmark, this.backwards);
			SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan = new SortOperator.SortOperatorDefinition(this.context.Culture, this.CurrentPlan, this.SkipToSatisfied ? 0 : this.skipTo, this.MaxRowsSatisfied ? 0 : this.maxRows, this.sortOrder, list, this.backwards, true);
			this.BookmarkSatisfied = true;
			this.CurrentPlan = currentPlan;
			this.CurrentSortOrder = this.sortOrder;
			this.CurrentConstantColumns = null;
			this.SkipToSatisfied = true;
			this.MaxRowsSatisfied = true;
			this.CurrentPlanCost += 2 * QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(this.CurrentPlan.ColumnsToFetch), this.CurrentCardinality, false);
			int num = int.MaxValue;
			for (int i = 0; i < list.Count; i++)
			{
				StartStopKey startKey = list[i].StartKey;
				int count = startKey.Count;
				StartStopKey stopKey = list[i].StopKey;
				int val = Math.Max(count, stopKey.Count);
				num = Math.Min(num, val);
			}
			this.CurrentCardinality = QueryPlanner.ComputeRowsRemaining(num, this.CurrentCardinality);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00033F1C File Offset: 0x0003211C
		private QueryPlanner.AndOrLeg CreateBaseTableAccessLeg(Index index, IList<Column> indexColumnsToFetch)
		{
			SearchCriteria searchCriteria = null;
			bool flag = false;
			IList<KeyRange> list;
			this.ComputeKeyRanges(index, this.sortOrder, Bookmark.BOT, false, ref searchCriteria, out list, ref flag);
			if (searchCriteria is SearchCriteriaFalse)
			{
				this.TraceDetail("Discarding? (result set is empty...)");
				return null;
			}
			if (this.table.IsPartitioned)
			{
				for (int i = 0; i < list.Count; i++)
				{
					StartStopKey startKey = list[i].StartKey;
					int count = startKey.Count;
					StartStopKey stopKey = list[i].StopKey;
					if (Math.Min(count, stopKey.Count) < this.table.SpecialCols.NumberOfPartioningColumns)
					{
						this.TraceDetail("Plan is not possible because based table is partitioned and there is no partitioning key");
						DiagnosticContext.TraceLocation((LID)41704U);
						return null;
					}
				}
			}
			SimpleQueryOperator.SimpleQueryOperatorDefinition planOperator = new TableOperator.TableOperatorDefinition(this.context.Culture, index.IndexTable, index, indexColumnsToFetch, null, null, QueryPlanner.GetEffectiveRenameDictionary(index, this.renameDictionary), 0, 0, list, false, true, true);
			int num = 90000;
			int planCost = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(index.Columns), num, false);
			return new QueryPlanner.AndOrLeg(planOperator, planCost, num);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0003402C File Offset: 0x0003222C
		private QueryPlanner.AndOrLeg CreateIndexAccessLeg(IPseudoIndex index, SearchCriteria indexCriteria, IList<Column> indexColumnsToFetch)
		{
			bool flag = false;
			IList<KeyRange> list;
			this.ComputeKeyRanges(index, this.sortOrder, Bookmark.BOT, false, ref indexCriteria, out list, ref flag);
			if (indexCriteria is SearchCriteriaFalse)
			{
				this.TraceDetail("Discarding? (result set is empty...)");
				return null;
			}
			SearchCriteria restriction = (indexCriteria is SearchCriteriaTrue) ? null : indexCriteria;
			SimpleQueryOperator.SimpleQueryOperatorDefinition planOperator;
			if (index.IndexTable is TableFunction)
			{
				planOperator = new TableFunctionOperator.TableFunctionOperatorDefinition(this.context.Culture, (TableFunction)index.IndexTable, this.parameters, indexColumnsToFetch, restriction, index.RenameDictionary, 0, 0, list, false, true);
			}
			else
			{
				planOperator = new TableOperator.TableOperatorDefinition(this.context.Culture, index.IndexTable, index.IndexTable.PrimaryKeyIndex, indexColumnsToFetch, null, restriction, index.RenameDictionary, 0, 0, list, false, true, true);
			}
			int num = (index.IndexKeyPrefix == null) ? 0 : index.IndexKeyPrefix.Count;
			int num2 = int.MaxValue;
			for (int i = 0; i < list.Count; i++)
			{
				StartStopKey startKey = list[i].StartKey;
				int val = startKey.Count - num;
				StartStopKey stopKey = list[i].StopKey;
				int val2 = Math.Max(val, stopKey.Count - num);
				num2 = Math.Min(num2, val2);
			}
			int num3 = QueryPlanner.ComputeRowsRemaining(num2, 90000);
			int planCost = QueryPlanner.EstimateCost(QueryPlanner.EstimateSize(index.Columns), num3, false);
			num3 = QueryPlanner.ComputeRowsRemaining(restriction, this.CompareInfo, this.FilterFactorHints, num3);
			return new QueryPlanner.AndOrLeg(planOperator, planCost, num3);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000341A0 File Offset: 0x000323A0
		private QueryPlanner.AndOrLeg CreateFullTextIndexOrLeg(List<Column> indexColumnsToFetch, out SearchCriteria residualCriteria)
		{
			SearchCriteria searchCriteria;
			this.ExtractAndSimplifyIndexCriteriaForFullTextIndexOR(this.RemainingCriteria, out searchCriteria, out residualCriteria);
			if (this.RemainingCriteria != residualCriteria && searchCriteria != null && this.GetFullTextFlavor(searchCriteria) > FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
			{
				return this.CreateFullTextIndexAccessLeg(searchCriteria, indexColumnsToFetch);
			}
			residualCriteria = null;
			return null;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000341E0 File Offset: 0x000323E0
		private QueryPlanner.AndOrLeg CreateFullTextIndexAndLeg(List<Column> indexColumnsToFetch, out SearchCriteria residualCriteria)
		{
			SearchCriteria searchCriteria;
			this.ExtractAndSimplifyIndexCriteriaForFullText(this.RemainingCriteria, out searchCriteria, out residualCriteria);
			if (this.RemainingCriteria != residualCriteria && searchCriteria != null && this.GetFullTextFlavor(searchCriteria) > FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
			{
				return this.CreateFullTextIndexAccessLeg(searchCriteria, indexColumnsToFetch);
			}
			residualCriteria = null;
			return null;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00034220 File Offset: 0x00032420
		private QueryPlanner.AndOrLeg CreateIndexAndLeg(IPseudoIndex index, List<Column> indexColumnsToFetch, out SearchCriteria residualCriteria)
		{
			SearchCriteria searchCriteria;
			this.ExtractAndSimplifyIndexCriteriaForAND(index, this.RemainingCriteria, out searchCriteria, out residualCriteria);
			if (searchCriteria != null)
			{
				return this.CreateIndexAccessLeg(index, searchCriteria, indexColumnsToFetch);
			}
			residualCriteria = null;
			return null;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00034250 File Offset: 0x00032450
		private QueryPlanner.AndOrLeg CreateIndexOrLeg(IPseudoIndex index, List<Column> indexColumnsToFetch, out SearchCriteria residualCriteria)
		{
			SearchCriteria searchCriteria;
			this.ExtractAndSimplifyIndexCriteriaForOR(index, this.RemainingCriteria, out searchCriteria, out residualCriteria);
			if (searchCriteria != null)
			{
				return this.CreateIndexAccessLeg(index, searchCriteria, indexColumnsToFetch);
			}
			residualCriteria = null;
			return null;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00034280 File Offset: 0x00032480
		private void TraceDetail(string text)
		{
			this.TraceDetail(text, null, null, null, 0, 0, null, 0);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0003429B File Offset: 0x0003249B
		private void TraceSummary(string text)
		{
			this.TraceSummary(text, null, 0, 0, null, 0);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000342AC File Offset: 0x000324AC
		private void TraceDetail(string text, IList<QueryPlanner.AndOrLeg> legs, QueryPlanner.AndOrLeg newLeg, SearchCriteria criteria, int cost, int cardinality, DataAccessOperator.DataAccessOperatorDefinition plan, int plansConsidered)
		{
			if (ExTraceGlobals.QueryPlannerDetailTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
			{
				StringBuilder stringBuilder = this.BuildTrace(text, legs, newLeg, criteria, cost, cardinality, plan, plansConsidered);
				string text2 = stringBuilder.ToString();
				this.AddDiagnosticTrace(text2);
				if (ExTraceGlobals.QueryPlannerSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.QueryPlannerDetailTracer.TraceDebug(0L, text2);
				}
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0003430C File Offset: 0x0003250C
		private void TraceSummary(string text, SearchCriteria criteria, int cost, int cardinality, DataAccessOperator.DataAccessOperatorDefinition plan, int plansConsidered)
		{
			if (ExTraceGlobals.QueryPlannerSummaryTracer.IsTraceEnabled(TraceType.DebugTrace) || this.IsDiagnosticTraceEnabled)
			{
				StringBuilder stringBuilder = this.BuildTrace(text, null, null, criteria, cost, cardinality, plan, plansConsidered);
				string text2 = stringBuilder.ToString();
				this.AddDiagnosticTrace(text2);
				if (ExTraceGlobals.QueryPlannerSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.QueryPlannerSummaryTracer.TraceDebug(0L, text2);
				}
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00034368 File Offset: 0x00032568
		private StringBuilder BuildTrace(string text, IList<QueryPlanner.AndOrLeg> legs, QueryPlanner.AndOrLeg newLeg, SearchCriteria criteria, int cost, int cardinality, DataAccessOperator.DataAccessOperatorDefinition plan, int plansConsidered)
		{
			StringFormatOptions formatOptions = this.IsDiagnosticTraceEnabled ? StringFormatOptions.SkipParametersData : StringFormatOptions.IncludeDetails;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder(100);
			for (int i = 0; i < this.traceIndent; i++)
			{
				stringBuilder.Append(". ");
			}
			stringBuilder.Append(text);
			if (legs != null || newLeg != null)
			{
				stringBuilder.Append(" legs:[");
				if (legs != null)
				{
					for (int j = 0; j < legs.Count; j++)
					{
						if (j > 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append("[");
						stringBuilder.Append(legs[j].PlanOperator.ToString(formatOptions));
						stringBuilder.Append("] cost:[");
						stringBuilder.Append(legs[j].PlanCost);
						stringBuilder.Append("] cardinality:[");
						stringBuilder.Append(legs[j].PlanCardinality);
						stringBuilder.Append("]");
					}
					flag = true;
				}
				if (newLeg != null)
				{
					if (legs != null && legs.Count != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("[");
					stringBuilder.Append(newLeg.PlanOperator.ToString(formatOptions));
					stringBuilder.Append("] cost:[");
					stringBuilder.Append(newLeg.PlanCost);
					stringBuilder.Append("] cardinality:[");
					stringBuilder.Append(newLeg.PlanCardinality);
					stringBuilder.Append("]");
					flag = true;
				}
			}
			if (criteria != null)
			{
				stringBuilder.Append(flag ? "] criteria:[" : " criteria:[");
				stringBuilder.Append(criteria.ToString(formatOptions));
				flag = true;
			}
			if (cost != 0)
			{
				stringBuilder.Append(flag ? "] cost:[" : " cost:[");
				stringBuilder.Append(cost);
				flag = true;
			}
			if (cardinality != 0)
			{
				stringBuilder.Append(flag ? "] cardinality:[" : " cardinality:[");
				stringBuilder.Append(cardinality);
				flag = true;
			}
			if (plan != null)
			{
				stringBuilder.Append(flag ? "] plan:[" : " plan:[");
				stringBuilder.Append(plan.ToString(formatOptions));
				flag = true;
			}
			if (flag)
			{
				stringBuilder.Append("] plansConsidered:[");
				stringBuilder.Append(plansConsidered);
				stringBuilder.Append("]");
			}
			return stringBuilder;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000345AC File Offset: 0x000327AC
		private bool KeyRangesValidForPartitionedTable(IList<KeyRange> keyRanges)
		{
			int i = 0;
			while (i < keyRanges.Count)
			{
				StartStopKey startKey = keyRanges[i].StartKey;
				if (!startKey.IsEmpty)
				{
					StartStopKey stopKey = keyRanges[i].StopKey;
					if (!stopKey.IsEmpty)
					{
						StartStopKey startKey2 = keyRanges[i].StartKey;
						if (startKey2.Count >= this.table.SpecialCols.NumberOfPartioningColumns)
						{
							StartStopKey stopKey2 = keyRanges[i].StopKey;
							if (stopKey2.Count >= this.table.SpecialCols.NumberOfPartioningColumns)
							{
								int j = 0;
								while (j < this.table.SpecialCols.NumberOfPartioningColumns)
								{
									StartStopKey startKey3 = keyRanges[i].StartKey;
									object x = startKey3.Values[j];
									StartStopKey stopKey3 = keyRanges[i].StopKey;
									if (ValueHelper.ValuesCompare(x, stopKey3.Values[j]) == 0)
									{
										StartStopKey startKey4 = keyRanges[i].StartKey;
										object x2 = startKey4.Values[j];
										StartStopKey startKey5 = keyRanges[0].StartKey;
										if (ValueHelper.ValuesCompare(x2, startKey5.Values[j]) == 0)
										{
											StartStopKey startKey6 = keyRanges[i].StartKey;
											if (startKey6.Values[j] != null)
											{
												StartStopKey stopKey4 = keyRanges[i].StopKey;
												if (stopKey4.Values[j] != null)
												{
													j++;
													continue;
												}
											}
										}
									}
									return false;
								}
								i++;
								continue;
							}
						}
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00034F18 File Offset: 0x00033118
		private void AddCriteriaToRange(SortOrder keySortOrder, IIndex index, ref SearchCriteria criteria, ref List<object> startKeyValues, ref List<object> stopKeyValues, ref bool startKeyInclusive, ref bool stopKeyInclusive, bool backwards)
		{
			List<object> tempStartKeyValues = startKeyValues;
			List<object> tempStopKeyValues = stopKeyValues;
			bool tempStartKeyInclusive = startKeyInclusive;
			bool tempStopKeyInclusive = stopKeyInclusive;
			bool stillInKeyPrefix = true;
			int i;
			for (i = 0; i < keySortOrder.Count; i++)
			{
				int startKeyLength = (tempStartKeyValues == null) ? 0 : tempStartKeyValues.Count;
				int stopKeyLength = (tempStopKeyValues == null) ? 0 : tempStopKeyValues.Count;
				if (i > Math.Max(startKeyLength, stopKeyLength))
				{
					break;
				}
				if (stillInKeyPrefix && i > 0 && (i > Math.Min(startKeyLength, stopKeyLength) || !ValueHelper.ValuesEqual(tempStartKeyValues[i - 1], tempStopKeyValues[i - 1], this.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)))
				{
					stillInKeyPrefix = false;
				}
				Column physicalKeyColumn = keySortOrder[i].Column;
				bool physicalKeyColumnAscending = keySortOrder[i].Ascending;
				criteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
				{
					if (criterion is SearchCriteriaAnd)
					{
						return criterion;
					}
					if (criterion is SearchCriteriaCompare && stillInKeyPrefix)
					{
						ConstantColumn constantColumn = ((SearchCriteriaCompare)criterion).Rhs as ConstantColumn;
						Column lhs = ((SearchCriteriaCompare)criterion).Lhs;
						if (constantColumn != null && lhs.Type == constantColumn.Type)
						{
							object obj = constantColumn.Value;
							bool flag = false;
							Column column = lhs;
							if (index != null && lhs.Table != index.IndexTable && !index.GetIndexColumn(lhs, true, out column))
							{
								column = lhs;
							}
							if (column == physicalKeyColumn)
							{
								if (column != lhs)
								{
									if ((lhs.Type == typeof(string) || lhs.Type == typeof(byte[])) && column.MaxLength < lhs.MaxLength)
									{
										obj = ValueHelper.TruncateValueIfNecessary(obj, lhs.Type, column.MaxLength, out flag);
									}
									if (flag)
									{
										stillInKeyPrefix = false;
									}
								}
								if (((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
								{
									bool flag2 = false;
									if (tempStartKeyInclusive && i == startKeyLength && (startKeyLength >= stopKeyLength || ValueHelper.ValuesEqual(obj, tempStopKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)))
									{
										if (tempStartKeyValues == null)
										{
											tempStartKeyValues = new List<object>(4);
										}
										tempStartKeyValues.Add(obj);
										startKeyLength++;
										if (!flag)
										{
											flag2 = true;
										}
									}
									if (tempStopKeyInclusive && i == stopKeyLength)
									{
										if (tempStopKeyValues == null)
										{
											tempStopKeyValues = new List<object>(4);
										}
										tempStopKeyValues.Add(obj);
										stopKeyLength++;
										if (!flag && tempStopKeyValues.Count > i && ValueHelper.ValuesEqual(obj, tempStartKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
										{
											flag2 = true;
										}
									}
									if (i < Math.Min(startKeyLength, stopKeyLength) && ValueHelper.ValuesEqual(tempStartKeyValues[i], tempStopKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
									{
										if (!ValueHelper.ValuesEqual(tempStartKeyValues[i], obj, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
										{
											return Factory.CreateSearchCriteriaFalse();
										}
										if (!flag)
										{
											flag2 = true;
										}
									}
									if (flag2)
									{
										return Factory.CreateSearchCriteriaTrue();
									}
								}
								else if (((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual || ((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.LessThan || ((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThanEqual || ((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThan)
								{
									bool flag3 = false;
									bool flag4 = false;
									if (flag || ((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual || ((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.GreaterThanEqual)
									{
										flag4 = true;
									}
									bool flag5 = false;
									if (((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.LessThanEqual || ((SearchCriteriaCompare)criterion).RelOp == SearchCriteriaCompare.SearchRelOp.LessThan)
									{
										flag5 = true;
									}
									bool flag6 = backwards ^ physicalKeyColumnAscending;
									bool flag7 = flag6 == flag5;
									if (flag7)
									{
										if (i == stopKeyLength)
										{
											if (tempStopKeyValues == null)
											{
												tempStopKeyValues = new List<object>(4);
											}
											tempStopKeyValues.Add(obj);
											stopKeyLength++;
											if (!flag4)
											{
												tempStopKeyInclusive = false;
											}
										}
										else if (i < stopKeyLength && ((flag5 && ValueHelper.ValuesCompare(obj, tempStopKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) < 0) || (!flag5 && ValueHelper.ValuesCompare(obj, tempStopKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) > 0)))
										{
											tempStopKeyValues.RemoveRange(i, stopKeyLength - i);
											tempStopKeyValues.Add(obj);
											stopKeyLength = i + 1;
											if (!flag4)
											{
												tempStopKeyInclusive = false;
											}
										}
									}
									else if (i == startKeyLength)
									{
										if (tempStartKeyValues == null)
										{
											tempStartKeyValues = new List<object>(4);
										}
										tempStartKeyValues.Add(obj);
										startKeyLength++;
										if (!flag4)
										{
											tempStartKeyInclusive = false;
										}
									}
									else if (i < startKeyLength && ((flag5 && ValueHelper.ValuesCompare(obj, tempStartKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) < 0) || (!flag5 && ValueHelper.ValuesCompare(obj, tempStartKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) > 0)))
									{
										tempStartKeyValues.RemoveRange(i, startKeyLength - i);
										tempStartKeyValues.Add(obj);
										startKeyLength = i + 1;
										if (!flag4)
										{
											tempStartKeyInclusive = false;
										}
									}
									if (i < Math.Min(startKeyLength, stopKeyLength) && ((flag6 && ValueHelper.ValuesCompare(tempStartKeyValues[i], tempStopKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) > 0) || (!flag6 && ValueHelper.ValuesCompare(tempStartKeyValues[i], tempStopKeyValues[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) < 0)))
									{
										return Factory.CreateSearchCriteriaFalse();
									}
									bool flag8 = false;
									bool flag9 = false;
									object x = null;
									if (flag7 && i < stopKeyLength)
									{
										x = tempStopKeyValues[i];
										flag9 = tempStopKeyInclusive;
										flag8 = true;
									}
									else if (!flag7 && i < startKeyLength)
									{
										x = tempStartKeyValues[i];
										flag9 = tempStartKeyInclusive;
										flag8 = true;
									}
									if (flag8)
									{
										int num = ValueHelper.ValuesCompare(x, obj, compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
										if (((flag5 && num <= 0) || (!flag5 && num >= 0)) && (num != 0 || flag4 == flag9) && !flag)
										{
											flag3 = true;
										}
									}
									if (flag3)
									{
										return Factory.CreateSearchCriteriaTrue();
									}
								}
							}
						}
					}
					return null;
				}, this.CompareInfo, true);
			}
			startKeyValues = tempStartKeyValues;
			stopKeyValues = tempStopKeyValues;
			startKeyInclusive = tempStartKeyInclusive;
			stopKeyInclusive = tempStopKeyInclusive;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000350DC File Offset: 0x000332DC
		private IList<KeyRange> ComputeResidualKeyRanges(SortOrder desiredSortOrder, Bookmark bookmark, bool backwards)
		{
			StartStopKey empty = StartStopKey.Empty;
			if (!bookmark.IsBOT && !bookmark.IsEOT)
			{
				List<object> values = new List<object>(bookmark.KeyValues);
				bool inclusive = bookmark.PositionedOn ^ backwards;
				empty = new StartStopKey(inclusive, values);
			}
			return new List<KeyRange>(1)
			{
				new KeyRange(empty, StartStopKey.Empty)
			};
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0003513C File Offset: 0x0003333C
		private void ComputeKeyRanges(IIndex index, SortOrder bookmarkSortOrder, Bookmark bookmark, bool backwards, ref SearchCriteria criteria, out IList<KeyRange> keyRanges, ref bool bookmarkSatisfied)
		{
			SortOrder keySortOrder = index.SortOrder;
			StartStopKey empty = StartStopKey.Empty;
			StartStopKey empty2 = StartStopKey.Empty;
			List<object> list = null;
			List<object> list2 = null;
			bool inclusive = true;
			bool inclusive2 = true;
			int num = (index.IndexKeyPrefix == null) ? 0 : index.IndexKeyPrefix.Count;
			if (num != 0)
			{
				list = new List<object>(num + 4);
				list2 = new List<object>(num + 4);
				list.AddRange(index.IndexKeyPrefix);
				list2.AddRange(index.IndexKeyPrefix);
			}
			bool flag;
			if (bookmark.IsBOT || bookmark.IsEOT)
			{
				bookmarkSatisfied = true;
			}
			else if (!bookmarkSatisfied && SortOrder.IsMatch(bookmarkSortOrder, index.LogicalSortOrder, index.ConstantColumns, out flag))
			{
				int num2 = num;
				for (int i = 0; i < bookmarkSortOrder.Count; i++)
				{
					Column column = bookmarkSortOrder[i].Column;
					object item = bookmark.KeyValues[i];
					Column column2;
					if (!index.GetIndexColumn(column, true, out column2))
					{
						column2 = column;
					}
					if (!(column2 is ConstantColumn))
					{
						if (list == null)
						{
							list = new List<object>(bookmarkSortOrder.Count);
						}
						list.Add(item);
						num2++;
					}
				}
				inclusive = (bookmark.PositionedOn ^ this.backwards);
				bookmarkSatisfied = true;
			}
			if (this.outerCriteria != null)
			{
				SearchCriteria searchCriteria = this.outerCriteria;
				this.AddCriteriaToRange(keySortOrder, index, ref searchCriteria, ref list, ref list2, ref inclusive, ref inclusive2, backwards);
			}
			if (criteria != null)
			{
				this.SimplifyCriteria(index, ref criteria);
				this.AddCriteriaToRange(keySortOrder, index, ref criteria, ref list, ref list2, ref inclusive, ref inclusive2, backwards);
			}
			if (criteria != null || this.outerCriteria != null)
			{
				int val = (list == null) ? 0 : list.Count;
				int val2 = (list2 == null) ? 0 : list2.Count;
				object[] array = null;
				int num3 = 0;
				while (num3 < Math.Min(val, val2) && ValueHelper.ValuesEqual(list[num3], list2[num3], this.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
				{
					num3++;
				}
				if (num3 != keySortOrder.Count)
				{
					if (num3 > 0)
					{
						array = new object[num3];
						list.CopyTo(0, array, 0, num3);
					}
					List<KeyRange> list3 = null;
					if (criteria != null)
					{
						list3 = QueryPlanner.BuildKeyRangesFromOrCriteria(array, index, ref criteria, backwards ^ !keySortOrder[num3].Ascending, this.context.Culture);
					}
					if (list3 == null && this.outerCriteria != null)
					{
						SearchCriteria searchCriteria2 = this.outerCriteria;
						list3 = QueryPlanner.BuildKeyRangesFromOrCriteria(array, index, ref searchCriteria2, backwards ^ !keySortOrder[num3].Ascending, this.context.Culture);
					}
					if (list3 != null)
					{
						if (list == null && list2 == null)
						{
							keyRanges = list3;
							return;
						}
						keyRanges = KeyRange.RemoveInaccessibleRanges(list3, new StartStopKey(inclusive, list), new StartStopKey(inclusive2, list2), keySortOrder, this.CompareInfo, backwards);
						return;
					}
				}
			}
			if (list != null)
			{
				empty = new StartStopKey(inclusive, list);
			}
			if (list2 != null)
			{
				empty2 = new StartStopKey(inclusive2, list2);
			}
			keyRanges = new List<KeyRange>(1)
			{
				new KeyRange(empty, empty2)
			};
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00035438 File Offset: 0x00033638
		private int PushPlanStack()
		{
			int num = this.topOfStackIndex;
			if (num > 30)
			{
				throw new StoreException((LID)60448U, ErrorCodeValue.TooComplex, "Failed to build a query plan");
			}
			QueryPlanner.PlanStackFrame item = new QueryPlanner.PlanStackFrame(this.planStack[this.topOfStackIndex]);
			this.planStack.Add(item);
			this.topOfStackIndex++;
			return num;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0003549D File Offset: 0x0003369D
		private void PopPlanStack(int startTopOfStackIndex)
		{
			this.planStack.RemoveAt(this.topOfStackIndex);
			this.topOfStackIndex--;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000354C0 File Offset: 0x000336C0
		private SearchCriteria RemoveCriteria(SearchCriteriaOr criteria, SearchCriteria criteriaToRemove)
		{
			if (criteria == null || object.ReferenceEquals(criteria, criteriaToRemove))
			{
				return null;
			}
			if (criteria.NestedCriteria.Length == 1)
			{
				return null;
			}
			SearchCriteria[] array = new SearchCriteria[criteria.NestedCriteria.Length - 1];
			int num = 0;
			foreach (SearchCriteria searchCriteria in criteria.NestedCriteria)
			{
				if (searchCriteria != criteriaToRemove)
				{
					array[num++] = searchCriteria;
				}
			}
			return Factory.CreateSearchCriteriaOr(array);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0003552C File Offset: 0x0003372C
		private SearchCriteria RemoveCriteria(SearchCriteriaAnd criteria, SearchCriteria criteriaToRemove)
		{
			if (criteria == null || object.ReferenceEquals(criteria, criteriaToRemove))
			{
				return null;
			}
			if (criteria.NestedCriteria.Length == 1)
			{
				return null;
			}
			SearchCriteria[] array = new SearchCriteria[criteria.NestedCriteria.Length - 1];
			int num = 0;
			foreach (SearchCriteria searchCriteria in criteria.NestedCriteria)
			{
				if (searchCriteria != criteriaToRemove)
				{
					array[num++] = searchCriteria;
				}
			}
			return Factory.CreateSearchCriteriaAnd(array);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00035768 File Offset: 0x00033968
		private void ExtractAndSimplifyIndexCriteriaForFullTextIndexOR(SearchCriteria inputCriteria, out SearchCriteria indexCriteria, out SearchCriteria residualCriteria)
		{
			List<SearchCriteria> orLegs = null;
			indexCriteria = null;
			residualCriteria = inputCriteria;
			if (residualCriteria == null)
			{
				return;
			}
			residualCriteria = inputCriteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaTrue || criterion is SearchCriteriaFalse || criterion is SearchCriteriaBitMask)
				{
					return null;
				}
				if (criterion is SearchCriteriaOr)
				{
					if (this.GetFullTextFlavor(criterion) <= FullTextIndexSchema.CriteriaFullTextFlavor.CannotBeServiced)
					{
						return null;
					}
					return criterion;
				}
				else if (criterion is SearchCriteriaAnd)
				{
					SearchCriteria item;
					SearchCriteria searchCriteria;
					this.ExtractAndSimplifyIndexCriteriaForFullText(criterion, out item, out searchCriteria);
					if (searchCriteria != null && this.GetFullTextFlavor(searchCriteria) != FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
					{
						return null;
					}
					if (orLegs == null)
					{
						orLegs = new List<SearchCriteria>(10);
					}
					orLegs.Add(item);
					if (searchCriteria == null)
					{
						return Factory.CreateSearchCriteriaFalse();
					}
					return searchCriteria;
				}
				else if (criterion is SearchCriteriaNot)
				{
					SearchCriteriaNot searchCriteriaNot = criterion as SearchCriteriaNot;
					SearchCriteria searchCriteria2;
					SearchCriteria searchCriteria3;
					this.ExtractAndSimplifyIndexCriteriaForFullText(searchCriteriaNot.Criteria, out searchCriteria2, out searchCriteria3);
					if (searchCriteria3 != null && this.GetFullTextFlavor(searchCriteria3) != FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
					{
						return null;
					}
					if (orLegs == null)
					{
						orLegs = new List<SearchCriteria>(10);
					}
					orLegs.Add(searchCriteriaNot);
					if (searchCriteria3 == null)
					{
						return Factory.CreateSearchCriteriaFalse();
					}
					return Factory.CreateSearchCriteriaNot(searchCriteria3);
				}
				else if (criterion is SearchCriteriaNear)
				{
					SearchCriteriaNear searchCriteriaNear = criterion as SearchCriteriaNear;
					SearchCriteria searchCriteria4;
					SearchCriteria searchCriteria5;
					this.ExtractAndSimplifyIndexCriteriaForFullText(searchCriteriaNear.Criteria, out searchCriteria4, out searchCriteria5);
					if (searchCriteria5 == null && searchCriteria4 is SearchCriteriaAnd)
					{
						if (orLegs == null)
						{
							orLegs = new List<SearchCriteria>(10);
						}
						orLegs.Add(searchCriteriaNear);
						return Factory.CreateSearchCriteriaFalse();
					}
					DiagnosticContext.TraceLocation((LID)59048U);
					return null;
				}
				else
				{
					if (criterion is SearchCriteriaCompare || criterion is SearchCriteriaText)
					{
						FullTextIndexSchema.CriteriaFullTextFlavor fullTextFlavor = this.GetFullTextFlavor(criterion);
						if (fullTextFlavor >= FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
						{
							if (orLegs == null)
							{
								orLegs = new List<SearchCriteria>(10);
							}
							orLegs.Add(criterion);
							if (fullTextFlavor > FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
							{
								return Factory.CreateSearchCriteriaFalse();
							}
						}
						return null;
					}
					return null;
				}
			}, this.CompareInfo, false);
			if (orLegs != null)
			{
				if (orLegs.Count == 1)
				{
					indexCriteria = orLegs[0];
				}
				else
				{
					indexCriteria = Factory.CreateSearchCriteriaOr(orLegs.ToArray());
				}
			}
			if (residualCriteria is SearchCriteriaFalse)
			{
				residualCriteria = null;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00035AD8 File Offset: 0x00033CD8
		private void ExtractAndSimplifyIndexCriteriaForFullText(SearchCriteria inputCriteria, out SearchCriteria indexCriteria, out SearchCriteria residualCriteria)
		{
			List<SearchCriteria> andLegs = null;
			indexCriteria = null;
			residualCriteria = inputCriteria;
			if (residualCriteria == null || residualCriteria is SearchCriteriaTrue)
			{
				return;
			}
			residualCriteria = inputCriteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaTrue || criterion is SearchCriteriaFalse || criterion is SearchCriteriaBitMask)
				{
					return null;
				}
				if (criterion is SearchCriteriaAnd)
				{
					return criterion;
				}
				if (criterion is SearchCriteriaOr)
				{
					bool flag;
					FullTextIndexSchema.CriteriaFullTextFlavor fullTextFlavor = this.GetFullTextFlavor(criterion, out flag);
					if (fullTextFlavor == FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryCannotBeServiced && this.hints != null && this.hints.GetSupplementaryCriteria != null && !this.IsDiagnosticTraceEnabled)
					{
						SearchCriteria searchCriteria = this.hints.GetSupplementaryCriteria(this.context, criterion, this.MaxNumberOfFullTextSupplementaryRestrictions);
						if (searchCriteria != null)
						{
							fullTextFlavor = this.GetFullTextFlavor(searchCriteria, out flag);
							if (andLegs == null)
							{
								andLegs = new List<SearchCriteria>(10);
							}
							andLegs.Add(searchCriteria);
							return null;
						}
					}
					if (fullTextFlavor <= FullTextIndexSchema.CriteriaFullTextFlavor.CannotBeServiced)
					{
						return null;
					}
					if (flag)
					{
						SearchCriteria item;
						SearchCriteria searchCriteria2;
						this.ExtractAndSimplifyIndexCriteriaForFullTextIndexOR(criterion, out item, out searchCriteria2);
						if (searchCriteria2 != null && this.GetFullTextFlavor(searchCriteria2) != FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
						{
							return null;
						}
						if (andLegs == null)
						{
							andLegs = new List<SearchCriteria>(10);
						}
						andLegs.Add(item);
						if (searchCriteria2 == null)
						{
							return Factory.CreateSearchCriteriaTrue();
						}
						return searchCriteria2;
					}
					else
					{
						if (andLegs == null)
						{
							andLegs = new List<SearchCriteria>(10);
						}
						andLegs.Add(criterion);
						if (fullTextFlavor <= FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
						{
							return null;
						}
						return Factory.CreateSearchCriteriaTrue();
					}
				}
				else if (criterion is SearchCriteriaNot)
				{
					SearchCriteriaNot searchCriteriaNot = criterion as SearchCriteriaNot;
					bool flag2;
					if (this.GetFullTextFlavor(searchCriteriaNot, out flag2) >= FullTextIndexSchema.CriteriaFullTextFlavor.CanBeServiced && !flag2)
					{
						if (andLegs == null)
						{
							andLegs = new List<SearchCriteria>(10);
						}
						andLegs.Add(searchCriteriaNot);
						return Factory.CreateSearchCriteriaTrue();
					}
					SearchCriteria searchCriteria3;
					SearchCriteria searchCriteria4;
					this.ExtractAndSimplifyIndexCriteriaForFullText(searchCriteriaNot.Criteria, out searchCriteria3, out searchCriteria4);
					if (searchCriteria4 != null && this.GetFullTextFlavor(searchCriteria4) != FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
					{
						return null;
					}
					if (andLegs == null)
					{
						andLegs = new List<SearchCriteria>(10);
					}
					andLegs.Add(searchCriteriaNot);
					if (searchCriteria4 == null)
					{
						return Factory.CreateSearchCriteriaTrue();
					}
					return Factory.CreateSearchCriteriaNot(searchCriteria4);
				}
				else if (criterion is SearchCriteriaNear)
				{
					SearchCriteriaNear searchCriteriaNear = criterion as SearchCriteriaNear;
					SearchCriteria searchCriteria5;
					SearchCriteria searchCriteria6;
					this.ExtractAndSimplifyIndexCriteriaForFullText(searchCriteriaNear.Criteria, out searchCriteria5, out searchCriteria6);
					if (searchCriteria6 == null && searchCriteria5 is SearchCriteriaAnd)
					{
						if (andLegs == null)
						{
							andLegs = new List<SearchCriteria>(10);
						}
						andLegs.Add(searchCriteriaNear);
						return Factory.CreateSearchCriteriaTrue();
					}
					DiagnosticContext.TraceLocation((LID)59989U);
					return null;
				}
				else
				{
					if (criterion is SearchCriteriaCompare || criterion is SearchCriteriaText)
					{
						FullTextIndexSchema.CriteriaFullTextFlavor fullTextFlavor2 = this.GetFullTextFlavor(criterion);
						if (fullTextFlavor2 >= FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
						{
							if (andLegs == null)
							{
								andLegs = new List<SearchCriteria>(10);
							}
							andLegs.Add(criterion);
							if (fullTextFlavor2 > FullTextIndexSchema.CriteriaFullTextFlavor.SupplementaryOnly)
							{
								return Factory.CreateSearchCriteriaTrue();
							}
						}
						return null;
					}
					return null;
				}
			}, this.CompareInfo, false);
			if (andLegs != null)
			{
				if (andLegs.Count == 1)
				{
					indexCriteria = andLegs[0];
				}
				else
				{
					indexCriteria = Factory.CreateSearchCriteriaAnd(andLegs.ToArray());
				}
			}
			if (residualCriteria is SearchCriteriaTrue)
			{
				residualCriteria = null;
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00035CE0 File Offset: 0x00033EE0
		private void ExtractAndSimplifyIndexCriteriaForAND(IIndex index, SearchCriteria inputCriteria, out SearchCriteria indexCriteria, out SearchCriteria residualCriteria)
		{
			List<SearchCriteria> andLegs = null;
			indexCriteria = null;
			residualCriteria = inputCriteria;
			if (residualCriteria == null || residualCriteria is SearchCriteriaTrue)
			{
				return;
			}
			residualCriteria = inputCriteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaTrue || criterion is SearchCriteriaFalse || criterion is SearchCriteriaOr || criterion is SearchCriteriaBitMask || criterion is SearchCriteriaNot)
				{
					return null;
				}
				if (criterion is SearchCriteriaAnd || criterion is SearchCriteriaNear)
				{
					return criterion;
				}
				if (criterion is SearchCriteriaText)
				{
					if (this.GetFullTextFlavor(criterion) >= FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
					{
						return null;
					}
					SearchCriteriaText searchCriteriaText = criterion as SearchCriteriaText;
					Column lhs = searchCriteriaText.Lhs;
					Column rhs = searchCriteriaText.Rhs;
					if (!QueryPlanner.IsColumnSatisfiableByIndex(index, rhs, false))
					{
						return null;
					}
					if (!QueryPlanner.IsColumnSatisfiableByIndex(index, lhs, false))
					{
						return null;
					}
					if (andLegs == null)
					{
						andLegs = new List<SearchCriteria>(10);
					}
					andLegs.Add(searchCriteriaText);
					return Factory.CreateSearchCriteriaTrue();
				}
				else
				{
					if (criterion is SearchCriteriaCompare)
					{
						if (this.GetFullTextFlavor(criterion) < FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
						{
							SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
							Column rhs2 = searchCriteriaCompare.Rhs;
							Column lhs2 = searchCriteriaCompare.Lhs;
							bool flag = QueryPlanner.IsColumnSatisfiableByIndex(index, lhs2, QueryPlanner.CanAcceptTruncatedLhsColumnForCompare(rhs2));
							bool flag2 = QueryPlanner.IsColumnSatisfiableByIndex(index, rhs2, QueryPlanner.CanAcceptTruncatedLhsColumnForCompare(lhs2));
							if ((flag && rhs2 is ConstantColumn) || (flag2 && lhs2 is ConstantColumn) || (flag && flag2))
							{
								if (andLegs == null)
								{
									andLegs = new List<SearchCriteria>(10);
								}
								andLegs.Add(searchCriteriaCompare);
								return Factory.CreateSearchCriteriaTrue();
							}
						}
						return null;
					}
					return null;
				}
			}, this.CompareInfo, false);
			if (andLegs != null)
			{
				if (andLegs.Count == 1)
				{
					indexCriteria = andLegs[0];
					return;
				}
				indexCriteria = Factory.CreateSearchCriteriaAnd(andLegs.ToArray());
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00035EE8 File Offset: 0x000340E8
		private void ExtractAndSimplifyIndexCriteriaForOR(IIndex index, SearchCriteria inputCriteria, out SearchCriteria indexCriteria, out SearchCriteria residualCriteria)
		{
			List<SearchCriteria> orLegs = null;
			indexCriteria = null;
			residualCriteria = inputCriteria;
			if (residualCriteria == null)
			{
				return;
			}
			residualCriteria = inputCriteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaTrue || criterion is SearchCriteriaFalse || criterion is SearchCriteriaAnd || criterion is SearchCriteriaNear || criterion is SearchCriteriaBitMask || criterion is SearchCriteriaNot)
				{
					return null;
				}
				if (criterion is SearchCriteriaOr)
				{
					return criterion;
				}
				if (criterion is SearchCriteriaText)
				{
					if (this.GetFullTextFlavor(criterion) >= FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
					{
						return null;
					}
					SearchCriteriaText searchCriteriaText = criterion as SearchCriteriaText;
					Column lhs = searchCriteriaText.Lhs;
					Column rhs = searchCriteriaText.Rhs;
					if (!QueryPlanner.IsColumnSatisfiableByIndex(index, rhs, false))
					{
						return null;
					}
					if (!QueryPlanner.IsColumnSatisfiableByIndex(index, lhs, false))
					{
						return null;
					}
					if (orLegs == null)
					{
						orLegs = new List<SearchCriteria>(10);
					}
					orLegs.Add(searchCriteriaText);
					return Factory.CreateSearchCriteriaFalse();
				}
				else
				{
					if (criterion is SearchCriteriaCompare)
					{
						if (this.GetFullTextFlavor(criterion) < FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
						{
							SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
							Column rhs2 = searchCriteriaCompare.Rhs;
							Column lhs2 = searchCriteriaCompare.Lhs;
							bool flag = QueryPlanner.IsColumnSatisfiableByIndex(index, lhs2, QueryPlanner.CanAcceptTruncatedLhsColumnForCompare(rhs2));
							bool flag2 = QueryPlanner.IsColumnSatisfiableByIndex(index, rhs2, QueryPlanner.CanAcceptTruncatedLhsColumnForCompare(lhs2));
							if ((flag && rhs2 is ConstantColumn) || (flag2 && lhs2 is ConstantColumn) || (flag && flag2))
							{
								if (orLegs == null)
								{
									orLegs = new List<SearchCriteria>(10);
								}
								orLegs.Add(searchCriteriaCompare);
								return Factory.CreateSearchCriteriaFalse();
							}
						}
						return null;
					}
					return null;
				}
			}, this.CompareInfo, false);
			if (orLegs != null)
			{
				if (orLegs.Count == 1)
				{
					indexCriteria = orLegs[0];
				}
				else
				{
					indexCriteria = Factory.CreateSearchCriteriaOr(orLegs.ToArray());
				}
			}
			if (residualCriteria is SearchCriteriaFalse)
			{
				residualCriteria = null;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00035F80 File Offset: 0x00034180
		private bool CriteriaComplexEnoughForRecursion(SearchCriteria inputCriteria)
		{
			return inputCriteria != null && !(inputCriteria is SearchCriteriaTrue) && !(inputCriteria is SearchCriteriaFalse) && !(inputCriteria is SearchCriteriaText) && !(inputCriteria is SearchCriteriaCompare) && !(inputCriteria is SearchCriteriaBitMask) && ((!(inputCriteria is SearchCriteriaAnd) && !(inputCriteria is SearchCriteriaOr) && !(inputCriteria is SearchCriteriaNear) && !(inputCriteria is SearchCriteriaNot)) || true);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00035FE0 File Offset: 0x000341E0
		private void ExtractIndexCriteria(SearchCriteria criteria, IIndex index, out SearchCriteria outerCriteria, out SearchCriteria residualCriteria)
		{
			if (this.IndexSatisfiesCriteriaColumns(index, null, criteria))
			{
				outerCriteria = criteria;
				residualCriteria = null;
			}
			else
			{
				outerCriteria = null;
				residualCriteria = criteria;
				if (criteria is SearchCriteriaAnd)
				{
					int i = 0;
					while (i < ((SearchCriteriaAnd)criteria).NestedCriteria.Length)
					{
						SearchCriteria item = ((SearchCriteriaAnd)criteria).NestedCriteria[i];
						if (this.IndexSatisfiesCriteriaColumns(index, null, item))
						{
							List<SearchCriteria> list = new List<SearchCriteria>(4);
							List<SearchCriteria> list2 = new List<SearchCriteria>(4);
							for (int j = 0; j < ((SearchCriteriaAnd)criteria).NestedCriteria.Length; j++)
							{
								item = ((SearchCriteriaAnd)criteria).NestedCriteria[j];
								if (j == i || (j > i && this.IndexSatisfiesCriteriaColumns(index, null, item)))
								{
									list2.Add(item);
								}
								else
								{
									list.Add(item);
								}
							}
							if (list.Count == 1)
							{
								residualCriteria = list[0];
							}
							else
							{
								residualCriteria = Factory.CreateSearchCriteriaAnd(list.ToArray());
							}
							if (list2.Count == 1)
							{
								outerCriteria = list2[0];
								break;
							}
							outerCriteria = Factory.CreateSearchCriteriaAnd(list2.ToArray());
							break;
						}
						else
						{
							i++;
						}
					}
				}
			}
			if (outerCriteria != null)
			{
				outerCriteria = outerCriteria.InspectAndFix(null, this.CompareInfo, false);
			}
			if (residualCriteria != null)
			{
				residualCriteria = residualCriteria.InspectAndFix(null, this.CompareInfo, false);
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000361C0 File Offset: 0x000343C0
		private void SimplifyCriteria(IIndex index, ref SearchCriteria criteria)
		{
			IReadOnlyDictionary<Column, Column> localRenameDictionary = QueryPlanner.GetEffectiveRenameDictionary(index, this.renameDictionary);
			criteria = criteria.InspectAndFix((localRenameDictionary == null) ? null : new SearchCriteria.InspectAndFixCriteriaDelegate(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				if (criterion is SearchCriteriaCompare)
				{
					SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
					ConstantColumn constantColumn = searchCriteriaCompare.Rhs as ConstantColumn;
					if (constantColumn != null)
					{
						Column lhs = searchCriteriaCompare.Lhs;
						if (localRenameDictionary.TryGetValue(lhs, out lhs) && lhs is ConstantColumn)
						{
							SearchCriteriaCompare searchCriteriaCompare2 = Factory.CreateSearchCriteriaCompare(lhs, searchCriteriaCompare.RelOp, constantColumn);
							bool? flag = new bool?(searchCriteriaCompare2.Evaluate(EmptyTwir.Instance, compareInfo));
							if (flag != null)
							{
								if (!flag.Value)
								{
									return Factory.CreateSearchCriteriaFalse();
								}
								return Factory.CreateSearchCriteriaTrue();
							}
						}
					}
				}
				return criterion;
			}), this.CompareInfo, true);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0003620C File Offset: 0x0003440C
		private List<Column> GetBaseTableIndexColumns(FullTextIndex fullTextIndex)
		{
			for (int i = 0; i < this.eagerIndexes.Count; i++)
			{
				if (this.eagerIndexes[i].Unique && QueryPlanner.AreColumnsSatisfiableByIndexes(null, fullTextIndex, null, false, this.eagerIndexes[i].Columns))
				{
					return new List<Column>(this.eagerIndexes[i].Columns);
				}
			}
			return null;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00036278 File Offset: 0x00034478
		private List<Column> GetBaseTableIndexColumns(IIndex index)
		{
			if (index.IndexTable == index.Table)
			{
				return new List<Column>(index.Columns);
			}
			Index index2 = null;
			for (int i = 0; i < this.eagerIndexes.Count; i++)
			{
				if (this.eagerIndexes[i].Unique && QueryPlanner.AreColumnsSatisfiableByIndexes(null, index, null, false, this.eagerIndexes[i].Columns))
				{
					index2 = this.eagerIndexes[i];
					break;
				}
			}
			return new List<Column>(index2.Columns);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00036308 File Offset: 0x00034508
		private Index GetForeignKeyIndex()
		{
			Index result = null;
			for (int i = 0; i < this.eagerIndexes.Count; i++)
			{
				if (this.eagerIndexes[i].Unique)
				{
					bool flag = true;
					for (int j = 0; j < this.eagerIndexes[i].Columns.Count; j++)
					{
						if (!this.CurrentPlan.ColumnsToFetch.Contains(this.eagerIndexes[i].Columns[j]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						result = this.eagerIndexes[i];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000363A6 File Offset: 0x000345A6
		private bool IndexOnly(IPseudoIndex index, SearchCriteria remainingCriteria)
		{
			return this.TwoIndexOnly(index, null, remainingCriteria);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000363B4 File Offset: 0x000345B4
		private bool TwoIndexOnly(IPseudoIndex index, IPseudoIndex secondIndex, SearchCriteria remainingCriteria)
		{
			return index.ShouldBeCurrent && (secondIndex == null || secondIndex.ShouldBeCurrent) && !this.IndexMayBeStale && QueryPlanner.AreColumnsSatisfiableByIndexes(this.renameDictionary, index, secondIndex, false, this.fullColumnsToFetch) && QueryPlanner.AreColumnsSatisfiableByIndexes(this.renameDictionary, index, secondIndex, true, this.truncatedColumnsToFetch) && (remainingCriteria == null || this.IndexSatisfiesCriteriaColumns(index, secondIndex, remainingCriteria));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00036604 File Offset: 0x00034804
		private bool IndexSatisfiesCriteriaColumns(IIndex index, IIndex secondIndex, SearchCriteria criteria)
		{
			bool result = true;
			if (criteria != null)
			{
				criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
				{
					if (criterion is SearchCriteriaAnd || criterion is SearchCriteriaOr || criterion is SearchCriteriaNot || criterion is SearchCriteriaNear || criterion is SearchCriteriaTrue || criterion is SearchCriteriaFalse)
					{
						return criterion;
					}
					if (criterion is SearchCriteriaCompare)
					{
						SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
						Column lhs = searchCriteriaCompare.Lhs;
						Column rhs = searchCriteriaCompare.Rhs;
						if (this.GetFullTextFlavor(criterion) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
						{
							result = false;
						}
						else if (!QueryPlanner.IsColumnSatisfiableByIndexes(this.renameDictionary, index, secondIndex, false, rhs))
						{
							result = false;
						}
						else if (!QueryPlanner.IsColumnSatisfiableByIndexes(this.renameDictionary, index, secondIndex, QueryPlanner.CanAcceptTruncatedLhsColumnForCompare(rhs), lhs))
						{
							result = false;
						}
						return null;
					}
					if (criterion is SearchCriteriaBitMask)
					{
						SearchCriteriaBitMask searchCriteriaBitMask = (SearchCriteriaBitMask)criterion;
						Column lhs2 = searchCriteriaBitMask.Lhs;
						Column rhs2 = searchCriteriaBitMask.Rhs;
						if (!QueryPlanner.IsColumnSatisfiableByIndexes(this.renameDictionary, index, secondIndex, false, rhs2))
						{
							result = false;
						}
						else if (!QueryPlanner.IsColumnSatisfiableByIndexes(this.renameDictionary, index, secondIndex, false, lhs2))
						{
							result = false;
						}
						return null;
					}
					if (criterion is SearchCriteriaText)
					{
						SearchCriteriaText searchCriteriaText = criterion as SearchCriteriaText;
						Column lhs3 = searchCriteriaText.Lhs;
						Column rhs3 = searchCriteriaText.Rhs;
						if (this.GetFullTextFlavor(criterion) == FullTextIndexSchema.CriteriaFullTextFlavor.MustBeServiced)
						{
							result = false;
						}
						else if (!QueryPlanner.IsColumnSatisfiableByIndexes(this.renameDictionary, index, secondIndex, false, lhs3))
						{
							result = false;
						}
						else if (!QueryPlanner.IsColumnSatisfiableByIndexes(this.renameDictionary, index, secondIndex, false, rhs3))
						{
							result = false;
						}
						return null;
					}
					result = false;
					return null;
				}, this.CompareInfo, false);
			}
			return result;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00036704 File Offset: 0x00034904
		[Conditional("DEBUG")]
		private void AssertIndexImplicitCriteria(bool topLevel, IIndex index, SearchCriteria criteria)
		{
			if (topLevel && index.ConstantColumns != null && index.ConstantColumns.Count != 0 && !(criteria is SearchCriteriaFalse))
			{
				HashSet<Column> constantColumnsSeen = new HashSet<Column>();
				criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
				{
					if (criterion is SearchCriteriaAnd)
					{
						return criterion;
					}
					if (criterion is SearchCriteriaCompare)
					{
						SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
						if (index.ConstantColumns.Contains(searchCriteriaCompare.Lhs) && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal && searchCriteriaCompare.Rhs is ConstantColumn && searchCriteriaCompare.Rhs == index.RenameDictionary[searchCriteriaCompare.Lhs])
						{
							constantColumnsSeen.Add(searchCriteriaCompare.Lhs);
						}
					}
					return null;
				}, null, false);
			}
		}

		// Token: 0x0400057A RID: 1402
		private const int MaximumDepthOfPlanStack = 30;

		// Token: 0x0400057B RID: 1403
		private const int DatabasePageSize = 32768;

		// Token: 0x0400057C RID: 1404
		private const int BaseRowSize = 24576;

		// Token: 0x0400057D RID: 1405
		private const int MaxColumnSize = 500;

		// Token: 0x0400057E RID: 1406
		private const int DefaultCardinality = 90000;

		// Token: 0x0400057F RID: 1407
		private const int DefaultCardinalityWithTableFunction = 50;

		// Token: 0x04000580 RID: 1408
		private const double DefaultFilterFactor = 0.2;

		// Token: 0x04000581 RID: 1409
		private const double DefaultMaximumFilterFactor = 0.9;

		// Token: 0x04000582 RID: 1410
		private static Column fakeConstantColumn;

		// Token: 0x04000583 RID: 1411
		private static bool canUseFullTextIndex = true;

		// Token: 0x04000584 RID: 1412
		private readonly Context context;

		// Token: 0x04000585 RID: 1413
		private readonly Table table;

		// Token: 0x04000586 RID: 1414
		private readonly object[] parameters;

		// Token: 0x04000587 RID: 1415
		private readonly SearchCriteria criteria;

		// Token: 0x04000588 RID: 1416
		private readonly SearchCriteria outerCriteria;

		// Token: 0x04000589 RID: 1417
		private readonly IReadOnlyDictionary<Column, Column> renameDictionary;

		// Token: 0x0400058A RID: 1418
		private readonly CategorizedQueryParams categorizedQueryParams;

		// Token: 0x0400058B RID: 1419
		private readonly SortOrder sortOrder;

		// Token: 0x0400058C RID: 1420
		private readonly int skipTo;

		// Token: 0x0400058D RID: 1421
		private readonly int maxRows;

		// Token: 0x0400058E RID: 1422
		private readonly bool mustUseLazyIndex;

		// Token: 0x0400058F RID: 1423
		private readonly bool backwards;

		// Token: 0x04000590 RID: 1424
		private readonly bool simplePlanOnly;

		// Token: 0x04000591 RID: 1425
		private readonly bool allowUnrestrictedComplexPlans;

		// Token: 0x04000592 RID: 1426
		private readonly bool canJoinToBaseTable;

		// Token: 0x04000593 RID: 1427
		private readonly QueryPlanner.Hints hints;

		// Token: 0x04000594 RID: 1428
		private readonly IList<IIndex> lazyIndexes;

		// Token: 0x04000595 RID: 1429
		private readonly IList<IIndex> masterIndexes;

		// Token: 0x04000596 RID: 1430
		private readonly IList<Index> eagerIndexes;

		// Token: 0x04000597 RID: 1431
		private readonly FullTextIndex fullTextIndex;

		// Token: 0x04000598 RID: 1432
		private Bookmark bookmark;

		// Token: 0x04000599 RID: 1433
		private SearchCriteria bookmarkCriteria;

		// Token: 0x0400059A RID: 1434
		private List<Column> fullColumnsToFetch;

		// Token: 0x0400059B RID: 1435
		private IList<Column> longValueColumnsToPreread;

		// Token: 0x0400059C RID: 1436
		private List<Column> truncatedColumnsToFetch;

		// Token: 0x0400059D RID: 1437
		private List<QueryPlanner.PlanStackFrame> planStack;

		// Token: 0x0400059E RID: 1438
		private int topOfStackIndex;

		// Token: 0x0400059F RID: 1439
		private int traceIndent;

		// Token: 0x040005A0 RID: 1440
		private int planAlternativesConsidered;

		// Token: 0x040005A1 RID: 1441
		private int lowestCost;

		// Token: 0x040005A2 RID: 1442
		private int lowestCardinality;

		// Token: 0x040005A3 RID: 1443
		private SimpleQueryOperator.SimpleQueryOperatorDefinition lowestCostPlan;

		// Token: 0x040005A4 RID: 1444
		private bool perfectLazyIndexPlan;

		// Token: 0x040005A5 RID: 1445
		private bool perfectEagerIndexPlan;

		// Token: 0x040005A6 RID: 1446
		private IList<string> diagnosticTrace;

		// Token: 0x020000FE RID: 254
		public abstract class QueryPlanTracker : IExecutionPlanner
		{
			// Token: 0x06000A68 RID: 2664 RVA: 0x0003677D File Offset: 0x0003497D
			protected QueryPlanTracker(QueryPlanner planner)
			{
				this.qp = planner;
			}

			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0003678C File Offset: 0x0003498C
			protected QueryPlanner QueryPlanner
			{
				get
				{
					return this.qp;
				}
			}

			// Token: 0x06000A6A RID: 2666 RVA: 0x00036794 File Offset: 0x00034994
			public void AppendToTraceContentBuilder(TraceContentBuilder cb, int indentLevel, string title)
			{
				cb.Indent(indentLevel);
				cb.AppendLine(title);
				foreach (string value in this.GetExecutionTrace())
				{
					cb.Indent(indentLevel);
					cb.AppendLine(value);
				}
			}

			// Token: 0x06000A6B RID: 2667
			internal abstract void CreatePlanWithTrace();

			// Token: 0x06000A6C RID: 2668 RVA: 0x000367F8 File Offset: 0x000349F8
			private IList<string> GetExecutionTrace()
			{
				if (this.qp != null)
				{
					this.qp.EnableDiagnosticTrace();
					try
					{
						this.CreatePlanWithTrace();
					}
					catch (StoreException ex)
					{
						NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
						return new string[]
						{
							ex.GetType().Name,
							ex.Message,
							ex.StackTrace
						};
					}
					return this.qp.GetDiagnosticTrace();
				}
				return new string[0];
			}

			// Token: 0x040005A9 RID: 1449
			private readonly QueryPlanner qp;
		}

		// Token: 0x020000FF RID: 255
		public sealed class QueryPlanTrackerCost : QueryPlanner.QueryPlanTracker
		{
			// Token: 0x06000A6D RID: 2669 RVA: 0x0003687C File Offset: 0x00034A7C
			private QueryPlanTrackerCost(QueryPlanner planner) : base(planner)
			{
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x00036885 File Offset: 0x00034A85
			public static QueryPlanner.QueryPlanTrackerCost Create(QueryPlanner planner)
			{
				return new QueryPlanner.QueryPlanTrackerCost(planner);
			}

			// Token: 0x06000A6F RID: 2671 RVA: 0x00036890 File Offset: 0x00034A90
			internal override void CreatePlanWithTrace()
			{
				int num;
				int num2;
				base.QueryPlanner.CreatePlanDefinition(out num, out num2);
			}
		}

		// Token: 0x02000100 RID: 256
		public sealed class QueryPlanTrackerCount : QueryPlanner.QueryPlanTracker
		{
			// Token: 0x06000A70 RID: 2672 RVA: 0x000368AD File Offset: 0x00034AAD
			private QueryPlanTrackerCount(QueryPlanner planner) : base(planner)
			{
			}

			// Token: 0x06000A71 RID: 2673 RVA: 0x000368B6 File Offset: 0x00034AB6
			public static QueryPlanner.QueryPlanTrackerCount Create(QueryPlanner planner)
			{
				return new QueryPlanner.QueryPlanTrackerCount(planner);
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x000368BE File Offset: 0x00034ABE
			internal override void CreatePlanWithTrace()
			{
				base.QueryPlanner.CreateCountPlanDefinition();
			}
		}

		// Token: 0x02000101 RID: 257
		public sealed class QueryPlanTrackerOrdinal : QueryPlanner.QueryPlanTracker
		{
			// Token: 0x06000A73 RID: 2675 RVA: 0x000368CC File Offset: 0x00034ACC
			private QueryPlanTrackerOrdinal(QueryPlanner planner) : base(planner)
			{
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x000368D5 File Offset: 0x00034AD5
			public static QueryPlanner.QueryPlanTrackerOrdinal Create(QueryPlanner planner)
			{
				return new QueryPlanner.QueryPlanTrackerOrdinal(planner);
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x000368DD File Offset: 0x00034ADD
			internal override void CreatePlanWithTrace()
			{
				base.QueryPlanner.CreateOrdinalPositionPlanDefinition();
			}
		}

		// Token: 0x02000102 RID: 258
		public class Hints
		{
			// Token: 0x040005AA RID: 1450
			public static QueryPlanner.Hints Empty = new QueryPlanner.Hints();

			// Token: 0x040005AB RID: 1451
			public QueryPlanner.Hints.GetSupplementaryCriteriaDelegate GetSupplementaryCriteria;

			// Token: 0x040005AC RID: 1452
			public Dictionary<Column, FilterFactorHint> FilterFactorHints;

			// Token: 0x02000103 RID: 259
			// (Invoke) Token: 0x06000A79 RID: 2681
			public delegate SearchCriteria GetSupplementaryCriteriaDelegate(Context context, SearchCriteria originalCriteria, int maxNumberOfTerms);
		}

		// Token: 0x02000104 RID: 260
		private class AndOrLeg
		{
			// Token: 0x06000A7C RID: 2684 RVA: 0x000368FF File Offset: 0x00034AFF
			public AndOrLeg(SimpleQueryOperator.SimpleQueryOperatorDefinition planOperator, int planCost, int planCardinality)
			{
				this.planOperator = planOperator;
				this.planCost = planCost;
				this.planCardinality = planCardinality;
			}

			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0003691C File Offset: 0x00034B1C
			public SimpleQueryOperator.SimpleQueryOperatorDefinition PlanOperator
			{
				get
				{
					return this.planOperator;
				}
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00036924 File Offset: 0x00034B24
			public int PlanCost
			{
				get
				{
					return this.planCost;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0003692C File Offset: 0x00034B2C
			public int PlanCardinality
			{
				get
				{
					return this.planCardinality;
				}
			}

			// Token: 0x040005AD RID: 1453
			private readonly SimpleQueryOperator.SimpleQueryOperatorDefinition planOperator;

			// Token: 0x040005AE RID: 1454
			private readonly int planCost;

			// Token: 0x040005AF RID: 1455
			private readonly int planCardinality;
		}

		// Token: 0x02000105 RID: 261
		private class PlanStackFrame
		{
			// Token: 0x06000A80 RID: 2688 RVA: 0x00036934 File Offset: 0x00034B34
			internal PlanStackFrame(SortOrder currentSortOrder, ISet<Column> currentConstantColumns, bool skipToSatisfied, bool maxRowsSatisfied, SearchCriteria remainingCriteria, bool columnsToFetchSatisfied, bool bookmarkSatisfied, int currentCardinality, bool indexMayBeStale)
			{
				this.currentPlan = null;
				this.currentPlanCost = 0;
				this.currentSortOrder = currentSortOrder;
				this.currentConstantColumns = currentConstantColumns;
				this.skipToSatisfied = skipToSatisfied;
				this.maxRowsSatisfied = maxRowsSatisfied;
				this.remainingCriteria = remainingCriteria;
				this.columnsToFetchSatisfied = columnsToFetchSatisfied;
				this.bookmarkSatisfied = bookmarkSatisfied;
				this.currentCardinality = currentCardinality;
				this.indexMayBeStale = indexMayBeStale;
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x0003699C File Offset: 0x00034B9C
			internal PlanStackFrame(QueryPlanner.PlanStackFrame inputFrame) : this(inputFrame.currentSortOrder, inputFrame.currentConstantColumns, inputFrame.skipToSatisfied, inputFrame.maxRowsSatisfied, inputFrame.remainingCriteria, inputFrame.columnsToFetchSatisfied, inputFrame.bookmarkSatisfied, inputFrame.currentCardinality, inputFrame.indexMayBeStale)
			{
				this.currentPlan = inputFrame.currentPlan;
				this.currentPlanCost = inputFrame.currentPlanCost;
			}

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000369FD File Offset: 0x00034BFD
			// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00036A05 File Offset: 0x00034C05
			public SimpleQueryOperator.SimpleQueryOperatorDefinition CurrentPlan
			{
				get
				{
					return this.currentPlan;
				}
				set
				{
					this.currentPlan = value;
				}
			}

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00036A0E File Offset: 0x00034C0E
			// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00036A16 File Offset: 0x00034C16
			public int CurrentPlanCost
			{
				get
				{
					return this.currentPlanCost;
				}
				set
				{
					this.currentPlanCost = value;
				}
			}

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00036A1F File Offset: 0x00034C1F
			// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00036A27 File Offset: 0x00034C27
			public SortOrder CurrentSortOrder
			{
				get
				{
					return this.currentSortOrder;
				}
				set
				{
					this.currentSortOrder = value;
				}
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00036A30 File Offset: 0x00034C30
			// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00036A38 File Offset: 0x00034C38
			public ISet<Column> CurrentConstantColumns
			{
				get
				{
					return this.currentConstantColumns;
				}
				set
				{
					this.currentConstantColumns = value;
				}
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00036A41 File Offset: 0x00034C41
			// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00036A49 File Offset: 0x00034C49
			public bool SkipToSatisfied
			{
				get
				{
					return this.skipToSatisfied;
				}
				set
				{
					this.skipToSatisfied = value;
				}
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00036A52 File Offset: 0x00034C52
			// (set) Token: 0x06000A8D RID: 2701 RVA: 0x00036A5A File Offset: 0x00034C5A
			public bool MaxRowsSatisfied
			{
				get
				{
					return this.maxRowsSatisfied;
				}
				set
				{
					this.maxRowsSatisfied = value;
				}
			}

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00036A63 File Offset: 0x00034C63
			// (set) Token: 0x06000A8F RID: 2703 RVA: 0x00036A6B File Offset: 0x00034C6B
			public SearchCriteria RemainingCriteria
			{
				get
				{
					return this.remainingCriteria;
				}
				set
				{
					this.remainingCriteria = value;
				}
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00036A74 File Offset: 0x00034C74
			// (set) Token: 0x06000A91 RID: 2705 RVA: 0x00036A7C File Offset: 0x00034C7C
			public bool ColumnsToFetchSatisfied
			{
				get
				{
					return this.columnsToFetchSatisfied;
				}
				set
				{
					this.columnsToFetchSatisfied = value;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00036A85 File Offset: 0x00034C85
			// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00036A8D File Offset: 0x00034C8D
			public bool BookmarkSatisfied
			{
				get
				{
					return this.bookmarkSatisfied;
				}
				set
				{
					this.bookmarkSatisfied = value;
				}
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00036A96 File Offset: 0x00034C96
			// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00036A9E File Offset: 0x00034C9E
			public int CurrentCardinality
			{
				get
				{
					return this.currentCardinality;
				}
				set
				{
					this.currentCardinality = value;
				}
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00036AA7 File Offset: 0x00034CA7
			// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00036AAF File Offset: 0x00034CAF
			public bool IndexMayBeStale
			{
				get
				{
					return this.indexMayBeStale;
				}
				set
				{
					this.indexMayBeStale = value;
				}
			}

			// Token: 0x040005B0 RID: 1456
			private SimpleQueryOperator.SimpleQueryOperatorDefinition currentPlan;

			// Token: 0x040005B1 RID: 1457
			private int currentPlanCost;

			// Token: 0x040005B2 RID: 1458
			private SortOrder currentSortOrder;

			// Token: 0x040005B3 RID: 1459
			private ISet<Column> currentConstantColumns;

			// Token: 0x040005B4 RID: 1460
			private bool skipToSatisfied;

			// Token: 0x040005B5 RID: 1461
			private bool maxRowsSatisfied;

			// Token: 0x040005B6 RID: 1462
			private SearchCriteria remainingCriteria;

			// Token: 0x040005B7 RID: 1463
			private bool columnsToFetchSatisfied;

			// Token: 0x040005B8 RID: 1464
			private bool bookmarkSatisfied;

			// Token: 0x040005B9 RID: 1465
			private int currentCardinality;

			// Token: 0x040005BA RID: 1466
			private bool indexMayBeStale;
		}
	}
}
