using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000662 RID: 1634
	internal sealed class ExchangeSettingsSchema : InternalExchangeSettingsSchema
	{
		// Token: 0x04003458 RID: 13400
		public static readonly ADPropertyDefinition History = XMLSerializableBase.ConfigXmlProperty<SettingsXml, XMLSerializableDictionary<string, SettingsHistory>>("SettingsHistory", ExchangeObjectVersion.Exchange2007, InternalExchangeSettingsSchema.ConfigurationXML, null, (SettingsXml configXml) => configXml.History.Value, null, null, null);
	}
}
