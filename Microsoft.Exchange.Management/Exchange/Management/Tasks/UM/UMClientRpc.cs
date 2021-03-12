using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D5E RID: 3422
	internal class UMClientRpc : UMRpcClient
	{
		// Token: 0x0600833C RID: 33596 RVA: 0x002181FD File Offset: 0x002163FD
		public UMClientRpc(string serverName) : base(serverName)
		{
			ExTraceGlobals.DiagnosticTracer.TraceDebug((long)this.GetHashCode(), "In UMClientRpc");
		}

		// Token: 0x0600833D RID: 33597 RVA: 0x0021821C File Offset: 0x0021641C
		public ActiveCalls[] GetUmActiveCallList(bool isDialPlan, string dialPlan, bool isIpGateway, string ipGateway)
		{
			ExTraceGlobals.DiagnosticTracer.TraceDebug((long)this.GetHashCode(), "In UMClientRpc.GetUmActiveCallList");
			byte[] umActiveCalls = base.GetUmActiveCalls(isDialPlan, dialPlan, isIpGateway, ipGateway);
			return (ActiveCalls[])Serialization.BytesToObject(umActiveCalls);
		}
	}
}
