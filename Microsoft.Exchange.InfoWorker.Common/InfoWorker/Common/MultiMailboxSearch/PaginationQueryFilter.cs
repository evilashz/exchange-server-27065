using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000206 RID: 518
	internal class PaginationQueryFilter
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003CBCC File Offset: 0x0003ADCC
		internal PaginationQueryFilter(PagingInfo pagingInfo)
		{
			Util.ThrowOnNull(pagingInfo, "pagingInfo");
			this.BuildPaginationQueryFilters(pagingInfo);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003CBE8 File Offset: 0x0003ADE8
		private void BuildPaginationQueryFilters(PagingInfo pagingInfo)
		{
			if (pagingInfo.SortValue == null || pagingInfo.SortValue.SortColumnValue == null)
			{
				return;
			}
			this.equalsQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, pagingInfo.SortValue.SortColumn, pagingInfo.SortValue.SortColumnValue);
			if (pagingInfo.Direction != PageDirection.Next)
			{
				if (pagingInfo.Direction == PageDirection.Previous)
				{
					if (pagingInfo.SortBy.SortOrder == SortOrder.Ascending)
					{
						this.comparisionQueryFilter = new ComparisonFilter(ComparisonOperator.LessThan, pagingInfo.SortValue.SortColumn, pagingInfo.SortValue.SortColumnValue);
						return;
					}
					this.comparisionQueryFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, pagingInfo.SortValue.SortColumn, pagingInfo.SortValue.SortColumnValue);
				}
				return;
			}
			if (pagingInfo.SortBy.SortOrder == SortOrder.Ascending)
			{
				this.comparisionQueryFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, pagingInfo.SortValue.SortColumn, pagingInfo.SortValue.SortColumnValue);
				return;
			}
			this.comparisionQueryFilter = new ComparisonFilter(ComparisonOperator.LessThan, pagingInfo.SortValue.SortColumn, pagingInfo.SortValue.SortColumnValue);
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003CCE3 File Offset: 0x0003AEE3
		internal QueryFilter EqualsQueryFilter
		{
			get
			{
				return this.equalsQueryFilter;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003CCEB File Offset: 0x0003AEEB
		internal QueryFilter ComparisionQueryFilter
		{
			get
			{
				return this.comparisionQueryFilter;
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		public override bool Equals(object obj)
		{
			PaginationQueryFilter paginationQueryFilter = obj as PaginationQueryFilter;
			return paginationQueryFilter != null && this.equalsQueryFilter == paginationQueryFilter.equalsQueryFilter && this.comparisionQueryFilter == paginationQueryFilter.comparisionQueryFilter;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003CD2C File Offset: 0x0003AF2C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000993 RID: 2451
		private QueryFilter equalsQueryFilter;

		// Token: 0x04000994 RID: 2452
		private QueryFilter comparisionQueryFilter;
	}
}
