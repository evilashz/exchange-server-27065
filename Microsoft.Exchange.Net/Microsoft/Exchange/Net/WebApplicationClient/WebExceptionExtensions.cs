using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B2F RID: 2863
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public static class WebExceptionExtensions
	{
		// Token: 0x06003DBD RID: 15805 RVA: 0x000A0B8C File Offset: 0x0009ED8C
		public static WebExceptionTroubleshootingID GetTroubleshootingID(this WebException exception)
		{
			WebExceptionStatus status = exception.Status;
			if (status != WebExceptionStatus.ConnectFailure)
			{
				switch (status)
				{
				case WebExceptionStatus.ProtocolError:
				{
					HttpWebResponse httpWebResponse = (HttpWebResponse)exception.Response;
					if (httpWebResponse != null)
					{
						return httpWebResponse.GetTroubleshootingID();
					}
					break;
				}
				case WebExceptionStatus.TrustFailure:
					return WebExceptionTroubleshootingID.TrustFailure;
				}
				return WebExceptionTroubleshootingID.Uncategorized;
			}
			return WebExceptionTroubleshootingID.ServiceUnavailable;
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000A0BD8 File Offset: 0x0009EDD8
		public static WebExceptionTroubleshootingID GetTroubleshootingID(this HttpWebResponse proxyResponse)
		{
			HttpStatusCode statusCode = proxyResponse.StatusCode;
			switch (statusCode)
			{
			case HttpStatusCode.Unauthorized:
				return WebExceptionTroubleshootingID.Unauthorized;
			case HttpStatusCode.PaymentRequired:
				break;
			case HttpStatusCode.Forbidden:
				return WebExceptionTroubleshootingID.Forbidden;
			case HttpStatusCode.NotFound:
				if (proxyResponse.Server.StartsWith("Microsoft-HTTPAPI"))
				{
					return WebExceptionTroubleshootingID.ServiceUnavailable;
				}
				break;
			default:
				if (statusCode == HttpStatusCode.ServiceUnavailable)
				{
					return WebExceptionTroubleshootingID.ServiceUnavailable;
				}
				break;
			}
			return WebExceptionTroubleshootingID.Uncategorized;
		}
	}
}
