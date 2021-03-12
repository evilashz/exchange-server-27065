using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000308 RID: 776
	internal class NspiAsyncRpcState_ResortRestriction : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_ResortRestriction,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x000388B4 File Offset: 0x00037CB4
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiResortRestrictionFlags flags, IntPtr pState, IntPtr pInDNTList, IntPtr ppDNTList)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pInDNTList = pInDNTList;
			this.ppDNTList = ppDNTList;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00035B20 File Offset: 0x00034F20
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiResortRestrictionFlags.None;
			this.pState = IntPtr.Zero;
			this.pInDNTList = IntPtr.Zero;
			this.ppDNTList = IntPtr.Zero;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000388F0 File Offset: 0x00037CF0
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			if (this.ppDNTList != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppDNTList, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			int[] mids = NspiHelper.ConvertCountedIntArrayFromNative(this.pInDNTList);
			base.AsyncDispatch.BeginResortRestriction(null, this.contextHandle, this.flags, state, mids, asyncCallback, this);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00038988 File Offset: 0x00037D88
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int[] array = null;
			NspiStatus result = NspiStatus.Success;
			NspiState nspiState = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndResortRestriction(asyncResult, out nspiState, out array);
				if (this.pState != IntPtr.Zero && nspiState != null)
				{
					nspiState.MarshalToNative(this.pState);
				}
				if (this.ppDNTList != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(array, true);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppDNTList, val);
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

		// Token: 0x04000E59 RID: 3673
		private IntPtr contextHandle;

		// Token: 0x04000E5A RID: 3674
		private NspiResortRestrictionFlags flags;

		// Token: 0x04000E5B RID: 3675
		private IntPtr pState;

		// Token: 0x04000E5C RID: 3676
		private IntPtr pInDNTList;

		// Token: 0x04000E5D RID: 3677
		private IntPtr ppDNTList;
	}
}
