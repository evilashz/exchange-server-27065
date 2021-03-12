using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers
{
	// Token: 0x020007A6 RID: 1958
	internal class EcpStartPage
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x00053E81 File Offset: 0x00052081
		// (set) Token: 0x0600278A RID: 10122 RVA: 0x00053E89 File Offset: 0x00052089
		public Uri FinalUri { get; private set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00053E92 File Offset: 0x00052092
		// (set) Token: 0x0600278C RID: 10124 RVA: 0x00053E9A File Offset: 0x0005209A
		public Uri StaticFileUri { get; protected set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00053EA3 File Offset: 0x000520A3
		// (set) Token: 0x0600278E RID: 10126 RVA: 0x00053EAB File Offset: 0x000520AB
		protected Uri StaticFileLocalUri { get; set; }

		// Token: 0x0600278F RID: 10127 RVA: 0x00053EB4 File Offset: 0x000520B4
		public static EcpStartPage Parse(HttpWebResponseWrapper response)
		{
			if (response.Body == null || response.Body.IndexOf("4818bc65-fef6-4044-bebf-48f21be0a9d3", StringComparison.OrdinalIgnoreCase) < 0)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingEcpStartPage("4818bc65-fef6-4044-bebf-48f21be0a9d3"), response.Request, response, "4818bc65-fef6-4044-bebf-48f21be0a9d3");
			}
			EcpStartPage ecpStartPage = new EcpStartPage();
			foreach (string fileName in EcpStartPage.StaticFileNames)
			{
				string text = ParsingUtility.ParseFilePath(response, fileName);
				if (!string.IsNullOrEmpty(text))
				{
					ecpStartPage.StaticFileLocalUri = new Uri(text, UriKind.RelativeOrAbsolute);
					break;
				}
			}
			if (ecpStartPage.StaticFileLocalUri != null)
			{
				if (!ecpStartPage.StaticFileLocalUri.IsAbsoluteUri)
				{
					ecpStartPage.StaticFileUri = new Uri(response.Request.RequestUri, ecpStartPage.StaticFileLocalUri);
				}
				else
				{
					ecpStartPage.StaticFileUri = ecpStartPage.StaticFileLocalUri;
				}
			}
			ecpStartPage.FinalUri = response.Request.RequestUri;
			return ecpStartPage;
		}

		// Token: 0x040023AC RID: 9132
		internal const string StartPageMarker = "4818bc65-fef6-4044-bebf-48f21be0a9d3";

		// Token: 0x040023AD RID: 9133
		internal static string[] StaticFileNames = new string[]
		{
			"clear1x1.gif",
			"common.js"
		};
	}
}
