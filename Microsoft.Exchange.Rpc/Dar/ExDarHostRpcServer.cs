using System;

namespace Microsoft.Exchange.Rpc.Dar
{
	// Token: 0x0200040F RID: 1039
	internal abstract class ExDarHostRpcServer : RpcServerBase
	{
		// Token: 0x060011AF RID: 4527
		public abstract byte[] SendHostRequest(int version, int type, byte[] inputParameterBytes);

		// Token: 0x060011B0 RID: 4528 RVA: 0x00058B08 File Offset: 0x00057F08
		public ExDarHostRpcServer()
		{
		}

		// Token: 0x04001046 RID: 4166
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IExDarHost_v1_0_s_ifspec;
	}
}
