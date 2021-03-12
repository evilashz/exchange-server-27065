using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000062 RID: 98
	public class RedirSuiteServiceProxy : OwaPage
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00012E68 File Offset: 0x00011068
		protected override void OnLoad(EventArgs e)
		{
			string value = base.Request.Headers["Host"];
			string text = base.Request.QueryString["returnUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				string script = string.Format("window.top.location = 'https://{0}/owa/InitSuiteServiceProxy.aspx?exsvurl=1&realm=office365.com&returnUrl={1}'", HttpUtility.JavaScriptStringEncode(value), HttpUtility.JavaScriptStringEncode(HttpUtility.UrlEncode(text)));
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "Redir", script, true);
			}
		}
	}
}
