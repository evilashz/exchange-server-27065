using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Transport.QueueViewer
{
	// Token: 0x02000357 RID: 855
	internal class PagingEngine<ObjectType, SchemaType> where ObjectType : PagedDataObject where SchemaType : PagedObjectSchema, new()
	{
		// Token: 0x06002522 RID: 9506 RVA: 0x0008F9CC File Offset: 0x0008DBCC
		public PagingEngine()
		{
			this.maxQueryResultCount = Components.TransportAppConfig.RemoteDelivery.MaxQueryResultCount;
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x0008FA2F File Offset: 0x0008DC2F
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x0008FA37 File Offset: 0x0008DC37
		public bool SearchForward
		{
			get
			{
				return this.searchForward;
			}
			set
			{
				this.searchForward = value;
				ExTraceGlobals.QueuingTracer.TraceDebug<bool>((long)this.GetHashCode(), "Search forward: {0}", this.searchForward);
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x0008FA5C File Offset: 0x0008DC5C
		// (set) Token: 0x06002526 RID: 9510 RVA: 0x0008FA64 File Offset: 0x0008DC64
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
				ExTraceGlobals.QueuingTracer.TraceDebug<int>((long)this.GetHashCode(), "Page size: {0}", this.pageSize);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x0008FA89 File Offset: 0x0008DC89
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x0008FA94 File Offset: 0x0008DC94
		public ObjectType BookmarkObject
		{
			get
			{
				return this.bookmarkObject;
			}
			set
			{
				this.bookmarkObject = value;
				ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "Bookmark object: {0}", new object[]
				{
					(this.bookmarkObject != null) ? this.bookmarkObject : "(null)"
				});
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0008FAE8 File Offset: 0x0008DCE8
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x0008FAF0 File Offset: 0x0008DCF0
		public int BookmarkIndex
		{
			get
			{
				return this.bookmarkIndex;
			}
			set
			{
				this.bookmarkIndex = value;
				ExTraceGlobals.QueuingTracer.TraceDebug<int>((long)this.GetHashCode(), "Bookmark index: {0}", this.bookmarkIndex);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x0008FB15 File Offset: 0x0008DD15
		// (set) Token: 0x0600252C RID: 9516 RVA: 0x0008FB1D File Offset: 0x0008DD1D
		public bool IncludeBookmark
		{
			get
			{
				return this.includeBookmark;
			}
			set
			{
				this.includeBookmark = value;
				ExTraceGlobals.QueuingTracer.TraceDebug<bool>((long)this.GetHashCode(), "Include bookmark: {0}", this.includeBookmark);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0008FB42 File Offset: 0x0008DD42
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x0008FB4A File Offset: 0x0008DD4A
		public bool IncludeDetails
		{
			get
			{
				return this.includeDetails;
			}
			set
			{
				this.includeDetails = value;
				ExTraceGlobals.QueuingTracer.TraceDebug<bool>((long)this.GetHashCode(), "Include details: {0}", this.includeDetails);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x0008FB6F File Offset: 0x0008DD6F
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x0008FB77 File Offset: 0x0008DD77
		public bool IncludeComponentLatencyInfo
		{
			get
			{
				return this.includeComponentLatencyInfo;
			}
			set
			{
				this.includeComponentLatencyInfo = value;
				ExTraceGlobals.QueuingTracer.TraceDebug<bool>((long)this.GetHashCode(), "Include ComponentLatencyInfo: {0}", this.includeComponentLatencyInfo);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0008FB9C File Offset: 0x0008DD9C
		public bool FilterUsesOnlyBasicFields
		{
			get
			{
				return this.filterUsesOnlyBasicFields;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x0008FBA4 File Offset: 0x0008DDA4
		public bool IdentitySearch
		{
			get
			{
				return this.identitySearch;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x0008FBAC File Offset: 0x0008DDAC
		public object IdentitySearchValue
		{
			get
			{
				return this.identitySearchValue;
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0008FBB4 File Offset: 0x0008DDB4
		public void ResetResultSet()
		{
			this.resultSet.Clear();
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0008FBC1 File Offset: 0x0008DDC1
		public bool AddToResultSet(ObjectType dataObject)
		{
			if (this.resultSet.Count == this.maxQueryResultCount || this.resultSet.Count == this.PageSize)
			{
				return false;
			}
			this.resultSet.Add(dataObject);
			return true;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0008FBF8 File Offset: 0x0008DDF8
		public void SetFilter(QueryFilter queryFilter)
		{
			this.filterUsesOnlyBasicFields = true;
			if (queryFilter == null)
			{
				this.filter = null;
				this.csharpFilters = null;
				return;
			}
			this.filter = new List<PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType>>();
			this.csharpFilters = new List<PagingEngine<ObjectType, SchemaType>.CSharpFilterOperation<ObjectType>>();
			this.RecurseExpressionTree(queryFilter);
			ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "Query filter parsed to a node structure");
			if (this.identitySearch)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "Search by Identity {0}", new object[]
				{
					this.identitySearchValue
				});
			}
			foreach (PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType> arg in this.filter)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType>>((long)this.GetHashCode(), "{0}", arg);
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0008FCD8 File Offset: 0x0008DED8
		public void SetSortOrder(QueueViewerSortOrderEntry[] originalSortOrder)
		{
			this.normalizedSortOrder = new List<PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType>>();
			bool flag = true;
			if (originalSortOrder != null)
			{
				foreach (QueueViewerSortOrderEntry queueViewerSortOrderEntry in originalSortOrder)
				{
					int fieldIndex = PagingEngine<ObjectType, SchemaType>.schema.GetFieldIndex(queueViewerSortOrderEntry.Property);
					bool flag2 = true;
					foreach (PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType> internalSortOrderEntry in this.normalizedSortOrder)
					{
						if (internalSortOrderEntry.Field == fieldIndex)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						this.normalizedSortOrder.Add(new PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType>(fieldIndex, queueViewerSortOrderEntry.SortDirection));
						if (queueViewerSortOrderEntry.Property == "Identity")
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (flag)
			{
				this.normalizedSortOrder.Add(new PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType>(PagingEngine<ObjectType, SchemaType>.schema.GetFieldIndex("Identity"), ListSortDirection.Ascending));
			}
			ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "Sort order normalized");
			foreach (PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType> arg in this.normalizedSortOrder)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType>>((long)this.GetHashCode(), "{0}", arg);
			}
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0008FE3C File Offset: 0x0008E03C
		public bool ApplyFilterConditions(ObjectType dataObject, out bool failedOnBasicField)
		{
			failedOnBasicField = false;
			if (this.filter == null)
			{
				return true;
			}
			foreach (PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType> filterOperation in this.filter)
			{
				if (!filterOperation.Evaluate(dataObject))
				{
					if (PagingEngine<ObjectType, SchemaType>.schema.IsBasicField(filterOperation.Field))
					{
						failedOnBasicField = true;
					}
					return false;
				}
			}
			foreach (PagingEngine<ObjectType, SchemaType>.CSharpFilterOperation<ObjectType> csharpFilterOperation in this.csharpFilters)
			{
				if (!csharpFilterOperation.Evaluate(dataObject))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0008FF04 File Offset: 0x0008E104
		public bool ApplyFilterConditions(ObjectType dataObject)
		{
			bool flag;
			return this.ApplyFilterConditions(dataObject, out flag);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0008FF1C File Offset: 0x0008E11C
		public bool ApplyBookmarkConditions(PagedDataObject dataObject, out bool onlyBasicFieldsUsed)
		{
			onlyBasicFieldsUsed = true;
			if (this.bookmarkObject == null)
			{
				return true;
			}
			foreach (PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType> internalSortOrderEntry in this.normalizedSortOrder)
			{
				ComparisonOperator comparisonOperator = this.searchForward ? ((internalSortOrderEntry.SortDirection == ListSortDirection.Ascending) ? ComparisonOperator.GreaterThan : ComparisonOperator.LessThan) : ((internalSortOrderEntry.SortDirection == ListSortDirection.Ascending) ? ComparisonOperator.LessThan : ComparisonOperator.GreaterThan);
				int num = PagingEngine<ObjectType, SchemaType>.schema.CompareField(internalSortOrderEntry.Field, dataObject, this.bookmarkObject);
				ComparisonOperator comparisonOperator2 = (num < 0) ? ComparisonOperator.LessThan : ((num == 0) ? ComparisonOperator.Equal : ComparisonOperator.GreaterThan);
				if (!PagingEngine<ObjectType, SchemaType>.schema.IsBasicField(internalSortOrderEntry.Field))
				{
					onlyBasicFieldsUsed = false;
				}
				if (comparisonOperator2 == comparisonOperator)
				{
					return true;
				}
				if (comparisonOperator2 != ComparisonOperator.Equal)
				{
					return false;
				}
			}
			return this.includeBookmark;
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0008FFFC File Offset: 0x0008E1FC
		public bool ApplyBookmarkConditions(PagedDataObject dataObject)
		{
			bool flag;
			return this.ApplyBookmarkConditions(dataObject, out flag);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00090014 File Offset: 0x0008E214
		public ObjectType[] GetPage(int totalCount, out int pageOffset, out bool redoQuery)
		{
			pageOffset = 0;
			redoQuery = false;
			if (totalCount < this.resultSet.Count)
			{
				throw new InvalidOperationException();
			}
			this.OrderResultSet();
			int num = -1;
			int num2;
			if (this.bookmarkObject != null || this.bookmarkIndex < 0)
			{
				num2 = Math.Min(this.pageSize, this.resultSet.Count);
				if (this.searchForward)
				{
					if (num2 == 0 && totalCount > 0)
					{
						this.searchForward = false;
						this.bookmarkObject = default(ObjectType);
						redoQuery = true;
					}
					else
					{
						num = 0;
						pageOffset = totalCount - this.resultSet.Count;
					}
				}
				else if (num2 < this.pageSize && totalCount > num2)
				{
					this.searchForward = true;
					this.bookmarkObject = default(ObjectType);
					redoQuery = true;
				}
				else
				{
					num = this.resultSet.Count - num2;
					pageOffset = num;
				}
			}
			else
			{
				if (this.searchForward)
				{
					num = (this.includeBookmark ? this.bookmarkIndex : (this.bookmarkIndex + 1));
					if (num >= this.resultSet.Count)
					{
						num2 = Math.Min(this.pageSize, this.resultSet.Count);
						num = this.resultSet.Count - num2;
					}
					else
					{
						num2 = Math.Min(this.pageSize, this.resultSet.Count - num);
					}
				}
				else
				{
					int num3 = this.includeBookmark ? this.bookmarkIndex : (this.bookmarkIndex - 1);
					num2 = Math.Min(this.pageSize, this.resultSet.Count);
					if (num3 >= this.resultSet.Count)
					{
						num = this.resultSet.Count - num2;
					}
					else if (num3 < 0 || num3 + 1 < num2)
					{
						num = 0;
					}
					else
					{
						num = num3 + 1 - num2;
					}
				}
				pageOffset = num;
			}
			if (!redoQuery)
			{
				ObjectType[] array = new ObjectType[num2];
				if (num2 > 0)
				{
					this.resultSet.CopyTo(num, array, 0, num2);
				}
				return array;
			}
			return null;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000901E8 File Offset: 0x0008E3E8
		private void OrderResultSet()
		{
			if (this.resultSet.Count == 0 || this.normalizedSortOrder == null)
			{
				return;
			}
			this.comparer = new PagingEngine<ObjectType, SchemaType>.PagedDataObjectComparer<ObjectType, SchemaType>(this);
			for (int i = this.resultSet.Count / 2 - 1; i >= 0; i--)
			{
				this.SiftDown(i, this.resultSet.Count - 1);
			}
			for (int j = this.resultSet.Count - 1; j >= 1; j--)
			{
				ObjectType value = this.resultSet[0];
				this.resultSet[0] = this.resultSet[j];
				this.resultSet[j] = value;
				this.SiftDown(0, j - 1);
			}
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0009029C File Offset: 0x0008E49C
		private void SiftDown(int root, int bottom)
		{
			for (int i = 2 * root + 1; i <= bottom; i = 2 * root + 1)
			{
				if (i + 1 <= bottom && this.comparer.Compare(this.resultSet[i + 1], this.resultSet[i]) > 0)
				{
					i++;
				}
				if (this.comparer.Compare(this.resultSet[root], this.resultSet[i]) >= 0)
				{
					return;
				}
				ObjectType value = this.resultSet[root];
				this.resultSet[root] = this.resultSet[i];
				this.resultSet[i] = value;
				root = i;
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00090354 File Offset: 0x0008E554
		private void RecurseExpressionTree(QueryFilter filterNode)
		{
			AndFilter andFilter = filterNode as AndFilter;
			if (andFilter != null)
			{
				foreach (QueryFilter filterNode2 in andFilter.Filters)
				{
					this.RecurseExpressionTree(filterNode2);
				}
				return;
			}
			NotFilter notFilter = filterNode as NotFilter;
			if (notFilter != null)
			{
				ExistsFilter existsFilter = notFilter.Filter as ExistsFilter;
				if (existsFilter != null)
				{
					this.PreprocessExistsFilter(existsFilter, false);
					return;
				}
				throw new QueueViewerException(QVErrorCode.QV_E_FILTER_TYPE_NOT_SUPPORTED);
			}
			else
			{
				ExistsFilter existsFilter2 = filterNode as ExistsFilter;
				if (existsFilter2 != null)
				{
					this.PreprocessExistsFilter(existsFilter2, true);
					return;
				}
				TextFilter textFilter = filterNode as TextFilter;
				if (textFilter != null)
				{
					PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType> filterOperation = new PagingEngine<ObjectType, SchemaType>.TextFilterOperation<ObjectType, SchemaType>(textFilter);
					if (!PagingEngine<ObjectType, SchemaType>.schema.IsBasicField(filterOperation.Field))
					{
						this.filterUsesOnlyBasicFields = false;
					}
					this.filter.Add(filterOperation);
					return;
				}
				ComparisonFilter comparisonFilter = filterNode as ComparisonFilter;
				if (comparisonFilter != null)
				{
					PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType> filterOperation2 = new PagingEngine<ObjectType, SchemaType>.ComparisonFilterOperation<ObjectType, SchemaType>(comparisonFilter);
					if (!PagingEngine<ObjectType, SchemaType>.schema.IsBasicField(filterOperation2.Field))
					{
						this.filterUsesOnlyBasicFields = false;
					}
					if (comparisonFilter.Property.Name == "Identity" && comparisonFilter.ComparisonOperator == ComparisonOperator.Equal)
					{
						this.identitySearch = true;
						this.identitySearchValue = comparisonFilter.PropertyValue;
						return;
					}
					this.filter.Add(filterOperation2);
					return;
				}
				else
				{
					CSharpFilter<ObjectType> csharpFilter = filterNode as CSharpFilter<ObjectType>;
					if (csharpFilter != null)
					{
						PagingEngine<ObjectType, SchemaType>.CSharpFilterOperation<ObjectType> item = new PagingEngine<ObjectType, SchemaType>.CSharpFilterOperation<ObjectType>(csharpFilter);
						this.csharpFilters.Add(item);
						this.filterUsesOnlyBasicFields = false;
						return;
					}
					throw new QueueViewerException(QVErrorCode.QV_E_FILTER_TYPE_NOT_SUPPORTED);
				}
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000904DC File Offset: 0x0008E6DC
		private void PreprocessExistsFilter(ExistsFilter filterNode, bool affirmative)
		{
			PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType> filterOperation = new PagingEngine<ObjectType, SchemaType>.ComparisonFilterOperation<ObjectType, SchemaType>(new ComparisonFilter(affirmative ? ComparisonOperator.NotEqual : ComparisonOperator.Equal, filterNode.Property, null));
			if (!PagingEngine<ObjectType, SchemaType>.schema.IsBasicField(filterOperation.Field))
			{
				this.filterUsesOnlyBasicFields = false;
			}
			this.filter.Add(filterOperation);
		}

		// Token: 0x04001337 RID: 4919
		private const int MaxQueryResultCountDefault = 250000;

		// Token: 0x04001338 RID: 4920
		private static PagedObjectSchema schema = ObjectSchema.GetInstance<SchemaType>();

		// Token: 0x04001339 RID: 4921
		private bool filterUsesOnlyBasicFields;

		// Token: 0x0400133A RID: 4922
		private List<PagingEngine<ObjectType, SchemaType>.FilterOperation<ObjectType, SchemaType>> filter;

		// Token: 0x0400133B RID: 4923
		private List<PagingEngine<ObjectType, SchemaType>.CSharpFilterOperation<ObjectType>> csharpFilters;

		// Token: 0x0400133C RID: 4924
		private List<PagingEngine<ObjectType, SchemaType>.InternalSortOrderEntry<ObjectType, SchemaType>> normalizedSortOrder;

		// Token: 0x0400133D RID: 4925
		private PagingEngine<ObjectType, SchemaType>.PagedDataObjectComparer<ObjectType, SchemaType> comparer;

		// Token: 0x0400133E RID: 4926
		private bool searchForward = true;

		// Token: 0x0400133F RID: 4927
		private int pageSize = 1000;

		// Token: 0x04001340 RID: 4928
		private ObjectType bookmarkObject = default(ObjectType);

		// Token: 0x04001341 RID: 4929
		private int bookmarkIndex = -1;

		// Token: 0x04001342 RID: 4930
		private bool includeBookmark;

		// Token: 0x04001343 RID: 4931
		private bool includeDetails;

		// Token: 0x04001344 RID: 4932
		private bool includeComponentLatencyInfo;

		// Token: 0x04001345 RID: 4933
		private bool identitySearch;

		// Token: 0x04001346 RID: 4934
		private object identitySearchValue;

		// Token: 0x04001347 RID: 4935
		private List<ObjectType> resultSet = new List<ObjectType>();

		// Token: 0x04001348 RID: 4936
		private int maxQueryResultCount = 250000;

		// Token: 0x02000358 RID: 856
		private abstract class FilterOperation<T, S> where T : PagedDataObject where S : PagedObjectSchema, new()
		{
			// Token: 0x06002542 RID: 9538 RVA: 0x00090538 File Offset: 0x0008E738
			public FilterOperation(SinglePropertyFilter filter)
			{
				this.Field = PagingEngine<T, S>.schema.GetFieldIndex(filter.Property.Name);
				this.filter = filter;
			}

			// Token: 0x06002543 RID: 9539 RVA: 0x00090562 File Offset: 0x0008E762
			public override string ToString()
			{
				return this.filter.ToString();
			}

			// Token: 0x06002544 RID: 9540
			public abstract bool Evaluate(T dataObject);

			// Token: 0x04001349 RID: 4937
			public readonly int Field;

			// Token: 0x0400134A RID: 4938
			protected SinglePropertyFilter filter;
		}

		// Token: 0x02000359 RID: 857
		private class CSharpFilterOperation<T> where T : PagedDataObject
		{
			// Token: 0x06002545 RID: 9541 RVA: 0x0009056F File Offset: 0x0008E76F
			public CSharpFilterOperation(CSharpFilter<T> filter)
			{
				if (filter == null)
				{
					throw new ArgumentNullException("filter");
				}
				this.csharpFilter = filter;
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x0009058C File Offset: 0x0008E78C
			public bool Evaluate(T dataObject)
			{
				return this.csharpFilter.Match(dataObject);
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x0009059A File Offset: 0x0008E79A
			public override string ToString()
			{
				return this.csharpFilter.ToString();
			}

			// Token: 0x0400134B RID: 4939
			private CSharpFilter<T> csharpFilter;
		}

		// Token: 0x0200035A RID: 858
		private class ComparisonFilterOperation<T, S> : PagingEngine<ObjectType, SchemaType>.FilterOperation<T, S> where T : PagedDataObject where S : PagedObjectSchema, new()
		{
			// Token: 0x06002548 RID: 9544 RVA: 0x000905A7 File Offset: 0x0008E7A7
			public ComparisonFilterOperation(ComparisonFilter filter) : base(filter)
			{
			}

			// Token: 0x06002549 RID: 9545 RVA: 0x000905B0 File Offset: 0x0008E7B0
			public override bool Evaluate(T dataObject)
			{
				ComparisonFilter comparisonFilter = (ComparisonFilter)this.filter;
				int num = PagingEngine<T, S>.schema.CompareField(this.Field, dataObject, comparisonFilter.PropertyValue);
				ComparisonOperator comparisonOperator = (num < 0) ? ComparisonOperator.LessThan : ((num == 0) ? ComparisonOperator.Equal : ComparisonOperator.GreaterThan);
				return comparisonOperator == comparisonFilter.ComparisonOperator || (comparisonFilter.ComparisonOperator == ComparisonOperator.LessThanOrEqual && (comparisonOperator == ComparisonOperator.LessThan || comparisonOperator == ComparisonOperator.Equal)) || (comparisonFilter.ComparisonOperator == ComparisonOperator.GreaterThanOrEqual && (comparisonOperator == ComparisonOperator.GreaterThan || comparisonOperator == ComparisonOperator.Equal)) || (comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual && comparisonOperator != ComparisonOperator.Equal);
			}
		}

		// Token: 0x0200035B RID: 859
		private class TextFilterOperation<T, S> : PagingEngine<ObjectType, SchemaType>.FilterOperation<T, S> where T : PagedDataObject where S : PagedObjectSchema, new()
		{
			// Token: 0x0600254A RID: 9546 RVA: 0x00090634 File Offset: 0x0008E834
			public TextFilterOperation(TextFilter filter) : base(filter)
			{
				Type fieldType = PagingEngine<T, S>.schema.GetFieldType(this.Field);
				this.matchPattern = filter.Text;
				this.matchOptions = filter.MatchOptions;
				MethodInfo method = fieldType.GetMethod("ParsePattern");
				if (method != null)
				{
					string text = filter.Text;
					if (filter.MatchOptions == MatchOptions.Prefix)
					{
						text += "*";
					}
					else if (filter.MatchOptions == MatchOptions.Suffix)
					{
						text = "*" + text;
					}
					else if (filter.MatchOptions == MatchOptions.SubString)
					{
						text = "*" + text + "*";
					}
					object[] array = new object[]
					{
						text,
						this.matchOptions
					};
					try
					{
						this.matchPattern = method.Invoke(null, array);
					}
					catch (TargetInvocationException ex)
					{
						if (ex.InnerException is ArgumentException || ex.InnerException is ArgumentNullException)
						{
							throw new QueueViewerException(QVErrorCode.QV_E_INVALID_IDENTITY_STRING);
						}
						throw;
					}
					this.matchOptions = (MatchOptions)array[1];
				}
			}

			// Token: 0x0600254B RID: 9547 RVA: 0x00090750 File Offset: 0x0008E950
			public override bool Evaluate(T dataObject)
			{
				return PagingEngine<T, S>.schema.MatchField(this.Field, dataObject, this.matchPattern, this.matchOptions);
			}

			// Token: 0x0400134C RID: 4940
			private object matchPattern;

			// Token: 0x0400134D RID: 4941
			private MatchOptions matchOptions;
		}

		// Token: 0x0200035C RID: 860
		private class InternalSortOrderEntry<T, S> where T : PagedDataObject where S : PagedObjectSchema, new()
		{
			// Token: 0x0600254C RID: 9548 RVA: 0x00090774 File Offset: 0x0008E974
			public InternalSortOrderEntry(int field, ListSortDirection sortDirection)
			{
				this.Field = field;
				this.SortDirection = sortDirection;
			}

			// Token: 0x0600254D RID: 9549 RVA: 0x0009078A File Offset: 0x0008E98A
			public override string ToString()
			{
				return string.Format("({0} {1})", PagingEngine<T, S>.schema.GetFieldName(this.Field), this.SortDirection);
			}

			// Token: 0x0400134E RID: 4942
			public readonly int Field;

			// Token: 0x0400134F RID: 4943
			public readonly ListSortDirection SortDirection;
		}

		// Token: 0x0200035D RID: 861
		private class PagedDataObjectComparer<T, S> : IComparer<T> where T : PagedDataObject where S : PagedObjectSchema, new()
		{
			// Token: 0x0600254E RID: 9550 RVA: 0x000907B1 File Offset: 0x0008E9B1
			public PagedDataObjectComparer(PagingEngine<T, S> pagingEngine)
			{
				this.engine = pagingEngine;
			}

			// Token: 0x0600254F RID: 9551 RVA: 0x000907C0 File Offset: 0x0008E9C0
			public int Compare(T x, T y)
			{
				int num = 0;
				int i = 0;
				while (i < this.engine.normalizedSortOrder.Count)
				{
					PagingEngine<T, S>.InternalSortOrderEntry<T, S> internalSortOrderEntry = this.engine.normalizedSortOrder[i];
					num = PagingEngine<T, S>.schema.CompareField(internalSortOrderEntry.Field, x, y);
					if (num != 0)
					{
						if (internalSortOrderEntry.SortDirection == ListSortDirection.Descending)
						{
							num = -num;
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				return num;
			}

			// Token: 0x04001350 RID: 4944
			private PagingEngine<T, S> engine;
		}
	}
}
