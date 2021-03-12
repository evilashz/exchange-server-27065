using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000013 RID: 19
	internal class AmCachedLastLogUpdater : TimerComponent
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00005000 File Offset: 0x00003200
		internal AmCachedLastLogUpdater() : base(TimeSpan.FromSeconds((double)RegistryParameters.PamLastLogUpdaterIntervalInSec), TimeSpan.FromSeconds((double)RegistryParameters.PamLastLogUpdaterIntervalInSec), "AmCachedLastLogUpdater")
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005054 File Offset: 0x00003254
		internal ExDateTime AddEntries(string serverNameFqdn, DateTime initiatedTimeUtc, KeyValuePair<Guid, long>[] lastLogEntries)
		{
			AmServerName amServerName = new AmServerName(serverNameFqdn);
			lock (this.locker)
			{
				AmCachedLastLogUpdater.ServerRequestInfo serverRequestInfo;
				if (!this.serverPropertyMap.TryGetValue(amServerName, out serverRequestInfo))
				{
					serverRequestInfo = new AmCachedLastLogUpdater.ServerRequestInfo(amServerName);
					this.serverPropertyMap[amServerName] = serverRequestInfo;
				}
				serverRequestInfo.Update(initiatedTimeUtc, lastLogEntries);
			}
			return this.GetLastUpdatedTime(amServerName);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000050C8 File Offset: 0x000032C8
		private ExDateTime GetLastUpdatedTime(AmServerName serverName)
		{
			ExDateTime minValue;
			if (!this.serverUpdateTimeMap.TryGetValue(serverName, out minValue))
			{
				minValue = ExDateTime.MinValue;
			}
			return minValue;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000050EC File Offset: 0x000032EC
		internal Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo> Cleanup()
		{
			Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo> result;
			lock (this.locker)
			{
				Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo> dictionary = this.serverPropertyMap;
				this.serverPropertyMap = new Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo>(32);
				result = dictionary;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005140 File Offset: 0x00003340
		protected override void TimerCallbackInternal()
		{
			this.Flush();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005148 File Offset: 0x00003348
		internal void Flush()
		{
			if (AmSystemManager.Instance.Config.IsPAM)
			{
				Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo> dictionary = this.Cleanup();
				if (dictionary.Count > 0)
				{
					Exception ex = null;
					try
					{
						using (IClusterDB clusterDB = ClusterDB.Open())
						{
							if (clusterDB.IsInitialized)
							{
								using (IClusterDBWriteBatch clusterDBWriteBatch = clusterDB.CreateWriteBatch("ExchangeActiveManager\\LastLog"))
								{
									this.PopulateBatch(clusterDBWriteBatch, dictionary);
									clusterDBWriteBatch.Execute();
									ExDateTime now = ExDateTime.Now;
									foreach (AmServerName key in dictionary.Keys)
									{
										this.serverUpdateTimeMap[key] = now;
									}
									goto IL_BA;
								}
							}
							ExTraceGlobals.ClusterTracer.TraceError((long)this.GetHashCode(), "Flush(): clusdb is not initialized");
							IL_BA:;
						}
					}
					catch (ADExternalException ex2)
					{
						ex = ex2;
					}
					catch (ADOperationException ex3)
					{
						ex = ex3;
					}
					catch (ADTransientException ex4)
					{
						ex = ex4;
					}
					catch (ClusterException ex5)
					{
						ex = ex5;
					}
					catch (AmServerException ex6)
					{
						ex = ex6;
					}
					catch (AmServerTransientException ex7)
					{
						ex = ex7;
					}
					if (ex != null)
					{
						ReplayCrimsonEvents.CachedLastLogUpdateFailed.LogPeriodic<int, string>(AmServerName.LocalComputerName.NetbiosName, TimeSpan.FromMinutes(5.0), dictionary.Count, ex.Message);
					}
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000052F4 File Offset: 0x000034F4
		private void PopulateBatch(IClusterDBWriteBatch writeBatch, Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo> requestInfoMap)
		{
			Dictionary<Guid, AmDbStateInfo> dbStateInfoMap = new Dictionary<Guid, AmDbStateInfo>();
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsPAM)
			{
				throw new AmInvalidConfiguration(string.Format("Role = {0}", config.Role));
			}
			AmDbStateInfo[] array = config.DbState.ReadAll();
			if (array != null)
			{
				dbStateInfoMap = array.ToDictionary((AmDbStateInfo s) => s.DatabaseGuid);
			}
			foreach (AmCachedLastLogUpdater.ServerRequestInfo serverRequestInfo in requestInfoMap.Values)
			{
				AmServerName serverName = serverRequestInfo.ServerName;
				Dictionary<Guid, long> databaseLogGenMap = serverRequestInfo.DatabaseLogGenMap;
				HashSet<Guid> databasesByServer = this.GetDatabasesByServer(serverName);
				string value = serverRequestInfo.MostRecentRequestReceivedUtc.ToString("s");
				foreach (KeyValuePair<Guid, long> keyValuePair in databaseLogGenMap)
				{
					Guid key = keyValuePair.Key;
					long value2 = keyValuePair.Value;
					string text = key.ToString();
					string valueName = AmDbState.ConstructLastLogTimeStampProperty(text);
					if (this.IsDatabaseActiveOnServer(dbStateInfoMap, key, serverName))
					{
						writeBatch.SetValue(text, value2.ToString());
						writeBatch.SetValue(valueName, value);
					}
					databasesByServer.Remove(key);
				}
				foreach (Guid databaseGuid in databasesByServer)
				{
					string valueName2 = AmDbState.ConstructLastLogTimeStampProperty(databaseGuid.ToString());
					if (this.IsDatabaseActiveOnServer(dbStateInfoMap, databaseGuid, serverName))
					{
						writeBatch.SetValue(valueName2, value);
					}
				}
				writeBatch.SetValue(serverName.NetbiosName, value);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005508 File Offset: 0x00003708
		private HashSet<Guid> GetDatabasesByServer(AmServerName serverName)
		{
			HashSet<Guid> hashSet = new HashSet<Guid>();
			IADConfig adconfig = Dependencies.ADConfig;
			IADServer server = adconfig.GetServer(serverName);
			if (server != null)
			{
				IEnumerable<IADDatabase> databasesOnServer = adconfig.GetDatabasesOnServer(serverName);
				if (databasesOnServer == null)
				{
					return hashSet;
				}
				using (IEnumerator<IADDatabase> enumerator = databasesOnServer.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IADDatabase iaddatabase = enumerator.Current;
						hashSet.Add(iaddatabase.Guid);
					}
					return hashSet;
				}
			}
			ExTraceGlobals.ClusterTracer.TraceError<AmServerName>((long)this.GetHashCode(), "MiniSever object is null when querying for server '{0}'", serverName);
			return hashSet;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005598 File Offset: 0x00003798
		private bool IsDatabaseActiveOnServer(Dictionary<Guid, AmDbStateInfo> dbStateInfoMap, Guid databaseGuid, AmServerName serverName)
		{
			AmDbStateInfo amDbStateInfo;
			return dbStateInfoMap != null && dbStateInfoMap.TryGetValue(databaseGuid, out amDbStateInfo) && amDbStateInfo != null && amDbStateInfo.IsActiveServerValid && AmServerName.IsEqual(amDbStateInfo.ActiveServer, serverName);
		}

		// Token: 0x04000043 RID: 67
		private Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo> serverPropertyMap = new Dictionary<AmServerName, AmCachedLastLogUpdater.ServerRequestInfo>(32);

		// Token: 0x04000044 RID: 68
		private readonly ConcurrentDictionary<AmServerName, ExDateTime> serverUpdateTimeMap = new ConcurrentDictionary<AmServerName, ExDateTime>();

		// Token: 0x04000045 RID: 69
		private readonly object locker = new object();

		// Token: 0x02000014 RID: 20
		internal class ServerRequestInfo
		{
			// Token: 0x060000B7 RID: 183 RVA: 0x000055CF File Offset: 0x000037CF
			internal ServerRequestInfo(AmServerName serverName)
			{
				this.ServerName = serverName;
				this.MostRecentRequestReceivedUtc = SharedHelper.DateTimeMinValueUtc;
				this.DatabaseLogGenMap = new Dictionary<Guid, long>();
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x000055F4 File Offset: 0x000037F4
			internal void Update(DateTime initiatedTimeUtc, KeyValuePair<Guid, long>[] lastLogEntries)
			{
				this.MostRecentRequestReceivedUtc = initiatedTimeUtc;
				foreach (KeyValuePair<Guid, long> keyValuePair in lastLogEntries)
				{
					Guid key = keyValuePair.Key;
					long value = keyValuePair.Value;
					long num = 0L;
					if (!this.DatabaseLogGenMap.TryGetValue(key, out num) || value > num)
					{
						this.DatabaseLogGenMap[key] = value;
					}
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005661 File Offset: 0x00003861
			// (set) Token: 0x060000BA RID: 186 RVA: 0x00005669 File Offset: 0x00003869
			internal AmServerName ServerName { get; private set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000BB RID: 187 RVA: 0x00005672 File Offset: 0x00003872
			// (set) Token: 0x060000BC RID: 188 RVA: 0x0000567A File Offset: 0x0000387A
			internal DateTime MostRecentRequestReceivedUtc { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00005683 File Offset: 0x00003883
			// (set) Token: 0x060000BE RID: 190 RVA: 0x0000568B File Offset: 0x0000388B
			internal Dictionary<Guid, long> DatabaseLogGenMap { get; set; }
		}
	}
}
