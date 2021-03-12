using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E4 RID: 740
	internal class MSERVEntrySchema : ObjectSchema
	{
		// Token: 0x04001591 RID: 5521
		public static readonly SimpleProviderPropertyDefinition ExternalDirectoryOrganizationId = new SimpleProviderPropertyDefinition("ExternalDirectoryOrganizationId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001592 RID: 5522
		public static readonly SimpleProviderPropertyDefinition DomainName = new SimpleProviderPropertyDefinition("DomainName", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001593 RID: 5523
		public static readonly SimpleProviderPropertyDefinition AddressForPartnerId = new SimpleProviderPropertyDefinition("AddressForPartnerId", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001594 RID: 5524
		public static readonly SimpleProviderPropertyDefinition PartnerId = new SimpleProviderPropertyDefinition("PartnerId", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001595 RID: 5525
		public static readonly SimpleProviderPropertyDefinition AddressForMinorPartnerId = new SimpleProviderPropertyDefinition("AddressForMinorPartnerId", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001596 RID: 5526
		public static readonly SimpleProviderPropertyDefinition MinorPartnerId = new SimpleProviderPropertyDefinition("MinorPartnerId", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001597 RID: 5527
		public static readonly SimpleProviderPropertyDefinition Forest = new SimpleProviderPropertyDefinition("Forest", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
