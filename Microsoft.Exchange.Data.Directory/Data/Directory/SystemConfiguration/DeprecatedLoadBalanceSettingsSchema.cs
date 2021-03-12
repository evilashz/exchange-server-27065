using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003EE RID: 1006
	[ObjectScope(ConfigScopes.Global)]
	internal class DeprecatedLoadBalanceSettingsSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001EFE RID: 7934
		public static readonly ADPropertyDefinition IncludedMailboxDatabases = new ADPropertyDefinition("IncludedMailboxDatabases", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchIncludedMailboxDatabases", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001EFF RID: 7935
		public static readonly ADPropertyDefinition UseIncludedMailboxDatabases = new ADPropertyDefinition("UseIncludedMailboxDatabases", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchUseIncludedMailboxDatabases", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F00 RID: 7936
		public static readonly ADPropertyDefinition ExcludedMailboxDatabases = new ADPropertyDefinition("ExcludedMailboxDatabases", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchExcludedMailboxDatabases", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F01 RID: 7937
		public static readonly ADPropertyDefinition UseExcludedMailboxDatabases = new ADPropertyDefinition("UseExcludedMailboxDatabases", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchUseExcludedMailboxDatabases", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
