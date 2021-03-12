using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200089E RID: 2206
	[Cmdlet("Get", "DatabaseAvailabilityGroupNetwork")]
	public sealed class GetDatabaseAvailabilityGroupNetwork : GetTenantADObjectWithIdentityTaskBase<DatabaseAvailabilityGroupNetworkIdParameter, DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x00142025 File Offset: 0x00140225
		// (set) Token: 0x06004D70 RID: 19824 RVA: 0x0014203C File Offset: 0x0014023C
		[Parameter]
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

		// Token: 0x06004D71 RID: 19825 RVA: 0x0014204F File Offset: 0x0014024F
		protected override bool IsKnownException(Exception e)
		{
			return DagTaskHelper.IsKnownException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x00142063 File Offset: 0x00140263
		private IConfigurationSession SetupAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 63, "SetupAdSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\GetDatabaseAvailabilityGroupNetwork.cs");
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00142084 File Offset: 0x00140284
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession adSession = this.SetupAdSession();
			return new DagNetworkConfigDataProvider(adSession, this.m_targetServerName, this.m_dag);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x001420AC File Offset: 0x001402AC
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Server != null)
			{
				ITopologyConfigurationSession globalConfigSession = base.GlobalConfigSession;
				Server server = (Server)base.GetDataObject<Server>(this.Server, globalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				if (base.HasErrors || server == null)
				{
					return;
				}
				if (!server.IsE14OrLater)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorServerNotE14OrLater(this.Server.ToString())), ErrorCategory.InvalidOperation, this.Server);
				}
				if (!server.IsMailboxServer)
				{
					base.WriteError(server.GetServerRoleError(ServerRole.Mailbox), ErrorCategory.InvalidOperation, this.Server);
					return;
				}
				this.m_targetServerName = server.Fqdn;
				this.m_dag = null;
				ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
				if (databaseAvailabilityGroup == null)
				{
					base.WriteError(new ServerNotInDagException(server.Fqdn), ErrorCategory.InvalidData, null);
				}
				this.m_dag = globalConfigSession.Read<DatabaseAvailabilityGroup>(databaseAvailabilityGroup);
				if (this.m_dag == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(databaseAvailabilityGroup.ToString())), ErrorCategory.InvalidData, databaseAvailabilityGroup);
				}
			}
		}

		// Token: 0x04002E4B RID: 11851
		private string m_targetServerName;

		// Token: 0x04002E4C RID: 11852
		private DatabaseAvailabilityGroup m_dag;
	}
}
