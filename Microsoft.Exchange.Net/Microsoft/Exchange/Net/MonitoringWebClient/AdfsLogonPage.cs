using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000786 RID: 1926
	internal class AdfsLogonPage : LiveIdLogonPage
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00050618 File Offset: 0x0004E818
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x00050620 File Offset: 0x0004E820
		public string UserNameFieldName { get; protected set; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00050629 File Offset: 0x0004E829
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x00050631 File Offset: 0x0004E831
		public string PasswordFieldName { get; protected set; }

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x0005063A File Offset: 0x0004E83A
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x00050642 File Offset: 0x0004E842
		public bool IsIntegratedAuthChallenge { get; protected set; }

		// Token: 0x06002647 RID: 9799 RVA: 0x0005064C File Offset: 0x0004E84C
		public static bool TryParse(HttpWebResponseWrapper response, out AdfsLogonPage result)
		{
			if (!AdfsLogonPage.IsAdfsRequest(response.Request))
			{
				result = null;
				return false;
			}
			result = new AdfsLogonPage();
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				result.IsIntegratedAuthChallenge = true;
				result.SetPostUrl(response.Request.RequestUri, response.Request);
				return true;
			}
			result.IsIntegratedAuthChallenge = false;
			result.SetPostUrl(ParsingUtility.ParseFormAction(response), response.Request);
			result.HiddenFields = ParsingUtility.ParseInputFields(response);
			foreach (string text in result.HiddenFields.Keys)
			{
				if (AdfsLogonPage.UserNameMarkers.ContainsMatchingSubstring(text))
				{
					result.UserNameFieldName = text;
				}
				if (AdfsLogonPage.PasswordMarkers.ContainsMatchingSubstring(text))
				{
					result.PasswordFieldName = text;
				}
			}
			if (result.UserNameFieldName == null)
			{
				string text2 = string.Join(", ", AdfsLogonPage.UserNameMarkers);
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingUserNameFieldFromAdfsResponse(text2), response.Request, response, text2);
			}
			if (result.PasswordFieldName == null)
			{
				string text3 = string.Join(", ", AdfsLogonPage.PasswordMarkers);
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingPasswordFieldFromAdfsResponse(text3), response.Request, response, text3);
			}
			return true;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00050794 File Offset: 0x0004E994
		internal static bool IsAdfsRequest(HttpWebRequestWrapper request)
		{
			return request.RequestUri.PathAndQuery.IndexOf("adfs", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0400230F RID: 8975
		private static readonly string[] UserNameMarkers = new string[]
		{
			"username",
			"email"
		};

		// Token: 0x04002310 RID: 8976
		private static readonly string[] PasswordMarkers = new string[]
		{
			"password"
		};
	}
}
