using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006AB RID: 1707
	internal sealed class IRMConfigurationSchema : ADContainerSchema
	{
		// Token: 0x040035D0 RID: 13776
		internal const int ServerCertificatesVersionShift = 24;

		// Token: 0x040035D1 RID: 13777
		internal const int ServerCertificatesVersionLength = 8;

		// Token: 0x040035D2 RID: 13778
		public static readonly ADPropertyDefinition ServiceLocation = new ADPropertyDefinition("ServiceLocation", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchRMSServiceLocationUrl", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040035D3 RID: 13779
		public static readonly ADPropertyDefinition PublishingLocation = new ADPropertyDefinition("PublishingLocation", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchRMSPublishingLocationUrl", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040035D4 RID: 13780
		public static readonly ADPropertyDefinition LicensingLocation = new ADPropertyDefinition("LicensingLocation", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchRMSLicensingLocationUrl", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040035D5 RID: 13781
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("ControlPointFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchControlPointFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 228, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040035D6 RID: 13782
		public static readonly ADPropertyDefinition JournalReportDecryptionEnabled = new ADPropertyDefinition("JournalReportDecryptionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 4), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 4), null, null);

		// Token: 0x040035D7 RID: 13783
		public static readonly ADPropertyDefinition TransportDecryptionOptional = new ADPropertyDefinition("TransportDecryptionOptional", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 128), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 128), null, null);

		// Token: 0x040035D8 RID: 13784
		public static readonly ADPropertyDefinition TransportDecryptionMandatory = new ADPropertyDefinition("TransportDecryptionMandatory", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 256), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 256), null, null);

		// Token: 0x040035D9 RID: 13785
		public static readonly ADPropertyDefinition ExternalLicensingEnabled = new ADPropertyDefinition("ExternalLicensingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 16), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 16), null, null);

		// Token: 0x040035DA RID: 13786
		public static readonly ADPropertyDefinition InternalLicensingEnabled = new ADPropertyDefinition("InternalLicensingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 512), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 512), null, null);

		// Token: 0x040035DB RID: 13787
		public static readonly ADPropertyDefinition SearchEnabled = new ADPropertyDefinition("SearchEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 32), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 32), null, null);

		// Token: 0x040035DC RID: 13788
		public static readonly ADPropertyDefinition ClientAccessServerEnabled = new ADPropertyDefinition("ClientAccessServerEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 64), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 64), null, null);

		// Token: 0x040035DD RID: 13789
		public static readonly ADPropertyDefinition InternetConfidentialEnabled = new ADPropertyDefinition("InternetConfidentialEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 1024), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 1024), null, null);

		// Token: 0x040035DE RID: 13790
		public static readonly ADPropertyDefinition EDiscoverySuperUserDisabled = new ADPropertyDefinition("EDiscoverySuperUserDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IRMConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(IRMConfigurationSchema.Flags, 2048), ADObject.FlagSetterDelegate(IRMConfigurationSchema.Flags, 2048), null, null);

		// Token: 0x040035DF RID: 13791
		public static readonly ADPropertyDefinition RMSOnlineKeySharingLocation = new ADPropertyDefinition("RMSOnlineKeySharingLocation", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchRMSOnlineKeySharingLocationUrl", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute),
			new UriSchemeConstraint(new List<string>
			{
				Uri.UriSchemeHttps
			})
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute),
			new UriSchemeConstraint(new List<string>
			{
				Uri.UriSchemeHttps
			})
		}, null, null);

		// Token: 0x040035E0 RID: 13792
		public static readonly ADPropertyDefinition ServerCertificatesVersion = ADObject.BitfieldProperty("ServerCertificatesVersion", 24, 8, IRMConfigurationSchema.Flags);

		// Token: 0x040035E1 RID: 13793
		public static readonly ADPropertyDefinition SharedServerBoxRacIdentity = new ADPropertyDefinition("SharedServerBoxRacIdentity", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSharedIdentityServerBoxRAC", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040035E2 RID: 13794
		public static readonly ADPropertyDefinition RMSOnlineVersion = new ADPropertyDefinition("RMSOnlineVersion", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchRMSOnlineCertificationLocationUrl", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
