using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods
{
	// Token: 0x02000050 RID: 80
	public static class PropertyDefinitionExtensions
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00006795 File Offset: 0x00004995
		internal static ComparisonFilter AsBooleanComparisonQueryFilter(this PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition != null && propertyDefinition.Type == typeof(bool))
			{
				return new ComparisonFilter(ComparisonOperator.Equal, propertyDefinition, true);
			}
			return null;
		}
	}
}
