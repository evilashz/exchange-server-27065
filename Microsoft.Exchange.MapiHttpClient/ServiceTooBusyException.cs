using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class ServiceTooBusyException : ProtocolFailureException
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000918C File Offset: 0x0000738C
		public ServiceTooBusyException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.Success, ServiceCode.TooBusy, innerException, requestHeaders, responseHeaders)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000091B2 File Offset: 0x000073B2
		private ServiceTooBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
