using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	internal class MultivaluedInstanceComparisonFilter : ComparisonFilter
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00007B9C File Offset: 0x00005D9C
		public MultivaluedInstanceComparisonFilter(ComparisonOperator comparisonOperator, PropertyDefinition property, object propertyValue) : base(comparisonOperator, property, propertyValue)
		{
		}
	}
}
