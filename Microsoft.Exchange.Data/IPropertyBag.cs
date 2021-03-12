using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000011 RID: 17
	public interface IPropertyBag : IReadOnlyPropertyBag
	{
		// Token: 0x1700000E RID: 14
		object this[PropertyDefinition propertyDefinition]
		{
			get;
			set;
		}

		// Token: 0x06000057 RID: 87
		void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray);
	}
}
