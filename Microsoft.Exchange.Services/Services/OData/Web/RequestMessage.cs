using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000DFE RID: 3582
	internal class RequestMessage : IODataRequestMessage
	{
		// Token: 0x06005CB3 RID: 23731 RVA: 0x0012110C File Offset: 0x0011F30C
		public RequestMessage(HttpContext httpContext)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			this.HttpContext = httpContext;
			this.Method = this.HttpContext.Request.HttpMethod;
			this.Url = this.HttpContext.Request.Url;
			this.headers = new List<KeyValuePair<string, string>>();
			foreach (string text in this.HttpContext.Request.Headers.AllKeys)
			{
				this.headers.Add(new KeyValuePair<string, string>(text, this.HttpContext.Request.Headers[text]));
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06005CB4 RID: 23732 RVA: 0x001211B7 File Offset: 0x0011F3B7
		// (set) Token: 0x06005CB5 RID: 23733 RVA: 0x001211BF File Offset: 0x0011F3BF
		public HttpContext HttpContext { get; private set; }

		// Token: 0x06005CB6 RID: 23734 RVA: 0x001211C8 File Offset: 0x0011F3C8
		public string GetHeader(string headerName)
		{
			return this.HttpContext.Request.Headers[headerName];
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x001211E0 File Offset: 0x0011F3E0
		public void SetHeader(string headerName, string headerValue)
		{
			this.HttpContext.Request.Headers[headerName] = headerValue;
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x001211F9 File Offset: 0x0011F3F9
		public Stream GetStream()
		{
			return this.HttpContext.Request.InputStream;
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x0012120B File Offset: 0x0011F40B
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06005CBA RID: 23738 RVA: 0x00121213 File Offset: 0x0011F413
		// (set) Token: 0x06005CBB RID: 23739 RVA: 0x0012121B File Offset: 0x0011F41B
		public Uri Url { get; set; }

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x00121224 File Offset: 0x0011F424
		// (set) Token: 0x06005CBD RID: 23741 RVA: 0x0012122C File Offset: 0x0011F42C
		public string Method { get; set; }

		// Token: 0x04003244 RID: 12868
		private List<KeyValuePair<string, string>> headers;
	}
}
