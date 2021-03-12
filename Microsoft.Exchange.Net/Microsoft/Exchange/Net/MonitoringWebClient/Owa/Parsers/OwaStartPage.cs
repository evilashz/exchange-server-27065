using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers
{
	// Token: 0x020007D0 RID: 2000
	internal abstract class OwaStartPage
	{
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x0005901A File Offset: 0x0005721A
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x00059022 File Offset: 0x00057222
		public Uri FinalUri { get; protected set; }

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x0005902B File Offset: 0x0005722B
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x00059033 File Offset: 0x00057233
		public Uri StaticFileUri { get; protected set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x0005903C File Offset: 0x0005723C
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x00059044 File Offset: 0x00057244
		protected Uri StaticFileLocalUri { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600295B RID: 10587
		protected abstract string StaticFileName { get; }

		// Token: 0x0600295C RID: 10588 RVA: 0x00059050 File Offset: 0x00057250
		public static OwaStartPage Parse(HttpWebResponseWrapper response)
		{
			Owa14StartPage owa14StartPage = null;
			Owa15StartPage owa15StartPage = null;
			OwaStartPage owaStartPage;
			if (Owa14StartPage.TryParse(response, out owa14StartPage))
			{
				owaStartPage = owa14StartPage;
			}
			else
			{
				if (!Owa15StartPage.TryParse(response, out owa15StartPage))
				{
					throw new MissingKeywordException(MonitoringWebClientStrings.MissingOwaStartPage(string.Format("{0},{1}", "forms_premium_startpage_aspx", "Program.main")), response.Request, response, "OWA start page");
				}
				owaStartPage = owa15StartPage;
			}
			if (owaStartPage.StaticFileLocalUri == null)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingStaticFile(owaStartPage.StaticFileName), response.Request, response, "OWA static files");
			}
			owaStartPage.FinalUri = response.Request.RequestUri;
			if (!owaStartPage.StaticFileLocalUri.IsAbsoluteUri)
			{
				owaStartPage.StaticFileUri = new Uri(response.Request.RequestUri, owaStartPage.StaticFileLocalUri);
			}
			else
			{
				owaStartPage.StaticFileUri = owaStartPage.StaticFileLocalUri;
			}
			return owaStartPage;
		}
	}
}
