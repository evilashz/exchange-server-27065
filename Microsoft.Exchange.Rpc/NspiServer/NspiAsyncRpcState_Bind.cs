using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x020002FC RID: 764
	internal class NspiAsyncRpcState_Bind : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Bind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003816C File Offset: 0x0003756C
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr bindingHandle, NspiBindFlags flags, IntPtr pState, IntPtr pServerGuid, IntPtr pContextHandle)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.bindingHandle = bindingHandle;
			this.flags = flags;
			this.pState = pState;
			this.pServerGuid = pServerGuid;
			this.pContextHandle = pContextHandle;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00035958 File Offset: 0x00034D58
		public override void InternalReset()
		{
			this.bindingHandle = IntPtr.Zero;
			this.flags = NspiBindFlags.None;
			this.pState = IntPtr.Zero;
			this.pServerGuid = IntPtr.Zero;
			this.pContextHandle = IntPtr.Zero;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000381A8 File Offset: 0x000375A8
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			if (this.pContextHandle != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.pContextHandle, IntPtr.Zero);
			}
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			ClientBinding clientBinding = new RpcClientBinding(this.bindingHandle, base.AsyncState);
			Guid? guid = null;
			if (this.pServerGuid != IntPtr.Zero)
			{
				Guid value = NspiHelper.ConvertGuidFromNative(this.pServerGuid);
				Guid? guid2 = new Guid?(value);
				guid = guid2;
			}
			base.AsyncDispatch.BeginBind(null, clientBinding, this.flags, state, guid, asyncCallback, this);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00038278 File Offset: 0x00037678
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			Guid? guid = null;
			IntPtr zero = IntPtr.Zero;
			int result = (int)base.AsyncDispatch.EndBind(asyncResult, out guid, out zero);
			if (this.pContextHandle != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.pContextHandle, zero);
			}
			if (guid != null && this.pServerGuid != IntPtr.Zero)
			{
				Marshal.StructureToPtr(guid.Value, this.pServerGuid, false);
			}
			return result;
		}

		// Token: 0x04000E04 RID: 3588
		private IntPtr bindingHandle;

		// Token: 0x04000E05 RID: 3589
		private NspiBindFlags flags;

		// Token: 0x04000E06 RID: 3590
		private IntPtr pState;

		// Token: 0x04000E07 RID: 3591
		private IntPtr pServerGuid;

		// Token: 0x04000E08 RID: 3592
		private IntPtr pContextHandle;
	}
}
