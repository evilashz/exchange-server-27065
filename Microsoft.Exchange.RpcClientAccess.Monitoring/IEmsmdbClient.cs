using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200000E RID: 14
	internal interface IEmsmdbClient : IRpcClient, IDisposable
	{
		// Token: 0x06000054 RID: 84
		IAsyncResult BeginConnect(string userDn, TimeSpan timeout, bool useMonitoringContext, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000055 RID: 85
		ConnectCallResult EndConnect(IAsyncResult asyncResult);

		// Token: 0x06000056 RID: 86
		IAsyncResult BeginLogon(string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000057 RID: 87
		IAsyncResult BeginPublicLogon(string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000058 RID: 88
		LogonCallResult EndLogon(IAsyncResult asyncResult);

		// Token: 0x06000059 RID: 89
		LogonCallResult EndPublicLogon(IAsyncResult asyncResult);

		// Token: 0x0600005A RID: 90
		IAsyncResult BeginDummy(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600005B RID: 91
		DummyCallResult EndDummy(IAsyncResult asyncResult);

		// Token: 0x0600005C RID: 92
		string GetConnectionUriString();
	}
}
