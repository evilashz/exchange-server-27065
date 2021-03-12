using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000260 RID: 608
	public class SuiteServiceProxy : Page
	{
		// Token: 0x06001480 RID: 5248 RVA: 0x0007D2EC File Offset: 0x0007B4EC
		protected override void OnLoad(EventArgs e)
		{
			this.Context.Response.Headers.Remove("X-FRAME-OPTIONS");
			SuiteServiceProxyHelper suiteServiceProxyHelper = new SuiteServiceProxyHelper();
			string text = string.Join(", ", from domain in suiteServiceProxyHelper.GetSuiteServiceProxyOriginAllowedList()
			select HttpUtility.JavaScriptStringEncode(domain, true));
			string suiteServiceProxyScriptUrl = suiteServiceProxyHelper.GetSuiteServiceProxyScriptUrl();
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(suiteServiceProxyScriptUrl))
			{
				throw new ArgumentException("Call GetShellInfo to Shell service failed");
			}
			string script = string.Format("\r\n                var settings = {{\r\n                    originAuthorityValidator: O365SuiteServiceProxy.ServiceHandlers.validateOrigin,\r\n                    onBeforeSendRequest: O365SuiteServiceProxy.ServiceHandlers.onBeforeSendRequestSuiteService,\r\n                    trustedOriginAuthorities: [{0}]\r\n                }};\r\n\r\n                O365SuiteServiceProxy.RequestExecutorMessageProcessor.init(settings);", text);
			this.Page.ClientScript.RegisterStartupScript(typeof(SuiteServiceProxy), "InitializationScript", script, true);
			this.Page.ClientScript.RegisterClientScriptInclude("SuiteServiceProxy", suiteServiceProxyScriptUrl);
		}
	}
}
