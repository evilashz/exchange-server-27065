using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F1 RID: 753
	internal class ClientAsyncCallState_Unbind : ClientAsyncCallState
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00033D18 File Offset: 0x00033118
		private unsafe void Cleanup()
		{
			if (this.m_pContextHandle != IntPtr.Zero)
			{
				if (Marshal.ReadIntPtr(this.m_pContextHandle) != IntPtr.Zero)
				{
					<Module>.RpcSsDestroyClientContext((void**)this.m_pContextHandle.ToPointer());
				}
				Marshal.FreeHGlobal(this.m_pContextHandle);
				this.m_pContextHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000348EC File Offset: 0x00033CEC
		public ClientAsyncCallState_Unbind(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle, NspiUnbindFlags flags) : base("Unbind", asyncCallback, asyncState)
		{
			try
			{
				this.m_pContextHandle = IntPtr.Zero;
				this.m_flags = flags;
				this.isContextHandleValid = true;
				bool flag = false;
				try
				{
					IntPtr pContextHandle = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_pContextHandle = pContextHandle;
					Marshal.WriteIntPtr(this.m_pContextHandle, contextHandle);
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

		// Token: 0x06000D7F RID: 3455 RVA: 0x00033D84 File Offset: 0x00033184
		private void ~ClientAsyncCallState_Unbind()
		{
			this.Cleanup();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00034994 File Offset: 0x00033D94
		[HandleProcessCorruptedStateExceptions]
		public unsafe override void InternalBegin()
		{
			int num;
			try
			{
				<Module>.cli_NspiUnbind((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), (void**)this.m_pContextHandle.ToPointer(), this.m_flags);
			}
			catch when (delegate
			{
				// Failed to create a 'catch-when' expression
				num = ((Marshal.GetExceptionCode() == 6) ? 1 : 0);
				endfilter(num != 0);
			})
			{
				this.isContextHandleValid = false;
				base.Completion();
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00034A00 File Offset: 0x00033E00
		public NspiStatus End(out IntPtr contextHandle)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = NspiStatus.Success;
				if (this.isContextHandleValid)
				{
					nspiStatus = (NspiStatus)base.CheckCompletion();
					IntPtr intPtr = Marshal.ReadIntPtr(this.m_pContextHandle);
					contextHandle = intPtr;
				}
				else
				{
					contextHandle = IntPtr.Zero;
				}
				result = nspiStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00034A70 File Offset: 0x00033E70
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

		// Token: 0x04000DC5 RID: 3525
		private IntPtr m_pContextHandle;

		// Token: 0x04000DC6 RID: 3526
		private NspiUnbindFlags m_flags;

		// Token: 0x04000DC7 RID: 3527
		private bool isContextHandleValid;
	}
}
