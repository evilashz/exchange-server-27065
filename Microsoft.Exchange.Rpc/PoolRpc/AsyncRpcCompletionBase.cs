using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200038C RID: 908
	internal class AsyncRpcCompletionBase : IRpcAsyncCompletion
	{
		// Token: 0x0600101C RID: 4124 RVA: 0x00048400 File Offset: 0x00047800
		public unsafe AsyncRpcCompletionBase(SafeRpcAsyncStateHandle pAsyncState, uint* pcbAuxOut, byte** ppbAuxOut)
		{
			this.m_pAsyncState = pAsyncState;
			this.m_pcbAuxOut = pcbAuxOut;
			this.m_ppbAuxOut = ppbAuxOut;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00048428 File Offset: 0x00047828
		public virtual void AbortAsyncCall(uint exceptionCode)
		{
			this.m_pAsyncState.AbortCall(exceptionCode);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00048444 File Offset: 0x00047844
		public unsafe virtual void FailAsyncCall(int errorCode, ArraySegment<byte> auxiliaryOut)
		{
			int num = errorCode;
			int num2 = 0;
			byte* ptr = null;
			IntPtr intPtr = IntPtr.Zero;
			bool flag = true;
			if (!this.m_pAsyncState.IsInvalid)
			{
				try
				{
					if (null != this.m_ppbAuxOut)
					{
						<Module>.MToUBytesSegment(auxiliaryOut, (int*)(&num2), &ptr);
					}
					IntPtr intPtr2 = this.m_pAsyncState.Detach();
					intPtr = intPtr2;
					if (IntPtr.Zero != intPtr2)
					{
						uint* pcbAuxOut = this.m_pcbAuxOut;
						if (null != pcbAuxOut)
						{
							*(int*)pcbAuxOut = num2;
						}
						byte** ppbAuxOut = this.m_ppbAuxOut;
						if (null != ppbAuxOut)
						{
							*(long*)ppbAuxOut = ptr;
							ptr = null;
						}
						<Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)intPtr.ToPointer(), (void*)(&num));
					}
					flag = false;
				}
				finally
				{
					if (ptr != null)
					{
						<Module>.MIDL_user_free((void*)ptr);
					}
					if (flag)
					{
						this.AbortAsyncCall(1726U);
					}
				}
			}
		}

		// Token: 0x04000F5B RID: 3931
		protected SafeRpcAsyncStateHandle m_pAsyncState;

		// Token: 0x04000F5C RID: 3932
		protected unsafe uint* m_pcbAuxOut;

		// Token: 0x04000F5D RID: 3933
		protected unsafe byte** m_ppbAuxOut;
	}
}
