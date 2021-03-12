using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002DE RID: 734
	internal class GlobalLocatorServiceDomainSchema : ObjectSchema
	{
		// Token: 0x0400157D RID: 5501
		public static readonly SimpleProviderPropertyDefinition ExternalDirectoryOrganizationId = new SimpleProviderPropertyDefinition("ExternalDirectoryOrganizationId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400157E RID: 5502
		public static readonly SimpleProviderPropertyDefinition DomainName = new SimpleProviderPropertyDefinition("DomainName", ExchangeObjectVersion.Exchange2012, typeof(SmtpDomain), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400157F RID: 5503
		public static readonly SimpleProviderPropertyDefinition DomainFlags = new SimpleProviderPropertyDefinition("DomainFlags", ExchangeObjectVersion.Exchange2012, typeof(GlsDomainFlags?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001580 RID: 5504
		public static readonly SimpleProviderPropertyDefinition DomainInUse = new SimpleProviderPropertyDefinition("DomainInUse", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
