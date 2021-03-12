using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid.Entity;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000928 RID: 2344
	internal class PowerShellTenantSession : PowerShellCommonSession, ITenantSession, ICommonSession, IDisposable
	{
		// Token: 0x0600538D RID: 21389 RVA: 0x001592CE File Offset: 0x001574CE
		public PowerShellTenantSession(ILogger logger, string targetServer, PSCredential credentials) : base(logger, targetServer, PowershellConnectionType.Tenant, credentials)
		{
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x001592DA File Offset: 0x001574DA
		public void EnableOrganizationCustomization()
		{
			base.RemotePowershellSession.RunCommand("Enable-OrganizationCustomization", null);
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x001592F6 File Offset: 0x001574F6
		public IEnumerable<IInboundConnector> GetInboundConnectors()
		{
			return (from c in base.RemotePowershellSession.RunOneCommand<TenantInboundConnector>("Get-InboundConnector", null, true)
			select new InboundConnector(c)).ToList<InboundConnector>();
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00159334 File Offset: 0x00157534
		public IInboundConnector GetInboundConnector(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			TenantInboundConnector tenantInboundConnector = base.RemotePowershellSession.RunOneCommandSingleResult<TenantInboundConnector>("Get-InboundConnector", sessionParameters, true);
			if (tenantInboundConnector != null)
			{
				return new InboundConnector(tenantInboundConnector);
			}
			return null;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00159374 File Offset: 0x00157574
		public IntraOrganizationConfiguration GetIntraOrganizationConfiguration()
		{
			SessionParameters parameters = new SessionParameters();
			return base.RemotePowershellSession.RunOneCommandSingleResult<IntraOrganizationConfiguration>("Get-IntraOrganizationConfiguration", parameters, true);
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0015939C File Offset: 0x0015759C
		public IntraOrganizationConnector GetIntraOrganizationConnector(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			return base.RemotePowershellSession.RunOneCommandSingleResult<IntraOrganizationConnector>("Get-IntraOrganizationConnector", sessionParameters, true);
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x001593D0 File Offset: 0x001575D0
		public IOrganizationalUnit GetOrganizationalUnit()
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("SingleNodeOnly", true);
			ExtendedOrganizationalUnit extendedOrganizationalUnit = base.RemotePowershellSession.RunOneCommandSingleResult<ExtendedOrganizationalUnit>("Get-OrganizationalUnit", sessionParameters, true);
			if (extendedOrganizationalUnit != null)
			{
				return new OrganizationalUnit
				{
					Name = extendedOrganizationalUnit.Name
				};
			}
			return null;
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x001594B0 File Offset: 0x001576B0
		public IOnPremisesOrganization GetOnPremisesOrganization(Guid organizationGuid)
		{
			return (from o in base.RemotePowershellSession.RunOneCommand<Microsoft.Exchange.Data.Directory.SystemConfiguration.OnPremisesOrganization>("Get-OnPremisesOrganization", null, true)
			select new Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization
			{
				Identity = (ADObjectId)o.Identity,
				OrganizationGuid = o.OrganizationGuid,
				OrganizationName = o.OrganizationName,
				HybridDomains = o.HybridDomains,
				InboundConnector = o.InboundConnector,
				OutboundConnector = o.OutboundConnector,
				Name = o.Name,
				OrganizationRelationship = o.OrganizationRelationship
			}).FirstOrDefault((Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization o) => o.OrganizationGuid == organizationGuid);
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x00159517 File Offset: 0x00157717
		public IEnumerable<IOutboundConnector> GetOutboundConnectors()
		{
			return (from c in base.RemotePowershellSession.RunOneCommand<TenantOutboundConnector>("Get-OutboundConnector", null, true)
			select new OutboundConnector(c)).ToList<OutboundConnector>();
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00159554 File Offset: 0x00157754
		public IOutboundConnector GetOutboundConnector(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			TenantOutboundConnector tenantOutboundConnector = base.RemotePowershellSession.RunOneCommandSingleResult<TenantOutboundConnector>("Get-OutboundConnector", sessionParameters, true);
			if (tenantOutboundConnector != null)
			{
				return new OutboundConnector(tenantOutboundConnector);
			}
			return null;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x00159594 File Offset: 0x00157794
		public IInboundConnector NewInboundConnector(IInboundConnector configuration)
		{
			SessionParameters parameters = this.BuildParameters(configuration);
			TenantInboundConnector tenantInboundConnector = base.RemotePowershellSession.RunOneCommandSingleResult<TenantInboundConnector>("New-InboundConnector", parameters, false);
			if (tenantInboundConnector != null)
			{
				return new InboundConnector(tenantInboundConnector);
			}
			return null;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x001595C8 File Offset: 0x001577C8
		public void NewIntraOrganizationConnector(string name, string discoveryEndpoint, MultiValuedProperty<SmtpDomain> targetAddressDomains, bool enabled)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", name);
			sessionParameters.Set("DiscoveryEndpoint", discoveryEndpoint);
			sessionParameters.Set<SmtpDomain>("TargetAddressDomains", targetAddressDomains);
			sessionParameters.Set("Enabled", enabled);
			base.RemotePowershellSession.RunOneCommand("New-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x00159620 File Offset: 0x00157820
		public IOnPremisesOrganization NewOnPremisesOrganization(IOrganizationConfig onPremisesOrgConfig, MultiValuedProperty<SmtpDomain> hybridDomains, IInboundConnector inboundConnector, IOutboundConnector outboundConnector, OrganizationRelationship tenantOrgRel)
		{
			Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization onPremisesOrganization = new Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization(onPremisesOrgConfig.Guid, onPremisesOrgConfig.Name, hybridDomains, inboundConnector.Identity, outboundConnector.Identity, onPremisesOrgConfig.Guid.ToString(), (ADObjectId)tenantOrgRel.Identity);
			SessionParameters sessionParameters = this.BuildParameters(onPremisesOrganization);
			sessionParameters.Set("Name", onPremisesOrganization.Name);
			sessionParameters.Set("OrganizationGuid", onPremisesOrganization.OrganizationGuid);
			Microsoft.Exchange.Data.Directory.SystemConfiguration.OnPremisesOrganization onPremisesOrganization2 = base.RemotePowershellSession.RunOneCommandSingleResult<Microsoft.Exchange.Data.Directory.SystemConfiguration.OnPremisesOrganization>("New-OnPremisesOrganization", sessionParameters, false);
			if (onPremisesOrganization2 != null)
			{
				return new Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization
				{
					Identity = (ADObjectId)onPremisesOrganization2.Identity,
					OrganizationGuid = onPremisesOrganization2.OrganizationGuid,
					OrganizationName = onPremisesOrganization2.OrganizationName,
					HybridDomains = onPremisesOrganization2.HybridDomains,
					InboundConnector = onPremisesOrganization2.InboundConnector,
					OutboundConnector = onPremisesOrganization2.OutboundConnector,
					Name = onPremisesOrganization2.Name,
					OrganizationRelationship = onPremisesOrganization2.OrganizationRelationship
				};
			}
			return null;
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0015971C File Offset: 0x0015791C
		public IOutboundConnector NewOutboundConnector(IOutboundConnector configuration)
		{
			SessionParameters parameters = this.BuildParameters(configuration);
			TenantOutboundConnector tenantOutboundConnector = base.RemotePowershellSession.RunOneCommandSingleResult<TenantOutboundConnector>("New-OutboundConnector", parameters, false);
			if (tenantOutboundConnector != null)
			{
				return new OutboundConnector(tenantOutboundConnector);
			}
			return null;
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x00159750 File Offset: 0x00157950
		public void RemoveInboundConnector(ADObjectId identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity.ToString());
			sessionParameters.Set("Confirm", false);
			base.RemotePowershellSession.RunOneCommand("Remove-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00159794 File Offset: 0x00157994
		public void RemoveIntraOrganizationConnector(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("Confirm", false);
			base.RemotePowershellSession.RunOneCommand("Remove-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x001597D4 File Offset: 0x001579D4
		public void RemoveOutboundConnector(ADObjectId identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity.ToString());
			sessionParameters.Set("Confirm", false);
			base.RemotePowershellSession.RunOneCommand("Remove-OutboundConnector", sessionParameters, false);
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00159818 File Offset: 0x00157A18
		public void SetFederatedOrganizationIdentifier(string defaultDomain)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("DefaultDomain", defaultDomain);
			sessionParameters.Set("Enabled", true);
			base.RemotePowershellSession.RunOneCommand("Set-FederatedOrganizationIdentifier", sessionParameters, false);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00159858 File Offset: 0x00157A58
		public void SetInboundConnector(IInboundConnector configuration)
		{
			SessionParameters sessionParameters = this.BuildParameters(configuration);
			sessionParameters.Set("Identity", configuration.Identity.ToString());
			base.RemotePowershellSession.RunCommand("Set-InboundConnector", sessionParameters);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00159898 File Offset: 0x00157A98
		public void SetIntraOrganizationConnector(string identity, string discoveryEndpoint, MultiValuedProperty<SmtpDomain> targetAddressDomains, bool enabled)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("DiscoveryEndpoint", discoveryEndpoint);
			sessionParameters.Set<SmtpDomain>("TargetAddressDomains", targetAddressDomains);
			sessionParameters.Set("Enabled", enabled);
			base.RemotePowershellSession.RunOneCommand("Set-IntraOrganizationConnector", sessionParameters, false);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x001598F0 File Offset: 0x00157AF0
		public void SetOnPremisesOrganization(IOnPremisesOrganization configuration, IOrganizationConfig onPremisesOrgConfig, MultiValuedProperty<SmtpDomain> hybridDomains, IInboundConnector inboundConnector, IOutboundConnector outboundConnector, OrganizationRelationship tenantOrgRel)
		{
			Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization onPremisesOrganization = (Microsoft.Exchange.Management.Hybrid.Entity.OnPremisesOrganization)configuration;
			onPremisesOrganization.HybridDomains = hybridDomains;
			onPremisesOrganization.InboundConnector = inboundConnector.Identity;
			onPremisesOrganization.OutboundConnector = outboundConnector.Identity;
			onPremisesOrganization.OrganizationName = onPremisesOrgConfig.Name;
			onPremisesOrganization.OrganizationRelationship = (ADObjectId)tenantOrgRel.Identity;
			SessionParameters sessionParameters = this.BuildParameters(configuration);
			sessionParameters.Set("Identity", configuration.Identity.ToString());
			base.RemotePowershellSession.RunCommand("Set-OnPremisesOrganization", sessionParameters);
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00159974 File Offset: 0x00157B74
		public void SetOutboundConnector(IOutboundConnector configuration)
		{
			SessionParameters sessionParameters = this.BuildParameters(configuration);
			sessionParameters.Set("Identity", configuration.Identity.ToString());
			base.RemotePowershellSession.RunCommand("Set-OutboundConnector", sessionParameters);
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x001599B1 File Offset: 0x00157BB1
		public void RenameInboundConnector(IInboundConnector c, string name)
		{
			((InboundConnector)c).Name = name;
			this.SetInboundConnector(c);
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x001599C6 File Offset: 0x00157BC6
		public void RenameOutboundConnector(IOutboundConnector c, string name)
		{
			((OutboundConnector)c).Name = name;
			this.SetOutboundConnector(c);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x001599DC File Offset: 0x00157BDC
		public IInboundConnector BuildExpectedInboundConnector(ADObjectId identity, string name, SmtpX509Identifier tlsCertificateName)
		{
			return new InboundConnector(name, tlsCertificateName)
			{
				Identity = identity
			};
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00159A08 File Offset: 0x00157C08
		public IOutboundConnector BuildExpectedOutboundConnector(ADObjectId identity, string name, string tlsCertificateSubjectDomainName, MultiValuedProperty<SmtpDomain> hybridDomains, string smartHost, bool centralizedTransportFeatureEnabled)
		{
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty = new MultiValuedProperty<SmtpDomainWithSubdomains>();
			if (centralizedTransportFeatureEnabled)
			{
				multiValuedProperty.Add(new SmtpDomainWithSubdomains("*"));
			}
			else
			{
				foreach (SmtpDomainWithSubdomains item in from s in hybridDomains
				select new SmtpDomainWithSubdomains(s.Domain))
				{
					multiValuedProperty.Add(item);
				}
			}
			MultiValuedProperty<SmartHost> smartHosts = new MultiValuedProperty<SmartHost>
			{
				new SmartHost(smartHost)
			};
			return new OutboundConnector(name, tlsCertificateSubjectDomainName, multiValuedProperty, smartHosts, centralizedTransportFeatureEnabled)
			{
				Identity = identity
			};
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00159AC0 File Offset: 0x00157CC0
		private SessionParameters BuildParameters(IInboundConnector configuration)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", configuration.Name);
			sessionParameters.Set("ConnectorType", configuration.ConnectorType);
			sessionParameters.Set("RequireTLS", configuration.RequireTls);
			sessionParameters.Set<AddressSpace>("SenderDomains", configuration.SenderDomains);
			sessionParameters.Set("TLSSenderCertificateName", configuration.TLSSenderCertificateName);
			sessionParameters.Set("CloudServicesMailEnabled", configuration.CloudServicesMailEnabled);
			return sessionParameters;
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00159B40 File Offset: 0x00157D40
		private SessionParameters BuildParameters(IOutboundConnector configuration)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", configuration.Name);
			sessionParameters.Set<SmtpDomainWithSubdomains>("RecipientDomains", configuration.RecipientDomains);
			sessionParameters.Set<SmartHost>("SmartHosts", configuration.SmartHosts);
			sessionParameters.Set("ConnectorType", configuration.ConnectorType);
			sessionParameters.Set("TLSSettings", (Enum)configuration.TlsSettings);
			sessionParameters.Set("TLSDomain", configuration.TlsDomain);
			sessionParameters.Set("CloudServicesMailEnabled", configuration.CloudServicesMailEnabled);
			sessionParameters.Set("RouteAllMessagesViaOnPremises", configuration.RouteAllMessagesViaOnPremises);
			sessionParameters.Set("UseMxRecord", false);
			return sessionParameters;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x00159C00 File Offset: 0x00157E00
		private SessionParameters BuildParameters(IOnPremisesOrganization configuration)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set<SmtpDomain>("HybridDomains", configuration.HybridDomains, (SmtpDomain d) => d.Domain);
			sessionParameters.Set("InboundConnector", TaskCommon.ToStringOrNull(configuration.InboundConnector));
			sessionParameters.Set("OutboundConnector", TaskCommon.ToStringOrNull(configuration.OutboundConnector));
			sessionParameters.Set("OrganizationRelationship", TaskCommon.ToStringOrNull(configuration.OrganizationRelationship));
			sessionParameters.Set("OrganizationName", configuration.OrganizationName);
			return sessionParameters;
		}

		// Token: 0x040030E8 RID: 12520
		private const string Enable_OrganizationCustomization = "Enable-OrganizationCustomization";

		// Token: 0x040030E9 RID: 12521
		private const string Get_AcceptedDomain = "Get-AcceptedDomain";

		// Token: 0x040030EA RID: 12522
		private const string Get_FederatedOrganizationIdentifier = "Get-FederatedOrganizationIdentifier";

		// Token: 0x040030EB RID: 12523
		private const string Get_InboundConnector = "Get-InboundConnector";

		// Token: 0x040030EC RID: 12524
		private const string Get_IntraOrganizationConfiguration = "Get-IntraOrganizationConfiguration";

		// Token: 0x040030ED RID: 12525
		private const string Get_IntraOrganizationConnector = "Get-IntraOrganizationConnector";

		// Token: 0x040030EE RID: 12526
		private const string Get_OrganizationalUnit = "Get-OrganizationalUnit";

		// Token: 0x040030EF RID: 12527
		private const string Get_OnPremisesOrganization = "Get-OnPremisesOrganization";

		// Token: 0x040030F0 RID: 12528
		private const string Get_OutboundConnector = "Get-OutboundConnector";

		// Token: 0x040030F1 RID: 12529
		private const string New_InboundConnector = "New-InboundConnector";

		// Token: 0x040030F2 RID: 12530
		private const string New_IntraOrganizationConnector = "New-IntraOrganizationConnector";

		// Token: 0x040030F3 RID: 12531
		private const string New_OnPremisesOrganization = "New-OnPremisesOrganization";

		// Token: 0x040030F4 RID: 12532
		private const string New_OutboundConnector = "New-OutboundConnector";

		// Token: 0x040030F5 RID: 12533
		private const string Remove_InboundConnector = "Remove-InboundConnector";

		// Token: 0x040030F6 RID: 12534
		private const string Remove_IntraOrganizationConnector = "Remove-IntraOrganizationConnector";

		// Token: 0x040030F7 RID: 12535
		private const string Remove_OutboundConnector = "Remove-OutboundConnector";

		// Token: 0x040030F8 RID: 12536
		private const string Set_InboundConnector = "Set-InboundConnector";

		// Token: 0x040030F9 RID: 12537
		private const string Set_IntraOrganizationConnector = "Set-IntraOrganizationConnector";

		// Token: 0x040030FA RID: 12538
		private const string Set_OnPremisesOrganization = "Set-OnPremisesOrganization";

		// Token: 0x040030FB RID: 12539
		private const string Set_OutboundConnector = "Set-OutboundConnector";
	}
}
