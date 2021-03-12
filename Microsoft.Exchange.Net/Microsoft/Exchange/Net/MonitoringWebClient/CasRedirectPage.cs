using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000789 RID: 1929
	internal class CasRedirectPage
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x00050976 File Offset: 0x0004EB76
		// (set) Token: 0x06002653 RID: 9811 RVA: 0x0005097E File Offset: 0x0004EB7E
		public string TargetUrl { get; private set; }

		// Token: 0x06002654 RID: 9812 RVA: 0x00050987 File Offset: 0x0004EB87
		private CasRedirectPage()
		{
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00050990 File Offset: 0x0004EB90
		public static bool TryParse(HttpWebResponseWrapper response, out CasRedirectPage errorPage)
		{
			if (response.Body == null || response.Body.IndexOf("ASP.casredirect_aspx", StringComparison.OrdinalIgnoreCase) < 0)
			{
				errorPage = null;
				return false;
			}
			Regex regex = new Regex("window.location.href[\\s]*=[\\s]*\"(?<URL>[^\"]*)\"", RegexOptions.IgnoreCase);
			Match match = regex.Match(response.Body);
			string s = match.Result("${URL}");
			errorPage = new CasRedirectPage();
			errorPage.TargetUrl = ParsingUtility.JavascriptDecode(s);
			return true;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000509F8 File Offset: 0x0004EBF8
		public override string ToString()
		{
			return string.Format("CasRedirectPage. Target URL: {0}", this.TargetUrl);
		}

		// Token: 0x04002319 RID: 8985
		internal const string PageMarker = "ASP.casredirect_aspx";
	}
}
