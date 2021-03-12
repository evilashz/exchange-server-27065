using System;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000039 RID: 57
	internal class AttachmentWebOperationContext : IAttachmentWebOperationContext, IOutgoingWebResponseContext
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000054E4 File Offset: 0x000036E4
		internal AttachmentWebOperationContext(HttpContext httpContext, IOutgoingWebResponseContext response)
		{
			this.response = response;
			this.httpContext = httpContext;
			this.userAgent = new UserAgent(httpContext.Request.UserAgent, UserContextManager.GetUserContext(httpContext).FeaturesManager.ClientServerSettings.ChangeLayout.Enabled, httpContext.Request.Cookies);
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			this.isPublicLogon = userContext.IsPublicLogon;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000556A File Offset: 0x0000376A
		public UserAgent UserAgent
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005572 File Offset: 0x00003772
		public bool IsPublicLogon
		{
			get
			{
				return this.isPublicLogon;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000557A File Offset: 0x0000377A
		public string GetRequestHeader(string name)
		{
			return this.httpContext.Request.Headers[name];
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005592 File Offset: 0x00003792
		public void SetNoCacheNoStore()
		{
			HttpContext.Current.Response.Cache.SetNoStore();
			HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
		}

		// Token: 0x17000066 RID: 102
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000055BD File Offset: 0x000037BD
		public string ContentType
		{
			set
			{
				this.response.ContentType = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000055CB File Offset: 0x000037CB
		public string ETag
		{
			set
			{
				this.response.ETag = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000055D9 File Offset: 0x000037D9
		public string Expires
		{
			set
			{
				this.response.Expires = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000055E7 File Offset: 0x000037E7
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000055F4 File Offset: 0x000037F4
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005602 File Offset: 0x00003802
		public NameValueCollection Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000560F File Offset: 0x0000380F
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005616 File Offset: 0x00003816
		public bool SuppressContent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000074 RID: 116
		private readonly bool isPublicLogon;

		// Token: 0x04000075 RID: 117
		private IOutgoingWebResponseContext response;

		// Token: 0x04000076 RID: 118
		private UserAgent userAgent;

		// Token: 0x04000077 RID: 119
		private readonly HttpContext httpContext;
	}
}
