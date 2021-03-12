using System;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000034 RID: 52
	public interface IPropertyDefinitionMap
	{
		// Token: 0x0600010A RID: 266
		bool TryGetPropertyDefinition(PropertyInfo propertyInfo, out PropertyDefinition propertyDefinition);
	}
}
