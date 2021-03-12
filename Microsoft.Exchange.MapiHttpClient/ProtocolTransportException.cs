using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class ProtocolTransportException : ProtocolFailureException
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00009160 File Offset: 0x00007360
		public ProtocolTransportException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.Success, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009182 File Offset: 0x00007382
		private ProtocolTransportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
