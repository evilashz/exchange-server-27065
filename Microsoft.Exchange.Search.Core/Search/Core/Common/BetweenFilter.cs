using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000055 RID: 85
	internal class BetweenFilter : AndFilter
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000032F8 File Offset: 0x000014F8
		public BetweenFilter(ComparisonFilter leftFilter, ComparisonFilter rightFilter) : base(new QueryFilter[]
		{
			leftFilter,
			rightFilter
		})
		{
			if (leftFilter.ComparisonOperator != ComparisonOperator.GreaterThan && leftFilter.ComparisonOperator != ComparisonOperator.GreaterThanOrEqual)
			{
				throw new ExArgumentException(string.Format("Left comparison of BetweenFilter must be GreaterThan or GreaterThanOrEqual. Actual:{0}", leftFilter.ComparisonOperator));
			}
			if (rightFilter.ComparisonOperator != ComparisonOperator.LessThan && rightFilter.ComparisonOperator != ComparisonOperator.LessThanOrEqual)
			{
				throw new ExArgumentException(string.Format("Right comparison of BetweenFilter must be LessThan or LessThanOrEqual. Actual:{0}", leftFilter.ComparisonOperator));
			}
			if (rightFilter.Property != leftFilter.Property)
			{
				throw new ExArgumentException(string.Format("Left filter property {0} doesn't match right filter property {1}", leftFilter.Property.Name, rightFilter.Property.Name));
			}
			this.Property = rightFilter.Property;
			this.Left = leftFilter;
			this.Right = rightFilter;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000033C3 File Offset: 0x000015C3
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000033CB File Offset: 0x000015CB
		public PropertyDefinition Property { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000033D4 File Offset: 0x000015D4
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000033DC File Offset: 0x000015DC
		public ComparisonFilter Left { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000033E5 File Offset: 0x000015E5
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000033ED File Offset: 0x000015ED
		public ComparisonFilter Right { get; private set; }
	}
}
