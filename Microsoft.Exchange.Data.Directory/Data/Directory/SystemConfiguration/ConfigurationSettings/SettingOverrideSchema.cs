using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000667 RID: 1639
	internal class SettingOverrideSchema : ADConfigurationObjectSchema
	{
		// Token: 0x0400346B RID: 13419
		public static readonly ADPropertyDefinition ConfigurationXmlRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x0400346C RID: 13420
		public static readonly ADPropertyDefinition ConfigurationXml = XMLSerializableBase.ConfigurationXmlProperty<SettingOverrideXml>(SettingOverrideSchema.ConfigurationXmlRaw);
	}
}
