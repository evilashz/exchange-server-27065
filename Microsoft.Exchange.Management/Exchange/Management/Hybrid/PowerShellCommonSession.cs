using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid.Entity;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000922 RID: 2338
	internal abstract class PowerShellCommonSession : ICommonSession, IDisposable
	{
		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x00157869 File Offset: 0x00155A69
		// (set) Token: 0x06005315 RID: 21269 RVA: 0x00157871 File Offset: 0x00155A71
		private protected RemotePowershellSession RemotePowershellSession { protected get; private set; }

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06005316 RID: 21270 RVA: 0x0015787C File Offset: 0x00155A7C
		// (remove) Token: 0x06005317 RID: 21271 RVA: 0x001578B4 File Offset: 0x00155AB4
		public event Func<string, bool> ShouldInvokePowershellCommand;

		// Token: 0x06005318 RID: 21272 RVA: 0x001578E9 File Offset: 0x00155AE9
		public PowerShellCommonSession(ILogger logger, string targetServer, PowershellConnectionType connectionType, PSCredential credentials)
		{
			this.RemotePowershellSession = new RemotePowershellSession(logger, targetServer, connectionType, true, new Func<string, bool>(this.ShouldInvokePowershellCommandHandler));
			this.RemotePowershellSession.Connect(credentials, Thread.CurrentThread.CurrentUICulture);
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0015792B File Offset: 0x00155B2B
		public IEnumerable<IAcceptedDomain> GetAcceptedDomain()
		{
			return (from a in this.RemotePowershellSession.RunOneCommand<Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain>("Get-AcceptedDomain", null, true)
			select new Microsoft.Exchange.Management.Hybrid.Entity.AcceptedDomain(a)).ToList<Microsoft.Exchange.Management.Hybrid.Entity.AcceptedDomain>();
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x00157968 File Offset: 0x00155B68
		public IFederatedOrganizationIdentifier GetFederatedOrganizationIdentifier()
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.SetNull<bool>("IncludeExtendedDomainInfo");
			FederatedOrganizationIdWithDomainStatus federatedOrganizationIdWithDomainStatus = this.RemotePowershellSession.RunOneCommandSingleResult<FederatedOrganizationIdWithDomainStatus>("Get-FederatedOrganizationIdentifier", sessionParameters, true);
			if (federatedOrganizationIdWithDomainStatus != null)
			{
				return new FederatedOrganizationIdentifier
				{
					Enabled = federatedOrganizationIdWithDomainStatus.Enabled,
					Domains = federatedOrganizationIdWithDomainStatus.Domains,
					AccountNamespace = federatedOrganizationIdWithDomainStatus.AccountNamespace,
					DelegationTrustLink = federatedOrganizationIdWithDomainStatus.DelegationTrustLink,
					DefaultDomain = federatedOrganizationIdWithDomainStatus.DefaultDomain
				};
			}
			return null;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x001579E4 File Offset: 0x00155BE4
		public IFederationTrust GetFederationTrust(ObjectId identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity.ToString());
			Microsoft.Exchange.Data.Directory.SystemConfiguration.FederationTrust federationTrust = this.RemotePowershellSession.RunOneCommandSingleResult<Microsoft.Exchange.Data.Directory.SystemConfiguration.FederationTrust>("Get-FederationTrust", sessionParameters, false);
			if (federationTrust != null)
			{
				return new Microsoft.Exchange.Management.Hybrid.Entity.FederationTrust
				{
					Name = federationTrust.Name,
					TokenIssuerUri = federationTrust.TokenIssuerUri
				};
			}
			return null;
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x00157A40 File Offset: 0x00155C40
		public IFederationInformation GetFederationInformation(string domainName)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("DomainName", domainName);
			sessionParameters.Set("BypassAdditionalDomainValidation", true);
			Dictionary<string, object> dictionary = null;
			try
			{
				dictionary = this.RemotePowershellSession.GetPowershellUntypedObjectAsMembers("Get-FederationInformation", null, sessionParameters);
			}
			catch
			{
			}
			if (dictionary != null)
			{
				FederationInformation federationInformation = new FederationInformation();
				object value;
				if (dictionary.TryGetValue("TargetAutodiscoverEpr", out value))
				{
					federationInformation.TargetAutodiscoverEpr = TaskCommon.ToStringOrNull(value);
				}
				if (dictionary.TryGetValue("TargetApplicationUri", out value))
				{
					federationInformation.TargetApplicationUri = TaskCommon.ToStringOrNull(value);
				}
				return federationInformation;
			}
			return null;
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x00157AD8 File Offset: 0x00155CD8
		public void RemoveOrganizationRelationship(string identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Identity", identity);
			sessionParameters.Set("Confirm", false);
			this.RemotePowershellSession.RunCommand("Remove-OrganizationRelationship", sessionParameters);
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x00157B20 File Offset: 0x00155D20
		public void NewOrganizationRelationship(string name, string targetApplicationUri, string targetAutodiscoverEpr, IEnumerable<SmtpDomain> domainNames)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Name", name);
			sessionParameters.Set("TargetApplicationUri", targetApplicationUri);
			sessionParameters.Set("TargetAutodiscoverEpr", targetAutodiscoverEpr);
			sessionParameters.Set("Enabled", true);
			sessionParameters.Set<SmtpDomain>("DomainNames", domainNames, (SmtpDomain d) => d.Domain);
			this.RemotePowershellSession.RunCommand("New-OrganizationRelationship", sessionParameters);
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x00157B9F File Offset: 0x00155D9F
		public IEnumerable<OrganizationRelationship> GetOrganizationRelationship()
		{
			return this.RemotePowershellSession.RunOneCommand<OrganizationRelationship>("Get-OrganizationRelationship", null, true);
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x00157BB3 File Offset: 0x00155DB3
		public IEnumerable<DomainContentConfig> GetRemoteDomain()
		{
			return this.RemotePowershellSession.RunOneCommand<DomainContentConfig>("Get-RemoteDomain", null, true);
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x00157BC8 File Offset: 0x00155DC8
		public void RemoveRemoteDomain(ObjectId identity)
		{
			SessionParameters sessionParameters = new SessionParameters();
			sessionParameters.Set("Confirm", false);
			sessionParameters.Set("Identity", identity.ToString());
			this.RemotePowershellSession.RunOneCommand("Remove-RemoteDomain", sessionParameters, false);
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x00157C0A File Offset: 0x00155E0A
		public void SetOrganizationRelationship(ObjectId identity, SessionParameters parameters)
		{
			parameters.Set("Identity", identity.ToString());
			this.RemotePowershellSession.RunCommand("Set-OrganizationRelationship", parameters);
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x00157C30 File Offset: 0x00155E30
		public IOrganizationConfig GetOrganizationConfig()
		{
			Microsoft.Exchange.Data.Directory.Management.OrganizationConfig organizationConfig = this.RemotePowershellSession.RunOneCommandSingleResult<Microsoft.Exchange.Data.Directory.Management.OrganizationConfig>("Get-OrganizationConfig", null, false);
			if (organizationConfig != null)
			{
				return new Microsoft.Exchange.Management.Hybrid.Entity.OrganizationConfig
				{
					Name = organizationConfig.Name,
					Guid = organizationConfig.Guid,
					AdminDisplayVersion = organizationConfig.AdminDisplayVersion,
					IsUpgradingOrganization = organizationConfig.IsUpgradingOrganization,
					OrganizationConfigHash = organizationConfig.OrganizationConfigHash,
					IsDehydrated = organizationConfig.IsDehydrated
				};
			}
			return null;
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x00157CA4 File Offset: 0x00155EA4
		public IFederationInformation BuildFederationInformation(string targetApplicationUri, string targetAutodiscoverEpr)
		{
			return new FederationInformation
			{
				TargetApplicationUri = targetApplicationUri,
				TargetAutodiscoverEpr = targetAutodiscoverEpr
			};
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x00157CC6 File Offset: 0x00155EC6
		public void Dispose()
		{
			if (this.RemotePowershellSession != null)
			{
				this.RemotePowershellSession.Dispose();
				this.RemotePowershellSession = null;
			}
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x00157CE2 File Offset: 0x00155EE2
		private bool ShouldInvokePowershellCommandHandler(string command)
		{
			return this.ShouldInvokePowershellCommand == null || this.ShouldInvokePowershellCommand(command);
		}

		// Token: 0x040030A0 RID: 12448
		protected const string Set_FederatedOrganizationIdentifier = "Set-FederatedOrganizationIdentifier";

		// Token: 0x040030A1 RID: 12449
		protected const string Get_FederationTrust = "Get-FederationTrust";

		// Token: 0x040030A2 RID: 12450
		private const string Get_AcceptedDomain = "Get-AcceptedDomain";

		// Token: 0x040030A3 RID: 12451
		private const string Get_FederatedOrganizationIdentifier = "Get-FederatedOrganizationIdentifier";

		// Token: 0x040030A4 RID: 12452
		private const string Get_FederationInformation = "Get-FederationInformation";

		// Token: 0x040030A5 RID: 12453
		private const string Remove_OrganizationRelationship = "Remove-OrganizationRelationship";

		// Token: 0x040030A6 RID: 12454
		private const string New_OrganizationRelationship = "New-OrganizationRelationship";

		// Token: 0x040030A7 RID: 12455
		private const string Get_OrganizationRelationship = "Get-OrganizationRelationship";

		// Token: 0x040030A8 RID: 12456
		private const string Get_OrganizationConfig = "Get-OrganizationConfig";

		// Token: 0x040030A9 RID: 12457
		private const string Get_RemoteDomain = "Get-RemoteDomain";

		// Token: 0x040030AA RID: 12458
		private const string Remove_RemoteDomain = "Remove-RemoteDomain";

		// Token: 0x040030AB RID: 12459
		private const string Set_OrganizationRelationship = "Set-OrganizationRelationship";
	}
}
