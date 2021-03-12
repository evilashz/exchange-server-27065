using System;
using System.Management.Automation;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Replay.Monitoring.Client;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A0 RID: 2208
	public abstract class GetRedundancyTaskBase<TIdentity, TDataObject> : GetSystemConfigurationObjectTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x001435BD File Offset: 0x001417BD
		// (set) Token: 0x06004D97 RID: 19863 RVA: 0x001435D4 File Offset: 0x001417D4
		[Alias(new string[]
		{
			"Dag"
		})]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public DatabaseAvailabilityGroupIdParameter DatabaseAvailabilityGroup
		{
			get
			{
				return (DatabaseAvailabilityGroupIdParameter)base.Fields["Dag"];
			}
			set
			{
				base.Fields["Dag"] = value;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x001435E7 File Offset: 0x001417E7
		// (set) Token: 0x06004D99 RID: 19865 RVA: 0x001435FE File Offset: 0x001417FE
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ServerIdParameter ServerToContact
		{
			get
			{
				return (ServerIdParameter)base.Fields["ServerToContact"];
			}
			set
			{
				base.Fields["ServerToContact"] = value;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06004D9A RID: 19866 RVA: 0x00143611 File Offset: 0x00141811
		// (set) Token: 0x06004D9B RID: 19867 RVA: 0x00143633 File Offset: 0x00141833
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateRange(0, 2147483)]
		public int TimeoutInSeconds
		{
			get
			{
				return (int)(base.Fields["TimeoutInSeconds"] ?? 10);
			}
			set
			{
				base.Fields["TimeoutInSeconds"] = value;
			}
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x0014364B File Offset: 0x0014184B
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00143654 File Offset: 0x00141854
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Identity == null && this.DatabaseAvailabilityGroup == null)
			{
				base.WriteError(new InvalidParamSpecifyIdentityOrDagException(), ErrorCategory.InvalidArgument, null);
			}
			if (this.Identity != null)
			{
				TIdentity identity = this.Identity;
				if (identity.RawIdentity.Contains("*"))
				{
					base.WriteError(new InvalidParamIdentityHasWildcardException(), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x001436D0 File Offset: 0x001418D0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			DatabaseAvailabilityGroup databaseAvailabilityGroup = null;
			ITopologyConfigurationSession globalConfigSession = base.GlobalConfigSession;
			if (this.DatabaseAvailabilityGroup != null)
			{
				databaseAvailabilityGroup = this.LookupDag(this.DatabaseAvailabilityGroup);
			}
			else if (this.Identity != null)
			{
				ADObjectId adobjectId = this.LookupIdentityObjectAndGetDagId();
				databaseAvailabilityGroup = globalConfigSession.Read<DatabaseAvailabilityGroup>(adobjectId);
				if (databaseAvailabilityGroup == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorDagNotFound(adobjectId.Name)), ErrorCategory.InvalidData, adobjectId);
				}
			}
			string serverToQueryFqdn = this.DiscoverServerToQuery(this.ServerToContact, databaseAvailabilityGroup);
			HealthInfoPersisted dagHealthInfo = this.GetDagHealthInfo(serverToQueryFqdn);
			this.WriteResultsFromHealthInfo(dagHealthInfo, serverToQueryFqdn);
		}

		// Token: 0x06004D9F RID: 19871
		protected abstract ADObjectId LookupIdentityObjectAndGetDagId();

		// Token: 0x06004DA0 RID: 19872
		protected abstract void WriteResultsFromHealthInfo(HealthInfoPersisted hip, string serverToQueryFqdn);

		// Token: 0x06004DA1 RID: 19873 RVA: 0x00143804 File Offset: 0x00141A04
		protected HealthInfoPersisted GetDagHealthInfo(string serverToQueryFqdn)
		{
			HealthInfoPersisted hip = null;
			Exception ex = MonitoringServiceClient.HandleException(delegate
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)this.TimeoutInSeconds);
				using (MonitoringServiceClient monitoringServiceClient = MonitoringServiceClient.Open(serverToQueryFqdn, timeSpan, timeSpan, timeSpan, MonitoringServiceClient.ReceiveTimeout))
				{
					Task<HealthInfoPersisted> dagHealthInfoAsync = monitoringServiceClient.GetDagHealthInfoAsync();
					if (!dagHealthInfoAsync.Wait(timeSpan))
					{
						throw new TimeoutException(Strings.GetDagHealthInfoRequestTimedOut(this.TimeoutInSeconds));
					}
					hip = dagHealthInfoAsync.Result;
				}
			});
			if (ex != null)
			{
				base.WriteError(new GetDagHealthInfoRequestException(serverToQueryFqdn, ex.Message, ex), ErrorCategory.InvalidResult, serverToQueryFqdn);
				return null;
			}
			hip.ToString();
			return hip;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00143874 File Offset: 0x00141A74
		protected DatabaseAvailabilityGroup LookupDag(DatabaseAvailabilityGroupIdParameter dagParam)
		{
			ADObjectId id = new DatabaseAvailabilityGroupContainer().Id;
			return (DatabaseAvailabilityGroup)base.GetDataObject<DatabaseAvailabilityGroup>(dagParam, base.GlobalConfigSession, id, new LocalizedString?(Strings.ErrorDagNotFound(dagParam.ToString())), new LocalizedString?(Strings.ErrorDagNotUnique(dagParam.ToString())));
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x001438C4 File Offset: 0x00141AC4
		protected Server LookupServer(ServerIdParameter serverParam)
		{
			ADObjectId id = new ServersContainer().Id;
			Server server = (Server)base.GetDataObject<Server>(serverParam, base.GlobalConfigSession, id, new LocalizedString?(Strings.ErrorMailboxServerNotFound(serverParam.ToString())), new LocalizedString?(Strings.ErrorMailboxServerNotUnique(serverParam.ToString())));
			if (!server.IsMailboxServer)
			{
				base.WriteError(server.GetServerRoleError(ServerRole.Mailbox), ErrorCategory.InvalidOperation, serverParam);
				return null;
			}
			if (!server.IsE14OrLater)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServerNotE14OrLater(server.Name)), ErrorCategory.InvalidOperation, serverParam);
				return null;
			}
			return server;
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00143954 File Offset: 0x00141B54
		protected string DiscoverServerToQuery(ServerIdParameter serverToContactParam, DatabaseAvailabilityGroup dag)
		{
			string fqdn;
			if (serverToContactParam == null)
			{
				AmServerName primaryActiveManager = this.GetPrimaryActiveManager(dag);
				fqdn = primaryActiveManager.Fqdn;
			}
			else
			{
				Server server = this.LookupServer(serverToContactParam);
				ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
				if (databaseAvailabilityGroup == null)
				{
					base.WriteError(new ServerMustBeInDagException(server.Fqdn), ErrorCategory.InvalidData, serverToContactParam);
					return null;
				}
				if (!databaseAvailabilityGroup.Equals(dag.Id))
				{
					base.WriteError(new ServerToContactMustBeInSameDagException(server.Name, dag.Name, databaseAvailabilityGroup.Name), ErrorCategory.InvalidData, serverToContactParam);
					return null;
				}
				fqdn = server.Fqdn;
			}
			return fqdn;
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x001439D8 File Offset: 0x00141BD8
		private AmServerName GetPrimaryActiveManager(DatabaseAvailabilityGroup dag)
		{
			Exception ex = null;
			AmServerName result = null;
			try
			{
				result = DagTaskHelper.GetPrimaryActiveManagerNode(dag);
			}
			catch (AmFailedToDeterminePAM amFailedToDeterminePAM)
			{
				ex = amFailedToDeterminePAM;
			}
			catch (AmServerException ex2)
			{
				ex = ex2;
			}
			catch (AmServerTransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				base.WriteError(new PAMCouldNotBeDeterminedException(dag.Name, ex.Message, ex), ErrorCategory.ConnectionError, dag);
				return null;
			}
			return result;
		}

		// Token: 0x04002E50 RID: 11856
		protected const int DefaultTimeoutSecs = 10;
	}
}
