using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000584 RID: 1412
	internal static class RpcLockHealthSetEscalationStateIfRequiredImpl
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x000D417C File Offset: 0x000D237C
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcLockHealthSetEscalationStateIfRequiredImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcLockHealthSetEscalationStateIfRequiredImpl.Request>(requestInfo, 1, 0);
			HealthSetEscalationState healthSetEscalationState = MonitorResultCacheManager.Instance.LockHealthSetEscalationStateIfRequired(request.HealthSetName, request.EscalationState, request.LockOwnerId);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcLockHealthSetEscalationStateIfRequiredImpl.Reply
			{
				HealthSetEscalationState = healthSetEscalationState
			}, 1, 0);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000D41C8 File Offset: 0x000D23C8
		public static HealthSetEscalationState SendRequest(string serverName, string healthSetName, EscalationState escalationState, string lockOwnerId, int timeoutInMSec = 30000)
		{
			WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.GenericRpcTracer, RpcLockHealthSetEscalationStateIfRequiredImpl.traceContext, "RpcLockHealthSetEscalationStateIfRequiredImpl.LockHealthSetEscalationStateIfRequired: healthSetName:{0} escalationState:{1} lockOwnerId:{2}", healthSetName, escalationState.ToString(), lockOwnerId, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcLockHealthSetEscalationStateIfRequiredImpl.cs", 91);
			RpcLockHealthSetEscalationStateIfRequiredImpl.Request attachedRequest = new RpcLockHealthSetEscalationStateIfRequiredImpl.Request(healthSetName, escalationState, lockOwnerId);
			RpcGenericRequestInfo rpcGenericRequestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.LockHealthSetEscalationStateIfRequired, 1, 0);
			WTFDiagnostics.TraceDebug<int>(ExTraceGlobals.ResultCacheTracer, RpcLockHealthSetEscalationStateIfRequiredImpl.traceContext, "Invoking LockHealthSetEscalationStateIfRequired RPC with {0} bytes of data", rpcGenericRequestInfo.AttachedData.Length, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcLockHealthSetEscalationStateIfRequiredImpl.cs", 109);
			RpcLockHealthSetEscalationStateIfRequiredImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcLockHealthSetEscalationStateIfRequiredImpl.Reply>(rpcGenericRequestInfo, serverName, timeoutInMSec);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcLockHealthSetEscalationStateIfRequiredImpl.traceContext, "RpcLockHealthSetEscalationStateIfRequiredImpl.LockHealthSetEscalationStateIfRequired returned. (serverName:{0})", serverName, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcLockHealthSetEscalationStateIfRequiredImpl.cs", 121);
			return reply.HealthSetEscalationState;
		}

		// Token: 0x04001954 RID: 6484
		public const int CommandMajorVersion = 1;

		// Token: 0x04001955 RID: 6485
		public const int CommandMinorVersion = 0;

		// Token: 0x04001956 RID: 6486
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.LockHealthSetEscalationStateIfRequired;

		// Token: 0x04001957 RID: 6487
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000585 RID: 1413
		[Serializable]
		internal class Request
		{
			// Token: 0x0600236B RID: 9067 RVA: 0x000D427E File Offset: 0x000D247E
			public Request(string healthSetName, EscalationState escalationState, string lockOwnerId)
			{
				this.HealthSetName = healthSetName;
				this.EscalationState = escalationState;
				this.LockOwnerId = lockOwnerId;
			}

			// Token: 0x17000776 RID: 1910
			// (get) Token: 0x0600236C RID: 9068 RVA: 0x000D429B File Offset: 0x000D249B
			// (set) Token: 0x0600236D RID: 9069 RVA: 0x000D42A3 File Offset: 0x000D24A3
			public string HealthSetName { get; set; }

			// Token: 0x17000777 RID: 1911
			// (get) Token: 0x0600236E RID: 9070 RVA: 0x000D42AC File Offset: 0x000D24AC
			// (set) Token: 0x0600236F RID: 9071 RVA: 0x000D42B4 File Offset: 0x000D24B4
			public EscalationState EscalationState { get; set; }

			// Token: 0x17000778 RID: 1912
			// (get) Token: 0x06002370 RID: 9072 RVA: 0x000D42BD File Offset: 0x000D24BD
			// (set) Token: 0x06002371 RID: 9073 RVA: 0x000D42C5 File Offset: 0x000D24C5
			public string LockOwnerId { get; set; }
		}

		// Token: 0x02000586 RID: 1414
		[Serializable]
		internal class Reply
		{
			// Token: 0x17000779 RID: 1913
			// (get) Token: 0x06002372 RID: 9074 RVA: 0x000D42CE File Offset: 0x000D24CE
			// (set) Token: 0x06002373 RID: 9075 RVA: 0x000D42D6 File Offset: 0x000D24D6
			public HealthSetEscalationState HealthSetEscalationState { get; set; }
		}
	}
}
