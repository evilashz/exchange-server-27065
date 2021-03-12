using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000010 RID: 16
	public interface IReadOnlyPropertyBag
	{
		// Token: 0x1700000D RID: 13
		object this[PropertyDefinition propertyDefinition]
		{
			get;
		}

		// Token: 0x06000054 RID: 84
		object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray);
	}
}
