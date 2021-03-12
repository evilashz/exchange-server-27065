using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200038F RID: 911
	internal class EcPoolCloseSessionCompletion : AsyncRpcCompletionBase, IPoolCloseSessionCompletion
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x000489B4 File Offset: 0x00047DB4
		public EcPoolCloseSessionCompletion(SafeRpcAsyncStateHandle pAsyncState)
		{
			this.m_pAsyncState = pAsyncState;
			this.m_pcbAuxOut = null;
			this.m_ppbAuxOut = null;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000489E0 File Offset: 0x00047DE0
		public unsafe virtual void CompleteAsyncCall()
		{
			if (!this.m_pAsyncState.IsInvalid)
			{
				IntPtr intPtr = this.m_pAsyncState.Detach();
				IntPtr intPtr2 = intPtr;
				if (IntPtr.Zero != intPtr)
				{
					int num = 0;
					<Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)intPtr2.ToPointer(), (void*)(&num));
				}
			}
		}
	}
}
