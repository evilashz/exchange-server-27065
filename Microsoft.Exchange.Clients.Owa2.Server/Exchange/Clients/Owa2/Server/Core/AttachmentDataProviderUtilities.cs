using System;
using System.Net;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200002F RID: 47
	internal static class AttachmentDataProviderUtilities
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004100 File Offset: 0x00002300
		public static AttachmentResultCode GetResultCodeFromWebException(WebException exception, DataProviderCallLogEvent logEvent)
		{
			HttpWebResponse httpWebResponse = exception.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				logEvent.ErrorResponseHeaders = httpWebResponse.Headers;
			}
			WebExceptionStatus status = exception.Status;
			if (status == WebExceptionStatus.ProtocolError)
			{
				if (httpWebResponse != null)
				{
					HttpStatusCode statusCode = httpWebResponse.StatusCode;
					if (statusCode <= HttpStatusCode.NotFound)
					{
						if (statusCode == HttpStatusCode.Unauthorized)
						{
							return AttachmentResultCode.AccessDenied;
						}
						if (statusCode == HttpStatusCode.NotFound)
						{
							return AttachmentResultCode.NotFound;
						}
					}
					else if (statusCode == HttpStatusCode.RequestTimeout || statusCode == HttpStatusCode.GatewayTimeout)
					{
						return AttachmentResultCode.Timeout;
					}
				}
				return AttachmentResultCode.GenericFailure;
			}
			if (status == WebExceptionStatus.Timeout)
			{
				return AttachmentResultCode.Timeout;
			}
			return AttachmentResultCode.GenericFailure;
		}

		// Token: 0x04000062 RID: 98
		public const string ClientRequestIdHeader = "client-request-id";
	}
}
