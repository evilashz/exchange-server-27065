using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Cluster.Shared.Serialization;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001C5 RID: 453
	internal class DbCopyHealthInfoInternalTable : ReaderWriterLockedBase
	{
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x000498C0 File Offset: 0x00047AC0
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DatabaseHealthTrackerTracer;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x000498C7 File Offset: 0x00047AC7
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x000498CF File Offset: 0x00047ACF
		public DateTime CreateTimeUtc { get; set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x000498D8 File Offset: 0x00047AD8
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x000498E0 File Offset: 0x00047AE0
		public DateTime LastUpdateTimeUtc { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000498E9 File Offset: 0x00047AE9
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x000498F1 File Offset: 0x00047AF1
		public bool IsFileNotReadAtInitialization { get; private set; }

		// Token: 0x060011C6 RID: 4550 RVA: 0x000498FA File Offset: 0x00047AFA
		public DbCopyHealthInfoInternalTable(string persistedFilePath)
		{
			this.m_filePath = persistedFilePath;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00049964 File Offset: 0x00047B64
		public void ReportServerFoundInAD(AmServerName serverName)
		{
			base.WriterLockedOperation(delegate
			{
				ServerHealthInfo orAddServerHealthInfo = this.GetOrAddServerHealthInfo(serverName);
				this.ReportTransition(orAddServerHealthInfo.ServerFoundInAD, true);
			});
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00049A30 File Offset: 0x00047C30
		public void ReportDbCopyFoundInAD(IADDatabase db, AmServerName serverName)
		{
			base.WriterLockedOperation(delegate
			{
				DbHealthInfo orAddDbHealthInfo = this.GetOrAddDbHealthInfo(db.Guid, db.Name);
				this.ReportTransition(orAddDbHealthInfo.DbFoundInAD, true);
				bool conditionMet = !DatabaseHealthMonitor.ShouldMonitorDatabase(db);
				this.ReportTransition(orAddDbHealthInfo.SkippedFromMonitoring, conditionMet);
				DbCopyHealthInfo orAddDbCopy = orAddDbHealthInfo.GetOrAddDbCopy(serverName);
				DateTime utcNow = DateTime.UtcNow;
				orAddDbCopy.LastTouchedTime = utcNow;
				this.ReportTransition(orAddDbCopy.CopyFoundInAD, true);
			});
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00049B94 File Offset: 0x00047D94
		public void ReportDbCopyStatusFound(Guid dbGuid, string dbName, AmServerName serverName, CopyStatusClientCachedEntry status)
		{
			base.WriterLockedOperation(delegate
			{
				DbCopyHealthInfo orAddDbCopyHealthInfo = this.GetOrAddDbCopyHealthInfo(dbGuid, dbName, serverName);
				orAddDbCopyHealthInfo.LastTouchedTime = DateTime.UtcNow;
				if (status.Result == CopyStatusRpcResult.Success)
				{
					orAddDbCopyHealthInfo.CopyStatusRetrieved.ReportSuccess();
					orAddDbCopyHealthInfo.LastCopyStatusTransitionTime = status.CopyStatus.LastStatusTransitionTime;
					this.ReportTransition(orAddDbCopyHealthInfo.CopyIsAvailable, status.CopyStatus.IsLastCopyAvailabilityChecksPassed);
					this.ReportTransition(orAddDbCopyHealthInfo.CopyIsRedundant, status.CopyStatus.IsLastCopyRedundancyChecksPassed);
					this.ReportTransition(orAddDbCopyHealthInfo.CopyStatusHealthy, status.CopyStatus.CopyStatus == CopyStatusEnum.Healthy);
					this.ReportTransition(orAddDbCopyHealthInfo.CopyStatusActive, status.CopyStatus.IsActiveCopy());
					this.ReportTransition(orAddDbCopyHealthInfo.CopyStatusMounted, status.CopyStatus.CopyStatus == CopyStatusEnum.Mounted);
					return;
				}
				orAddDbCopyHealthInfo.CopyStatusRetrieved.ReportFailure();
			});
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00049F40 File Offset: 0x00048140
		public void PossiblyReportObjectsNotFoundInAD(IMonitoringADConfig adConfig)
		{
			base.WriterLockedOperation(delegate
			{
				List<AmServerName> list = new List<AmServerName>();
				List<Guid> list2 = new List<Guid>();
				List<Tuple<DbHealthInfo, DbCopyHealthInfo>> list3 = new List<Tuple<DbHealthInfo, DbCopyHealthInfo>>();
				foreach (ServerHealthInfo serverHealthInfo in this.m_serverInfos.Values)
				{
					if (adConfig.LookupMiniServerByName(serverHealthInfo.ServerName) == null)
					{
						this.ReportTransition(serverHealthInfo.ServerFoundInAD, false);
						if (this.IsADObjectMissingTooLong(serverHealthInfo.ServerFoundInAD.FailedDuration))
						{
							list.Add(serverHealthInfo.ServerName);
						}
					}
				}
				foreach (KeyValuePair<Guid, DbHealthInfo> keyValuePair in this.m_dbServerInfos)
				{
					Guid key = keyValuePair.Key;
					DbHealthInfo value = keyValuePair.Value;
					IADDatabase iaddatabase = null;
					bool flag = true;
					if (adConfig.DatabaseByGuidMap.TryGetValue(key, out iaddatabase))
					{
						flag = false;
					}
					foreach (DbCopyHealthInfo dbCopyHealthInfo in value.DbServerInfos.Values)
					{
						bool flag2 = true;
						if (!flag)
						{
							IADDatabaseCopy databaseCopy = iaddatabase.GetDatabaseCopy(dbCopyHealthInfo.ServerName.NetbiosName);
							if (databaseCopy != null)
							{
								flag2 = false;
							}
						}
						if (flag2)
						{
							this.ReportTransition(dbCopyHealthInfo.CopyFoundInAD, false);
							if (this.IsADObjectMissingTooLong(dbCopyHealthInfo.CopyFoundInAD.FailedDuration))
							{
								list3.Add(new Tuple<DbHealthInfo, DbCopyHealthInfo>(value, dbCopyHealthInfo));
							}
						}
					}
					if (flag)
					{
						this.ReportTransition(value.DbFoundInAD, false);
						if (this.IsADObjectMissingTooLong(value.DbFoundInAD.FailedDuration))
						{
							list2.Add(key);
						}
					}
				}
				if (list.Count > 0)
				{
					foreach (AmServerName key2 in list)
					{
						this.m_serverInfos.Remove(key2);
					}
				}
				if (list3.Count > 0)
				{
					foreach (Tuple<DbHealthInfo, DbCopyHealthInfo> tuple in list3)
					{
						tuple.Item1.RemoveDbCopy(tuple.Item2.ServerName);
					}
				}
				if (list2.Count > 0)
				{
					foreach (Guid key3 in list2)
					{
						this.m_dbServerInfos.Remove(key3);
					}
				}
			});
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004A2BC File Offset: 0x000484BC
		public void UpdateAvailabilityRedundancyStates(IMonitoringADConfig adConfig)
		{
			base.WriterLockedOperation(delegate
			{
				foreach (DbHealthInfo dbHealthInfo in this.m_dbServerInfos.Values)
				{
					dbHealthInfo.UpdateAvailabilityRedundancyStates();
				}
				foreach (ServerHealthInfo serverHealthInfo in this.m_serverInfos.Values)
				{
					AmServerName serverName = serverHealthInfo.ServerName;
					IEnumerable<IADDatabase> dbs;
					if (!adConfig.DatabaseMap.TryGetValue(serverName, out dbs))
					{
						dbs = new IADDatabase[0];
					}
					bool conditionMet = this.IsAnyDbMatchingCriteria(serverHealthInfo, dbs, (DbHealthInfo dbhi, DbCopyHealthInfo serverCopy) => dbhi.AvailabilityCount <= 1 && serverCopy.IsAvailable());
					this.ReportTransition(serverHealthInfo.CriticalForMaintainingAvailability, conditionMet);
					bool conditionMet2 = this.IsAnyDbMatchingCriteria(serverHealthInfo, dbs, (DbHealthInfo dbhi, DbCopyHealthInfo serverCopy) => dbhi.RedundancyCount <= 2 && serverCopy.IsRedundant());
					this.ReportTransition(serverHealthInfo.CriticalForMaintainingRedundancy, conditionMet2);
					bool conditionMet3 = this.IsAnyDbMatchingCriteria(serverHealthInfo, dbs, (DbHealthInfo dbhi, DbCopyHealthInfo serverCopy) => dbhi.AvailabilityCount <= 1 && !serverCopy.IsAvailable());
					this.ReportTransition(serverHealthInfo.CriticalForRestoringAvailability, conditionMet3);
					bool conditionMet4 = this.IsAnyDbMatchingCriteria(serverHealthInfo, dbs, (DbHealthInfo dbhi, DbCopyHealthInfo serverCopy) => dbhi.RedundancyCount <= 2 && !serverCopy.IsRedundant());
					this.ReportTransition(serverHealthInfo.CriticalForRestoringRedundancy, conditionMet4);
					int num = this.CountOfDbsMatchingCriteria(serverHealthInfo, dbs, (DbHealthInfo dbhi, DbCopyHealthInfo serverCopy) => dbhi.AvailabilityCount == 2 && !serverCopy.IsAvailable());
					this.ReportTransition(serverHealthInfo.HighForRestoringAvailability, num > adConfig.Dag.AutoDagDatabaseCopiesPerVolume);
					DbCopyHealthInfoInternalTable.Tracer.TraceDebug<AmServerName, int>((long)this.GetHashCode(), "UpdateAvailabilityRedundancyStates(): For server '{0}', found {1} copies for HighForRestoringAvailability state.", serverName, num);
					int num2 = this.CountOfDbsMatchingCriteria(serverHealthInfo, dbs, (DbHealthInfo dbhi, DbCopyHealthInfo serverCopy) => dbhi.RedundancyCount == 3 && !serverCopy.IsRedundant());
					this.ReportTransition(serverHealthInfo.HighForRestoringRedundancy, num2 > adConfig.Dag.AutoDagDatabaseCopiesPerVolume);
					DbCopyHealthInfoInternalTable.Tracer.TraceDebug<AmServerName, int>((long)this.GetHashCode(), "UpdateAvailabilityRedundancyStates(): For server '{0}', found {1} copies for HighForRestoringRedundancy state.", serverName, num2);
				}
			});
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004A2F0 File Offset: 0x000484F0
		public Exception PersistHealthInfoToXml()
		{
			DbCopyHealthInfoInternalTable.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthTracker: Writing out the health info to file '{0}'", this.m_filePath);
			HealthInfoPersisted toSerialize = this.ConvertToHealthInfoPersisted();
			Exception ex = DataContractSerializeHelper.SerializeToXmlFile(toSerialize, this.m_filePath);
			DbCopyHealthInfoInternalTable.Tracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "DatabaseHealthTracker: Finished writing out the health info to file '{0}'. Exception: {1}", this.m_filePath, ex);
			return ex;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004A424 File Offset: 0x00048624
		public HealthInfoPersisted ConvertToHealthInfoPersisted()
		{
			HealthInfoPersisted hi = new HealthInfoPersisted();
			hi.CreateTimeUtcStr = DateTimeHelper.ToPersistedString(this.CreateTimeUtc);
			hi.LastUpdateTimeUtcStr = DateTimeHelper.ToPersistedString(this.LastUpdateTimeUtc);
			base.ReaderLockedOperation(delegate
			{
				foreach (KeyValuePair<Guid, DbHealthInfo> keyValuePair in this.m_dbServerInfos)
				{
					DbHealthInfoPersisted item = keyValuePair.Value.ConvertToSerializable();
					hi.Databases.Add(item);
				}
				foreach (ServerHealthInfo serverHealthInfo in this.m_serverInfos.Values)
				{
					ServerHealthInfoPersisted item2 = serverHealthInfo.ConvertToSerializable();
					hi.Servers.Add(item2);
				}
			});
			return hi;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0004A490 File Offset: 0x00048690
		public Exception InitializeHealthInfoFromXML()
		{
			DbCopyHealthInfoInternalTable.Tracer.TraceDebug<string>((long)this.GetHashCode(), "InitializeHealthInfoFromXML: Initializing the health info table from file '{0}'", this.m_filePath);
			HealthInfoPersisted healthInfoPersisted;
			Exception ex = DataContractSerializeHelper.DeserializeFromXmlFile<HealthInfoPersisted>(this.m_filePath, out healthInfoPersisted);
			if (ex == null)
			{
				DbCopyHealthInfoInternalTable.Tracer.TraceDebug((long)this.GetHashCode(), "InitializeHealthInfoFromXML: Successfully deserialized the XML file.");
				DateTime lastUpdateTimeUtc = healthInfoPersisted.GetLastUpdateTimeUtc();
				TimeSpan timeSpan = DateTimeHelper.SafeSubtract(DateTime.UtcNow, lastUpdateTimeUtc);
				if (timeSpan > DatabaseHealthTracker.LastUpdateTimeDiffThreshold)
				{
					DbCopyHealthInfoInternalTable.Tracer.TraceError<TimeSpan, TimeSpan, DateTime>((long)this.GetHashCode(), "InitializeHealthInfoFromXML: The file was last modified too long ago at '{2}'! lastUpdateTimeDiff = '{0}', Threshold = '{1}'", timeSpan, DatabaseHealthTracker.LastUpdateTimeDiffThreshold, lastUpdateTimeUtc);
					ReplayCrimsonEvents.DHTInitFromFileTooOld.Log<DateTime, TimeSpan, TimeSpan>(lastUpdateTimeUtc, timeSpan, DatabaseHealthTracker.LastUpdateTimeDiffThreshold);
				}
				this.InitializeFromHealthInfoPersisted(healthInfoPersisted);
				return null;
			}
			this.CreateTimeUtc = DateTime.UtcNow;
			this.IsFileNotReadAtInitialization = true;
			if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
			{
				DbCopyHealthInfoInternalTable.Tracer.TraceDebug((long)this.GetHashCode(), "InitializeHealthInfoFromXML: Initialization of health info table will be skipped because the XML file was not found.");
				DbCopyHealthInfoInternalTable.Tracer.TraceDebug<DateTime>((long)this.GetHashCode(), "A new health table is possibly being created. CreateTimeUtc = {0}", this.CreateTimeUtc);
				ReplayCrimsonEvents.DHTInitFileNotFound.Log<string>(this.m_filePath);
				return null;
			}
			DbCopyHealthInfoInternalTable.Tracer.TraceError<Exception>((long)this.GetHashCode(), "InitializeHealthInfoFromXML: Initialization of health info table failed because the XML file could not be deserialized. Error: {0}", ex);
			return ex;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004A708 File Offset: 0x00048908
		public void InitializeFromHealthInfoPersisted(HealthInfoPersisted hip)
		{
			if (hip == null)
			{
				DbCopyHealthInfoInternalTable.Tracer.TraceDebug((long)this.GetHashCode(), "InitializeFromHealthInfoPersisted() received <NULL> for 'hip'. Ignoring and starting a new health info table.");
				return;
			}
			base.WriterLockedOperation(delegate
			{
				this.CreateTimeUtc = this.ParseIntoDateTime(hip.CreateTimeUtcStr);
				this.LastUpdateTimeUtc = this.ParseIntoDateTime(hip.LastUpdateTimeUtcStr);
				DbCopyHealthInfoInternalTable.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "InitializeHealthInfoFromXML: LastUpdateTimeUtc from XML file: {0}, CreateTimeUtc: {1}", DateTimeHelper.ToStringInvariantCulture(this.LastUpdateTimeUtc), DateTimeHelper.ToStringInvariantCulture(this.CreateTimeUtc));
				foreach (DbHealthInfoPersisted dbHealthInfoPersisted in hip.Databases)
				{
					DbHealthInfo dbHealthInfo = this.AddNewDbHealthInfo(dbHealthInfoPersisted.DbGuid, dbHealthInfoPersisted.DbName);
					dbHealthInfo.InitializeFromSerializable(dbHealthInfoPersisted);
				}
				foreach (ServerHealthInfoPersisted serverHealthInfoPersisted in hip.Servers)
				{
					ServerHealthInfo serverHealthInfo = this.AddNewServerHealthInfo(serverHealthInfoPersisted.ServerFqdn);
					serverHealthInfo.InitializeFromSerializable(serverHealthInfoPersisted);
				}
			});
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004A75C File Offset: 0x0004895C
		private DateTime ParseIntoDateTime(string dateTimeStr)
		{
			DateTime minValue = DateTime.MinValue;
			DateTimeHelper.ParseIntoDateTimeIfPossible(dateTimeStr, ref minValue);
			return minValue;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004A778 File Offset: 0x00048978
		private DbCopyHealthInfo GetOrAddDbCopyHealthInfo(Guid dbGuid, string dbName, AmServerName serverName)
		{
			DbHealthInfo orAddDbHealthInfo = this.GetOrAddDbHealthInfo(dbGuid, dbName);
			return orAddDbHealthInfo.GetOrAddDbCopy(serverName);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004A798 File Offset: 0x00048998
		private ServerHealthInfo GetOrAddServerHealthInfo(AmServerName serverName)
		{
			ServerHealthInfo serverHealthInfo;
			if (!this.m_serverInfos.ContainsKey(serverName))
			{
				serverHealthInfo = new ServerHealthInfo(serverName);
				this.m_serverInfos[serverName] = serverHealthInfo;
			}
			else
			{
				serverHealthInfo = this.m_serverInfos[serverName];
			}
			return serverHealthInfo;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004A7DC File Offset: 0x000489DC
		private ServerHealthInfo AddNewServerHealthInfo(string serverFqdn)
		{
			AmServerName amServerName = new AmServerName(serverFqdn);
			ServerHealthInfo serverHealthInfo = new ServerHealthInfo(amServerName);
			this.m_serverInfos[amServerName] = serverHealthInfo;
			return serverHealthInfo;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004A808 File Offset: 0x00048A08
		private DbHealthInfo GetOrAddDbHealthInfo(Guid dbGuid, string dbName)
		{
			DbHealthInfo dbHealthInfo;
			if (!this.m_dbServerInfos.ContainsKey(dbGuid))
			{
				dbHealthInfo = new DbHealthInfo(dbGuid, dbName);
				this.m_dbServerInfos[dbGuid] = dbHealthInfo;
			}
			else
			{
				dbHealthInfo = this.m_dbServerInfos[dbGuid];
				dbHealthInfo.DbName = dbName;
			}
			return dbHealthInfo;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004A854 File Offset: 0x00048A54
		private DbHealthInfo AddNewDbHealthInfo(Guid dbGuid, string dbName)
		{
			DbHealthInfo dbHealthInfo = new DbHealthInfo(dbGuid, dbName);
			this.m_dbServerInfos[dbGuid] = dbHealthInfo;
			return dbHealthInfo;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004A877 File Offset: 0x00048A77
		private void ReportTransition(StateTransitionInfo state, bool conditionMet)
		{
			if (conditionMet)
			{
				state.ReportSuccess();
				return;
			}
			state.ReportFailure();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004A889 File Offset: 0x00048A89
		private bool IsADObjectMissingTooLong(TimeSpan missingDuration)
		{
			return missingDuration > DbCopyHealthInfoInternalTable.MissingObjectAgeThreshold;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004A898 File Offset: 0x00048A98
		private bool DatabaseMatchPredicateWrapper(IADDatabase db, AmServerName serverName, Func<DbHealthInfo, DbCopyHealthInfo, bool> matchPredicate)
		{
			DbHealthInfo dbHealthInfo = this.m_dbServerInfos[db.Guid];
			DbCopyHealthInfo dbCopy = dbHealthInfo.GetDbCopy(serverName);
			return !dbHealthInfo.SkippedFromMonitoring.IsSuccess && matchPredicate(dbHealthInfo, dbCopy);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004A8F8 File Offset: 0x00048AF8
		private bool IsAnyDbMatchingCriteria(ServerHealthInfo serverInfo, IEnumerable<IADDatabase> dbs, Func<DbHealthInfo, DbCopyHealthInfo, bool> matchPredicate)
		{
			AmServerName serverName = serverInfo.ServerName;
			return dbs.Any((IADDatabase db) => this.DatabaseMatchPredicateWrapper(db, serverName, matchPredicate));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004A960 File Offset: 0x00048B60
		private int CountOfDbsMatchingCriteria(ServerHealthInfo serverInfo, IEnumerable<IADDatabase> dbs, Func<DbHealthInfo, DbCopyHealthInfo, bool> matchPredicate)
		{
			AmServerName serverName = serverInfo.ServerName;
			return dbs.Count((IADDatabase db) => this.DatabaseMatchPredicateWrapper(db, serverName, matchPredicate));
		}

		// Token: 0x040006CE RID: 1742
		private static readonly TimeSpan MissingObjectAgeThreshold = TimeSpan.FromDays((double)RegistryParameters.MonitoringDHTMissingObjectCleanupAgeThresholdInDays);

		// Token: 0x040006CF RID: 1743
		private Dictionary<Guid, DbHealthInfo> m_dbServerInfos = new Dictionary<Guid, DbHealthInfo>(160);

		// Token: 0x040006D0 RID: 1744
		private Dictionary<AmServerName, ServerHealthInfo> m_serverInfos = new Dictionary<AmServerName, ServerHealthInfo>(16);

		// Token: 0x040006D1 RID: 1745
		private readonly string m_filePath;
	}
}
