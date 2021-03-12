using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods
{
	// Token: 0x02000051 RID: 81
	public static class PropertyValueExtensions
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000067C0 File Offset: 0x000049C0
		internal static QueryFilter AsBooleanQueryFilter(this object propertyValue)
		{
			if (!(propertyValue is bool))
			{
				return null;
			}
			if ((bool)propertyValue)
			{
				return new TrueFilter();
			}
			return new FalseFilter();
		}
	}
}
