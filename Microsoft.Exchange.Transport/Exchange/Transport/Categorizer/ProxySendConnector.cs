using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000247 RID: 583
	internal class ProxySendConnector : SmtpSendConnectorConfig
	{
		// Token: 0x06001979 RID: 6521 RVA: 0x00067228 File Offset: 0x00065428
		public ProxySendConnector(string name, Server localServerConfig, ADObjectId localRoutingGroupId, string fqdn)
		{
			if (localServerConfig == null)
			{
				throw new ArgumentNullException("localServerConfig");
			}
			if (localRoutingGroupId == null)
			{
				throw new ArgumentNullException("localRoutingGroupId");
			}
			ADObjectId childId = localRoutingGroupId.GetChildId("Connections").GetChildId(name);
			base.SetId(childId);
			base.Name = name;
			string text = fqdn;
			if (string.IsNullOrEmpty(text))
			{
				text = localServerConfig.Fqdn;
				if (string.IsNullOrEmpty(text))
				{
					text = "local";
				}
			}
			base.Fqdn = new Fqdn(text);
			base.SmtpMaxMessagesPerConnection = localServerConfig.IntraOrgConnectorSmtpMaxMessagesPerConnection;
			base.ProtocolLoggingLevel = localServerConfig.IntraOrgConnectorProtocolLoggingLevel;
			this.PopulateCalculatedProperties();
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000672C4 File Offset: 0x000654C4
		public ProxySendConnector(string name, Server localServerConfig, ADObjectId localRoutingGroupId, bool internalConnector, bool requireTls, TlsAuthLevel? tlsAuthLevel, SmtpDomainWithSubdomains tlsDomain, bool useExternalDnsServer, int port, string fqdn, string certificateSubject) : this(name, localServerConfig, localRoutingGroupId, fqdn)
		{
			base.Port = port;
			if (internalConnector)
			{
				base.SmartHostAuthMechanism = SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer;
			}
			else
			{
				base.RequireTLS = requireTls;
				base.TlsAuthLevel = tlsAuthLevel;
				base.TlsDomain = tlsDomain;
				base.UseExternalDNSServersEnabled = useExternalDnsServer;
				base.CertificateSubject = certificateSubject;
			}
			this.PopulateCalculatedProperties();
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006731F File Offset: 0x0006551F
		internal override RawSecurityDescriptor GetSecurityDescriptor()
		{
			return EnterpriseRelaySendConnector.SecurityDescriptor;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00067328 File Offset: 0x00065528
		private void PopulateCalculatedProperties()
		{
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (adpropertyDefinition.IsCalculated && !base.ExchangeVersion.IsOlderThan(adpropertyDefinition.VersionAdded))
				{
					object obj = this.propertyBag[adpropertyDefinition];
				}
			}
		}
	}
}
