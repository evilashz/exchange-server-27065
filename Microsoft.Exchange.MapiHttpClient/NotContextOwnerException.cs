using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class NotContextOwnerException : ProtocolFailureException
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00009130 File Offset: 0x00007330
		public NotContextOwnerException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.NotContextOwner, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009153 File Offset: 0x00007353
		private NotContextOwnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
