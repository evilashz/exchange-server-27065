using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002F3 RID: 755
	internal class ClientAsyncCallState_GetMatches : ClientAsyncCallState
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x00033EB4 File Offset: 0x000332B4
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
			SafeRpcMemoryHandle midsHandle = this.m_midsHandle;
			if (midsHandle != null)
			{
				((IDisposable)midsHandle).Dispose();
				this.m_midsHandle = null;
			}
			SafeRpcMemoryHandle restrictionHandle = this.m_restrictionHandle;
			if (restrictionHandle != null)
			{
				((IDisposable)restrictionHandle).Dispose();
				this.m_restrictionHandle = null;
			}
			SafeSRowSetHandle rowsetOutHandle = this.m_rowsetOutHandle;
			if (rowsetOutHandle != null)
			{
				((IDisposable)rowsetOutHandle).Dispose();
				this.m_rowsetOutHandle = null;
			}
			SafeRpcMemoryHandle midsOutHandle = this.m_midsOutHandle;
			if (midsOutHandle != null)
			{
				((IDisposable)midsOutHandle).Dispose();
				this.m_midsOutHandle = null;
			}
			if (this.m_ppRowSet != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ppRowSet);
				this.m_ppRowSet = IntPtr.Zero;
			}
			if (this.m_ppMids != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ppMids);
				this.m_ppMids = IntPtr.Zero;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00035484 File Offset: 0x00034884
		public ClientAsyncCallState_GetMatches(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr contextHandle, NspiGetMatchesFlags flags, NspiState state, int[] mids, int interfaceOptions, Restriction restriction, IntPtr propName, int maxRows, PropertyTag[] propertyTags) : base("GetMatches", asyncCallback, asyncState)
		{
			try
			{
				this.m_contextHandle = contextHandle;
				this.m_flags = flags;
				this.m_interfaceOptions = interfaceOptions;
				this.m_pPropName = propName;
				this.m_maxRows = maxRows;
				this.m_midsHandle = null;
				this.m_propTagsHandle = null;
				this.m_stateHandle = null;
				this.m_restrictionHandle = null;
				this.m_midsOutHandle = null;
				this.m_rowsetOutHandle = null;
				this.m_ppRowSet = IntPtr.Zero;
				this.m_ppMids = IntPtr.Zero;
				bool flag = false;
				try
				{
					this.m_stateHandle = NspiHelper.ConvertNspiStateToNative(state);
					if (mids != null)
					{
						SafeRpcMemoryHandle midsHandle = NspiHelper.ConvertIntArrayToPropTagArray(mids, false);
						this.m_midsHandle = midsHandle;
					}
					if (propertyTags != null)
					{
						this.m_propTagsHandle = MarshalHelper.ConvertPropertyTagArrayToSPropTagArray(propertyTags);
					}
					if (restriction != null)
					{
						this.m_restrictionHandle = MarshalHelper.ConvertRestrictionToSRestriction(restriction, MarshalHelper.GetString8CodePage(state));
					}
					IntPtr ppRowSet = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_ppRowSet = ppRowSet;
					Marshal.WriteIntPtr(this.m_ppRowSet, IntPtr.Zero);
					IntPtr ppMids = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_ppMids = ppMids;
					Marshal.WriteIntPtr(this.m_ppMids, IntPtr.Zero);
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

		// Token: 0x06000D8B RID: 3467 RVA: 0x00033FB8 File Offset: 0x000333B8
		private void ~ClientAsyncCallState_GetMatches()
		{
			this.Cleanup();
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00033FCC File Offset: 0x000333CC
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
			SafeRpcMemoryHandle restrictionHandle = this.m_restrictionHandle;
			void* ptr2;
			if (restrictionHandle != null)
			{
				ptr2 = restrictionHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr2 = null;
			}
			SafeRpcMemoryHandle midsHandle = this.m_midsHandle;
			void* ptr3;
			if (midsHandle != null)
			{
				ptr3 = midsHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr3 = null;
			}
			SafeRpcMemoryHandle stateHandle = this.m_stateHandle;
			void* ptr4;
			if (stateHandle != null)
			{
				ptr4 = stateHandle.DangerousGetHandle().ToPointer();
			}
			else
			{
				ptr4 = null;
			}
			<Module>.cli_NspiGetMatches((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_contextHandle.ToPointer(), this.m_flags, (__MIDL_nspi_0002*)ptr4, (_SPropTagArray_r*)ptr3, this.m_interfaceOptions, (_SRestriction_r*)ptr2, (_MAPINAMEID_r*)this.m_pPropName.ToPointer(), this.m_maxRows, (_SPropTagArray_r**)this.m_ppMids.ToPointer(), (_SPropTagArray_r*)ptr, (_SRowSet_r**)this.m_ppRowSet.ToPointer());
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00034C80 File Offset: 0x00034080
		public NspiStatus End(out NspiState state, out int[] mids, out PropertyValue[][] rowset)
		{
			NspiStatus result;
			try
			{
				NspiStatus nspiStatus = (NspiStatus)base.CheckCompletion();
				state = NspiHelper.ConvertNspiStateFromNative(this.m_stateHandle);
				SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(Marshal.ReadIntPtr(this.m_ppMids));
				this.m_midsOutHandle = safeRpcMemoryHandle;
				IntPtr pPropTagArray = safeRpcMemoryHandle.DangerousGetHandle();
				mids = MarshalHelper.ConvertSPropTagArrayToIntArray(pPropTagArray);
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

		// Token: 0x06000D8E RID: 3470 RVA: 0x00034D2C File Offset: 0x0003412C
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

		// Token: 0x04000DCF RID: 3535
		private IntPtr m_contextHandle;

		// Token: 0x04000DD0 RID: 3536
		private NspiGetMatchesFlags m_flags;

		// Token: 0x04000DD1 RID: 3537
		private int m_interfaceOptions;

		// Token: 0x04000DD2 RID: 3538
		private IntPtr m_pPropName;

		// Token: 0x04000DD3 RID: 3539
		private int m_maxRows;

		// Token: 0x04000DD4 RID: 3540
		private SafeRpcMemoryHandle m_midsHandle;

		// Token: 0x04000DD5 RID: 3541
		private SafeRpcMemoryHandle m_propTagsHandle;

		// Token: 0x04000DD6 RID: 3542
		private SafeRpcMemoryHandle m_stateHandle;

		// Token: 0x04000DD7 RID: 3543
		private SafeRpcMemoryHandle m_restrictionHandle;

		// Token: 0x04000DD8 RID: 3544
		private SafeRpcMemoryHandle m_midsOutHandle;

		// Token: 0x04000DD9 RID: 3545
		private SafeSRowSetHandle m_rowsetOutHandle;

		// Token: 0x04000DDA RID: 3546
		private IntPtr m_ppRowSet;

		// Token: 0x04000DDB RID: 3547
		private IntPtr m_ppMids;
	}
}
