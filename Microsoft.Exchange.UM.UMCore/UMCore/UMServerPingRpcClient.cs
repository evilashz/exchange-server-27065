using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000226 RID: 550
	internal sealed class UMServerPingRpcClient : UMServerPingRpcClientBase
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x00047744 File Offset: 0x00045944
		internal UMServerPingRpcClient(string server) : base(server)
		{
			this.targetServer = server;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00047754 File Offset: 0x00045954
		public override void Ping(Guid dialPlanGuid, ref bool availableToTakeCalls)
		{
			try
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcClient: Executing Ping on {0}. DialPlan:{1}", new object[]
				{
					this.targetServer,
					dialPlanGuid
				});
				base.Ping(dialPlanGuid, ref availableToTakeCalls);
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcClient: Ping on {0} succeeded. DialPlan:{1} AvailableToTakeCalls:{2}", new object[]
				{
					this.targetServer,
					dialPlanGuid,
					availableToTakeCalls
				});
			}
			catch (RpcException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcClient: Ping on {0} failed. Dialplan:{1}. ErrorCode:{2} CallStack:{3}", new object[]
				{
					this.targetServer,
					dialPlanGuid,
					ex.ErrorCode,
					ex
				});
				throw;
			}
		}

		// Token: 0x04000B75 RID: 2933
		private string targetServer;
	}
}
