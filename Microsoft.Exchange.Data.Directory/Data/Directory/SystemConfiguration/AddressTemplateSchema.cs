using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000343 RID: 835
	internal class AddressTemplateSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040017B3 RID: 6067
		public static readonly ADPropertyDefinition DisplayName = SharedPropertyDefinitions.MandatoryDisplayName;

		// Token: 0x040017B4 RID: 6068
		public static readonly ADPropertyDefinition AddressSyntax = new ADPropertyDefinition("AddressSyntax", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "addressSyntax", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 4096)
		}, null, null);

		// Token: 0x040017B5 RID: 6069
		public static readonly ADPropertyDefinition AddressType = new ADPropertyDefinition("AddressType", ExchangeObjectVersion.Exchange2003, typeof(string), "addressType", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017B6 RID: 6070
		public static readonly ADPropertyDefinition PerMsgDialogDisplayTable = new ADPropertyDefinition("PerMsgDialogDisplayTable", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "perMsgDialogDisplayTable", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 32768)
		}, null, null);

		// Token: 0x040017B7 RID: 6071
		public static readonly ADPropertyDefinition PerRecipDialogDisplayTable = new ADPropertyDefinition("PerRecipDialogDisplayTable", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "perRecipDialogDisplayTable", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 32768)
		}, null, null);

		// Token: 0x040017B8 RID: 6072
		public static readonly ADPropertyDefinition ProxyGenerationEnabled = new ADPropertyDefinition("ProxyGenerationEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "proxyGenerationEnabled", ADPropertyDefinitionFlags.Binary, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017B9 RID: 6073
		public static readonly ADPropertyDefinition TemplateBlob = DetailsTemplateSchema.TemplateBlob;

		// Token: 0x040017BA RID: 6074
		public static readonly ADPropertyDefinition ExchangeLegacyDN = SharedPropertyDefinitions.ExchangeLegacyDN;
	}
}
