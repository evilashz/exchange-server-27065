using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000016 RID: 22
	internal interface IRfriClient : IRpcClient, IDisposable
	{
		// Token: 0x0600009E RID: 158
		IAsyncResult BeginGetNewDsa(string userLegacyDN, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600009F RID: 159
		RfriCallResult EndGetNewDsa(IAsyncResult asyncResult, out string server);

		// Token: 0x060000A0 RID: 160
		IAsyncResult BeginGetFqdnFromLegacyDn(string serverDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000A1 RID: 161
		RfriCallResult EndGetFqdnFromLegacyDn(IAsyncResult asyncResult, out string serverFqdn);
	}
}
