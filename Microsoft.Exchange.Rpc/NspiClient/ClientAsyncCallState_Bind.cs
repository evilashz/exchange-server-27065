using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F0 RID: 752
	internal class ClientAsyncCallState_Bind : ClientAsyncCallState
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00033C14 File Offset: 0x00033014
		private void Cleanup()
		{
			SafeRpcMemoryHandle stateHandle = this.m_stateHandle;
			if (stateHandle != null)
			{
				((IDisposable)stateHandle).Dispose();
				this.m_stateHandle = null;
			}
			SafeRpcMemoryHandle guidHandle = this.m_guidHandle;
			if (guidHandle != null)
			{
				((IDisposable)guidHandle).Dispose();
				this.m_guidHandle = null;
			}
			if (this.m_pContextHandle != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pContextHandle);
				this.m_pContextHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00034738 File Offset: 0x00033B38
		public ClientAsyncCallState_Bind(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr bindingHandle, NspiBindFlags flags, NspiState state, Guid? guid) : base("Bind", asyncCallback, asyncState)
		{
			try
			{
				this.m_pRpcBindingHandle = bindingHandle;
				this.m_pContextHandle = IntPtr.Zero;
				this.m_flags = flags;
				this.m_stateHandle = null;
				this.m_guidHandle = null;
				bool flag = false;
				try
				{
					this.m_stateHandle = NspiHelper.ConvertNspiStateToNative(state);
					if (guid != null)
					{
						Guid value = guid.Value;
						this.m_guidHandle = NspiHelper.ConvertGuidToNative(value);
					}
					IntPtr pContextHandle = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_pContextHandle = pContextHandle;
					Marshal.WriteIntPtr(this.m_pContextHandle, IntPtr.Zero);
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

		// Token: 0x06000D79 RID: 3449 RVA: 0x00033C84 File Offset: 0x00033084
		private void ~ClientAsyncCallState_Bind()
		{
			this.Cleanup();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00033C98 File Offset: 0x00033098
		public unsafe override void InternalBegin()
		{
			SafeRpcMemoryHandle guidHandle = this.m_guidHandle;
			void* ptr;
			if (guidHandle != null)
			{
				ptr = guidHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr = null;
			}
			SafeRpcMemoryHandle stateHandle = this.m_stateHandle;
			void* ptr2;
			if (stateHandle != null)
			{
				ptr2 = stateHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr2 = null;
			}
			<Module>.cli_NspiBind((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer(), this.m_flags, (__MIDL_nspi_0002*)ptr2, (__MIDL_nspi_0001*)ptr, (void**)this.m_pContextHandle.ToPointer());
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0003481C File Offset: 0x00033C1C
		public NspiStatus End(out Guid? guid, out IntPtr contextHandle)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = (NspiStatus)base.CheckCompletion();
				guid = null;
				SafeRpcMemoryHandle guidHandle = this.m_guidHandle;
				if (guidHandle != null)
				{
					Guid value = NspiHelper.ConvertGuidFromNative(guidHandle.DangerousGetHandle());
					Guid? guid2 = new Guid?(value);
					guid = guid2;
				}
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pContextHandle);
				contextHandle = intPtr;
				result = nspiStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000348A8 File Offset: 0x00033CA8
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

		// Token: 0x04000DC0 RID: 3520
		private IntPtr m_pRpcBindingHandle;

		// Token: 0x04000DC1 RID: 3521
		private NspiBindFlags m_flags;

		// Token: 0x04000DC2 RID: 3522
		private SafeRpcMemoryHandle m_stateHandle;

		// Token: 0x04000DC3 RID: 3523
		private SafeRpcMemoryHandle m_guidHandle;

		// Token: 0x04000DC4 RID: 3524
		private IntPtr m_pContextHandle;
	}
}
