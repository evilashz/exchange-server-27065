using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000302 RID: 770
	internal class NspiAsyncRpcState_QueryRows : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_QueryRows,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x00038550 File Offset: 0x00037950
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiQueryRowsFlags flags, IntPtr pState, int midCount, IntPtr pMids, int rowCount, IntPtr pPropTags, IntPtr ppRows)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.midCount = midCount;
			this.pMids = pMids;
			this.rowCount = rowCount;
			this.pPropTags = pPropTags;
			this.ppRows = ppRows;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000359F0 File Offset: 0x00034DF0
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiQueryRowsFlags.None;
			this.pState = IntPtr.Zero;
			this.midCount = 0;
			this.pMids = IntPtr.Zero;
			this.rowCount = 0;
			this.pPropTags = IntPtr.Zero;
			this.ppRows = IntPtr.Zero;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000385A4 File Offset: 0x000379A4
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			int[] mids = null;
			PropertyTag[] propertyTags = null;
			if (this.ppRows != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppRows, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			if (this.pMids != IntPtr.Zero)
			{
				mids = NspiHelper.ConvertIntArrayFromNative(this.pMids, this.midCount);
			}
			else if (this.midCount > 0)
			{
				throw new FailRpcException("Null array with none zero count", -2147024809);
			}
			if (this.pPropTags != IntPtr.Zero)
			{
				propertyTags = MarshalHelper.ConvertSPropTagArrayToPropertyTagArray(this.pPropTags);
			}
			base.AsyncDispatch.BeginQueryRows(null, this.contextHandle, this.flags, state, mids, this.rowCount, propertyTags, asyncCallback, this);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003B238 File Offset: 0x0003A638
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyValue[][] array = null;
			NspiStatus result = NspiStatus.Success;
			NspiState nspiState = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndQueryRows(asyncResult, out nspiState, out array);
				if (this.pState != IntPtr.Zero && nspiState != null)
				{
					nspiState.MarshalToNative(this.pState);
				}
				if (this.ppRows != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = MarshalHelper.ConvertPropertyValueArraysToSRowSet(array, MarshalHelper.GetString8CodePage(nspiState));
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppRows, val);
					}
				}
			}
			finally
			{
				if (safeRpcMemoryHandle != null)
				{
					((IDisposable)safeRpcMemoryHandle).Dispose();
				}
			}
			return (int)result;
		}

		// Token: 0x04000E27 RID: 3623
		private IntPtr contextHandle;

		// Token: 0x04000E28 RID: 3624
		private NspiQueryRowsFlags flags;

		// Token: 0x04000E29 RID: 3625
		private IntPtr pState;

		// Token: 0x04000E2A RID: 3626
		private int midCount;

		// Token: 0x04000E2B RID: 3627
		private IntPtr pMids;

		// Token: 0x04000E2C RID: 3628
		private int rowCount;

		// Token: 0x04000E2D RID: 3629
		private IntPtr pPropTags;

		// Token: 0x04000E2E RID: 3630
		private IntPtr ppRows;
	}
}
