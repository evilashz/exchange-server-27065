using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003FF RID: 1023
	internal class EdgeDomainContentConfigSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04001F4D RID: 8013
		public static readonly ADPropertyDefinition DomainName = new ADPropertyDefinition("DomainName", ExchangeObjectVersion.Exchange2003, typeof(SmtpDomainWithSubdomains), "domainName", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F4E RID: 8014
		internal static readonly ADPropertyDefinition ADNonMimeCharacterSet = new ADPropertyDefinition("ADNonMimeCharacterSet", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchNonMIMECharacterSet", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F4F RID: 8015
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("RemoteDomainFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchDomainContentConfigFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F50 RID: 8016
		public static readonly ADPropertyDefinition NonMimeCharacterSet = new ADPropertyDefinition("NonMimeCharacterSet", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EdgeDomainContentConfigSchema.ADNonMimeCharacterSet
		}, null, new GetterDelegate(DomainContentConfig.NonMimeCharacterSetGetter), new SetterDelegate(DomainContentConfig.NonMimeCharacterSetSetter), null, null);

		// Token: 0x04001F51 RID: 8017
		public static readonly ADPropertyDefinition TrustedMailOutboundEnabled = new ADPropertyDefinition("TrustedMailOutboundEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EdgeDomainContentConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(EdgeDomainContentConfigSchema.Flags, 1), ADObject.FlagSetterDelegate(EdgeDomainContentConfigSchema.Flags, 1), null, null);

		// Token: 0x04001F52 RID: 8018
		public static readonly ADPropertyDefinition TrustedMailInboundEnabled = new ADPropertyDefinition("TrustedMailInboundEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EdgeDomainContentConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(EdgeDomainContentConfigSchema.Flags, 2), ADObject.FlagSetterDelegate(EdgeDomainContentConfigSchema.Flags, 2), null, null);
	}
}
