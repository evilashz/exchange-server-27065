using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008C3 RID: 2243
	[Cmdlet("Set", "DatabaseAvailabilityGroupConfiguration", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetDatabaseAvailabilityGroupConfiguration : SetTopologySystemConfigurationObjectTask<DatabaseAvailabilityGroupConfigurationIdParameter, DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x0014CAF6 File Offset: 0x0014ACF6
		// (set) Token: 0x06004FA7 RID: 20391 RVA: 0x0014CB0D File Offset: 0x0014AD0D
		[Parameter(Mandatory = false)]
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

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x0014CB25 File Offset: 0x0014AD25
		// (set) Token: 0x06004FA9 RID: 20393 RVA: 0x0014CB3C File Offset: 0x0014AD3C
		[Parameter(Mandatory = false)]
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

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06004FAA RID: 20394 RVA: 0x0014CB54 File Offset: 0x0014AD54
		// (set) Token: 0x06004FAB RID: 20395 RVA: 0x0014CB6B File Offset: 0x0014AD6B
		[Parameter(Mandatory = false)]
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

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x0014CB83 File Offset: 0x0014AD83
		// (set) Token: 0x06004FAD RID: 20397 RVA: 0x0014CB9A File Offset: 0x0014AD9A
		[Parameter(Mandatory = false)]
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

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x0014CBB2 File Offset: 0x0014ADB2
		// (set) Token: 0x06004FAF RID: 20399 RVA: 0x0014CBC9 File Offset: 0x0014ADC9
		[Parameter(Mandatory = false)]
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

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0014CBE4 File Offset: 0x0014ADE4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			DatabaseAvailabilityGroupConfiguration databaseAvailabilityGroupConfiguration = (DatabaseAvailabilityGroupConfiguration)base.PrepareDataObject();
			DagConfigurationHelper dagConfigurationHelper = DagConfigurationHelper.Deserialize(databaseAvailabilityGroupConfiguration.ConfigurationXML);
			if (dagConfigurationHelper.Version <= 1)
			{
				if (base.Fields["ServersPerDag"] != null)
				{
					dagConfigurationHelper.ServersPerDag = this.ServersPerDag;
				}
				if (base.Fields["DatabasesPerServer"] != null)
				{
					dagConfigurationHelper.DatabasesPerServer = this.DatabasesPerServer;
				}
				if (base.Fields["DatabasesPerVolume"] != null)
				{
					dagConfigurationHelper.DatabasesPerVolume = this.DatabasesPerVolume;
				}
				if (base.Fields["CopiesPerDatabase"] != null)
				{
					dagConfigurationHelper.CopiesPerDatabase = this.CopiesPerDatabase;
				}
				if (base.Fields["MinCopiesPerDatabaseForMonitoring"] != null)
				{
					dagConfigurationHelper.MinCopiesPerDatabaseForMonitoring = this.MinCopiesPerDatabaseForMonitoring;
				}
				this.m_configXML = dagConfigurationHelper.Serialize();
				databaseAvailabilityGroupConfiguration.ConfigurationXML = this.m_configXML;
				TaskLogger.LogExit();
				return databaseAvailabilityGroupConfiguration;
			}
			throw new DagConfigVersionConflictException(databaseAvailabilityGroupConfiguration.Name, 1, dagConfigurationHelper.Version);
		}

		// Token: 0x04002F27 RID: 12071
		private string m_configXML;
	}
}
