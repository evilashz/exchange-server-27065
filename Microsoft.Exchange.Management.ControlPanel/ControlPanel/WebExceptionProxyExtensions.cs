using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A1 RID: 929
	internal static class WebExceptionProxyExtensions
	{
		// Token: 0x0600311B RID: 12571 RVA: 0x00095F78 File Offset: 0x00094178
		public static bool IsProxyNeedIdentityError(this WebException exception)
		{
			HttpWebResponse httpWebResponse = exception.Response as HttpWebResponse;
			return httpWebResponse != null && httpWebResponse.StatusCode == (HttpStatusCode)441;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00095FA4 File Offset: 0x000941A4
		public static ExEventLog.EventTuple GetProxyEventLogTuple(this WebException exception)
		{
			switch (exception.GetTroubleshootingID())
			{
			case WebExceptionTroubleshootingID.TrustFailure:
				return EcpEventLogConstants.Tuple_ProxyErrorSslTrustFailure;
			case WebExceptionTroubleshootingID.ServiceUnavailable:
				return EcpEventLogConstants.Tuple_ProxyErrorServiceUnavailable;
			case WebExceptionTroubleshootingID.Unauthorized:
				return EcpEventLogConstants.Tuple_ProxyErrorUnauthorized;
			case WebExceptionTroubleshootingID.Forbidden:
				return EcpEventLogConstants.Tuple_ProxyErrorForbidden;
			}
			return EcpEventLogConstants.Tuple_ProxyRequestFailed;
		}
	}
}
