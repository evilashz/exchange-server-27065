using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006B4 RID: 1716
	internal class TenantOutboundConnectorSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003618 RID: 13848
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchSmtpSendEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003619 RID: 13849
		public static readonly ADPropertyDefinition SmartHostTypeFlag = new ADPropertyDefinition("SmartHostType", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpSmartHostType", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400361A RID: 13850
		public static readonly ADPropertyDefinition Comment = new ADPropertyDefinition("Comment", ExchangeObjectVersion.Exchange2007, typeof(string), "AdminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x0400361B RID: 13851
		public static readonly ADPropertyDefinition UseMxRecord = new ADPropertyDefinition("UseMx", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.SmartHostTypeFlag
		}, null, ADObject.FlagGetterDelegate(TenantOutboundConnectorSchema.SmartHostTypeFlag, 1), ADObject.FlagSetterDelegate(TenantOutboundConnectorSchema.SmartHostTypeFlag, 1), null, null);

		// Token: 0x0400361C RID: 13852
		public static readonly ADPropertyDefinition ConnectorType = new ADPropertyDefinition("ConnectorType", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpSendType", ADPropertyDefinitionFlags.PersistDefaultValue, 2, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400361D RID: 13853
		public static readonly ADPropertyDefinition ConnectorSourceFlags = new ADPropertyDefinition("TenantConnectorSourceFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportOutboundSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400361E RID: 13854
		internal static readonly ADPropertyDefinition RecipientDomains = new ADPropertyDefinition("RecipientDomains", ExchangeObjectVersion.Exchange2003, typeof(string), "routingList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400361F RID: 13855
		public static readonly ADPropertyDefinition RecipientDomainsEx = new ADPropertyDefinition("RecipientDomainsEx", ExchangeObjectVersion.Exchange2003, typeof(SmtpDomainWithSubdomains), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.RecipientDomains
		}, null, new GetterDelegate(TenantOutboundConnector.RecipientDomainsGetter), new SetterDelegate(TenantOutboundConnector.RecipientDomainsSetter), null, null);

		// Token: 0x04003620 RID: 13856
		public static readonly ADPropertyDefinition SmartHostsString = new ADPropertyDefinition("SmartHostsString", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchSmtpSmartHost", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003621 RID: 13857
		public static readonly ADPropertyDefinition SmartHosts = new ADPropertyDefinition("SmartHosts", ExchangeObjectVersion.Exchange2003, typeof(SmartHost), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.SmartHostsString
		}, null, new GetterDelegate(TenantOutboundConnector.SmartHostsGetter), new SetterDelegate(TenantOutboundConnector.SmartHostsSetter), null, null);

		// Token: 0x04003622 RID: 13858
		public static readonly ADPropertyDefinition OutboundConnectorFlags = new ADPropertyDefinition("SendConnectorFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpSendFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003623 RID: 13859
		public static readonly ADPropertyDefinition TlsSettings = new ADPropertyDefinition("TlsSettings", ExchangeObjectVersion.Exchange2007, typeof(TlsAuthLevel?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.OutboundConnectorFlags
		}, null, new GetterDelegate(TenantOutboundConnector.TlsAuthLevelGetter), new SetterDelegate(TenantOutboundConnector.TlsAuthLevelSetter), null, null);

		// Token: 0x04003624 RID: 13860
		public static readonly ADPropertyDefinition TlsDomain = new ADPropertyDefinition("TlsDomain", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomainWithSubdomains), "msExchSmtpSendTlsDomain", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new DisallowStarSmtpDomainWithSubdomainsConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003625 RID: 13861
		public static readonly ADPropertyDefinition IsTransportRuleScoped = new ADPropertyDefinition("IsTransportRuleScoped", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.OutboundConnectorFlags
		}, null, ADObject.FlagGetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 4096), ADObject.FlagSetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 4096), null, null);

		// Token: 0x04003626 RID: 13862
		public static readonly ADPropertyDefinition OnPremisesOrganizationBackLink = new ADPropertyDefinition("OnPremisesOrganizationBackLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchOnPremisesOutboundConnectorBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003627 RID: 13863
		public static readonly ADPropertyDefinition RouteAllMessagesViaOnPremises = new ADPropertyDefinition("RouteAllMessagesViaOnPremises", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.OutboundConnectorFlags
		}, null, ADObject.FlagGetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 1), ADObject.FlagSetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 1), null, null);

		// Token: 0x04003628 RID: 13864
		public static readonly ADPropertyDefinition CloudServicesMailEnabled = new ADPropertyDefinition("CloudServicesMailEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.OutboundConnectorFlags
		}, null, ADObject.FlagGetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 8192), ADObject.FlagSetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 8192), null, null);

		// Token: 0x04003629 RID: 13865
		public static readonly ADPropertyDefinition AllAcceptedDomains = new ADPropertyDefinition("AllAcceptedDomains", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantOutboundConnectorSchema.OutboundConnectorFlags
		}, null, ADObject.FlagGetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 16384), ADObject.FlagSetterDelegate(TenantOutboundConnectorSchema.OutboundConnectorFlags, 16384), null, null);
	}
}
