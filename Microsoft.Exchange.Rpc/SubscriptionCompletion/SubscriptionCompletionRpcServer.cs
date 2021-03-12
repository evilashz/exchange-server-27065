using System;

namespace Microsoft.Exchange.Rpc.SubscriptionCompletion
{
	// Token: 0x020003F4 RID: 1012
	internal abstract class SubscriptionCompletionRpcServer : RpcServerBase
	{
		// Token: 0x06001156 RID: 4438
		public abstract byte[] SubscriptionComplete(int version, byte[] pInBytes);

		// Token: 0x06001157 RID: 4439 RVA: 0x00056F7C File Offset: 0x0005637C
		public SubscriptionCompletionRpcServer()
		{
		}

		// Token: 0x04001024 RID: 4132
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISubscriptionCompletion_v1_0_s_ifspec;
	}
}
