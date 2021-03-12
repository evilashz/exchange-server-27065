using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class ContextNotFoundException : ProtocolFailureException
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00008E64 File Offset: 0x00007064
		public ContextNotFoundException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.ContextNotFound, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008E87 File Offset: 0x00007087
		private ContextNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
