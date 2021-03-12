using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200079D RID: 1949
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AggregationQueryResult : DisposableObject, IQueryResult, IDisposable
	{
		// Token: 0x0600497F RID: 18815 RVA: 0x00134074 File Offset: 0x00132274
		private AggregationQueryResult(IQueryResult rawQueryResult, QueryFilter aggregationFilter, int columnsRequested)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.rawQueryResult = rawQueryResult;
				this.aggregationFilter = aggregationFilter;
				this.aggregationPropertyBag = new MailboxSyncPropertyBag(rawQueryResult.Columns.PropertyDefinitions);
				this.aggregationPropertyBag.AddColumnsFromFilter(aggregationFilter);
				this.columnsRequested = columnsRequested;
				this.unusedRows = new Queue<object[]>();
				this.currentRow = -1;
				this.moreRowsAvailableToFetch = true;
				disposeGuard.Success();
			}
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x00134108 File Offset: 0x00132308
		internal static AggregationQueryResult FromQueryResult(IQueryResult rawQueryResult, QueryFilter aggregationFilter, int columnsRequested)
		{
			Util.ThrowOnNullArgument(rawQueryResult, "rawQueryResult");
			Util.ThrowOnNullArgument(aggregationFilter, "aggregationFilter");
			return new AggregationQueryResult(rawQueryResult, aggregationFilter, columnsRequested);
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x00134128 File Offset: 0x00132328
		public int EstimatedRowCount
		{
			get
			{
				this.CheckDisposed(null);
				return this.rawQueryResult.EstimatedRowCount;
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x0013413C File Offset: 0x0013233C
		public StoreSession StoreSession
		{
			get
			{
				this.CheckDisposed(null);
				return this.rawQueryResult.StoreSession;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x00134150 File Offset: 0x00132350
		public ColumnPropertyDefinitions Columns
		{
			get
			{
				this.CheckDisposed(null);
				return this.rawQueryResult.Columns;
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x00134164 File Offset: 0x00132364
		public int CurrentRow
		{
			get
			{
				this.CheckDisposed(null);
				return this.currentRow;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x00134173 File Offset: 0x00132373
		public new bool IsDisposed
		{
			get
			{
				return base.IsDisposed;
			}
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0013417B File Offset: 0x0013237B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AggregationQueryResult>(this);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00134183 File Offset: 0x00132383
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.rawQueryResult.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0013419A File Offset: 0x0013239A
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter, SeekToConditionFlags flags)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("SeekToCondition");
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x001341AD File Offset: 0x001323AD
		public bool SeekToCondition(uint bookMark, bool useForwardDirection, QueryFilter seekFilter, SeekToConditionFlags flags)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("SeekToCondition");
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x001341C0 File Offset: 0x001323C0
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("SeekToCondition");
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x001341D3 File Offset: 0x001323D3
		public int SeekToOffset(SeekReference reference, int offset)
		{
			this.CheckDisposed(null);
			if (reference == SeekReference.OriginBeginning && offset == 0)
			{
				return this.rawQueryResult.SeekToOffset(reference, offset);
			}
			throw new NotSupportedException("SeekToOffset is only supported at the beginning of the result set");
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x001341FA File Offset: 0x001323FA
		public void SetTableColumns(ICollection<PropertyDefinition> propertyDefinitions)
		{
			this.CheckDisposed(null);
			this.rawQueryResult.SetTableColumns(propertyDefinitions);
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x0013420F File Offset: 0x0013240F
		public object[][] GetRows(int rowCount, out bool mightBeMoreRows)
		{
			return this.GetRows(rowCount, QueryRowsFlags.None, out mightBeMoreRows);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x0013421C File Offset: 0x0013241C
		public object[][] GetRows(int rowCount, QueryRowsFlags flags, out bool mightBeMoreRows)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<QueryRowsFlags>(flags, "flags");
			int num = rowCount * 3;
			if (!this.moreRowsAvailableToFetch || num <= this.unusedRows.Count)
			{
				mightBeMoreRows = this.moreRowsAvailableToFetch;
			}
			else
			{
				num -= this.unusedRows.Count;
				object[][] rows = this.rawQueryResult.GetRows(num, flags, out mightBeMoreRows);
				foreach (object[] item in rows)
				{
					this.unusedRows.Enqueue(item);
				}
				this.moreRowsAvailableToFetch = mightBeMoreRows;
			}
			object[][] array2 = this.FilterUnusedRows(rowCount);
			if (this.unusedRows.Count > 0)
			{
				mightBeMoreRows = true;
			}
			if (array2.Length > 0)
			{
				this.currentRow += array2.Length;
			}
			return array2;
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x001342DC File Offset: 0x001324DC
		public object[][] ExpandRow(int rowCount, long categoryId, out int rowsInExpandedCategory)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("ExpandRow");
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x001342EF File Offset: 0x001324EF
		public int CollapseRow(long categoryId)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("CollapseRow");
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00134302 File Offset: 0x00132502
		public uint CreateBookmark()
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("CreateBookmark");
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00134315 File Offset: 0x00132515
		public void FreeBookmark(uint bookmarkPosition)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("FreeBookmark");
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00134328 File Offset: 0x00132528
		public int SeekRowBookmark(uint bookmarkPosition, int rowCount, bool wantRowsSought, out bool soughtLess, out bool positionChanged)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("SeekRowBookmark");
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x0013433B File Offset: 0x0013253B
		public NativeStorePropertyDefinition[] GetAllPropertyDefinitions(params PropertyTagPropertyDefinition[] excludeProperties)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("GetAllPropertyDefinitions");
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0013434E File Offset: 0x0013254E
		public byte[] GetCollapseState(byte[] instanceKey)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("GetCollapseState");
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00134361 File Offset: 0x00132561
		public uint SetCollapseState(byte[] collapseState)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("SetCollapseState");
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x00134374 File Offset: 0x00132574
		public object Advise(SubscriptionSink subscriptionSink, bool asyncMode)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("Advise");
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00134387 File Offset: 0x00132587
		public void Unadvise(object notificationHandle)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("Unadvise");
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x0013439A File Offset: 0x0013259A
		public IStorePropertyBag[] GetPropertyBags(int rowCount)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException("GetPropertyBags");
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x001343B0 File Offset: 0x001325B0
		private object[][] FilterUnusedRows(int rowCount)
		{
			this.CheckDisposed(null);
			List<object[]> list = new List<object[]>(rowCount);
			while (list.Count < rowCount && this.unusedRows.Count > 0)
			{
				object[] array = this.unusedRows.Dequeue();
				this.aggregationPropertyBag.Bind(array);
				if (EvaluatableFilter.Evaluate(this.aggregationFilter, this.aggregationPropertyBag))
				{
					if (array.Length > this.columnsRequested)
					{
						Array.Resize<object>(ref array, this.columnsRequested);
					}
					list.Add(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x04002798 RID: 10136
		private const int FetchMultiplier = 3;

		// Token: 0x04002799 RID: 10137
		private readonly IQueryResult rawQueryResult;

		// Token: 0x0400279A RID: 10138
		private readonly QueryFilter aggregationFilter;

		// Token: 0x0400279B RID: 10139
		private readonly MailboxSyncPropertyBag aggregationPropertyBag;

		// Token: 0x0400279C RID: 10140
		private readonly int columnsRequested;

		// Token: 0x0400279D RID: 10141
		private Queue<object[]> unusedRows;

		// Token: 0x0400279E RID: 10142
		private int currentRow;

		// Token: 0x0400279F RID: 10143
		private bool moreRowsAvailableToFetch;
	}
}
