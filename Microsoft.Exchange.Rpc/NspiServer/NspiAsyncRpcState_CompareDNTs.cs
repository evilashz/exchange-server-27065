using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000310 RID: 784
	internal class NspiAsyncRpcState_CompareDNTs : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E97 RID: 3735 RVA: 0x00038E00 File Offset: 0x00038200
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiCompareDNTsFlags flags, IntPtr pState, int mid1, int mid2, IntPtr pResult)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.mid1 = mid1;
			this.mid2 = mid2;
			this.pResult = pResult;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00035C18 File Offset: 0x00035018
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiCompareDNTsFlags.None;
			this.pState = IntPtr.Zero;
			this.mid1 = 0;
			this.mid2 = 0;
			this.pResult = IntPtr.Zero;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00038E44 File Offset: 0x00038244
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			if (this.pResult != IntPtr.Zero)
			{
				Marshal.WriteInt32(this.pResult, 0);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			base.AsyncDispatch.BeginCompareDNTs(null, this.contextHandle, this.flags, state, this.mid1, this.mid2, asyncCallback, this);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00038ED0 File Offset: 0x000382D0
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int val = 0;
			int result = (int)base.AsyncDispatch.EndCompareDNTs(asyncResult, out val);
			if (this.pResult != IntPtr.Zero)
			{
				Marshal.WriteInt32(this.pResult, val);
			}
			return result;
		}

		// Token: 0x04000E8D RID: 3725
		private IntPtr contextHandle;

		// Token: 0x04000E8E RID: 3726
		private NspiCompareDNTsFlags flags;

		// Token: 0x04000E8F RID: 3727
		private IntPtr pState;

		// Token: 0x04000E90 RID: 3728
		private int mid1;

		// Token: 0x04000E91 RID: 3729
		private int mid2;

		// Token: 0x04000E92 RID: 3730
		private IntPtr pResult;
	}
}
