using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000257 RID: 599
	internal class AndFilterConverter : BaseNonLeafFilterConverter
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0004CBA0 File Offset: 0x0004ADA0
		protected override QueryFilter CreateNonLeafFilter(QueryFilter[] childFilters)
		{
			if (!this.IsAcceptableChildCount(childFilters.Length))
			{
				ExTraceGlobals.SearchTracer.TraceError<int>((long)this.GetHashCode(), "[AndFilterConverter::CreateNonLeafFilter] Expected one or more child filters but found {0}", childFilters.Length);
				throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidRestriction);
			}
			return new AndFilter(childFilters);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004CBDC File Offset: 0x0004ADDC
		protected override bool IsAcceptableChildCount(int childCount)
		{
			return childCount > 0;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004CBE2 File Offset: 0x0004ADE2
		protected override SearchExpressionType CreateSearchExpression()
		{
			return new AndType();
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004CBEC File Offset: 0x0004ADEC
		internal override int GetQueryFilterChildCount(QueryFilter queryFilter)
		{
			AndFilter andFilter = queryFilter as AndFilter;
			return andFilter.FilterCount;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004CC08 File Offset: 0x0004AE08
		internal override QueryFilter GetQueryFilterChild(QueryFilter queryFilter, int childIndex)
		{
			AndFilter andFilter = queryFilter as AndFilter;
			if (childIndex < andFilter.FilterCount)
			{
				return andFilter.Filters[childIndex];
			}
			return null;
		}
	}
}
