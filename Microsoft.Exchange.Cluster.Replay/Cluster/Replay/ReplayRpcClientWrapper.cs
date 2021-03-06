using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002FD RID: 765
	internal class ReplayRpcClientWrapper : IReplayRpcClient
	{
		// Token: 0x06001F0C RID: 7948 RVA: 0x0008C4C4 File Offset: 0x0008A6C4
		internal static void RequestSuspend(string serverName, Guid guid, string suspendComment)
		{
			if (suspendComment == null)
			{
				suspendComment = string.Empty;
			}
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(guid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid>(0L, "RequestSuspend(): Now making RequestSuspend RPC to server {0} for guid {1}.", serverName, guid);
				return rpcClient.RequestSuspend(guid, suspendComment);
			});
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0008C56C File Offset: 0x0008A76C
		internal static void RequestSuspend2(string serverName, Guid guid, string suspendComment, uint flags)
		{
			if (suspendComment == null)
			{
				suspendComment = string.Empty;
			}
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(guid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid, uint>(0L, "RequestSuspend2(): Now making RequestSuspend RPC to server {0} for guid {1} (flags={2}).", serverName, guid, flags);
				return rpcClient.RequestSuspend2(guid, suspendComment, flags);
			});
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0008C658 File Offset: 0x0008A858
		public void RequestSuspend3(string serverName, Guid guid, string suspendComment, uint flags, uint initiator)
		{
			if (suspendComment == null)
			{
				suspendComment = string.Empty;
			}
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(guid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "RequestSuspend3(): Now making RequestSuspend RPC to server {0} for guid {1} (flags={2}, initiator={3}).", new object[]
				{
					serverName,
					guid,
					flags,
					initiator
				});
				return rpcClient.RequestSuspend3(guid, suspendComment, flags, initiator);
			});
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0008C700 File Offset: 0x0008A900
		internal static void RequestResume(string serverName, Guid guid)
		{
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(guid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid>(0L, "RequestResume(): Now making RequestResume RPC to server {0} for guid {1}.", serverName, guid);
				return rpcClient.RequestResume(guid);
			});
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0008C788 File Offset: 0x0008A988
		public void RequestResume2(string serverName, Guid guid, uint flags)
		{
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(guid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid, uint>(0L, "RequestResume2(): Now making RequestResume RPC to server {0} for guid {1} (flags={2}).", serverName, guid, flags);
				return rpcClient.RequestResume2(guid, flags);
			});
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0008C8B4 File Offset: 0x0008AAB4
		public void RpccDisableReplayLag(string serverName, Guid dbGuid, string disableReason, ActionInitiatorType actionInitiator)
		{
			ServerVersion serverVersion = ReplayRpcClientWrapper.GetServerVersion(serverName);
			if (!ReplayRpcVersionControl.IsDisableReplayLagRpcSupported(serverVersion))
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string, ServerVersion, ServerVersion>(0L, "RpccDisableReplayLag(): RPC to server '{0}' not supported. Server version: {1:x}. Supported version: {2:x}.", serverName, serverVersion, ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion);
				throw new ReplayLagRpcUnsupportedException(serverName, serverVersion.ToString(), ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion.ToString());
			}
			if (disableReason == null)
			{
				disableReason = string.Empty;
			}
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(dbGuid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				if (ReplayRpcVersionControl.IsDisableReplayLagRpcV2Supported(serverVersion))
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "RpccDisableReplayLag(): Now making RpccDisableReplayLag2 RPC to server {0} for guid {1} (disableReason = {2}, actionInitiator = {3}).", new object[]
					{
						serverName,
						dbGuid,
						disableReason,
						actionInitiator
					});
					return rpcClient.RpccDisableReplayLag2(dbGuid, disableReason, (uint)actionInitiator);
				}
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "RpccDisableReplayLag(): Now making RpccDisableReplayLag RPC to server {0} for guid {1} (disableReason = {2}, actionInitiator = {3}).", new object[]
				{
					serverName,
					dbGuid,
					disableReason,
					actionInitiator
				});
				return rpcClient.RpccDisableReplayLag(dbGuid, disableReason);
			});
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0008CA08 File Offset: 0x0008AC08
		public void RpccEnableReplayLag(string serverName, Guid dbGuid, ActionInitiatorType actionInitiator)
		{
			ServerVersion serverVersion = ReplayRpcClientWrapper.GetServerVersion(serverName);
			if (!ReplayRpcVersionControl.IsDisableReplayLagRpcSupported(serverVersion))
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string, ServerVersion, ServerVersion>(0L, "RpccEnableReplayLag(): RPC to server '{0}' not supported. Server version: {1:x}. Supported version: {2:x}.", serverName, serverVersion, ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion);
				throw new ReplayLagRpcUnsupportedException(serverName, serverVersion.ToString(), ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion.ToString());
			}
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(dbGuid), TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				if (ReplayRpcVersionControl.IsDisableReplayLagRpcV2Supported(serverVersion))
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid, ActionInitiatorType>(0L, "RpccEnableReplayLag(): Now making RpccEnableReplayLag2 RPC to server {0} for guid {1} (actionInitiator = {2}).", serverName, dbGuid, actionInitiator);
					return rpcClient.RpccEnableReplayLag2(dbGuid, (uint)actionInitiator);
				}
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid, ActionInitiatorType>(0L, "RpccEnableReplayLag(): Now making RpccEnableReplayLag RPC to server {0} for guid {1} (actionInitiator = {2}).", serverName, dbGuid, actionInitiator);
				return rpcClient.RpccEnableReplayLag(dbGuid);
			});
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0008CAB8 File Offset: 0x0008ACB8
		public RpcDatabaseCopyStatusBasic[] GetCopyStatusBasic(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids)
		{
			return this.GetCopyStatusBasic(serverName, collectionFlags2, dbGuids, RegistryParameters.GetMailboxDatabaseCopyStatusRPCTimeoutInMSec);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0008CBA8 File Offset: 0x0008ADA8
		public RpcDatabaseCopyStatusBasic[] GetCopyStatusBasic(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, int timeoutMs)
		{
			if (dbGuids == null || dbGuids.Length == 0)
			{
				dbGuids = new Guid[]
				{
					Guid.Empty
				};
			}
			RpcDatabaseCopyStatusBasic[] statusResults = null;
			ReplayRpcClientWrapper.RunRpcOperation(serverName, null, timeoutMs, TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				RpcErrorExceptionInfo result;
				try
				{
					if (ReplayRpcVersionControl.IsGetCopyStatusBasicRpcSupported(ReplayRpcClientWrapper.GetServerVersion(serverName)))
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string>(0L, "GetCopyStatusBasic(): Now making RpccGetCopyStatusBasic() RPC to server {0}.", serverName);
						result = rpcClient.RpccGetCopyStatusBasic(collectionFlags2, dbGuids, ref statusResults);
					}
					else
					{
						RpcDatabaseCopyStatus[] array = null;
						ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string>(0L, "GetCopyStatusBasic(): Now making GetCopyStatusEx2() RPC to server {0}.", serverName);
						RpcErrorExceptionInfo copyStatusEx = rpcClient.GetCopyStatusEx2(ReplayRpcClientWrapper.ConvertToLegacyCopyStatusFlags(collectionFlags2), dbGuids, ref array);
						if (!copyStatusEx.IsFailed() && array != null)
						{
							statusResults = ReplayRpcClientWrapper.ConvertLegacyCopyStatusToBasicArray(array);
						}
						result = copyStatusEx;
					}
				}
				catch (RpcException ex)
				{
					if (ReplayRpcErrorCode.IsRpcTimeoutError(ex.ErrorCode))
					{
						throw new ReplayServiceDownException(serverName, ex.Message, ex);
					}
					throw;
				}
				return result;
			});
			return statusResults;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0008CC35 File Offset: 0x0008AE35
		public RpcDatabaseCopyStatus2[] GetCopyStatus(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids)
		{
			return this.GetCopyStatus(serverName, collectionFlags2, dbGuids, RegistryParameters.GetMailboxDatabaseCopyStatusRPCTimeoutInMSec);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0008CD40 File Offset: 0x0008AF40
		public RpcDatabaseCopyStatus2[] GetCopyStatus(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, int timeoutMs)
		{
			if (dbGuids == null || dbGuids.Length == 0)
			{
				dbGuids = new Guid[]
				{
					Guid.Empty
				};
			}
			RpcDatabaseCopyStatus2[] statusResults = null;
			ReplayRpcClientWrapper.RunRpcOperation(serverName, null, timeoutMs, TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				RpcErrorExceptionInfo result;
				try
				{
					if (ReplayRpcVersionControl.IsGetCopyStatusEx4RpcSupported(ReplayRpcClientWrapper.GetServerVersion(serverName)))
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string>(0L, "GetCopyStatus(): Now making RpccGetCopyStatusEx4() RPC to server {0}.", serverName);
						RpcErrorExceptionInfo rpcErrorExceptionInfo = rpcClient.RpccGetCopyStatusEx4(collectionFlags2, dbGuids, ref statusResults);
						if (!rpcErrorExceptionInfo.IsFailed())
						{
							ReplayRpcClientWrapper.DeserializeExtendedErrorInfo(statusResults);
						}
						result = rpcErrorExceptionInfo;
					}
					else
					{
						RpcDatabaseCopyStatus[] array = null;
						ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string>(0L, "GetCopyStatus(): Now making GetCopyStatusEx2() RPC to server {0}.", serverName);
						RpcErrorExceptionInfo copyStatusEx = rpcClient.GetCopyStatusEx2(ReplayRpcClientWrapper.ConvertToLegacyCopyStatusFlags(collectionFlags2), dbGuids, ref array);
						if (!copyStatusEx.IsFailed() && array != null)
						{
							statusResults = ReplayRpcClientWrapper.ConvertLegacyCopyStatusArray(array);
						}
						result = copyStatusEx;
					}
				}
				catch (RpcException ex)
				{
					if (ReplayRpcErrorCode.IsRpcTimeoutError(ex.ErrorCode))
					{
						throw new ReplayServiceDownException(serverName, ex.Message, ex);
					}
					throw;
				}
				return result;
			});
			return statusResults;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0008CDD0 File Offset: 0x0008AFD0
		internal static void DeserializeExtendedErrorInfo(RpcDatabaseCopyStatus2[] statusResults)
		{
			if (statusResults != null)
			{
				foreach (RpcDatabaseCopyStatus2 rpcDatabaseCopyStatus in statusResults)
				{
					if (rpcDatabaseCopyStatus.ExtendedErrorInfoBytes != null && rpcDatabaseCopyStatus.ExtendedErrorInfoBytes.Length > 0)
					{
						try
						{
							rpcDatabaseCopyStatus.ExtendedErrorInfo = ExtendedErrorInfo.Deserialize(rpcDatabaseCopyStatus.ExtendedErrorInfoBytes);
						}
						catch (SerializationException arg)
						{
							ExTraceGlobals.ReplayServiceRpcTracer.TraceError<Guid, SerializationException>(0L, "GetCopyStatus(): Failed to deserialize ExtendedErrorInfo for database '{0}'. Exception: {1}", rpcDatabaseCopyStatus.DBGuid, arg);
						}
						catch (TargetInvocationException arg2)
						{
							ExTraceGlobals.ReplayServiceRpcTracer.TraceError<Guid, TargetInvocationException>(0L, "GetCopyStatus(): Failed to deserialize ExtendedErrorInfo for database '{0}'. Exception: {1}", rpcDatabaseCopyStatus.DBGuid, arg2);
						}
					}
				}
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0008CF0C File Offset: 0x0008B10C
		public RpcCopyStatusContainer GetCopyStatusWithHealthState(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, int timeoutMs)
		{
			if (dbGuids == null || dbGuids.Length == 0)
			{
				dbGuids = new Guid[]
				{
					Guid.Empty
				};
			}
			RpcCopyStatusContainer container = null;
			if (ReplayRpcVersionControl.IsGetCopyStatusWithHealthStateRpcSupported(ReplayRpcClientWrapper.GetServerVersion(serverName)))
			{
				ReplayRpcClientWrapper.RunRpcOperation(serverName, null, timeoutMs, TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					RpcErrorExceptionInfo result;
					try
					{
						ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string>(0L, "GetCopyStatusWithHealthState(): Now making RpccGetCopyStatusWithHealthState() RPC to server {0}.", serverName);
						RpcErrorExceptionInfo rpcErrorExceptionInfo = rpcClient.RpccGetCopyStatusWithHealthState(collectionFlags2, dbGuids, ref container);
						if (!rpcErrorExceptionInfo.IsFailed())
						{
							ReplayRpcClientWrapper.DeserializeExtendedErrorInfo(container.CopyStatuses);
						}
						result = rpcErrorExceptionInfo;
					}
					catch (RpcException ex)
					{
						if (ReplayRpcErrorCode.IsRpcTimeoutError(ex.ErrorCode))
						{
							throw new ReplayServiceDownException(serverName, ex.Message, ex);
						}
						throw;
					}
					return result;
				});
			}
			else
			{
				container = new RpcCopyStatusContainer();
				container.HealthStates = null;
				container.CopyStatuses = this.GetCopyStatus(serverName, collectionFlags2, dbGuids, timeoutMs);
			}
			return container;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0008CFF1 File Offset: 0x0008B1F1
		internal static void RunConfigurationUpdater(string serverName)
		{
			ReplayRpcClientWrapper.RunConfigurationUpdater(serverName, 10000);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0008CFFE File Offset: 0x0008B1FE
		public void NotifyChangedReplayConfiguration(string serverName, Guid dbGuid, ServerVersion serverVersion, bool waitForCompletion, bool isHighPriority, ReplayConfigChangeHints changeHint)
		{
			if (ReplayRpcVersionControl.IsNotifyChangedReplayConfigurationRpcSupported(serverVersion))
			{
				ReplayRpcClientWrapper.NotifyChangedReplayConfiguration(serverName, dbGuid, waitForCompletion, !waitForCompletion, isHighPriority, changeHint, 10000);
				return;
			}
			ReplayRpcClientWrapper.RunConfigurationUpdater(serverName, 10000);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0008D02B File Offset: 0x0008B22B
		public void NotifyChangedReplayConfigurationAsync(string serverName, Guid dbGuid, ReplayConfigChangeHints changeHint)
		{
			ReplayRpcClientWrapper.NotifyChangedReplayConfiguration(serverName, dbGuid, false, true, false, changeHint, 10000);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0008D06C File Offset: 0x0008B26C
		public static void RunInstallFailoverClustering(AmServerName serverName, out string verboseLog)
		{
			string lambdaLog = null;
			try
			{
				ReplayRpcClientWrapper.RunRpcOperation(serverName, DagRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<AmServerName>(0L, "RunAddNodeToCluster(): Now making RPC to server {0} to install failover-clustering.", serverName);
					return rpcClient.RpccInstallFailoverClustering(out lambdaLog);
				});
			}
			finally
			{
				verboseLog = lambdaLog;
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0008D15C File Offset: 0x0008B35C
		internal static void RunCreateCluster(AmServerName rpcServerName, string clusterName, AmServerName firstNode, string[] ipaddrs, uint[] netmasks, out string verboseLog)
		{
			string lambdaLog = null;
			try
			{
				ReplayRpcClientWrapper.RunRpcOperation(rpcServerName, null, 900000, DagRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "RunAddNodeToCluster(): Now making RPC to server {0} to CreateCluster {1}, firstNode={2}, ipaddrs={3}, netmasks={4}.", new object[]
					{
						rpcServerName,
						clusterName,
						firstNode,
						string.Join(",", ipaddrs),
						netmasks
					});
					return rpcClient.RpccCreateCluster(clusterName, firstNode.Fqdn, ipaddrs, netmasks, out lambdaLog);
				});
			}
			finally
			{
				verboseLog = lambdaLog;
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0008D224 File Offset: 0x0008B424
		internal static void RunDestroyCluster(AmServerName rpcServerName, string clusterName, out string verboseLog)
		{
			string lambdaLog = null;
			try
			{
				ReplayRpcClientWrapper.RunRpcOperation(rpcServerName, DagRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<AmServerName, string>(0L, "RunAddNodeToCluster(): Now making RPC to server {0} to destroy cluster {1}.", rpcServerName, clusterName);
					return rpcClient.RpccDestroyCluster(clusterName, out lambdaLog);
				});
			}
			finally
			{
				verboseLog = lambdaLog;
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0008D2CC File Offset: 0x0008B4CC
		internal static void RunAddNodeToCluster(AmServerName serverName, AmServerName newNode, out string verboseLog)
		{
			string lambdaLog = null;
			try
			{
				ReplayRpcClientWrapper.RunRpcOperation(serverName, null, 900000, DagRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<AmServerName, AmServerName>(0L, "RunAddNodeToCluster(): Now making RPC to server {0} to add {1}.", serverName, newNode);
					return rpcClient.RpccAddNodeToCluster(newNode.Fqdn, out lambdaLog);
				});
			}
			finally
			{
				verboseLog = lambdaLog;
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0008D380 File Offset: 0x0008B580
		internal static void RunEvictNodeFromCluster(AmServerName rpcServerName, AmServerName convictedNode, out string verboseLog)
		{
			string lambdaLog = null;
			try
			{
				ReplayRpcClientWrapper.RunRpcOperation(rpcServerName, null, 900000, DagRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<AmServerName, AmServerName>(0L, "RunAddNodeToCluster(): Now making RPC to server {0} to evict {1} from the cluster.", rpcServerName, convictedNode);
					return rpcClient.RpccEvictNodeFromCluster(convictedNode.Fqdn, out lambdaLog);
				});
			}
			finally
			{
				verboseLog = lambdaLog;
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0008D424 File Offset: 0x0008B624
		internal static void RunForceCleanupNode(AmServerName rpcServerName, out string verboseLog)
		{
			string lambdaLog = null;
			try
			{
				ReplayRpcClientWrapper.RunRpcOperation(rpcServerName, DagRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<AmServerName>(0L, "RunForceCleanupNode(): Now making RPC to server {0} to force cleanup itself from the cluster.", rpcServerName);
					return rpcClient.RpccForceCleanupNode(out lambdaLog);
				});
			}
			finally
			{
				verboseLog = lambdaLog;
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0008D4AC File Offset: 0x0008B6AC
		private static void RunConfigurationUpdater(string serverName, int timeoutMs)
		{
			ReplayRpcClientWrapper.RunRpcOperation(serverName, null, timeoutMs, TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string>(0L, "RunConfigurationUpdater(): Now making RunConfigurationUpdater RPC to server {0}.", serverName);
				return rpcClient.RunConfigurationUpdater();
			});
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0008D590 File Offset: 0x0008B790
		private static void NotifyChangedReplayConfiguration(string serverName, Guid dbGuid, bool waitForCompletion, bool exitAfterEnqueueing, bool isHighPriority, ReplayConfigChangeHints changeHint, int timeoutMs)
		{
			ReplayRpcClientWrapper.RunRpcOperation(serverName, new Guid?(dbGuid), timeoutMs, TasksRpcExceptionWrapper.Instance, delegate(ReplayRpcClient rpcClient)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug(0L, "NotifyChangedReplayConfiguration(): Now making RPC to server {0}, for dbGuid {1}. WaitForCompletion={2}, ExitAfterEnqueueing={3}, IsHighPriority={4}, ChangeHint={5}", new object[]
				{
					serverName,
					dbGuid,
					waitForCompletion,
					exitAfterEnqueueing,
					isHighPriority,
					changeHint
				});
				return rpcClient.RpccNotifyChangedReplayConfiguration(dbGuid, waitForCompletion, exitAfterEnqueueing, isHighPriority, (int)changeHint);
			});
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0008D5F8 File Offset: 0x0008B7F8
		private static ReplayRpcClient RpcClientFactory(AmServerName serverName, int timeoutMs)
		{
			ReplayRpcClient replayRpcClient = new ReplayRpcClient(serverName.Fqdn);
			if (timeoutMs != -1)
			{
				replayRpcClient.SetTimeOut(timeoutMs);
			}
			return replayRpcClient;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0008D620 File Offset: 0x0008B820
		private static void RunRpcOperation(string serverName, IHaRpcExceptionWrapper rpcExceptionWrapperInstance, ReplayRpcClientWrapper.InternalRpcOperation rpcOperation)
		{
			AmServerName serverName2 = new AmServerName(serverName);
			ReplayRpcClientWrapper.RunRpcOperation(serverName2, null, -1, rpcExceptionWrapperInstance, rpcOperation);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0008D648 File Offset: 0x0008B848
		private static void RunRpcOperation(AmServerName serverName, IHaRpcExceptionWrapper rpcExceptionWrapperInstance, ReplayRpcClientWrapper.InternalRpcOperation rpcOperation)
		{
			ReplayRpcClientWrapper.RunRpcOperation(serverName, null, -1, rpcExceptionWrapperInstance, rpcOperation);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0008D668 File Offset: 0x0008B868
		private static void RunRpcOperation(string serverName, Guid? dbGuid, IHaRpcExceptionWrapper rpcExceptionWrapperInstance, ReplayRpcClientWrapper.InternalRpcOperation rpcOperation)
		{
			AmServerName serverName2 = new AmServerName(serverName);
			ReplayRpcClientWrapper.RunRpcOperation(serverName2, dbGuid, -1, rpcExceptionWrapperInstance, rpcOperation);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0008D688 File Offset: 0x0008B888
		private static void RunRpcOperation(string serverName, Guid? dbGuid, int timeoutMs, IHaRpcExceptionWrapper rpcExceptionWrapperInstance, ReplayRpcClientWrapper.InternalRpcOperation rpcOperation)
		{
			AmServerName serverName2 = new AmServerName(serverName);
			ReplayRpcClientWrapper.RunRpcOperation(serverName2, dbGuid, timeoutMs, rpcExceptionWrapperInstance, rpcOperation);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0008D6A8 File Offset: 0x0008B8A8
		private static void RunRpcOperation(AmServerName serverName, Guid? dbGuid, int timeoutMs, IHaRpcExceptionWrapper rpcExceptionWrapperInstance, ReplayRpcClientWrapper.InternalRpcOperation rpcOperation)
		{
			string databaseName = null;
			if (dbGuid != null)
			{
				databaseName = dbGuid.Value.ToString();
			}
			ReplayRpcClientWrapper.RunRpcOperationDbName(serverName, databaseName, timeoutMs, rpcExceptionWrapperInstance, rpcOperation);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0008D73C File Offset: 0x0008B93C
		private static void RunRpcOperationDbName(AmServerName serverName, string databaseName, int timeoutMs, IHaRpcExceptionWrapper rpcExceptionWrapperInstance, ReplayRpcClientWrapper.InternalRpcOperation rpcOperation)
		{
			RpcErrorExceptionInfo errorInfo = null;
			rpcExceptionWrapperInstance.ClientRetryableOperation(serverName.Fqdn, delegate
			{
				using (ReplayRpcClient replayRpcClient = ReplayRpcClientWrapper.RpcClientFactory(serverName, timeoutMs))
				{
					errorInfo = rpcOperation(replayRpcClient);
				}
			});
			rpcExceptionWrapperInstance.ClientRethrowIfFailed(databaseName, serverName.Fqdn, errorInfo);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0008D7A4 File Offset: 0x0008B9A4
		private static RpcGetDatabaseCopyStatusFlags ConvertToLegacyCopyStatusFlags(RpcGetDatabaseCopyStatusFlags2 newFlags)
		{
			RpcGetDatabaseCopyStatusFlags rpcGetDatabaseCopyStatusFlags = RpcGetDatabaseCopyStatusFlags.None;
			rpcGetDatabaseCopyStatusFlags |= RpcGetDatabaseCopyStatusFlags.CollectConnectionStatus;
			rpcGetDatabaseCopyStatusFlags |= RpcGetDatabaseCopyStatusFlags.CollectExtendedErrorInfo;
			if ((newFlags & RpcGetDatabaseCopyStatusFlags2.ReadThrough) == RpcGetDatabaseCopyStatusFlags2.None)
			{
				rpcGetDatabaseCopyStatusFlags |= RpcGetDatabaseCopyStatusFlags.UseServerSideCaching;
			}
			return rpcGetDatabaseCopyStatusFlags;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0008D8F4 File Offset: 0x0008BAF4
		private static RpcDatabaseCopyStatus2[] ConvertLegacyCopyStatusArray(RpcDatabaseCopyStatus[] oldStatuses)
		{
			return (from status in oldStatuses
			let newStatus = new RpcDatabaseCopyStatus2(status)
			select newStatus).ToArray<RpcDatabaseCopyStatus2>();
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0008DA80 File Offset: 0x0008BC80
		private static RpcDatabaseCopyStatusBasic[] ConvertLegacyCopyStatusToBasicArray(RpcDatabaseCopyStatus[] oldStatuses)
		{
			return (from status in oldStatuses
			let basicStatus = new RpcDatabaseCopyStatus2(status)
			select basicStatus).ToArray<RpcDatabaseCopyStatusBasic>();
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0008DADC File Offset: 0x0008BCDC
		private static ServerVersion GetServerVersion(string serverName)
		{
			if (RegistryTestHook.TargetServerVersionOverride > 0)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, int>(0L, "GetServerVersion( {0} ) is returning TargetServerVersionOverride registry override of {1}.", serverName, RegistryTestHook.TargetServerVersionOverride);
				return new ServerVersion(RegistryTestHook.TargetServerVersionOverride);
			}
			Exception ex = null;
			try
			{
				AmServerName amServerName = new AmServerName(serverName);
				IADServer iadserver = Dependencies.ReplayAdObjectLookup.MiniServerLookup.FindMiniServerByShortNameEx(amServerName.NetbiosName, out ex);
				if (iadserver == null)
				{
					ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string, Exception>(0L, "GetServerVersion( {0} ) failed with exception: {1}", serverName, ex);
					throw new ReplayFailedToFindServerRpcVersionException(serverName, ex);
				}
				return iadserver.AdminDisplayVersion;
			}
			catch (AmCommonTransientException ex2)
			{
				ex = ex2;
			}
			ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string, Exception>(0L, "GetServerVersion( {0} ) failed with exception: {1}", serverName, ex);
			throw new ReplayFailedToFindServerRpcVersionException(serverName, ex);
		}

		// Token: 0x04000CED RID: 3309
		private const int ShortRpcTimeoutMs = 10000;

		// Token: 0x04000CEE RID: 3310
		private const int LongerRpcTimeoutMs = 900000;

		// Token: 0x020002FE RID: 766
		// (Invoke) Token: 0x06001F35 RID: 7989
		public delegate RpcErrorExceptionInfo InternalRpcOperation(ReplayRpcClient rpcClient);
	}
}
