using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000569 RID: 1385
	internal static class RpcCancelObserverImpl
	{
		// Token: 0x060022C2 RID: 8898 RVA: 0x000D2710 File Offset: 0x000D0910
		public static void HandleCancelObserver(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcCancelObserverImpl.CancelObserverInput cancelObserverInput = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcCancelObserverImpl.CancelObserverInput>(requestInfo, 1, 0);
			RpcCancelObserverImpl.CancelObserverReply cancelObserverReply = new RpcCancelObserverImpl.CancelObserverReply();
			MonitoringServerManager.RemoveSubject(cancelObserverInput.ServerName);
			cancelObserverReply.IsAccepted = true;
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, cancelObserverReply, 1, 0);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000D274C File Offset: 0x000D094C
		public static void SendCancelObserver(string serverName)
		{
			RpcCancelObserverImpl.CancelObserverInput cancelObserverInput = new RpcCancelObserverImpl.CancelObserverInput();
			cancelObserverInput.ServerName = NativeHelpers.GetLocalComputerFqdn(true);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcCancelObserverImpl.traceContext, "RpcCancelObserverImpl.CancelObserver() called. (serverName:{0})", serverName, null, "SendCancelObserver", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcCancelObserverImpl.cs", 89);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(cancelObserverInput, ActiveMonitoringGenericRpcCommandId.CancelObserver, 1, 0);
			RpcCancelObserverImpl.CancelObserverReply cancelObserverReply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcCancelObserverImpl.CancelObserverReply>(requestInfo, serverName, 5000);
			WTFDiagnostics.TraceDebug<string, bool>(ExTraceGlobals.GenericRpcTracer, RpcCancelObserverImpl.traceContext, "RpcCancelObserverImpl.CancelObserver() returned. (serverName:{0}, isAccepted:{1})", serverName, cancelObserverReply.IsAccepted, null, "SendCancelObserver", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcCancelObserverImpl.cs", 108);
		}

		// Token: 0x040018F8 RID: 6392
		public const int MajorVersion = 1;

		// Token: 0x040018F9 RID: 6393
		public const int MinorVersion = 0;

		// Token: 0x040018FA RID: 6394
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x0200056A RID: 1386
		[Serializable]
		internal class CancelObserverInput
		{
			// Token: 0x1700073E RID: 1854
			// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000D27D8 File Offset: 0x000D09D8
			// (set) Token: 0x060022C6 RID: 8902 RVA: 0x000D27E0 File Offset: 0x000D09E0
			public string ServerName { get; set; }
		}

		// Token: 0x0200056B RID: 1387
		[Serializable]
		internal class CancelObserverReply
		{
			// Token: 0x1700073F RID: 1855
			// (get) Token: 0x060022C8 RID: 8904 RVA: 0x000D27F1 File Offset: 0x000D09F1
			// (set) Token: 0x060022C9 RID: 8905 RVA: 0x000D27F9 File Offset: 0x000D09F9
			public bool IsAccepted { get; set; }
		}
	}
}
