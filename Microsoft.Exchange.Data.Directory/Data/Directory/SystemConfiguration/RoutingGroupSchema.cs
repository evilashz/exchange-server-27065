using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200056A RID: 1386
	internal class RoutingGroupSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002A1E RID: 10782
		public static readonly ADPropertyDefinition RoutingMasterDN = new ADPropertyDefinition("RoutingMasterDN", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchRoutingMasterDN", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A1F RID: 10783
		public static readonly ADPropertyDefinition RoutingGroupMembersDN = new ADPropertyDefinition("RoutingGroupMembersDN", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchRoutingGroupMembersBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
