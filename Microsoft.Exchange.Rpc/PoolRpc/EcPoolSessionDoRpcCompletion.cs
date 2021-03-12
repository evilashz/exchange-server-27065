using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x02000390 RID: 912
	internal class EcPoolSessionDoRpcCompletion : AsyncRpcCompletionBase, IPoolSessionDoRpcCompletion
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x00048A28 File Offset: 0x00047E28
		public unsafe EcPoolSessionDoRpcCompletion(SafeRpcAsyncStateHandle pAsyncState, uint* pulFlags, uint* pcbOut, byte** ppbOut, uint* pcbAuxOut, byte** ppbAuxOut)
		{
			this.m_pAsyncState = pAsyncState;
			this.m_pcbAuxOut = pcbAuxOut;
			this.m_ppbAuxOut = ppbAuxOut;
			this.m_pulFlags = pulFlags;
			this.m_pcbOut = pcbOut;
			this.m_ppbOut = ppbOut;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00048A68 File Offset: 0x00047E68
		public unsafe virtual void CompleteAsyncCall(uint flags, ArraySegment<byte> response, ArraySegment<byte> auxiliaryOut)
		{
			bool flag = false;
			bool flag2 = true;
			int num = 0;
			int num2 = 0;
			byte* ptr = null;
			int num3 = 0;
			byte* ptr2 = null;
			IntPtr intPtr = IntPtr.Zero;
			if (!this.m_pAsyncState.IsInvalid)
			{
				try
				{
					<Module>.MToUBytesSegment(auxiliaryOut, (int*)(&num3), &ptr2);
					<Module>.MToUBytesSegment(response, (int*)(&num2), &ptr);
					*(int*)this.m_pulFlags = (int)flags;
					IntPtr intPtr2 = this.m_pAsyncState.Detach();
					intPtr = intPtr2;
					if (IntPtr.Zero != intPtr2)
					{
						*(int*)this.m_pulFlags = (int)flags;
						*(int*)this.m_pcbOut = num2;
						*(long*)this.m_ppbOut = ptr;
						ptr = null;
						*(int*)this.m_pcbAuxOut = num3;
						*(long*)this.m_ppbAuxOut = ptr2;
						ptr2 = null;
						<Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)intPtr.ToPointer(), (void*)(&num));
					}
					flag = true;
					flag2 = false;
				}
				finally
				{
					if (ptr != null)
					{
						<Module>.MIDL_user_free((void*)ptr);
					}
					if (ptr2 != null)
					{
						<Module>.MIDL_user_free((void*)ptr2);
					}
					if (flag2)
					{
						this.AbortAsyncCall(1726U);
					}
					else if (!flag)
					{
						int errorCode = (0 != num) ? num : -2147467259;
						this.FailAsyncCall(errorCode, auxiliaryOut);
					}
				}
			}
		}

		// Token: 0x04000F67 RID: 3943
		private unsafe uint* m_pulFlags;

		// Token: 0x04000F68 RID: 3944
		private unsafe uint* m_pcbOut;

		// Token: 0x04000F69 RID: 3945
		private unsafe byte** m_ppbOut;
	}
}
