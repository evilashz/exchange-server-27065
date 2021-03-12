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
	// Token: 0x0200089D RID: 2205
	[Cmdlet("Get", "DatabaseAvailabilityGroupConfiguration")]
	[OutputType(new Type[]
	{
		typeof(DagConfigurationEntry)
	})]
	public sealed class GetDatabaseAvailabilityGroupConfiguration : GetSystemConfigurationObjectTask<DatabaseAvailabilityGroupConfigurationIdParameter, DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x06004D6A RID: 19818 RVA: 0x00141F0F File Offset: 0x0014010F
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 52, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\GetDatabaseAvailabilityGroupConfiguration.cs");
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x00141F3C File Offset: 0x0014013C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			DatabaseAvailabilityGroupConfiguration dagConfig = (DatabaseAvailabilityGroupConfiguration)dataObject;
			DagConfigurationEntry dagConfigurationEntry = this.ConstructNewDagConfigEntry(dagConfig);
			if (dagConfigurationEntry != null)
			{
				base.WriteResult(dagConfigurationEntry);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x00141F84 File Offset: 0x00140184
		private DagConfigurationEntry ConstructNewDagConfigEntry(DatabaseAvailabilityGroupConfiguration dagConfig)
		{
			DagConfigurationHelper dagConfigurationHelper = DagConfigurationHelper.Deserialize(dagConfig.ConfigurationXML);
			if (dagConfigurationHelper.Version <= 1)
			{
				return new DagConfigurationEntry
				{
					Name = dagConfig.Name,
					Identity = dagConfig.Identity,
					ServersPerDag = dagConfigurationHelper.ServersPerDag,
					DatabasesPerServer = dagConfigurationHelper.DatabasesPerServer,
					DatabasesPerVolume = dagConfigurationHelper.DatabasesPerVolume,
					CopiesPerDatabase = dagConfigurationHelper.CopiesPerDatabase,
					MinCopiesPerDatabaseForMonitoring = dagConfigurationHelper.MinCopiesPerDatabaseForMonitoring
				};
			}
			throw new DagConfigVersionConflictException(dagConfig.Name, 1, dagConfigurationHelper.Version);
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x0014201A File Offset: 0x0014021A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
