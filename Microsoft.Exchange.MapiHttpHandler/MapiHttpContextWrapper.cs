using System;
using System.Web;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000042 RID: 66
	public class MapiHttpContextWrapper : HttpContextWrapper
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		public MapiHttpContextWrapper(HttpContext context) : base(context)
		{
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000E3E7 File Offset: 0x0000C5E7
		public override HttpRequestBase Request
		{
			get
			{
				if (this.request == null)
				{
					this.request = base.Request;
				}
				return this.request;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000E403 File Offset: 0x0000C603
		public override HttpResponseBase Response
		{
			get
			{
				if (this.response == null)
				{
					this.response = base.Response;
				}
				return this.response;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E420 File Offset: 0x0000C620
		public static MapiHttpContextWrapper GetWrapper(HttpContext context)
		{
			MapiHttpContextWrapper mapiHttpContextWrapper = context.Items["MapiHttpContextWrapper"] as MapiHttpContextWrapper;
			if (mapiHttpContextWrapper == null)
			{
				mapiHttpContextWrapper = new MapiHttpContextWrapper(context);
				context.Items["MapiHttpContextWrapper"] = mapiHttpContextWrapper;
			}
			return mapiHttpContextWrapper;
		}

		// Token: 0x04000108 RID: 264
		private const string ContextWrapperName = "MapiHttpContextWrapper";

		// Token: 0x04000109 RID: 265
		private HttpRequestBase request;

		// Token: 0x0400010A RID: 266
		private HttpResponseBase response;
	}
}
