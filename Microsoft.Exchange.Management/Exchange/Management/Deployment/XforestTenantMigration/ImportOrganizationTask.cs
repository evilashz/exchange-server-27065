using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D86 RID: 3462
	[Cmdlet("Import", "Organization", DefaultParameterSetName = "Identity")]
	public sealed class ImportOrganizationTask : GetTaskBase<ExchangeConfigurationUnit>
	{
		// Token: 0x17002955 RID: 10581
		// (get) Token: 0x060084F7 RID: 34039 RVA: 0x0021FF1E File Offset: 0x0021E11E
		// (set) Token: 0x060084F8 RID: 34040 RVA: 0x0021FF35 File Offset: 0x0021E135
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OrganizationData Data
		{
			get
			{
				return (OrganizationData)base.Fields["Data"];
			}
			set
			{
				base.Fields["Data"] = value;
			}
		}

		// Token: 0x17002956 RID: 10582
		// (get) Token: 0x060084F9 RID: 34041 RVA: 0x0021FF48 File Offset: 0x0021E148
		// (set) Token: 0x060084FA RID: 34042 RVA: 0x0021FF5F File Offset: 0x0021E15F
		[Parameter(Mandatory = true, ParameterSetName = "Identity")]
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

		// Token: 0x17002957 RID: 10583
		// (get) Token: 0x060084FB RID: 34043 RVA: 0x0021FF72 File Offset: 0x0021E172
		private ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x17002958 RID: 10584
		// (get) Token: 0x060084FC RID: 34044 RVA: 0x0021FF7A File Offset: 0x0021E17A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002959 RID: 10585
		// (get) Token: 0x060084FD RID: 34045 RVA: 0x0021FF80 File Offset: 0x0021E180
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

		// Token: 0x060084FE RID: 34046 RVA: 0x0021FFBC File Offset: 0x0021E1BC
		protected override void InternalProcessRecord()
		{
			if (this.Data == null)
			{
				throw new ArgumentNullException("Data");
			}
			TaskLogger.LogEnter();
			this.directoryInfo = new DirectoryBindingInfo((NetworkCredential)this.Credential);
			DirectoryObjectCollection organizationalUnit = this.Data.OrganizationalUnit;
			organizationalUnit.AddRange(this.Data.ConfigurationUnit);
			OrganizationConfigurationTree organizationConfigurationTree = OrganizationMigrationManager.CalculateImportOrder(organizationalUnit);
			organizationConfigurationTree.WriteVerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose);
			organizationConfigurationTree.WriteErrorDelegate = new Task.TaskErrorLoggingDelegate(base.WriteError);
			organizationConfigurationTree.WriteWarningDelegate = new Task.TaskWarningLoggingDelegate(this.WriteWarning);
			OrganizationMigrationManager.UpdateDirectoryObjectProperties(organizationalUnit, this.directoryInfo);
			OrganizationMigrationManager.Import(this.directoryInfo, organizationalUnit, organizationConfigurationTree, this.Data.SourceFqdn, this.directoryInfo.Credential.Domain, this.Data.RootOrgName, base.RootOrgContainerId.ToString(), this.Data.SourceADSite, this.site.Name);
			TaskLogger.LogExit();
		}

		// Token: 0x060084FF RID: 34047 RVA: 0x002200B7 File Offset: 0x0021E2B7
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x06008500 RID: 34048 RVA: 0x002200C8 File Offset: 0x0021E2C8
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, this.SessionSettings, 147, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ImportOrganizationTask.cs");
			this.site = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.FullyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 153, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TenantMigration\\ImportOrganizationTask.cs").GetLocalSite();
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x00220126 File Offset: 0x0021E326
		protected override void InternalStateReset()
		{
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			base.InternalStateReset();
		}

		// Token: 0x04004046 RID: 16454
		private ADSessionSettings sessionSettings;

		// Token: 0x04004047 RID: 16455
		private ADSite site;

		// Token: 0x04004048 RID: 16456
		private DirectoryBindingInfo directoryInfo;
	}
}
