using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200048F RID: 1167
	internal class IntraOrganizationConnectorSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002404 RID: 9220
		public static readonly ADPropertyDefinition TargetAddressDomains = OrganizationRelationshipSchema.DomainNames;

		// Token: 0x04002405 RID: 9221
		public static readonly ADPropertyDefinition DiscoveryEndpoint = OrganizationRelationshipSchema.TargetAutodiscoverEpr;

		// Token: 0x04002406 RID: 9222
		public static readonly ADPropertyDefinition Enabled = OrganizationRelationshipSchema.Enabled;
	}
}
