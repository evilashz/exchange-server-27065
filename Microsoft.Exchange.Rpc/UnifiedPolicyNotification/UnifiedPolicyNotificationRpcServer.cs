using System;

namespace Microsoft.Exchange.Rpc.UnifiedPolicyNotification
{
	// Token: 0x0200040E RID: 1038
	internal abstract class UnifiedPolicyNotificationRpcServer : RpcServerBase
	{
		// Token: 0x060011AC RID: 4524
		public abstract byte[] Notify(int version, int type, byte[] inputParameterBytes);

		// Token: 0x060011AD RID: 4525 RVA: 0x00058A98 File Offset: 0x00057E98
		public UnifiedPolicyNotificationRpcServer()
		{
		}

		// Token: 0x04001045 RID: 4165
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IUnifiedPolicyNotification_v1_0_s_ifspec;
	}
}
