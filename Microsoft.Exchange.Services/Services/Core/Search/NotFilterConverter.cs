using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000272 RID: 626
	internal class NotFilterConverter : BaseNonLeafFilterConverter
	{
		// Token: 0x06001059 RID: 4185 RVA: 0x0004EEE9 File Offset: 0x0004D0E9
		protected override QueryFilter CreateNonLeafFilter(QueryFilter[] childFilters)
		{
			return new NotFilter(childFilters[0]);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0004EEF3 File Offset: 0x0004D0F3
		protected override bool IsAcceptableChildCount(int childCount)
		{
			return childCount == 1;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004EEF9 File Offset: 0x0004D0F9
		protected override SearchExpressionType CreateSearchExpression()
		{
			return new NotType();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0004EF00 File Offset: 0x0004D100
		internal override int GetQueryFilterChildCount(QueryFilter queryFilter)
		{
			return 1;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0004EF04 File Offset: 0x0004D104
		internal override QueryFilter GetQueryFilterChild(QueryFilter queryFilter, int childIndex)
		{
			NotFilter notFilter = queryFilter as NotFilter;
			if (childIndex == 0)
			{
				return notFilter.Filter;
			}
			return null;
		}
	}
}
