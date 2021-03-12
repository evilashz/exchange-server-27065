using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class HttpServiceUnavailableException : ProtocolFailureException
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00009038 File Offset: 0x00007238
		public HttpServiceUnavailableException(LID failureLID, string failureDescription, string failureInfo, string httpStatusDescription, ResponseCode responseCode, ServiceCode serviceCode, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, HttpStatusCode.ServiceUnavailable, httpStatusDescription, responseCode, serviceCode, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000905F File Offset: 0x0000725F
		protected HttpServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
