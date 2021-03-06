using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000026 RID: 38
	internal class ActiveManagerCore : IServiceComponent
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00009C59 File Offset: 0x00007E59
		internal ActiveManagerCore(IReplicaInstanceManager replicaInstanceManager, IReplayAdObjectLookup adLookup, IADConfig adConfig)
		{
			this.m_replicaInstanceManager = replicaInstanceManager;
			this.AdLookup = adLookup;
			this.m_registryMonitor = new RegistryMonitor(adConfig);
			ActiveManagerServerPerfmon.GetServerForDatabaseServerCalls.RawValue = 0L;
			ActiveManagerServerPerfmon.GetServerForDatabaseServerCallsPerSec.RawValue = 0L;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009C93 File Offset: 0x00007E93
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009C9B File Offset: 0x00007E9B
		public IReplayAdObjectLookup AdLookup { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009CA4 File Offset: 0x00007EA4
		public string Name
		{
			get
			{
				return "Active Manager";
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009CAB File Offset: 0x00007EAB
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.ActiveManager;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00009CAE File Offset: 0x00007EAE
		public bool IsCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009CB1 File Offset: 0x00007EB1
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public bool Start()
		{
			AmTrace.Debug("Starting Active Manager server", new object[0]);
			AmSystemManager.Instance.Start();
			this.m_registryMonitor.Start();
			bool flag = false;
			return !flag;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009CF2 File Offset: 0x00007EF2
		public void Stop()
		{
			AmTrace.Debug("Stopping Active Manager server", new object[0]);
			this.m_registryMonitor.Stop();
			AmSystemManager.Instance.Stop();
			AmTrace.Debug("Finished stopping Active Manager", new object[0]);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009D2C File Offset: 0x00007F2C
		internal static bool IsLocalNodePAM()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			AmTrace.Debug("IsLocalNodePAM(): AM is in '{0}' role.", new object[]
			{
				config.Role
			});
			return config.IsPAM;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009D6C File Offset: 0x00007F6C
		internal static bool IsLocalNodePubliclyUp()
		{
			AmServerName localComputerName = AmServerName.LocalComputerName;
			AmConfig config = AmSystemManager.Instance.Config;
			bool flag = false;
			if (config.IsUnknown)
			{
				AmTrace.Error("IsLocalNodePubliclyUp( {0} ): Returning 'false' since AM role is Unknown.", new object[]
				{
					localComputerName
				});
			}
			else if (config.IsPamOrSam)
			{
				flag = config.DagConfig.IsNodePubliclyUp(localComputerName);
				AmTrace.Debug("IsLocalNodePubliclyUp( {0} ): Returning '{1}' for PAM/SAM role.", new object[]
				{
					localComputerName,
					flag
				});
			}
			else
			{
				AmTrace.Debug("IsLocalNodePubliclyUp( {0} ): Returning 'true' since AM is in Standalone role.", new object[]
				{
					localComputerName
				});
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009E04 File Offset: 0x00008004
		internal static MountStatus GetDatabaseMountStatus(Guid dbGuid, out string activeServer)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			activeServer = string.Empty;
			if (config.IsUnknown)
			{
				AmTrace.Warning("GetDatabaseMountStatus: returning Unknown since AM role is Unknown", new object[0]);
				return MountStatus.Unknown;
			}
			AmDbStateInfo amDbStateInfo = config.DbState.Read(dbGuid);
			if (amDbStateInfo.IsEntryExist)
			{
				activeServer = amDbStateInfo.ActiveServer.NetbiosName;
				return amDbStateInfo.MountStatus;
			}
			AmTrace.Debug("GetDatabaseMountStatus: returning Dismounted since we failed to find the AmDbStateInfo for {0}, it could be a brand new db", new object[]
			{
				dbGuid.ToString()
			});
			return MountStatus.Dismounted;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009E8C File Offset: 0x0000808C
		internal static AmDbStateInfo GetDatabaseStateInfo(Guid dbGuid)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsUnknown)
			{
				return config.DbState.Read(dbGuid);
			}
			AmTrace.Error("GetDatabaseStateInfo: AmConfig is invalid!", new object[0]);
			throw new AmInvalidConfiguration(config.LastError);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009ED4 File Offset: 0x000080D4
		internal static long GetLastLogGenerationNumber(Guid dbGuid)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsUnknown)
			{
				AmTrace.Error("GetLastLogGenerationNumber: AmConfig is invalid!", new object[0]);
				throw new AmInvalidConfiguration(config.LastError);
			}
			long result;
			if (config.DbState.GetLastLogGenerationNumber(dbGuid, out result))
			{
				return result;
			}
			AmTrace.Warning("GetLastLogGenerationNumber: returning 0 for DB '{0}' since the corresponding LastLog key was not found.", new object[]
			{
				dbGuid
			});
			return 0L;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009F40 File Offset: 0x00008140
		internal static void SetLastLogGenerationNumber(Guid dbGuid, long lastLogGenNumber)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsUnknown)
			{
				config.DbState.SetLastLogGenerationNumber(dbGuid, lastLogGenNumber);
				config.DbState.SetLastLogGenerationTimeStamp(dbGuid, ExDateTime.Now.ToUtc());
				return;
			}
			AmTrace.Error("SetLastLogGenerationNumber: AmConfig is invalid!", new object[0]);
			throw new AmInvalidConfiguration(config.LastError);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009FA4 File Offset: 0x000081A4
		internal static ExDateTime GetLastLogGenerationTimeStamp(Guid dbGuid, out bool doesValueExist)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsUnknown)
			{
				AmTrace.Error("GetLastLogGenerationTimeStamp: AmConfig is invalid!", new object[0]);
				throw new AmInvalidConfiguration(config.LastError);
			}
			ExDateTime minValue;
			doesValueExist = config.DbState.GetLastLogGenerationTimeStamp(dbGuid, out minValue);
			if (doesValueExist)
			{
				return minValue;
			}
			minValue = ExDateTime.MinValue;
			AmTrace.Warning("GetLastLogGenerationTimeStamp: returning {0} for DB '{1}' since the corresponding LastLog key was not found.", new object[]
			{
				minValue,
				dbGuid
			});
			return minValue;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A024 File Offset: 0x00008224
		internal static void SetLastLogGenerationTimeStamp(Guid dbGuid, ExDateTime timeStamp)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsUnknown)
			{
				config.DbState.SetLastLogGenerationTimeStamp(dbGuid, timeStamp);
				return;
			}
			AmTrace.Error("SetLastLogGenerationTimeStamp: AmConfig is invalid!", new object[0]);
			throw new AmInvalidConfiguration(config.LastError);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A078 File Offset: 0x00008278
		internal static bool AttemptServerSwitchoverOnShutdown()
		{
			AmTrace.Entering("ActiveManagerCore.AttemptServerSwitchoverOnShutdown", new object[0]);
			ExDateTime utcNow = ExDateTime.UtcNow;
			ReplayEventLogConstants.Tuple_PreShutdownStart.LogEvent(null, new object[0]);
			Exception ex = null;
			bool result = false;
			try
			{
				AmConfig config = AmSystemManager.Instance.Config;
				if (config.IsPamOrSam)
				{
					string fqdn = config.DagConfig.CurrentPAM.Fqdn;
					AmTrace.Debug("{0} Trying to mount all the databases on other servers", new object[]
					{
						ExDateTime.Now
					});
					AmRpcClientHelper.ServerSwitchOver(fqdn, AmServerName.LocalComputerName.Fqdn);
					if (config.IsPAM)
					{
						AmTrace.Debug("{0} Trying to move PAM off this node", new object[]
						{
							ExDateTime.Now
						});
						using (IAmCluster amCluster = ClusterFactory.Instance.Open())
						{
							using (IAmClusterGroup amClusterGroup = amCluster.FindCoreClusterGroup())
							{
								TimeSpan ts = ExDateTime.UtcNow.Subtract(utcNow);
								TimeSpan timeSpan = TimeSpan.FromSeconds(115.0).Subtract(ts);
								if (!(timeSpan <= TimeSpan.Zero))
								{
									if (amClusterGroup.MoveGroupToReplayEnabledNode((string targetNode) => AmHelper.IsReplayRunning(targetNode), "Network Name", timeSpan, out fqdn))
									{
										ReplayEventLogConstants.Tuple_SuccMovePAM.LogEvent(null, new object[]
										{
											Environment.MachineName,
											fqdn
										});
										AmTrace.Debug("{0} Moved PAM to another node", new object[]
										{
											ExDateTime.Now
										});
										goto IL_197;
									}
								}
								ReplayEventLogConstants.Tuple_FailedMovePAM.LogEvent(null, new object[]
								{
									Environment.MachineName
								});
								IL_197:;
							}
						}
					}
					result = true;
					ReplayEventLogConstants.Tuple_PreShutdownOK.LogEvent(null, new object[0]);
				}
			}
			catch (ClusterException ex2)
			{
				ex = ex2;
			}
			catch (AmRpcException ex3)
			{
				ex = ex3;
			}
			catch (AmServerTransientException ex4)
			{
				ex = ex4;
			}
			catch (AmServerException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (ex != null)
				{
					ReplayEventLogConstants.Tuple_PreShutdownFailed.LogEvent(null, new object[]
					{
						ex.Message
					});
				}
				AmTrace.Leaving("ActiveManagerCore.AttemptServerSwitchoverOnShutdown", new object[0]);
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000A34C File Offset: 0x0000854C
		internal static void ValidatePamOrStandalone(string operation)
		{
			ActiveManagerCore.ValidateForCurrentRole(true, operation);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A358 File Offset: 0x00008558
		internal static void ValidateForCurrentRole(bool isStandaloneAllowed, string operation)
		{
			if (RegistryParameters.ConfigInitializedCheckTimeoutInSec > 0)
			{
				ManualOneShotEvent.Result result = AmSystemManager.Instance.ConfigInitializedEvent.WaitOne(TimeSpan.FromSeconds((double)RegistryParameters.ConfigInitializedCheckTimeoutInSec));
				if (result != ManualOneShotEvent.Result.Success)
				{
					ReplayCrimsonEvents.ConfigInitializedEventNotSet.LogPeriodic<TimeSpan, ManualOneShotEvent.Result, string>(AmSystemManager.Instance, DiagCore.DefaultEventSuppressionInterval, TimeSpan.FromSeconds((double)RegistryParameters.ConfigInitializedCheckTimeoutInSec), result, operation);
				}
			}
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.Role == AmRole.SAM)
			{
				AmReferralException ex = new AmReferralException(config.DagConfig.CurrentPAM.Fqdn);
				throw ex;
			}
			if (!isStandaloneAllowed && config.Role == AmRole.Standalone)
			{
				throw new AmOperationInvalidForStandaloneRoleException();
			}
			if (config.Role == AmRole.Unknown)
			{
				throw new AmInvalidConfiguration(config.LastError);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A400 File Offset: 0x00008600
		internal AmDbStatusInfo2 AmServerGetServerForDatabase(Guid mdbGuid)
		{
			ActiveManagerServerPerfmon.GetServerForDatabaseServerCalls.Increment();
			ActiveManagerServerPerfmon.GetServerForDatabaseServerCallsPerSec.Increment();
			AmServerDbStatusInfoCache statusInfoCache = AmSystemManager.Instance.StatusInfoCache;
			return statusInfoCache.GetEntry(mdbGuid);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A438 File Offset: 0x00008638
		internal void MountDatabase(Guid mdbGuid, MountFlags storeFlags, AmMountFlags amMountFlags, DatabaseMountDialOverride mountDialOverride, AmDbActionCode actionCode)
		{
			AmTrace.Debug("MountDatabase({0}, storeFlags({1}), amFlags({2}), {3}, {4}) called", new object[]
			{
				mdbGuid,
				storeFlags,
				amMountFlags,
				mountDialOverride,
				actionCode
			});
			ActiveManagerCore.ValidatePamOrStandalone("MountDatabase");
			IADDatabase iaddatabase = this.AdLookup.DatabaseLookup.FindAdObjectByGuidEx(mdbGuid, AdObjectLookupFlags.ReadThrough);
			if (iaddatabase == null)
			{
				throw new AmDatabaseNotFoundException(mdbGuid);
			}
			AmDbMountOperation amDbMountOperation = new AmDbMountOperation(iaddatabase, actionCode);
			amDbMountOperation.StoreMountFlags = storeFlags;
			amDbMountOperation.AmMountFlags = amMountFlags;
			amDbMountOperation.MountDialOverride = mountDialOverride;
			amDbMountOperation.Enqueue();
			AmDbCompletionReason amDbCompletionReason = amDbMountOperation.Wait();
			AmTrace.Debug("MountDatabase({0}) completed (reason={1})", new object[]
			{
				mdbGuid,
				amDbCompletionReason
			});
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A4FC File Offset: 0x000086FC
		internal void DismountDatabase(Guid mdbGuid, UnmountFlags flags, AmDbActionCode actionCode)
		{
			AmTrace.Debug("Task: DismountDatabase({0},{1},{2}) called", new object[]
			{
				mdbGuid,
				flags,
				actionCode
			});
			ActiveManagerCore.ValidatePamOrStandalone("DismountDatabase");
			IADDatabase iaddatabase = this.AdLookup.DatabaseLookup.FindAdObjectByGuidEx(mdbGuid, AdObjectLookupFlags.ReadThrough);
			if (iaddatabase == null)
			{
				throw new AmDatabaseNotFoundException(mdbGuid);
			}
			AmDbDismountOperation amDbDismountOperation = new AmDbDismountOperation(iaddatabase, actionCode);
			amDbDismountOperation.Flags = flags;
			amDbDismountOperation.Enqueue();
			AmDbCompletionReason amDbCompletionReason = amDbDismountOperation.Wait();
			AmTrace.Debug("DismountDatabase({0}) completed (reason={1})", new object[]
			{
				mdbGuid,
				amDbCompletionReason
			});
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A59C File Offset: 0x0000879C
		internal void MoveDatabase(Guid mdbGuid, MountFlags mountFlags, UnmountFlags dismountFlags, DatabaseMountDialOverride mountDialOverride, AmServerName fromServer, AmServerName targetServer, bool tryOtherHealthyServers, AmBcsSkipFlags skipValidationChecks, AmDbActionCode actionCode, string moveComment, ref AmDatabaseMoveResult databaseMoveResult)
		{
			AmTrace.Debug("Task: MoveDatabase({0},{1},{2},{3},{4},{5},{6},{7},'{8}') called", new object[]
			{
				mdbGuid,
				mountFlags,
				dismountFlags,
				mountDialOverride,
				fromServer,
				targetServer,
				tryOtherHealthyServers,
				actionCode,
				moveComment
			});
			ActiveManagerCore.ValidatePamOrStandalone("MoveDatabase");
			ThirdPartyManager.PreventOperationWhenTPREnabled("MoveDatabase");
			IADDatabase iaddatabase = this.AdLookup.DatabaseLookup.FindAdObjectByGuidEx(mdbGuid, AdObjectLookupFlags.ReadThrough);
			if (iaddatabase == null)
			{
				throw new AmDatabaseNotFoundException(mdbGuid);
			}
			AmDbMoveOperation amDbMoveOperation = new AmDbMoveOperation(iaddatabase, actionCode);
			AmDbMoveArguments arguments = amDbMoveOperation.Arguments;
			AmDbCompletionReason amDbCompletionReason = AmDbCompletionReason.None;
			arguments.MountFlags = mountFlags;
			arguments.DismountFlags = dismountFlags;
			arguments.MountDialOverride = mountDialOverride;
			arguments.SourceServer = fromServer;
			arguments.TargetServer = targetServer;
			arguments.TryOtherHealthyServers = tryOtherHealthyServers;
			arguments.SkipValidationChecks = skipValidationChecks;
			arguments.MoveComment = moveComment;
			amDbMoveOperation.Arguments = arguments;
			amDbMoveOperation.Enqueue();
			try
			{
				amDbCompletionReason = amDbMoveOperation.Wait();
			}
			finally
			{
				if (amDbMoveOperation.DetailedStatus != null)
				{
					databaseMoveResult = amDbMoveOperation.ConvertDetailedStatusToRpcMoveResult(amDbMoveOperation.DetailedStatus);
				}
			}
			AmTrace.Debug("MoveDatabase({0}) completed (reason={1})", new object[]
			{
				mdbGuid,
				amDbCompletionReason
			});
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A6F4 File Offset: 0x000088F4
		internal void ServerSwitchOver(AmServerName sourceServer)
		{
			AmTrace.Debug("ServerSwitchover({0}) called", new object[]
			{
				sourceServer
			});
			ActiveManagerCore.ValidatePamOrStandalone("ServerSwitchOver");
			AmEvtSwitchoverOnShutdown amEvtSwitchoverOnShutdown = new AmEvtSwitchoverOnShutdown(sourceServer);
			amEvtSwitchoverOnShutdown.Notify();
			amEvtSwitchoverOnShutdown.WaitForSwitchoverComplete();
			AmTrace.Debug("ServerSwitchover({0}) completed", new object[]
			{
				sourceServer
			});
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A74C File Offset: 0x0000894C
		internal List<AmDatabaseMoveResult> ServerMoveAllDatabases(AmServerName sourceServer, AmServerName targetServer, MountFlags mountFlags, UnmountFlags dismountFlags, DatabaseMountDialOverride mountDialOverride, bool tryOtherHealthyServers, AmBcsSkipFlags skipValidationChecks, AmDbActionCode actionCode, string moveComment, string componentName)
		{
			AmTrace.Debug("ServerMoveAllDatabases() called: sourceServer='{0}', targetServer='{1}', mountFlags='{2}', dismountFlags='{3}', mountDialOverride='{4}', tryOtherHealthyServers='{5}', skipValidationChecks='{6}', actionCode='{7}', MoveComment='{8}' Component='{9}'", new object[]
			{
				sourceServer,
				targetServer,
				mountFlags,
				dismountFlags,
				mountDialOverride,
				tryOtherHealthyServers,
				skipValidationChecks,
				actionCode,
				moveComment,
				componentName
			});
			ActiveManagerCore.ValidatePamOrStandalone("ServerMoveAllDatabases");
			ThirdPartyManager.PreventOperationWhenTPREnabled("ServerMoveAllDatabases");
			AmDbMoveArguments amDbMoveArguments = new AmDbMoveArguments(actionCode);
			amDbMoveArguments.SourceServer = sourceServer;
			amDbMoveArguments.TargetServer = targetServer;
			amDbMoveArguments.MountFlags = mountFlags;
			amDbMoveArguments.DismountFlags = dismountFlags;
			amDbMoveArguments.MountDialOverride = mountDialOverride;
			amDbMoveArguments.TryOtherHealthyServers = tryOtherHealthyServers;
			amDbMoveArguments.SkipValidationChecks = skipValidationChecks;
			amDbMoveArguments.MoveComment = moveComment;
			amDbMoveArguments.ComponentName = componentName;
			if (string.IsNullOrEmpty(sourceServer.Fqdn))
			{
				return MoveBackToServer.Move(amDbMoveArguments);
			}
			AmEvtMoveAllDatabasesBase amEvtMoveAllDatabasesBase;
			if (actionCode.IsAutomaticManagedAvailabilityFailover)
			{
				amEvtMoveAllDatabasesBase = new AmEvtMoveAllDatabasesOnComponentRequest(sourceServer);
			}
			else
			{
				amEvtMoveAllDatabasesBase = new AmEvtMoveAllDatabasesOnAdminRequest(sourceServer);
			}
			amEvtMoveAllDatabasesBase.MoveArgs = amDbMoveArguments;
			amEvtMoveAllDatabasesBase.Notify();
			amEvtMoveAllDatabasesBase.WaitForSwitchoverComplete();
			AmTrace.Debug("ServerMoveAllDatabases({0}) completed", new object[]
			{
				sourceServer
			});
			return amEvtMoveAllDatabasesBase.GetMoveResultsForOperationsRun();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A874 File Offset: 0x00008A74
		internal AmPamInfo GetPrimaryActiveManager()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsPamOrSam)
			{
				AmTrace.Debug("GetPAM called on Standalone machine!!", new object[0]);
				throw new AmOperationNotValidOnCurrentRole(config.LastError);
			}
			return new AmPamInfo(config.DagConfig.CurrentPAM.Fqdn);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A8CC File Offset: 0x00008ACC
		internal AmRole GetActiveManagerRole(out string errorMessage)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			errorMessage = config.LastError;
			AmTrace.Debug("GetActiveManagerRole: AM is in Role {0}. ErrorMessage={1}", new object[]
			{
				config.Role,
				errorMessage
			});
			return config.Role;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A918 File Offset: 0x00008B18
		internal void ReportSystemEvent(AmSystemEventCode eventCode, string reportingServer)
		{
			AmTrace.Debug("ReportSystemEvent({0},{1}) called", new object[]
			{
				eventCode,
				reportingServer
			});
			ReplayCrimsonEvents.SystemEventReported.Log<AmSystemEventCode, string>(eventCode, reportingServer);
			ActiveManagerCore.ValidatePamOrStandalone("ReportSystemEvent");
			AmServerName amServerName = new AmServerName(reportingServer);
			AmSystemManager.Instance.StoreStateMarker.SetStoreState(amServerName, eventCode);
			if (eventCode == AmSystemEventCode.StoreServiceStarted)
			{
				AmEvtStoreServiceStarted amEvtStoreServiceStarted = new AmEvtStoreServiceStarted(amServerName);
				amEvtStoreServiceStarted.Notify();
				return;
			}
			if (eventCode == AmSystemEventCode.StoreServiceStopped)
			{
				new AmEvtStoreServiceStopped(amServerName)
				{
					IsGracefulStop = true
				}.Notify();
				return;
			}
			if (eventCode == AmSystemEventCode.StoreServiceUnexpectedlyStopped)
			{
				new AmEvtStoreServiceStopped(amServerName)
				{
					IsGracefulStop = false
				}.Notify();
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A9BC File Offset: 0x00008BBC
		internal void AmRefreshConfiguration(AmRefreshConfigurationFlags refreshFlags, int maxSecondsToWait)
		{
			ExDateTime now = ExDateTime.Now;
			AmConfig config = AmSystemManager.Instance.Config;
			Dependencies.ADConfig.Refresh("AmRefreshConfiguration");
			AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
			ExDateTime t = now.AddSeconds((double)maxSecondsToWait);
			if ((refreshFlags & AmRefreshConfigurationFlags.Wait) == AmRefreshConfigurationFlags.Wait)
			{
				while (object.ReferenceEquals(config, AmSystemManager.Instance.Config) && ExDateTime.Now < t)
				{
					Thread.Sleep(2000);
				}
				if (object.ReferenceEquals(config, AmSystemManager.Instance.Config))
				{
					throw new AmRefreshConfigTimeoutException(maxSecondsToWait);
				}
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000AA50 File Offset: 0x00008C50
		internal void RemountDatabase(Guid mdbGuid, MountFlags mountFlags, DatabaseMountDialOverride mountDialOverride, AmServerName fromServer, AmDbActionCode actionCode)
		{
			AmTrace.Debug("RemountDatabase ({0},{1},{2},{3},{4}) called", new object[]
			{
				mdbGuid,
				mountFlags,
				mountDialOverride,
				fromServer,
				actionCode
			});
			ActiveManagerCore.ValidatePamOrStandalone("RemountDatabase");
			IADDatabase iaddatabase = this.AdLookup.DatabaseLookup.FindAdObjectByGuidEx(mdbGuid, AdObjectLookupFlags.ReadThrough);
			if (iaddatabase == null)
			{
				throw new AmDatabaseNotFoundException(mdbGuid);
			}
			AmDbRemountOperation amDbRemountOperation = new AmDbRemountOperation(iaddatabase, actionCode);
			amDbRemountOperation.Flags = mountFlags;
			amDbRemountOperation.MountDialOverride = mountDialOverride;
			amDbRemountOperation.FromServer = fromServer;
			amDbRemountOperation.Enqueue();
			AmDbCompletionReason amDbCompletionReason = amDbRemountOperation.Wait();
			AmTrace.Debug("RemountDatabase({0}) completed (reason={1})", new object[]
			{
				mdbGuid,
				amDbCompletionReason
			});
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000AB10 File Offset: 0x00008D10
		internal void AttemptCopyLastLogsDirect(Guid mdbGuid, AmAcllArgs acllArgs, ref AmAcllReturnStatus acllStatus)
		{
			acllStatus = new AmAcllReturnStatus();
			ThirdPartyManager.PreventOperationWhenTPREnabled("AttemptCopyLastLogsDirect");
			if (!AmHelper.IsDatabaseRcrEnabled(mdbGuid))
			{
				throw new AmDbMoveOperationNotSupportedException(mdbGuid.ToString());
			}
			AmTrace.Debug("AttemptCopyLastLogsDirect: Calling AmAcllCallback ({0})", new object[]
			{
				mdbGuid
			});
			acllStatus = this.m_replicaInstanceManager.AmAttemptCopyLastLogsCallback(mdbGuid, acllArgs);
			if (acllArgs.ActionCode.IsAutomaticShutdownSwitchover)
			{
				if (!acllStatus.NoLoss)
				{
					ReplayCrimsonEvents.AcllFailedOnSwitchover.Log<Guid>(mdbGuid);
					throw new AcllFailedException(acllStatus.LastError);
				}
				ReplayCrimsonEvents.AcllLosslessOnSwitchover.Log<Guid>(mdbGuid);
				return;
			}
			else
			{
				if (!acllStatus.NoLoss && acllStatus.MountAllowed)
				{
					ReplayCrimsonEvents.LossyMountEnabled.Log<Guid>(mdbGuid);
					AmTrace.Warning("AttemptCopyLastLogsDirect: ACLL detected a lossy mount will be allowed for DB {0}.", new object[]
					{
						mdbGuid
					});
					return;
				}
				if (acllStatus.MountAllowed)
				{
					AmTrace.Debug("AttemptCopyLastLogsDirect: ACLL completed with no loss for DB {0}.", new object[]
					{
						mdbGuid
					});
					ReplayCrimsonEvents.AcllLosslessOnMoveOrFailover.Log<Guid>(mdbGuid);
					return;
				}
				if (string.IsNullOrEmpty(acllStatus.LastError))
				{
					ReplayCrimsonEvents.MountRejected.Log<Guid>(mdbGuid);
					throw new AmDbMountNotAllowedDueToLossException();
				}
				ReplayCrimsonEvents.MountRejectedAcllError.Log<Guid, string>(mdbGuid, acllStatus.LastError);
				throw new AmDbMountNotAllowedDueToAcllErrorException(acllStatus.LastError, acllStatus.NumberOfLogsLost);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000AD64 File Offset: 0x00008F64
		internal void MountDatabaseDirect(Guid mdbGuid, MountFlags storeFlags, AmMountFlags amMountFlags, AmDbActionCode actionCode)
		{
			AmTrace.Debug("MountDatabaseDirect called. (Guid={0}, StoreFlags={1}, AmMountFlags={2} ActionCode={3})", new object[]
			{
				mdbGuid,
				storeFlags,
				amMountFlags,
				actionCode
			});
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsUnknown)
			{
				bool flag = false;
				LogStreamResetOnMount logReset = null;
				MountDirectPerformanceTracker mountPerf = new MountDirectPerformanceTracker(mdbGuid);
				ReplicaInstanceContext replicaInstanceContext = null;
				try
				{
					int actualMountFlags = (int)storeFlags;
					IADDatabase db = this.AdLookup.DatabaseLookup.FindAdObjectByGuidEx(mdbGuid, AdObjectLookupFlags.None);
					if (db == null)
					{
						throw new AmDatabaseNotFoundException(mdbGuid);
					}
					if (AmSystemManager.Instance.IsSystemShutdownInProgress)
					{
						ReplayCrimsonEvents.MountRejectedSinceSystemShutdownInProgress.Log<string, Guid, ExDateTime?>(db.Name, mdbGuid, AmSystemManager.Instance.SystemShutdownStartTime);
						throw new AmInvalidConfiguration(string.Empty);
					}
					ExTraceGlobals.FaultInjectionTracer.TraceTest(2198220093U);
					mountPerf.RunTimedOperation(MountDatabaseDirectOperation.AmPreMountCallback, delegate
					{
						this.m_replicaInstanceManager.AmPreMountCallback(mdbGuid, ref actualMountFlags, amMountFlags, mountPerf, out logReset, out replicaInstanceContext);
					});
					mountPerf.RunTimedOperation(MountDatabaseDirectOperation.RegistryReplicatorCopy, delegate
					{
						this.m_registryMonitor.PreMountCopy(mdbGuid, db.IsPublicFolderDatabase);
					});
					if (replicaInstanceContext != null)
					{
						replicaInstanceContext.IsReplayDatabaseDismountPending = false;
					}
					ExTraceGlobals.FaultInjectionTracer.TraceTest(2166762813U);
					try
					{
						mountPerf.RunTimedOperation(MountDatabaseDirectOperation.StoreMount, delegate
						{
							AmStoreHelper.Mount(mdbGuid, (MountFlags)actualMountFlags);
						});
					}
					catch (MapiExceptionDismountInProgress mapiExceptionDismountInProgress)
					{
						AmTrace.Error("AmStoreHelper.Mount encountered exception {0}", new object[]
						{
							mapiExceptionDismountInProgress
						});
						mountPerf.IsDismountInProgress = true;
						throw;
					}
					((ReplicaInstanceManager)this.m_replicaInstanceManager).ClearLossyMountRecord(mdbGuid);
					((ReplicaInstanceManager)this.m_replicaInstanceManager).ClearLastAcllRunWithSkipHealthChecksRecord(mdbGuid);
					AmTrace.Debug("MountDatabaseDirect initiating UpdateReplicationContentIndexing() asynchronously for DB '{0}'.", new object[]
					{
						mdbGuid
					});
					ThreadPool.QueueUserWorkItem(delegate(object obj)
					{
						Exception ex = ReplicaInstance.UpdateReplicationContentIndexing();
						if (ex != null)
						{
							AmTrace.Error("MountDatabaseDirect background call of UpdateReplicationContentIndexing() failed for DB '{0}'. Exception: {1}", new object[]
							{
								(Guid)obj,
								ex
							});
						}
					}, mdbGuid);
					this.DetermineWorkerProcessId(mdbGuid);
					this.m_registryMonitor.PostMountCopy();
					flag = true;
					return;
				}
				finally
				{
					if (replicaInstanceContext != null)
					{
						replicaInstanceContext.BestEffortDismountReplayDatabase();
					}
					if (logReset != null && logReset.ResetPending)
					{
						if (flag)
						{
							mountPerf.RunTimedOperation(MountDatabaseDirectOperation.ConfirmLogReset, delegate
							{
								logReset.ConfirmLogReset();
							});
							Dependencies.ConfigurationUpdater.NotifyChangedReplayConfiguration(mdbGuid, false, true, true, true, ReplayConfigChangeHints.AmPreMountCallbackLogStreamReset, -1);
						}
						else
						{
							logReset.CancelLogReset();
						}
					}
					mountPerf.LogEvent();
				}
			}
			AmTrace.Error("MountDatabaseDirect: AmConfig is invalid!", new object[0]);
			throw new AmInvalidConfiguration(config.LastError);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B0E0 File Offset: 0x000092E0
		private void DetermineWorkerProcessId(Guid mdbGuid)
		{
			Exception ex = null;
			try
			{
				using (IStoreRpc newStoreControllerInstance = Dependencies.GetNewStoreControllerInstance(null))
				{
					int processId;
					int num;
					int num2;
					int num3;
					newStoreControllerInstance.GetDatabaseProcessInfo(mdbGuid, out processId, out num, out num2, out num3);
					((ReplicaInstanceManager)this.m_replicaInstanceManager).SetWorkerProcessId(mdbGuid, processId);
				}
			}
			catch (MapiRetryableException ex2)
			{
				ex = ex2;
			}
			catch (MapiPermanentException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					AmTrace.Error("DetermineWorkerProcessId caught {0}", new object[]
					{
						ex
					});
					ReplayEventLogConstants.Tuple_DetermineWorkerProcessIdFailed.LogEvent(Environment.MachineName, new object[]
					{
						mdbGuid,
						ex
					});
				}
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B1EC File Offset: 0x000093EC
		internal void DismountDatabaseDirect(Guid mdbGuid, UnmountFlags flags, AmDbActionCode actionCode)
		{
			AmTrace.Debug("DismountDatabaseDirect called. (Guid={0}, Flags={1}, ActionCode={2})", new object[]
			{
				mdbGuid,
				flags,
				actionCode
			});
			Exception ex = AmStoreHelper.Dismount(mdbGuid, flags);
			if (ex != null)
			{
				AmTrace.Error("DismountDatabaseDirect: Store dismount RPC for ({0}) threw exception: {1}", new object[]
				{
					mdbGuid,
					ex
				});
				throw ex;
			}
			AmTrace.Debug("DismountDatabaseDirect: Store dismount RPC for ({0}) completed.", new object[]
			{
				mdbGuid
			});
			AmTrace.Debug("DismountDatabaseDirect initiating UpdateReplicationContentIndexing() asynchronously for DB '{0}'.", new object[]
			{
				mdbGuid
			});
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				Exception ex2 = ReplicaInstance.UpdateReplicationContentIndexing();
				if (ex2 != null)
				{
					AmTrace.Error("DismountDatabaseDirect background call of UpdateReplicationContentIndexing() failed for DB '{0}'. Exception: {1}", new object[]
					{
						(Guid)obj,
						ex2
					});
				}
			}, mdbGuid);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000B2B4 File Offset: 0x000094B4
		internal void ReportServiceKill(string serviceName, AmServerName serverName, ExDateTime timeStampInUtc)
		{
			AmServiceKillStatusContainer serviceKillStatusContainer = AmSystemManager.Instance.ServiceKillStatusContainer;
			if (serviceKillStatusContainer != null)
			{
				serviceKillStatusContainer.UpdateStatus(serviceName, serverName, timeStampInUtc);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000B2D8 File Offset: 0x000094D8
		internal List<AmDeferredRecoveryEntry> GetDeferredRecoveryEntries()
		{
			ActiveManagerCore.ValidateForCurrentRole(false, "GetDeferredRecoveryEntries");
			return AmSystemManager.Instance.TransientFailoverSuppressor.GetEntriesForTask();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B2F4 File Offset: 0x000094F4
		internal void GenericRpcDispatch(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			switch (requestInfo.CommandId)
			{
			case 1:
				RpcKillServiceImpl.HandleRequest(requestInfo, ref replyInfo);
				return;
			case 2:
				RpcUpdateLastLogImpl.HandleRequest(requestInfo, ref replyInfo);
				return;
			default:
				throw new ActiveManagerUnknownGenericRpcCommandException(requestInfo.ServerVersion, ActiveManagerGenericRpcHelper.LocalServerVersion, requestInfo.CommandId);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B340 File Offset: 0x00009540
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x040000AA RID: 170
		private IReplicaInstanceManager m_replicaInstanceManager;

		// Token: 0x040000AB RID: 171
		private RegistryMonitor m_registryMonitor;
	}
}
