using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000003 RID: 3
	internal class EdgeSync
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000245C File Offset: 0x0000065C
		public EdgeSync(bool consoleDiagnostics, EdgeSyncAppConfig appConfig)
		{
			this.appConfig = appConfig;
			this.config = new EdgeSyncConfig(this.rootOrgConfigSession);
			this.topology = new Topology(this.rootOrgConfigSession);
			this.syncNowState = new SyncNowState(this);
			this.consoleDiagnostics = consoleDiagnostics;
			EdgeSync.serviceStartTime = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002524 File Offset: 0x00000724
		public EdgeSync()
		{
			this.config = new EdgeSyncConfig(this.rootOrgConfigSession);
			this.topology = new Topology(this.rootOrgConfigSession);
			Exception ex;
			this.config.Initialize(out ex);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000025CC File Offset: 0x000007CC
		public EdgeSyncAppConfig AppConfig
		{
			get
			{
				return this.appConfig;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000025D4 File Offset: 0x000007D4
		public EdgeSyncConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000025DC File Offset: 0x000007DC
		public EdgeSyncLogSession EdgeSyncLogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025E4 File Offset: 0x000007E4
		public Topology Topology
		{
			get
			{
				return this.topology;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000025EC File Offset: 0x000007EC
		public SyncNowState SyncNowState
		{
			get
			{
				return this.syncNowState;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025F4 File Offset: 0x000007F4
		public bool ConsoleDiagnostics
		{
			get
			{
				return this.consoleDiagnostics;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025FC File Offset: 0x000007FC
		public bool Shutdown
		{
			get
			{
				return this.shutdown;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002604 File Offset: 0x00000804
		public ITopologyConfigurationSession ConfigSession
		{
			get
			{
				return this.rootOrgConfigSession;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000260C File Offset: 0x0000080C
		public IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002614 File Offset: 0x00000814
		private bool ConfigSyncEnabled
		{
			get
			{
				return (this.appConfig.EnabledSyncType & SyncTreeType.Configuration) == SyncTreeType.Configuration;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002626 File Offset: 0x00000826
		private bool RecipientSyncEnabled
		{
			get
			{
				return (this.appConfig.EnabledSyncType & SyncTreeType.Recipients) == SyncTreeType.Recipients;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002638 File Offset: 0x00000838
		public bool Initialize()
		{
			Exception ex;
			if (!this.config.Initialize(out ex))
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_Failure, null, null);
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_InitializationFailureException, null, new object[]
				{
					ex
				});
				return false;
			}
			this.ConfigureEdgeSyncLog();
			this.config.ConfigChanged += this.OnConfigChange;
			if (!this.InitializeDirectTrust(out ex))
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_Failure, null, null);
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.DirectTrust, ex, "Exception during Initialization of DirectTrust");
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_InitializationFailureException, null, new object[]
				{
					ex
				});
				return false;
			}
			if (!this.topology.Initialize(this.logSession, out ex))
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_Failure, null, null);
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Topology, ex, "Exception during Initialization of Topology");
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_InitializationFailureException, null, new object[]
				{
					ex
				});
				return false;
			}
			this.topology.TopologyChanged += this.OnTopologyChange;
			lock (this.syncObject)
			{
				if (!this.CreateSynchronizationProvidersFromAppConfig(out ex))
				{
					EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_Failure, null, null);
					this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Topology, ex, "Exception during Initialization of providers from App.Config");
					EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_InitializationFailureException, null, new object[]
					{
						ex
					});
					return false;
				}
				if (!this.CreateSynchronizationProvidersFromConnectors(out ex))
				{
					EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_Failure, null, null);
					this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Configuration, ex, "Exception during Initialization of providers from EdgeSyncConnector in AD");
					EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_InitializationFailureException, null, new object[]
					{
						ex
					});
					return false;
				}
			}
			if (!SyncNowServer.Start(this, this.topology.ServerSecurity, out ex))
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_Failure, null, null);
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Topology, ex, "Exception during Initialization of RPC server");
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_InitializationFailureException, null, new object[]
				{
					ex
				});
				return false;
			}
			this.logSession.LogService("Service Started.");
			EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeSyncStarted, null, new object[0]);
			return true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void InitiateShutdown()
		{
			lock (this.syncObject)
			{
				this.shutdown = true;
				if (this.logSession != null)
				{
					this.logSession.LogService("Service Stopping.");
				}
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeSyncStopping, null, new object[0]);
				if (this.topology != null)
				{
					this.topology.Shutdown();
				}
				if (this.config != null)
				{
					this.config.Shutdown();
				}
				SyncNowServer.Stop();
				if (this.credentialMaintenanceJob != null)
				{
					this.credentialMaintenanceJob.InitiateShutdown();
				}
				foreach (EdgeServer edgeServer in this.currentTargetServers.Values)
				{
					edgeServer.InitiateShutdown();
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029BC File Offset: 0x00000BBC
		public void WaitShutdown()
		{
			int num = 0;
			for (;;)
			{
				bool flag = true;
				if (this.logSession != null)
				{
					this.logSession.LogService("Check if all jobs have been shutdown");
				}
				if (num > 30)
				{
					break;
				}
				foreach (EdgeServer edgeServer in this.currentTargetServers.Values)
				{
					if (edgeServer.HasShutdown())
					{
						if (this.logSession != null)
						{
							this.logSession.LogService("Edge server " + edgeServer.Name + " is shutdown");
						}
					}
					else
					{
						if (this.logSession != null)
						{
							this.logSession.LogService("Edge server " + edgeServer.Name + " is not shutdown yet");
						}
						flag = false;
					}
				}
				if (this.credentialMaintenanceJob != null)
				{
					if (this.credentialMaintenanceJob.HasShutdown)
					{
						if (this.logSession != null)
						{
							this.logSession.LogService("Credential Maintenance job shutdown");
						}
						this.credentialMaintenanceJob = null;
					}
					else
					{
						if (this.logSession != null)
						{
							this.logSession.LogService("Credential Maintenance job is not shutdown yet");
						}
						flag = false;
					}
				}
				if (flag)
				{
					goto Block_9;
				}
				if (this.logSession != null)
				{
					this.logSession.LogService("Sleep for 1 second to wait for all jobs to shutdown");
				}
				Thread.Sleep(1000);
				num++;
			}
			if (this.logSession != null)
			{
				this.logSession.LogService("Time limit exceeded. Forcefully shutdown");
				goto IL_169;
			}
			goto IL_169;
			Block_9:
			if (this.logSession != null)
			{
				this.logSession.LogService("All pending jobs shutdown");
			}
			IL_169:
			if (this.logSession != null)
			{
				this.logSession.LogService("Service Stopped.");
			}
			EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeSyncStopped, null, new object[0]);
			if (this.edgeSyncLog != null)
			{
				this.logSession = null;
				this.edgeSyncLog.Close();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void Synchronize()
		{
			lock (this.syncObject)
			{
				if (!this.shutdown)
				{
					this.previousTargetServers = this.currentTargetServers;
					this.currentTargetServers = new Dictionary<string, EdgeServer>();
					foreach (SynchronizationProvider provider in this.synchronizationProviders)
					{
						this.ScheduleNewSync(provider);
					}
					this.InternalSynchronize(false, string.Empty, false, false);
					this.StopObsoleteSync();
					if (this.credentialMaintenanceJob == null)
					{
						EdgeSyncLogSession edgeSyncLogSession = this.edgeSyncLog.OpenSession(Guid.NewGuid().ToString("N"), string.Empty, 0, string.Empty, this.config.ServiceConfig.LogLevel);
						this.credentialMaintenanceJob = new CredentialMaintenanceJob(EdgeSync.serviceStartTime, TimeSpan.FromMinutes(1.0), edgeSyncLogSession);
						this.credentialMaintenanceJob.BeginExecute();
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public void SynchronizeNow(string targetServer, bool forceFullSync, bool forceUpdateCookie)
		{
			lock (this.syncObject)
			{
				if (!this.shutdown)
				{
					this.previousTargetServers = this.currentTargetServers;
					this.currentTargetServers = new Dictionary<string, EdgeServer>();
					foreach (SynchronizationProvider provider in this.synchronizationProviders)
					{
						this.ScheduleNewSync(provider);
					}
					this.InternalSynchronize(true, targetServer, forceFullSync, forceUpdateCookie);
					this.StopObsoleteSync();
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D60 File Offset: 0x00000F60
		private void ConfigureEdgeSyncLog()
		{
			if (this.edgeSyncLog == null)
			{
				this.edgeSyncLog = new EdgeSyncLog("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version, "EdgeSync Log", "EdgeSync", "EdgeSync");
			}
			this.edgeSyncLog.Enabled = this.config.ServiceConfig.LogEnabled;
			LocalLongFullPath localLongFullPath = null;
			string path;
			if (LocalLongFullPath.TryParse(this.config.ServiceConfig.LogPath, out localLongFullPath))
			{
				path = this.config.ServiceConfig.LogPath;
			}
			else
			{
				path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.config.ServiceConfig.LogPath);
			}
			this.edgeSyncLog.Configure(path, this.config.ServiceConfig.LogMaxAge, this.config.ServiceConfig.LogMaxDirectorySize, this.config.ServiceConfig.LogMaxFileSize);
			if (this.logSession == null)
			{
				this.logSession = this.edgeSyncLog.OpenSession(Guid.NewGuid().ToString("N"), string.Empty, 0, string.Empty, this.config.ServiceConfig.LogLevel);
				return;
			}
			this.logSession.LoggingLevel = this.config.ServiceConfig.LogLevel;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002EAC File Offset: 0x000010AC
		private bool InitializeDirectTrust(out Exception exception)
		{
			exception = null;
			try
			{
				DirectTrust.Load();
			}
			catch (ADTransientException ex)
			{
				exception = ex;
			}
			catch (ADOperationException ex2)
			{
				exception = ex2;
			}
			return exception == null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EF4 File Offset: 0x000010F4
		private bool CreateSynchronizationProvidersFromConnectors(out Exception exception)
		{
			exception = null;
			foreach (EdgeSyncConnector edgeSyncConnector in this.config.Connectors.Values)
			{
				if (edgeSyncConnector.Enabled && !this.CreateSynchronizationProvider(edgeSyncConnector, edgeSyncConnector.AssemblyPath, edgeSyncConnector.Name, edgeSyncConnector.SynchronizationProvider, out exception))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F78 File Offset: 0x00001178
		private bool CreateSynchronizationProvidersFromAppConfig(out Exception exception)
		{
			exception = null;
			foreach (SynchronizationProviderInfo synchronizationProviderInfo in this.appConfig.SynchronizationProviderList)
			{
				if (synchronizationProviderInfo.Enabled && !this.CreateSynchronizationProvider(null, synchronizationProviderInfo.AssemblyPath, synchronizationProviderInfo.Name, synchronizationProviderInfo.SynchronizationProvider, out exception))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002FFC File Offset: 0x000011FC
		private bool CreateSynchronizationProvider(EdgeSyncConnector connector, string assemblyPath, string displayName, string type, out Exception exception)
		{
			exception = null;
			try
			{
				string text = assemblyPath;
				if (!File.Exists(text))
				{
					DirectoryInfo dir = new DirectoryInfo(ConfigurationContext.Setup.InstallPath);
					text = Util.FindAssembly(dir, assemblyPath);
					if (text == null)
					{
						exception = new FileNotFoundException(Strings.InvalidProviderPath(displayName, assemblyPath));
						return false;
					}
				}
				if (string.IsNullOrEmpty(type))
				{
					exception = new ArgumentException(Strings.ProviderNull(displayName));
					return false;
				}
				Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(text));
				SynchronizationProvider synchronizationProvider = (SynchronizationProvider)assembly.CreateInstance(type);
				if (synchronizationProvider == null)
				{
					exception = new ArgumentException(Strings.CouldNotCreateProvider(displayName));
					return false;
				}
				synchronizationProvider.Initialize(connector);
				this.synchronizationProviders.Add(synchronizationProvider);
			}
			catch (ExDirectoryException ex)
			{
				exception = ex;
				return false;
			}
			catch (IOException ex2)
			{
				exception = ex2;
				return false;
			}
			catch (BadImageFormatException ex3)
			{
				exception = ex3;
				return false;
			}
			catch (SecurityException ex4)
			{
				exception = ex4;
				return false;
			}
			catch (MissingMethodException ex5)
			{
				exception = ex5;
				return false;
			}
			catch (TargetInvocationException ex6)
			{
				exception = ex6;
				return false;
			}
			catch (TypeInitializationException ex7)
			{
				exception = ex7;
				return false;
			}
			catch (InvalidCastException ex8)
			{
				exception = ex8;
				return false;
			}
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003180 File Offset: 0x00001380
		private void OnTopologyChange(object sender, EventArgs e)
		{
			this.Synchronize();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003188 File Offset: 0x00001388
		private void OnConfigChange(object sender, EventArgs e)
		{
			this.logSession.LogConfiguration("Changes detected in EdgeSync AD configuration.");
			lock (this.syncObject)
			{
				if (this.shutdown)
				{
					return;
				}
				this.ConfigureEdgeSyncLog();
				this.synchronizationProviders.Clear();
				Exception exception;
				if (!this.CreateSynchronizationProvidersFromAppConfig(out exception))
				{
					this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Topology, exception, "Failed to refresh providers from App.Config");
					return;
				}
				if (!this.CreateSynchronizationProvidersFromConnectors(out exception))
				{
					this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Topology, exception, "Failed to refresh SynchronizationProviders from EdgeSync config in AD");
					return;
				}
			}
			this.Synchronize();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003234 File Offset: 0x00001434
		private void InternalSynchronize(bool syncNow, string targetServer, bool forceFullSync, bool forceUpdateCookie)
		{
			foreach (EdgeServer edgeServer in this.currentTargetServers.Values)
			{
				if (syncNow)
				{
					if (string.IsNullOrEmpty(targetServer) || targetServer.Equals(edgeServer.Name, StringComparison.OrdinalIgnoreCase) || targetServer.Equals(edgeServer.Host, StringComparison.OrdinalIgnoreCase))
					{
						this.SyncNowState.AddPendingEdge(edgeServer.Name, edgeServer);
						edgeServer.SynchronizeNow(forceFullSync, forceUpdateCookie);
					}
				}
				else
				{
					edgeServer.Synchronize();
				}
			}
			EdgeSyncTopologyPerfCounters.EdgesLeased.RawValue = (long)this.currentTargetServers.Count;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000032E8 File Offset: 0x000014E8
		private void ScheduleNewSync(SynchronizationProvider provider)
		{
			foreach (TargetServerConfig targetServerConfig in provider.TargetServerConfigs)
			{
				string key = provider.Identity + targetServerConfig.Host;
				EdgeServer edgeServer = null;
				if (this.previousTargetServers.TryGetValue(key, out edgeServer))
				{
					this.previousTargetServers.Remove(key);
					edgeServer.RefreshConfig(targetServerConfig, provider);
				}
				else
				{
					edgeServer = new EdgeServer(targetServerConfig, provider.LeaseLockTryCount);
					this.logSession.LogTopology(edgeServer.Host, "Create a new sync target");
					if (this.ConfigSyncEnabled)
					{
						EdgeSyncLogSession edgeSyncLogSession = this.edgeSyncLog.OpenSession(Guid.NewGuid().ToString("N"), string.IsNullOrEmpty(targetServerConfig.ShortHostName) ? targetServerConfig.Host : targetServerConfig.ShortHostName, targetServerConfig.Port, string.Empty, this.config.ServiceConfig.LogLevel);
						edgeServer.ConfigureSynchronizerManager(SyncTreeType.Configuration, provider, edgeSyncLogSession);
					}
					if (this.RecipientSyncEnabled)
					{
						EdgeSyncLogSession edgeSyncLogSession2 = this.edgeSyncLog.OpenSession(Guid.NewGuid().ToString("N"), string.IsNullOrEmpty(targetServerConfig.ShortHostName) ? targetServerConfig.Host : targetServerConfig.ShortHostName, targetServerConfig.Port, string.Empty, this.config.ServiceConfig.LogLevel);
						edgeServer.ConfigureSynchronizerManager(SyncTreeType.Recipients, provider, edgeSyncLogSession2);
					}
				}
				this.currentTargetServers.Add(key, edgeServer);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003488 File Offset: 0x00001688
		private void StopObsoleteSync()
		{
			foreach (EdgeServer edgeServer in this.previousTargetServers.Values)
			{
				edgeServer.InitiateShutdown();
				this.logSession.LogTopology(edgeServer.Host, "Sync target removed.");
			}
			this.previousTargetServers = null;
		}

		// Token: 0x04000005 RID: 5
		private static long serviceStartTime;

		// Token: 0x04000006 RID: 6
		private readonly object syncObject = new object();

		// Token: 0x04000007 RID: 7
		private readonly Topology topology;

		// Token: 0x04000008 RID: 8
		private readonly SyncNowState syncNowState;

		// Token: 0x04000009 RID: 9
		private readonly bool consoleDiagnostics;

		// Token: 0x0400000A RID: 10
		private EdgeSyncConfig config;

		// Token: 0x0400000B RID: 11
		private EdgeSyncAppConfig appConfig;

		// Token: 0x0400000C RID: 12
		private EdgeSyncLog edgeSyncLog;

		// Token: 0x0400000D RID: 13
		private EdgeSyncLogSession logSession;

		// Token: 0x0400000E RID: 14
		private bool shutdown;

		// Token: 0x0400000F RID: 15
		private Dictionary<string, EdgeServer> currentTargetServers = new Dictionary<string, EdgeServer>();

		// Token: 0x04000010 RID: 16
		private Dictionary<string, EdgeServer> previousTargetServers;

		// Token: 0x04000011 RID: 17
		private CredentialMaintenanceJob credentialMaintenanceJob;

		// Token: 0x04000012 RID: 18
		private ITopologyConfigurationSession rootOrgConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 112, "rootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\EdgeSyncMain\\EdgeSync.cs");

		// Token: 0x04000013 RID: 19
		private IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 119, "recipientSession", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\EdgeSyncMain\\EdgeSync.cs");

		// Token: 0x04000014 RID: 20
		private List<SynchronizationProvider> synchronizationProviders = new List<SynchronizationProvider>();
	}
}
