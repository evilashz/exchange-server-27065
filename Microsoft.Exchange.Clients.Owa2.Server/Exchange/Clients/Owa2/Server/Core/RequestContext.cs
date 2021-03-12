using System;
using System.Globalization;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000247 RID: 583
	internal sealed class RequestContext
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x0004E3C1 File Offset: 0x0004C5C1
		private RequestContext(HttpContext httpContext)
		{
			this.httpContext = httpContext;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		public static RequestContext Current
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return (RequestContext)httpContext.Items["RequestContext"];
				}
				return null;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0004E415 File Offset: 0x0004C615
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x0004E41D File Offset: 0x0004C61D
		public bool ErrorSent
		{
			get
			{
				return this.errorSent;
			}
			set
			{
				this.errorSent = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0004E426 File Offset: 0x0004C626
		public HttpContext HttpContext
		{
			get
			{
				return this.httpContext;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004E42E File Offset: 0x0004C62E
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x0004E436 File Offset: 0x0004C636
		public IMailboxContext UserContext
		{
			get
			{
				return this.userContext;
			}
			set
			{
				this.userContext = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0004E43F File Offset: 0x0004C63F
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x0004E447 File Offset: 0x0004C647
		public OwaRequestType RequestType
		{
			get
			{
				return this.requestType;
			}
			set
			{
				this.requestType = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0004E450 File Offset: 0x0004C650
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x0004E458 File Offset: 0x0004C658
		internal HttpStatusCode HttpStatusCode
		{
			get
			{
				return this.httpStatusCode;
			}
			set
			{
				this.httpStatusCode = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0004E461 File Offset: 0x0004C661
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x0004E469 File Offset: 0x0004C669
		internal string DestinationUrl
		{
			get
			{
				return this.destinationUrl;
			}
			set
			{
				this.destinationUrl = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0004E472 File Offset: 0x0004C672
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0004E47A File Offset: 0x0004C67A
		internal string DestinationUrlQueryString
		{
			get
			{
				return this.destinationUrlQueryString;
			}
			set
			{
				this.destinationUrlQueryString = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0004E483 File Offset: 0x0004C683
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0004E48B File Offset: 0x0004C68B
		internal bool FailedToSaveUserCulture
		{
			get
			{
				return this.failedToSaveUserCulture;
			}
			set
			{
				this.failedToSaveUserCulture = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0004E494 File Offset: 0x0004C694
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0004E49C File Offset: 0x0004C69C
		internal CultureInfo LanguagePostUserCulture
		{
			get
			{
				return this.languagePostUserCulture;
			}
			set
			{
				this.languagePostUserCulture = value;
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0004E4A8 File Offset: 0x0004C6A8
		internal static RequestContext Create(HttpContext httpContext)
		{
			RequestContext requestContext = new RequestContext(httpContext);
			ExTraceGlobals.RequestTracer.TraceDebug<DateTime>(0L, "New request received: {0}", requestContext.creationTime);
			return requestContext;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
		internal static RequestContext Get(HttpContext httpContext)
		{
			return (RequestContext)httpContext.Items["RequestContext"];
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0004E4EB File Offset: 0x0004C6EB
		internal void Set(HttpContext httpContext)
		{
			httpContext.Items["RequestContext"] = this;
		}

		// Token: 0x04000C1A RID: 3098
		private const string RequestContextKey = "RequestContext";

		// Token: 0x04000C1B RID: 3099
		private HttpContext httpContext;

		// Token: 0x04000C1C RID: 3100
		private OwaRequestType requestType;

		// Token: 0x04000C1D RID: 3101
		private HttpStatusCode httpStatusCode = HttpStatusCode.OK;

		// Token: 0x04000C1E RID: 3102
		private string destinationUrl;

		// Token: 0x04000C1F RID: 3103
		private string destinationUrlQueryString;

		// Token: 0x04000C20 RID: 3104
		private bool failedToSaveUserCulture;

		// Token: 0x04000C21 RID: 3105
		private CultureInfo languagePostUserCulture;

		// Token: 0x04000C22 RID: 3106
		private IMailboxContext userContext;

		// Token: 0x04000C23 RID: 3107
		private bool errorSent;

		// Token: 0x04000C24 RID: 3108
		private readonly DateTime creationTime = DateTime.UtcNow;
	}
}
