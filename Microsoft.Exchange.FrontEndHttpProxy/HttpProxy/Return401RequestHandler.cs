using System;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000CB RID: 203
	internal sealed class Return401RequestHandler : IHttpHandler
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0002C93C File Offset: 0x0002AB3C
		internal Return401RequestHandler()
		{
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0002C944 File Offset: 0x0002AB44
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0002C947 File Offset: 0x0002AB47
		public void ProcessRequest(HttpContext context)
		{
			context.Response.StatusCode = 401;
		}
	}
}
