using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers
{
	// Token: 0x020007D5 RID: 2005
	internal class OwaPreloadPage
	{
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x0005953D File Offset: 0x0005773D
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x00059545 File Offset: 0x00057745
		public Uri CdnUri { get; protected set; }

		// Token: 0x06002978 RID: 10616 RVA: 0x00059550 File Offset: 0x00057750
		public static OwaPreloadPage Parse(HttpWebResponseWrapper response)
		{
			if (!response.Body.Contains(OwaPreloadPage.PageMarker))
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingPreloadPage(OwaPreloadPage.PageMarker), response.Request, response, OwaPreloadPage.PageMarker);
			}
			Uri cdnUri;
			if (!Uri.TryCreate(ParsingUtility.ParseJavascriptStringVariable(response, "basePath"), UriKind.Absolute, out cdnUri))
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.BadPreloadPath("basePath"), response.Request, response, "basePath");
			}
			return new OwaPreloadPage
			{
				CdnUri = cdnUri
			};
		}

		// Token: 0x040024A8 RID: 9384
		private static string PageMarker = "{19A79CE9-889B-4145-2C4C-474C37DC7B4F}";
	}
}
