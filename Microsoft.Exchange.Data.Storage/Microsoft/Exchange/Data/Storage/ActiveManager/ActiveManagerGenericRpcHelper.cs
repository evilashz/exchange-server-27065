using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Rpc.Common;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000303 RID: 771
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ActiveManagerGenericRpcHelper
	{
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0008D96C File Offset: 0x0008BB6C
		public static int LocalServerVersion
		{
			get
			{
				if (ActiveManagerGenericRpcHelper.localServerVersion == 0)
				{
					Version version = Assembly.GetExecutingAssembly().GetName().Version;
					ServerVersion serverVersion = new ServerVersion(version.Major, version.Minor, version.Build, version.Revision);
					ActiveManagerGenericRpcHelper.localServerVersion = serverVersion.ToInt();
				}
				return ActiveManagerGenericRpcHelper.localServerVersion;
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0008D9C0 File Offset: 0x0008BBC0
		public static RpcGenericReplyInfo PrepareServerReply(RpcGenericRequestInfo request, object attachedReply, int majorVersion, int minorVersion)
		{
			byte[] attachedData = SerializationServices.Serialize(attachedReply);
			return new RpcGenericReplyInfo(ActiveManagerGenericRpcHelper.LocalServerVersion, request.CommandId, majorVersion, minorVersion, attachedData);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0008D9EC File Offset: 0x0008BBEC
		public static RpcGenericRequestInfo PrepareClientRequest(object attachedRequest, int commandId, int majorVersion, int minorVersion)
		{
			byte[] attachedData = SerializationServices.Serialize(attachedRequest);
			return new RpcGenericRequestInfo(ActiveManagerGenericRpcHelper.LocalServerVersion, commandId, majorVersion, minorVersion, attachedData);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0008DA44 File Offset: 0x0008BC44
		public static T RunRpcAndGetReply<T>(RpcGenericRequestInfo requestInfo, string serverName, int timeoutInMSec) where T : class
		{
			RpcGenericReplyInfo replyInfo = null;
			AmRpcClientHelper.RunRpcOperation(AmRpcOperationHint.GenericRpc, serverName, new int?(timeoutInMSec), delegate(AmRpcClient rpcClient, string rpcServerName)
			{
				ExTraceGlobals.ActiveMonitoringRpcTracer.TraceDebug<string>(0L, "GenericRequest(): Now making GenericRequest RPC to server {0}.", serverName);
				return rpcClient.GenericRequest(requestInfo, out replyInfo);
			});
			return SerializationServices.Deserialize<T>(replyInfo.AttachedData);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0008DAA0 File Offset: 0x0008BCA0
		public static T ValidateAndGetAttachedRequest<T>(RpcGenericRequestInfo requestInfo, int majorVersion, int minorVersion) where T : class
		{
			if (requestInfo.CommandMajorVersion > majorVersion)
			{
				throw new ActiveManagerGenericRpcVersionNotSupportedException(requestInfo.ServerVersion, requestInfo.CommandId, requestInfo.CommandMajorVersion, requestInfo.CommandMinorVersion, ActiveManagerGenericRpcHelper.LocalServerVersion, majorVersion, minorVersion);
			}
			return SerializationServices.Deserialize<T>(requestInfo.AttachedData);
		}

		// Token: 0x04001463 RID: 5219
		private static int localServerVersion;
	}
}
