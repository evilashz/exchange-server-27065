using System;

namespace Microsoft.Exchange.Rpc.SubscriptionSubmission
{
	// Token: 0x020003F8 RID: 1016
	internal abstract class SubscriptionSubmissionRpcServer : RpcServerBase
	{
		// Token: 0x06001165 RID: 4453
		public abstract byte[] SubscriptionSubmit(int version, byte[] pInBytes);

		// Token: 0x06001166 RID: 4454 RVA: 0x000572C0 File Offset: 0x000566C0
		public SubscriptionSubmissionRpcServer()
		{
		}

		// Token: 0x04001029 RID: 4137
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISubscriptionSubmission_v1_0_s_ifspec;
	}
}
