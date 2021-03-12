using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000017 RID: 23
	internal interface ISampleClient : IDisposable
	{
		// Token: 0x060000A2 RID: 162
		IAsyncResult BeginSomeOperation(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000A3 RID: 163
		int EndSomeOperation(IAsyncResult asyncResult);
	}
}
