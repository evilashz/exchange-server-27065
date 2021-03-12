using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000273 RID: 627
	internal class OrFilterConverter : BaseNonLeafFilterConverter
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x0004EF2B File Offset: 0x0004D12B
		protected override QueryFilter CreateNonLeafFilter(QueryFilter[] childFilters)
		{
			if (!this.IsAcceptableChildCount(childFilters.Length))
			{
				ExTraceGlobals.SearchTracer.TraceError<int>((long)this.GetHashCode(), "[OrFilterConverter::CreateNonLeafFilter] Expected one or more child filters but found {0}", childFilters.Length);
				throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidRestriction);
			}
			return new OrFilter(childFilters);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004EF67 File Offset: 0x0004D167
		protected override bool IsAcceptableChildCount(int childCount)
		{
			return childCount > 0;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004EF6D File Offset: 0x0004D16D
		protected override SearchExpressionType CreateSearchExpression()
		{
			return new OrType();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004EF74 File Offset: 0x0004D174
		internal override int GetQueryFilterChildCount(QueryFilter queryFilter)
		{
			OrFilter orFilter = queryFilter as OrFilter;
			return orFilter.FilterCount;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004EF90 File Offset: 0x0004D190
		internal override QueryFilter GetQueryFilterChild(QueryFilter queryFilter, int childIndex)
		{
			OrFilter orFilter = queryFilter as OrFilter;
			if (childIndex < orFilter.FilterCount)
			{
				return orFilter.Filters[childIndex];
			}
			return null;
		}
	}
}
