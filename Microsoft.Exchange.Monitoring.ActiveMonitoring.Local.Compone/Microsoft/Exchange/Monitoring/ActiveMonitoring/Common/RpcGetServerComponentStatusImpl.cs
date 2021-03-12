using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200057F RID: 1407
	internal static class RpcGetServerComponentStatusImpl
	{
		// Token: 0x06002349 RID: 9033 RVA: 0x000D3570 File Offset: 0x000D1770
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGetServerComponentStatusImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetServerComponentStatusImpl.Request>(requestInfo, 1, 0);
			DateTime lastOfflineRequestStartTime = DateTime.MinValue;
			DateTime lastOfflineRequestEndTime = DateTime.MinValue;
			RecoveryActionEntry recoveryActionEntry = RecoveryActionHelper.FindActionEntry(RecoveryActionId.TakeComponentOffline, null, request.QueryStartTime, request.QueryEndTime);
			if (recoveryActionEntry != null)
			{
				lastOfflineRequestStartTime = recoveryActionEntry.StartTime;
				lastOfflineRequestEndTime = recoveryActionEntry.EndTime;
			}
			RpcGetServerComponentStatusImpl.Reply reply = new RpcGetServerComponentStatusImpl.Reply();
			ServerComponentEnum serverComponentEnum = ServerComponentEnum.None;
			Enum.TryParse<ServerComponentEnum>(request.ServerComponentName, out serverComponentEnum);
			if (serverComponentEnum != ServerComponentEnum.None)
			{
				reply.IsOnline = ServerComponentStateManager.IsOnline(serverComponentEnum);
			}
			else
			{
				reply.IsOnline = true;
			}
			reply.LastOfflineRequestStartTime = lastOfflineRequestStartTime;
			reply.LastOfflineRequestEndTime = lastOfflineRequestEndTime;
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, reply, 1, 0);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000D3608 File Offset: 0x000D1808
		public static void SendRequest(string serverName, ServerComponentEnum serverComponent, DateTime queryStartTime, DateTime queryEndTime, out bool isOnline, out DateTime lastOfflineRequestStartTime, out DateTime lastOfflineRequestEndTime, int timeoutInMSec = 30000)
		{
			RpcGetServerComponentStatusImpl.Request request = new RpcGetServerComponentStatusImpl.Request();
			request.ServerComponentName = serverComponent.ToString();
			request.QueryStartTime = queryStartTime;
			request.QueryEndTime = queryEndTime;
			WTFDiagnostics.TraceDebug<string, DateTime, DateTime>(ExTraceGlobals.GenericRpcTracer, RpcGetServerComponentStatusImpl.traceContext, "RpcGetServerComponentStatusImpl.SendRequest() called. (serverName:{0}, queryStartTime:{1}, queryEndTime: {2})", serverName, queryStartTime, queryEndTime, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetServerComponentStatusImpl.cs", 137);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(request, ActiveMonitoringGenericRpcCommandId.GetServerComponentStatus, 1, 0);
			RpcGetServerComponentStatusImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetServerComponentStatusImpl.Reply>(requestInfo, serverName, timeoutInMSec);
			isOnline = reply.IsOnline;
			lastOfflineRequestStartTime = reply.LastOfflineRequestStartTime;
			lastOfflineRequestEndTime = reply.LastOfflineRequestEndTime;
			WTFDiagnostics.TraceDebug<string, bool, DateTime, DateTime>(ExTraceGlobals.GenericRpcTracer, RpcGetServerComponentStatusImpl.traceContext, "RpcGetServerComponentStatusImpl.SendRequest() returned. (serverName:{0}, isOnline:{1} lastOfflineRequestStartTime:{2}, lastOfflineRequestEndTime: {3})", serverName, isOnline, lastOfflineRequestStartTime, lastOfflineRequestEndTime, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetServerComponentStatusImpl.cs", 162);
		}

		// Token: 0x04001945 RID: 6469
		public const int MajorVersion = 1;

		// Token: 0x04001946 RID: 6470
		public const int MinorVersion = 0;

		// Token: 0x04001947 RID: 6471
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetServerComponentStatus;

		// Token: 0x04001948 RID: 6472
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000580 RID: 1408
		[Serializable]
		internal class Request
		{
			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x0600234C RID: 9036 RVA: 0x000D36DB File Offset: 0x000D18DB
			// (set) Token: 0x0600234D RID: 9037 RVA: 0x000D36E3 File Offset: 0x000D18E3
			public string ServerComponentName { get; set; }

			// Token: 0x17000771 RID: 1905
			// (get) Token: 0x0600234E RID: 9038 RVA: 0x000D36EC File Offset: 0x000D18EC
			// (set) Token: 0x0600234F RID: 9039 RVA: 0x000D36F4 File Offset: 0x000D18F4
			public DateTime QueryStartTime { get; set; }

			// Token: 0x17000772 RID: 1906
			// (get) Token: 0x06002350 RID: 9040 RVA: 0x000D36FD File Offset: 0x000D18FD
			// (set) Token: 0x06002351 RID: 9041 RVA: 0x000D3705 File Offset: 0x000D1905
			public DateTime QueryEndTime { get; set; }
		}

		// Token: 0x02000581 RID: 1409
		[Serializable]
		internal class Reply
		{
			// Token: 0x17000773 RID: 1907
			// (get) Token: 0x06002353 RID: 9043 RVA: 0x000D3716 File Offset: 0x000D1916
			// (set) Token: 0x06002354 RID: 9044 RVA: 0x000D371E File Offset: 0x000D191E
			public bool IsOnline { get; set; }

			// Token: 0x17000774 RID: 1908
			// (get) Token: 0x06002355 RID: 9045 RVA: 0x000D3727 File Offset: 0x000D1927
			// (set) Token: 0x06002356 RID: 9046 RVA: 0x000D372F File Offset: 0x000D192F
			public DateTime LastOfflineRequestStartTime { get; set; }

			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x06002357 RID: 9047 RVA: 0x000D3738 File Offset: 0x000D1938
			// (set) Token: 0x06002358 RID: 9048 RVA: 0x000D3740 File Offset: 0x000D1940
			public DateTime LastOfflineRequestEndTime { get; set; }
		}
	}
}
