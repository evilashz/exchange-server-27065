using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E00 RID: 3584
	internal class ResponseMessage : IODataResponseMessage
	{
		// Token: 0x06005CC7 RID: 23751 RVA: 0x001216BC File Offset: 0x0011F8BC
		public ResponseMessage(HttpContext httpContext)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			this.HttpContext = httpContext;
			this.headers = new List<KeyValuePair<string, string>>();
			foreach (string text in this.HttpContext.Response.Headers.AllKeys)
			{
				this.headers.Add(new KeyValuePair<string, string>(text, this.HttpContext.Response.Headers[text]));
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x0012173B File Offset: 0x0011F93B
		// (set) Token: 0x06005CC9 RID: 23753 RVA: 0x00121743 File Offset: 0x0011F943
		public HttpContext HttpContext { get; private set; }

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x0012174C File Offset: 0x0011F94C
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x00121754 File Offset: 0x0011F954
		public Stream GetStream()
		{
			return this.HttpContext.Response.OutputStream;
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x00121766 File Offset: 0x0011F966
		// (set) Token: 0x06005CCD RID: 23757 RVA: 0x00121778 File Offset: 0x0011F978
		public int StatusCode
		{
			get
			{
				return this.HttpContext.Response.StatusCode;
			}
			set
			{
				this.HttpContext.Response.StatusCode = value;
			}
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x0012178B File Offset: 0x0011F98B
		public string GetHeader(string headerName)
		{
			return this.HttpContext.Response.Headers[headerName];
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x001217A3 File Offset: 0x0011F9A3
		public void SetHeader(string headerName, string headerValue)
		{
			this.HttpContext.Response.Headers[headerName] = headerValue;
			if (string.Equals(headerName, "content-type", StringComparison.OrdinalIgnoreCase))
			{
				this.HttpContext.Response.ContentType = headerValue;
			}
		}

		// Token: 0x04003249 RID: 12873
		private List<KeyValuePair<string, string>> headers;
	}
}
