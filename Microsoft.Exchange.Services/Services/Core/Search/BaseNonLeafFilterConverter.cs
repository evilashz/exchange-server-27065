using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000256 RID: 598
	internal abstract class BaseNonLeafFilterConverter : BaseSingleFilterConverter
	{
		// Token: 0x06000F9C RID: 3996
		protected abstract SearchExpressionType CreateSearchExpression();

		// Token: 0x06000F9D RID: 3997
		protected abstract bool IsAcceptableChildCount(int childCount);

		// Token: 0x06000F9E RID: 3998
		protected abstract QueryFilter CreateNonLeafFilter(QueryFilter[] childFilters);

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0004CAD5 File Offset: 0x0004ACD5
		internal override bool IsLeafFilter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FA0 RID: 4000
		internal abstract int GetQueryFilterChildCount(QueryFilter queryFilter);

		// Token: 0x06000FA1 RID: 4001
		internal abstract QueryFilter GetQueryFilterChild(QueryFilter queryFilter, int childIndex);

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0004CAD8 File Offset: 0x0004ACD8
		internal QueryFilter ConvertToQueryFilter(Stack<QueryFilter> workingStack)
		{
			int num = 0;
			QueryFilter[] array = new QueryFilter[workingStack.Count];
			while (workingStack.Count > 0)
			{
				array[num++] = workingStack.Pop();
			}
			return this.CreateNonLeafFilter(array);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0004CB14 File Offset: 0x0004AD14
		internal SearchExpressionType ConvertToSearchExpresson(Stack<SearchExpressionType> workingStack)
		{
			if (!this.IsAcceptableChildCount(workingStack.Count))
			{
				ExTraceGlobals.SearchTracer.TraceError<int>((long)this.GetHashCode(), "[BaseNonLeafFilterConverter::ConvertToSearchExpresson] Incorrect child filter count: {0}", workingStack.Count);
				throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidRestriction);
			}
			SearchExpressionType searchExpressionType = this.CreateSearchExpression();
			List<SearchExpressionType> list = new List<SearchExpressionType>();
			while (workingStack.Count > 0)
			{
				SearchExpressionType item = workingStack.Pop();
				list.Add(item);
			}
			INonLeafSearchExpressionType nonLeafSearchExpressionType = searchExpressionType as INonLeafSearchExpressionType;
			if (nonLeafSearchExpressionType != null)
			{
				nonLeafSearchExpressionType.Items = list.ToArray();
			}
			return searchExpressionType;
		}
	}
}
