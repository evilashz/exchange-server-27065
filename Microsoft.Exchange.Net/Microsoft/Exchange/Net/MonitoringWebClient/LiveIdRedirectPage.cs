using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000791 RID: 1937
	internal class LiveIdRedirectPage
	{
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x00050F79 File Offset: 0x0004F179
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x00050F81 File Offset: 0x0004F181
		public string TargetUrl { get; private set; }

		// Token: 0x06002677 RID: 9847 RVA: 0x00050F8A File Offset: 0x0004F18A
		private LiveIdRedirectPage()
		{
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00050F94 File Offset: 0x0004F194
		public static bool TryParse(HttpWebResponseWrapper response, out LiveIdRedirectPage errorPage)
		{
			if (response.Body == null || response.Body.IndexOf("onload=\"javascript:rd();\"", StringComparison.OrdinalIgnoreCase) < 0)
			{
				errorPage = null;
				return false;
			}
			Regex regex = new Regex("window.location.replace\\(\"(?<URL>[^\"]*)\"\\)", RegexOptions.IgnoreCase);
			Match match = regex.Match(response.Body);
			if (!match.Success)
			{
				errorPage = null;
				return false;
			}
			string s = match.Result("${URL}");
			errorPage = new LiveIdRedirectPage();
			errorPage.TargetUrl = ParsingUtility.JavascriptDecode(s);
			return true;
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00051009 File Offset: 0x0004F209
		public override string ToString()
		{
			return string.Format("LiveIdRedirectPage. Target URL: {0}", this.TargetUrl);
		}

		// Token: 0x04002328 RID: 9000
		internal const string PageMarker = "onload=\"javascript:rd();\"";
	}
}
