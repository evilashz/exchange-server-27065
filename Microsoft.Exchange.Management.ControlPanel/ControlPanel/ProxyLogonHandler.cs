using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200039D RID: 925
	internal class ProxyLogonHandler : IHttpHandler
	{
		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x00095C8B File Offset: 0x00093E8B
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x00095C8E File Offset: 0x00093E8E
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			context.Response.StatusCode = 241;
		}
	}
}
