using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects
{
	// Token: 0x02000095 RID: 149
	internal class CoreObjectPropertyDefinitionFactory : IPropertyDefinitionFactory
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x00028DC2 File Offset: 0x00026FC2
		public CoreObjectPropertyDefinitionFactory(StoreSession session, ICorePropertyBag propertyMappingReference)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(propertyMappingReference, "propertyMappingReference");
			this.session = session;
			this.propertyMappingReference = propertyMappingReference;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00028DEE File Offset: 0x00026FEE
		public bool TryGetPropertyDefinitionsFromPropertyTags(PropertyTag[] propertyTags, out NativeStorePropertyDefinition[] propertyDefinitions)
		{
			return MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(this.session, this.propertyMappingReference, propertyTags, out propertyDefinitions);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00028E03 File Offset: 0x00027003
		public bool TryGetPropertyDefinitionsFromPropertyTags(PropertyTag[] propertyTags, bool supportsCompatibleType, out NativeStorePropertyDefinition[] propertyDefinitions)
		{
			return MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(this.session, this.propertyMappingReference, propertyTags, supportsCompatibleType, out propertyDefinitions);
		}

		// Token: 0x04000277 RID: 631
		private readonly StoreSession session;

		// Token: 0x04000278 RID: 632
		private readonly ICorePropertyBag propertyMappingReference;
	}
}
