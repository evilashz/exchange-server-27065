using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F2 RID: 754
	internal class ClientAsyncCallState_GetHierarchyInfo : ClientAsyncCallState
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x00033D98 File Offset: 0x00033198
		private void Cleanup()
		{
			SafeRpcMemoryHandle stateHandle = this.m_stateHandle;
			if (stateHandle != null)
			{
				((IDisposable)stateHandle).Dispose();
				this.m_stateHandle = null;
			}
			SafeSRowSetHandle rowsetOutHandle = this.m_rowsetOutHandle;
			if (rowsetOutHandle != null)
			{
				((IDisposable)rowsetOutHandle).Dispose();
				this.m_rowsetOutHandle = null;
			}
			if (this.m_pdwVersion != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pdwVersion);
				this.m_pdwVersion = IntPtr.Zero;
			}
			if (this.m_ppRowSet != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ppRowSet);
				this.m_ppRowSet = IntPtr.Zero;
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00034AB4 File Offset: 0x00033EB4
		public ClientAsyncCallState_GetHierarchyInfo(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle, NspiGetHierarchyInfoFlags flags, NspiState state, int version) : base("GetHierarchyInfo", asyncCallback, asyncState)
		{
			try
			{
				this.m_contextHandle = contextHandle;
				this.m_flags = flags;
				this.m_stateHandle = null;
				this.m_rowsetOutHandle = null;
				this.m_pdwVersion = IntPtr.Zero;
				this.m_ppRowSet = IntPtr.Zero;
				bool flag = false;
				try
				{
					this.m_codePage = MarshalHelper.GetString8CodePage(state);
					this.m_stateHandle = NspiHelper.ConvertNspiStateToNative(state);
					IntPtr pdwVersion = Marshal.AllocHGlobal(4);
					this.m_pdwVersion = pdwVersion;
					Marshal.WriteInt32(this.m_pdwVersion, version);
					IntPtr ppRowSet = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_ppRowSet = ppRowSet;
					Marshal.WriteIntPtr(this.m_ppRowSet, IntPtr.Zero);
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

		// Token: 0x06000D85 RID: 3461 RVA: 0x00033E38 File Offset: 0x00033238
		private void ~ClientAsyncCallState_GetHierarchyInfo()
		{
			this.Cleanup();
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00033E4C File Offset: 0x0003324C
		public unsafe override void InternalBegin()
		{
			SafeRpcMemoryHandle stateHandle = this.m_stateHandle;
			void* ptr;
			if (stateHandle != null)
			{
				ptr = stateHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr = null;
			}
			<Module>.cli_NspiGetHierarchyInfo((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_contextHandle.ToPointer(), this.m_flags, (__MIDL_nspi_0002*)ptr, (uint*)this.m_pdwVersion.ToPointer(), (_SRowSet_r**)this.m_ppRowSet.ToPointer());
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00034BB4 File Offset: 0x00033FB4
		public NspiStatus End(out int codePage, out int version, out PropertyValue[][] rowset)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = (NspiStatus)base.CheckCompletion();
				codePage = this.m_codePage;
				version = Marshal.ReadInt32(this.m_pdwVersion);
				SafeSRowSetHandle safeSRowSetHandle = new SafeSRowSetHandle(Marshal.ReadIntPtr(this.m_ppRowSet));
				this.m_rowsetOutHandle = safeSRowSetHandle;
				IntPtr pRowSet = safeSRowSetHandle.DangerousGetHandle();
				rowset = MarshalHelper.ConvertSRowSetToPropertyValueArrays(pRowSet, this.m_codePage);
				result = nspiStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00034C3C File Offset: 0x0003403C
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

		// Token: 0x04000DC8 RID: 3528
		private IntPtr m_contextHandle;

		// Token: 0x04000DC9 RID: 3529
		private NspiGetHierarchyInfoFlags m_flags;

		// Token: 0x04000DCA RID: 3530
		private SafeRpcMemoryHandle m_stateHandle;

		// Token: 0x04000DCB RID: 3531
		private SafeSRowSetHandle m_rowsetOutHandle;

		// Token: 0x04000DCC RID: 3532
		private IntPtr m_pdwVersion;

		// Token: 0x04000DCD RID: 3533
		private IntPtr m_ppRowSet;

		// Token: 0x04000DCE RID: 3534
		private int m_codePage;
	}
}
