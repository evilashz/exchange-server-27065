using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A3 RID: 675
	internal class AccountPartitionSchema : ADObjectSchema
	{
		// Token: 0x040012B3 RID: 4787
		public static readonly ADPropertyDefinition ProvisioningFlags = SharedPropertyDefinitions.ProvisioningFlags;

		// Token: 0x040012B4 RID: 4788
		public static readonly ADPropertyDefinition TrustedDomainLink = new ADPropertyDefinition("ms-Exch-Trusted-Domain-Link", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchTrustedDomainLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040012B5 RID: 4789
		public static readonly ADPropertyDefinition IsLocalForest = new ADPropertyDefinition("IsLocalForest", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AccountPartitionSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(AccountPartition.IsLocalForestFilterBuilder), ADObject.FlagGetterDelegate(AccountPartitionSchema.ProvisioningFlags, 1), ADObject.FlagSetterDelegate(AccountPartitionSchema.ProvisioningFlags, 1), null, null);

		// Token: 0x040012B6 RID: 4790
		public static readonly ADPropertyDefinition PartitionId = new ADPropertyDefinition("PartitionId", ExchangeObjectVersion.Exchange2007, typeof(PartitionId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AccountPartitionSchema.ProvisioningFlags,
			AccountPartitionSchema.TrustedDomainLink,
			ADObjectSchema.Id,
			ADObjectSchema.ObjectState
		}, null, new GetterDelegate(AccountPartition.PartitionIdGetter), null, null, null);

		// Token: 0x040012B7 RID: 4791
		public static readonly ADPropertyDefinition EnabledForProvisioning = new ADPropertyDefinition("EnabledForProvisioning", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AccountPartitionSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(AccountPartitionSchema.ProvisioningFlags, 2), ADObject.FlagSetterDelegate(AccountPartitionSchema.ProvisioningFlags, 2), null, null);

		// Token: 0x040012B8 RID: 4792
		public static readonly ADPropertyDefinition IsSecondary = new ADPropertyDefinition("IsSecondary", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AccountPartitionSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(AccountPartition.IsSecondaryFilterBuilder), ADObject.FlagGetterDelegate(AccountPartitionSchema.ProvisioningFlags, 4), ADObject.FlagSetterDelegate(AccountPartitionSchema.ProvisioningFlags, 4), null, null);
	}
}
