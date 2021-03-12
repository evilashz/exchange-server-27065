using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EmsmdbCallResult : RpcCallResult
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000025D1 File Offset: 0x000007D1
		protected EmsmdbCallResult(Exception exception, IPropertyBag httpResponseInformation) : this(exception, ErrorCode.None, null, null, httpResponseInformation)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025DE File Offset: 0x000007DE
		protected EmsmdbCallResult(Exception exception, ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace) : this(exception, errorCode, remoteExceptionTrace, null, null)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025EC File Offset: 0x000007EC
		protected EmsmdbCallResult(Exception exception, ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace, MonitoringActivityAuxiliaryBlock activityContext, IPropertyBag httpResponseInformation) : base(exception, errorCode, remoteExceptionTrace, activityContext)
		{
			if (httpResponseInformation != null)
			{
				WebHeaderCollection webHeaderCollection;
				if (httpResponseInformation.TryGet(ContextPropertySchema.RequestHeaderCollection, out webHeaderCollection))
				{
					this.httpRequestHeaders = webHeaderCollection;
				}
				webHeaderCollection = null;
				if (httpResponseInformation.TryGet(ContextPropertySchema.ResponseHeaderCollection, out webHeaderCollection))
				{
					this.httpResponseHeaders = webHeaderCollection;
				}
				httpResponseInformation.TryGet(ContextPropertySchema.ResponseStatusCode, out this.httpStatusCode);
				httpResponseInformation.TryGet(ContextPropertySchema.ResponseStatusCodeDescription, out this.httpStatusCodeDescription);
			}
			this.FilterHttpHeaders();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000267A File Offset: 0x0000087A
		public WebHeaderCollection HttpResponseHeaders
		{
			get
			{
				return this.httpResponseHeaders;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002682 File Offset: 0x00000882
		public WebHeaderCollection HttpRequestHeaders
		{
			get
			{
				return this.httpRequestHeaders;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000268A File Offset: 0x0000088A
		public HttpStatusCode HttpResponseStatusCode
		{
			get
			{
				return this.httpStatusCode;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002692 File Offset: 0x00000892
		public string HttpResponseStatusCodeDescription
		{
			get
			{
				return this.httpStatusCodeDescription;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000269A File Offset: 0x0000089A
		private void FilterHttpHeaders()
		{
			this.httpRequestHeaders.Remove("Cookie");
			this.httpResponseHeaders.Remove("Set-Cookie");
		}

		// Token: 0x04000016 RID: 22
		private readonly WebHeaderCollection httpResponseHeaders = new WebHeaderCollection();

		// Token: 0x04000017 RID: 23
		private readonly WebHeaderCollection httpRequestHeaders = new WebHeaderCollection();

		// Token: 0x04000018 RID: 24
		private readonly HttpStatusCode httpStatusCode;

		// Token: 0x04000019 RID: 25
		private readonly string httpStatusCodeDescription;
	}
}
