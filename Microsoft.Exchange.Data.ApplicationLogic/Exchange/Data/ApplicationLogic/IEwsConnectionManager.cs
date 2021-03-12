using System;
using System.Net.Security;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A3 RID: 163
	internal interface IEwsConnectionManager
	{
		// Token: 0x060006FC RID: 1788
		string GetSmtpAddress();

		// Token: 0x060006FD RID: 1789
		string GetPrincipalInfoForTracing();

		// Token: 0x060006FE RID: 1790
		void ReloadPrincipal();

		// Token: 0x060006FF RID: 1791
		Uri GetBackEndWebServicesUrl();

		// Token: 0x06000700 RID: 1792
		IExchangeService CreateBinding(RemoteCertificateValidationCallback certificateErrorHandler);
	}
}
