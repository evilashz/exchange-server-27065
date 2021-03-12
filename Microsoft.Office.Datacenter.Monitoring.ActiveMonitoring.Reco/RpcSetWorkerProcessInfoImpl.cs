using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000021 RID: 33
	public static class RpcSetWorkerProcessInfoImpl
	{
		// Token: 0x06000106 RID: 262 RVA: 0x000046B0 File Offset: 0x000028B0
		public static void SendRequest(string serverName, RpcSetWorkerProcessInfoImpl.WorkerProcessInfo info, int timeoutInMsec = 30000)
		{
			RpcSetWorkerProcessInfoImpl.Request attachedRequest = new RpcSetWorkerProcessInfoImpl.Request
			{
				WorkerInfo = info
			};
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.SetWorkerProcessInfo, 1, 0);
			ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcSetWorkerProcessInfoImpl.Reply>(requestInfo, serverName, timeoutInMsec);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000046E0 File Offset: 0x000028E0
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcSetWorkerProcessInfoImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcSetWorkerProcessInfoImpl.Request>(requestInfo, 1, 0);
			WorkerProcessRepository.Instance.WorkerProcessInfo = request.WorkerInfo;
			RpcSetWorkerProcessInfoImpl.Reply attachedReply = new RpcSetWorkerProcessInfoImpl.Reply();
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, attachedReply, 1, 0);
		}

		// Token: 0x0400008E RID: 142
		public const int MajorVersion = 1;

		// Token: 0x0400008F RID: 143
		public const int MinorVersion = 0;

		// Token: 0x04000090 RID: 144
		internal const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.SetWorkerProcessInfo;

		// Token: 0x02000022 RID: 34
		[Serializable]
		public class WorkerProcessInfo
		{
			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000108 RID: 264 RVA: 0x00004718 File Offset: 0x00002918
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00004733 File Offset: 0x00002933
			internal DateTime ProcessStartTime
			{
				get
				{
					return this.ProcessStartTimeUtc.ToLocalTime();
				}
				set
				{
					this.ProcessStartTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600010A RID: 266 RVA: 0x00004742 File Offset: 0x00002942
			// (set) Token: 0x0600010B RID: 267 RVA: 0x0000474A File Offset: 0x0000294A
			internal DateTime ProcessStartTimeUtc { get; set; }
		}

		// Token: 0x02000023 RID: 35
		[Serializable]
		internal class Request
		{
			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600010D RID: 269 RVA: 0x0000475B File Offset: 0x0000295B
			// (set) Token: 0x0600010E RID: 270 RVA: 0x00004763 File Offset: 0x00002963
			public RpcSetWorkerProcessInfoImpl.WorkerProcessInfo WorkerInfo { get; set; }
		}

		// Token: 0x02000024 RID: 36
		[Serializable]
		internal class Reply
		{
		}
	}
}
