using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.SubscriptionNotification
{
	// Token: 0x020003F6 RID: 1014
	internal abstract class SubscriptionNotificationRpcServer : RpcServerBase
	{
		// Token: 0x0600115D RID: 4445
		public abstract byte[] InvokeSubscriptionNotificationEndPoint(int version, byte[] pInBytes);

		// Token: 0x0600115E RID: 4446 RVA: 0x00057098 File Offset: 0x00056498
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsRpcConnectionError(int errorCode)
		{
			return errorCode == 1753 || errorCode == 1722 || errorCode == 1727;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0005712C File Offset: 0x0005652C
		public SubscriptionNotificationRpcServer()
		{
		}

		// Token: 0x04001026 RID: 4134
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISubscriptionNotification_v1_0_s_ifspec;
	}
}
