using System;
using System.ComponentModel;
using System.Configuration;
using System.Security;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Serialization;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.HA.Services;
using Microsoft.Mapi;

namespace Microsoft.Exchange.HA.SupportApi
{
	// Token: 0x0200037E RID: 894
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, IncludeExceptionDetailInFaults = true)]
	internal class SupportApiService : IInternalSupportApi
	{
		// Token: 0x060023D7 RID: 9175 RVA: 0x000A7B78 File Offset: 0x000A5D78
		private static bool AuthorizeRequest(WindowsIdentity wid)
		{
			IdentityReferenceCollection groups = wid.Groups;
			foreach (IdentityReference left in groups)
			{
				if (left == SupportApiService.s_localAdminsSid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000A7BD4 File Offset: 0x000A5DD4
		private static void ThrowNotAuthorized()
		{
			throw new SecurityException("Not authorized to access the SupportApi");
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000A7BE0 File Offset: 0x000A5DE0
		private static void CheckSecurity()
		{
			WindowsIdentity windowsIdentity = ServiceSecurityContext.Current.PrimaryIdentity as WindowsIdentity;
			if (windowsIdentity == null)
			{
				SupportApiService.ThrowNotAuthorized();
			}
			if (!SupportApiService.AuthorizeRequest(windowsIdentity))
			{
				SupportApiService.ThrowNotAuthorized();
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000A7C12 File Offset: 0x000A5E12
		public void DisconnectCopier(Guid dbGuid)
		{
			LogCopier.TestDisconnectCopier(dbGuid);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000A7C1A File Offset: 0x000A5E1A
		public void ConnectCopier(Guid dbGuid)
		{
			LogCopier.TestConnectCopier(dbGuid);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000A7C24 File Offset: 0x000A5E24
		public void SetFailedAndSuspended(Guid dbGuid, bool fSuspendCopy, uint errorEventId, string failedMsg)
		{
			ReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
			replicaInstanceManager.RequestSuspendAndFail_SupportApi(dbGuid, fSuspendCopy, errorEventId, failedMsg, "Suspended by the SupportApi SetFailedAndSuspended() test call.", false);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000A7C4D File Offset: 0x000A5E4D
		public void TriggerShutdownSwitchover()
		{
			ActiveManagerCore.AttemptServerSwitchoverOnShutdown();
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000A7C55 File Offset: 0x000A5E55
		public void IgnoreGranularCompletions(Guid dbGuid)
		{
			LogCopier.TestIgnoreGranularCompletions(dbGuid);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000A7C5D File Offset: 0x000A5E5D
		public void ReloadRegistryParameters()
		{
			RegistryParameters.TestLoadRegistryParameters();
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000A7C64 File Offset: 0x000A5E64
		public void TriggerLogSourceCorruption(Guid dbGuid, bool granular, bool granularRepairFails, int countOfLogsBeforeCorruption)
		{
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000A7C68 File Offset: 0x000A5E68
		public void SetCopyProperty(Guid dbGuid, string propName, string propVal)
		{
			char[] separator = new char[]
			{
				'.'
			};
			string[] array = propName.Split(separator, 2);
			if (array.Length > 1)
			{
				if (SharedHelper.StringIEquals(array[0], "MonitoredDatabase"))
				{
					MonitoredDatabase.SetCopyProperty(dbGuid, array[1], propVal);
					return;
				}
				if (SharedHelper.StringIEquals(array[0], "LogCopier"))
				{
					LogCopier.SetCopyProperty(dbGuid, array[1], propVal);
					return;
				}
			}
			string message = string.Format("SetCopyProperty doesn't recognize '{0}'", propName);
			throw new ArgumentException(message);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000A7CD8 File Offset: 0x000A5ED8
		public static SupportApiService StartListening(out Exception exception)
		{
			exception = null;
			SupportApiService supportApiService = null;
			try
			{
				supportApiService = new SupportApiService();
				int num = 2014;
				NetTcpBinding netTcpBinding = new NetTcpBinding();
				netTcpBinding.PortSharingEnabled = true;
				string uriString = string.Format("net.tcp://localhost:{0}/Microsoft.Exchange.HA.SupportApi", num);
				Uri uri = new Uri(uriString);
				supportApiService.m_host = new ServiceHost(supportApiService, new Uri[]
				{
					uri
				});
				supportApiService.m_host.AddServiceEndpoint(typeof(IInternalSupportApi), netTcpBinding, string.Empty);
				supportApiService.m_host.Open();
				return supportApiService;
			}
			catch (CommunicationException ex)
			{
				exception = ex;
			}
			catch (ConfigurationException ex2)
			{
				exception = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				exception = ex3;
			}
			ReplayCrimsonEvents.SupportApiFailedToStart.LogPeriodic<string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, exception.ToString());
			if (supportApiService != null && supportApiService.m_host != null)
			{
				supportApiService.m_host.Abort();
			}
			return null;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000A7DDC File Offset: 0x000A5FDC
		public void StopListening()
		{
			this.m_host.Close();
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000A7DE9 File Offset: 0x000A5FE9
		private byte[] SerializeException(Exception exception)
		{
			return Serialization.ObjectToBytes(exception);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000A7DF4 File Offset: 0x000A5FF4
		private Exception DoAction(Action action)
		{
			Exception result = null;
			try
			{
				SupportApiService.CheckSecurity();
				action();
			}
			catch (ADTransientException ex)
			{
				result = ex;
			}
			catch (ADExternalException ex2)
			{
				result = ex2;
			}
			catch (ADOperationException ex3)
			{
				result = ex3;
			}
			catch (AmServiceShuttingDownException ex4)
			{
				result = ex4;
			}
			catch (MapiPermanentException ex5)
			{
				result = ex5;
			}
			catch (MapiRetryableException ex6)
			{
				result = ex6;
			}
			catch (DataSourceTransientException ex7)
			{
				result = ex7;
			}
			catch (DataSourceOperationException ex8)
			{
				result = ex8;
			}
			catch (HaRpcServerBaseException ex9)
			{
				result = ex9;
			}
			catch (TransientException ex10)
			{
				result = ex10;
			}
			catch (Win32Exception ex11)
			{
				result = ex11;
			}
			catch (SecurityException ex12)
			{
				result = ex12;
			}
			return result;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000A7EF4 File Offset: 0x000A60F4
		public void TriggerConfigUpdater()
		{
			Dependencies.ConfigurationUpdater.RunConfigurationUpdater(true, ReplayConfigChangeHints.RunConfigUpdaterRpc);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000A7F04 File Offset: 0x000A6104
		public void TriggerDumpster(Guid dbGuid, DateTime inspectorTime)
		{
			IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreatePartiallyConsistentRootOrgSession(true);
			IADDatabase db = iadtoplogyConfigurationSession.FindDatabaseByGuid(dbGuid);
			IADServer server = iadtoplogyConfigurationSession.FindServerByName(Environment.MachineName);
			IADDatabaseAvailabilityGroup dag = iadtoplogyConfigurationSession.FindDagByServer(server);
			ReplayConfiguration configuration = RemoteReplayConfiguration.TaskGetReplayConfig(dag, db, server);
			DumpsterRedeliveryWrapper.MarkRedeliveryRequired(configuration, inspectorTime, 0L, 0L);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000A7F4C File Offset: 0x000A614C
		public void TriggerDumpsterEx(Guid dbGuid, bool fTriggerSafetyNet, DateTime failoverTimeUtc, DateTime startTimeUtc, DateTime endTimeUtc, long lastLogGenBeforeActivation, long numLogsLost)
		{
			IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreatePartiallyConsistentRootOrgSession(true);
			IADDatabase db = iadtoplogyConfigurationSession.FindDatabaseByGuid(dbGuid);
			IADServer server = iadtoplogyConfigurationSession.FindServerByName(Environment.MachineName);
			IADDatabaseAvailabilityGroup dag = iadtoplogyConfigurationSession.FindDagByServer(server);
			ReplayConfiguration configuration = RemoteReplayConfiguration.TaskGetReplayConfig(dag, db, server);
			DumpsterRedeliveryWrapper.MarkRedeliveryRequired(configuration, failoverTimeUtc, startTimeUtc, endTimeUtc, lastLogGenBeforeActivation, numLogsLost);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000A7F98 File Offset: 0x000A6198
		public void DoDumpsterRedeliveryIfRequired(Guid dbGuid)
		{
			IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreatePartiallyConsistentRootOrgSession(true);
			IADDatabase db = iadtoplogyConfigurationSession.FindDatabaseByGuid(dbGuid);
			IADServer server = iadtoplogyConfigurationSession.FindServerByName(Environment.MachineName);
			IADDatabaseAvailabilityGroup dag = iadtoplogyConfigurationSession.FindDagByServer(server);
			ReplayConfiguration replayConfig = RemoteReplayConfiguration.TaskGetReplayConfig(dag, db, server);
			DumpsterRedeliveryWrapper.DoRedeliveryIfRequired(replayConfig);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000A7FD9 File Offset: 0x000A61D9
		public void TriggerServerLocatorRestart()
		{
			ServerLocatorManager.Instance.RestartServiceHost(this, null);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000A7FE8 File Offset: 0x000A61E8
		public void TriggerTruncation(Guid dbGuid)
		{
			ReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
			ReplicaInstance replicaInstance;
			if (replicaInstanceManager.TryGetReplicaInstance(dbGuid, out replicaInstance))
			{
				LogTruncater component = replicaInstance.GetComponent<LogTruncater>();
				component.TimerCallback(null);
				return;
			}
			throw new ArgumentException("Unable to find a ReplicaInstance with a DB Guid of " + dbGuid, "dbGuid");
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000A8034 File Offset: 0x000A6234
		public bool Ping()
		{
			return true;
		}

		// Token: 0x04000F50 RID: 3920
		private static SecurityIdentifier s_localAdminsSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

		// Token: 0x04000F51 RID: 3921
		private ServiceHost m_host;
	}
}
