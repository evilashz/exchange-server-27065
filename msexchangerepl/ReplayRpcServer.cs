using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.HA.ManagedAvailability;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplayRpcServer : ReplayRpcServerBase
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000034FF File Offset: 0x000016FF
		public static bool Started
		{
			get
			{
				return ReplayRpcServer.m_fRpcServerStarted;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003508 File Offset: 0x00001708
		public static bool TryStart(IReplicaInstanceManager replicaInstanceManager, SeedManager seedManager, HealthStateTracker healthStateTracker)
		{
			bool fRpcServerStarted;
			lock (ReplayRpcServer.m_locker)
			{
				if (!ReplayRpcServer.m_fRpcServerStarted)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "Start(): Starting Replay RPC Server...");
					ReplayRpcServer.m_seedmanager = seedManager;
					ReplayRpcServer.m_replicaInstanceManager = replicaInstanceManager;
					ReplayRpcServer.m_healthStateTracker = healthStateTracker;
					try
					{
						FileSecurity replayRpcSecurity = ObjectSecurity.ReplayRpcSecurity;
						FileSecurity baseRpcSecurity = ObjectSecurity.BaseRpcSecurity;
						if (replayRpcSecurity != null)
						{
							ReplayRpcServer.m_server = (ReplayRpcServer)RpcServerBase.RegisterServer(typeof(ReplayRpcServer), replayRpcSecurity, 1, false, (uint)RegistryParameters.MaximumRpcThreadCount);
							if (ReplayRpcServer.m_server != null)
							{
								ReplayRpcServer.m_server.BaseSecurityDescriptor = baseRpcSecurity;
								ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "Start(): Replay RPC Server successfully started.");
								ReplayRpcServer.m_fRpcServerStarted = true;
							}
						}
						if (ReplayRpcServer.m_server == null)
						{
							ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "Start(): Replay RPC Server failed to start!");
							ReplayEventLogConstants.Tuple_RpcServerFailedToStart.LogEvent(ReplayRpcServer.m_locker.GetHashCode().ToString(), new object[0]);
						}
						goto IL_223;
					}
					catch (RpcException ex)
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string>(0L, "Start(): RPC error occured while registering the Replay RPC server. Exception: {0}", ex.ToString());
						ReplayEventLogConstants.Tuple_ReplayRpcServerFailedToRegister.LogEvent(ReplayRpcServer.m_locker.GetHashCode().ToString(), new object[]
						{
							ex.Message
						});
						goto IL_223;
					}
					catch (ADTransientException ex2)
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string>(0L, "Start(): Transient exception occurred while retrieving the ReplayRpcSecurity object. Exception: {0}", ex2.ToString());
						ReplayEventLogConstants.Tuple_RpcServerFailedToFindExchangeServersUsg.LogEvent(ReplayRpcServer.m_locker.GetHashCode().ToString(), new object[]
						{
							ex2.Message
						});
						goto IL_223;
					}
					catch (ADExternalException ex3)
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string>(0L, "Start(): Permanent exception occurred while retrieving the ReplayRpcSecurity object. Exception: {0}", ex3.ToString());
						ReplayEventLogConstants.Tuple_RpcServerFailedToFindExchangeServersUsg.LogEvent(ReplayRpcServer.m_locker.GetHashCode().ToString(), new object[]
						{
							ex3.Message
						});
						goto IL_223;
					}
					catch (ADOperationException ex4)
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string>(0L, "Start(): Permanent exception occurred while retrieving the ReplayRpcSecurity object. Exception: {0}", ex4.ToString());
						ReplayEventLogConstants.Tuple_RpcServerFailedToFindExchangeServersUsg.LogEvent(ReplayRpcServer.m_locker.GetHashCode().ToString(), new object[]
						{
							ex4.Message
						});
						goto IL_223;
					}
				}
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "Start(): Replay RPC Server is already started. Returning.");
				IL_223:
				fRpcServerStarted = ReplayRpcServer.m_fRpcServerStarted;
			}
			return fRpcServerStarted;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000037CC File Offset: 0x000019CC
		public static void Stop()
		{
			lock (ReplayRpcServer.m_locker)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "Stopping the Replay RPC server...");
				if (ReplayRpcServer.m_server != null)
				{
					RpcServerBase.StopServer(ReplayRpcServerBase.RpcIntfHandle);
					ReplayRpcServer.m_fRpcServerStarted = false;
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "Replay RPC server successfully stopped.");
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003840 File Offset: 0x00001A40
		public ObjectSecurity BaseSecurityDescriptor
		{
			set
			{
				this.m_sdBaseSystemBinaryForm = value.GetSecurityDescriptorBinaryForm();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003850 File Offset: 0x00001A50
		public override RpcErrorExceptionInfo RequestSuspend(Guid guid, string suspendComment)
		{
			return this.RequestSuspend3(guid, suspendComment, 3U, 0U);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000386C File Offset: 0x00001A6C
		public override RpcErrorExceptionInfo RequestSuspend2(Guid guid, string suspendComment, uint flags)
		{
			return this.RequestSuspend3(guid, suspendComment, flags, 0U);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000038D0 File Offset: 0x00001AD0
		private RpcErrorExceptionInfo DoRpcAction(Action operation, IHaRpcExceptionWrapper rpcWrapper, ReplayCrimsonEvent startEvent, ReplayCrimsonEvent successEvent, ReplayCrimsonEvent failEvent, Guid dbGuid, params object[] startEventArgs)
		{
			RpcErrorExceptionInfo rpcErrorExceptionInfo = null;
			bool failed = true;
			int num = (startEventArgs == null) ? 0 : startEventArgs.Length;
			object[] array = new object[num + 1];
			array[0] = dbGuid;
			if (num > 0)
			{
				startEventArgs.CopyTo(array, 1);
			}
			try
			{
				startEvent.LogGeneric(array);
				rpcErrorExceptionInfo = rpcWrapper.RunRpcServerOperation(delegate()
				{
					operation();
					failed = false;
					successEvent.LogGeneric(new object[]
					{
						dbGuid
					});
				});
			}
			finally
			{
				if (failed)
				{
					string text;
					if (rpcErrorExceptionInfo != null && rpcErrorExceptionInfo.ErrorMessage != null)
					{
						text = rpcErrorExceptionInfo.ErrorMessage;
					}
					else
					{
						text = ReplayStrings.UnknownError;
					}
					failEvent.LogGeneric(new object[]
					{
						dbGuid,
						text
					});
				}
			}
			return rpcErrorExceptionInfo;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000039FC File Offset: 0x00001BFC
		public override RpcErrorExceptionInfo RequestSuspend3(Guid guid, string suspendComment, uint flags, uint initiator)
		{
			DatabaseCopyActionFlags suspendFlags = (DatabaseCopyActionFlags)flags;
			ActionInitiatorType suspendInitiator = (ActionInitiatorType)initiator;
			Action operation = delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("suspend RPC called");
				ReplayRpcServer.m_replicaInstanceManager.RequestSuspend(guid, suspendComment, suspendFlags, suspendInitiator);
			};
			return this.DoRpcAction(operation, TasksRpcExceptionWrapper.Instance, ReplayCrimsonEvents.RpcSuspendRequested, ReplayCrimsonEvents.RpcSuspendSucceeded, ReplayCrimsonEvents.RpcSuspendFailed, guid, new object[]
			{
				suspendComment,
				suspendFlags,
				suspendInitiator
			});
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A8C File Offset: 0x00001C8C
		public override RpcErrorExceptionInfo RequestResume(Guid guid)
		{
			return this.RequestResume2(guid, 3U);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public override RpcErrorExceptionInfo RequestResume2(Guid guid, uint flags)
		{
			DatabaseCopyActionFlags resumeFlags = (DatabaseCopyActionFlags)flags;
			Action operation = delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("Resume RPC was issued");
				ReplayRpcServer.m_replicaInstanceManager.RequestResume(guid, resumeFlags);
			};
			return this.DoRpcAction(operation, TasksRpcExceptionWrapper.Instance, ReplayCrimsonEvents.RpcResumeRequested, ReplayCrimsonEvents.RpcResumeSucceeded, ReplayCrimsonEvents.RpcResumeFailed, guid, new object[]
			{
				resumeFlags
			});
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003B68 File Offset: 0x00001D68
		public override RpcErrorExceptionInfo RpcsDisableReplayLag2(Guid dbGuid, string disableReason, ActionInitiatorType actionInitiator)
		{
			Action operation = delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("Disable replay lag");
				ReplayRpcServer.m_replicaInstanceManager.DisableReplayLag(dbGuid, actionInitiator, disableReason);
			};
			return this.DoRpcAction(operation, TasksRpcExceptionWrapper.Instance, ReplayCrimsonEvents.RpcDisableReplayLagRequested, ReplayCrimsonEvents.RpcDisableReplayLagSucceeded, ReplayCrimsonEvents.RpcDisableReplayLagFailed, dbGuid, new object[]
			{
				disableReason
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003BFC File Offset: 0x00001DFC
		public override RpcErrorExceptionInfo RpcsEnableReplayLag2(Guid dbGuid, ActionInitiatorType actionInitiator)
		{
			Action operation = delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("Enable replay lag");
				ReplayRpcServer.m_replicaInstanceManager.EnableReplayLag(dbGuid, actionInitiator);
			};
			return this.DoRpcAction(operation, TasksRpcExceptionWrapper.Instance, ReplayCrimsonEvents.RpcEnableReplayLagRequested, ReplayCrimsonEvents.RpcEnableReplayLagSucceeded, ReplayCrimsonEvents.RpcEnableReplayLagFailed, dbGuid, new object[0]);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003C88 File Offset: 0x00001E88
		public override RpcErrorExceptionInfo GetCopyStatus(RpcGetDatabaseCopyStatusFlags collectionFlags, Guid[] dbGuids, ref RpcDatabaseCopyStatus[] dbStatuses)
		{
			ReplayServerPerfmon.GetCopyStatusServerCalls.Increment();
			ReplayServerPerfmon.GetCopyStatusServerCallsPerSec.Increment();
			RpcDatabaseCopyStatus[] array = null;
			RpcDatabaseCopyStatus2[] tempStatuses2 = null;
			RpcErrorExceptionInfo result = TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				tempStatuses2 = this.GetCopyStatusInternal(this.ConvertLegacyCopyStatusFlags(collectionFlags), dbGuids);
			});
			if (tempStatuses2 != null)
			{
				array = this.ConvertToLegacyCopyStatusArray(tempStatuses2);
			}
			if (array != null)
			{
				dbStatuses = array;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public override RpcErrorExceptionInfo GetCopyStatus2(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, out int nCopyStatusesReturned, out byte[] serializedCopyStatus)
		{
			ReplayServerPerfmon.GetCopyStatusServerCalls.Increment();
			ReplayServerPerfmon.GetCopyStatusServerCallsPerSec.Increment();
			RpcDatabaseCopyStatus2[] tempStatuses = null;
			nCopyStatusesReturned = 0;
			serializedCopyStatus = null;
			ReplayRpcServer.ServerCopyStatusCacheEntry curCacheEntry = this.m_cachedFullCopyStatus;
			bool isFullQuery = false;
			if (dbGuids != null && dbGuids.Length == 1 && dbGuids[0] == Guid.Empty)
			{
				isFullQuery = true;
			}
			RpcErrorExceptionInfo result;
			if ((collectionFlags2 & RpcGetDatabaseCopyStatusFlags2.ReadThrough) != RpcGetDatabaseCopyStatusFlags2.None || curCacheEntry == null || !isFullQuery || DateTime.UtcNow > curCacheEntry.CreateTimeUtc + TimeSpan.FromSeconds((double)RegistryParameters.GetCopyStatusRpcCacheTTLInSec))
			{
				ReplayRpcServer.Tracer.TraceDebug<bool>(0L, "GetCopyStatus2 calculating status. isFullQuery={0}", isFullQuery);
				result = TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
				{
					curCacheEntry = new ReplayRpcServer.ServerCopyStatusCacheEntry();
					tempStatuses = this.GetCopyStatusInternal(collectionFlags2, dbGuids);
					if (tempStatuses != null && tempStatuses.Length > 0)
					{
						curCacheEntry.SerializedCopyStatus = SerializationServices.Serialize(tempStatuses);
						curCacheEntry.NumberOfCopyStatusesReturned = tempStatuses.Length;
					}
					if (isFullQuery && (this.m_cachedFullCopyStatus == null || curCacheEntry.CreateTimeUtc > this.m_cachedFullCopyStatus.CreateTimeUtc))
					{
						this.m_cachedFullCopyStatus = curCacheEntry;
						ReplayRpcServer.Tracer.TraceDebug(0L, "GetCopyStatus2 updated cache");
					}
				});
			}
			else
			{
				result = new RpcErrorExceptionInfo();
				ReplayRpcServer.Tracer.TraceDebug(0L, "GetCopyStatus2 used cache");
			}
			if (curCacheEntry != null)
			{
				serializedCopyStatus = curCacheEntry.SerializedCopyStatus;
				nCopyStatusesReturned = curCacheEntry.NumberOfCopyStatusesReturned;
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F48 File Offset: 0x00002148
		public override RpcErrorExceptionInfo GetCopyStatusBasic(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, ref RpcDatabaseCopyStatusBasic[] dbStatuses)
		{
			RpcDatabaseCopyStatusBasic[] array = null;
			RpcDatabaseCopyStatus2[] tempStatuses2 = null;
			RpcErrorExceptionInfo result = TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				tempStatuses2 = this.GetCopyStatusInternal(collectionFlags2, dbGuids);
			});
			if (tempStatuses2 != null)
			{
				array = this.ConvertToBasicCopyStatus(tempStatuses2);
			}
			if (array != null)
			{
				dbStatuses = array;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003FD4 File Offset: 0x000021D4
		public override RpcErrorExceptionInfo GetCopyStatusWithHealthState(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, ref RpcCopyStatusContainer container)
		{
			ReplayServerPerfmon.GetCopyStatusServerCalls.Increment();
			ReplayServerPerfmon.GetCopyStatusServerCallsPerSec.Increment();
			RpcCopyStatusContainer tmpContainer = null;
			RpcErrorExceptionInfo result = TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				tmpContainer = this.GetCopyStatusWithComponentStateInternal(collectionFlags2, dbGuids);
			});
			if (tmpContainer != null)
			{
				container = tmpContainer;
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004058 File Offset: 0x00002258
		public override RpcErrorExceptionInfo GetDagNetworkConfig(ref byte[] configAsBytes)
		{
			DagNetworkConfiguration config = null;
			RpcErrorExceptionInfo result = TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				config = NetworkManager.GetDagNetworkConfig();
			});
			if (config != null)
			{
				configAsBytes = Serialization.ObjectToBytes(config);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000040B4 File Offset: 0x000022B4
		public override RpcErrorExceptionInfo SetDagNetwork(byte[] requestAsBytes)
		{
			SetDagNetworkRequest req = (SetDagNetworkRequest)Serialization.BytesToObject(requestAsBytes);
			return TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				NetworkManager.SetDagNetwork(req);
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004108 File Offset: 0x00002308
		public override RpcErrorExceptionInfo SetDagNetworkConfig(byte[] requestAsBytes)
		{
			SetDagNetworkConfigRequest req = (SetDagNetworkConfigRequest)Serialization.BytesToObject(requestAsBytes);
			return TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				NetworkManager.SetDagNetworkConfig(req);
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000415C File Offset: 0x0000235C
		public override RpcErrorExceptionInfo RemoveDagNetwork(byte[] requestAsBytes)
		{
			RemoveDagNetworkRequest req = (RemoveDagNetworkRequest)Serialization.BytesToObject(requestAsBytes);
			return TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				NetworkManager.RemoveDagNetwork(req);
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000041A8 File Offset: 0x000023A8
		public override RpcErrorExceptionInfo RunConfigurationUpdater()
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RunConfigurationUpdater() RPC entered.");
			return TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				Dependencies.ConfigurationUpdater.RunConfigurationUpdater(true, ReplayConfigChangeHints.RunConfigUpdaterRpc);
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000422C File Offset: 0x0000242C
		public override RpcErrorExceptionInfo RpcsNotifyChangedReplayConfiguration(Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, int changeHint)
		{
			ReplayConfigChangeHints changeHintEnum = (ReplayConfigChangeHints)changeHint;
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsNotifyChangedReplayConfiguration() RPC entered: dbGuid='{0}', waitForCompletion='{1}', exitAfterEnqueueing='{2}', isHighPriority='{3}', changeHint='{4}'", new object[]
			{
				dbGuid,
				waitForCompletion,
				exitAfterEnqueueing,
				isHighPriority,
				changeHintEnum
			});
			return TasksRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				Dependencies.ConfigurationUpdater.NotifyChangedReplayConfiguration(dbGuid, waitForCompletion, exitAfterEnqueueing, isHighPriority, true, changeHintEnum, -1);
			});
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000433C File Offset: 0x0000253C
		public override RpcErrorExceptionInfo RpcsPrepareDatabaseSeedAndBegin(RpcSeederArgs seederArgs, ref RpcSeederStatus seederStatus)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<RpcSeederArgs>((long)this.GetHashCode(), "PrepareDatabaseSeedAndBegin() RPC called with the following args: {0}", seederArgs);
			Action operation = delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("PrepareSeed");
				if (seederArgs.InstanceGuid.Equals(Guid.Empty))
				{
					ReplayRpcServer.m_seedmanager.BeginServerLevelSeed(seederArgs);
					return;
				}
				ReplayRpcServer.m_seedmanager.PrepareDbSeedAndBegin(seederArgs);
			};
			return this.DoRpcAction(operation, SeederRpcExceptionWrapper.Instance, ReplayCrimsonEvents.RpcBeginSeedRequested, ReplayCrimsonEvents.RpcBeginSeedSucceeded, ReplayCrimsonEvents.RpcBeginSeedFailed, seederArgs.InstanceGuid, new object[]
			{
				seederArgs
			});
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000043E0 File Offset: 0x000025E0
		public override RpcErrorExceptionInfo CancelDbSeed(Guid dbGuid)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "CancelDbSeed() RPC called with the following args: {0}", dbGuid);
			Action operation = delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("CancelDbSeed");
				ReplayRpcServer.m_seedmanager.CancelDbSeed(dbGuid, true);
			};
			return this.DoRpcAction(operation, SeederRpcExceptionWrapper.Instance, ReplayCrimsonEvents.RpcCancelSeedRequested, ReplayCrimsonEvents.RpcCancelSeedSucceeded, ReplayCrimsonEvents.RpcCancelSeedFailed, dbGuid, new object[0]);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004470 File Offset: 0x00002670
		public override RpcErrorExceptionInfo EndDbSeed(Guid dbGuid)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "EndDbSeed() RPC called with the following args: {0}", dbGuid);
			return SeederRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("EndDbSeed");
				ReplayRpcServer.m_seedmanager.EndDbSeed(dbGuid);
			});
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000044E8 File Offset: 0x000026E8
		public override RpcErrorExceptionInfo RpcsGetDatabaseSeedStatus(Guid databaseGuid, ref RpcSeederStatus seederStatus)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "RpcsGetDatabaseSeedStatus() RPC called with the following args: {0}", databaseGuid);
			RpcSeederStatus tempStatus = null;
			RpcErrorExceptionInfo result = SeederRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				ThirdPartyManager.PreventOperationWhenTPREnabled("GetSeedStatus");
				tempStatus = ReplayRpcServer.m_seedmanager.GetDbSeedStatus(databaseGuid);
			});
			seederStatus = tempStatus;
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000455C File Offset: 0x0000275C
		public override RpcErrorExceptionInfo RpcsInstallFailoverClustering(ref string verboseLog)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsInstallFailoverClustering() RPC called.");
			verboseLog = string.Empty;
			string lambdaLog = string.Empty;
			RpcErrorExceptionInfo result = DagRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				DagHelper.InstallFailoverClustering(out lambdaLog);
			});
			verboseLog = lambdaLog;
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004610 File Offset: 0x00002810
		public override RpcErrorExceptionInfo RpcsCreateCluster(string clusterName, string firstNodeName, string[] ipAddresses, uint[] netMasks, ref string verboseLog)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsCreateCluster() RPC called.");
			verboseLog = string.Empty;
			string lambdaLog = string.Empty;
			AmServerName firstNodeAmName = new AmServerName(firstNodeName);
			RpcErrorExceptionInfo result = DagRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				using (DagHelper.CreateDagCluster(clusterName, firstNodeAmName, ipAddresses, netMasks, out lambdaLog))
				{
				}
			});
			verboseLog = lambdaLog;
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000046AC File Offset: 0x000028AC
		public override RpcErrorExceptionInfo RpcsDestroyCluster(string clusterName, ref string verboseLog)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsDestroyCluster() RPC called.");
			verboseLog = string.Empty;
			string lambdaLog = string.Empty;
			RpcErrorExceptionInfo result = DagRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				DagHelper.DestroyDagCluster(clusterName, out lambdaLog);
			});
			verboseLog = lambdaLog;
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000472C File Offset: 0x0000292C
		public override RpcErrorExceptionInfo RpcsAddNodeToCluster(string newNode, ref string verboseLog)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsAddNodeToCluster() RPC called.");
			verboseLog = string.Empty;
			string lambdaLog = string.Empty;
			AmServerName newNodeName = new AmServerName(newNode);
			RpcErrorExceptionInfo result = DagRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				DagHelper.AddDagClusterNode(newNodeName, out lambdaLog);
			});
			verboseLog = lambdaLog;
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000047B0 File Offset: 0x000029B0
		public override RpcErrorExceptionInfo RpcsEvictNodeFromCluster(string convictedNode, ref string verboseLog)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsEvictNodeFromCluster() RPC called.");
			verboseLog = string.Empty;
			string lambdaLog = string.Empty;
			AmServerName convictedNodeName = new AmServerName(convictedNode);
			RpcErrorExceptionInfo result = DagRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				DagHelper.EvictDagClusterNode(convictedNodeName, out lambdaLog);
			});
			verboseLog = lambdaLog;
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000482C File Offset: 0x00002A2C
		public override RpcErrorExceptionInfo RpcsForceCleanupNode(ref string verboseLog)
		{
			ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug((long)this.GetHashCode(), "RpcsForceCleanupNode() RPC called.");
			verboseLog = string.Empty;
			string lambdaLog = string.Empty;
			RpcErrorExceptionInfo result = DagRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				DagHelper.ForceCleanupNode(out lambdaLog);
			});
			verboseLog = lambdaLog;
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004888 File Offset: 0x00002A88
		private RpcGetDatabaseCopyStatusFlags2 ConvertLegacyCopyStatusFlags(RpcGetDatabaseCopyStatusFlags oldFlags)
		{
			RpcGetDatabaseCopyStatusFlags2 rpcGetDatabaseCopyStatusFlags = RpcGetDatabaseCopyStatusFlags2.None;
			if ((oldFlags & RpcGetDatabaseCopyStatusFlags.UseServerSideCaching) == RpcGetDatabaseCopyStatusFlags.None)
			{
				rpcGetDatabaseCopyStatusFlags |= RpcGetDatabaseCopyStatusFlags2.ReadThrough;
			}
			return rpcGetDatabaseCopyStatusFlags;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000049D0 File Offset: 0x00002BD0
		private RpcDatabaseCopyStatus[] ConvertToLegacyCopyStatusArray(RpcDatabaseCopyStatus2[] statuses)
		{
			return (from status in statuses
			let oldStatus = new RpcDatabaseCopyStatus(status)
			select oldStatus).ToArray<RpcDatabaseCopyStatus>();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004A2C File Offset: 0x00002C2C
		private RpcDatabaseCopyStatusBasic[] ConvertToBasicCopyStatus(RpcDatabaseCopyStatus2[] statuses)
		{
			return statuses.Cast<RpcDatabaseCopyStatusBasic>().ToArray<RpcDatabaseCopyStatusBasic>();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A48 File Offset: 0x00002C48
		private RpcCopyStatusContainer GetCopyStatusWithComponentStateInternal(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids)
		{
			return new RpcCopyStatusContainer
			{
				CopyStatuses = this.GetCopyStatusInternal(collectionFlags2, dbGuids),
				HealthStates = ReplayRpcServer.m_healthStateTracker.GetComponentStates()
			};
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A7C File Offset: 0x00002C7C
		private RpcDatabaseCopyStatus2[] GetCopyStatusInternal(RpcGetDatabaseCopyStatusFlags2 collectionFlags, Guid[] dbGuids)
		{
			bool flag = false;
			List<RpcDatabaseCopyStatus2> list = null;
			if (dbGuids == null || dbGuids.Length <= 0)
			{
				throw new ReplayServiceRpcArgumentException("dbGuids");
			}
			ReplayRpcServer.m_replicaInstanceManager.TryWaitForFirstFullConfigUpdater();
			if (dbGuids.Length == 1 && dbGuids[0] == Guid.Empty)
			{
				List<ReplicaInstanceContainer> allReplicaInstanceContainers = ReplayRpcServer.m_replicaInstanceManager.GetAllReplicaInstanceContainers();
				if (allReplicaInstanceContainers == null || allReplicaInstanceContainers.Count <= 0)
				{
					goto IL_108;
				}
				list = new List<RpcDatabaseCopyStatus2>(allReplicaInstanceContainers.Count);
				using (List<ReplicaInstanceContainer>.Enumerator enumerator = allReplicaInstanceContainers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReplicaInstanceContainer replicaInstanceContainer = enumerator.Current;
						list.Add(replicaInstanceContainer.GetCopyStatus(collectionFlags));
						flag = true;
					}
					goto IL_108;
				}
			}
			list = new List<RpcDatabaseCopyStatus2>(dbGuids.Length);
			foreach (Guid guid in dbGuids)
			{
				ReplicaInstanceContainer replicaInstanceContainer2;
				if (ReplayRpcServer.m_replicaInstanceManager.TryGetReplicaInstanceContainer(guid, out replicaInstanceContainer2))
				{
					list.Add(replicaInstanceContainer2.GetCopyStatus(collectionFlags));
					flag = true;
				}
				else
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<Guid>((long)this.GetHashCode(), "GetCopyStatus: Did NOT find ReplicaInstance for {0}.", guid);
				}
			}
			IL_108:
			if (!flag || list == null || list.Count == 0)
			{
				throw new ReplayServiceRpcUnknownInstanceException();
			}
			return list.ToArray();
		}

		// Token: 0x0400001D RID: 29
		public static readonly Trace Tracer = ExTraceGlobals.ReplayServiceRpcTracer;

		// Token: 0x0400001E RID: 30
		private static ReplayRpcServer m_server;

		// Token: 0x0400001F RID: 31
		private static SeedManager m_seedmanager;

		// Token: 0x04000020 RID: 32
		private static IReplicaInstanceManager m_replicaInstanceManager;

		// Token: 0x04000021 RID: 33
		private static HealthStateTracker m_healthStateTracker;

		// Token: 0x04000022 RID: 34
		private static bool m_fRpcServerStarted;

		// Token: 0x04000023 RID: 35
		private static object m_locker = new object();

		// Token: 0x04000024 RID: 36
		private ReplayRpcServer.ServerCopyStatusCacheEntry m_cachedFullCopyStatus;

		// Token: 0x02000008 RID: 8
		private class ServerCopyStatusCacheEntry
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000088 RID: 136 RVA: 0x00004BDE File Offset: 0x00002DDE
			// (set) Token: 0x06000089 RID: 137 RVA: 0x00004BE6 File Offset: 0x00002DE6
			public int NumberOfCopyStatusesReturned { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600008A RID: 138 RVA: 0x00004BEF File Offset: 0x00002DEF
			// (set) Token: 0x0600008B RID: 139 RVA: 0x00004BF7 File Offset: 0x00002DF7
			public byte[] SerializedCopyStatus { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600008C RID: 140 RVA: 0x00004C00 File Offset: 0x00002E00
			// (set) Token: 0x0600008D RID: 141 RVA: 0x00004C08 File Offset: 0x00002E08
			public DateTime CreateTimeUtc { get; set; }

			// Token: 0x0600008E RID: 142 RVA: 0x00004C11 File Offset: 0x00002E11
			public ServerCopyStatusCacheEntry()
			{
				this.CreateTimeUtc = DateTime.UtcNow;
			}
		}
	}
}
