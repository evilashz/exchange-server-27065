using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class EndpointDisabledException : ProtocolFailureException
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00008E94 File Offset: 0x00007094
		public EndpointDisabledException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.EndpointDisabled, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008EB7 File Offset: 0x000070B7
		private EndpointDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
