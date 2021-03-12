using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002BB RID: 699
	internal class DetailsTemplateSchema : ADConfigurationObjectSchema
	{
		// Token: 0x0400133B RID: 4923
		public static readonly ADPropertyDefinition TemplateBlob = new ADPropertyDefinition("TemplateBlob", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "addressEntryDisplayTable", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400133C RID: 4924
		public static readonly ADPropertyDefinition TemplateBlobOriginal = new ADPropertyDefinition("TemplateBlobOriginal", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "originalDisplayTable", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400133D RID: 4925
		public static readonly ADPropertyDefinition HelpFileName = new ADPropertyDefinition("HelpFileName", ExchangeObjectVersion.Exchange2003, typeof(string), "helpFileName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400133E RID: 4926
		public static readonly ADPropertyDefinition HelpData32 = new ADPropertyDefinition("HelpData32", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "helpData32", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400133F RID: 4927
		public static readonly ADPropertyDefinition Pages = new ADPropertyDefinition("Pages", ExchangeObjectVersion.Exchange2003, typeof(Page), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001340 RID: 4928
		public static readonly ADPropertyDefinition ExchangeLegacyDN = SharedPropertyDefinitions.ExchangeLegacyDN;
	}
}
