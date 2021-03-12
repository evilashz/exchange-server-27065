using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000015 RID: 21
	internal interface INspiClient : IRpcClient, IDisposable
	{
		// Token: 0x06000090 RID: 144
		IAsyncResult BeginBind(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000091 RID: 145
		NspiCallResult EndBind(IAsyncResult asyncResult);

		// Token: 0x06000092 RID: 146
		IAsyncResult BeginUnbind(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000093 RID: 147
		NspiCallResult EndUnbind(IAsyncResult asyncResult);

		// Token: 0x06000094 RID: 148
		IAsyncResult BeginGetHierarchyInfo(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000095 RID: 149
		NspiCallResult EndGetHierarchyInfo(IAsyncResult asyncResult, out int version);

		// Token: 0x06000096 RID: 150
		IAsyncResult BeginGetMatches(string primarySmtpAddress, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000097 RID: 151
		NspiCallResult EndGetMatches(IAsyncResult asyncResult, out int[] minimalIds);

		// Token: 0x06000098 RID: 152
		IAsyncResult BeginQueryRows(int[] minimalIds, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000099 RID: 153
		NspiCallResult EndQueryRows(IAsyncResult asyncResult, out string homeMDB, out string userLegacyDN);

		// Token: 0x0600009A RID: 154
		IAsyncResult BeginDNToEph(string serverLegacyDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600009B RID: 155
		NspiCallResult EndDNToEph(IAsyncResult asyncResult, out int[] minimalIds);

		// Token: 0x0600009C RID: 156
		IAsyncResult BeginGetNetworkAddresses(int[] minimalIds, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600009D RID: 157
		NspiCallResult EndGetNetworkAddresses(IAsyncResult asyncResult, out string[] networkAddresses);
	}
}
