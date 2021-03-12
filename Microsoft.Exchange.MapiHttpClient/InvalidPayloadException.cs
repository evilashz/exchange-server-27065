using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class InvalidPayloadException : ProtocolFailureException
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000906C File Offset: 0x0000726C
		public InvalidPayloadException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.InvalidPayload, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000908F File Offset: 0x0000728F
		private InvalidPayloadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
