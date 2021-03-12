using System;
using System.Net;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B89 RID: 2953
	internal interface IClientDiagnosticsHandler
	{
		// Token: 0x06003F41 RID: 16193
		object BeforeSendRequest(WebHeaderCollection transportRequestHeaders, string soapRequest);

		// Token: 0x06003F42 RID: 16194
		void AfterRecieveReply(WebHeaderCollection transportResponseHeaders, HttpStatusCode httpStatusCode, bool isfault, string soapResponse, object correlationState);
	}
}
