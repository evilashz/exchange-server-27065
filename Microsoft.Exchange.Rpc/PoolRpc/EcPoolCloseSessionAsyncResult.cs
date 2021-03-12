using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200036C RID: 876
	internal sealed class EcPoolCloseSessionAsyncResult : RpcAsyncResult
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00046D28 File Offset: 0x00046128
		public unsafe EcPoolCloseSessionAsyncResult(AsyncCallback callback, object asyncState) : base(callback, asyncState)
		{
			try
			{
				EcPoolCloseSessionAsyncState* ptr = <Module>.@new(112UL);
				EcPoolCloseSessionAsyncState* ptr2;
				if (ptr != null)
				{
					initblk(ptr, 0, 112L);
					ptr2 = ptr;
				}
				else
				{
					ptr2 = null;
				}
				this.m_pAsyncState = ptr2;
				if (null == ptr2)
				{
					throw new OutOfMemoryException();
				}
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00046648 File Offset: 0x00045A48
		private unsafe void ~EcPoolCloseSessionAsyncResult()
		{
			<Module>.delete((void*)this.m_pAsyncState);
			this.m_pAsyncState = null;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00046668 File Offset: 0x00045A68
		public unsafe int Complete()
		{
			if (null == this.m_pAsyncState)
			{
				throw new InvalidOperationException();
			}
			int result;
			try
			{
				int num = 0;
				int num2 = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.m_pAsyncState, (void*)(&num));
				if (num2 != null)
				{
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num2, "EcPoolCloseSession, RpcAsyncCompleteCall");
				}
				result = num;
			}
			finally
			{
				<Module>.delete((void*)this.m_pAsyncState);
				this.m_pAsyncState = null;
			}
			return result;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000466E0 File Offset: 0x00045AE0
		public unsafe EcPoolCloseSessionAsyncState* NativeState()
		{
			return this.m_pAsyncState;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00046D90 File Offset: 0x00046190
		[HandleProcessCorruptedStateExceptions]
		protected sealed override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.~EcPoolCloseSessionAsyncResult();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000F50 RID: 3920
		private unsafe EcPoolCloseSessionAsyncState* m_pAsyncState;
	}
}
