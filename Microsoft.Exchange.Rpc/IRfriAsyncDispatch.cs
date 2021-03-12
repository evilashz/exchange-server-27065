using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001DF RID: 479
	internal interface IRfriAsyncDispatch
	{
		// Token: 0x06000A0E RID: 2574
		ICancelableAsyncResult BeginGetNewDSA(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetNewDSAFlags flags, string userDn, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A0F RID: 2575
		RfriStatus EndGetNewDSA(ICancelableAsyncResult asyncResult, out string server);

		// Token: 0x06000A10 RID: 2576
		ICancelableAsyncResult BeginGetFQDNFromLegacyDN(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetFQDNFromLegacyDNFlags flags, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A11 RID: 2577
		RfriStatus EndGetFQDNFromLegacyDN(ICancelableAsyncResult asyncResult, out string serverFqdn);

		// Token: 0x06000A12 RID: 2578
		ICancelableAsyncResult BeginGetAddressBookUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetAddressBookUrlFlags flags, string hostname, string userDn, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A13 RID: 2579
		RfriStatus EndGetAddressBookUrl(ICancelableAsyncResult asyncResult, out string url);

		// Token: 0x06000A14 RID: 2580
		ICancelableAsyncResult BeginGetMailboxUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetMailboxUrlFlags flags, string hostname, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A15 RID: 2581
		RfriStatus EndGetMailboxUrl(ICancelableAsyncResult asyncResult, out string url);
	}
}
