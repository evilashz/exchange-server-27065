using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000661 RID: 1633
	internal class InternalExchangeSettingsSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003456 RID: 13398
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04003457 RID: 13399
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<SettingsXml>(InternalExchangeSettingsSchema.ConfigurationXMLRaw);
	}
}
