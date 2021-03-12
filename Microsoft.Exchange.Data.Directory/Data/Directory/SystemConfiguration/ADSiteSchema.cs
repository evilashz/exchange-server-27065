using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200038C RID: 908
	internal class ADSiteSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001969 RID: 6505
		internal static readonly ADPropertyDefinition ADSiteFlags = new ADPropertyDefinition("ADSiteFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportSiteFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400196A RID: 6506
		public static readonly ADPropertyDefinition HubSiteEnabled = ADObject.BitfieldProperty("HubSiteEnabled", 0, ADSiteSchema.ADSiteFlags);

		// Token: 0x0400196B RID: 6507
		public static readonly ADPropertyDefinition InboundMailDisabled = ADObject.BitfieldProperty("InboundMailDisabled", 1, ADSiteSchema.ADSiteFlags);

		// Token: 0x0400196C RID: 6508
		public static readonly ADPropertyDefinition PartnerId = new ADPropertyDefinition("PartnerId", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPartnerId", ADPropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400196D RID: 6509
		public static readonly ADPropertyDefinition MinorPartnerId = new ADPropertyDefinition("MinorPartnerId", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMinorPartnerId", ADPropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400196E RID: 6510
		internal static readonly ADPropertyDefinition ResponsibleForSites = new ADPropertyDefinition("ResponsibleForSites", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchResponsibleForSites", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
