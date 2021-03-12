using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200078D RID: 1933
	internal class FbaRedirectPage
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x00050C51 File Offset: 0x0004EE51
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x00050C59 File Offset: 0x0004EE59
		public string FbaLogonPagePathAndQuery { get; set; }

		// Token: 0x06002667 RID: 9831 RVA: 0x00050C64 File Offset: 0x0004EE64
		public static FbaRedirectPage Parse(HttpWebResponseWrapper response)
		{
			FbaRedirectPage fbaRedirectPage = new FbaRedirectPage();
			if (response.Body == null || response.Body.IndexOf("{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}", StringComparison.OrdinalIgnoreCase) < 0)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingOwaFbaRedirectPage("{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}"), response.Request, response, "{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}");
			}
			fbaRedirectPage.FbaLogonPagePathAndQuery = "/owa/auth/" + ParsingUtility.ParseJavascriptStringVariable(response, "a_sLgn") + ParsingUtility.ParseJavascriptStringVariable(response, "a_sUrl");
			return fbaRedirectPage;
		}

		// Token: 0x04002322 RID: 8994
		private const string RedirectPageMarker = "{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}";
	}
}
