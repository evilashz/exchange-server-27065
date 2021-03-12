using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000407 RID: 1031
	internal abstract class UMRpcServer : RpcServerBase
	{
		// Token: 0x06001198 RID: 4504
		public abstract byte[] GetUmActiveCalls([MarshalAs(UnmanagedType.U1)] bool isDialPlan, string dialPlan, [MarshalAs(UnmanagedType.U1)] bool isIpGateway, string ipGateway);

		// Token: 0x06001199 RID: 4505 RVA: 0x00058920 File Offset: 0x00057D20
		public UMRpcServer()
		{
		}

		// Token: 0x0400103A RID: 4154
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IUM_v2_0_s_ifspec;
	}
}
