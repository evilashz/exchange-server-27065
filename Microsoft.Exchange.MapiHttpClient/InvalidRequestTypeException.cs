using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class InvalidRequestTypeException : ProtocolFailureException
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000909C File Offset: 0x0000729C
		public InvalidRequestTypeException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders, MapiHttpVersion mapiHttpVersion) : base(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.InvalidRequestType, ServiceCode.Success, innerException, requestHeaders, responseHeaders)
		{
			this.mapiHttpVersion = mapiHttpVersion;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000090C6 File Offset: 0x000072C6
		public MapiHttpVersion MapiHttpVersion
		{
			get
			{
				return this.mapiHttpVersion;
			}
		}

		// Token: 0x04000101 RID: 257
		private readonly MapiHttpVersion mapiHttpVersion;
	}
}
