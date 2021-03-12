using System;
using System.Management;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B3 RID: 2227
	[Cmdlet("New", "DatabaseAvailabilityGroup", SupportsShouldProcess = true)]
	public sealed class NewDatabaseAvailabilityGroup : NewSystemConfigurationObjectTask<DatabaseAvailabilityGroup>
	{
		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x00147576 File Offset: 0x00145776
		// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x0014758D File Offset: 0x0014578D
		[Parameter(Mandatory = false)]
		public FileShareWitnessServerName WitnessServer
		{
			get
			{
				return (FileShareWitnessServerName)base.Fields["WitnessServer"];
			}
			set
			{
				base.Fields["WitnessServer"] = value;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x001475A0 File Offset: 0x001457A0
		// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x001475B7 File Offset: 0x001457B7
		[Parameter(Mandatory = false)]
		public NonRootLocalLongFullPath WitnessDirectory
		{
			get
			{
				return (NonRootLocalLongFullPath)base.Fields["WitnessDirectory"];
			}
			set
			{
				base.Fields["WitnessDirectory"] = value;
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x001475CA File Offset: 0x001457CA
		// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x001475F5 File Offset: 0x001457F5
		[Parameter(Mandatory = false)]
		public ThirdPartyReplicationMode ThirdPartyReplication
		{
			get
			{
				if (base.Fields["ThirdPartyReplication"] == null)
				{
					return ThirdPartyReplicationMode.Disabled;
				}
				return (ThirdPartyReplicationMode)base.Fields["ThirdPartyReplication"];
			}
			set
			{
				base.Fields["ThirdPartyReplication"] = value;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x0014760D File Offset: 0x0014580D
		// (set) Token: 0x06004ED8 RID: 20184 RVA: 0x00147624 File Offset: 0x00145824
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public IPAddress[] DatabaseAvailabilityGroupIpAddresses
		{
			get
			{
				return (IPAddress[])base.Fields["DatabaseAvailabilityGroupIpAddresses"];
			}
			set
			{
				base.Fields["DatabaseAvailabilityGroupIpAddresses"] = value;
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x00147637 File Offset: 0x00145837
		// (set) Token: 0x06004EDA RID: 20186 RVA: 0x0014764E File Offset: 0x0014584E
		[Parameter(Mandatory = false)]
		public DatabaseAvailabilityGroupConfigurationIdParameter DagConfiguration
		{
			get
			{
				return (DatabaseAvailabilityGroupConfigurationIdParameter)base.Fields["DagConfiguration"];
			}
			set
			{
				base.Fields["DagConfiguration"] = value;
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x00147661 File Offset: 0x00145861
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewDatabaseAvailabilityGroup(base.Name.ToString());
			}
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0014767B File Offset: 0x0014587B
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 152, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\NewDatabaseAvailabilityGroup.cs");
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x001476A8 File Offset: 0x001458A8
		private void ValidateDagNameIsValidComputerName(string dagName)
		{
			bool flag = true;
			Regex regex = new Regex("^[-A-Za-z0-9]+$", RegexOptions.CultureInvariant);
			if (dagName.Length > 15)
			{
				flag = false;
			}
			else
			{
				Match match = regex.Match(dagName);
				if (!match.Success)
				{
					flag = false;
				}
			}
			if (!flag)
			{
				this.m_output.WriteError(new DagTaskDagNameMustBeComputerNameExceptionM1(dagName), ErrorCategory.InvalidArgument, null);
			}
			ITopologyConfigurationSession configSession = (ITopologyConfigurationSession)base.DataSession;
			bool flag3;
			bool flag2 = DagTaskHelper.DoesComputerAccountExist(configSession, dagName, out flag3);
			if (!flag2)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.GetHashCode(), "The computer account {0} does not exist.", dagName);
				return;
			}
			if (flag3)
			{
				this.m_output.WriteError(new DagTaskComputerAccountExistsAndIsEnabledException(dagName), ErrorCategory.InvalidArgument, null);
				return;
			}
			ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.GetHashCode(), "The computer account {0} exists, but is disabled.", dagName);
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x00147768 File Offset: 0x00145968
		private void LogCommandLineParameters()
		{
			this.m_output.AppendLogMessage("commandline: {0}", new object[]
			{
				base.MyInvocation.Line
			});
			string[] array = new string[]
			{
				"Name",
				"WitnessServer",
				"WitnessDirectory",
				"WhatIf"
			};
			foreach (string text in array)
			{
				this.m_output.AppendLogMessage("Option '{0}' = '{1}'.", new object[]
				{
					text,
					base.Fields[text]
				});
			}
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x00147810 File Offset: 0x00145A10
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.m_output = new HaTaskOutputHelper("new-databaseavailabiltygroup", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_output.CreateTempLogFile();
			this.m_output.AppendLogMessage("new-dag started", new object[0]);
			this.LogCommandLineParameters();
			if (this.DatabaseAvailabilityGroupIpAddresses != null)
			{
				foreach (IPAddress ipaddress in this.DatabaseAvailabilityGroupIpAddresses)
				{
					if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
					{
						this.m_output.WriteErrorSimple(new DagTaskDagIpAddressesMustBeIpv4Exception());
					}
				}
			}
			this.m_dagName = base.Name;
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DatabaseAvailabilityGroupSchema.Name, this.m_dagName);
			DatabaseAvailabilityGroup[] array = this.ConfigurationSession.Find<DatabaseAvailabilityGroup>(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				base.WriteError(new ADObjectAlreadyExistsException(Strings.NewDagErrorDuplicateName(this.m_dagName)), ErrorCategory.InvalidArgument, this.m_dagName);
			}
			this.ValidateDagNameIsValidComputerName(this.m_dagName);
			this.m_fsw = new FileShareWitness((ITopologyConfigurationSession)base.DataSession, this.m_dagName, this.WitnessServer, this.WitnessDirectory);
			try
			{
				this.m_fsw.Initialize();
			}
			catch (LocalizedException error)
			{
				this.m_output.WriteErrorSimple(error);
			}
			catch (ManagementException error2)
			{
				this.m_output.WriteErrorSimple(error2);
			}
			try
			{
				this.m_fsw.Create();
				if (this.m_fsw.IsJustCreated)
				{
					this.m_fsw.Delete();
				}
			}
			catch (LocalizedException ex)
			{
				if (this.m_fsw.GetExceptionType(ex) != FileShareWitnessExceptionType.FswDeleteError)
				{
					this.m_output.WriteWarning(ex.LocalizedString);
				}
			}
			base.InternalValidate();
			DagTaskHelper.VerifyDagIsWithinScopes<DatabaseAvailabilityGroup>(this, this.DataObject, false);
			this.m_output.WriteVerbose(Strings.NewDagPassedChecks);
			TaskLogger.LogExit();
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x00147A28 File Offset: 0x00145C28
		private void InitializeDagAdObject()
		{
			foreach (ADObjectId identity in this.m_newDag.Servers)
			{
				Server dagMemberServer = (Server)base.DataSession.Read<Server>(identity);
				DagTaskHelper.RevertDagServersDatabasesToStandalone(this.ConfigurationSession, this.m_output, dagMemberServer);
			}
			if (base.Fields["DatabaseAvailabilityGroupIpAddresses"] != null)
			{
				MultiValuedProperty<IPAddress> databaseAvailabilityGroupIpv4Addresses = new MultiValuedProperty<IPAddress>(this.DatabaseAvailabilityGroupIpAddresses);
				this.m_newDag.DatabaseAvailabilityGroupIpv4Addresses = databaseAvailabilityGroupIpv4Addresses;
			}
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x00147AC8 File Offset: 0x00145CC8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.ThirdPartyReplication == ThirdPartyReplicationMode.Enabled)
			{
				this.m_output.WriteWarning(Strings.WarningMessageNewTPRDag);
			}
			this.InitializeDagAdObject();
			base.InternalProcessRecord();
			this.m_output.WriteVerbose(Strings.NewDagCompletedSuccessfully);
			this.m_output.CloseTempLogFile();
			TaskLogger.LogExit();
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x00147B20 File Offset: 0x00145D20
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			DatabaseAvailabilityGroup databaseAvailabilityGroup = new DatabaseAvailabilityGroup();
			databaseAvailabilityGroup.SetId(((ITopologyConfigurationSession)this.ConfigurationSession).GetDatabaseAvailabilityGroupContainerId().GetChildId(base.Name));
			databaseAvailabilityGroup.Name = this.m_dagName;
			databaseAvailabilityGroup.SetWitnessServer(this.m_fsw.FileShareWitnessShare, this.m_fsw.WitnessDirectory);
			databaseAvailabilityGroup.ThirdPartyReplication = this.ThirdPartyReplication;
			if (this.DagConfiguration != null)
			{
				DatabaseAvailabilityGroupConfiguration databaseAvailabilityGroupConfiguration = DagConfigurationHelper.DagConfigIdParameterToDagConfig(this.DagConfiguration, this.ConfigurationSession);
				databaseAvailabilityGroup.DatabaseAvailabilityGroupConfiguration = databaseAvailabilityGroupConfiguration.Id;
			}
			this.m_newDag = databaseAvailabilityGroup;
			TaskLogger.LogExit();
			return databaseAvailabilityGroup;
		}

		// Token: 0x04002ED7 RID: 11991
		private FileShareWitness m_fsw;

		// Token: 0x04002ED8 RID: 11992
		private DatabaseAvailabilityGroup m_newDag;

		// Token: 0x04002ED9 RID: 11993
		private string m_dagName;

		// Token: 0x04002EDA RID: 11994
		private HaTaskOutputHelper m_output;

		// Token: 0x020008B4 RID: 2228
		private static class ParameterNames
		{
			// Token: 0x04002EDB RID: 11995
			public const string DatabaseAvailabilityGroupIpAddresses = "DatabaseAvailabilityGroupIpAddresses";

			// Token: 0x04002EDC RID: 11996
			public const string ThirdPartyReplication = "ThirdPartyReplication";
		}
	}
}
