using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.AdminRpc
{
	// Token: 0x020001BE RID: 446
	internal sealed class EcListAllMdbStatus50AsyncResult : RpcAsyncResult
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00014370 File Offset: 0x00013770
		public unsafe EcListAllMdbStatus50AsyncResult(AsyncCallback callback, object asyncState) : base(callback, asyncState)
		{
			try
			{
				EcListAllMdbStatus50AsyncState* ptr = <Module>.@new(144UL);
				EcListAllMdbStatus50AsyncState* ptr2;
				try
				{
					if (ptr != null)
					{
						*(int*)(ptr + 112L / (long)sizeof(EcListAllMdbStatus50AsyncState)) = 0;
						*(int*)(ptr + 116L / (long)sizeof(EcListAllMdbStatus50AsyncState)) = 0;
						*(long*)(ptr + 120L / (long)sizeof(EcListAllMdbStatus50AsyncState)) = 0L;
						*(int*)(ptr + 128L / (long)sizeof(EcListAllMdbStatus50AsyncState)) = 0;
						*(long*)(ptr + 136L / (long)sizeof(EcListAllMdbStatus50AsyncState)) = 0L;
						ptr2 = ptr;
					}
					else
					{
						ptr2 = 0L;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr);
					throw;
				}
				this.m_pAsyncState = ptr2;
				if (0L == ptr2)
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

		// Token: 0x0600094B RID: 2379 RVA: 0x00014420 File Offset: 0x00013820
		private unsafe void ~EcListAllMdbStatus50AsyncResult()
		{
			EcListAllMdbStatus50AsyncState* pAsyncState = this.m_pAsyncState;
			if (pAsyncState != null)
			{
				<Module>.EcListAllMdbStatus50AsyncState.__delDtor(pAsyncState, 1U);
			}
			this.m_pAsyncState = null;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00014448 File Offset: 0x00013848
		public unsafe int Complete(out uint databaseCount, out byte[] mdbStatus, out byte[] auxOut)
		{
			if (null == this.m_pAsyncState)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			try
			{
				databaseCount = 0U;
				mdbStatus = null;
				auxOut = null;
				int num2 = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.m_pAsyncState, (void*)(&num));
				if (num2 != null)
				{
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num2, "EcListAllMdbStatus50, RpcAsyncCompleteCall");
				}
				EcListAllMdbStatus50AsyncState* pAsyncState = this.m_pAsyncState;
				byte[] array;
				if (*(int*)(pAsyncState + 128L / (long)sizeof(EcListAllMdbStatus50AsyncState)) != 0)
				{
					IntPtr uPtrData = new IntPtr(*(long*)(pAsyncState + 136L / (long)sizeof(EcListAllMdbStatus50AsyncState)));
					array = <Module>.UToMBytes(*(int*)(this.m_pAsyncState + 128L / (long)sizeof(EcListAllMdbStatus50AsyncState)), uPtrData);
				}
				else
				{
					array = null;
				}
				auxOut = array;
				if (0 == num)
				{
					databaseCount = (uint)(*(int*)(this.m_pAsyncState + 112L / (long)sizeof(EcListAllMdbStatus50AsyncState)));
					EcListAllMdbStatus50AsyncState* pAsyncState2 = this.m_pAsyncState;
					byte[] array2;
					if (*(int*)(pAsyncState2 + 116L / (long)sizeof(EcListAllMdbStatus50AsyncState)) != 0)
					{
						IntPtr uPtrData2 = new IntPtr(*(long*)(pAsyncState2 + 120L / (long)sizeof(EcListAllMdbStatus50AsyncState)));
						array2 = <Module>.UToMBytes(*(int*)(this.m_pAsyncState + 116L / (long)sizeof(EcListAllMdbStatus50AsyncState)), uPtrData2);
					}
					else
					{
						array2 = null;
					}
					mdbStatus = array2;
				}
			}
			finally
			{
				EcListAllMdbStatus50AsyncState* pAsyncState3 = this.m_pAsyncState;
				if (pAsyncState3 != null)
				{
					<Module>.EcListAllMdbStatus50AsyncState.__delDtor(pAsyncState3, 1U);
				}
				this.m_pAsyncState = null;
			}
			return num;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00011D0C File Offset: 0x0001110C
		public unsafe sealed override void Cancel()
		{
			<Module>.RpcAsyncCancelCall((_RPC_ASYNC_STATE*)this.m_pAsyncState, 1);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00011D28 File Offset: 0x00011128
		public unsafe EcListAllMdbStatus50AsyncState* NativeState()
		{
			return this.m_pAsyncState;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00014558 File Offset: 0x00013958
		[HandleProcessCorruptedStateExceptions]
		protected sealed override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.~EcListAllMdbStatus50AsyncResult();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000B6A RID: 2922
		private unsafe EcListAllMdbStatus50AsyncState* m_pAsyncState;
	}
}
