using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000520 RID: 1312
	internal sealed class OnPremisesOrganizationSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002798 RID: 10136
		public static readonly ADPropertyDefinition OrganizationGuid = new ADPropertyDefinition("OrganizationGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchOnPremisesOrganizationGuid", ADPropertyDefinitionFlags.WriteOnce | ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002799 RID: 10137
		public static readonly ADPropertyDefinition HybridDomains = new ADPropertyDefinition("HybridDomains", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchCoexistenceDomains", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400279A RID: 10138
		public static readonly ADPropertyDefinition InboundConnectorLink = new ADPropertyDefinition("InboundConnectorLink", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchOnPremisesInboundConnectorLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400279B RID: 10139
		public static readonly ADPropertyDefinition OutboundConnectorLink = new ADPropertyDefinition("OutboundConnectorLink", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchOnPremisesOutboundConnectorLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400279C RID: 10140
		public static readonly ADPropertyDefinition OrganizationRelationshipLink = new ADPropertyDefinition("OrganizationRelationshipLink", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchTrustedDomainLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400279D RID: 10141
		public static readonly ADPropertyDefinition OrganizationName = new ADPropertyDefinition("OrganizationName", ExchangeObjectVersion.Exchange2007, typeof(string), "AdminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);
	}
}
