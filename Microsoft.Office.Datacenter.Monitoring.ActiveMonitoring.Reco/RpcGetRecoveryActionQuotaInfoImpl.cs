using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000028 RID: 40
	public static class RpcGetRecoveryActionQuotaInfoImpl
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00004928 File Offset: 0x00002B28
		public static RecoveryActionHelper.RecoveryActionQuotaInfo SendRequest(string serverName, RecoveryActionId actionId, string resourceName, int maxAllowedQuota, DateTime queryStartTime, DateTime queryEndTime, int timeoutInMSec = 30000)
		{
			RpcGetRecoveryActionQuotaInfoImpl.Request attachedRequest = new RpcGetRecoveryActionQuotaInfoImpl.Request(actionId, resourceName, maxAllowedQuota, queryStartTime, queryEndTime, TimeSpan.FromMilliseconds((double)timeoutInMSec));
			WTFDiagnostics.TraceDebug<string, DateTime, DateTime>(ExTraceGlobals.GenericRpcTracer, RpcGetRecoveryActionQuotaInfoImpl.traceContext, "RpcGetRecoveryActionQuotaInfoImpl.SendRequest() called. (serverName:{0}, queryStartTime:{1}, queryEndTime: {2})", serverName, queryStartTime, queryEndTime, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\Rpc\\RpcGetRecoveryActionQuotaInfoImpl.cs", 78);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.GetRecoveryActionQuotaInfo, 1, 0);
			RpcGetRecoveryActionQuotaInfoImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetRecoveryActionQuotaInfoImpl.Reply>(requestInfo, serverName, timeoutInMSec);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcGetRecoveryActionQuotaInfoImpl.traceContext, "RpcGetRecoveryActionQuotaInfoImpl.SendRequest() returned. (serverName:{0})", serverName, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\Rpc\\RpcGetRecoveryActionQuotaInfoImpl.cs", 99);
			return reply.QuotaInfo;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000049B0 File Offset: 0x00002BB0
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGetRecoveryActionQuotaInfoImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetRecoveryActionQuotaInfoImpl.Request>(requestInfo, 1, 0);
			RecoveryActionHelper.RecoveryActionQuotaInfo recoveryActionQuotaInfo = RecoveryActionHelper.GetRecoveryActionQuotaInfo(request.ActionId, request.ResourceName, request.MaxAllowedQuota, request.QueryStartTime, request.QueryEndTime, request.Timeout);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcGetRecoveryActionQuotaInfoImpl.Reply
			{
				QuotaInfo = recoveryActionQuotaInfo
			}, 1, 0);
		}

		// Token: 0x040000A1 RID: 161
		public const int MajorVersion = 1;

		// Token: 0x040000A2 RID: 162
		public const int MinorVersion = 0;

		// Token: 0x040000A3 RID: 163
		internal const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetRecoveryActionQuotaInfo;

		// Token: 0x040000A4 RID: 164
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000029 RID: 41
		[Serializable]
		internal class Request
		{
			// Token: 0x0600012E RID: 302 RVA: 0x00004A14 File Offset: 0x00002C14
			public Request(RecoveryActionId actionId, string resourceName, int maxAllowedQuota, DateTime queryStartTime, DateTime queryEndTime, TimeSpan timeout)
			{
				this.ActionId = actionId;
				this.ResourceName = resourceName;
				this.MaxAllowedQuota = maxAllowedQuota;
				this.QueryStartTimeUtc = queryStartTime.ToUniversalTime();
				this.QueryEndTimeUtc = queryEndTime.ToUniversalTime();
				this.Timeout = timeout;
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x0600012F RID: 303 RVA: 0x00004A53 File Offset: 0x00002C53
			// (set) Token: 0x06000130 RID: 304 RVA: 0x00004A5B File Offset: 0x00002C5B
			public RecoveryActionId ActionId { get; set; }

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000131 RID: 305 RVA: 0x00004A64 File Offset: 0x00002C64
			// (set) Token: 0x06000132 RID: 306 RVA: 0x00004A6C File Offset: 0x00002C6C
			public string ResourceName { get; set; }

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000133 RID: 307 RVA: 0x00004A75 File Offset: 0x00002C75
			// (set) Token: 0x06000134 RID: 308 RVA: 0x00004A7D File Offset: 0x00002C7D
			public int MaxAllowedQuota { get; private set; }

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000135 RID: 309 RVA: 0x00004A86 File Offset: 0x00002C86
			// (set) Token: 0x06000136 RID: 310 RVA: 0x00004A8E File Offset: 0x00002C8E
			public DateTime QueryStartTimeUtc { get; set; }

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000137 RID: 311 RVA: 0x00004A98 File Offset: 0x00002C98
			public DateTime QueryStartTime
			{
				get
				{
					return this.QueryStartTimeUtc.ToLocalTime();
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000138 RID: 312 RVA: 0x00004AB3 File Offset: 0x00002CB3
			// (set) Token: 0x06000139 RID: 313 RVA: 0x00004ABB File Offset: 0x00002CBB
			public DateTime QueryEndTimeUtc { get; set; }

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600013A RID: 314 RVA: 0x00004AC4 File Offset: 0x00002CC4
			public DateTime QueryEndTime
			{
				get
				{
					return this.QueryEndTimeUtc.ToLocalTime();
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600013B RID: 315 RVA: 0x00004ADF File Offset: 0x00002CDF
			// (set) Token: 0x0600013C RID: 316 RVA: 0x00004AE7 File Offset: 0x00002CE7
			public TimeSpan Timeout { get; set; }
		}

		// Token: 0x0200002A RID: 42
		[Serializable]
		internal class Reply
		{
			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600013D RID: 317 RVA: 0x00004AF0 File Offset: 0x00002CF0
			// (set) Token: 0x0600013E RID: 318 RVA: 0x00004AF8 File Offset: 0x00002CF8
			public RecoveryActionHelper.RecoveryActionQuotaInfo QuotaInfo { get; set; }
		}
	}
}
