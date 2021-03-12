using System;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x0200040A RID: 1034
	internal abstract class UMServerPingRpcServerBase : RpcServerBase
	{
		// Token: 0x060011A1 RID: 4513
		public abstract int Ping(Guid dialPlanGuid, ref bool availableToTakeCalls);

		// Token: 0x060011A2 RID: 4514 RVA: 0x0005895C File Offset: 0x00057D5C
		public UMServerPingRpcServerBase()
		{
		}

		// Token: 0x04001040 RID: 4160
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IUMServerHealth_v1_0_s_ifspec;
	}
}
