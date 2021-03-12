using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class ProtocolException : Exception
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00008B16 File Offset: 0x00006D16
		public ProtocolException(LID failureLID, string failureDescription, string failureInfo, Exception innerException = null) : this(failureLID, failureDescription, failureInfo, innerException, null, null)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008B28 File Offset: 0x00006D28
		public ProtocolException(LID failureLID, string failureDescription, string failureInfo, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders)
		{
			this.httpRequestHeaders = new WebHeaderCollection();
			this.httpResponseHeaders = new WebHeaderCollection();
			base..ctor(string.Format("{0} [LID={1}] {2}", failureDescription, (uint)failureLID, failureInfo), innerException);
			this.failureLID = failureLID;
			this.failureDescription = failureDescription;
			this.failureInfo = failureInfo;
			if (requestHeaders != null)
			{
				this.httpRequestHeaders = requestHeaders;
			}
			if (responseHeaders != null)
			{
				this.httpResponseHeaders = responseHeaders;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008B92 File Offset: 0x00006D92
		protected ProtocolException(SerializationInfo info, StreamingContext context)
		{
			this.httpRequestHeaders = new WebHeaderCollection();
			this.httpResponseHeaders = new WebHeaderCollection();
			base..ctor(info, context);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00008BB2 File Offset: 0x00006DB2
		public LID FailureLID
		{
			get
			{
				return this.failureLID;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00008BBA File Offset: 0x00006DBA
		public string FailureDescription
		{
			get
			{
				return this.failureDescription;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public string FailureInfo
		{
			get
			{
				return this.failureInfo;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008BCA File Offset: 0x00006DCA
		public WebHeaderCollection HttpRequestHeaders
		{
			get
			{
				return this.httpRequestHeaders;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008BD2 File Offset: 0x00006DD2
		public WebHeaderCollection HttpResponseHeaders
		{
			get
			{
				return this.httpResponseHeaders;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008BDC File Offset: 0x00006DDC
		public static ProtocolException FromHttpStatusCode(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders, Exception innerException = null)
		{
			if (httpStatusCode == HttpStatusCode.ServiceUnavailable)
			{
				return new HttpServiceUnavailableException(failureLID, string.Format("{0} [HttpStatusCode={1} {2}]", failureDescription, (int)httpStatusCode, httpStatusDescription), failureInfo, httpStatusDescription, ResponseCode.Success, ServiceCode.Success, innerException, requestHeaders, responseHeaders);
			}
			return new ProtocolFailureException(failureLID, string.Format("{0} [HttpStatusCode={1} {2}]", failureDescription, (int)httpStatusCode, httpStatusDescription), failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.Success, ServiceCode.Success, innerException, requestHeaders, responseHeaders);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008C40 File Offset: 0x00006E40
		public static ProtocolException FromResponseCode(LID failureLID, string failureDescription, ResponseCode responseCode, Exception innerException = null)
		{
			return ProtocolException.FromResponseCode(failureLID, failureDescription, string.Empty, HttpStatusCode.OK, "OK", responseCode, innerException, null, null, null);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008C68 File Offset: 0x00006E68
		public static ProtocolException FromResponseCode(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, ResponseCode responseCode, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders, MapiHttpVersion mapiHttpVersion)
		{
			string text = string.Format("{0} [ResponseCode={1}]", failureDescription, responseCode);
			switch (responseCode)
			{
			case ResponseCode.InvalidRequestType:
				return new InvalidRequestTypeException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders, mapiHttpVersion);
			case ResponseCode.ContextNotFound:
				return new ContextNotFoundException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			case ResponseCode.NotContextOwner:
				return new NotContextOwnerException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			case ResponseCode.InvalidPayload:
				return new InvalidPayloadException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			case ResponseCode.MissingCookie:
				return new MissingCookieException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			case ResponseCode.InvalidSequence:
				return new InvalidSequenceException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			case ResponseCode.EndpointDisabled:
				return new EndpointDisabledException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			}
			return new ProtocolFailureException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, responseCode, ServiceCode.Success, innerException, requestHeaders, responseHeaders);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008D5C File Offset: 0x00006F5C
		public static ProtocolException FromServiceCode(LID failureLID, string failureDescription, ServiceCode serviceCode, Exception innerException = null)
		{
			return ProtocolException.FromServiceCode(failureLID, failureDescription, string.Empty, HttpStatusCode.OK, "OK", serviceCode, innerException, null, null);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008D84 File Offset: 0x00006F84
		public static ProtocolException FromServiceCode(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, ServiceCode serviceCode, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders)
		{
			string text = string.Format("{0} [ServiceCode={1}]", failureDescription, serviceCode);
			switch (serviceCode)
			{
			case ServiceCode.Unavailable:
				return new ServiceUnavailableException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			case ServiceCode.TooBusy:
				return new ServiceTooBusyException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
			default:
				return new ProtocolFailureException(failureLID, text, failureInfo, httpStatusCode, httpStatusDescription, ResponseCode.Success, serviceCode, innerException, requestHeaders, responseHeaders);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008DF5 File Offset: 0x00006FF5
		public static ProtocolException FromTransportException(LID failureLID, string failureDescription, string failureInfo, HttpStatusCode httpStatusCode, string httpStatusDescription, Exception innerException, WebHeaderCollection requestHeaders, WebHeaderCollection responseHeaders)
		{
			return new ProtocolTransportException(failureLID, failureDescription, failureInfo, httpStatusCode, httpStatusDescription, innerException, requestHeaders, responseHeaders);
		}

		// Token: 0x040000F8 RID: 248
		private readonly LID failureLID;

		// Token: 0x040000F9 RID: 249
		private readonly string failureDescription;

		// Token: 0x040000FA RID: 250
		private readonly string failureInfo;

		// Token: 0x040000FB RID: 251
		private readonly WebHeaderCollection httpRequestHeaders;

		// Token: 0x040000FC RID: 252
		private readonly WebHeaderCollection httpResponseHeaders;
	}
}
