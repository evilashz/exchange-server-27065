using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000205 RID: 517
	internal class ExchangeAsyncRpcState_NotificationConnect : BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationConnect,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_EMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x0001FF88 File Offset: 0x0001F388
		public void Initialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_EMSMDB asyncServer, IntPtr contextHandle, IntPtr pNotificationContextHandle)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.pNotificationContextHandle = pNotificationContextHandle;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0001E328 File Offset: 0x0001D728
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.pNotificationContextHandle = IntPtr.Zero;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001FFAC File Offset: 0x0001F3AC
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			base.AsyncDispatch.BeginNotificationConnect(null, this.contextHandle, asyncCallback, this);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001FFD4 File Offset: 0x0001F3D4
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			IntPtr zero = IntPtr.Zero;
			int result = base.AsyncDispatch.EndNotificationConnect(asyncResult, out zero);
			Marshal.WriteIntPtr(this.pNotificationContextHandle, zero);
			return result;
		}

		// Token: 0x04000C47 RID: 3143
		private IntPtr contextHandle;

		// Token: 0x04000C48 RID: 3144
		private IntPtr pNotificationContextHandle;
	}
}
