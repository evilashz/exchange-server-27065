using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F5 RID: 757
	internal class ClientAsyncCallState_DNToEph : ClientAsyncCallState
	{
		// Token: 0x06000D95 RID: 3477 RVA: 0x00034218 File Offset: 0x00033618
		private void Cleanup()
		{
			SafeStringArrayHandle stringsHandle = this.m_stringsHandle;
			if (stringsHandle != null)
			{
				((IDisposable)stringsHandle).Dispose();
				this.m_stringsHandle = null;
			}
			SafeRpcMemoryHandle midsOutHandle = this.m_midsOutHandle;
			if (midsOutHandle != null)
			{
				((IDisposable)midsOutHandle).Dispose();
				this.m_midsOutHandle = null;
			}
			if (this.m_ppmids != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ppmids);
				this.m_ppmids = IntPtr.Zero;
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00034F68 File Offset: 0x00034368
		public ClientAsyncCallState_DNToEph(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle, NspiDNToEphFlags flags, string[] DNs) : base("DNToEph", asyncCallback, asyncState)
		{
			try
			{
				this.m_contextHandle = contextHandle;
				this.m_flags = flags;
				this.m_stringsHandle = null;
				this.m_midsOutHandle = null;
				this.m_ppmids = IntPtr.Zero;
				bool flag = false;
				try
				{
					this.m_stringsHandle = new SafeStringArrayHandle(DNs, true);
					IntPtr ppmids = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_ppmids = ppmids;
					Marshal.WriteIntPtr(this.m_ppmids, IntPtr.Zero);
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

		// Token: 0x06000D97 RID: 3479 RVA: 0x00034288 File Offset: 0x00033688
		private void ~ClientAsyncCallState_DNToEph()
		{
			this.Cleanup();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003429C File Offset: 0x0003369C
		public unsafe override void InternalBegin()
		{
			SafeStringArrayHandle stringsHandle = this.m_stringsHandle;
			void* ptr;
			if (stringsHandle != null)
			{
				ptr = stringsHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr = null;
			}
			<Module>.cli_NspiDNToEph((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_contextHandle.ToPointer(), this.m_flags, (_StringsArray*)ptr, (_SPropTagArray_r**)this.m_ppmids.ToPointer());
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00035030 File Offset: 0x00034430
		public NspiStatus End(out int[] mids)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = (NspiStatus)base.CheckCompletion();
				SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(Marshal.ReadIntPtr(this.m_ppmids));
				this.m_midsOutHandle = safeRpcMemoryHandle;
				IntPtr pPropTagArray = safeRpcMemoryHandle.DangerousGetHandle();
				mids = MarshalHelper.ConvertSPropTagArrayToIntArray(pPropTagArray);
				result = nspiStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00035098 File Offset: 0x00034498
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

		// Token: 0x04000DE5 RID: 3557
		private IntPtr m_contextHandle;

		// Token: 0x04000DE6 RID: 3558
		private NspiDNToEphFlags m_flags;

		// Token: 0x04000DE7 RID: 3559
		private SafeStringArrayHandle m_stringsHandle;

		// Token: 0x04000DE8 RID: 3560
		private SafeRpcMemoryHandle m_midsOutHandle;

		// Token: 0x04000DE9 RID: 3561
		private IntPtr m_ppmids;
	}
}
