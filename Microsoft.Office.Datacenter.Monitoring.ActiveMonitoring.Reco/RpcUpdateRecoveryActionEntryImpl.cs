using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200002B RID: 43
	public static class RpcUpdateRecoveryActionEntryImpl
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00004B0C File Offset: 0x00002D0C
		public static void SendRequest(string serverName, RecoveryActionEntry entry, int timeoutInMsec = 30000)
		{
			RpcUpdateRecoveryActionEntryImpl.Request attachedRequest = new RpcUpdateRecoveryActionEntryImpl.Request
			{
				Entry = RecoveryActionHelper.CreateSerializableRecoveryActionEntry(entry)
			};
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.UpdateRecoveryActionEntry, 1, 0);
			ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcUpdateRecoveryActionEntryImpl.Reply>(requestInfo, serverName, timeoutInMsec);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004B44 File Offset: 0x00002D44
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcUpdateRecoveryActionEntryImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcUpdateRecoveryActionEntryImpl.Request>(requestInfo, 1, 0);
			RecoveryActionsRepository.Instance.AddEntry(request.Entry, true, false);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcUpdateRecoveryActionEntryImpl.Reply(), 1, 0);
		}

		// Token: 0x040000AC RID: 172
		public const int MajorVersion = 1;

		// Token: 0x040000AD RID: 173
		public const int MinorVersion = 0;

		// Token: 0x040000AE RID: 174
		internal const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.UpdateRecoveryActionEntry;

		// Token: 0x0200002C RID: 44
		[Serializable]
		internal class Request
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000142 RID: 322 RVA: 0x00004B7B File Offset: 0x00002D7B
			// (set) Token: 0x06000143 RID: 323 RVA: 0x00004B83 File Offset: 0x00002D83
			public RecoveryActionHelper.RecoveryActionEntrySerializable Entry { get; set; }
		}

		// Token: 0x0200002D RID: 45
		[Serializable]
		internal class Reply
		{
		}
	}
}
