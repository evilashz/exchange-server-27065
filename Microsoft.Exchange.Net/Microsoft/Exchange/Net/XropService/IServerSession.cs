using System;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA2 RID: 2978
	internal interface IServerSession
	{
		// Token: 0x06003FE8 RID: 16360
		IAsyncResult BeginConnect(ConnectRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FE9 RID: 16361
		ConnectResponse EndConnect(IAsyncResult asyncResult);

		// Token: 0x06003FEA RID: 16362
		IAsyncResult BeginExecute(ExecuteRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FEB RID: 16363
		ExecuteResponse EndExecute(IAsyncResult asyncResult);

		// Token: 0x06003FEC RID: 16364
		IAsyncResult BeginDisconnect(DisconnectRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FED RID: 16365
		DisconnectResponse EndDisconnect(IAsyncResult asyncResult);
	}
}
