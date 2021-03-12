using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002FC RID: 764
	internal class ReplayRpcClientHelper
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x0008C3B4 File Offset: 0x0008A5B4
		public static void RequestSuspend3(string servername, Guid guid, string suspendcomment, uint flags, uint initiator)
		{
			ReplayRpcClientHelper.defaultInstance.RequestSuspend3(servername, guid, suspendcomment, flags, initiator);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0008C3C6 File Offset: 0x0008A5C6
		internal static void RequestResume2(string servername, Guid guid, uint flags)
		{
			ReplayRpcClientHelper.defaultInstance.RequestResume2(servername, guid, flags);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0008C3D5 File Offset: 0x0008A5D5
		internal static void RpccDisableReplayLag(string serverName, Guid dbGuid, string disableReason, ActionInitiatorType actionInitiator)
		{
			ReplayRpcClientHelper.defaultInstance.RpccDisableReplayLag(serverName, dbGuid, disableReason, actionInitiator);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0008C3E5 File Offset: 0x0008A5E5
		internal static void RpccEnableReplayLag(string serverName, Guid dbGuid, ActionInitiatorType actionInitiator)
		{
			ReplayRpcClientHelper.defaultInstance.RpccEnableReplayLag(serverName, dbGuid, actionInitiator);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0008C3F4 File Offset: 0x0008A5F4
		internal static RpcDatabaseCopyStatus2[] GetCopyStatus(string serverName, RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids)
		{
			return ReplayRpcClientHelper.defaultInstance.GetCopyStatus(serverName, collectionFlags2, dbGuids);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0008C403 File Offset: 0x0008A603
		internal static void NotifyChangedReplayConfiguration(string serverName, Guid dbGuid, ServerVersion serverVersion, bool waitForCompletion, bool isHighPriority, ReplayConfigChangeHints changeHint)
		{
			ReplayRpcClientHelper.defaultInstance.NotifyChangedReplayConfiguration(serverName, dbGuid, serverVersion, waitForCompletion, isHighPriority, changeHint);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0008C417 File Offset: 0x0008A617
		internal static void NotifyChangedReplayConfigurationAsync(string serverName, Guid dbGuid, ReplayConfigChangeHints changeHint)
		{
			ReplayRpcClientHelper.defaultInstance.NotifyChangedReplayConfigurationAsync(serverName, dbGuid, changeHint);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0008C428 File Offset: 0x0008A628
		internal static Dictionary<Guid, RpcDatabaseCopyStatus2> ParseStatusResults(RpcDatabaseCopyStatus2[] statusResults)
		{
			Dictionary<Guid, RpcDatabaseCopyStatus2> dictionary = new Dictionary<Guid, RpcDatabaseCopyStatus2>();
			if (statusResults != null && statusResults.Length > 0)
			{
				foreach (RpcDatabaseCopyStatus2 rpcDatabaseCopyStatus in statusResults)
				{
					if (!dictionary.ContainsKey(rpcDatabaseCopyStatus.DBGuid))
					{
						dictionary.Add(rpcDatabaseCopyStatus.DBGuid, rpcDatabaseCopyStatus);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04000CEC RID: 3308
		private static ReplayRpcClientWrapper defaultInstance = new ReplayRpcClientWrapper();
	}
}
