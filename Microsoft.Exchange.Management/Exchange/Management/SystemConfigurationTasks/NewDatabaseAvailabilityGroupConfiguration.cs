using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B5 RID: 2229
	[Cmdlet("New", "DatabaseAvailabilityGroupConfiguration", SupportsShouldProcess = true)]
	public sealed class NewDatabaseAvailabilityGroupConfiguration : NewSystemConfigurationObjectTask<DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x00147BC0 File Offset: 0x00145DC0
		// (set) Token: 0x06004EE5 RID: 20197 RVA: 0x00147BD7 File Offset: 0x00145DD7
		[Parameter(Mandatory = true)]
		public int ServersPerDag
		{
			get
			{
				return (int)base.Fields["ServersPerDag"];
			}
			set
			{
				base.Fields["ServersPerDag"] = value;
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x00147BEF File Offset: 0x00145DEF
		// (set) Token: 0x06004EE7 RID: 20199 RVA: 0x00147C06 File Offset: 0x00145E06
		[Parameter(Mandatory = true)]
		public int DatabasesPerServer
		{
			get
			{
				return (int)base.Fields["DatabasesPerServer"];
			}
			set
			{
				base.Fields["DatabasesPerServer"] = value;
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x00147C1E File Offset: 0x00145E1E
		// (set) Token: 0x06004EE9 RID: 20201 RVA: 0x00147C35 File Offset: 0x00145E35
		[Parameter(Mandatory = true)]
		public int DatabasesPerVolume
		{
			get
			{
				return (int)base.Fields["DatabasesPerVolume"];
			}
			set
			{
				base.Fields["DatabasesPerVolume"] = value;
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06004EEA RID: 20202 RVA: 0x00147C4D File Offset: 0x00145E4D
		// (set) Token: 0x06004EEB RID: 20203 RVA: 0x00147C64 File Offset: 0x00145E64
		[Parameter(Mandatory = true)]
		public int CopiesPerDatabase
		{
			get
			{
				return (int)base.Fields["CopiesPerDatabase"];
			}
			set
			{
				base.Fields["CopiesPerDatabase"] = value;
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x00147C7C File Offset: 0x00145E7C
		// (set) Token: 0x06004EED RID: 20205 RVA: 0x00147C93 File Offset: 0x00145E93
		[Parameter(Mandatory = true)]
		public int MinCopiesPerDatabaseForMonitoring
		{
			get
			{
				return (int)base.Fields["MinCopiesPerDatabaseForMonitoring"];
			}
			set
			{
				base.Fields["MinCopiesPerDatabaseForMonitoring"] = value;
			}
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00147CAC File Offset: 0x00145EAC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.m_output = new HaTaskOutputHelper("new-databaseavailabiltygroupconfiguration", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_output.CreateTempLogFile();
			this.m_output.AppendLogMessage("new-dagconfiguration started", new object[0]);
			this.LogCommandLineParameters();
			this.m_dagConfigName = base.Name;
			DagConfigurationHelper dagConfigurationHelper = new DagConfigurationHelper(this.ServersPerDag, this.DatabasesPerServer, this.DatabasesPerVolume, this.CopiesPerDatabase, this.MinCopiesPerDatabaseForMonitoring);
			this.m_dagConfigXML = dagConfigurationHelper.Serialize();
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DatabaseAvailabilityGroupConfigurationSchema.Name, this.m_dagConfigName);
			DatabaseAvailabilityGroupConfiguration[] array = this.ConfigurationSession.Find<DatabaseAvailabilityGroupConfiguration>(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				base.WriteError(new ADObjectAlreadyExistsException(Strings.NewDagConfigurationErrorDuplicateName(this.m_dagConfigName)), ErrorCategory.InvalidArgument, this.m_dagConfigName);
			}
			base.InternalValidate();
			this.m_output.WriteVerbose(Strings.NewDagConfigurationPassedChecks);
			TaskLogger.LogExit();
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x00147DCC File Offset: 0x00145FCC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			this.m_output.WriteVerbose(Strings.NewDagConfigurationCompletedSuccessfully);
			this.m_output.CloseTempLogFile();
			TaskLogger.LogExit();
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x00147DFC File Offset: 0x00145FFC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			DatabaseAvailabilityGroupConfiguration databaseAvailabilityGroupConfiguration = new DatabaseAvailabilityGroupConfiguration();
			databaseAvailabilityGroupConfiguration.SetId(((ITopologyConfigurationSession)this.ConfigurationSession).GetDatabaseAvailabilityGroupContainerId().GetChildId(this.m_dagConfigName));
			databaseAvailabilityGroupConfiguration.Name = this.m_dagConfigName;
			databaseAvailabilityGroupConfiguration.ConfigurationXML = this.m_dagConfigXML;
			TaskLogger.LogExit();
			return databaseAvailabilityGroupConfiguration;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00147E54 File Offset: 0x00146054
		private void LogCommandLineParameters()
		{
			this.m_output.AppendLogMessage("commandline: {0}", new object[]
			{
				base.MyInvocation.Line
			});
			string[] array = new string[]
			{
				"Name",
				"ServersPerDag",
				"DatabasesPerServer",
				"DatabasesPerVolume",
				"CopiesPerDatabase",
				"MinCopiesPerDatabaseForMonitoring"
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

		// Token: 0x04002EDD RID: 11997
		private string m_dagConfigName;

		// Token: 0x04002EDE RID: 11998
		private string m_dagConfigXML;

		// Token: 0x04002EDF RID: 11999
		private HaTaskOutputHelper m_output;
	}
}
