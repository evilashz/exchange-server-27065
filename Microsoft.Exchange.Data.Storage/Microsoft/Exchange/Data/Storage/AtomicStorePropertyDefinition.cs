using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A8 RID: 168
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class AtomicStorePropertyDefinition : StorePropertyDefinition
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x0005143E File Offset: 0x0004F63E
		protected AtomicStorePropertyDefinition(PropertyTypeSpecifier propertyTypeSpecifier, string displayName, Type type, PropertyFlags childFlags, PropertyDefinitionConstraint[] constraints) : base(propertyTypeSpecifier, displayName, type, childFlags, constraints)
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0005144D File Offset: 0x0004F64D
		protected override bool InternalIsDirty(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.IsDirty(this);
		}
	}
}
