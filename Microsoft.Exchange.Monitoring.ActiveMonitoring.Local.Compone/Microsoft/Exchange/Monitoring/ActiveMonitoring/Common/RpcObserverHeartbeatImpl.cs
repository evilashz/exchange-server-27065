using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000574 RID: 1396
	internal static class RpcObserverHeartbeatImpl
	{
		// Token: 0x060022EB RID: 8939 RVA: 0x000D2D34 File Offset: 0x000D0F34
		public static void HandleObserverHeartbeat(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcObserverHeartbeatImpl.ObserverHeartbeatInput observerHeartbeatInput = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcObserverHeartbeatImpl.ObserverHeartbeatInput>(requestInfo, 1, 0);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcObserverHeartbeatImpl.ObserverHeartbeatReply
			{
				Response = MonitoringServerManager.UpdateObserverHeartbeat(observerHeartbeatInput.ServerName)
			}, 1, 0);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000D2D6C File Offset: 0x000D0F6C
		public static void SendObserverHeartbeat(string serverName, out ObserverHeartbeatResponse response)
		{
			response = ObserverHeartbeatResponse.NoResponse;
			RpcObserverHeartbeatImpl.ObserverHeartbeatInput observerHeartbeatInput = new RpcObserverHeartbeatImpl.ObserverHeartbeatInput();
			observerHeartbeatInput.ServerName = NativeHelpers.GetLocalComputerFqdn(true);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcObserverHeartbeatImpl.traceContext, "RpcObserverHeartbeatImpl.ObserverHeartbeat() called. (serverName:{0})", serverName, null, "SendObserverHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcObserverHeartbeatImpl.cs", 116);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(observerHeartbeatInput, ActiveMonitoringGenericRpcCommandId.ObserverHeartbeat, 1, 0);
			RpcObserverHeartbeatImpl.ObserverHeartbeatReply observerHeartbeatReply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcObserverHeartbeatImpl.ObserverHeartbeatReply>(requestInfo, serverName, 5000);
			response = observerHeartbeatReply.Response;
			WTFDiagnostics.TraceDebug<string, ObserverHeartbeatResponse>(ExTraceGlobals.GenericRpcTracer, RpcObserverHeartbeatImpl.traceContext, "RpcObserverHeartbeatImpl.ObserverHeartbeat() returned. (serverName:{0}, response:{1})", serverName, response, null, "SendObserverHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcObserverHeartbeatImpl.cs", 137);
		}

		// Token: 0x04001913 RID: 6419
		public const int MajorVersion = 1;

		// Token: 0x04001914 RID: 6420
		public const int MinorVersion = 0;

		// Token: 0x04001915 RID: 6421
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000575 RID: 1397
		[Serializable]
		internal class ObserverHeartbeatInput
		{
			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x060022EE RID: 8942 RVA: 0x000D2E02 File Offset: 0x000D1002
			// (set) Token: 0x060022EF RID: 8943 RVA: 0x000D2E0A File Offset: 0x000D100A
			public string ServerName { get; set; }
		}

		// Token: 0x02000576 RID: 1398
		[Serializable]
		internal class ObserverHeartbeatReply
		{
			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x060022F1 RID: 8945 RVA: 0x000D2E1B File Offset: 0x000D101B
			// (set) Token: 0x060022F2 RID: 8946 RVA: 0x000D2E23 File Offset: 0x000D1023
			public ObserverHeartbeatResponse Response { get; set; }
		}
	}
}
