using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200012F RID: 303
	public class InitSuiteServiceProxy : Page
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x000456C0 File Offset: 0x000438C0
		protected override void OnLoad(EventArgs e)
		{
			HttpCookie httpCookie = new HttpCookie("SuiteServiceProxyInit", "true");
			httpCookie.Expires = new DateTime(9999, 12, 31);
			base.Response.Cookies.Add(httpCookie);
			string text = base.Request.QueryString["returnUrl"];
			if (!string.IsNullOrEmpty(text) && this.IsRedirectAllowed(text))
			{
				base.Response.Redirect(text, false);
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00045738 File Offset: 0x00043938
		protected bool IsRedirectAllowed(string returnUrl)
		{
			bool result = false;
			Uri uri = new Uri(returnUrl);
			if (uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
			{
				SuiteServiceProxyHelper suiteServiceProxyHelper = new SuiteServiceProxyHelper();
				string[] suiteServiceProxyOriginAllowedList = suiteServiceProxyHelper.GetSuiteServiceProxyOriginAllowedList();
				foreach (string pattern in suiteServiceProxyOriginAllowedList)
				{
					Regex regex = new Regex(pattern);
					if (regex.IsMatch(uri.Authority))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
	}
}
