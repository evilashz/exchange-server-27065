using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class InvalidSequenceException : ProtocolFailureException
	{
		// Token: 0x0600018D RID: 397 RVA: 0x000090D0 File Offset: 0x000072D0
		public InvalidSequenceException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.InvalidSequence, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000090F3 File Offset: 0x000072F3
		private InvalidSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
