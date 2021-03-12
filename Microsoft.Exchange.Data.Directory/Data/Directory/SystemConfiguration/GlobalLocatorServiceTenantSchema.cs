using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E2 RID: 738
	internal class GlobalLocatorServiceTenantSchema : ObjectSchema
	{
		// Token: 0x04001587 RID: 5511
		public static readonly SimpleProviderPropertyDefinition ExternalDirectoryOrganizationId = new SimpleProviderPropertyDefinition("ExternalDirectoryOrganizationId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001588 RID: 5512
		public static readonly ADPropertyDefinition DomainNames = new ADPropertyDefinition("DomainNames", ExchangeObjectVersion.Exchange2012, typeof(string), "domainNames", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001589 RID: 5513
		public static readonly SimpleProviderPropertyDefinition ResourceForest = new SimpleProviderPropertyDefinition("ResourceForest", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400158A RID: 5514
		public static readonly SimpleProviderPropertyDefinition AccountForest = new SimpleProviderPropertyDefinition("AccountForest", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400158B RID: 5515
		public static readonly SimpleProviderPropertyDefinition PrimarySite = new SimpleProviderPropertyDefinition("PrimarySite", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400158C RID: 5516
		public static readonly SimpleProviderPropertyDefinition SmtpNextHopDomain = new SimpleProviderPropertyDefinition("SmtpNextHopDomain", ExchangeObjectVersion.Exchange2012, typeof(SmtpDomain), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400158D RID: 5517
		public static readonly SimpleProviderPropertyDefinition TenantFlags = new SimpleProviderPropertyDefinition("TenantFlags", ExchangeObjectVersion.Exchange2012, typeof(GlsTenantFlags), PropertyDefinitionFlags.None, GlsTenantFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400158E RID: 5518
		public static readonly SimpleProviderPropertyDefinition TenantContainerCN = new SimpleProviderPropertyDefinition("TenantContainerCN", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400158F RID: 5519
		public static readonly SimpleProviderPropertyDefinition ResumeCache = new SimpleProviderPropertyDefinition("ResumeCache", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001590 RID: 5520
		public static readonly SimpleProviderPropertyDefinition IsOfflineData = new SimpleProviderPropertyDefinition("IsOfflineData", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
