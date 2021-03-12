using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000531 RID: 1329
	internal class OutlookProviderSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002813 RID: 10259
		public static readonly ADPropertyDefinition CertPrincipalName = new ADPropertyDefinition("CertPrincipalName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAutoDiscoverCertPrincipalName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002814 RID: 10260
		public static readonly ADPropertyDefinition Server = new ADPropertyDefinition("Server", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAutoDiscoverServer", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002815 RID: 10261
		public static readonly ADPropertyDefinition TTL = new ADPropertyDefinition("TTL", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchAutoDiscoverTTL", ADPropertyDefinitionFlags.PersistDefaultValue, 1, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, 65536)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002816 RID: 10262
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("Flags", ExchangeObjectVersion.Exchange2007, typeof(OutlookProviderFlags), "msExchAutoDiscoverFlags", ADPropertyDefinitionFlags.None, OutlookProviderFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002817 RID: 10263
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04002818 RID: 10264
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<OutlookProviderConfigXML>(OutlookProviderSchema.ConfigurationXMLRaw);

		// Token: 0x04002819 RID: 10265
		public static readonly ADPropertyDefinition RequiredClientVersions = XMLSerializableBase.ConfigXmlProperty<OutlookProviderConfigXML, ClientVersionCollection>("RequiredClientVersions", ExchangeObjectVersion.Exchange2003, OutlookProviderSchema.ConfigurationXML, null, (OutlookProviderConfigXML configXml) => configXml.RequiredClientVersions, delegate(OutlookProviderConfigXML configXml, ClientVersionCollection value)
		{
			configXml.RequiredClientVersions = value;
		}, null, null);
	}
}
