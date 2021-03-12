using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Web;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000836 RID: 2102
	internal class OutgoingHttpResponseContextWrapper : IOutgoingWebResponseContext
	{
		// Token: 0x06003C94 RID: 15508 RVA: 0x000D5F29 File Offset: 0x000D4129
		public OutgoingHttpResponseContextWrapper(HttpResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.response = response;
			this.response.TrySkipIisCustomErrors = true;
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000D5F52 File Offset: 0x000D4152
		// (set) Token: 0x06003C96 RID: 15510 RVA: 0x000D5F5F File Offset: 0x000D415F
		public HttpStatusCode StatusCode
		{
			get
			{
				return (HttpStatusCode)this.response.StatusCode;
			}
			set
			{
				this.response.StatusCode = (int)value;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (set) Token: 0x06003C97 RID: 15511 RVA: 0x000D5F6D File Offset: 0x000D416D
		public string ETag
		{
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.response.Cache.SetETag(value);
				}
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000D5F88 File Offset: 0x000D4188
		public string Expires
		{
			set
			{
				this.response.Headers["Expires"] = value;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (set) Token: 0x06003C99 RID: 15513 RVA: 0x000D5FA0 File Offset: 0x000D41A0
		public string ContentType
		{
			set
			{
				this.response.ContentType = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x000D5FAE File Offset: 0x000D41AE
		public long ContentLength
		{
			set
			{
				this.response.Headers["Content-Length"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000D5FD1 File Offset: 0x000D41D1
		public NameValueCollection Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003C9C RID: 15516 RVA: 0x000D5FDE File Offset: 0x000D41DE
		// (set) Token: 0x06003C9D RID: 15517 RVA: 0x000D5FEB File Offset: 0x000D41EB
		public bool SuppressContent
		{
			get
			{
				return this.response.SuppressContent;
			}
			set
			{
				this.response.SuppressContent = value;
			}
		}

		// Token: 0x04002175 RID: 8565
		private HttpResponse response;
	}
}
