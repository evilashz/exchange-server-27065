using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200002A RID: 42
	internal class AmDatabaseStateTracker
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		internal static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DbTrackerTracer;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C103 File Offset: 0x0000A303
		internal AmDatabaseStateTracker()
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C133 File Offset: 0x0000A333
		internal bool Initialize()
		{
			return this.Initialize(TimeSpan.FromSeconds((double)RegistryParameters.DatabaseStateTrackerInitTimeoutInSec));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C18C File Offset: 0x0000A38C
		internal bool Initialize(TimeSpan timeToWait)
		{
			if (RegistryParameters.DatabaseStateTrackerDisabled)
			{
				AmDatabaseStateTracker.Tracer.TraceWarning(0L, "Initialize(): AmDatabaseStateTracker disabled by registry override");
				return false;
			}
			if (!AmSystemManager.Instance.Config.IsPAM)
			{
				return false;
			}
			if (this.isInitSuccess)
			{
				return true;
			}
			if (!this.isInitInProgress)
			{
				ThreadPool.QueueUserWorkItem(delegate(object unused)
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					try
					{
						this.InternalInitialize();
					}
					finally
					{
						AmDatabaseStateTracker.Tracer.TraceDebug<TimeSpan>(0L, "AmDatabaseStateTracker.InternalInitialize() took {0} to complete.", stopwatch.Elapsed);
					}
				});
				AmDatabaseStateTracker.Tracer.TraceDebug<TimeSpan>(0L, "Waiting for initialization to complete in {0}.", timeToWait);
				ManualOneShotEvent.Result arg = this.initializationCompleteEvent.WaitOne(timeToWait);
				AmDatabaseStateTracker.Tracer.TraceDebug<ManualOneShotEvent.Result>(0L, "initializationCompleteEvent.WaitOne() returned {0}", arg);
			}
			return this.isInitSuccess;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C228 File Offset: 0x0000A428
		internal void Cleanup()
		{
			if (RegistryParameters.DatabaseStateTrackerDisabled)
			{
				AmDatabaseStateTracker.Tracer.TraceWarning(0L, "Cleanup(): AmDatabaseStateTracker disabled by registry override");
				return;
			}
			lock (this.locker)
			{
				AmDatabaseStateTracker.Tracer.TraceDebug(0L, "Cleaning up");
				this.isInitSuccess = false;
				this.isInitInProgress = false;
				this.initializationCompleteEvent.Dispose();
				this.serverInfoMap.Clear();
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		internal bool IsMaxActivesExceeded(AmServerName serverName, int? maxActivesConfigured)
		{
			if (RegistryParameters.DatabaseStateTrackerDisabled)
			{
				AmDatabaseStateTracker.Tracer.TraceWarning(0L, "IsMaxActivesExceeded(): AmDatabaseStateTracker disabled by registry override");
				return false;
			}
			AmDatabaseStateTracker.Tracer.TraceDebug<AmServerName, string>(0L, "IsMaxActivesExceeded(serverName={0}, maxActivesConfigured={1})", serverName, (maxActivesConfigured != null) ? maxActivesConfigured.Value.ToString() : "<null>");
			if (maxActivesConfigured == null)
			{
				AmDatabaseStateTracker.Tracer.TraceDebug<AmServerName>(0L, "maxActivesConfigured is null for {0}. Skipping", serverName);
				return false;
			}
			if (!this.Initialize())
			{
				AmDatabaseStateTracker.Tracer.TraceError(0L, "IsMaxActivesExceeded() skipped since initialization did not complete");
				return false;
			}
			bool result;
			lock (this.locker)
			{
				result = this.IsMaxActivesExceededInternal(serverName, maxActivesConfigured);
			}
			return result;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C378 File Offset: 0x0000A578
		internal bool UpdateActiveIfMaxActivesNotExceeded(Guid databaseGuid, AmServerName serverName, int? maxActivesConfigured)
		{
			if (RegistryParameters.DatabaseStateTrackerDisabled)
			{
				AmDatabaseStateTracker.Tracer.TraceWarning(0L, "UpdateActiveIfMaxActivesNotExceeded(): AmDatabaseStateTracker disabled by registry override");
				return true;
			}
			AmDatabaseStateTracker.Tracer.TraceDebug<Guid, AmServerName, string>(0L, "UpdateActiveIfMaxActivesNotExceeded(databaseGuid={0}, serverName={1}, maxActivesConfigured={2})", databaseGuid, serverName, (maxActivesConfigured != null) ? maxActivesConfigured.Value.ToString() : "<null>");
			if (!this.Initialize())
			{
				AmDatabaseStateTracker.Tracer.TraceError(0L, "UpdateActiveIfMaxActivesNotExceeded() skipped since initialization did not complete");
				return true;
			}
			bool result;
			lock (this.locker)
			{
				bool flag2 = this.IsMaxActivesExceededInternal(serverName, maxActivesConfigured);
				if (!flag2)
				{
					this.UpdateActive(databaseGuid, serverName);
				}
				result = !flag2;
			}
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C438 File Offset: 0x0000A638
		internal void UpdateActive(Guid databaseGuid, AmServerName serverName)
		{
			if (RegistryParameters.DatabaseStateTrackerDisabled)
			{
				AmDatabaseStateTracker.Tracer.TraceWarning(0L, "UpdateActive(): AmDatabaseStateTracker disabled by registry override");
				return;
			}
			AmDatabaseStateTracker.Tracer.TraceDebug<Guid, AmServerName>(0L, "UpdateActive(database={0}, serverName={1})", databaseGuid, serverName);
			if (!this.Initialize())
			{
				AmDatabaseStateTracker.Tracer.TraceError(0L, "UpdateActive() skipped since initialization did not complete");
				return;
			}
			lock (this.locker)
			{
				if (this.IsUpdateRequired(databaseGuid, serverName))
				{
					foreach (AmDatabaseStateTracker.ServerInfo serverInfo in this.serverInfoMap.Values)
					{
						serverInfo.ActiveDatabases.Remove(databaseGuid);
					}
					this.UpdateActiveInternal(this.serverInfoMap, databaseGuid, serverName);
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C520 File Offset: 0x0000A720
		private bool IsUpdateRequired(Guid databaseGuid, AmServerName serverName)
		{
			bool result = true;
			AmDatabaseStateTracker.ServerInfo serverInfo;
			if (this.serverInfoMap.TryGetValue(serverName, out serverInfo) && serverInfo != null && serverInfo.ActiveDatabases.Contains(databaseGuid))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C554 File Offset: 0x0000A754
		private bool IsMaxActivesExceededInternal(AmServerName serverName, int? maxActivesConfigured)
		{
			if (maxActivesConfigured == null)
			{
				AmDatabaseStateTracker.Tracer.TraceDebug<AmServerName>(0L, "IsMaxActivesExceededInternal(): maxActivesConfigured is null for {0}. Skipping", serverName);
				return false;
			}
			AmDatabaseStateTracker.ServerInfo serverInfo;
			if (this.serverInfoMap.TryGetValue(serverName, out serverInfo) && serverInfo.ActiveDatabases.Count >= maxActivesConfigured.Value)
			{
				AmDatabaseStateTracker.Tracer.TraceError<AmServerName>(0L, "MaxActives exceeded on server {0}", serverName);
				return true;
			}
			return false;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		private void UpdateActiveInternal(Dictionary<AmServerName, AmDatabaseStateTracker.ServerInfo> serverInfoMap, Guid databaseGuid, AmServerName serverName)
		{
			AmDatabaseStateTracker.Tracer.TraceDebug<Guid, AmServerName>(0L, "Database {0} is active on {1}", databaseGuid, serverName);
			AmDatabaseStateTracker.ServerInfo serverInfo;
			if (!serverInfoMap.TryGetValue(serverName, out serverInfo))
			{
				serverInfo = new AmDatabaseStateTracker.ServerInfo();
				serverInfo.Name = serverName;
				serverInfo.ActiveDatabases = new HashSet<Guid>();
				serverInfoMap[serverName] = serverInfo;
			}
			serverInfo.ActiveDatabases.Add(databaseGuid);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C620 File Offset: 0x0000A820
		private void InternalInitialize()
		{
			if (this.isInitSuccess || this.isInitInProgress)
			{
				return;
			}
			lock (this.locker)
			{
				if (!this.isInitSuccess && !this.isInitInProgress)
				{
					this.isInitInProgress = true;
					AmDatabaseStateTracker.Tracer.TraceInformation(0, 0L, "InternalInitialize() in started");
					Exception ex = null;
					try
					{
						ex = SharedHelper.RunADOperationEx(delegate(object param0, EventArgs param1)
						{
							this.serverInfoMap = this.GenerateServerInfoMapFromClusdb();
						});
						if (ex == null)
						{
							this.isInitSuccess = true;
							this.initializationCompleteEvent.Set();
						}
					}
					catch (ClusterException ex2)
					{
						ex = ex2;
					}
					catch (AmCommonException ex3)
					{
						ex = ex3;
					}
					finally
					{
						this.isInitInProgress = false;
						AmDatabaseStateTracker.Tracer.TraceInformation<bool>(0, 0L, "InternalInitialize() completed (isInitComplete={0})", this.isInitSuccess);
					}
					if (ex != null)
					{
						AmDatabaseStateTracker.Tracer.TraceError<Exception>(0L, "InternalInitialize() failed with {0}", ex);
						ReplayCrimsonEvents.DbStateTrackerInitializationFailed.LogPeriodic<string, Exception>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
					}
				}
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C754 File Offset: 0x0000A954
		private Dictionary<AmServerName, AmDatabaseStateTracker.ServerInfo> GenerateServerInfoMapFromClusdb()
		{
			IEnumerable<IADDatabase> allDatabasesInDag = this.GetAllDatabasesInDag();
			Dictionary<AmServerName, AmDatabaseStateTracker.ServerInfo> result = new Dictionary<AmServerName, AmDatabaseStateTracker.ServerInfo>();
			if (allDatabasesInDag != null)
			{
				foreach (IADDatabase iaddatabase in allDatabasesInDag)
				{
					AmDbStateInfo amDbStateInfo = AmSystemManager.Instance.Config.DbState.Read(iaddatabase.Guid);
					if (amDbStateInfo != null && amDbStateInfo.IsEntryExist)
					{
						this.UpdateActiveInternal(result, iaddatabase.Guid, amDbStateInfo.ActiveServer);
					}
					else
					{
						AmDatabaseStateTracker.Tracer.TraceError<string>(0L, "GenerateServerInfoMapFromClusdb() could not find state for database '{0}' in ClusDB", iaddatabase.Name);
					}
				}
			}
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C800 File Offset: 0x0000AA00
		private IEnumerable<IADDatabase> GetAllDatabasesInDag()
		{
			return Dependencies.ADConfig.GetDatabasesInLocalDag();
		}

		// Token: 0x040000C2 RID: 194
		private object locker = new object();

		// Token: 0x040000C3 RID: 195
		private Dictionary<AmServerName, AmDatabaseStateTracker.ServerInfo> serverInfoMap = new Dictionary<AmServerName, AmDatabaseStateTracker.ServerInfo>(16);

		// Token: 0x040000C4 RID: 196
		private ManualOneShotEvent initializationCompleteEvent = new ManualOneShotEvent("DatabaseTrackerInitEvent");

		// Token: 0x040000C5 RID: 197
		private bool isInitInProgress;

		// Token: 0x040000C6 RID: 198
		private bool isInitSuccess;

		// Token: 0x0200002B RID: 43
		internal class ServerInfo
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000C80C File Offset: 0x0000AA0C
			// (set) Token: 0x060001EA RID: 490 RVA: 0x0000C814 File Offset: 0x0000AA14
			internal AmServerName Name { get; set; }

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x060001EB RID: 491 RVA: 0x0000C81D File Offset: 0x0000AA1D
			// (set) Token: 0x060001EC RID: 492 RVA: 0x0000C825 File Offset: 0x0000AA25
			internal HashSet<Guid> ActiveDatabases { get; set; }
		}
	}
}
