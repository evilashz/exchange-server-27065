using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B6 RID: 2230
	[Cmdlet("New", "DatabaseAvailabilityGroupNetwork", SupportsShouldProcess = true)]
	public sealed class NewDatabaseAvailabilityGroupNetwork : NewTenantADTaskBase<DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x00147F13 File Offset: 0x00146113
		// (set) Token: 0x06004EF4 RID: 20212 RVA: 0x00147F20 File Offset: 0x00146120
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x00147F2E File Offset: 0x0014612E
		// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x00147F45 File Offset: 0x00146145
		[Parameter(Mandatory = true, Position = 1)]
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

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00147F58 File Offset: 0x00146158
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x00147F6F File Offset: 0x0014616F
		[Parameter]
		public DatabaseAvailabilityGroupSubnetId[] Subnets
		{
			get
			{
				return (DatabaseAvailabilityGroupSubnetId[])base.Fields["Subnets"];
			}
			set
			{
				base.Fields["Subnets"] = value;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00147F82 File Offset: 0x00146182
		// (set) Token: 0x06004EFA RID: 20218 RVA: 0x00147F8F File Offset: 0x0014618F
		[Parameter]
		public string Description
		{
			get
			{
				return this.DataObject.Description;
			}
			set
			{
				this.DataObject.Description = value;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x00147F9D File Offset: 0x0014619D
		// (set) Token: 0x06004EFC RID: 20220 RVA: 0x00147FAA File Offset: 0x001461AA
		[Parameter]
		public bool ReplicationEnabled
		{
			get
			{
				return this.DataObject.ReplicationEnabled;
			}
			set
			{
				this.DataObject.ReplicationEnabled = value;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x00147FB8 File Offset: 0x001461B8
		// (set) Token: 0x06004EFE RID: 20222 RVA: 0x00147FC5 File Offset: 0x001461C5
		[Parameter]
		public bool IgnoreNetwork
		{
			get
			{
				return this.DataObject.IgnoreNetwork;
			}
			set
			{
				this.DataObject.IgnoreNetwork = value;
			}
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x00147FD3 File Offset: 0x001461D3
		protected override bool IsKnownException(Exception e)
		{
			return DagTaskHelper.IsKnownException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x00147FE8 File Offset: 0x001461E8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				DatabaseAvailabilityGroupNetwork dataObject = this.DataObject;
				return Strings.ConfirmationMessageNewDatabaseAvailabilityGroupNetwork(dataObject.Name);
			}
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x00148007 File Offset: 0x00146207
		private IConfigurationSession SetupAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 129, "SetupAdSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\NewDatabaseAvailabilityGroupNetwork.cs");
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0014802C File Offset: 0x0014622C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession adSession = this.SetupAdSession();
			return new DagNetworkConfigDataProvider(adSession, null, null);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x0014804C File Offset: 0x0014624C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			DatabaseAvailabilityGroupNetwork dataObject = this.DataObject;
			IConfigurationSession configSession = this.SetupAdSession();
			DatabaseAvailabilityGroup databaseAvailabilityGroup = DagTaskHelper.DagIdParameterToDag(this.DatabaseAvailabilityGroup, configSession);
			DagTaskHelper.VerifyDagAndServersAreWithinScopes<DatabaseAvailabilityGroupNetwork>(this, databaseAvailabilityGroup, true);
			if (databaseAvailabilityGroup.IsDagEmpty())
			{
				base.WriteError(new DagNetworkEmptyDagException(databaseAvailabilityGroup.Name), ErrorCategory.InvalidArgument, null);
			}
			DagTaskHelper.PreventTaskWhenAutoNetConfigIsEnabled(databaseAvailabilityGroup, this);
			DagNetworkObjectId identity = new DagNetworkObjectId(databaseAvailabilityGroup.Name, dataObject.Name);
			dataObject.SetIdentity(identity);
			DagNetworkConfigDataProvider dagNetworkConfigDataProvider = (DagNetworkConfigDataProvider)base.DataSession;
			DagNetworkConfiguration dagNetworkConfiguration = dagNetworkConfigDataProvider.ReadNetConfig(databaseAvailabilityGroup);
			DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork = dagNetworkConfiguration.FindNetwork(dataObject.Name);
			if (databaseAvailabilityGroupNetwork != null)
			{
				throw new DagNetworkManagementException(ServerStrings.DagNetworkCreateDupName(dataObject.Name));
			}
			DagNetworkValidation.ValidateSwitches(dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (base.Fields["Subnets"] != null)
			{
				DagNetworkValidation.ValidateSubnets(this.Subnets, dagNetworkConfiguration, this.Name, null, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				dataObject.ReplaceSubnets(this.Subnets);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x020008B7 RID: 2231
		private static class ParameterNames
		{
			// Token: 0x04002EE0 RID: 12000
			public const string Subnets = "Subnets";

			// Token: 0x04002EE1 RID: 12001
			public const string DagId = "DatabaseAvailabilityGroup";
		}
	}
}
