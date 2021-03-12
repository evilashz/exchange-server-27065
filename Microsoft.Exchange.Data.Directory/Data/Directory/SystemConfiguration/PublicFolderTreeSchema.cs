using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000547 RID: 1351
	internal sealed class PublicFolderTreeSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040028E8 RID: 10472
		public static readonly ADPropertyDefinition PublicFolderTreeType = new ADPropertyDefinition("PublicFolderTreeType", ExchangeObjectVersion.Exchange2003, typeof(PublicFolderTreeType), "msExchPFTreeType", ADPropertyDefinitionFlags.PersistDefaultValue, Microsoft.Exchange.Data.Directory.SystemConfiguration.PublicFolderTreeType.Mapi, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(PublicFolderTreeType))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028E9 RID: 10473
		public static readonly ADPropertyDefinition PublicFolderDefaultAdminAcl = SharedPropertyDefinitions.PublicFolderDefaultAdminAcl;

		// Token: 0x040028EA RID: 10474
		public static readonly ADPropertyDefinition PublicDatabases = new ADPropertyDefinition("PublicDatabases", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchOwningPFTreeBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
