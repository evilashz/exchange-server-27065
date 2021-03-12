﻿using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200036E RID: 878
	internal sealed class EcPoolWaitForNotificationsAsyncResult : RpcAsyncResult
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x00047058 File Offset: 0x00046458
		public unsafe EcPoolWaitForNotificationsAsyncResult(AsyncCallback callback, object asyncState) : base(callback, asyncState)
		{
			try
			{
				EcPoolWaitForNotificationsAsyncState* ptr = <Module>.@new(128UL);
				EcPoolWaitForNotificationsAsyncState* ptr2;
				try
				{
					if (ptr != null)
					{
						*(int*)(ptr + 112L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)) = 0;
						*(long*)(ptr + 120L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)) = 0L;
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

		// Token: 0x06000FDD RID: 4061 RVA: 0x000470EC File Offset: 0x000464EC
		private unsafe void ~EcPoolWaitForNotificationsAsyncResult()
		{
			EcPoolWaitForNotificationsAsyncState* pAsyncState = this.m_pAsyncState;
			if (pAsyncState != null)
			{
				ulong num = (ulong)(*(long*)(pAsyncState + 120L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)));
				if (num != 0UL)
				{
					<Module>.MIDL_user_free(num);
				}
				<Module>.delete((void*)pAsyncState);
			}
			this.m_pAsyncState = null;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00047124 File Offset: 0x00046524
		public unsafe int Complete(out uint[] sessionHandles)
		{
			if (null == this.m_pAsyncState)
			{
				throw new InvalidOperationException();
			}
			int result;
			try
			{
				int num = 0;
				int num2 = 0;
				sessionHandles = null;
				int num3 = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.m_pAsyncState, (void*)(&num));
				if (num3 != null)
				{
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num3, "EcPoolWaitForNotificationsAsync, RpcAsyncCompleteCall");
				}
				EcPoolWaitForNotificationsAsyncState* pAsyncState = this.m_pAsyncState;
				if (0L != *(long*)(pAsyncState + 120L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)) && 0 != *(int*)(pAsyncState + 112L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)))
				{
					for (;;)
					{
						pAsyncState = this.m_pAsyncState;
						uint num4 = (uint)(*(int*)(pAsyncState + 112L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)));
						if (num2 >= num4)
						{
							break;
						}
						tagRPC_BLOCK_HEADER tagRPC_BLOCK_HEADER = 0;
						initblk(ref tagRPC_BLOCK_HEADER + 2, 0, 2L);
						num = <Module>.EcUnpackData<struct\u0020tagRPC_BLOCK_HEADER>(*(long*)(pAsyncState + 120L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)), num4, &num2, &tagRPC_BLOCK_HEADER);
						if (num != null)
						{
							break;
						}
						pAsyncState = this.m_pAsyncState;
						if (*(int*)(pAsyncState + 112L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)) - num2 < tagRPC_BLOCK_HEADER)
						{
							break;
						}
						if (5 == *(ref tagRPC_BLOCK_HEADER + 2))
						{
							tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER = 0;
							initblk(ref tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER + 4, 0, 4L);
							byte* ptr = num2 + *(long*)(pAsyncState + 120L / (long)sizeof(EcPoolWaitForNotificationsAsyncState));
							int num5 = 0;
							num = <Module>.EcUnpackData<struct\u0020tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER>(ptr, tagRPC_BLOCK_HEADER, &num5, &tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER);
							if (num != null)
							{
								break;
							}
							if ((ulong)(*(ref tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER + 4)) <= (ulong)(tagRPC_BLOCK_HEADER - num5) >> 2)
							{
								sessionHandles = new uint[*(ref tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER + 4)];
								for (uint num6 = 0; num6 < *(ref tagRPC_POOL_WAIT_FOR_NOTIFICATIONS_ASYNC_RESPONSE_HEADER + 4); num6++)
								{
									uint num7 = 0;
									num = <Module>.EcUnpackData<unsigned\u0020long>(ptr, tagRPC_BLOCK_HEADER, &num5, &num7);
									if (num != null)
									{
										goto IL_140;
									}
									sessionHandles[num6] = num7;
								}
							}
						}
						num2 += tagRPC_BLOCK_HEADER;
					}
				}
				IL_140:
				result = num;
			}
			finally
			{
				EcPoolWaitForNotificationsAsyncState* pAsyncState2 = this.m_pAsyncState;
				if (pAsyncState2 != null)
				{
					ulong num8 = (ulong)(*(long*)(pAsyncState2 + 120L / (long)sizeof(EcPoolWaitForNotificationsAsyncState)));
					if (num8 != 0UL)
					{
						<Module>.MIDL_user_free(num8);
					}
					<Module>.delete((void*)pAsyncState2);
				}
				this.m_pAsyncState = null;
			}
			return result;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00046744 File Offset: 0x00045B44
		public unsafe sealed override void Cancel()
		{
			<Module>.RpcAsyncCancelCall((_RPC_ASYNC_STATE*)this.m_pAsyncState, 1);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00046760 File Offset: 0x00045B60
		public unsafe EcPoolWaitForNotificationsAsyncState* NativeState()
		{
			return this.m_pAsyncState;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000472C8 File Offset: 0x000466C8
		[HandleProcessCorruptedStateExceptions]
		protected sealed override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.~EcPoolWaitForNotificationsAsyncResult();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000F52 RID: 3922
		private unsafe EcPoolWaitForNotificationsAsyncState* m_pAsyncState;
	}
}
