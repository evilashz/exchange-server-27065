using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200058B RID: 1419
	internal static class RpcGetCrimsonEventImpl
	{
		// Token: 0x06002382 RID: 9090 RVA: 0x000D445C File Offset: 0x000D265C
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGetCrimsonEventImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetCrimsonEventImpl.Request>(requestInfo, 1, 0);
			RpcGetCrimsonEventImpl.Reply reply = new RpcGetCrimsonEventImpl.Reply();
			if (DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(request.Servername))
			{
				reply.LastResultTimestamp = RpcGetCrimsonEventImpl.GetLocalLastResultTimestamp<MonitorResult>("HealthManagerHeartbeatMonitor");
				reply.ResultType = WorkItemResultType.Monitor;
			}
			else
			{
				reply.LastResultTimestamp = RpcGetCrimsonEventImpl.GetLocalLastResultTimestamp<ResponderResult>("HealthManagerHeartbeatResponder");
				reply.ResultType = WorkItemResultType.Responder;
			}
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, reply, 1, 0);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000D44C8 File Offset: 0x000D26C8
		public static void SendRequest(string serverName, out DateTime? lastResultTimestamp, int timeoutInMSec = 30000)
		{
			RpcGetCrimsonEventImpl.Request request = new RpcGetCrimsonEventImpl.Request();
			request.Servername = serverName;
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcGetCrimsonEventImpl.traceContext, "RpcGetCrimsonEventImpl.SendRequest() called. (serverName:{0})", serverName, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetCrimsonEventImpl.cs", 123);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(request, ActiveMonitoringGenericRpcCommandId.GetCrimsonMostRecentResultInfo, 1, 0);
			RpcGetCrimsonEventImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetCrimsonEventImpl.Reply>(requestInfo, serverName, timeoutInMSec);
			lastResultTimestamp = reply.LastResultTimestamp;
			WTFDiagnostics.TraceDebug<string, DateTime?, WorkItemResultType>(ExTraceGlobals.GenericRpcTracer, RpcGetCrimsonEventImpl.traceContext, "RpcGetCrimsonEventImpl.SendRequest() returned. (serverName:{0}, lastResultTimestamp:{1} type:{2})", serverName, lastResultTimestamp, reply.ResultType, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetCrimsonEventImpl.cs", 144);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000D4558 File Offset: 0x000D2758
		private static DateTime? GetLocalLastResultTimestamp<TResult>(string resultName) where TResult : WorkItemResult, IPersistence, new()
		{
			DateTime? result = null;
			TResult tresult = default(TResult);
			using (CrimsonReader<TResult> crimsonReader = new CrimsonReader<TResult>())
			{
				crimsonReader.QueryUserPropertyCondition = string.Format("(ResultName='{0}')", resultName);
				crimsonReader.IsReverseDirection = true;
				tresult = crimsonReader.ReadNext();
			}
			if (tresult != null)
			{
				result = new DateTime?(tresult.ExecutionEndTime);
			}
			return result;
		}

		// Token: 0x04001968 RID: 6504
		public const int MajorVersion = 1;

		// Token: 0x04001969 RID: 6505
		public const int MinorVersion = 0;

		// Token: 0x0400196A RID: 6506
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetCrimsonMostRecentResultInfo;

		// Token: 0x0400196B RID: 6507
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x0200058C RID: 1420
		[Serializable]
		internal class Request
		{
			// Token: 0x1700077E RID: 1918
			// (get) Token: 0x06002386 RID: 9094 RVA: 0x000D45E0 File Offset: 0x000D27E0
			// (set) Token: 0x06002387 RID: 9095 RVA: 0x000D45E8 File Offset: 0x000D27E8
			public string Servername { get; set; }
		}

		// Token: 0x0200058D RID: 1421
		[Serializable]
		internal class Reply
		{
			// Token: 0x1700077F RID: 1919
			// (get) Token: 0x06002389 RID: 9097 RVA: 0x000D45F9 File Offset: 0x000D27F9
			// (set) Token: 0x0600238A RID: 9098 RVA: 0x000D4601 File Offset: 0x000D2801
			public DateTime? LastResultTimestamp { get; set; }

			// Token: 0x17000780 RID: 1920
			// (get) Token: 0x0600238B RID: 9099 RVA: 0x000D460A File Offset: 0x000D280A
			// (set) Token: 0x0600238C RID: 9100 RVA: 0x000D4612 File Offset: 0x000D2812
			public WorkItemResultType ResultType { get; set; }
		}
	}
}
