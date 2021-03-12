using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class MissingCookieException : ProtocolFailureException
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00009100 File Offset: 0x00007300
		public MissingCookieException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.MissingCookie, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009123 File Offset: 0x00007323
		private MissingCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
