using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000752 RID: 1874
	internal class RunspaceServerSettingsPresentationObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003E02 RID: 15874
		public static readonly SimpleProviderPropertyDefinition UserPreferredGlobalCatalog = new SimpleProviderPropertyDefinition("UserPreferredGlobalCatalog", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E03 RID: 15875
		public static readonly SimpleProviderPropertyDefinition DefaultGlobalCatalog = new SimpleProviderPropertyDefinition("DefaultGlobalCatalog", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E04 RID: 15876
		public static readonly SimpleProviderPropertyDefinition DefaultConfigurationDomainController = new SimpleProviderPropertyDefinition("DefaultConfigurationDomainController", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E05 RID: 15877
		public static readonly SimpleProviderPropertyDefinition DefaultPreferredDomainControllers = new SimpleProviderPropertyDefinition("DefaultPreferredDomainControllers", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E06 RID: 15878
		public static readonly SimpleProviderPropertyDefinition DefaultConfigurationDomainControllersForAllForests = new SimpleProviderPropertyDefinition("DefaultConfigurationDomainControllersForAllForests", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E07 RID: 15879
		public static readonly SimpleProviderPropertyDefinition DefaultGlobalCatalogsForAllForests = new SimpleProviderPropertyDefinition("DefaultGlobalCatalogsForAllForests", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E08 RID: 15880
		public static readonly SimpleProviderPropertyDefinition UserPreferredConfigurationDomainController = new SimpleProviderPropertyDefinition("UserPreferredConfigurationDomainController", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E09 RID: 15881
		public static readonly SimpleProviderPropertyDefinition UserPreferredDomainControllers = new SimpleProviderPropertyDefinition("UserPreferredDomainControllers", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E0A RID: 15882
		public static readonly SimpleProviderPropertyDefinition RecipientViewRoot = new SimpleProviderPropertyDefinition("RecipientViewRoot", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E0B RID: 15883
		public static readonly SimpleProviderPropertyDefinition ViewEntireForest = new SimpleProviderPropertyDefinition("ViewEntireForest", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E0C RID: 15884
		public static readonly SimpleProviderPropertyDefinition WriteOriginatingChangeTimestamp = new SimpleProviderPropertyDefinition("WriteOriginatingChangeTimestamp", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003E0D RID: 15885
		public static readonly SimpleProviderPropertyDefinition WriteShadowProperties = new SimpleProviderPropertyDefinition("WriteShadowProperties", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
