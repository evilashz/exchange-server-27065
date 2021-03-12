using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CCD RID: 3277
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PropertyDependency
	{
		// Token: 0x060071A8 RID: 29096 RVA: 0x001F7BC3 File Offset: 0x001F5DC3
		internal PropertyDependency(NativeStorePropertyDefinition property, PropertyDependencyType type)
		{
			this.Type = type;
			this.Property = property;
		}

		// Token: 0x04004F01 RID: 20225
		internal readonly NativeStorePropertyDefinition Property;

		// Token: 0x04004F02 RID: 20226
		internal readonly PropertyDependencyType Type;
	}
}
