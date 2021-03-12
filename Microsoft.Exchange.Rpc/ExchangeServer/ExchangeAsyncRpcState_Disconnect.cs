using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000201 RID: 513
	internal class ExchangeAsyncRpcState_Disconnect : BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_Disconnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x0001FB2C File Offset: 0x0001EF2C
		public void Initialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer, IntPtr pContextHandle)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.pContextHandle = pContextHandle;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001E214 File Offset: 0x0001D614
		public override void InternalReset()
		{
			this.pContextHandle = IntPtr.Zero;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001FB48 File Offset: 0x0001EF48
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			IntPtr contextHandle = Marshal.ReadIntPtr(this.pContextHandle);
			base.AsyncDispatch.BeginDisconnect(null, contextHandle, asyncCallback, this);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001FB78 File Offset: 0x0001EF78
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			IntPtr zero = IntPtr.Zero;
			int result = base.AsyncDispatch.EndDisconnect(asyncResult, out zero);
			Marshal.WriteIntPtr(this.pContextHandle, zero);
			return result;
		}

		// Token: 0x04000C24 RID: 3108
		private IntPtr pContextHandle;
	}
}
