using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000228 RID: 552
	internal class MonitoringADConfig : IMonitoringADConfig
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00052D96 File Offset: 0x00050F96
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x00052D9E File Offset: 0x00050F9E
		public AmServerName TargetServerName { get; private set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00052DA7 File Offset: 0x00050FA7
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00052DAF File Offset: 0x00050FAF
		public IADServer TargetMiniServer { get; private set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00052DB8 File Offset: 0x00050FB8
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00052DC0 File Offset: 0x00050FC0
		public MonitoringServerRole ServerRole { get; private set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00052DC9 File Offset: 0x00050FC9
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00052DD1 File Offset: 0x00050FD1
		public IADDatabaseAvailabilityGroup Dag { get; private set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00052DDA File Offset: 0x00050FDA
		public List<IADServer> Servers
		{
			get
			{
				return this.m_serverList;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00052DE2 File Offset: 0x00050FE2
		public List<AmServerName> AmServerNames
		{
			get
			{
				return this.m_amServerNameList;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00052DEA File Offset: 0x00050FEA
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00052DF2 File Offset: 0x00050FF2
		public Dictionary<AmServerName, IEnumerable<IADDatabase>> DatabaseMap { get; private set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00052DFB File Offset: 0x00050FFB
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00052E03 File Offset: 0x00051003
		public Dictionary<AmServerName, IEnumerable<IADDatabase>> DatabasesIncludingMisconfiguredMap { get; private set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00052E0C File Offset: 0x0005100C
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x00052E14 File Offset: 0x00051014
		public Dictionary<Guid, IADDatabase> DatabaseByGuidMap { get; private set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00052E1D File Offset: 0x0005101D
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x00052E25 File Offset: 0x00051025
		public DateTime CreateTimeUtc { get; private set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00052E2E File Offset: 0x0005102E
		private static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00052E35 File Offset: 0x00051035
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00052E3D File Offset: 0x0005103D
		private IReplayAdObjectLookup AdLookup { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00052E46 File Offset: 0x00051046
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x00052E4E File Offset: 0x0005104E
		private IReplayAdObjectLookup AdLookupPartiallyConsistent { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00052E57 File Offset: 0x00051057
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00052E5F File Offset: 0x0005105F
		private IADToplogyConfigurationSession AdSessionIgnoreInvalid { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00052E68 File Offset: 0x00051068
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x00052E70 File Offset: 0x00051070
		private IADToplogyConfigurationSession AdSessionPartiallyConsistent { get; set; }

		// Token: 0x060014FF RID: 5375 RVA: 0x00052E79 File Offset: 0x00051079
		protected MonitoringADConfig(AmServerName serverName, IReplayAdObjectLookup adLookup, IReplayAdObjectLookup adLookupPartiallyConsistent, IADToplogyConfigurationSession adSession, IADToplogyConfigurationSession adSessionPartiallyConsistent, Func<bool> isServiceShuttingDownFunc)
		{
			this.m_targetServerName = serverName;
			this.AdLookup = adLookup;
			this.AdLookupPartiallyConsistent = adLookupPartiallyConsistent;
			this.AdSessionIgnoreInvalid = adSession;
			this.AdSessionPartiallyConsistent = adSessionPartiallyConsistent;
			this.m_isServiceShuttingDownFunc = isServiceShuttingDownFunc;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00052EAE File Offset: 0x000510AE
		public static IDisposable SetTestHook(Action testAddDbCopy)
		{
			return MonitoringADConfig.hookableTestAddDbCopy.SetTestHook(testAddDbCopy);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00052ED0 File Offset: 0x000510D0
		public static MonitoringADConfig GetConfig(AmServerName serverName, IReplayAdObjectLookup adLookup, IReplayAdObjectLookup adLookupPartiallyConsistent, IADToplogyConfigurationSession adSession, IADToplogyConfigurationSession adSessionPartiallyConsistent, Func<bool> isServiceShuttingDownFunc)
		{
			ReplayServerPerfmon.ADConfigRefreshCalls.Increment();
			ReplayServerPerfmon.ADConfigRefreshCallsPerSec.Increment();
			Stopwatch stopwatch = Stopwatch.StartNew();
			MonitoringADConfig config = new MonitoringADConfig(serverName, adLookup, adLookupPartiallyConsistent, adSession, adSessionPartiallyConsistent, isServiceShuttingDownFunc);
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				config.Refresh();
			}, 2);
			ReplayServerPerfmon.ADConfigRefreshLatency.IncrementBy(stopwatch.ElapsedTicks);
			ReplayServerPerfmon.ADConfigRefreshLatencyBase.Increment();
			ExTraceGlobals.ADCacheTracer.TraceDebug<TimeSpan>((long)config.GetHashCode(), "MonitoringADConfig.GetConfig took {0}", stopwatch.Elapsed);
			if (stopwatch.Elapsed > MonitoringADConfig.MaxHealthyADRefreshDuration)
			{
				ReplayCrimsonEvents.ADConfigRefreshWasSlow.LogPeriodic<TimeSpan>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, stopwatch.Elapsed);
			}
			if (ex != null)
			{
				MonitoringADConfig.Tracer.TraceError<string, string>((long)config.GetHashCode(), "MonitoringADConfig.GetConfig( {0} ): Got exception: {1}", serverName.NetbiosName, AmExceptionHelper.GetExceptionToStringOrNoneString(ex));
				ReplayCrimsonEvents.ADConfigRefreshFailed.LogPeriodic<string, string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, ex.ToString(), Environment.StackTrace);
				throw new MonitoringADConfigException(ex.Message, ex);
			}
			return config;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00052FEC File Offset: 0x000511EC
		public IADServer LookupMiniServerByName(AmServerName serverName)
		{
			IADServer result = null;
			if (this.m_miniServerLookup != null)
			{
				this.m_miniServerLookup.TryGetValue(serverName, out result);
			}
			return result;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00053014 File Offset: 0x00051214
		protected void Refresh()
		{
			MonitoringADConfig.Tracer.TraceDebug<AmServerName>((long)this.GetHashCode(), "MonitoringADConfig.Refresh() called against server name '{0}' ...", this.m_targetServerName);
			this.CreateTimeUtc = DateTime.UtcNow;
			this.TargetServerName = this.m_targetServerName;
			this.TargetMiniServer = this.LookupMiniServer(this.m_targetServerName.NetbiosName);
			this.CheckServiceShuttingDown();
			if (this.TargetMiniServer.DatabaseAvailabilityGroup == null)
			{
				this.ServerRole = MonitoringServerRole.Standalone;
				this.InitializeServerStructures(1);
				this.AddServerIfNeeded(this.TargetServerName, this.TargetMiniServer);
			}
			else
			{
				this.ServerRole = MonitoringServerRole.DagMember;
				this.Dag = this.LookupDag(this.TargetMiniServer.DatabaseAvailabilityGroup);
				this.CheckServiceShuttingDown();
				bool flag = false;
				this.InitializeServerStructures(this.Dag.Servers.Count);
				foreach (ADObjectId adobjectId in this.Dag.Servers)
				{
					IADServer iadserver = this.LookupMiniServer(adobjectId.Name);
					MonitoringADConfig.Tracer.TraceDebug<DatabaseCopyAutoActivationPolicyType, bool>((long)this.GetHashCode(), "MonitoringADConfig.Refresh() : Values of DatabaseCopyAutoActivationPolicy={0},  DatabaseCopyActivationDisabledAndMoveNow={1}", iadserver.DatabaseCopyAutoActivationPolicy, iadserver.DatabaseCopyActivationDisabledAndMoveNow);
					this.AddServerIfNeeded(iadserver);
					if (iadserver.Guid.Equals(this.TargetMiniServer.Guid))
					{
						flag = true;
					}
					this.CheckServiceShuttingDown();
				}
				if (!flag)
				{
					this.AddServerIfNeeded(this.TargetServerName, this.TargetMiniServer);
				}
			}
			MonitoringADConfig.hookableTestAddDbCopy.Value();
			this.LookupAndPopulateDatabases();
			DiagCore.RetailAssert(this.m_amServerNameList.Count == this.m_serverList.Count, "m_amServerNameList [{0}] != m_serverList [{1}], in length", new object[]
			{
				this.m_amServerNameList.Count,
				this.m_serverList.Count
			});
			DiagCore.RetailAssert(this.DatabaseMap.Keys.Count == this.m_serverList.Count, "DatabaseMap.Keys.Count [{0}] != m_serverList.Count [{1}]", new object[]
			{
				this.DatabaseMap.Count,
				this.m_serverList.Count
			});
			DiagCore.RetailAssert(this.DatabaseMap.ContainsKey(this.TargetServerName), "DatabaseMap should contain the target server entry!", new object[0]);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00053278 File Offset: 0x00051478
		private void LookupAndPopulateDatabases()
		{
			Dictionary<Guid, IADDatabase> dictionary = new Dictionary<Guid, IADDatabase>(Math.Min(160, this.m_serverList.Count * 20));
			Dictionary<AmServerName, IEnumerable<IADDatabase>> dictionary2 = new Dictionary<AmServerName, IEnumerable<IADDatabase>>(this.m_serverList.Count);
			Dictionary<AmServerName, IEnumerable<IADDatabase>> dictionary3 = new Dictionary<AmServerName, IEnumerable<IADDatabase>>(this.m_serverList.Count);
			Dictionary<string, IADDatabase> dictionary4 = new Dictionary<string, IADDatabase>(160, StringComparer.OrdinalIgnoreCase);
			foreach (IADServer iadserver in this.m_serverList)
			{
				AmServerName orConstructAmServerName = this.GetOrConstructAmServerName(iadserver);
				List<IADDatabase> list = new List<IADDatabase>(48);
				List<IADDatabase> list2 = new List<IADDatabase>(48);
				IEnumerable<IADDatabaseCopy> enumerable = this.LookupDatabaseCopies(iadserver);
				foreach (IADDatabaseCopy iaddatabaseCopy in enumerable)
				{
					IADDatabase iaddatabase;
					if (!dictionary4.TryGetValue(iaddatabaseCopy.DatabaseName, out iaddatabase))
					{
						iaddatabase = this.LookupDatabaseFromCopy(iaddatabaseCopy);
						if (iaddatabase == null)
						{
							MonitoringADConfig.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "MonitoringADConfig.LookupAndPopulateDatabases(): Found database copy object '{0}\\{1}' but couldn't read its parent Database object. Skipping this copy and moving to the next one on this server.", iaddatabaseCopy.DatabaseName, iaddatabaseCopy.Name);
							continue;
						}
						dictionary4[iaddatabaseCopy.DatabaseName] = iaddatabase;
					}
					if (this.GetDatabaseCopyFromDb(iaddatabase, iaddatabaseCopy) == null)
					{
						MonitoringADConfig.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "MonitoringADConfig.LookupAndPopulateDatabases(): Found database copy object '{0}\\{1}' that the cached DB object doesn't have yet. Skipping this copy and moving to the next one on this server.", iaddatabaseCopy.DatabaseName, iaddatabaseCopy.Name);
					}
					else
					{
						list2.Add(iaddatabase);
						bool isValid = iaddatabaseCopy.IsValid;
						bool isValid2 = iaddatabase.IsValid;
						if (isValid && isValid2)
						{
							if (!dictionary.ContainsKey(iaddatabase.Guid))
							{
								dictionary[iaddatabase.Guid] = iaddatabase;
							}
							list.Add(iaddatabase);
						}
						else if (!isValid)
						{
							MonitoringADConfig.Tracer.TraceError<string, string>((long)this.GetHashCode(), "MonitoringADConfig.LookupAndPopulateDatabases(): Found invalid database copy object '{0}\\{1}'", iaddatabase.Name, iaddatabaseCopy.Name);
						}
						else if (!isValid2)
						{
							MonitoringADConfig.Tracer.TraceError<string>((long)this.GetHashCode(), "MonitoringADConfig.LookupAndPopulateDatabases(): Found invalid database object '{0}'", iaddatabase.Name);
						}
						foreach (IADDatabaseCopy iaddatabaseCopy2 in iaddatabase.AllDatabaseCopies)
						{
							AmServerName orConstructAmServerName2 = this.GetOrConstructAmServerName(iaddatabaseCopy2.Name);
							if (!this.m_miniServerLookup.ContainsKey(orConstructAmServerName2))
							{
								this.ExcludeDatabaseCopy(iaddatabase, orConstructAmServerName2);
							}
						}
						this.CheckServiceShuttingDown();
					}
				}
				dictionary2.Add(orConstructAmServerName, list);
				dictionary3.Add(orConstructAmServerName, list2);
				MonitoringADConfig.Tracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "MonitoringADConfig.LookupAndPopulateDatabases(): Server ( {0} ) found: Valid DB Copies ({1}), All DB Copies ({2}).", iadserver.Name, list.Count, list2.Count);
				this.CheckServiceShuttingDown();
			}
			this.DatabaseMap = dictionary2;
			this.DatabaseByGuidMap = dictionary;
			this.DatabasesIncludingMisconfiguredMap = dictionary3;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0005356C File Offset: 0x0005176C
		private IADDatabaseCopy GetDatabaseCopyFromDb(IADDatabase database, IADDatabaseCopy dbCopy)
		{
			foreach (IADDatabaseCopy iaddatabaseCopy in database.AllDatabaseCopies)
			{
				if (iaddatabaseCopy.Guid.Equals(dbCopy.Guid))
				{
					return iaddatabaseCopy;
				}
			}
			return null;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000535B0 File Offset: 0x000517B0
		private void ExcludeDatabaseCopy(IADDatabase db, AmServerName tmpServer)
		{
			string netbiosName = tmpServer.NetbiosName;
			string name = db.Name;
			IADDatabaseCopy[] allDatabaseCopies = db.AllDatabaseCopies;
			MonitoringADConfig.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "MonitoringADConfig.Refresh(): Found database copy '{0}\\{1}' for which the host server is not yet in the DAG members list. Excluding this database copy from its database object.", name, netbiosName);
			db.ExcludeDatabaseCopyFromProperties(netbiosName);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000535F1 File Offset: 0x000517F1
		private void InitializeServerStructures(int serverCount)
		{
			this.m_serverList = new List<IADServer>(serverCount);
			this.m_amServerNameList = new List<AmServerName>(serverCount);
			this.m_miniServerLookup = new Dictionary<AmServerName, IADServer>(serverCount);
			this.m_amServerLookup = new Dictionary<string, AmServerName>(serverCount, AmServerName.Comparer);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00053628 File Offset: 0x00051828
		private void AddServerIfNeeded(IADServer miniServerToAdd)
		{
			AmServerName orConstructAmServerName = this.GetOrConstructAmServerName(miniServerToAdd);
			this.AddServerIfNeeded(orConstructAmServerName, miniServerToAdd);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00053648 File Offset: 0x00051848
		private void AddServerIfNeeded(AmServerName serverToAdd)
		{
			IADServer miniServerToAdd = this.LookupMiniServer(serverToAdd.NetbiosName);
			this.AddServerIfNeeded(serverToAdd, miniServerToAdd);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0005366C File Offset: 0x0005186C
		private void AddServerIfNeeded(AmServerName serverToAdd, IADServer miniServerToAdd)
		{
			if (!this.m_miniServerLookup.ContainsKey(serverToAdd))
			{
				this.m_miniServerLookup.Add(serverToAdd, miniServerToAdd);
				this.m_serverList.Add(miniServerToAdd);
				this.m_amServerNameList.Add(serverToAdd);
			}
			if (!this.m_amServerLookup.ContainsKey(serverToAdd.NetbiosName))
			{
				this.m_amServerLookup.Add(serverToAdd.NetbiosName, serverToAdd);
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000536D1 File Offset: 0x000518D1
		private void CheckServiceShuttingDown()
		{
			if (this.m_isServiceShuttingDownFunc != null && this.m_isServiceShuttingDownFunc())
			{
				throw new MonitoringADServiceShuttingDownException();
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000536EE File Offset: 0x000518EE
		private AmServerName GetOrConstructAmServerName(IADServer miniServer)
		{
			return this.GetOrConstructAmServerName(miniServer.Name);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000536FC File Offset: 0x000518FC
		private AmServerName GetOrConstructAmServerName(string serverName)
		{
			AmServerName amServerName = null;
			if (!this.m_amServerLookup.TryGetValue(serverName, out amServerName))
			{
				amServerName = new AmServerName(serverName, false);
				this.m_amServerLookup[serverName] = amServerName;
			}
			return amServerName;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00053734 File Offset: 0x00051934
		protected virtual IADServer LookupMiniServer(string serverShortName)
		{
			Exception ex = null;
			IADServer iadserver = this.AdLookup.MiniServerLookup.FindMiniServerByShortNameEx(serverShortName, out ex);
			if (iadserver == null)
			{
				MonitoringADConfig.Tracer.TraceError<string, string>((long)this.GetHashCode(), "LookupMiniServer( {0} ): Got exception: {1}", serverShortName, AmExceptionHelper.GetExceptionToStringOrNoneString(ex));
				throw new MonitoringCouldNotFindMiniServerException(serverShortName, ex);
			}
			MonitoringADConfig.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LookupMiniServer( {0} ): Found MiniServer object.", serverShortName);
			return iadserver;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00053798 File Offset: 0x00051998
		protected virtual IADDatabaseAvailabilityGroup LookupDag(ADObjectId dagObjectId)
		{
			Exception ex = null;
			IADDatabaseAvailabilityGroup iaddatabaseAvailabilityGroup = this.AdLookup.DagLookup.ReadAdObjectByObjectIdEx(dagObjectId, out ex);
			if (iaddatabaseAvailabilityGroup == null)
			{
				MonitoringADConfig.Tracer.TraceError<ADObjectId, string>((long)this.GetHashCode(), "LookupDag( {0} ): Got exception: {1}", dagObjectId, AmExceptionHelper.GetExceptionToStringOrNoneString(ex));
				throw new MonitoringCouldNotFindDagException(dagObjectId.Name, AmExceptionHelper.GetExceptionMessageOrNoneString(ex), ex);
			}
			MonitoringADConfig.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "LookupDag( {0} ): Found DAG object.", dagObjectId);
			return iaddatabaseAvailabilityGroup;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00053808 File Offset: 0x00051A08
		protected virtual IADDatabase LookupDatabaseFromCopy(IADDatabaseCopy dbCopy)
		{
			return this.AdLookup.DatabaseLookup.ReadAdObjectByObjectId(dbCopy.Id.Parent);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00053854 File Offset: 0x00051A54
		protected virtual IEnumerable<IADDatabaseCopy> LookupDatabaseCopies(IADServer miniServer)
		{
			MonitoringADConfig.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LookupDatabases ( {0} ): Searching for all valid/invalid database copies...", miniServer.Name);
			IEnumerable<IADDatabaseCopy> dbCopies = null;
			IADToplogyConfigurationSession adSession = this.AdSessionPartiallyConsistent;
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				dbCopies = adSession.GetAllDatabaseCopies(miniServer);
			}, 2);
			if (ex != null)
			{
				MonitoringADConfig.Tracer.TraceError<string, string>((long)this.GetHashCode(), "LookupDatabases ( {0} ): Got exception: {1}", miniServer.Name, AmExceptionHelper.GetExceptionToStringOrNoneString(ex));
				throw new MonitoringCouldNotFindDatabasesException(miniServer.Name, ex.Message, ex);
			}
			if (dbCopies == null)
			{
				dbCopies = new IADDatabaseCopy[0];
			}
			return dbCopies;
		}

		// Token: 0x0400081A RID: 2074
		private static Hookable<Action> hookableTestAddDbCopy = Hookable<Action>.Create(true, delegate()
		{
		});

		// Token: 0x0400081B RID: 2075
		private AmServerName m_targetServerName;

		// Token: 0x0400081C RID: 2076
		private static readonly TimeSpan MaxHealthyADRefreshDuration = TimeSpan.FromSeconds(5.0);

		// Token: 0x0400081D RID: 2077
		private Dictionary<AmServerName, IADServer> m_miniServerLookup;

		// Token: 0x0400081E RID: 2078
		private Dictionary<string, AmServerName> m_amServerLookup;

		// Token: 0x0400081F RID: 2079
		private List<IADServer> m_serverList;

		// Token: 0x04000820 RID: 2080
		private List<AmServerName> m_amServerNameList;

		// Token: 0x04000821 RID: 2081
		private Func<bool> m_isServiceShuttingDownFunc;
	}
}
