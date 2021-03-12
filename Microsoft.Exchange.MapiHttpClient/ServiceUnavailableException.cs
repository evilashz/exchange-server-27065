using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class ServiceUnavailableException : ProtocolFailureException
	{
		// Token: 0x06000197 RID: 407 RVA: 0x000091BC File Offset: 0x000073BC
		public ServiceUnavailableException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.Success, ServiceCode.Unavailable, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000091E2 File Offset: 0x000073E2
		private ServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
