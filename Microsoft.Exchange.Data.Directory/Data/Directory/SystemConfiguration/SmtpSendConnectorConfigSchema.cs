using System;
using System.Management.Automation;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B9 RID: 1465
	internal class SmtpSendConnectorConfigSchema : MailGatewaySchema
	{
		// Token: 0x0600435D RID: 17245 RVA: 0x000FCCDB File Offset: 0x000FAEDB
		internal static GetterDelegate AdvertisedDomainGetterDelegate()
		{
			return (IPropertyBag bag) => (SmtpDomain)bag[SmtpSendConnectorConfigSchema.Fqdn];
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x000FCD27 File Offset: 0x000FAF27
		internal static SetterDelegate AdvertisedDomainSetterDelegate()
		{
			return delegate(object value, IPropertyBag bag)
			{
				if (value == null)
				{
					bag[SmtpSendConnectorConfigSchema.Fqdn] = null;
					return;
				}
				bag[SmtpSendConnectorConfigSchema.Fqdn] = new Fqdn(((SmtpDomain)value).Domain);
			};
		}

		// Token: 0x04002DAE RID: 11694
		private const int UsernameMaxLength = 256;

		// Token: 0x04002DAF RID: 11695
		private const int PasswordMaxLength = 256;

		// Token: 0x04002DB0 RID: 11696
		public static readonly ADPropertyDefinition SmartHostsString = new ADPropertyDefinition("SmartHostsString", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchSmtpSmartHost", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB1 RID: 11697
		public static readonly ADPropertyDefinition Port = new ADPropertyDefinition("Port", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpSendPort", ADPropertyDefinitionFlags.PersistDefaultValue, 25, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 65535)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB2 RID: 11698
		public static readonly ADPropertyDefinition ConnectionInactivityTimeout = new ADPropertyDefinition("ConnectionInactivityTimeout", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchSmtpSendConnectionTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(10.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.OneDay),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB3 RID: 11699
		public static readonly ADPropertyDefinition SendConnectorSecurityFlags = new ADPropertyDefinition("SendConnectorSecurityFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchSmtpOutboundSecurityFlag", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB4 RID: 11700
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchSmtpSendEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB5 RID: 11701
		public static readonly ADPropertyDefinition TlsDomain = new ADPropertyDefinition("TlsDomain", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomainWithSubdomains), "msExchSmtpSendTlsDomain", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new DisallowStarSmtpDomainWithSubdomainsConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB6 RID: 11702
		public static readonly ADPropertyDefinition ErrorPoliciesBase = new ADPropertyDefinition("ErrorPolicies", ExchangeObjectVersion.Exchange2007, typeof(ErrorPolicies), "msExchSmtpSendNdrLevel", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.ErrorPolicies.Default, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB7 RID: 11703
		public static readonly ADPropertyDefinition ErrorPolicies = new ADPropertyDefinition("CalculatedErrorPolicies", ExchangeObjectVersion.Exchange2007, typeof(ErrorPolicies), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.ErrorPolicies.Default, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.ErrorPoliciesBase
		}, null, delegate(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[SmtpSendConnectorConfigSchema.ErrorPoliciesBase];
			num &= -3;
			return (ErrorPolicies)num;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			propertyBag[SmtpSendConnectorConfigSchema.ErrorPoliciesBase] = (ErrorPolicies)value;
		}, null, null);

		// Token: 0x04002DB8 RID: 11704
		public static readonly ADPropertyDefinition ProtocolLoggingLevel = new ADPropertyDefinition("ProtocolLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(ProtocolLoggingLevel), "msExchSmtpSendProtocolLoggingLevel", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.ProtocolLoggingLevel.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DB9 RID: 11705
		public static readonly ADPropertyDefinition AuthenticationUserName = new ADPropertyDefinition("AuthenticationUserName", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchSmtpOutboundSecurityUserName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x04002DBA RID: 11706
		public static readonly ADPropertyDefinition EncryptedAuthenticationPassword = new ADPropertyDefinition("EncryptedAuthenticationPassword", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchSmtpOutboundSecurityPassword", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x04002DBB RID: 11707
		public static readonly ADPropertyDefinition Fqdn = new ADPropertyDefinition("Fqdn", ExchangeObjectVersion.Exchange2007, typeof(Fqdn), "msExchSMTPSendConnectorFQDN", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DBC RID: 11708
		public static readonly ADPropertyDefinition TlsCertificateName = new ADPropertyDefinition("TlsCertificateName", ExchangeObjectVersion.Exchange2007, typeof(SmtpX509Identifier), "msExchSmtpTLSCertificate", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DBD RID: 11709
		public static readonly ADPropertyDefinition SendConnectorFlags = new ADPropertyDefinition("SendConnectorFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpSendFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DBE RID: 11710
		public static readonly ADPropertyDefinition SourceIPAddress = new ADPropertyDefinition("SourceIPAddress", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchSmtpSendBindingIPAddress", ADPropertyDefinitionFlags.None, IPAddress.Any, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DBF RID: 11711
		public static readonly ADPropertyDefinition SmtpMaxMessagesPerConnection = new ADPropertyDefinition("SmtpMaxMessagesPerConnection", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpMaxMessagesPerConnection", ADPropertyDefinitionFlags.PersistDefaultValue, 20, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002DC0 RID: 11712
		public static readonly ADPropertyDefinition DNSRoutingEnabled = new ADPropertyDefinition("DNSRoutingEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DC1 RID: 11713
		public static readonly ADPropertyDefinition SmartHosts = new ADPropertyDefinition("SmartHosts", ExchangeObjectVersion.Exchange2003, typeof(SmartHost), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SmartHostsString
		}, null, new GetterDelegate(SmtpSendConnectorConfig.SmartHostsGetter), new SetterDelegate(SmtpSendConnectorConfig.SmartHostsSetter), null, null);

		// Token: 0x04002DC2 RID: 11714
		public static readonly ADPropertyDefinition ForceHELO = new ADPropertyDefinition("ForceHELO", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags, 4194304), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags, 4194304), null, null);

		// Token: 0x04002DC3 RID: 11715
		public static readonly ADPropertyDefinition FrontendProxyEnabled = new ADPropertyDefinition("FrontendProxyEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 8192), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 8192), null, null);

		// Token: 0x04002DC4 RID: 11716
		public static readonly ADPropertyDefinition IgnoreSTARTTLS = new ADPropertyDefinition("IgnoreSTARTTLS", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags, 8388608), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags, 8388608), null, null);

		// Token: 0x04002DC5 RID: 11717
		public static readonly ADPropertyDefinition CloudServicesMailEnabled = new ADPropertyDefinition("CloudServicesMailEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 131072), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 131072), null, null);

		// Token: 0x04002DC6 RID: 11718
		public static readonly ADPropertyDefinition RequireOorg = new ADPropertyDefinition("RequireOorg", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 8), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 8), null, null);

		// Token: 0x04002DC7 RID: 11719
		public static readonly ADPropertyDefinition RequireTLS = new ADPropertyDefinition("RequireTLS", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags, 4), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorSecurityFlags, 4), null, null);

		// Token: 0x04002DC8 RID: 11720
		public static readonly ADPropertyDefinition AuthenticationCredential = new ADPropertyDefinition("AuthenticationCredential", ExchangeObjectVersion.Exchange2003, typeof(PSCredential), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.AuthenticationUserName,
			SmtpSendConnectorConfigSchema.EncryptedAuthenticationPassword
		}, null, new GetterDelegate(SmtpSendConnectorConfig.AuthenticationCredentialGetter), new SetterDelegate(SmtpSendConnectorConfig.AuthenticationCredentialSetter), null, null);

		// Token: 0x04002DC9 RID: 11721
		public static readonly ADPropertyDefinition UseExternalDNSServersEnabled = new ADPropertyDefinition("UseExternalDNSServersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 2), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 2), null, null);

		// Token: 0x04002DCA RID: 11722
		public static readonly ADPropertyDefinition DomainSecureEnabled = new ADPropertyDefinition("DomainSecureEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, ADObject.FlagGetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 4), ADObject.FlagSetterDelegate(SmtpSendConnectorConfigSchema.SendConnectorFlags, 4), null, null);

		// Token: 0x04002DCB RID: 11723
		public static readonly ADPropertyDefinition SmartHostAuthMechanism = new ADPropertyDefinition("SmartHostAuthMechanism", ExchangeObjectVersion.Exchange2007, typeof(SmtpSendConnectorConfig.AuthMechanisms), null, ADPropertyDefinitionFlags.Calculated, SmtpSendConnectorConfig.AuthMechanisms.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, new GetterDelegate(SmtpSendConnectorConfig.SmartHostAuthMechanismGetter), new SetterDelegate(SmtpSendConnectorConfig.SmartHostAuthMechanismSetter), null, null);

		// Token: 0x04002DCC RID: 11724
		public static readonly ADPropertyDefinition TlsAuthLevel = new ADPropertyDefinition("TlsAuthLevel", ExchangeObjectVersion.Exchange2007, typeof(TlsAuthLevel?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SmtpSendConnectorConfigSchema.SendConnectorFlags
		}, null, new GetterDelegate(SmtpSendConnectorConfig.TlsAuthLevelGetter), new SetterDelegate(SmtpSendConnectorConfig.TlsAuthLevelSetter), null, null);
	}
}
