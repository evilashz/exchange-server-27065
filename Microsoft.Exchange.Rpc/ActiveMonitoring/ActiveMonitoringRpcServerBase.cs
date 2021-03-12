using System;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Rpc.ActiveMonitoring
{
	// Token: 0x02000142 RID: 322
	internal abstract class ActiveMonitoringRpcServerBase : RpcServerBase
	{
		// Token: 0x0600089E RID: 2206
		public abstract int RequestMonitoring(Guid mdbGuid);

		// Token: 0x0600089F RID: 2207
		public abstract void CancelMonitoring(Guid mdbGuid);

		// Token: 0x060008A0 RID: 2208
		public abstract int Heartbeat(string serverName, Guid mdbGuid);

		// Token: 0x060008A1 RID: 2209
		public abstract int RequestCredential(string serverName, Guid mdbGuid, string userPrincipalName, ref string credential);

		// Token: 0x060008A2 RID: 2210
		public abstract RpcErrorExceptionInfo GenericRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo);

		// Token: 0x060008A3 RID: 2211
		public abstract int CreateMonitoringMailbox(string displayName, Guid mdbGuid);

		// Token: 0x060008A4 RID: 2212 RVA: 0x000087FC File Offset: 0x00007BFC
		public static void StopServer()
		{
			RpcServerBase.StopServer(ActiveMonitoringRpcServerBase.RpcIntfHandle);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00008814 File Offset: 0x00007C14
		public ActiveMonitoringRpcServerBase()
		{
		}

		// Token: 0x04000AD2 RID: 2770
		internal static IntPtr RpcIntfHandle = (IntPtr)<Module>.IActiveMonitoringRpc_v1_0_s_ifspec;
	}
}
