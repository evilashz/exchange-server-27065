using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x020001FF RID: 511
	internal class ExchangeAsyncRpcState_Dummy : BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Dummy,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001FAAC File Offset: 0x0001EEAC
		public void Initialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer, IntPtr bindingHandle)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.bindingHandle = bindingHandle;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001E1FC File Offset: 0x0001D5FC
		public override void InternalReset()
		{
			this.bindingHandle = IntPtr.Zero;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001FAC8 File Offset: 0x0001EEC8
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			base.AsyncDispatch.BeginDummy(null, new RpcClientBinding(this.bindingHandle, base.AsyncState), asyncCallback, this);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001FAFC File Offset: 0x0001EEFC
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return base.AsyncDispatch.EndDummy(asyncResult);
		}

		// Token: 0x04000C1B RID: 3099
		private IntPtr bindingHandle;
	}
}
