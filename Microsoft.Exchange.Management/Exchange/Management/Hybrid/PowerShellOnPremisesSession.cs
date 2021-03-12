using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid.Entity;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000925 RID: 2341
	internal class PowerShellOnPremisesSession : PowerShellCommonSession, IOnPremisesSession, ICommonSession, IDisposable
	{
		// Token: 0x06005345 RID: 21317 RVA: 0x00157DE7 File Offset: 0x00155FE7
		public PowerShellOnPremisesSession(ILogger logger, string targetServer, PSCredential credentials) : base(logger, targetServer, PowershellConnectionType.OnPrem, credentials)
		{
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x00157DF4 File Offset: 0x00155FF4
		public void AddAvailabilityAddressSpace(string forestName, AvailabilityAccessMethod accessMethod, bool useServiceAccount, Uri proxyUrl)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("ForestName", forestName);
			sessionParameters.Set("AccessMethod", accessMethod);
			sessionParameters.Set("UseServiceAccount", useServiceAccount);
			sessionParameters.Set("ProxyUrl", proxyUrl);
			base.RemotePowershellSession.RunOneCommand("Add-AvailabilityAddressSpace", sessionParameters, false);
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x00157E50 File Offset: 0x00156050
		public void AddFederatedDomain(string domainName)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("DomainName", domainName);
			base.RemotePowershellSession.RunCommand("Add-FederatedDomain", sessionParameters);
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x00157E81 File Offset: 0x00156081
		public IEnumerable<AvailabilityAddressSpace> GetAvailabilityAddressSpace()
		{
			return base.RemotePowershellSession.RunOneCommand<AvailabilityAddressSpace>("Get-AvailabilityAddressSpace", null, true);
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x00157E95 File Offset: 0x00156095
		public IEnumerable<EmailAddressPolicy> GetEmailAddressPolicy()
		{
			return base.RemotePowershellSession.RunOneCommand<EmailAddressPolicy>("Get-EmailAddressPolicy", null, true);
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x00157EB4 File Offset: 0x001560B4
		public IEnumerable<IExchangeCertificate> GetExchangeCertificate(string server)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Server", server);
			return from c in base.RemotePowershellSession.RunOneCommand<ExchangeCertificate>("Get-ExchangeCertificate", sessionParameters, true)
			select new PowerShellOnPremisesSession.Certificate(c);
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x00157F08 File Offset: 0x00156108
		public IExchangeCertificate GetExchangeCertificate(string server, SmtpX509Identifier certificateName)
		{
			foreach (IExchangeCertificate exchangeCertificate in this.GetExchangeCertificate(server))
			{
				if (TaskCommon.AreEqual(exchangeCertificate.Identifier, certificateName))
				{
					return exchangeCertificate;
				}
			}
			return null;
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00157F64 File Offset: 0x00156164
		public IExchangeServer GetExchangeServer(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			Microsoft.Exchange.Data.Directory.Management.ExchangeServer exchangeServer = base.RemotePowershellSession.RunOneCommandSingleResult<Microsoft.Exchange.Data.Directory.Management.ExchangeServer>("Get-ExchangeServer", sessionParameters, true);
			if (exchangeServer != null)
			{
				return new Microsoft.Exchange.Management.Hybrid.Entity.ExchangeServer
				{
					Identity = (exchangeServer.Identity as ADObjectId),
					Name = exchangeServer.Name,
					ServerRole = exchangeServer.ServerRole,
					AdminDisplayVersion = exchangeServer.AdminDisplayVersion
				};
			}
			return null;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00158021 File Offset: 0x00156221
		public IEnumerable<IExchangeServer> GetExchangeServer()
		{
			return from result in base.RemotePowershellSession.RunOneCommand<Microsoft.Exchange.Data.Directory.Management.ExchangeServer>("Get-ExchangeServer", null, true)
			select new Microsoft.Exchange.Management.Hybrid.Entity.ExchangeServer
			{
				Identity = (result.Identity as ADObjectId),
				Name = result.Name,
				ServerRole = result.ServerRole,
				AdminDisplayVersion = result.AdminDisplayVersion
			};
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00158084 File Offset: 0x00156284
		public IEnumerable<IFederationTrust> GetFederationTrust()
		{
			return from f in base.RemotePowershellSession.RunOneCommand<Microsoft.Exchange.Data.Directory.SystemConfiguration.FederationTrust>("Get-FederationTrust", null, false)
			select new Microsoft.Exchange.Management.Hybrid.Entity.FederationTrust
			{
				Name = f.Name,
				TokenIssuerUri = f.TokenIssuerUri
			};
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x001580BC File Offset: 0x001562BC
		public IntraOrganizationConfiguration GetIntraOrganizationConfiguration()
		{
			SessionParameters parameters = new SessionParameters();
			return base.RemotePowershellSession.RunOneCommandSingleResult<IntraOrganizationConfiguration>("Get-IntraOrganizationConfiguration", parameters, true);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x001580E4 File Offset: 0x001562E4
		public IntraOrganizationConnector GetIntraOrganizationConnector(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			return base.RemotePowershellSession.RunOneCommandSingleResult<IntraOrganizationConnector>("Get-IntraOrganizationConnector", sessionParameters, true);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0015819C File Offset: 0x0015639C
		public IReceiveConnector GetReceiveConnector(ADObjectId server)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Server", server.Name);
			return (from c in base.RemotePowershellSession.RunOneCommand<Microsoft.Exchange.Data.Directory.SystemConfiguration.ReceiveConnector>("Get-ReceiveConnector", sessionParameters, true).Where(delegate(Microsoft.Exchange.Data.Directory.SystemConfiguration.ReceiveConnector c)
			{
				if (c.Bindings != null)
				{
					foreach (IPBinding ipbinding in c.Bindings)
					{
						if (ipbinding.Port == 25 && ipbinding.Address.ToString() == "::")
						{
							return true;
						}
					}
					return false;
				}
				return false;
			})
			select new Microsoft.Exchange.Management.Hybrid.Entity.ReceiveConnector(c)).FirstOrDefault<Microsoft.Exchange.Management.Hybrid.Entity.ReceiveConnector>();
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x00158223 File Offset: 0x00156423
		public IEnumerable<ISendConnector> GetSendConnector()
		{
			return (from c in base.RemotePowershellSession.RunOneCommand<SmtpSendConnectorConfig>("Get-SendConnector", null, true)
			select new Microsoft.Exchange.Management.Hybrid.Entity.SendConnector(c)).ToList<Microsoft.Exchange.Management.Hybrid.Entity.SendConnector>();
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x00158260 File Offset: 0x00156460
		public IEnumerable<ADWebServicesVirtualDirectory> GetWebServicesVirtualDirectory(string server)
		{
			SessionParameters sessionParameters = new SessionParameters();
			if (server != null)
			{
				sessionParameters.Set("Server", server);
			}
			return base.RemotePowershellSession.RunOneCommand<ADWebServicesVirtualDirectory>("Get-WebServicesVirtualDirectory", (server != null) ? sessionParameters : null, true);
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0015829C File Offset: 0x0015649C
		public void NewAcceptedDomain(string domainName, string name)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("DomainName", domainName);
			sessionParameters.Set("Name", name);
			base.RemotePowershellSession.RunCommand("New-AcceptedDomain", sessionParameters);
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x001582DC File Offset: 0x001564DC
		public void NewIntraOrganizationConnector(string name, string discoveryEndpoint, string onlineTargetAddress, bool enabled)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", name);
			sessionParameters.Set("DiscoveryEndpoint", discoveryEndpoint);
			MultiValuedProperty<SmtpDomain> multiValuedProperty = new MultiValuedProperty<SmtpDomain>();
			SmtpDomain item = new SmtpDomain(onlineTargetAddress);
			multiValuedProperty.TryAdd(item);
			sessionParameters.Set<SmtpDomain>("TargetAddressDomains", multiValuedProperty);
			sessionParameters.Set("Enabled", enabled);
			base.RemotePowershellSession.RunOneCommand("New-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x00158348 File Offset: 0x00156548
		public DomainContentConfig NewRemoteDomain(string domainName, string name)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", name);
			sessionParameters.Set("DomainName", domainName);
			return base.RemotePowershellSession.RunOneCommandSingleResult<DomainContentConfig>("New-RemoteDomain", sessionParameters, false);
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00158388 File Offset: 0x00156588
		public ISendConnector NewSendConnector(ISendConnector configuration)
		{
			SessionParameters parameters = this.SetSendConnectorParameters(configuration);
			SmtpSendConnectorConfig smtpSendConnectorConfig = base.RemotePowershellSession.RunOneCommandSingleResult<SmtpSendConnectorConfig>("New-SendConnector", parameters, false);
			if (smtpSendConnectorConfig != null)
			{
				return new Microsoft.Exchange.Management.Hybrid.Entity.SendConnector(smtpSendConnectorConfig);
			}
			return null;
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x001583BC File Offset: 0x001565BC
		public void RemoveAvailabilityAddressSpace(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("Confirm", false);
			base.RemotePowershellSession.RunOneCommand("Remove-AvailabilityAddressSpace", sessionParameters, false);
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x001583FC File Offset: 0x001565FC
		public void RemoveIntraOrganizationConnector(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("Confirm", false);
			base.RemotePowershellSession.RunOneCommand("Remove-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0015843C File Offset: 0x0015663C
		public void SetEmailAddressPolicy(string identity, string includedRecipients, ProxyAddressTemplateCollection enabledEmailAddressTemplates)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("ForceUpgrade", true);
			if (!string.IsNullOrEmpty(includedRecipients))
			{
				sessionParameters.Set("IncludedRecipients", includedRecipients);
			}
			if (enabledEmailAddressTemplates != null)
			{
				sessionParameters.Set<ProxyAddressTemplate>("EnabledEmailAddressTemplates", enabledEmailAddressTemplates);
			}
			base.RemotePowershellSession.RunCommand("Set-EmailAddressPolicy", sessionParameters);
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0015849C File Offset: 0x0015669C
		public void SetFederatedOrganizationIdentifier(string accountNamespace, string delegationTrustLink, string defaultDomain)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("AccountNamespace", accountNamespace);
			sessionParameters.Set("DelegationFederationTrust", delegationTrustLink);
			sessionParameters.Set("Enabled", true);
			sessionParameters.Set("DefaultDomain", defaultDomain);
			base.RemotePowershellSession.RunOneCommand("Set-FederatedOrganizationIdentifier", sessionParameters, false);
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x001584F4 File Offset: 0x001566F4
		public void SetFederationTrustRefreshMetadata(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.SetNull<bool>("RefreshMetadata");
			base.RemotePowershellSession.RunCommand("Set-Federationtrust", sessionParameters);
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x00158530 File Offset: 0x00156730
		public void SetIntraOrganizationConnector(string identity, string discoveryEndpoint, string onlineTargetAddress, bool enabled)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("DiscoveryEndpoint", discoveryEndpoint);
			MultiValuedProperty<SmtpDomain> multiValuedProperty = new MultiValuedProperty<SmtpDomain>();
			SmtpDomain item = new SmtpDomain(onlineTargetAddress);
			multiValuedProperty.TryAdd(item);
			sessionParameters.Set<SmtpDomain>("TargetAddressDomains", multiValuedProperty);
			sessionParameters.Set("Enabled", enabled);
			base.RemotePowershellSession.RunOneCommand("Set-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0015859C File Offset: 0x0015679C
		public void SetReceiveConnector(IReceiveConnector configuration)
		{
			SessionParameters sessionParameters = new SessionParameters();
			if (configuration.Identity != null)
			{
				sessionParameters.Set("Identity", configuration.Identity.ToString());
			}
			sessionParameters.Set("TLSCertificateName", TaskCommon.ToStringOrNull(configuration.TlsCertificateName));
			sessionParameters.Set("TLSDomainCapabilities", TaskCommon.ToStringOrNull(configuration.TlsDomainCapabilities));
			base.RemotePowershellSession.RunOneCommand("Set-ReceiveConnector", sessionParameters, false);
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0015860B File Offset: 0x0015680B
		public void SetRemoteDomain(string identity, SessionParameters parameters)
		{
			parameters.Set("Identity", identity);
			base.RemotePowershellSession.RunOneCommand("Set-RemoteDomain", parameters, false);
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0015862C File Offset: 0x0015682C
		public void SetSendConnector(ISendConnector configuration)
		{
			SessionParameters sessionParameters = this.SetSendConnectorParameters(configuration);
			sessionParameters.Set("Identity", configuration.Identity.ToString());
			base.RemotePowershellSession.RunOneCommand("Set-SendConnector", sessionParameters, false);
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0015866C File Offset: 0x0015686C
		private SessionParameters SetSendConnectorParameters(ISendConnector configuration)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", configuration.Name);
			sessionParameters.Set<AddressSpace>("AddressSpaces", configuration.AddressSpaces);
			sessionParameters.Set<ADObjectId>("SourceTransportServers", configuration.SourceTransportServers);
			sessionParameters.Set("DNSRoutingEnabled", configuration.DNSRoutingEnabled);
			sessionParameters.Set<SmartHost>("SmartHosts", configuration.SmartHosts);
			sessionParameters.Set("TLSDomain", configuration.TlsDomain);
			sessionParameters.Set("RequireTLS", configuration.RequireTLS);
			sessionParameters.Set("TLSAuthLevel", (Enum)configuration.TlsAuthLevel);
			sessionParameters.Set("ErrorPolicies", configuration.ErrorPolicies);
			sessionParameters.Set("TLSCertificateName", TaskCommon.ToStringOrNull(configuration.TlsCertificateName));
			sessionParameters.Set("CloudServicesMailEnabled", configuration.CloudServicesMailEnabled);
			sessionParameters.Set("Fqdn", configuration.Fqdn);
			return sessionParameters;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00158760 File Offset: 0x00156960
		public void SetWebServicesVirtualDirectory(string distinguishedName, bool proxyEnabled)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", distinguishedName);
			sessionParameters.Set("MRSProxyEnabled", proxyEnabled);
			base.RemotePowershellSession.RunCommand("Set-WebServicesVirtualDirectory", sessionParameters);
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x001587A0 File Offset: 0x001569A0
		public void UpdateEmailAddressPolicy(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("UpdateSecondaryAddressesOnly", true);
			base.RemotePowershellSession.RunOneCommand("Update-EmailAddressPolicy", sessionParameters, false);
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x001587E0 File Offset: 0x001569E0
		public ISendConnector BuildExpectedSendConnector(string name, string tenantCoexistenceDomain, MultiValuedProperty<ADObjectId> servers, string fqdn, string fopeCertificateSubjectDomainName, SmtpX509Identifier tlsCertificateName, bool enableSecureMail)
		{
			return new Microsoft.Exchange.Management.Hybrid.Entity.SendConnector(name, new MultiValuedProperty<AddressSpace>
			{
				new AddressSpace(tenantCoexistenceDomain)
			}, servers, fopeCertificateSubjectDomainName, tlsCertificateName, enableSecureMail, fqdn);
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00158810 File Offset: 0x00156A10
		public IReceiveConnector BuildExpectedReceiveConnector(ADObjectId server, SmtpX509Identifier tlsCertificateName, SmtpReceiveDomainCapabilities tlsDomainCapabilities)
		{
			return new Microsoft.Exchange.Management.Hybrid.Entity.ReceiveConnector
			{
				Server = server,
				TlsCertificateName = tlsCertificateName,
				TlsDomainCapabilities = tlsDomainCapabilities
			};
		}

		// Token: 0x040030B4 RID: 12468
		private const string Add_AvailabilityAddressSpace = "Add-AvailabilityAddressSpace";

		// Token: 0x040030B5 RID: 12469
		private const string Add_FederatedDomain = "Add-FederatedDomain";

		// Token: 0x040030B6 RID: 12470
		private const string Get_AvailabilityAddressSpace = "Get-AvailabilityAddressSpace";

		// Token: 0x040030B7 RID: 12471
		private const string Get_EmailAddressPolicy = "Get-EmailAddressPolicy";

		// Token: 0x040030B8 RID: 12472
		private const string Get_ExchangeCertificate = "Get-ExchangeCertificate";

		// Token: 0x040030B9 RID: 12473
		private const string Get_ExchangeServer = "Get-ExchangeServer";

		// Token: 0x040030BA RID: 12474
		private const string Get_IntraOrganizationConfiguration = "Get-IntraOrganizationConfiguration";

		// Token: 0x040030BB RID: 12475
		private const string Get_IntraOrganizationConnector = "Get-IntraOrganizationConnector";

		// Token: 0x040030BC RID: 12476
		private const string Get_ReceiveConnector = "Get-ReceiveConnector";

		// Token: 0x040030BD RID: 12477
		private const string Get_SendConnector = "Get-SendConnector";

		// Token: 0x040030BE RID: 12478
		private const string Get_WebServicesVirtualDirectory = "Get-WebServicesVirtualDirectory";

		// Token: 0x040030BF RID: 12479
		private const string New_AcceptedDomain = "New-AcceptedDomain";

		// Token: 0x040030C0 RID: 12480
		private const string New_IntraOrganizationConnector = "New-IntraOrganizationConnector";

		// Token: 0x040030C1 RID: 12481
		private const string New_RemoteDomain = "New-RemoteDomain";

		// Token: 0x040030C2 RID: 12482
		private const string New_SendConnector = "New-SendConnector";

		// Token: 0x040030C3 RID: 12483
		private const string Remove_AvailabilityAddressSpace = "Remove-AvailabilityAddressSpace";

		// Token: 0x040030C4 RID: 12484
		private const string Remove_IntraOrganizationConnector = "Remove-IntraOrganizationConnector";

		// Token: 0x040030C5 RID: 12485
		private const string Set_EmailAddressPolicy = "Set-EmailAddressPolicy";

		// Token: 0x040030C6 RID: 12486
		private const string Set_FederationTrust = "Set-Federationtrust";

		// Token: 0x040030C7 RID: 12487
		private const string Set_IntraOrganizationConnector = "Set-IntraOrganizationConnector";

		// Token: 0x040030C8 RID: 12488
		private const string Set_ReceiveConnector = "Set-ReceiveConnector";

		// Token: 0x040030C9 RID: 12489
		private const string Set_RemoteDomain = "Set-RemoteDomain";

		// Token: 0x040030CA RID: 12490
		private const string Set_SendConnector = "Set-SendConnector";

		// Token: 0x040030CB RID: 12491
		private const string Set_WebServicesVirtualDirectory = "Set-WebServicesVirtualDirectory";

		// Token: 0x040030CC RID: 12492
		private const string Update_EmailAddressPolicy = "Update-EmailAddressPolicy";

		// Token: 0x040030CD RID: 12493
		private const string SnapinName = "Microsoft.Exchange.Management.PowerShell.Setup";

		// Token: 0x02000926 RID: 2342
		private class Certificate : IExchangeCertificate
		{
			// Token: 0x0600536C RID: 21356 RVA: 0x00158839 File Offset: 0x00156A39
			public Certificate(ExchangeCertificate certificate)
			{
				this.certificate = certificate;
			}

			// Token: 0x170018EC RID: 6380
			// (get) Token: 0x0600536D RID: 21357 RVA: 0x00158848 File Offset: 0x00156A48
			public string Subject
			{
				get
				{
					return this.certificate.Subject;
				}
			}

			// Token: 0x170018ED RID: 6381
			// (get) Token: 0x0600536E RID: 21358 RVA: 0x00158855 File Offset: 0x00156A55
			public string Issuer
			{
				get
				{
					return this.certificate.Issuer;
				}
			}

			// Token: 0x170018EE RID: 6382
			// (get) Token: 0x0600536F RID: 21359 RVA: 0x00158862 File Offset: 0x00156A62
			public string Thumbprint
			{
				get
				{
					return this.certificate.Thumbprint;
				}
			}

			// Token: 0x170018EF RID: 6383
			// (get) Token: 0x06005370 RID: 21360 RVA: 0x0015886F File Offset: 0x00156A6F
			public bool IsSelfSigned
			{
				get
				{
					return this.certificate.IsSelfSigned;
				}
			}

			// Token: 0x170018F0 RID: 6384
			// (get) Token: 0x06005371 RID: 21361 RVA: 0x0015887C File Offset: 0x00156A7C
			public DateTime NotAfter
			{
				get
				{
					return this.certificate.NotAfter;
				}
			}

			// Token: 0x170018F1 RID: 6385
			// (get) Token: 0x06005372 RID: 21362 RVA: 0x00158889 File Offset: 0x00156A89
			public DateTime NotBefore
			{
				get
				{
					return this.certificate.NotBefore;
				}
			}

			// Token: 0x170018F2 RID: 6386
			// (get) Token: 0x06005373 RID: 21363 RVA: 0x00158896 File Offset: 0x00156A96
			public IList<SmtpDomainWithSubdomains> CertificateDomains
			{
				get
				{
					return this.certificate.CertificateDomains;
				}
			}

			// Token: 0x170018F3 RID: 6387
			// (get) Token: 0x06005374 RID: 21364 RVA: 0x001588A3 File Offset: 0x00156AA3
			public AllowedServices Services
			{
				get
				{
					return this.certificate.Services;
				}
			}

			// Token: 0x170018F4 RID: 6388
			// (get) Token: 0x06005375 RID: 21365 RVA: 0x001588B0 File Offset: 0x00156AB0
			public SmtpX509Identifier Identifier
			{
				get
				{
					SmtpX509Identifier result;
					try
					{
						result = SmtpX509Identifier.Parse(string.Format("<I>{0}<S>{1},", this.Issuer, this.Subject));
					}
					catch
					{
						result = null;
					}
					return result;
				}
			}

			// Token: 0x06005376 RID: 21366 RVA: 0x001588F4 File Offset: 0x00156AF4
			public bool Verify()
			{
				return this.certificate.Verify();
			}

			// Token: 0x040030D4 RID: 12500
			private ExchangeCertificate certificate;
		}
	}
}
