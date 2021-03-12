using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects
{
	// Token: 0x02000094 RID: 148
	internal interface IPropertyDefinitionFactory
	{
		// Token: 0x060005F0 RID: 1520
		bool TryGetPropertyDefinitionsFromPropertyTags(PropertyTag[] propertyTags, out NativeStorePropertyDefinition[] propertyDefinitions);

		// Token: 0x060005F1 RID: 1521
		bool TryGetPropertyDefinitionsFromPropertyTags(PropertyTag[] propertyTags, bool supportsCompatibleType, out NativeStorePropertyDefinition[] propertyDefinitions);
	}
}
