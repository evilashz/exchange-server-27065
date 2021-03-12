using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000300 RID: 768
	internal class NspiAsyncRpcState_UpdateStat : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_UpdateStat,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x000383F4 File Offset: 0x000377F4
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiUpdateStatFlags flags, IntPtr pState, IntPtr pDelta)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pDelta = pDelta;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000359B8 File Offset: 0x00034DB8
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiUpdateStatFlags.None;
			this.pState = IntPtr.Zero;
			this.pDelta = IntPtr.Zero;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00038428 File Offset: 0x00037828
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			bool deltaRequested = false;
			if (this.pDelta != IntPtr.Zero)
			{
				Marshal.WriteInt32(this.pDelta, 0);
				deltaRequested = true;
			}
			base.AsyncDispatch.BeginUpdateStat(null, this.contextHandle, this.flags, state, deltaRequested, asyncCallback, this);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x000384B0 File Offset: 0x000378B0
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			NspiState nspiState = null;
			int? num = null;
			int result = (int)base.AsyncDispatch.EndUpdateStat(asyncResult, out nspiState, out num);
			if (this.pState != IntPtr.Zero && nspiState != null)
			{
				nspiState.MarshalToNative(this.pState);
			}
			if (num != null && this.pDelta != IntPtr.Zero)
			{
				Marshal.WriteInt32(this.pDelta, num.Value);
			}
			return result;
		}

		// Token: 0x04000E1B RID: 3611
		private IntPtr contextHandle;

		// Token: 0x04000E1C RID: 3612
		private NspiUpdateStatFlags flags;

		// Token: 0x04000E1D RID: 3613
		private IntPtr pState;

		// Token: 0x04000E1E RID: 3614
		private IntPtr pDelta;
	}
}
