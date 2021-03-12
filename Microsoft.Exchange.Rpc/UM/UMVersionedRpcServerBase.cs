using System;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000409 RID: 1033
	internal abstract class UMVersionedRpcServerBase : RpcServerBase
	{
		// Token: 0x0600119E RID: 4510
		public abstract int ExecuteRequest(byte[] request, out byte[] response);

		// Token: 0x0600119F RID: 4511 RVA: 0x00058948 File Offset: 0x00057D48
		public UMVersionedRpcServerBase()
		{
		}

		// Token: 0x0400103C RID: 4156
		public static IntPtr UMPlayOnPhoneRpcIntfHandle = (IntPtr)<Module>.IUMPlayOnPhone_v1_0_s_ifspec;

		// Token: 0x0400103D RID: 4157
		public static IntPtr UMPartnerMessageRpcIntfHandle = (IntPtr)<Module>.IUMPartnerMessage_v1_0_s_ifspec;

		// Token: 0x0400103E RID: 4158
		public static IntPtr UMRecipientTasksRpcIntfHandle = (IntPtr)<Module>.IUMRecipientTasks_v1_0_s_ifspec;

		// Token: 0x0400103F RID: 4159
		public static IntPtr UMPromptPreviewRpcIntfHandle = (IntPtr)<Module>.IUMPromptPreview_v1_0_s_ifspec;
	}
}
