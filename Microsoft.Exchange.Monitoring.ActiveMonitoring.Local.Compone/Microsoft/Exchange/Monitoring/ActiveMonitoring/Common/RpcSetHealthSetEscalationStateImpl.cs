using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000587 RID: 1415
	internal static class RpcSetHealthSetEscalationStateImpl
	{
		// Token: 0x06002375 RID: 9077 RVA: 0x000D42E8 File Offset: 0x000D24E8
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcSetHealthSetEscalationStateImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcSetHealthSetEscalationStateImpl.Request>(requestInfo, 1, 0);
			bool succeeded = MonitorResultCacheManager.Instance.SetHealthSetEscalationState(request.HealthSetName, request.EscalationState, request.LockOwnerId);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcSetHealthSetEscalationStateImpl.Reply
			{
				Succeeded = succeeded
			}, 1, 0);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000D4334 File Offset: 0x000D2534
		public static bool SendRequest(string serverName, string healthSetName, EscalationState escalationState, string lockOwnerId, int timeoutInMSec = 30000)
		{
			WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.GenericRpcTracer, RpcSetHealthSetEscalationStateImpl.traceContext, "RpcSetHealthSetEscalationStateImpl.SetHealthSetEscalationState: healthSetName:{0} escalationState:{1} lockOwnerId:{2}", healthSetName, escalationState.ToString(), lockOwnerId, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcSetHealthSetEscalationStateImpl.cs", 91);
			RpcSetHealthSetEscalationStateImpl.Request attachedRequest = new RpcSetHealthSetEscalationStateImpl.Request(healthSetName, escalationState, lockOwnerId);
			RpcGenericRequestInfo rpcGenericRequestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.SetHealthSetEscalationState, 1, 0);
			WTFDiagnostics.TraceDebug<int>(ExTraceGlobals.ResultCacheTracer, RpcSetHealthSetEscalationStateImpl.traceContext, "Invoking SetHealthSetEscalationState RPC with {0} bytes of data", rpcGenericRequestInfo.AttachedData.Length, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcSetHealthSetEscalationStateImpl.cs", 109);
			RpcSetHealthSetEscalationStateImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcSetHealthSetEscalationStateImpl.Reply>(rpcGenericRequestInfo, serverName, timeoutInMSec);
			WTFDiagnostics.TraceDebug<bool, string>(ExTraceGlobals.GenericRpcTracer, RpcSetHealthSetEscalationStateImpl.traceContext, "RpcSetHealthSetEscalationStateImpl.SetHealthSetEscalationState returned '{0}'. (serverName:{1})", reply.Succeeded, serverName, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcSetHealthSetEscalationStateImpl.cs", 121);
			return reply.Succeeded;
		}

		// Token: 0x0400195C RID: 6492
		public const int CommandMajorVersion = 1;

		// Token: 0x0400195D RID: 6493
		public const int CommandMinorVersion = 0;

		// Token: 0x0400195E RID: 6494
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.SetHealthSetEscalationState;

		// Token: 0x0400195F RID: 6495
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000588 RID: 1416
		[Serializable]
		internal class Request
		{
			// Token: 0x06002378 RID: 9080 RVA: 0x000D43F0 File Offset: 0x000D25F0
			public Request(string healthSetName, EscalationState escalationState, string lockOwnerId)
			{
				this.HealthSetName = healthSetName;
				this.EscalationState = escalationState;
				this.LockOwnerId = lockOwnerId;
			}

			// Token: 0x1700077A RID: 1914
			// (get) Token: 0x06002379 RID: 9081 RVA: 0x000D440D File Offset: 0x000D260D
			// (set) Token: 0x0600237A RID: 9082 RVA: 0x000D4415 File Offset: 0x000D2615
			public string HealthSetName { get; set; }

			// Token: 0x1700077B RID: 1915
			// (get) Token: 0x0600237B RID: 9083 RVA: 0x000D441E File Offset: 0x000D261E
			// (set) Token: 0x0600237C RID: 9084 RVA: 0x000D4426 File Offset: 0x000D2626
			public EscalationState EscalationState { get; set; }

			// Token: 0x1700077C RID: 1916
			// (get) Token: 0x0600237D RID: 9085 RVA: 0x000D442F File Offset: 0x000D262F
			// (set) Token: 0x0600237E RID: 9086 RVA: 0x000D4437 File Offset: 0x000D2637
			public string LockOwnerId { get; set; }
		}

		// Token: 0x02000589 RID: 1417
		[Serializable]
		internal class Reply
		{
			// Token: 0x1700077D RID: 1917
			// (get) Token: 0x0600237F RID: 9087 RVA: 0x000D4440 File Offset: 0x000D2640
			// (set) Token: 0x06002380 RID: 9088 RVA: 0x000D4448 File Offset: 0x000D2648
			public bool Succeeded { get; set; }
		}
	}
}
