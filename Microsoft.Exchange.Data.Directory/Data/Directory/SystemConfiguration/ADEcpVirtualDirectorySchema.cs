using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000364 RID: 868
	internal sealed class ADEcpVirtualDirectorySchema : ExchangeWebAppVirtualDirectorySchema
	{
		// Token: 0x04001861 RID: 6241
		public static readonly ADPropertyDefinition ADFeatureSet = new ADPropertyDefinition("ADFeatureSet", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchManagementSettings", ADPropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001862 RID: 6242
		public static readonly ADPropertyDefinition AdminEnabled = new ADPropertyDefinition("AdminEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADEcpVirtualDirectorySchema.ADFeatureSet
		}, null, ADObject.FlagGetterDelegate(1, ADEcpVirtualDirectorySchema.ADFeatureSet), ADObject.FlagSetterDelegate(1, ADEcpVirtualDirectorySchema.ADFeatureSet), null, null);

		// Token: 0x04001863 RID: 6243
		public static readonly ADPropertyDefinition OwaOptionsEnabled = new ADPropertyDefinition("OwaOptionsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADEcpVirtualDirectorySchema.ADFeatureSet
		}, null, ADObject.FlagGetterDelegate(2, ADEcpVirtualDirectorySchema.ADFeatureSet), ADObject.FlagSetterDelegate(2, ADEcpVirtualDirectorySchema.ADFeatureSet), null, null);
	}
}
