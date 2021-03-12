using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeClient
{
	// Token: 0x020001EA RID: 490
	internal class ClientAsyncCallState_Disconnect : ClientAsyncCallState
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x0001C224 File Offset: 0x0001B624
		private unsafe void Cleanup()
		{
			if (this.m_pExCXH != IntPtr.Zero)
			{
				if (Marshal.ReadIntPtr(this.m_pExCXH) != IntPtr.Zero)
				{
					<Module>.RpcSsDestroyClientContext((void**)this.m_pExCXH.ToPointer());
				}
				Marshal.FreeHGlobal(this.m_pExCXH);
				this.m_pExCXH = IntPtr.Zero;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001D318 File Offset: 0x0001C718
		public ClientAsyncCallState_Disconnect(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle) : base("Disconnect", asyncCallback, asyncState)
		{
			try
			{
				this.m_pExCXH = IntPtr.Zero;
				this.isContextHandleValid = true;
				bool flag = false;
				try
				{
					IntPtr pExCXH = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_pExCXH = pExCXH;
					Marshal.WriteIntPtr(this.m_pExCXH, contextHandle);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001C290 File Offset: 0x0001B690
		private void ~ClientAsyncCallState_Disconnect()
		{
			this.Cleanup();
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001D3B8 File Offset: 0x0001C7B8
		[HandleProcessCorruptedStateExceptions]
		public unsafe override void InternalBegin()
		{
			int num;
			try
			{
				<Module>.cli_Async_EcDoDisconnect((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), (void**)this.m_pExCXH.ToPointer());
			}
			catch when (delegate
			{
				// Failed to create a 'catch-when' expression
				num = ((Marshal.GetExceptionCode() == 6) ? 1 : 0);
				endfilter(num != 0);
			})
			{
				this.isContextHandleValid = false;
				base.CompleteSynchronously(null);
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001D420 File Offset: 0x0001C820
		public int End(out IntPtr contextHandle)
		{
			int result;
			try
			{
				if (this.isContextHandleValid)
				{
					base.CheckCompletion();
					IntPtr intPtr = Marshal.ReadIntPtr(this.m_pExCXH);
					contextHandle = intPtr;
				}
				else
				{
					contextHandle = IntPtr.Zero;
				}
				result = 0;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001D490 File Offset: 0x0001C890
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.Cleanup();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000BCE RID: 3022
		private IntPtr m_pExCXH;

		// Token: 0x04000BCF RID: 3023
		private bool isContextHandleValid;
	}
}
