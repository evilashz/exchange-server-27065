using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200078E RID: 1934
	internal class FbaSilentRedirectPage
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x00050CDE File Offset: 0x0004EEDE
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x00050CE6 File Offset: 0x0004EEE6
		public Uri Destination { get; set; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00050CEF File Offset: 0x0004EEEF
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x00050CF7 File Offset: 0x0004EEF7
		public Dictionary<string, string> HiddenFields { get; protected set; }

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x00050D00 File Offset: 0x0004EF00
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

		// Token: 0x0600266E RID: 9838 RVA: 0x00050D90 File Offset: 0x0004EF90
		public static bool TryParse(HttpWebResponseWrapper response, out FbaSilentRedirectPage result)
		{
			if (response.Body == null || string.IsNullOrEmpty(response.Headers["X-OWA-Destination"]))
			{
				result = null;
				return false;
			}
			result = new FbaSilentRedirectPage();
			result.HiddenFields = ParsingUtility.ParseHiddenFields(response);
			if (!result.HiddenFields.ContainsKey("destination"))
			{
				result = null;
				return false;
			}
			result.Destination = new Uri(result.HiddenFields["destination"]);
			return true;
		}

		// Token: 0x04002324 RID: 8996
		private const string DestinationHeader = "X-OWA-Destination";

		// Token: 0x04002325 RID: 8997
		private const string DestinationHiddenFieldName = "destination";
	}
}
