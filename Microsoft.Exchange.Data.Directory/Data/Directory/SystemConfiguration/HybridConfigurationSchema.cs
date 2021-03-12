using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002B7 RID: 695
	internal sealed class HybridConfigurationSchema : ADConfigurationObjectSchema
	{
		// Token: 0x0400131B RID: 4891
		public static readonly ADPropertyDefinition ClientAccessServers = new ADPropertyDefinition("ClientAccessServers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchCoexistenceServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400131C RID: 4892
		public static readonly ADPropertyDefinition SendingTransportServers = new ADPropertyDefinition("SendingTransportServers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchCoexistenceTransportServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400131D RID: 4893
		public static readonly ADPropertyDefinition ReceivingTransportServers = new ADPropertyDefinition("ReceivingTransportServers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchCoexistenceFrontendTransportServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400131E RID: 4894
		public static readonly ADPropertyDefinition EdgeTransportServers = new ADPropertyDefinition("EdgeTransportServers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchCoexistenceEdgeTransportServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400131F RID: 4895
		public static readonly ADPropertyDefinition TlsCertificateName = new ADPropertyDefinition("TlsCertificateName", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchCoexistenceSecureMailCertificateThumbprint", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001320 RID: 4896
		public static readonly ADPropertyDefinition OnPremisesSmartHost = new ADPropertyDefinition("OnPremisesSmartHost", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), "msExchCoexistenceOnPremisesSmartHost", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001321 RID: 4897
		public static readonly ADPropertyDefinition Domains = new ADPropertyDefinition("Domains", ExchangeObjectVersion.Exchange2010, typeof(AutoDiscoverSmtpDomain), "msExchCoexistenceDomains", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001322 RID: 4898
		public static readonly ADPropertyDefinition ExternalIPAddresses = new ADPropertyDefinition("ExternalIPAddresses", ExchangeObjectVersion.Exchange2010, typeof(IPRange), "msExchCoexistenceExternalIPAddresses", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001323 RID: 4899
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("CoexistenceFeatureFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchCoexistenceFeatureFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001324 RID: 4900
		public static readonly ADPropertyDefinition FreeBusySharingEnabled = new ADPropertyDefinition("FreeBusySharingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 1), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 1), null, null);

		// Token: 0x04001325 RID: 4901
		public static readonly ADPropertyDefinition MoveMailboxEnabled = new ADPropertyDefinition("MoveMailboxEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 2), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 2), null, null);

		// Token: 0x04001326 RID: 4902
		public static readonly ADPropertyDefinition MailtipsEnabled = new ADPropertyDefinition("MailtipsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 4), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 4), null, null);

		// Token: 0x04001327 RID: 4903
		public static readonly ADPropertyDefinition MessageTrackingEnabled = new ADPropertyDefinition("MessageTrackingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 8), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 8), null, null);

		// Token: 0x04001328 RID: 4904
		public static readonly ADPropertyDefinition OwaRedirectionEnabled = new ADPropertyDefinition("OwaRedirectionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 16), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 16), null, null);

		// Token: 0x04001329 RID: 4905
		public static readonly ADPropertyDefinition OnlineArchiveEnabled = new ADPropertyDefinition("OnlineArchiveEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 32), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 32), null, null);

		// Token: 0x0400132A RID: 4906
		public static readonly ADPropertyDefinition SecureMailEnabled = new ADPropertyDefinition("SecureMailEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 64), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 64), null, null);

		// Token: 0x0400132B RID: 4907
		public static readonly ADPropertyDefinition CentralizedTransportOnPremEnabled = new ADPropertyDefinition("CentralizedTransportOnPremEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 128), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 128), null, null);

		// Token: 0x0400132C RID: 4908
		public static readonly ADPropertyDefinition CentralizedTransportInCloudEnabled = new ADPropertyDefinition("CentralizedTransportInCloudEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 256), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 256), null, null);

		// Token: 0x0400132D RID: 4909
		public static readonly ADPropertyDefinition Features = new ADPropertyDefinition("Features", ExchangeObjectVersion.Exchange2010, typeof(HybridFeature), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, new GetterDelegate(HybridConfiguration.FeaturesGetter), new SetterDelegate(HybridConfiguration.FeaturesSetter), null, null);

		// Token: 0x0400132E RID: 4910
		public static readonly ADPropertyDefinition PhotosEnabled = new ADPropertyDefinition("PhotosEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HybridConfigurationSchema.Flags
		}, null, ADObject.FlagGetterDelegate(HybridConfigurationSchema.Flags, 512), ADObject.FlagSetterDelegate(HybridConfigurationSchema.Flags, 512), null, null);

		// Token: 0x0400132F RID: 4911
		public static readonly ADPropertyDefinition ServiceInstanceFlags = new ADPropertyDefinition("ServiceInstanceFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchManagementSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001330 RID: 4912
		public static readonly ADPropertyDefinition ServiceInstance = ADObject.BitfieldProperty("ServiceInstance", 0, 5, HybridConfigurationSchema.ServiceInstanceFlags);
	}
}
