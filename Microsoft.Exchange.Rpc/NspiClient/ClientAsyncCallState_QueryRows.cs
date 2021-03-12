using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F4 RID: 756
	internal class ClientAsyncCallState_QueryRows : ClientAsyncCallState
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x000340B8 File Offset: 0x000334B8
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
			SafeSRowSetHandle rowsetOutHandle = this.m_rowsetOutHandle;
			if (rowsetOutHandle != null)
			{
				((IDisposable)rowsetOutHandle).Dispose();
				this.m_rowsetOutHandle = null;
			}
			if (this.m_pmids != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pmids);
				this.m_pmids = IntPtr.Zero;
			}
			if (this.m_ppRowSet != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ppRowSet);
				this.m_ppRowSet = IntPtr.Zero;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00034D70 File Offset: 0x00034170
		public ClientAsyncCallState_QueryRows(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle, NspiQueryRowsFlags flags, NspiState state, int[] mids, int count, PropertyTag[] propertyTags) : base("QueryRows", asyncCallback, asyncState)
		{
			try
			{
				this.m_contextHandle = contextHandle;
				this.m_flags = flags;
				this.m_ulCount = count;
				this.m_pmids = IntPtr.Zero;
				this.m_cmids = 0;
				this.m_propTagsHandle = null;
				this.m_stateHandle = null;
				this.m_rowsetOutHandle = null;
				this.m_ppRowSet = IntPtr.Zero;
				bool flag = false;
				try
				{
					this.m_stateHandle = NspiHelper.ConvertNspiStateToNative(state);
					if (propertyTags != null)
					{
						this.m_propTagsHandle = MarshalHelper.ConvertPropertyTagArrayToSPropTagArray(propertyTags);
					}
					IntPtr ppRowSet = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_ppRowSet = ppRowSet;
					Marshal.WriteIntPtr(this.m_ppRowSet, IntPtr.Zero);
					if (mids != null)
					{
						int num = mids.Length;
						if (num > 0)
						{
							this.m_cmids = num;
							IntPtr pmids = Marshal.AllocHGlobal((int)((long)num * 4L));
							this.m_pmids = pmids;
							Marshal.Copy(mids, 0, this.m_pmids, this.m_cmids);
						}
					}
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

		// Token: 0x06000D91 RID: 3473 RVA: 0x00034170 File Offset: 0x00033570
		private void ~ClientAsyncCallState_QueryRows()
		{
			this.Cleanup();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00034184 File Offset: 0x00033584
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
			<Module>.cli_NspiQueryRows((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_contextHandle.ToPointer(), this.m_flags, (__MIDL_nspi_0002*)ptr2, this.m_cmids, (uint*)this.m_pmids.ToPointer(), this.m_ulCount, (_SPropTagArray_r*)ptr, (_SRowSet_r**)this.m_ppRowSet.ToPointer());
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00034EA8 File Offset: 0x000342A8
		public NspiStatus End(out NspiState state, out PropertyValue[][] rowset)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = (NspiStatus)base.CheckCompletion();
				state = NspiHelper.ConvertNspiStateFromNative(this.m_stateHandle);
				SafeSRowSetHandle safeSRowSetHandle = new SafeSRowSetHandle(Marshal.ReadIntPtr(this.m_ppRowSet));
				this.m_rowsetOutHandle = safeSRowSetHandle;
				IntPtr pRowSet = safeSRowSetHandle.DangerousGetHandle();
				rowset = MarshalHelper.ConvertSRowSetToPropertyValueArrays(pRowSet, MarshalHelper.GetString8CodePage(state));
				result = nspiStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00034F24 File Offset: 0x00034324
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

		// Token: 0x04000DDC RID: 3548
		private IntPtr m_contextHandle;

		// Token: 0x04000DDD RID: 3549
		private NspiQueryRowsFlags m_flags;

		// Token: 0x04000DDE RID: 3550
		private int m_ulCount;

		// Token: 0x04000DDF RID: 3551
		private int m_cmids;

		// Token: 0x04000DE0 RID: 3552
		private IntPtr m_pmids;

		// Token: 0x04000DE1 RID: 3553
		private SafeRpcMemoryHandle m_propTagsHandle;

		// Token: 0x04000DE2 RID: 3554
		private SafeRpcMemoryHandle m_stateHandle;

		// Token: 0x04000DE3 RID: 3555
		private SafeSRowSetHandle m_rowsetOutHandle;

		// Token: 0x04000DE4 RID: 3556
		private IntPtr m_ppRowSet;
	}
}
