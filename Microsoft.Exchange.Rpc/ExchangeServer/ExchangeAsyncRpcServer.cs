using System;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x020001F9 RID: 505
	internal abstract class ExchangeAsyncRpcServer : BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001DF40 File Offset: 0x0001D340
		public override void DroppedConnection(IntPtr contextHandle)
		{
			IExchangeAsyncDispatch asyncDispatch = this.GetAsyncDispatch();
			if (asyncDispatch != null)
			{
				asyncDispatch.DroppedConnection(contextHandle);
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00021A40 File Offset: 0x00020E40
		public ExchangeAsyncRpcServer()
		{
		}
	}
}
