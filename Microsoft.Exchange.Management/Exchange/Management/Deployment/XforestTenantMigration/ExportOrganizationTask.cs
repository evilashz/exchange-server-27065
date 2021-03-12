using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D81 RID: 3457
	[Cmdlet("Export", "Organization", DefaultParameterSetName = "Identity")]
	public sealed class ExportOrganizationTask : GetTaskBase<ExchangeConfigurationUnit>
	{
		// Token: 0x1700293F RID: 10559
		// (get) Token: 0x060084B2 RID: 33970 RVA: 0x0021E18A File Offset: 0x0021C38A
		// (set) Token: 0x060084B3 RID: 33971 RVA: 0x0021E1A1 File Offset: 0x0021C3A1
		[Parameter(Mandatory = true, ParameterSetName = "CustomCredentials", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17002940 RID: 10560
		// (get) Token: 0x060084B4 RID: 33972 RVA: 0x0021E1B4 File Offset: 0x0021C3B4
		// (set) Token: 0x060084B5 RID: 33973 RVA: 0x0021E1BC File Offset: 0x0021C3BC
		[Parameter(Mandatory = true, ParameterSetName = "CustomCredentials")]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17002941 RID: 10561
		// (get) Token: 0x060084B6 RID: 33974 RVA: 0x0021E1C5 File Offset: 0x0021C3C5
		// (set) Token: 0x060084B7 RID: 33975 RVA: 0x0021E1DC File Offset: 0x0021C3DC
		[Parameter(Mandatory = true, ParameterSetName = "CustomCredentials")]
		public PSCredential Credential
		{
			get
			{
				return (PSCredential)base.Fields["Credential"];
			}
			set
			{
				base.Fields["Credential"] = value;
			}
		}

		// Token: 0x17002942 RID: 10562
		// (get) Token: 0x060084B8 RID: 33976 RVA: 0x0021E1EF File Offset: 0x0021C3EF
		private ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x17002943 RID: 10563
		// (get) Token: 0x060084B9 RID: 33977 RVA: 0x0021E1F7 File Offset: 0x0021C3F7
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002944 RID: 10564
		// (get) Token: 0x060084BA RID: 33978 RVA: 0x0021E1FC File Offset: 0x0021C3FC
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = new ExistsFilter(ExchangeConfigurationUnitSchema.OrganizationalUnitLink);
				if (base.InternalFilter != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						base.InternalFilter,
						queryFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x060084BB RID: 33979 RVA: 0x0021E238 File Offset: 0x0021C438
		protected override void InternalProcessRecord()
		{
			if (this.Identity == null)
			{
				throw new ArgumentNullException("Identity");
			}
			TaskLogger.LogEnter();
			LocalizedString? localizedString;
			IEnumerable<ExchangeConfigurationUnit> dataObjects = base.GetDataObjects<ExchangeConfigurationUnit>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, out localizedString);
			OrganizationId organizationId = null;
			using (IEnumerator<ExchangeConfigurationUnit> enumerator = dataObjects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ExchangeConfigurationUnit exchangeConfigurationUnit = enumerator.Current;
					organizationId = exchangeConfigurationUnit.OrganizationId;
					string originatingServer = exchangeConfigurationUnit.OriginatingServer;
				}
			}
			if (!base.HasErrors && organizationId == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ExchangeConfigurationUnit).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
			}
			this.directoryInfo = new DirectoryBindingInfo();
			string name = organizationId.ConfigurationUnit.ToString();
			string distinguishedName = organizationId.OrganizationalUnit.DistinguishedName;
			DirectoryObjectCollection orgUnit;
			using (SearchResultCollection subtree = this.GetSubtree(distinguishedName))
			{
				orgUnit = new DirectoryObjectCollection(distinguishedName, subtree);
			}
			string text = organizationId.ConfigurationUnit.DistinguishedName;
			ADObjectId adobjectId = new ADObjectId(text);
			text = adobjectId.AncestorDN(1).ToDNString();
			DirectoryObjectCollection configUnit;
			using (SearchResultCollection subtree2 = this.GetSubtree(text))
			{
				configUnit = new DirectoryObjectCollection(text, subtree2);
			}
			this.WriteResult(new OrganizationData(name, orgUnit, configUnit, base.RootOrgContainerId.ToString(), ADSession.GetDomainNamingContextForLocalForest().ToString(), this.site.Name));
			TaskLogger.LogExit();
		}

		// Token: 0x060084BC RID: 33980 RVA: 0x0021E41C File Offset: 0x0021C61C
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x060084BD RID: 33981 RVA: 0x0021E42C File Offset: 0x0021C62C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession;
			if (this.Credential == null && string.IsNullOrEmpty(this.DomainController))
			{
				tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, this.SessionSettings, 198, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ExportOrganizationTask.cs");
			}
			else if (this.Credential == null && !string.IsNullOrEmpty(this.DomainController))
			{
				tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, null, this.SessionSettings, 206, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ExportOrganizationTask.cs");
			}
			else if (this.Credential != null && string.IsNullOrEmpty(this.DomainController))
			{
				tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, this.Credential.GetNetworkCredential(), this.SessionSettings, 215, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ExportOrganizationTask.cs");
			}
			else
			{
				tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, this.Credential.GetNetworkCredential(), this.SessionSettings, 224, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ExportOrganizationTask.cs");
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 232, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ExportOrganizationTask.cs");
			this.site = topologyConfigurationSession.GetLocalSite();
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x060084BE RID: 33982 RVA: 0x0021E579 File Offset: 0x0021C779
		protected override void InternalStateReset()
		{
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			base.InternalStateReset();
		}

		// Token: 0x060084BF RID: 33983 RVA: 0x0021E5A8 File Offset: 0x0021C7A8
		private SearchResultCollection GetSubtree(string dn)
		{
			SearchResultCollection result;
			using (DirectoryEntry directoryEntry = this.directoryInfo.GetDirectoryEntry(this.directoryInfo.LdapBasePath + dn))
			{
				using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry))
				{
					directorySearcher.SearchScope = SearchScope.Subtree;
					directorySearcher.PageSize = int.MaxValue;
					result = directorySearcher.FindAll();
				}
			}
			return result;
		}

		// Token: 0x04004031 RID: 16433
		private ADSessionSettings sessionSettings;

		// Token: 0x04004032 RID: 16434
		private ADSite site;

		// Token: 0x04004033 RID: 16435
		private DirectoryBindingInfo directoryInfo;
	}
}
