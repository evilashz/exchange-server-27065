using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200084C RID: 2124
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPropertyBagFactory
	{
		// Token: 0x06004F23 RID: 20259
		PersistablePropertyBag CreateStorePropertyBag(PropertyBag propertyBag, ICollection<PropertyDefinition> prefetchPropertyArray);
	}
}
