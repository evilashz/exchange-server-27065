using System;
using System.Management.Automation;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalanceClient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MailboxLoadBalance
{
	// Token: 0x02000471 RID: 1137
	[Cmdlet("Get", "CapacitySummary", DefaultParameterSetName = "ForestSet")]
	public sealed class GetCapacitySummary : DataAccessTask<CapacitySummary>
	{
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x0009E51E File Offset: 0x0009C71E
		// (set) Token: 0x06002829 RID: 10281 RVA: 0x0009E535 File Offset: 0x0009C735
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "DatabaseSet")]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x0009E548 File Offset: 0x0009C748
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x0009E55F File Offset: 0x0009C75F
		[Parameter(Mandatory = true, ParameterSetName = "DagSet")]
		[ValidateNotNull]
		public DatabaseAvailabilityGroupIdParameter DatabaseAvailabilityGroup
		{
			get
			{
				return (DatabaseAvailabilityGroupIdParameter)base.Fields["DatabaseAvailabilityGroup"];
			}
			set
			{
				base.Fields["DatabaseAvailabilityGroup"] = value;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x0009E572 File Offset: 0x0009C772
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x0009E598 File Offset: 0x0009C798
		[Parameter(Mandatory = false, ParameterSetName = "ForestSet")]
		public SwitchParameter Forest
		{
			get
			{
				return (SwitchParameter)(base.Fields["Forest"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Forest"] = value;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x0009E5B0 File Offset: 0x0009C7B0
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x0009E5D6 File Offset: 0x0009C7D6
		[Parameter(Mandatory = false, ParameterSetName = "ServerSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DatabaseSet")]
		public SwitchParameter Refresh
		{
			get
			{
				return (SwitchParameter)(base.Fields["Refresh"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Refresh"] = value;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0009E5EE File Offset: 0x0009C7EE
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x0009E605 File Offset: 0x0009C805
		[Parameter(Mandatory = true, ParameterSetName = "ServerSet")]
		[ValidateNotNull]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0009E618 File Offset: 0x0009C818
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 193, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxLoadBalance\\GetCapacitySummary.cs");
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0009E648 File Offset: 0x0009C848
		protected override void InternalProcessRecord()
		{
			using (LoadBalanceAnchorContext loadBalanceAnchorContext = new LoadBalanceAnchorContext())
			{
				using (CmdletLogAdapter cmdletLogAdapter = new CmdletLogAdapter(loadBalanceAnchorContext.Logger, new Action<LocalizedString>(base.WriteVerbose), new Action<LocalizedString>(this.WriteWarning), new Action<LocalizedString>(base.WriteDebug)))
				{
					ILoadBalanceServicePort loadBalanceServicePort = LoadBalanceServiceAdapter.Create(cmdletLogAdapter);
					CapacitySummary capacitySummary = loadBalanceServicePort.GetCapacitySummary(this.objectIdentity, this.Refresh);
					base.WriteObject(capacitySummary);
				}
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0009E6E8 File Offset: 0x0009C8E8
		protected override void InternalValidate()
		{
			if (this.Database != null)
			{
				this.AssignDirectoryIdentity<MailboxDatabase>(this.Database, DirectoryObjectType.Database, Strings.ErrorDatabaseNotFound(this.Database.RawIdentity), Strings.ErrorDatabaseNotUnique(this.Database.RawIdentity));
				return;
			}
			if (this.Server != null)
			{
				this.AssignDirectoryIdentity<Server>(this.Server, DirectoryObjectType.Server, Strings.ErrorServerNotFound(this.Server.RawIdentity), Strings.ErrorServerNotUnique(this.Server.RawIdentity));
				return;
			}
			if (this.DatabaseAvailabilityGroup != null)
			{
				this.AssignDirectoryIdentity<DatabaseAvailabilityGroup>(this.DatabaseAvailabilityGroup, DirectoryObjectType.DatabaseAvailabilityGroup, Strings.ErrorDagNotFound(this.DatabaseAvailabilityGroup.RawIdentity), Strings.ErrorDagNotUnique(this.DatabaseAvailabilityGroup.RawIdentity));
				return;
			}
			this.objectIdentity = DirectoryIdentity.CreateForestIdentity(string.Empty);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0009E7A8 File Offset: 0x0009C9A8
		private void AssignDirectoryIdentity<TObject>(IIdentityParameter identityParameter, DirectoryObjectType objectType, LocalizedString notFoundErrorMsg, LocalizedString ambiguousObjErrorMsg) where TObject : ADConfigurationObject, new()
		{
			ADConfigurationObject adconfigurationObject = (ADConfigurationObject)base.GetDataObject<TObject>(identityParameter, this.ConfigurationSession, this.RootId, new LocalizedString?(notFoundErrorMsg), new LocalizedString?(ambiguousObjErrorMsg));
			this.objectIdentity = DirectoryIdentity.CreateFromADObjectId(adconfigurationObject.Id, objectType);
		}

		// Token: 0x04001DD6 RID: 7638
		private const string DagSet = "DagSet";

		// Token: 0x04001DD7 RID: 7639
		private const string DatabaseAvailabilityGroupParameter = "DatabaseAvailabilityGroup";

		// Token: 0x04001DD8 RID: 7640
		private const string DatabaseSet = "DatabaseSet";

		// Token: 0x04001DD9 RID: 7641
		private const string ForestParameter = "Forest";

		// Token: 0x04001DDA RID: 7642
		private const string ForestSet = "ForestSet";

		// Token: 0x04001DDB RID: 7643
		private const string ParameterDatabase = "Database";

		// Token: 0x04001DDC RID: 7644
		private const string RefreshParameter = "Refresh";

		// Token: 0x04001DDD RID: 7645
		private const string ServerParameter = "Server";

		// Token: 0x04001DDE RID: 7646
		private const string ServerSet = "ServerSet";

		// Token: 0x04001DDF RID: 7647
		private const string TaskNoun = "CapacitySummary";

		// Token: 0x04001DE0 RID: 7648
		private DirectoryIdentity objectIdentity;
	}
}
