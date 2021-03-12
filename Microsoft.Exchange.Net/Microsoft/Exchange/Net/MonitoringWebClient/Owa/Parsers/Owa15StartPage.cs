using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers
{
	// Token: 0x020007D2 RID: 2002
	internal class Owa15StartPage : OwaStartPage
	{
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x00059186 File Offset: 0x00057386
		protected override string StaticFileName
		{
			get
			{
				return string.Format("{0}, {1}", "microsoft.exchange.clients.owa2.client.core.framework.js", "preboot.js");
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0005919C File Offset: 0x0005739C
		public static bool TryParse(HttpWebResponseWrapper response, out Owa15StartPage result)
		{
			if (response.Body == null)
			{
				result = null;
				return false;
			}
			if (response.Body.IndexOf("Program.main", StringComparison.OrdinalIgnoreCase) < 0 && response.Body.IndexOf("StartPage.start", StringComparison.OrdinalIgnoreCase) < 0)
			{
				result = null;
				return false;
			}
			result = new Owa15StartPage();
			string text = ParsingUtility.ParseFilePath(response, "preboot.js");
			if (string.IsNullOrEmpty(text))
			{
				text = ParsingUtility.ParseFilePath(response, "preboot.0.js");
			}
			if (string.IsNullOrEmpty(text))
			{
				text = ParsingUtility.ParseFilePath(response, "microsoft.exchange.clients.owa2.client.core.framework.js");
			}
			if (string.IsNullOrEmpty(text))
			{
				result.StaticFileLocalUri = null;
			}
			else
			{
				result.StaticFileLocalUri = new Uri(text, UriKind.RelativeOrAbsolute);
			}
			return true;
		}

		// Token: 0x04002498 RID: 9368
		internal const string Df8StartPageMarker = "StartPage.start";

		// Token: 0x04002499 RID: 9369
		internal const string StartPageMarker = "Program.main";

		// Token: 0x0400249A RID: 9370
		private const string gaStaticFileName = "preboot.js";

		// Token: 0x0400249B RID: 9371
		private const string gaStaticFileName2 = "preboot.0.js";

		// Token: 0x0400249C RID: 9372
		private const string rtmStaticFileName = "microsoft.exchange.clients.owa2.client.core.framework.js";
	}
}
