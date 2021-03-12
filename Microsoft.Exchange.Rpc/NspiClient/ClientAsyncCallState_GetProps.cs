using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F6 RID: 758
	internal class ClientAsyncCallState_GetProps : ClientAsyncCallState
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x000342F8 File Offset: 0x000336F8
		private void Cleanup()
		{
			SafeRpcMemoryHandle propTagsHandle = this.m_propTagsHandle;
			if (propTagsHandle != null)
			{
				((IDisposable)propTagsHandle).Dispose();
				this.m_propTagsHandle = null;
			}
			SafeRpcMemoryHandle stateHandle = this.m_stateHandle;
			if (stateHandle != null)
			{
				((IDisposable)stateHandle).Dispose();
				this.m_stateHandle = null;
			}
			SafeSRowHandle rowOutHandle = this.m_rowOutHandle;
			if (rowOutHandle != null)
			{
				((IDisposable)rowOutHandle).Dispose();
				this.m_rowOutHandle = null;
			}
			if (this.m_ppRow != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ppRow);
				this.m_ppRow = IntPtr.Zero;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000350DC File Offset: 0x000344DC
		public ClientAsyncCallState_GetProps(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle, NspiGetPropsFlags flags, NspiState state, PropertyTag[] propertyTags) : base("GetProps", asyncCallback, asyncState)
		{
			try
			{
				this.m_contextHandle = contextHandle;
				this.m_flags = flags;
				this.m_propTagsHandle = null;
				this.m_stateHandle = null;
				this.m_rowOutHandle = null;
				this.m_ppRow = IntPtr.Zero;
				bool flag = false;
				try
				{
					this.m_codePage = MarshalHelper.GetString8CodePage(state);
					this.m_stateHandle = NspiHelper.ConvertNspiStateToNative(state);
					if (propertyTags != null)
					{
						this.m_propTagsHandle = MarshalHelper.ConvertPropertyTagArrayToSPropTagArray(propertyTags);
					}
					IntPtr ppRow = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_ppRow = ppRow;
					Marshal.WriteIntPtr(this.m_ppRow, IntPtr.Zero);
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

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003437C File Offset: 0x0003377C
		private void ~ClientAsyncCallState_GetProps()
		{
			this.Cleanup();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00034390 File Offset: 0x00033790
		public unsafe override void InternalBegin()
		{
			SafeRpcMemoryHandle propTagsHandle = this.m_propTagsHandle;
			void* ptr;
			if (propTagsHandle != null)
			{
				ptr = propTagsHandle.DangerousGetHandle().ToPointer();
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
			<Module>.cli_NspiGetProps((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_contextHandle.ToPointer(), this.m_flags, (__MIDL_nspi_0002*)ptr2, (_SPropTagArray_r*)ptr, (_SRow_r**)this.m_ppRow.ToPointer());
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000351C8 File Offset: 0x000345C8
		public NspiStatus End(out int codePage, [Out] PropertyValue[] row)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = (NspiStatus)base.CheckCompletion();
				codePage = this.m_codePage;
				SafeSRowHandle safeSRowHandle = new SafeSRowHandle(Marshal.ReadIntPtr(this.m_ppRow));
				this.m_rowOutHandle = safeSRowHandle;
				row = MarshalHelper.ConvertSRowToPropertyValueArray(safeSRowHandle.DangerousGetHandle(), this.m_codePage);
				result = nspiStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003523C File Offset: 0x0003463C
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

		// Token: 0x04000DEA RID: 3562
		private IntPtr m_contextHandle;

		// Token: 0x04000DEB RID: 3563
		private NspiGetPropsFlags m_flags;

		// Token: 0x04000DEC RID: 3564
		private SafeRpcMemoryHandle m_propTagsHandle;

		// Token: 0x04000DED RID: 3565
		private SafeRpcMemoryHandle m_stateHandle;

		// Token: 0x04000DEE RID: 3566
		private SafeSRowHandle m_rowOutHandle;

		// Token: 0x04000DEF RID: 3567
		private IntPtr m_ppRow;

		// Token: 0x04000DF0 RID: 3568
		private int m_codePage;
	}
}
