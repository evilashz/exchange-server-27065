using System;
using System.Collections.Specialized;
using System.Net;
using System.ServiceModel.Web;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000837 RID: 2103
	internal class OutgoingWebResponseContextWrapper : IOutgoingWebResponseContext
	{
		// Token: 0x06003C9E RID: 15518 RVA: 0x000D5FF9 File Offset: 0x000D41F9
		public OutgoingWebResponseContextWrapper(OutgoingWebResponseContext response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.response = response;
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000D6016 File Offset: 0x000D4216
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x000D6023 File Offset: 0x000D4223
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.response.StatusCode;
			}
			set
			{
				this.response.StatusCode = value;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (set) Token: 0x06003CA1 RID: 15521 RVA: 0x000D6031 File Offset: 0x000D4231
		public string ETag
		{
			set
			{
				this.response.ETag = value;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x000D603F File Offset: 0x000D423F
		public string Expires
		{
			set
			{
				this.response.Headers[HttpResponseHeader.Expires] = value;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x000D6054 File Offset: 0x000D4254
		public string ContentType
		{
			set
			{
				this.response.ContentType = value;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000D6062 File Offset: 0x000D4262
		public NameValueCollection Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000D606F File Offset: 0x000D426F
		public void SetHeader(string name, string value)
		{
			this.response.Headers[name] = value;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x000D6083 File Offset: 0x000D4283
		// (set) Token: 0x06003CA7 RID: 15527 RVA: 0x000D6090 File Offset: 0x000D4290
		public bool SuppressContent
		{
			get
			{
				return this.response.SuppressEntityBody;
			}
			set
			{
				this.response.SuppressEntityBody = value;
			}
		}

		// Token: 0x04002176 RID: 8566
		private OutgoingWebResponseContext response;
	}
}
