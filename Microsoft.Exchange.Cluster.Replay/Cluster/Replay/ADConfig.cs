using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000CD RID: 205
	internal class ADConfig : IADConfig
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00028297 File Offset: 0x00026497
		public ADConfig(IMonitoringADConfigProvider cfgSource)
		{
			this.cfgSource = cfgSource;
			ReplayServerPerfmon.ADConfigRefreshCalls.RawValue = 0L;
			ReplayServerPerfmon.ADConfigRefreshCallsPerSec.RawValue = 0L;
			this.synchronizer = new SynchronizedAction(new Action(this.RunRefresh));
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000282D8 File Offset: 0x000264D8
		private IMonitoringADConfig GetCurrentConfig()
		{
			IMonitoringADConfig result = null;
			try
			{
				result = this.cfgSource.GetConfig(true);
			}
			catch (MonitoringADConfigException ex)
			{
				ADConfig.Tracer.TraceError<MonitoringADConfigException>((long)this.GetHashCode(), "GetCurrentConfig failed: {0}", ex);
				ReplayCrimsonEvents.ADConfigGetCurrentConfigFailed.LogPeriodic<string, string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, ex.ToString(), Environment.StackTrace);
			}
			return result;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00028344 File Offset: 0x00026544
		public IADDatabaseAvailabilityGroup GetLocalDag()
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			return currentConfig.Dag;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00028364 File Offset: 0x00026564
		public IADServer GetLocalServer()
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			string localComputerFqdn = Dependencies.ManagementClassHelper.LocalComputerFqdn;
			new AmServerName(localComputerFqdn);
			return currentConfig.TargetMiniServer;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00028398 File Offset: 0x00026598
		public IADServer GetServer(AmServerName serverName)
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			IADServer iadserver = currentConfig.LookupMiniServerByName(serverName);
			if (iadserver == null)
			{
				iadserver = this.HandleMissingServer(serverName);
			}
			return iadserver;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000283C5 File Offset: 0x000265C5
		public IADServer GetServer(string nodeOrFqdn)
		{
			if (string.IsNullOrEmpty(nodeOrFqdn))
			{
				return this.GetLocalServer();
			}
			return this.GetServer(new AmServerName(nodeOrFqdn));
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000283E4 File Offset: 0x000265E4
		private IADServer HandleMissingServer(AmServerName serverName)
		{
			ADConfig.Tracer.TraceError<string>((long)this.GetHashCode(), "Server {0} not found", serverName.Fqdn);
			IReplayAdObjectLookup replayAdObjectLookup = Dependencies.ReplayAdObjectLookup;
			IADServer iadserver = replayAdObjectLookup.ServerLookup.FindServerByFqdn(serverName.Fqdn);
			if (iadserver != null)
			{
				this.Refresh("HandleMissingServer");
			}
			return iadserver;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00028434 File Offset: 0x00026634
		public IADDatabase GetDatabase(Guid dbGuid)
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			IADDatabase result;
			if (currentConfig.DatabaseByGuidMap.TryGetValue(dbGuid, out result))
			{
				return result;
			}
			return this.HandleMissingDatabase(dbGuid);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00028468 File Offset: 0x00026668
		private IADDatabase HandleMissingDatabase(Guid dbGuid)
		{
			ADConfig.Tracer.TraceError<Guid>((long)this.GetHashCode(), "Database {0} not found", dbGuid);
			IReplayAdObjectLookup replayAdObjectLookup = Dependencies.ReplayAdObjectLookup;
			IADDatabase iaddatabase = replayAdObjectLookup.DatabaseLookup.FindAdObjectByGuid(dbGuid);
			if (iaddatabase != null)
			{
				this.Refresh("HandleMissingDatabase");
			}
			return iaddatabase;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000284B0 File Offset: 0x000266B0
		public IEnumerable<IADDatabase> GetDatabasesOnServer(AmServerName serverName)
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			IEnumerable<IADDatabase> result = null;
			if (currentConfig.DatabaseMap.TryGetValue(serverName, out result))
			{
				return result;
			}
			return this.HandleMissingServerDatabases(serverName);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000284E4 File Offset: 0x000266E4
		public IEnumerable<IADDatabase> HandleMissingServerDatabases(AmServerName serverName)
		{
			ADConfig.Tracer.TraceError<AmServerName>((long)this.GetHashCode(), "Databases for server {0} not found", serverName);
			IReplayAdObjectLookup replayAdObjectLookup = Dependencies.ReplayAdObjectLookup;
			IADServer iadserver = replayAdObjectLookup.ServerLookup.FindServerByFqdn(serverName.Fqdn);
			if (iadserver != null)
			{
				this.Refresh("HandleMissingServerDatabases");
				IMonitoringADConfig currentConfig = this.GetCurrentConfig();
				if (currentConfig == null)
				{
					return null;
				}
				IEnumerable<IADDatabase> result = null;
				if (currentConfig.DatabaseMap.TryGetValue(serverName, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00028550 File Offset: 0x00026750
		public IEnumerable<IADDatabase> GetDatabasesOnServer(IADServer server)
		{
			AmServerName serverName = new AmServerName(server.Fqdn);
			return this.GetDatabasesOnServer(serverName);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00028570 File Offset: 0x00026770
		public IEnumerable<IADDatabase> GetDatabasesOnLocalServer()
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			return this.GetDatabasesOnServer(currentConfig.TargetServerName);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00028598 File Offset: 0x00026798
		public IEnumerable<IADDatabase> GetDatabasesInLocalDag()
		{
			IMonitoringADConfig currentConfig = this.GetCurrentConfig();
			if (currentConfig == null)
			{
				return null;
			}
			return currentConfig.DatabaseByGuidMap.Values;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000285BC File Offset: 0x000267BC
		public void Refresh(string reason)
		{
			this.Refresh(reason, RegistryParameters.ADConfigRefreshDefaultTimeoutInSec);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000285CA File Offset: 0x000267CA
		public void Refresh(string reason, int timeoutInSec)
		{
			ADConfig.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Refresh: {0}", reason);
			this.synchronizer.TryAction(TimeSpan.FromSeconds((double)timeoutInSec));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000285F8 File Offset: 0x000267F8
		private void RunRefresh()
		{
			MonitoringADConfigManager monitoringADConfigManager = this.cfgSource as MonitoringADConfigManager;
			if (monitoringADConfigManager != null)
			{
				monitoringADConfigManager.TryRefreshConfig();
			}
		}

		// Token: 0x04000397 RID: 919
		private IMonitoringADConfigProvider cfgSource;

		// Token: 0x04000398 RID: 920
		private static readonly Trace Tracer = ExTraceGlobals.ADCacheTracer;

		// Token: 0x04000399 RID: 921
		private SynchronizedAction synchronizer;
	}
}
