using System;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000694 RID: 1684
	internal class PartnerApplicationSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003542 RID: 13634
		internal const int EnabledBitShift = 0;

		// Token: 0x04003543 RID: 13635
		internal const int UseAuthServerBitShift = 1;

		// Token: 0x04003544 RID: 13636
		internal const int AcceptSecurityIdentifierInformationShift = 2;

		// Token: 0x04003545 RID: 13637
		public static readonly ADPropertyDefinition ApplicationIdentifier = new ADPropertyDefinition("ApplicationIdentifier", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAuthApplicationIdentifier", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003546 RID: 13638
		public static readonly ADPropertyDefinition AuthMetadataUrl = new ADPropertyDefinition("AuthMetadataUrl", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAuthMetadataUrl", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003547 RID: 13639
		public static readonly ADPropertyDefinition Realm = new ADPropertyDefinition("Realm", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAuthRealm", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003548 RID: 13640
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("Flags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchAuthFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003549 RID: 13641
		public static readonly ADPropertyDefinition CertificateDataRaw = new ADPropertyDefinition("CertificateDataRaw", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchAuthCertificateData", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400354A RID: 13642
		public static readonly ADPropertyDefinition CertificateDataString = new ADPropertyDefinition("CertificateDataString", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PartnerApplicationSchema.CertificateDataRaw
		}, null, delegate(IPropertyBag propertyBag)
		{
			MultiValuedProperty<byte[]> source = (MultiValuedProperty<byte[]>)propertyBag[PartnerApplicationSchema.CertificateDataRaw];
			return new MultiValuedProperty<string>((from d in source
			select Convert.ToBase64String(d)).ToArray<string>());
		}, null, null, null);

		// Token: 0x0400354B RID: 13643
		public static readonly ADPropertyDefinition UseAuthServer = ADObject.BitfieldProperty("UseAuthServer", 1, PartnerApplicationSchema.Flags);

		// Token: 0x0400354C RID: 13644
		public static readonly ADPropertyDefinition Enabled = ADObject.BitfieldProperty("Enabled", 0, PartnerApplicationSchema.Flags);

		// Token: 0x0400354D RID: 13645
		public static readonly ADPropertyDefinition AcceptSecurityIdentifierInformation = ADObject.BitfieldProperty("AcceptSecurityIdentifierInformation", 2, PartnerApplicationSchema.Flags);

		// Token: 0x0400354E RID: 13646
		public static readonly ADPropertyDefinition ThrottlingPolicy = new ADPropertyDefinition("ThrottlingPolicy", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchThrottlingPolicyDN", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400354F RID: 13647
		public static readonly ADPropertyDefinition LinkedAccount = new ADPropertyDefinition("LinkedAccount", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchAuthLinkedAccount", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003550 RID: 13648
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04003551 RID: 13649
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<PartnerApplicationConfigXML>(PartnerApplicationSchema.ConfigurationXMLRaw);

		// Token: 0x04003552 RID: 13650
		public static readonly ADPropertyDefinition IssuerIdentifier = XMLSerializableBase.ConfigXmlProperty<PartnerApplicationConfigXML, string>("IssuerIdentifier", ExchangeObjectVersion.Exchange2010, PartnerApplicationSchema.ConfigurationXML, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, (PartnerApplicationConfigXML configXml) => configXml.IssuerIdentifier, delegate(PartnerApplicationConfigXML configXml, string value)
		{
			configXml.IssuerIdentifier = value;
		}, null, null);

		// Token: 0x04003553 RID: 13651
		public static readonly ADPropertyDefinition ActAsPermissions = XMLSerializableBase.ConfigXmlProperty<PartnerApplicationConfigXML, string[]>("ActAsPermissions", ExchangeObjectVersion.Exchange2010, PartnerApplicationSchema.ConfigurationXML, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, (PartnerApplicationConfigXML configXml) => configXml.ActAsPermissions, delegate(PartnerApplicationConfigXML configXml, string[] value)
		{
			configXml.ActAsPermissions = value;
		}, null, null);

		// Token: 0x04003554 RID: 13652
		public static readonly ADPropertyDefinition AppOnlyPermissions = XMLSerializableBase.ConfigXmlProperty<PartnerApplicationConfigXML, string[]>("AppOnlyPermissions", ExchangeObjectVersion.Exchange2010, PartnerApplicationSchema.ConfigurationXML, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, (PartnerApplicationConfigXML configXml) => configXml.AppOnlyPermissions, delegate(PartnerApplicationConfigXML configXml, string[] value)
		{
			configXml.AppOnlyPermissions = value;
		}, null, null);
	}
}
