using System;
using System.Net;
using System.Web;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000003 RID: 3
	internal class AutoDiscoverV2HandlerV10 : AutoDiscoverV2HandlerBase, IHttpHandler
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000023D1 File Offset: 0x000005D1
		public override bool Validate(HttpContextBase context)
		{
			if (context.Request.Url.AbsolutePath.EndsWith("v1.0", StringComparison.OrdinalIgnoreCase))
			{
				throw AutoDiscoverResponseException.BadRequest(HttpStatusCode.BadRequest.ToString(), "Id segment is missing in the URL", null);
			}
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000240C File Offset: 0x0000060C
		public override string GetEmailAddressFromUrl(HttpContextBase context)
		{
			int num = context.Request.Url.AbsolutePath.LastIndexOf('/');
			return context.Request.Url.AbsolutePath.Substring(num + 1);
		}
	}
}
