using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200012A RID: 298
	internal class QueryHelper
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00031C30 File Offset: 0x0002FE30
		internal static SortBy GetSortBy(string sortString, PropertyDefinition[] allowedProperties)
		{
			TaskLogger.LogEnter();
			if (sortString == null)
			{
				TaskLogger.LogExit();
				return null;
			}
			PropertyDefinition columnDefinition = QueryHelper.MapPropertyName(sortString, allowedProperties, false);
			TaskLogger.LogExit();
			return new SortBy(columnDefinition, SortOrder.Ascending);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00031C64 File Offset: 0x0002FE64
		private static PropertyDefinition MapPropertyName(string propertyName, PropertyDefinition[] allowedProperties, bool forFilter)
		{
			foreach (PropertyDefinition propertyDefinition in allowedProperties)
			{
				if (string.Compare(propertyDefinition.Name, propertyName, true) == 0)
				{
					return propertyDefinition;
				}
			}
			if (forFilter)
			{
				throw new RecipientTaskException(Strings.ErrorInvalidFilterProperty(propertyName));
			}
			throw new RecipientTaskException(Strings.ErrorInvalidSortProperty(propertyName));
		}
	}
}
