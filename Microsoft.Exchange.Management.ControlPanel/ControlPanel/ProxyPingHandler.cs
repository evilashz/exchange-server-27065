using System;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200039E RID: 926
	internal class ProxyPingHandler : IHttpHandler
	{
		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x00095CA8 File Offset: 0x00093EA8
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x00095CAB File Offset: 0x00093EAB
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			context.Response.AppendHeader("msExchEcpVersion", ThemeResource.ApplicationVersion);
			context.Response.StatusCode = 200;
		}
	}
}
