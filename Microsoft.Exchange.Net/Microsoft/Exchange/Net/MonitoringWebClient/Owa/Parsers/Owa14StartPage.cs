using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers
{
	// Token: 0x020007D1 RID: 2001
	internal class Owa14StartPage : OwaStartPage
	{
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x00059126 File Offset: 0x00057326
		protected override string StaticFileName
		{
			get
			{
				return "clear1x1.gif";
			}
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00059130 File Offset: 0x00057330
		public static bool TryParse(HttpWebResponseWrapper response, out Owa14StartPage result)
		{
			if (response.Body == null || response.Body.IndexOf("forms_premium_startpage_aspx", StringComparison.OrdinalIgnoreCase) < 0)
			{
				result = null;
				return false;
			}
			result = new Owa14StartPage();
			result.StaticFileLocalUri = new Uri(ParsingUtility.ParseFilePath(response, "clear1x1.gif"), UriKind.RelativeOrAbsolute);
			return true;
		}

		// Token: 0x04002496 RID: 9366
		internal const string StartPageMarker = "forms_premium_startpage_aspx";

		// Token: 0x04002497 RID: 9367
		private const string staticFileName = "clear1x1.gif";
	}
}
