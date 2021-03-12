using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200002F RID: 47
	internal abstract class AmDbAction
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		internal AmDbAction(AmConfig cfg, IADDatabase db, AmDbActionCode actionCode, string uniqueOperationId)
		{
			this.m_database = db;
			this.m_dbTrace = new AmDbTrace(db);
			this.Config = cfg;
			this.DatabaseName = db.Name;
			this.DatabaseGuid = db.Guid;
			this.ActionCode = actionCode;
			this.UniqueOperationId = uniqueOperationId;
			this.CurrentAttemptNumber = 1;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000CC77 File Offset: 0x0000AE77
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000CC7F File Offset: 0x0000AE7F
		internal string DatabaseName { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000CC88 File Offset: 0x0000AE88
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000CC90 File Offset: 0x0000AE90
		internal Guid DatabaseGuid { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000CC99 File Offset: 0x0000AE99
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000CCA1 File Offset: 0x0000AEA1
		internal string UniqueOperationId { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000CCAA File Offset: 0x0000AEAA
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000CCB2 File Offset: 0x0000AEB2
		internal TimeSpan? LockTimeout
		{
			get
			{
				return this.m_lockTimeout;
			}
			set
			{
				this.m_lockTimeout = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000CCBB File Offset: 0x0000AEBB
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		internal AmReportStatusDelegate StatusCallback { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000CCCC File Offset: 0x0000AECC
		internal AmDbTrace DbTrace
		{
			get
			{
				return this.m_dbTrace;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		protected AmConfig Config { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		protected IADDatabase Database
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		protected AmDbStateInfo State
		{
			get
			{
				if (this.m_state == null)
				{
					lock (this)
					{
						if (this.m_state == null)
						{
							this.m_state = this.Config.DbState.Read(this.Database.Guid);
						}
					}
				}
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000CD65 File Offset: 0x0000AF65
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000CD6D File Offset: 0x0000AF6D
		protected AmDbActionCode ActionCode { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000CD76 File Offset: 0x0000AF76
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000CD7E File Offset: 0x0000AF7E
		protected int CurrentAttemptNumber { get; set; }

		// Token: 0x0600022D RID: 557 RVA: 0x0000CD88 File Offset: 0x0000AF88
		internal static void AttemptCopyLastLogsDirect(AmServerName serverName, Guid dbGuid, AmAcllArgs acllArgs, ref AmAcllReturnStatus acllStatus)
		{
			acllStatus = null;
			Dependencies.AmRpcClientWrapper.AttemptCopyLastLogsDirect(serverName.Fqdn, dbGuid, acllArgs.MountDialOverride, acllArgs.NumRetries, acllArgs.E00TimeoutMs, acllArgs.NetworkIOTimeoutMs, acllArgs.NetworkConnectTimeoutMs, acllArgs.SourceServer.Fqdn, (int)acllArgs.ActionCode, (int)acllArgs.SkipValidationChecks, acllArgs.MountPending, acllArgs.UniqueOperationId, acllArgs.SubactionAttemptNumber, ref acllStatus);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		internal static void MountDatabaseDirect(AmServerName serverName, AmServerName lastMountedServerName, Guid dbGuid, MountFlags storeFlags, AmMountFlags amFlags, AmDbActionCode actionCode)
		{
			AmFaultInject.GenerateMapiExceptionIfRequired(dbGuid, serverName);
			AmMountArg mountArg = new AmMountArg((int)storeFlags, (int)amFlags, lastMountedServerName.Fqdn, (int)actionCode);
			Dependencies.AmRpcClientWrapper.MountDatabaseDirectEx(serverName.Fqdn, dbGuid, mountArg);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000CE34 File Offset: 0x0000B034
		internal static void DismountDatabaseDirect(AmServerName serverName, Guid dbGuid, UnmountFlags flags, AmDbActionCode actionCode)
		{
			AmFaultInject.GenerateMapiExceptionIfRequired(dbGuid, serverName);
			AmDismountArg dismountArg = new AmDismountArg((int)flags, (int)actionCode);
			Dependencies.AmRpcClientWrapper.DismountDatabaseDirect(serverName.Fqdn, dbGuid, dismountArg);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000CE68 File Offset: 0x0000B068
		internal static void DismountIfMismounted(IADDatabase db, AmDbActionCode actionCode, List<AmServerName> mismountedNodes)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.Role == AmRole.Unknown)
			{
				throw new AmInvalidConfiguration(config.LastError);
			}
			AmDbStateInfo amDbStateInfo = config.DbState.Read(db.Guid);
			if (amDbStateInfo.IsEntryExist)
			{
				using (List<AmServerName>.Enumerator enumerator = mismountedNodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AmServerName amServerName = enumerator.Current;
						if (!AmServerName.IsEqual(amServerName, amDbStateInfo.ActiveServer))
						{
							ReplayCrimsonEvents.DismountingMismountedDatabase.Log<string, Guid, AmServerName>(db.Name, db.Guid, amServerName);
							AmStoreHelper.RemoteDismount(amServerName, db.Guid, UnmountFlags.SkipCacheFlush, false);
						}
						else
						{
							AmTrace.Warning("Ignoring force dismount for {0} since it is the current active {1}", new object[]
							{
								db.Name,
								amServerName
							});
						}
					}
					return;
				}
			}
			AmTrace.Warning("DismountIfMismounted skipped since the database {0} was never mounted", new object[]
			{
				db.Name
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000CF64 File Offset: 0x0000B164
		internal static void SyncClusterDatabaseState(IADDatabase db, AmDbActionCode actionCode)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsUnknown)
			{
				AmTrace.Error("SyncClusterDatabaseState: Invalid configuration (db={0})", new object[]
				{
					db
				});
				throw new AmInvalidConfiguration(config.LastError);
			}
			AmDbStateInfo amDbStateInfo = config.DbState.Read(db.Guid);
			AmServerName amServerName;
			if (amDbStateInfo.IsActiveServerValid)
			{
				amServerName = amDbStateInfo.ActiveServer;
			}
			else
			{
				amServerName = new AmServerName(db.Server.Name);
			}
			MountStatus mountStatus = MountStatus.Dismounted;
			if ((config.IsStandalone || (config.IsPamOrSam && config.DagConfig.IsNodePubliclyUp(amServerName))) && AmStoreHelper.IsMounted(amServerName, db.Guid))
			{
				mountStatus = MountStatus.Mounted;
				AmSystemManager.Instance.DbNodeAttemptTable.ClearFailedTime(db.Guid);
			}
			MountStatus mountStatus2 = amDbStateInfo.MountStatus;
			if (mountStatus != mountStatus2 || !AmServerName.IsEqual(amDbStateInfo.LastMountedServer, amDbStateInfo.ActiveServer))
			{
				if (mountStatus != mountStatus2)
				{
					AmTrace.Debug("Mounted state reported by STORE is different from persistent state. (Database:{0}, PersistentState: {1}, StoreIsReporting: {2})", new object[]
					{
						db.Name,
						mountStatus2,
						mountStatus
					});
				}
				else
				{
					AmTrace.Debug("State is in transit. (Database:{0}, LastMountedServer: {1},ActiveServer: {2})", new object[]
					{
						db.Name,
						amDbStateInfo.LastMountedServer,
						amDbStateInfo.ActiveServer
					});
				}
				AmDbAction.WriteStateSyncMountStatus(config, amDbStateInfo, db.Guid, amServerName, mountStatus);
				ReplayCrimsonEvents.DatabaseMountStatusSynchronized.Log<string, Guid, MountStatus, MountStatus, AmServerName>(db.Name, db.Guid, mountStatus2, mountStatus, amServerName);
				return;
			}
			AmTrace.Debug("Ignored persistent state sync for {0} since nothing is out of sync", new object[]
			{
				db.Name
			});
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		internal static void SyncDatabaseOwningServerAndLegacyDn(IADDatabase db, AmDbActionCode actionCode)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			AmDbStateInfo amDbStateInfo = config.DbState.Read(db.Guid);
			AmServerName amServerName = new AmServerName(db.Server.Name);
			AmServerName amServerName2;
			if (amDbStateInfo.IsActiveServerValid)
			{
				amServerName2 = amDbStateInfo.ActiveServer;
			}
			else
			{
				amServerName2 = amServerName;
			}
			AmTrace.Debug("Synchronizing AD properties of database {0} (initialOwningServer:{1}, newActiveServer:{2})", new object[]
			{
				db.Name,
				amServerName,
				amServerName2
			});
			bool flag = SharedDependencies.WritableADHelper.SetDatabaseLegacyDnAndOwningServer(db.Guid, amDbStateInfo.LastMountedServer, amServerName2, false);
			if (flag)
			{
				ReplayCrimsonEvents.DatabaseAdPropertiesSynchronized.Log<string, Guid, AmServerName, AmServerName>(db.Name, db.Guid, amServerName, amServerName2);
				return;
			}
			AmTrace.Debug("Ignored ad sync request database {0}", new object[]
			{
				db.Name
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		internal static bool WriteStateSyncMountStatus(AmConfig amConfig, AmDbStateInfo stateInfo, Guid databaseGuid, AmServerName activeServer, MountStatus mountStatus)
		{
			bool flag = false;
			if (mountStatus == MountStatus.Mounted)
			{
				stateInfo.UpdateActiveServerAndIncrementFailoverSequenceNumber(activeServer);
				stateInfo.LastMountedServer = activeServer;
				stateInfo.IsAdminDismounted = false;
				stateInfo.MountStatus = mountStatus;
				stateInfo.IsAutomaticActionsAllowed = true;
				stateInfo.LastMountedTime = DateTime.UtcNow;
				flag = true;
			}
			else if (stateInfo.IsEntryExist)
			{
				stateInfo.MountStatus = mountStatus;
				if (stateInfo.IsMountSucceededAtleastOnce)
				{
					stateInfo.UpdateActiveServerAndIncrementFailoverSequenceNumber(activeServer);
					stateInfo.LastMountedServer = activeServer;
				}
				flag = true;
			}
			if (flag && AmDbAction.WriteState(amConfig, stateInfo, false))
			{
				AmDatabaseStateTracker databaseStateTracker = AmSystemManager.Instance.DatabaseStateTracker;
				if (databaseStateTracker != null)
				{
					databaseStateTracker.UpdateActive(databaseGuid, activeServer);
				}
			}
			return flag;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D25C File Offset: 0x0000B45C
		internal static bool WriteState(AmConfig cfg, AmDbStateInfo stateInfo, bool isBestEffort)
		{
			bool result = false;
			bool flag = true;
			try
			{
				AmConfig config = AmSystemManager.Instance.Config;
				if (cfg != null && cfg.Role != config.Role)
				{
					flag = false;
					if (!isBestEffort)
					{
						throw new AmRoleChangedWhileOperationIsInProgressException(cfg.Role.ToString(), config.Role.ToString());
					}
				}
				if (flag)
				{
					cfg.DbState.Write(stateInfo);
					result = true;
				}
			}
			catch (ClusterApiException ex)
			{
				AmTrace.Error("Error while trying to write state {0} to cluster database. (error={1})", new object[]
				{
					stateInfo,
					ex.Message
				});
				if (!isBestEffort)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		internal void Mount(MountFlags storeFlags, AmMountFlags amMountFlags, DatabaseMountDialOverride mountDialoverride, ref AmDbOperationDetailedStatus mountStatus)
		{
			mountStatus = new AmDbOperationDetailedStatus(this.Database);
			Exception ex = null;
			bool flag = true;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			AmDbOperationDetailedStatus tempStatus = mountStatus;
			try
			{
				using (AmDatabaseOperationLock.Lock(this.DatabaseGuid, AmDbLockReason.Mount, null))
				{
					ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						ReplayCrimsonEvents.ToplevelMountInitiated.LogGeneric(this.PrepareStartupArgs(new object[]
						{
							storeFlags,
							mountDialoverride
						}));
						this.EnsureAutomaticActionIsAllowed();
						this.ClearFailureAttemptIfAdminAction(this.DatabaseGuid);
						if (!this.State.IsEntryExist)
						{
							this.DbTrace.Info("Mounting database for the first time!", new object[0]);
						}
						this.MountInternal(storeFlags, amMountFlags, mountDialoverride, ref tempStatus);
					});
					mountStatus = tempStatus;
					this.WriteStateClearIfInProgressStatus(true);
					flag = false;
				}
			}
			catch (AmDbLockConflictException ex2)
			{
				ex = ex2;
			}
			finally
			{
				stopwatch.Stop();
				if (flag || ex != null)
				{
					string text = (ex != null) ? ex.Message : ReplayStrings.UnknownError;
					ReplayCrimsonEvents.ToplevelMountFailed.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						stopwatch.Elapsed,
						text
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseMountFailed.LogEvent(null, new object[]
					{
						this.DatabaseName,
						this.State.ActiveServer,
						text
					});
				}
				else
				{
					ReplayCrimsonEvents.ToplevelMountSuccess.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						stopwatch.Elapsed
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseMounted.LogEvent(null, new object[]
					{
						this.DatabaseName,
						this.State.ActiveServer
					});
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D610 File Offset: 0x0000B810
		internal void Dismount(UnmountFlags flags)
		{
			Exception ex = null;
			bool flag = true;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				using (AmDatabaseOperationLock.Lock(this.DatabaseGuid, AmDbLockReason.Dismount, this.m_lockTimeout))
				{
					ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						ReplayCrimsonEvents.ToplevelDismountInitiated.LogGeneric(this.PrepareStartupArgs(new object[]
						{
							flags
						}));
						if (!this.State.IsEntryExist)
						{
							throw new AmDatabaseNeverMountedException();
						}
						this.ClearFailureAttemptIfAdminAction(this.DatabaseGuid);
						this.DismountInternal(flags);
					});
					this.WriteStateClearIfInProgressStatus(true);
					flag = false;
				}
			}
			catch (AmDbLockConflictException ex2)
			{
				ex = ex2;
			}
			finally
			{
				stopwatch.Stop();
				AmSystemManager.Instance.TransientFailoverSuppressor.AdminRequestedForRemoval(this.State.ActiveServer, "Dismount-Database");
				if (flag || ex != null)
				{
					string text = (ex != null) ? ex.Message : ReplayStrings.UnknownError;
					ReplayCrimsonEvents.ToplevelDismountFailed.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						stopwatch.Elapsed,
						text
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseDismountFailed.LogEvent(null, new object[]
					{
						this.DatabaseName,
						this.State.ActiveServer,
						text
					});
				}
				else
				{
					ReplayCrimsonEvents.ToplevelDismountSuccess.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						stopwatch.Elapsed
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseDismounted.LogEvent(null, new object[]
					{
						this.DatabaseName,
						this.State.ActiveServer
					});
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D92C File Offset: 0x0000BB2C
		internal void Move(MountFlags mountFlags, UnmountFlags dismountFlags, DatabaseMountDialOverride mountDialoverride, AmServerName fromServer, AmServerName targetServer, bool tryOtherHealthyServers, AmBcsSkipFlags skipValidationChecks, string moveComment, string componentName, ref AmDbOperationDetailedStatus moveStatus)
		{
			moveStatus = new AmDbOperationDetailedStatus(this.Database);
			Exception ex = null;
			bool flag = true;
			AmServerName initialSourceServer = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			string initialActive = string.Empty;
			AmDbOperationDetailedStatus tempStatus = moveStatus;
			try
			{
				using (AmDatabaseOperationLock.Lock(this.DatabaseGuid, AmDbLockReason.Move, null))
				{
					ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						initialActive = this.GetSafeActiveServer();
						ReplayCrimsonEvents.ToplevelMoveInitiated.LogGeneric(this.PrepareStartupArgs(new object[]
						{
							mountFlags,
							dismountFlags,
							mountDialoverride,
							AmServerName.IsNullOrEmpty(fromServer) ? initialActive : fromServer.Fqdn,
							targetServer,
							tryOtherHealthyServers,
							skipValidationChecks,
							moveComment,
							componentName
						}));
						initialSourceServer = this.State.ActiveServer;
						this.EnsureAutomaticActionIsAllowed();
						this.ClearFailureAttemptIfAdminAction(this.DatabaseGuid);
						this.MoveInternal(mountFlags, dismountFlags, mountDialoverride, fromServer, targetServer, tryOtherHealthyServers, skipValidationChecks, componentName, ref tempStatus);
					});
					this.WriteStateClearIfInProgressStatus(true);
					flag = false;
				}
			}
			catch (AmDbLockConflictException ex2)
			{
				ex = ex2;
			}
			finally
			{
				stopwatch.Stop();
				moveStatus = tempStatus;
				if (flag || ex != null)
				{
					string text = (ex != null) ? ex.Message : ReplayStrings.UnknownError;
					ReplayCrimsonEvents.ToplevelMoveFailed.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						initialActive,
						stopwatch.Elapsed,
						text,
						moveComment
					}));
					if (AmServerName.IsNullOrEmpty(targetServer))
					{
						ReplayEventLogConstants.Tuple_AmDatabaseMoveUnspecifiedServerFailed.LogEvent(null, new object[]
						{
							this.DatabaseName,
							initialSourceServer,
							text,
							moveComment
						});
					}
					else
					{
						ReplayEventLogConstants.Tuple_AmDatabaseMoveFailed.LogEvent(null, new object[]
						{
							this.DatabaseName,
							initialActive,
							targetServer,
							text,
							moveComment
						});
					}
				}
				else
				{
					ReplayCrimsonEvents.ToplevelMoveSuccess.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						initialActive,
						stopwatch.Elapsed,
						moveComment
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseMoved.LogEvent(null, new object[]
					{
						this.DatabaseName,
						initialActive,
						moveStatus.FinalDbState.ActiveServer,
						moveComment
					});
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		internal void Remount(MountFlags mountFlags, DatabaseMountDialOverride mountDialoverride, AmServerName fromServer)
		{
			Exception ex = null;
			bool flag = true;
			AmServerName amServerName = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				using (AmDatabaseOperationLock.Lock(this.DatabaseGuid, AmDbLockReason.Remount, null))
				{
					ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
					{
						ReplayCrimsonEvents.ToplevelRemountInitiated.LogGeneric(this.PrepareStartupArgs(new object[]
						{
							mountFlags,
							mountDialoverride,
							fromServer
						}));
						if (!this.State.IsEntryExist)
						{
							this.DbTrace.Error("Database was never mounted. Remount is applicable only if it was mounted at least once", new object[0]);
							throw new AmDatabaseNeverMountedException();
						}
						if (this.State.IsAdminDismounted)
						{
							this.DbTrace.Error("Skipping remount action since the database was admin dismounted", new object[0]);
							throw new AmDbRemountSkippedSinceDatabaseWasAdminDismounted(this.DatabaseName);
						}
						if (!AmServerName.IsEqual(this.State.ActiveServer, fromServer))
						{
							this.DbTrace.Error("Skipping remount action since database master had changed", new object[0]);
							throw new AmDbRemountSkippedSinceMasterChanged(this.DatabaseName, this.State.ActiveServer.Fqdn, fromServer.NetbiosName);
						}
						this.EnsureAutomaticActionIsAllowed();
						this.RemountInternal(mountFlags, mountDialoverride, fromServer);
					});
					this.WriteStateClearIfInProgressStatus(true);
					flag = false;
				}
			}
			catch (AmDbLockConflictException ex2)
			{
				ex = ex2;
			}
			finally
			{
				stopwatch.Stop();
				if (flag || ex != null)
				{
					string text = (ex != null) ? ex.Message : ReplayStrings.UnknownError;
					ReplayCrimsonEvents.ToplevelRemountFailed.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						stopwatch.Elapsed,
						text
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseMountFailed.LogEvent(null, new object[]
					{
						this.DatabaseName,
						amServerName,
						text
					});
				}
				else
				{
					ReplayCrimsonEvents.ToplevelRemountSuccess.LogGeneric(this.PrepareCompletionArgs(new object[]
					{
						stopwatch.Elapsed
					}));
					ReplayEventLogConstants.Tuple_AmDatabaseMounted.LogEvent(null, new object[]
					{
						this.DatabaseName,
						amServerName
					});
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DF08 File Offset: 0x0000C108
		private TimeSpan DetermineDismountTimeout(AmDbActionCode action, bool isNodeUp)
		{
			this.DbTrace.Debug("DetermineDismountTimeout: ActionCode={0}, isNodeUp={1}", new object[]
			{
				action.ToString(),
				isNodeUp
			});
			TimeSpan timeSpan = this.DismountTimeoutShort;
			if (action.IsDismountOperation)
			{
				timeSpan = this.DismountTimeoutInfinite;
			}
			else if (action.Category == AmDbActionCategory.Remount)
			{
				timeSpan = this.DismountTimeoutShort;
			}
			else if (action.IsAutomaticShutdownSwitchover)
			{
				timeSpan = this.DismountTimeoutMedium;
			}
			else if (action.IsAutomaticOperation)
			{
				if (!isNodeUp)
				{
					timeSpan = this.DismountTimeoutAsync;
				}
				else
				{
					timeSpan = this.DismountTimeoutShort;
				}
			}
			else if (action.IsAdminMoveOperation)
			{
				if (!isNodeUp)
				{
					timeSpan = this.DismountTimeoutAsync;
				}
				else
				{
					timeSpan = this.DismountTimeoutMedium;
				}
			}
			this.DbTrace.Debug("DetermineDismountTimeout: Returning timeout of {0}", new object[]
			{
				timeSpan
			});
			return timeSpan;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		protected void DismountCommon(UnmountFlags flags)
		{
			AmServerName serverToDismount = this.State.ActiveServer;
			Exception dismountException = null;
			bool isSuccess = false;
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.ReportStatus(AmDbActionStatus.StoreDismountInitiated);
				isSuccess = this.AttemptDismount(this.State.ActiveServer, flags, false, out dismountException);
				if (this.ActionCode.IsAdminDismountOperation)
				{
					MountStatus storeDatabaseMountStatus = AmStoreHelper.GetStoreDatabaseMountStatus(serverToDismount, this.DatabaseGuid);
					if (storeDatabaseMountStatus == MountStatus.Dismounted)
					{
						this.WriteStateAdminDismounted();
						dismountException = null;
						return;
					}
					if (storeDatabaseMountStatus == MountStatus.Mounted)
					{
						if (dismountException == null)
						{
							dismountException = new AmDismountSucceededButStillMountedException(serverToDismount.Fqdn, this.Database.Name);
						}
						this.WriteStateDismountFinished(true, MountStatus.Mounted, true);
						return;
					}
					if (dismountException == null)
					{
						dismountException = new AmFailedToDetermineDatabaseMountStatusException(serverToDismount.Fqdn, this.Database.Name);
					}
					this.WriteStateDismountFinished(true, MountStatus.Dismounted, true);
				}
			});
			if (dismountException != null)
			{
				ex = dismountException;
			}
			if (ex != null)
			{
				this.ReportStatus(AmDbActionStatus.StoreDismountFailed);
				AmHelper.ThrowDbActionWrapperExceptionIfNecessary(ex);
				return;
			}
			this.ReportStatus(AmDbActionStatus.StoreDismountSuccessful);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000E26C File Offset: 0x0000C46C
		internal bool AttemptDismount(AmServerName serverName, UnmountFlags flags, bool isIgnoreKnownExceptions, out Exception exception)
		{
			bool result = false;
			exception = null;
			bool isNodeup = true;
			if (this.Config.IsPamOrSam && !this.Config.DagConfig.IsNodePubliclyUp(serverName))
			{
				isNodeup = false;
			}
			TimeSpan bestEffortDismountTimeout = this.DetermineDismountTimeout(this.ActionCode, isNodeup);
			this.DbTrace.Debug("Attempting dismount (server={0}, flags={1}, actionCode={2}, dismountTimeout={3}ms, ignoreException={4})", new object[]
			{
				serverName,
				flags,
				this.ActionCode,
				bestEffortDismountTimeout.TotalMilliseconds,
				isIgnoreKnownExceptions
			});
			AmDbAction.DismountMode modeOfDismount = AmDbAction.DismountMode.None;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool flag = true;
			MountStatus mountStatus = this.State.MountStatus;
			try
			{
				ReplayCrimsonEvents.StoreDismountInitiated.LogGeneric(this.PrepareSubactionArgs(new object[]
				{
					serverName,
					flags,
					bestEffortDismountTimeout
				}));
				if (AmServerName.IsEqual(this.State.ActiveServer, serverName))
				{
					this.WriteStateDismounting(false);
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3152424253U);
				exception = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					InvokeWithTimeout.Invoke(delegate()
					{
						if (isNodeup && AmHelper.IsReplayRunning(serverName))
						{
							this.DbTrace.Debug("Attempting Dismount through active manager", new object[0]);
							modeOfDismount = AmDbAction.DismountMode.ThroughReplayService;
							AmDbAction.DismountDatabaseDirect(serverName, this.DatabaseGuid, flags, this.ActionCode);
							return;
						}
						this.DbTrace.Debug("Attempting dismount by directly RPCing to store", new object[0]);
						modeOfDismount = AmDbAction.DismountMode.DirectlyToStore;
						AmStoreHelper.RemoteDismount(serverName, this.DatabaseGuid, flags, true);
					}, bestEffortDismountTimeout);
					this.DbTrace.Debug("Database is possibly dismounted at server {0}", new object[]
					{
						serverName
					});
				});
				if (exception != null)
				{
					this.DbTrace.Debug("Dismount failed with error: {0}", new object[]
					{
						exception
					});
				}
				flag = false;
			}
			finally
			{
				stopwatch.Stop();
				string text = null;
				if (flag)
				{
					text = ReplayStrings.UnknownError;
				}
				else if (exception != null)
				{
					text = exception.Message;
				}
				if (string.IsNullOrEmpty(text))
				{
					result = true;
					ReplayCrimsonEvents.StoreDismountSuccess.LogGeneric(this.PrepareSubactionArgs(new object[]
					{
						serverName,
						modeOfDismount,
						stopwatch.Elapsed
					}));
					if (AmServerName.IsEqual(this.State.ActiveServer, serverName))
					{
						this.WriteStateDismountFinished(true, MountStatus.Dismounted, false);
					}
				}
				else
				{
					ReplayCrimsonEvents.StoreDismountFailed.LogGeneric(this.PrepareSubactionArgs(new object[]
					{
						serverName,
						modeOfDismount,
						stopwatch.Elapsed,
						text
					}));
					if (AmServerName.IsEqual(this.State.ActiveServer, serverName))
					{
						this.WriteStateDismountFinished(true, mountStatus, false);
					}
				}
			}
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000E558 File Offset: 0x0000C758
		internal bool WriteState()
		{
			return this.WriteState(false);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000E561 File Offset: 0x0000C761
		internal bool WriteState(bool isBestEffort)
		{
			return AmDbAction.WriteState(this.Config, this.State, isBestEffort);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000E578 File Offset: 0x0000C778
		internal bool WriteStateMountStart(AmServerName serverToMount)
		{
			this.State.UpdateActiveServerAndIncrementFailoverSequenceNumber(serverToMount);
			if (this.State.IsAdminDismounted)
			{
				if (!this.ActionCode.IsAdminMountOperation)
				{
					throw new AmDbActionRejectedAdminDismountedException(this.ActionCode.ToString());
				}
				this.State.IsAdminDismounted = false;
			}
			if (this.ActionCode.IsAdminOperation)
			{
				this.State.IsAutomaticActionsAllowed = false;
			}
			this.State.MountStatus = MountStatus.Mounting;
			return this.WriteState();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
		internal bool WriteStateMountSkipped(AmServerName serverToMount)
		{
			this.State.UpdateActiveServerAndIncrementFailoverSequenceNumber(serverToMount);
			this.State.MountStatus = MountStatus.Dismounted;
			return this.WriteState();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E618 File Offset: 0x0000C818
		internal bool WriteStateMountSuccess()
		{
			this.State.LastMountedServer = this.State.ActiveServer;
			this.State.IsAutomaticActionsAllowed = true;
			this.State.MountStatus = MountStatus.Mounted;
			this.State.LastMountedTime = DateTime.UtcNow;
			return this.WriteState();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000E66C File Offset: 0x0000C86C
		internal bool WriteStateMountFailed(bool isBestEffort)
		{
			if (this.ActionCode.IsAdminOperation)
			{
				this.State.IsAutomaticActionsAllowed = false;
			}
			this.State.MountStatus = MountStatus.Dismounted;
			if (this.State.IsMountSucceededAtleastOnce)
			{
				this.State.LastMountedServer = this.State.ActiveServer;
			}
			return this.WriteState(isBestEffort);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		internal void WriteStateDismounting(bool isForce)
		{
			if (isForce || this.State.MountStatus != MountStatus.Dismounting)
			{
				this.State.MountStatus = MountStatus.Dismounting;
				this.WriteState();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000E6EE File Offset: 0x0000C8EE
		internal void WriteStateAdminDismounted()
		{
			this.State.IsAdminDismounted = true;
			this.State.IsAutomaticActionsAllowed = false;
			this.State.MountStatus = MountStatus.Dismounted;
			this.WriteState();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000E71C File Offset: 0x0000C91C
		internal bool WriteStateDismountFinished(bool isBestEffort, MountStatus mountStatus, bool isIgnoreIfPrevStateIsSame)
		{
			bool result = true;
			if (!isIgnoreIfPrevStateIsSame || this.State.MountStatus != mountStatus)
			{
				this.State.MountStatus = mountStatus;
				result = this.WriteState(isBestEffort);
			}
			return result;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000E751 File Offset: 0x0000C951
		internal bool WriteStateClearIfInProgressStatus(bool isBestEffort)
		{
			if (this.State.MountStatus == MountStatus.Dismounting || this.State.MountStatus == MountStatus.Mounting)
			{
				this.State.MountStatus = MountStatus.Dismounted;
				return this.WriteState(isBestEffort);
			}
			return false;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E784 File Offset: 0x0000C984
		protected object[] PrepareStartupArgs(params object[] args)
		{
			List<object> list = new List<object>();
			list.Add(this.UniqueOperationId);
			list.Add(this.DatabaseName);
			list.Add(this.DatabaseGuid);
			list.Add(this.GetSafeActiveServer());
			list.Add(this.ActionCode.Category);
			list.Add(this.ActionCode.Initiator);
			list.Add(this.ActionCode.Reason);
			list.Add(this.Config.Role);
			list.Add(this.Config.IsPamOrSam ? this.Config.DagConfig.CurrentPAM : AmServerName.LocalComputerName);
			if (args != null)
			{
				foreach (object item in args)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E874 File Offset: 0x0000CA74
		protected object[] PrepareSubactionArgs(params object[] args)
		{
			List<object> list = new List<object>();
			list.Add(this.UniqueOperationId);
			list.Add(this.DatabaseName);
			list.Add(this.DatabaseGuid);
			list.Add(this.CurrentAttemptNumber);
			if (args != null)
			{
				foreach (object item in args)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		protected object[] PrepareCompletionArgs(params object[] args)
		{
			List<object> list = new List<object>();
			list.Add(this.UniqueOperationId);
			list.Add(this.DatabaseName);
			list.Add(this.DatabaseGuid);
			list.Add(this.GetSafeActiveServer());
			if (args != null)
			{
				foreach (object item in args)
				{
					list.Add(item);
				}
			}
			list.Add(this.GetSafeFailoverSequenceNumber());
			return list.ToArray();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000E965 File Offset: 0x0000CB65
		protected void ReportStatus(AmDbActionStatus status)
		{
			if (this.StatusCallback != null)
			{
				this.StatusCallback(this.Database, status);
			}
		}

		// Token: 0x0600024A RID: 586
		protected abstract void MountInternal(MountFlags storeFlags, AmMountFlags amMountFlags, DatabaseMountDialOverride mountDialoverride, ref AmDbOperationDetailedStatus mountStatus);

		// Token: 0x0600024B RID: 587
		protected abstract void DismountInternal(UnmountFlags flags);

		// Token: 0x0600024C RID: 588
		protected abstract void MoveInternal(MountFlags mountFlags, UnmountFlags dismountFlags, DatabaseMountDialOverride mountDialoverride, AmServerName fromServer, AmServerName targetServer, bool tryOtherHealthyServers, AmBcsSkipFlags skipValidationChecks, string componentName, ref AmDbOperationDetailedStatus moveStatus);

		// Token: 0x0600024D RID: 589
		protected abstract void RemountInternal(MountFlags mountFlags, DatabaseMountDialOverride mountDialoverride, AmServerName fromServer);

		// Token: 0x0600024E RID: 590 RVA: 0x0000E984 File Offset: 0x0000CB84
		private string GetSafeActiveServer()
		{
			string result = null;
			try
			{
				result = this.State.ActiveServer.Fqdn;
			}
			catch (ClusterException)
			{
				result = "<unknown>";
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		private long GetSafeFailoverSequenceNumber()
		{
			long result = -1L;
			try
			{
				result = this.State.FailoverSequenceNumber;
			}
			catch (ClusterException)
			{
				result = -1L;
			}
			return result;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		private void ClearFailureAttemptIfAdminAction(Guid dbGuid)
		{
			if (this.ActionCode.IsAdminOperation)
			{
				AmSystemManager.Instance.DbNodeAttemptTable.ClearFailedTime(dbGuid);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EA18 File Offset: 0x0000CC18
		private void EnsureAutomaticActionIsAllowed()
		{
			if (this.ActionCode.IsAutomaticOperation)
			{
				if (!this.Database.MountAtStartup)
				{
					this.DbTrace.Error("Rejecting action {0} since MountAtStartup is set to false.", new object[]
					{
						this.ActionCode
					});
					throw new AmDbActionRejectedMountAtStartupNotEnabledException(this.ActionCode.ToString());
				}
				if (!this.State.IsEntryExist)
				{
					this.DbTrace.Error("Rejecting action {0} since mount was never attempted for database.", new object[]
					{
						this.ActionCode
					});
					throw new AmDatabaseNeverMountedException();
				}
				if (this.State.IsAdminDismounted)
				{
					if (!this.ActionCode.IsMoveOperation)
					{
						this.DbTrace.Error("Rejecting action {0} since database is admin dismounted.", new object[]
						{
							this.ActionCode
						});
						throw new AmDbActionRejectedAdminDismountedException(this.ActionCode.ToString());
					}
				}
				else if (!this.State.IsMountSucceededAtleastOnce)
				{
					this.DbTrace.Error("Rejecting action {0} since mount has never finished at least once successfully.", new object[]
					{
						this.ActionCode
					});
					throw new AmDatabaseNeverMountedException();
				}
			}
		}

		// Token: 0x040000DB RID: 219
		private readonly TimeSpan DismountTimeoutAsync = TimeSpan.Zero;

		// Token: 0x040000DC RID: 220
		private readonly TimeSpan DismountTimeoutShort = TimeSpan.FromSeconds((double)RegistryParameters.PamToSamDismountRpcTimeoutShortInSec);

		// Token: 0x040000DD RID: 221
		private readonly TimeSpan DismountTimeoutMedium = TimeSpan.FromSeconds((double)RegistryParameters.PamToSamDismountRpcTimeoutMediumInSec);

		// Token: 0x040000DE RID: 222
		private readonly TimeSpan DismountTimeoutInfinite = InvokeWithTimeout.InfiniteTimeSpan;

		// Token: 0x040000DF RID: 223
		private readonly IADDatabase m_database;

		// Token: 0x040000E0 RID: 224
		private AmDbStateInfo m_state;

		// Token: 0x040000E1 RID: 225
		private TimeSpan? m_lockTimeout = null;

		// Token: 0x040000E2 RID: 226
		private AmDbTrace m_dbTrace;

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x06000253 RID: 595
		internal delegate object[] PrepareSubactionArgsDelegate(params object[] args);

		// Token: 0x02000031 RID: 49
		internal enum DismountMode
		{
			// Token: 0x040000EB RID: 235
			None,
			// Token: 0x040000EC RID: 236
			ThroughReplayService,
			// Token: 0x040000ED RID: 237
			DirectlyToStore
		}
	}
}
