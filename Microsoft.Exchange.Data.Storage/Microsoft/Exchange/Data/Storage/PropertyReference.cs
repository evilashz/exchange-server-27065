using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AAD RID: 2733
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PropertyReference
	{
		// Token: 0x06006390 RID: 25488 RVA: 0x001A3C90 File Offset: 0x001A1E90
		internal PropertyReference(NativeStorePropertyDefinition usedProperty, PropertyAccess type)
		{
			this.AccessType = type;
			this.Property = usedProperty;
		}

		// Token: 0x04003843 RID: 14403
		internal readonly NativeStorePropertyDefinition Property;

		// Token: 0x04003844 RID: 14404
		internal readonly PropertyAccess AccessType;
	}
}
