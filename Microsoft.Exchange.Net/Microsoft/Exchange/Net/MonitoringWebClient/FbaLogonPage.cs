using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200078C RID: 1932
	internal class FbaLogonPage
	{
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x00050ADD File Offset: 0x0004ECDD
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x00050AE5 File Offset: 0x0004ECE5
		public Dictionary<string, string> HiddenFields { get; protected set; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x00050AEE File Offset: 0x0004ECEE
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x00050AF6 File Offset: 0x0004ECF6
		public string PostTarget { get; protected set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x00050AFF File Offset: 0x0004ECFF
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x00050B07 File Offset: 0x0004ED07
		public Uri StaticFileUri { get; set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00050B10 File Offset: 0x0004ED10
		public string HiddenFieldsString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this.HiddenFields.Keys)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append("&");
					}
					stringBuilder.AppendFormat("{0}={1}", text, HttpUtility.UrlEncode(this.HiddenFields[text]));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00050BA0 File Offset: 0x0004EDA0
		public static FbaLogonPage Parse(HttpWebResponseWrapper response)
		{
			if (response.Body == null || response.Body.IndexOf("{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}", StringComparison.OrdinalIgnoreCase) < 0)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingOwaFbaPage("{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}"), response.Request, response, "{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}");
			}
			FbaLogonPage fbaLogonPage = new FbaLogonPage();
			fbaLogonPage.PostTarget = ParsingUtility.ParseFormAction(response);
			fbaLogonPage.HiddenFields = ParsingUtility.ParseHiddenFields(response);
			string text = ParsingUtility.ParseFilePath(response, "flogon.js");
			if (!string.IsNullOrEmpty(text))
			{
				fbaLogonPage.StaticFileUri = new Uri(text, UriKind.RelativeOrAbsolute);
				if (!fbaLogonPage.StaticFileUri.IsAbsoluteUri)
				{
					fbaLogonPage.StaticFileUri = new Uri(response.Request.RequestUri, text);
				}
			}
			return fbaLogonPage;
		}

		// Token: 0x0400231D RID: 8989
		private const string LogonPageMarker = "{57A118C6-2DA9-419d-BE9A-F92B0F9A418B}";

		// Token: 0x0400231E RID: 8990
		private const string staticFileName = "flogon.js";
	}
}
