using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000321 RID: 801
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ActiveMonitoringGenericRpcHelper
	{
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x0009341C File Offset: 0x0009161C
		public static int LocalServerVersion
		{
			get
			{
				if (ActiveMonitoringGenericRpcHelper.localServerVersion == 0)
				{
					lock (ActiveMonitoringGenericRpcHelper.locker)
					{
						if (ActiveMonitoringGenericRpcHelper.localServerVersion == 0)
						{
							Version version = Assembly.GetExecutingAssembly().GetName().Version;
							ServerVersion serverVersion = new ServerVersion(version.Major, version.Minor, version.Build, version.Revision);
							ActiveMonitoringGenericRpcHelper.localServerVersion = serverVersion.ToInt();
						}
					}
				}
				return ActiveMonitoringGenericRpcHelper.localServerVersion;
			}
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000934A4 File Offset: 0x000916A4
		public static ActiveMonitoringRpcClient RpcClientFactory(string serverName, int timeoutMs)
		{
			ActiveMonitoringRpcClient activeMonitoringRpcClient = new ActiveMonitoringRpcClient(serverName);
			if (timeoutMs != -1)
			{
				activeMonitoringRpcClient.SetTimeOut(timeoutMs);
			}
			return activeMonitoringRpcClient;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0009351C File Offset: 0x0009171C
		public static void RunRpcOperation(string serverName, int timeoutMs, ActiveMonitoringRpcExceptionWrapper rpcExceptionWrapperInstance, ActiveMonitoringGenericRpcHelper.InternalRpcOperation rpcOperation)
		{
			RpcErrorExceptionInfo errorInfo = null;
			rpcExceptionWrapperInstance.ClientRetryableOperation(serverName, delegate
			{
				using (ActiveMonitoringRpcClient activeMonitoringRpcClient = ActiveMonitoringGenericRpcHelper.RpcClientFactory(serverName, timeoutMs))
				{
					errorInfo = rpcOperation(activeMonitoringRpcClient);
				}
			});
			rpcExceptionWrapperInstance.ClientRethrowIfFailed(serverName, errorInfo);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00093578 File Offset: 0x00091778
		public static RpcGenericReplyInfo PrepareServerReply(RpcGenericRequestInfo request, object attachedReply, int majorVersion, int minorVersion)
		{
			byte[] attachedData = SerializationServices.Serialize(attachedReply);
			return new RpcGenericReplyInfo(ActiveMonitoringGenericRpcHelper.LocalServerVersion, request.CommandId, majorVersion, minorVersion, attachedData);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000935A4 File Offset: 0x000917A4
		public static RpcGenericRequestInfo PrepareClientRequest(object attachedRequest, ActiveMonitoringGenericRpcCommandId commandId, int majorVersion, int minorVersion)
		{
			byte[] attachedData = SerializationServices.Serialize(attachedRequest);
			return new RpcGenericRequestInfo(ActiveMonitoringGenericRpcHelper.LocalServerVersion, (int)commandId, majorVersion, minorVersion, attachedData);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000935FC File Offset: 0x000917FC
		public static T RunRpcAndGetReply<T>(RpcGenericRequestInfo requestInfo, string serverName, int timeoutInMSec) where T : class
		{
			RpcGenericReplyInfo replyInfo = null;
			ActiveMonitoringGenericRpcHelper.RunRpcOperation(serverName, timeoutInMSec, ActiveMonitoringRpcExceptionWrapper.Instance, delegate(ActiveMonitoringRpcClient rpcClient)
			{
				ExTraceGlobals.ActiveMonitoringRpcTracer.TraceDebug<string>(0L, "GenericRequest(): Now making GenericRequest RPC to server {0}.", serverName);
				return rpcClient.GenericRequest(requestInfo, out replyInfo);
			});
			return SerializationServices.Deserialize<T>(replyInfo.AttachedData);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00093654 File Offset: 0x00091854
		public static T ValidateAndGetAttachedRequest<T>(RpcGenericRequestInfo requestInfo, int majorVersion, int minorVersion) where T : class
		{
			if (requestInfo.CommandMajorVersion > majorVersion)
			{
				throw new ActiveMonitoringRpcVersionNotSupportedException(requestInfo.ServerVersion, requestInfo.CommandId, requestInfo.CommandMajorVersion, requestInfo.CommandMinorVersion, ActiveMonitoringGenericRpcHelper.LocalServerVersion, majorVersion, minorVersion);
			}
			return SerializationServices.Deserialize<T>(requestInfo.AttachedData);
		}

		// Token: 0x04001560 RID: 5472
		public const int TinyRpcTimeoutMs = 5000;

		// Token: 0x04001561 RID: 5473
		public const int ShortRpcTimeoutMs = 30000;

		// Token: 0x04001562 RID: 5474
		public const int LongRpcTimeoutMs = 300000;

		// Token: 0x04001563 RID: 5475
		public const int LongerRpcTimeoutMs = 900000;

		// Token: 0x04001564 RID: 5476
		private static int localServerVersion;

		// Token: 0x04001565 RID: 5477
		private static object locker = new object();

		// Token: 0x02000322 RID: 802
		// (Invoke) Token: 0x060023CD RID: 9165
		public delegate RpcErrorExceptionInfo InternalRpcOperation(ActiveMonitoringRpcClient rpcClient);
	}
}
