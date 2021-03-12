using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A10 RID: 2576
	[Cmdlet("Get", "IntraOrganizationConfiguration")]
	public sealed class GetIntraOrganizationConfiguration : GetTaskBase<Server>
	{
		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x001858D7 File Offset: 0x00183AD7
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x001858DA File Offset: 0x00183ADA
		// (set) Token: 0x06005C69 RID: 23657 RVA: 0x001858F1 File Offset: 0x00183AF1
		[Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x06005C6A RID: 23658 RVA: 0x00185904 File Offset: 0x00183B04
		// (set) Token: 0x06005C6B RID: 23659 RVA: 0x0018591B File Offset: 0x00183B1B
		[Parameter(Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OnPremisesOrganizationIdParameter OrganizationGuid
		{
			get
			{
				return (OnPremisesOrganizationIdParameter)base.Fields["OrganizationGuid"];
			}
			set
			{
				base.Fields["OrganizationGuid"] = value;
			}
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x00185930 File Offset: 0x00183B30
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.CheckTopologyType();
			if (this.isRunningOnDatacenter && this.Organization != null)
			{
				string acceptedDomain = this.Organization.ToString();
				base.CurrentOrganizationId = OrganizationId.FromAcceptedDomain(acceptedDomain);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x0018597C File Offset: 0x00183B7C
		private void CheckTopologyType()
		{
			try
			{
				this.isRunningOnDatacenter = AcceptedDomainUtility.IsDatacenter;
			}
			catch (CannotDetermineExchangeModeException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x001859B4 File Offset: 0x00183BB4
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 142, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\IntraOrganizationConnector\\GetIntraOrganizationConfiguration.cs");
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x00185A07 File Offset: 0x00183C07
		protected override void InternalProcessRecord()
		{
			if (this.isRunningOnDatacenter)
			{
				this.SetOnlineConfiguration();
				return;
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x00185A28 File Offset: 0x00183C28
		private void SetOnlineConfiguration()
		{
			string uriString = Datacenter.IsGallatinDatacenter() ? GetIntraOrganizationConfiguration.GallatinAutoDiscoveryEndpoint : GetIntraOrganizationConfiguration.ProdAutoDiscoveryEndpoint;
			this.configuration.OnlineDiscoveryEndpoint = new Uri(uriString);
			IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
			AcceptedDomain[] source = configurationSession.Find<AcceptedDomain>(base.CurrentOrganizationId.ConfigurationUnit, QueryScope.SubTree, null, null, 0);
			List<AcceptedDomain> list = (from domain in source
			where domain.IsCoexistenceDomain
			select domain).ToList<AcceptedDomain>();
			if (list.Count == 1)
			{
				this.configuration.OnlineTargetAddress = list.First<AcceptedDomain>().DomainName.ToString();
			}
			else if (list.Count > 1)
			{
				base.ThrowTerminatingError(new MultipleCoexistenceDomainsFoundException(), ErrorCategory.InvalidData, null);
			}
			QueryFilter filter = null;
			if (this.OrganizationGuid != null)
			{
				Guid guid;
				if (Guid.TryParse(this.OrganizationGuid.ToString(), out guid))
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, OnPremisesOrganizationSchema.OrganizationGuid, guid);
				}
				else
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.InvalidOrganizationGuid(this.OrganizationGuid.ToString())), ErrorCategory.InvalidData, null);
				}
			}
			OnPremisesOrganization[] source2 = configurationSession.Find<OnPremisesOrganization>(base.CurrentOrganizationId.ConfigurationUnit, QueryScope.SubTree, filter, null, 0);
			int num = source2.Count<OnPremisesOrganization>();
			if (num == 1)
			{
				this.configuration.OnPremiseTargetAddresses = source2.First<OnPremisesOrganization>().HybridDomains;
				return;
			}
			if (num > 1)
			{
				base.ThrowTerminatingError(new MultipleOnPremisesOrganizationsFoundException(), ErrorCategory.ObjectNotFound, null);
				return;
			}
			this.WriteWarning(Strings.OnPremisesConfigurationObjectNotFound);
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x00185B9C File Offset: 0x00183D9C
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			if (!this.isRunningOnDatacenter)
			{
				if (!this.atLeastOneE15SP1CASFound)
				{
					base.ThrowTerminatingError(new NoCASE15SP1ServersFoundException(), ErrorCategory.ObjectNotFound, null);
				}
				if (this.configuration.OnPremiseWebServiceEndpoint == null)
				{
					base.ThrowTerminatingError(new NoWebServicesExternalUrlFoundException(), ErrorCategory.ObjectNotFound, null);
				}
				this.WriteWarning(Strings.DiscoveryEndpointWasConstructed(this.configuration.OnPremiseDiscoveryEndpoint.ToString()));
			}
			base.WriteResult(this.configuration);
			base.InternalEndProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x00185C30 File Offset: 0x00183E30
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			Server server = (Server)dataObject;
			bool flag = server.VersionNumber >= GetIntraOrganizationConfiguration.E15SP1MinVersion;
			bool flag2 = (server.CurrentServerRole & (ServerRole.Cafe | ServerRole.ClientAccess)) != ServerRole.None;
			bool hasMailboxRole = (server.CurrentServerRole & ServerRole.Mailbox) != ServerRole.None;
			this.SetConfigurationFlags(flag, flag2, hasMailboxRole);
			if (flag)
			{
				this.atLeastOneE15SP1CASFound = true;
			}
			if (flag2 && flag)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 282, "WriteResult", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\IntraOrganizationConnector\\GetIntraOrganizationConfiguration.cs");
				ADObjectId descendantId = server.Id.GetDescendantId("Protocols", "HTTP", new string[0]);
				ADWebServicesVirtualDirectory[] source = topologyConfigurationSession.Find<ADWebServicesVirtualDirectory>(descendantId, QueryScope.SubTree, null, null, 10);
				IEnumerable<ADWebServicesVirtualDirectory> source2 = (from v in source
				where v.ExternalUrl != null
				select v).ToList<ADWebServicesVirtualDirectory>();
				if (source2.Any<ADWebServicesVirtualDirectory>())
				{
					Uri externalUrl = source2.First<ADWebServicesVirtualDirectory>().ExternalUrl;
					if (this.configuration.OnPremiseWebServiceEndpoint != null)
					{
						if (string.Equals(externalUrl.ToString(), this.configuration.OnPremiseDiscoveryEndpoint.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							this.WriteWarning(Strings.MultipleWebServicesExternalUrlsFound);
						}
						return;
					}
					string scheme = externalUrl.Scheme;
					string authority = externalUrl.Authority;
					string uriString = string.Format("{0}://{1}/autodiscover/autodiscover.svc", scheme, authority);
					this.configuration.OnPremiseWebServiceEndpoint = externalUrl;
					this.configuration.OnPremiseDiscoveryEndpoint = new Uri(uriString);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x00185DC8 File Offset: 0x00183FC8
		private void SetConfigurationFlags(bool isAtLeastE15SP1, bool hasCasRole, bool hasMailboxRole)
		{
			if (isAtLeastE15SP1)
			{
				if (this.configuration.DeploymentIsCompleteIOCReady == null)
				{
					this.configuration.DeploymentIsCompleteIOCReady = new bool?(true);
				}
				if (hasCasRole && this.configuration.HasNonIOCReadyExchangeCASServerVersions == null)
				{
					this.configuration.HasNonIOCReadyExchangeCASServerVersions = new bool?(false);
				}
				if (hasMailboxRole && this.configuration.HasNonIOCReadyExchangeMailboxServerVersions == null)
				{
					this.configuration.HasNonIOCReadyExchangeMailboxServerVersions = new bool?(false);
					return;
				}
			}
			else
			{
				this.configuration.DeploymentIsCompleteIOCReady = new bool?(false);
				if (hasCasRole)
				{
					this.configuration.HasNonIOCReadyExchangeCASServerVersions = new bool?(true);
				}
				if (hasMailboxRole)
				{
					this.configuration.HasNonIOCReadyExchangeMailboxServerVersions = new bool?(true);
				}
			}
		}

		// Token: 0x0400345F RID: 13407
		private static readonly string ProdAutoDiscoveryEndpoint = "https://autodiscover-s.outlook.com/autodiscover/autodiscover.svc";

		// Token: 0x04003460 RID: 13408
		private static readonly string GallatinAutoDiscoveryEndpoint = "https://autodiscover-s.partner.outlook.cn/autodiscover/autodiscover.svc";

		// Token: 0x04003461 RID: 13409
		private static readonly int E15SP1MinVersion = new ServerVersion(15, 0, 847, 0).ToInt();

		// Token: 0x04003462 RID: 13410
		private IntraOrganizationConfiguration configuration = new IntraOrganizationConfiguration();

		// Token: 0x04003463 RID: 13411
		private bool atLeastOneE15SP1CASFound;

		// Token: 0x04003464 RID: 13412
		private bool isRunningOnDatacenter;
	}
}
