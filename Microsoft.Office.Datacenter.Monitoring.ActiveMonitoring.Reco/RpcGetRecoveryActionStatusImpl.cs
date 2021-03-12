using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000025 RID: 37
	public static class RpcGetRecoveryActionStatusImpl
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000477C File Offset: 0x0000297C
		public static List<RecoveryActionEntry> SendRequest(string serverName, RecoveryActionId actionId, string resourceName, RecoveryActionState state, RecoveryActionResult result, DateTime startTime, DateTime endTime, string xpathQueryString = null, int maxCount = -1, int timeoutInMsec = 30000)
		{
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(new RpcGetRecoveryActionStatusImpl.Request
			{
				ActionId = actionId,
				ResourceName = resourceName,
				State = state,
				Result = result,
				StartTime = startTime,
				EndTime = endTime,
				MaxEntries = maxCount,
				XPathQuery = xpathQueryString
			}, ActiveMonitoringGenericRpcCommandId.GetRecoveryActionStatus, 1, 0);
			RpcGetRecoveryActionStatusImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetRecoveryActionStatusImpl.Reply>(requestInfo, serverName, timeoutInMsec);
			return reply.RecoveryActionEntries;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000047E8 File Offset: 0x000029E8
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			bool isMoreEntriesAvailable = false;
			RpcGetRecoveryActionStatusImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetRecoveryActionStatusImpl.Request>(requestInfo, 1, 0);
			List<RecoveryActionEntry> recoveryActionEntries = RecoveryActionHelper.ReadEntries(request.ActionId, request.ResourceName, null, request.State, request.Result, request.StartTime, request.EndTime, out isMoreEntriesAvailable, request.XPathQuery, TimeSpan.MaxValue, request.MaxEntries);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcGetRecoveryActionStatusImpl.Reply
			{
				IsMoreEntriesAvailable = isMoreEntriesAvailable,
				RecoveryActionEntries = recoveryActionEntries
			}, 1, 0);
		}

		// Token: 0x04000093 RID: 147
		public const int MajorVersion = 1;

		// Token: 0x04000094 RID: 148
		public const int MinorVersion = 0;

		// Token: 0x04000095 RID: 149
		internal const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetRecoveryActionStatus;

		// Token: 0x02000026 RID: 38
		[Serializable]
		internal class Request
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000113 RID: 275 RVA: 0x0000485D File Offset: 0x00002A5D
			// (set) Token: 0x06000114 RID: 276 RVA: 0x00004865 File Offset: 0x00002A65
			public RecoveryActionId ActionId { get; set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000115 RID: 277 RVA: 0x0000486E File Offset: 0x00002A6E
			// (set) Token: 0x06000116 RID: 278 RVA: 0x00004876 File Offset: 0x00002A76
			public string ResourceName { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000117 RID: 279 RVA: 0x0000487F File Offset: 0x00002A7F
			// (set) Token: 0x06000118 RID: 280 RVA: 0x00004887 File Offset: 0x00002A87
			public string InstanceId { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000119 RID: 281 RVA: 0x00004890 File Offset: 0x00002A90
			// (set) Token: 0x0600011A RID: 282 RVA: 0x00004898 File Offset: 0x00002A98
			public RecoveryActionState State { get; set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600011B RID: 283 RVA: 0x000048A1 File Offset: 0x00002AA1
			// (set) Token: 0x0600011C RID: 284 RVA: 0x000048A9 File Offset: 0x00002AA9
			public RecoveryActionResult Result { get; set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600011D RID: 285 RVA: 0x000048B2 File Offset: 0x00002AB2
			// (set) Token: 0x0600011E RID: 286 RVA: 0x000048BA File Offset: 0x00002ABA
			public DateTime StartTime { get; set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600011F RID: 287 RVA: 0x000048C3 File Offset: 0x00002AC3
			// (set) Token: 0x06000120 RID: 288 RVA: 0x000048CB File Offset: 0x00002ACB
			public DateTime EndTime { get; set; }

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000121 RID: 289 RVA: 0x000048D4 File Offset: 0x00002AD4
			// (set) Token: 0x06000122 RID: 290 RVA: 0x000048DC File Offset: 0x00002ADC
			public string XPathQuery { get; set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000123 RID: 291 RVA: 0x000048E5 File Offset: 0x00002AE5
			// (set) Token: 0x06000124 RID: 292 RVA: 0x000048ED File Offset: 0x00002AED
			public int MaxEntries { get; set; }
		}

		// Token: 0x02000027 RID: 39
		[Serializable]
		internal class Reply
		{
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000126 RID: 294 RVA: 0x000048FE File Offset: 0x00002AFE
			// (set) Token: 0x06000127 RID: 295 RVA: 0x00004906 File Offset: 0x00002B06
			public bool IsMoreEntriesAvailable { get; set; }

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000128 RID: 296 RVA: 0x0000490F File Offset: 0x00002B0F
			// (set) Token: 0x06000129 RID: 297 RVA: 0x00004917 File Offset: 0x00002B17
			public List<RecoveryActionEntry> RecoveryActionEntries { get; set; }
		}
	}
}
