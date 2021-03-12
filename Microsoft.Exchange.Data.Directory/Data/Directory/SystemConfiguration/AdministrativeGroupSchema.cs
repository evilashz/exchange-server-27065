using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200034E RID: 846
	internal sealed class AdministrativeGroupSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x040017DF RID: 6111
		public static readonly ADPropertyDefinition PublicFolderDatabase = SharedPropertyDefinitions.SitePublicFolderDatabase;

		// Token: 0x040017E0 RID: 6112
		public static readonly ADPropertyDefinition LegacyExchangeDN = SharedPropertyDefinitions.LegacyExchangeDN;

		// Token: 0x040017E1 RID: 6113
		public static readonly ADPropertyDefinition PublicFolderDefaultAdminAcl = SharedPropertyDefinitions.PublicFolderDefaultAdminAcl;

		// Token: 0x040017E2 RID: 6114
		public static readonly ADPropertyDefinition DisplayName = SharedPropertyDefinitions.MandatoryDisplayName;

		// Token: 0x040017E3 RID: 6115
		public static readonly ADPropertyDefinition AdminGroupMode = new ADPropertyDefinition("AdminGroupMode", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchAdminGroupMode", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017E4 RID: 6116
		public static readonly ADPropertyDefinition DefaultAdminGroup = new ADPropertyDefinition("DefaultAdminGroup", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchDefaultAdminGroup", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
