using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers
{
	// Token: 0x020007D4 RID: 2004
	internal class OwaLanguageSelectionPage
	{
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0005944F File Offset: 0x0005764F
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x00059457 File Offset: 0x00057657
		public Uri FinalUri { get; private set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x00059460 File Offset: 0x00057660
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x00059468 File Offset: 0x00057668
		public string PostTarget { get; private set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x00059471 File Offset: 0x00057671
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x00059479 File Offset: 0x00057679
		public string Destination { get; private set; }

		// Token: 0x06002973 RID: 10611 RVA: 0x00059484 File Offset: 0x00057684
		public static bool TryParse(HttpWebResponseWrapper response, out OwaLanguageSelectionPage result)
		{
			if (response.Body == null || response.Body.IndexOf("ASP.languageselection_aspx", StringComparison.OrdinalIgnoreCase) < 0)
			{
				result = null;
				return false;
			}
			result = new OwaLanguageSelectionPage();
			result.FinalUri = response.Request.RequestUri;
			result.PostTarget = ParsingUtility.ParseFormAction(response);
			result.Destination = ParsingUtility.ParseFormDestination(response);
			return true;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000594E8 File Offset: 0x000576E8
		public static bool ContainsLanguagePageRedirection(HttpWebResponseWrapper response)
		{
			return response.StatusCode == HttpStatusCode.Found && response.Headers["Location"] != null && response.Headers["Location"].IndexOf("languageselection.aspx", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x040024A3 RID: 9379
		internal const string PageMarker = "ASP.languageselection_aspx";

		// Token: 0x040024A4 RID: 9380
		internal const string PageName = "languageselection.aspx";
	}
}
