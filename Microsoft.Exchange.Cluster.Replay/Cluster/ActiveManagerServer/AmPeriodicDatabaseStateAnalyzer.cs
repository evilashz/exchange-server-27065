using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200000A RID: 10
	internal class AmPeriodicDatabaseStateAnalyzer : AmStartupAutoMounter
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003A8C File Offset: 0x00001C8C
		internal AmPeriodicDatabaseStateAnalyzer()
		{
			this.m_reasonCode = AmDbActionReason.PeriodicAction;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A9C File Offset: 0x00001C9C
		protected override void RunInternal()
		{
			AmMultiNodeMdbStatusFetcher amMultiNodeMdbStatusFetcher = base.StartMdbStatusFetcher();
			Dictionary<Guid, DatabaseInfo> dbMap = base.GenerateDatabaseInfoMap();
			amMultiNodeMdbStatusFetcher.WaitUntilStatusIsReady();
			Dictionary<AmServerName, MdbStatus[]> mdbStatusMap = amMultiNodeMdbStatusFetcher.MdbStatusMap;
			if (mdbStatusMap == null)
			{
				ReplayEventLogConstants.Tuple_PeriodicOperationFailedRetrievingStatuses.LogEvent(null, new object[]
				{
					"AmPeriodicDatabaseStateAnalyzer"
				});
				return;
			}
			base.MergeDatabaseInfoWithMdbStatus(dbMap, mdbStatusMap, amMultiNodeMdbStatusFetcher.ServerInfoMap);
			Dictionary<Guid, DatabaseInfo> filteredMap = this.FilterDatabasesNeedingAction(dbMap);
			this.DeferDatabaseActionsIfRequired(filteredMap);
			this.InitiateSystemFailoverIfReplayUnreachable(amMultiNodeMdbStatusFetcher, dbMap);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B24 File Offset: 0x00001D24
		protected int GetActiveDatabaseCountOnServer(Dictionary<Guid, DatabaseInfo> dbMap, AmServerName server)
		{
			return dbMap.Values.Count((DatabaseInfo dbInfo) => dbInfo.IsActiveOnServerAndReplicated(server));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B58 File Offset: 0x00001D58
		protected void InitiateSystemFailoverIfReplayUnreachable(AmMultiNodeMdbStatusFetcher mdbStatusFetcher, Dictionary<Guid, DatabaseInfo> dbMap)
		{
			AmRole role = AmSystemManager.Instance.Config.Role;
			if (role != AmRole.PAM)
			{
				return;
			}
			if (!RegistryParameters.OnReplDownFailoverEnabled)
			{
				ReplayCrimsonEvents.FailoverOnReplDownDisabledInRegistry.LogPeriodic(Environment.MachineName, TimeSpan.FromHours(1.0));
				return;
			}
			AmSystemFailoverOnReplayDownTracker systemFailoverOnReplayDownTracker = AmSystemManager.Instance.SystemFailoverOnReplayDownTracker;
			if (systemFailoverOnReplayDownTracker == null)
			{
				ReplayCrimsonEvents.FailoverOnReplDownFailoverTrackerNotInitialized.LogPeriodic(Environment.MachineName, TimeSpan.FromHours(1.0));
				return;
			}
			foreach (KeyValuePair<AmServerName, AmMdbStatusServerInfo> keyValuePair in mdbStatusFetcher.ServerInfoMap)
			{
				AmServerName key = keyValuePair.Key;
				AmMdbStatusServerInfo value = keyValuePair.Value;
				if (value.IsReplayRunning)
				{
					systemFailoverOnReplayDownTracker.MarkReplayUp(key);
				}
				else if (value.IsStoreRunning)
				{
					systemFailoverOnReplayDownTracker.MarkReplayDown(key, false);
					int activeDatabaseCountOnServer = this.GetActiveDatabaseCountOnServer(dbMap, key);
					if (activeDatabaseCountOnServer > 0)
					{
						systemFailoverOnReplayDownTracker.ScheduleFailover(key);
					}
					else
					{
						ReplayCrimsonEvents.FailoverOnReplDownSkipped.LogPeriodic<AmServerName, string, string>(key, TimeSpan.FromDays(1.0), key, "NoActives", "Periodic");
					}
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003C84 File Offset: 0x00001E84
		protected Dictionary<Guid, DatabaseInfo> FilterDatabasesNeedingAction(Dictionary<Guid, DatabaseInfo> dbMap)
		{
			Dictionary<Guid, DatabaseInfo> dictionary = new Dictionary<Guid, DatabaseInfo>();
			foreach (DatabaseInfo databaseInfo in dbMap.Values)
			{
				databaseInfo.Analyze();
				if (base.IsActionRequired(databaseInfo))
				{
					if (this.m_isDebugOptionEnabled && this.m_amConfig.IsIgnoreServerDebugOptionEnabled(databaseInfo.ActiveServer))
					{
						AmTrace.Warning("Periodic action for database {0} is not performed since debug option {1} is set for server {2} which is the current active for the database", new object[]
						{
							databaseInfo.Database.Name,
							databaseInfo.ActiveServer.NetbiosName,
							AmDebugOptions.IgnoreServerFromAutomaticActions.ToString()
						});
					}
					else
					{
						dictionary.Add(databaseInfo.Database.Guid, databaseInfo);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D58 File Offset: 0x00001F58
		protected void DeferDatabaseActionsIfRequired(Dictionary<Guid, DatabaseInfo> filteredMap)
		{
			if (filteredMap.Count > 0)
			{
				AmEvtPeriodicDbStateRestore amEvtPeriodicDbStateRestore = new AmEvtPeriodicDbStateRestore(filteredMap);
				if (AmSystemManager.Instance.PeriodicEventManager.EnqueueDeferredSystemEvent(amEvtPeriodicDbStateRestore, RegistryParameters.AmDeferredDatabaseStateRestorerIntervalInMSec))
				{
					AmTrace.Debug("Enqueuing deferred system event for restoring database states if the state is confirmed (evt={0})", new object[]
					{
						amEvtPeriodicDbStateRestore
					});
					return;
				}
				AmTrace.Warning("There is already a timer pending for periodic action verification. Until it is completed a new one won't be posted.", new object[0]);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DB4 File Offset: 0x00001FB4
		protected override void LogStartupInternal()
		{
			AmTrace.Debug("Starting {0}", new object[]
			{
				base.GetType().Name
			});
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3982896445U);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003DF0 File Offset: 0x00001FF0
		protected override void LogCompletionInternal()
		{
			AmTrace.Debug("Finished {0}", new object[]
			{
				base.GetType().Name
			});
		}
	}
}
