using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class ProtocolFailureException : ProtocolException
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00008E08 File Offset: 0x00007008
		public ProtocolFailureException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, ResponseCode responseCode, ServiceCode serviceCode, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders) : base(failureLID, failureDescription, failureInfo, innerException, requestHeaders, responseHeaders)
		{
			this.httpStatusCode = httpStatusCode;
			this.httpStatusDescription = httpStatusDescription;
			this.responseCode = responseCode;
			this.serviceCode = serviceCode;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008E39 File Offset: 0x00007039
		protected ProtocolFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008E43 File Offset: 0x00007043
		public HttpStatusCode HttpStatusCode
		{
			get
			{
				return this.httpStatusCode;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008E4B File Offset: 0x0000704B
		public string HttpStatusDescription
		{
			get
			{
				return this.httpStatusDescription;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00008E53 File Offset: 0x00007053
		public ResponseCode ResponseCode
		{
			get
			{
				return this.responseCode;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00008E5B File Offset: 0x0000705B
		public ServiceCode ServiceCode
		{
			get
			{
				return this.serviceCode;
			}
		}

		// Token: 0x040000FD RID: 253
		private readonly HttpStatusCode httpStatusCode;

		// Token: 0x040000FE RID: 254
		private readonly string httpStatusDescription;

		// Token: 0x040000FF RID: 255
		private readonly ResponseCode responseCode;

		// Token: 0x04000100 RID: 256
		private readonly ServiceCode serviceCode;
	}
}
