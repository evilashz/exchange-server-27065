using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002DC RID: 732
	[Cmdlet("install", "ConfigurationContainer")]
	public sealed class InstallConfigurationContainerTask : InstallContainerTaskBase<ExchangeConfigurationUnit>
	{
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00071696 File Offset: 0x0006F896
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x0007169E File Offset: 0x0006F89E
		private new string[] Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x000716A7 File Offset: 0x0006F8A7
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x000716BE File Offset: 0x0006F8BE
		[Parameter(Mandatory = true)]
		public string ParentContainerDN
		{
			get
			{
				return (string)base.Fields[InstallConfigurationContainerTask.ParentContainerField];
			}
			set
			{
				base.Fields[InstallConfigurationContainerTask.ParentContainerField] = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x000716D1 File Offset: 0x0006F8D1
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x000716E8 File Offset: 0x0006F8E8
		[Parameter(Mandatory = true)]
		public ADOrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (ADOrganizationalUnitIdParameter)base.Fields[InstallConfigurationContainerTask.OrganizationalUnitField];
			}
			set
			{
				base.Fields[InstallConfigurationContainerTask.OrganizationalUnitField] = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x000716FB File Offset: 0x0006F8FB
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x00071712 File Offset: 0x0006F912
		[Parameter(Mandatory = false)]
		public string ProgramId
		{
			get
			{
				return (string)base.Fields["TenantProgramId"];
			}
			set
			{
				base.Fields["TenantProgramId"] = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00071725 File Offset: 0x0006F925
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x0007173C File Offset: 0x0006F93C
		[Parameter(Mandatory = false)]
		public string OfferId
		{
			get
			{
				return (string)base.Fields["TenantOfferId"];
			}
			set
			{
				base.Fields["TenantOfferId"] = value;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0007174F File Offset: 0x0006F94F
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x00071766 File Offset: 0x0006F966
		[Parameter(Mandatory = false)]
		public string ServicePlan
		{
			get
			{
				return (string)base.Fields["TenantServicePlan"];
			}
			set
			{
				base.Fields["TenantServicePlan"] = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00071779 File Offset: 0x0006F979
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x000717A3 File Offset: 0x0006F9A3
		[Parameter(Mandatory = false)]
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				if (string.IsNullOrEmpty(this.DataObject.ExternalDirectoryOrganizationId))
				{
					return Guid.Empty;
				}
				return new Guid(this.DataObject.ExternalDirectoryOrganizationId);
			}
			set
			{
				this.DataObject.ExternalDirectoryOrganizationId = value.ToString();
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000717BD File Offset: 0x0006F9BD
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x000717CA File Offset: 0x0006F9CA
		[Parameter(Mandatory = false)]
		public bool IsDirSyncRunning
		{
			get
			{
				return this.DataObject.IsDirSyncRunning;
			}
			set
			{
				this.DataObject.IsDirSyncRunning = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x000717D8 File Offset: 0x0006F9D8
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x000717E5 File Offset: 0x0006F9E5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncStatus
		{
			get
			{
				return this.DataObject.DirSyncStatus;
			}
			set
			{
				this.DataObject.DirSyncStatus = value;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000717F3 File Offset: 0x0006F9F3
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x00071800 File Offset: 0x0006FA00
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> CompanyTags
		{
			get
			{
				return this.DataObject.CompanyTags;
			}
			set
			{
				this.DataObject.CompanyTags = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0007180E File Offset: 0x0006FA0E
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x0007181B File Offset: 0x0006FA1B
		[Parameter(Mandatory = false)]
		public string Location
		{
			get
			{
				return this.DataObject.Location;
			}
			set
			{
				this.DataObject.Location = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x00071829 File Offset: 0x0006FA29
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x00071836 File Offset: 0x0006FA36
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return this.DataObject.PersistedCapabilities;
			}
			set
			{
				this.DataObject.PersistedCapabilities = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00071844 File Offset: 0x0006FA44
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x00071865 File Offset: 0x0006FA65
		[Parameter(Mandatory = false)]
		public bool CreateSharedConfiguration
		{
			get
			{
				return (bool)(base.Fields[InstallConfigurationContainerTask.CreateSharedConfigurationField] ?? true);
			}
			set
			{
				base.Fields[InstallConfigurationContainerTask.CreateSharedConfigurationField] = value;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00071880 File Offset: 0x0006FA80
		public InstallConfigurationContainerTask()
		{
			this.Name = new string[]
			{
				InstallConfigurationContainerTask.ConfigurationContainerName
			};
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000718B4 File Offset: 0x0006FAB4
		protected override IConfigDataProvider CreateSession()
		{
			base.CreateSession();
			return DirectorySessionFactory.Default.CreateTenantConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsObjectId(this.GetBaseContainer()), 194, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallTenantConfigurationContainerTask.cs");
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000718FA File Offset: 0x0006FAFA
		protected override ADObjectId GetBaseContainer()
		{
			return new ADObjectId(this.ParentContainerDN);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00071908 File Offset: 0x0006FB08
		protected override void InternalProcessRecord()
		{
			IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
			string linkResolutionServer = configurationSession.LinkResolutionServer;
			configurationSession.LinkResolutionServer = this.ouOriginatingServer;
			try
			{
				base.InternalProcessRecord();
				ExchangeConfigurationUnit dataObject = this.DataObject;
				dataObject.OrganizationalUnitLink = this.ou.Id;
				configurationSession.Save(dataObject);
				this.ou.ConfigurationUnit = dataObject.ConfigurationUnit;
				bool useConfigNC = configurationSession.UseConfigNC;
				configurationSession.UseConfigNC = false;
				try
				{
					configurationSession.Save(this.ou);
				}
				finally
				{
					configurationSession.UseConfigNC = useConfigNC;
				}
			}
			finally
			{
				configurationSession.LinkResolutionServer = linkResolutionServer;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000719B4 File Offset: 0x0006FBB4
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
			bool useConfigNC = configurationSession.UseConfigNC;
			configurationSession.UseConfigNC = false;
			try
			{
				this.ou = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.OrganizationalUnit, configurationSession, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.OrganizationalUnit.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(this.OrganizationalUnit.ToString())));
				this.ouOriginatingServer = this.ou.OriginatingServer;
			}
			finally
			{
				configurationSession.UseConfigNC = useConfigNC;
			}
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.PrepareDataObject();
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 290, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallTenantConfigurationContainerTask.cs");
			string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
			{
				topologyConfigurationSession.GetAdministrativeGroup().LegacyExchangeDN,
				this.GetRelativeDNTillConfigurationUnits(exchangeConfigurationUnit.Id)
			});
			exchangeConfigurationUnit.Name = exchangeConfigurationUnit.Id.Rdn.UnescapedName;
			exchangeConfigurationUnit.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, exchangeConfigurationUnit);
			exchangeConfigurationUnit.OrganizationStatus = OrganizationStatus.PendingCompletion;
			if (!string.IsNullOrEmpty(this.ServicePlan))
			{
				exchangeConfigurationUnit.ServicePlan = this.ServicePlan;
			}
			if (!string.IsNullOrEmpty(this.ProgramId) && !string.IsNullOrEmpty(this.OfferId))
			{
				exchangeConfigurationUnit.ResellerId = string.Format("{0}.{1}", this.ProgramId, this.OfferId);
				if (this.CreateSharedConfiguration)
				{
					this.DataObject.SharedConfigurationInfo = SharedConfigurationInfo.FromInstalledVersion(this.ProgramId, this.OfferId);
				}
			}
			if (string.IsNullOrEmpty(exchangeConfigurationUnit.ExternalDirectoryOrganizationId))
			{
				exchangeConfigurationUnit.ExternalDirectoryOrganizationId = Guid.NewGuid().ToString();
			}
			return exchangeConfigurationUnit;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00071B78 File Offset: 0x0006FD78
		private string GetRelativeDNTillConfigurationUnits(ADObjectId newTenant)
		{
			ADObjectId parent = newTenant.Parent;
			string text = string.Empty;
			while (parent != null && !string.Equals(parent.Name, ADObject.ConfigurationUnits, StringComparison.OrdinalIgnoreCase))
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}/cn={1}", new object[]
				{
					text,
					parent.Name
				});
				parent = parent.Parent;
			}
			return text;
		}

		// Token: 0x04000B0C RID: 2828
		private static readonly string ConfigurationContainerName = "Configuration";

		// Token: 0x04000B0D RID: 2829
		private static readonly string ParentContainerField = "ParentContainer";

		// Token: 0x04000B0E RID: 2830
		private static readonly string OrganizationalUnitField = "OrganizationalUnit";

		// Token: 0x04000B0F RID: 2831
		private static readonly string CreateSharedConfigurationField = "CreateSharedConfiguration";

		// Token: 0x04000B10 RID: 2832
		private string ouOriginatingServer = string.Empty;

		// Token: 0x04000B11 RID: 2833
		private ADOrganizationalUnit ou;
	}
}
