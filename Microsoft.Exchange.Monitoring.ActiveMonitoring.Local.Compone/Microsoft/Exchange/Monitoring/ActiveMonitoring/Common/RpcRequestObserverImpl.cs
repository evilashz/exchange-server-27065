using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000577 RID: 1399
	internal static class RpcRequestObserverImpl
	{
		// Token: 0x060022F4 RID: 8948 RVA: 0x000D2E34 File Offset: 0x000D1034
		public static void HandleRequestObserver(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcRequestObserverImpl.RequestObserverInput requestObserverInput = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcRequestObserverImpl.RequestObserverInput>(requestInfo, 1, 0);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcRequestObserverImpl.RequestObserverReply
			{
				IsAccepted = MonitoringServerManager.TryAddSubject(requestObserverInput.ServerName)
			}, 1, 0);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000D2E6C File Offset: 0x000D106C
		public static void SendRequestObserver(string serverName, out bool isAccepted)
		{
			isAccepted = false;
			RpcRequestObserverImpl.RequestObserverInput requestObserverInput = new RpcRequestObserverImpl.RequestObserverInput();
			requestObserverInput.ServerName = NativeHelpers.GetLocalComputerFqdn(true);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcRequestObserverImpl.traceContext, "RpcRequestObserverImpl.RequestObserver() called. (serverName:{0})", serverName, null, "SendRequestObserver", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcRequestObserverImpl.cs", 90);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(requestObserverInput, ActiveMonitoringGenericRpcCommandId.RequestObserver, 1, 0);
			RpcRequestObserverImpl.RequestObserverReply requestObserverReply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcRequestObserverImpl.RequestObserverReply>(requestInfo, serverName, 5000);
			isAccepted = requestObserverReply.IsAccepted;
			WTFDiagnostics.TraceDebug<string, bool>(ExTraceGlobals.GenericRpcTracer, RpcRequestObserverImpl.traceContext, "RpcRequestObserverImpl.RequestObserver() returned. (serverName:{0}, isAccepted:{1})", serverName, isAccepted, null, "SendRequestObserver", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcRequestObserverImpl.cs", 111);
		}

		// Token: 0x04001918 RID: 6424
		public const int MajorVersion = 1;

		// Token: 0x04001919 RID: 6425
		public const int MinorVersion = 0;

		// Token: 0x0400191A RID: 6426
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000578 RID: 1400
		[Serializable]
		internal class RequestObserverInput
		{
			// Token: 0x1700074A RID: 1866
			// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000D2EFF File Offset: 0x000D10FF
			// (set) Token: 0x060022F8 RID: 8952 RVA: 0x000D2F07 File Offset: 0x000D1107
			public string ServerName { get; set; }
		}

		// Token: 0x02000579 RID: 1401
		[Serializable]
		internal class RequestObserverReply
		{
			// Token: 0x1700074B RID: 1867
			// (get) Token: 0x060022FA RID: 8954 RVA: 0x000D2F18 File Offset: 0x000D1118
			// (set) Token: 0x060022FB RID: 8955 RVA: 0x000D2F20 File Offset: 0x000D1120
			public bool IsAccepted { get; set; }
		}
	}
}
