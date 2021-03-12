using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200056C RID: 1388
	internal static class RpcGetLastBugcheckStatusImpl
	{
		// Token: 0x060022CB RID: 8907 RVA: 0x000D280C File Offset: 0x000D0A0C
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			if (BugcheckSimulator.Instance.IsHangRpc)
			{
				TimeSpan duration = BugcheckSimulator.Instance.Duration;
				Thread.Sleep(duration + TimeSpan.FromSeconds(5.0));
			}
			RpcGetLastBugcheckStatusImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetLastBugcheckStatusImpl.Request>(requestInfo, 1, 0);
			DateTime systemBootTime = RecoveryActionHelper.GetSystemBootTime();
			DateTime inflightBugcheckStartTime = DateTime.MinValue;
			TimeSpan inflightBugcheckTimeRemaining = TimeSpan.Zero;
			RecoveryActionEntry recoveryActionEntry = RecoveryActionHelper.FindActionEntry(RecoveryActionId.ForceReboot, null, request.QueryStartTime, request.QueryEndTime);
			if (recoveryActionEntry != null && recoveryActionEntry.State == RecoveryActionState.Started)
			{
				inflightBugcheckStartTime = recoveryActionEntry.StartTime;
				inflightBugcheckTimeRemaining = recoveryActionEntry.EndTime - recoveryActionEntry.StartTime;
			}
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcGetLastBugcheckStatusImpl.Reply
			{
				SystemStartTime = systemBootTime,
				InflightBugcheckStartTime = inflightBugcheckStartTime,
				InflightBugcheckTimeRemaining = inflightBugcheckTimeRemaining
			}, 1, 0);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000D28D4 File Offset: 0x000D0AD4
		public static void SendRequest(string serverName, DateTime queryStartTime, DateTime queryEndTime, out DateTime systemStartTime, out DateTime inflightBugcheckStartTime, out TimeSpan inflightBugcheckTimeRemaining, int timeoutInMSec = 30000)
		{
			RpcGetLastBugcheckStatusImpl.Request request = new RpcGetLastBugcheckStatusImpl.Request();
			request.QueryStartTime = queryStartTime;
			request.QueryEndTime = queryEndTime;
			WTFDiagnostics.TraceDebug<string, DateTime, DateTime>(ExTraceGlobals.GenericRpcTracer, RpcGetLastBugcheckStatusImpl.traceContext, "RpcGetLastBugcheckStatusImpl.SendRequest() called. (serverName:{0}, queryStartTime:{1}, queryEndTime: {2})", serverName, queryStartTime, queryEndTime, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetLastBugcheckStatusImpl.cs", 127);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(request, ActiveMonitoringGenericRpcCommandId.GetLastBugcheckStatus, 1, 0);
			RpcGetLastBugcheckStatusImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetLastBugcheckStatusImpl.Reply>(requestInfo, serverName, timeoutInMSec);
			systemStartTime = reply.SystemStartTime;
			inflightBugcheckStartTime = reply.InflightBugcheckStartTime;
			inflightBugcheckTimeRemaining = reply.InflightBugcheckTimeRemaining;
			WTFDiagnostics.TraceDebug<string, DateTime, DateTime, TimeSpan>(ExTraceGlobals.GenericRpcTracer, RpcGetLastBugcheckStatusImpl.traceContext, "RpcGetLastBugcheckStatusImpl.SendRequest() returned. (serverName:{0}, systemStartTime:{1}, inflightBugcheckTime:{2}, inflightBugcheckDuration: {3})", serverName, systemStartTime, inflightBugcheckStartTime, inflightBugcheckTimeRemaining, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetLastBugcheckStatusImpl.cs", 152);
		}

		// Token: 0x040018FD RID: 6397
		public const int MajorVersion = 1;

		// Token: 0x040018FE RID: 6398
		public const int MinorVersion = 0;

		// Token: 0x040018FF RID: 6399
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetLastBugcheckStatus;

		// Token: 0x04001900 RID: 6400
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x0200056D RID: 1389
		[Serializable]
		internal class Request
		{
			// Token: 0x17000740 RID: 1856
			// (get) Token: 0x060022CE RID: 8910 RVA: 0x000D2998 File Offset: 0x000D0B98
			// (set) Token: 0x060022CF RID: 8911 RVA: 0x000D29A0 File Offset: 0x000D0BA0
			public DateTime QueryStartTime { get; set; }

			// Token: 0x17000741 RID: 1857
			// (get) Token: 0x060022D0 RID: 8912 RVA: 0x000D29A9 File Offset: 0x000D0BA9
			// (set) Token: 0x060022D1 RID: 8913 RVA: 0x000D29B1 File Offset: 0x000D0BB1
			public DateTime QueryEndTime { get; set; }
		}

		// Token: 0x0200056E RID: 1390
		[Serializable]
		internal class Reply
		{
			// Token: 0x17000742 RID: 1858
			// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000D29C2 File Offset: 0x000D0BC2
			// (set) Token: 0x060022D4 RID: 8916 RVA: 0x000D29CA File Offset: 0x000D0BCA
			public DateTime SystemStartTime { get; set; }

			// Token: 0x17000743 RID: 1859
			// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000D29D3 File Offset: 0x000D0BD3
			// (set) Token: 0x060022D6 RID: 8918 RVA: 0x000D29DB File Offset: 0x000D0BDB
			public DateTime InflightBugcheckStartTime { get; set; }

			// Token: 0x17000744 RID: 1860
			// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000D29E4 File Offset: 0x000D0BE4
			// (set) Token: 0x060022D8 RID: 8920 RVA: 0x000D29EC File Offset: 0x000D0BEC
			public TimeSpan InflightBugcheckTimeRemaining { get; set; }
		}
	}
}
