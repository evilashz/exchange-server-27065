using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000232 RID: 562
	internal class EnterpriseRelaySendConnector : SmtpSendConnectorConfig
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x00064C34 File Offset: 0x00062E34
		public EnterpriseRelaySendConnector(Server localServerConfig, ADObjectId localRoutingGroupId, bool disableExchangeServerAuth)
		{
			if (localServerConfig == null)
			{
				throw new ArgumentNullException("localServerConfig");
			}
			if (localRoutingGroupId == null)
			{
				throw new ArgumentNullException("localRoutingGroupId");
			}
			ADObjectId childId = localRoutingGroupId.GetChildId("Connections").GetChildId(Strings.IntraorgSendConnectorName);
			base.SetId(childId);
			base.Name = Strings.IntraorgSendConnectorName;
			base.CloudServicesMailEnabled = true;
			string text = localServerConfig.Fqdn;
			if (string.IsNullOrEmpty(text))
			{
				text = "local";
			}
			base.Fqdn = new Fqdn(text);
			base.SmtpMaxMessagesPerConnection = localServerConfig.IntraOrgConnectorSmtpMaxMessagesPerConnection;
			base.ProtocolLoggingLevel = localServerConfig.IntraOrgConnectorProtocolLoggingLevel;
			if (disableExchangeServerAuth)
			{
				base.SmartHostAuthMechanism = SmtpSendConnectorConfig.AuthMechanisms.None;
			}
			else
			{
				base.SmartHostAuthMechanism = SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer;
			}
			if (EnterpriseRelaySendConnector.SecurityDescriptor == null)
			{
				try
				{
					EnterpriseRelaySendConnector.securityDescriptor = EnterpriseRelaySendConnector.CreateSecurityDescriptor(disableExchangeServerAuth);
				}
				catch (ErrorExchangeGroupNotFoundException inner)
				{
					throw new TransportComponentLoadFailedException(Strings.ReadingADConfigFailed, inner);
				}
				catch (ADTransientException inner2)
				{
					throw new TransportComponentLoadFailedException(Strings.ReadingADConfigFailed, inner2);
				}
			}
			this.PopulateCalculatedProperties();
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00064D44 File Offset: 0x00062F44
		public static RawSecurityDescriptor SecurityDescriptor
		{
			get
			{
				return EnterpriseRelaySendConnector.securityDescriptor;
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00064D4B File Offset: 0x00062F4B
		internal override RawSecurityDescriptor GetSecurityDescriptor()
		{
			return EnterpriseRelaySendConnector.SecurityDescriptor;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00064D54 File Offset: 0x00062F54
		private static RawSecurityDescriptor CreateSecurityDescriptor(bool disableExchangeServerAuth)
		{
			PrincipalPermissionList permissions = EnterpriseRelaySendConnector.GetPermissions(disableExchangeServerAuth);
			SecurityIdentifier principal = permissions[0].Principal;
			SecurityIdentifier group = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, principal.AccountDomainSid);
			return permissions.CreateExtendedRightsSecurityDescriptor(principal, group);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00064E38 File Offset: 0x00063038
		private static PrincipalPermissionList GetPermissions(bool disableExchangeServerAuth)
		{
			IConfigurationSession configSession = null;
			IRecipientSession recipSession = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 223, "GetPermissions", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Categorizer\\Routing\\EnterpriseRelaySendConnector.cs");
				configSession.UseConfigNC = false;
				recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 229, "GetPermissions", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Categorizer\\Routing\\EnterpriseRelaySendConnector.cs");
			}, 0);
			PrincipalPermissionList sids = new PrincipalPermissionList(5);
			sids.Add(WellKnownSids.HubTransportServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow);
			sids.Add(WellKnownSids.EdgeTransportServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow);
			sids.Add(WellKnownSids.LegacyExchangeServers, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders);
			ADNotificationAdapter.RunADOperation(delegate()
			{
				SecurityIdentifier sidForExchangeKnownGuid = ReceiveConnector.PermissionGroupPermissions.GetSidForExchangeKnownGuid(recipSession, WellKnownGuid.ExSWkGuid, configSession.ConfigurationNamingContext.DistinguishedName);
				sids.Add(sidForExchangeKnownGuid, Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SMTPSendXShadow);
			});
			if (disableExchangeServerAuth)
			{
				sids.Add(SmtpSendConnectorConfig.AnonymousSecurityIdentifier, Permission.SMTPSendXShadow);
			}
			return sids;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00064EE8 File Offset: 0x000630E8
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

		// Token: 0x04000BF7 RID: 3063
		private static RawSecurityDescriptor securityDescriptor;
	}
}
