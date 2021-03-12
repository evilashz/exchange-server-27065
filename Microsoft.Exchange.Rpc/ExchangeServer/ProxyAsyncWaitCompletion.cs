using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x0200025D RID: 605
	internal class ProxyAsyncWaitCompletion : IProxyAsyncWaitCompletion
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x000273F4 File Offset: 0x000267F4
		public unsafe ProxyAsyncWaitCompletion(SafeRpcAsyncStateHandle pAsyncState, uint* pulFlagsOut)
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00027418 File Offset: 0x00026818
		public unsafe virtual void CompleteAsyncCall([MarshalAs(UnmanagedType.U1)] bool notificationsPending, int errorCode)
		{
			int num = errorCode;
			IntPtr intPtr = IntPtr.Zero;
			if (!this.m_pAsyncState.IsInvalid)
			{
				IntPtr intPtr2 = this.m_pAsyncState.Detach();
				intPtr = intPtr2;
				if (IntPtr.Zero != intPtr2)
				{
					uint* pulFlagsOut = this.m_pulFlagsOut;
					if (pulFlagsOut != null)
					{
						ProxyAsyncWaitCompletion.OutFlags outFlags = notificationsPending ? ProxyAsyncWaitCompletion.OutFlags.Pending : ProxyAsyncWaitCompletion.OutFlags.None;
						*(int*)pulFlagsOut = (int)outFlags;
						this.m_pulFlagsOut = null;
					}
					<Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)intPtr.ToPointer(), (void*)(&num));
				}
			}
		}

		// Token: 0x04000CD5 RID: 3285
		private SafeRpcAsyncStateHandle m_pAsyncState = pAsyncState;

		// Token: 0x04000CD6 RID: 3286
		private unsafe uint* m_pulFlagsOut = pulFlagsOut;

		// Token: 0x0200025E RID: 606
		private enum OutFlags
		{
			// Token: 0x04000CD8 RID: 3288
			None,
			// Token: 0x04000CD9 RID: 3289
			Pending
		}
	}
}
