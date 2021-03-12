using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200078F RID: 1935
	internal class LiveIdCompactTokenPage : LiveIdBasePage
	{
		// Token: 0x06002670 RID: 9840 RVA: 0x00050E14 File Offset: 0x0004F014
		public static LiveIdCompactTokenPage Parse(HttpWebResponseWrapper response)
		{
			LiveIdLogonErrorPage liveIdLogonErrorPage;
			if (LiveIdLogonErrorPage.TryParse(response, out liveIdLogonErrorPage))
			{
				throw new LogonException(MonitoringWebClientStrings.LogonError(liveIdLogonErrorPage.ErrorString), response.Request, response, liveIdLogonErrorPage);
			}
			LiveIdCompactTokenPage liveIdCompactTokenPage = new LiveIdCompactTokenPage();
			liveIdCompactTokenPage.SetPostUrl(ParsingUtility.ParseFormAction(response), response.Request);
			liveIdCompactTokenPage.HiddenFields = ParsingUtility.ParseHiddenFields(response);
			if (!liveIdCompactTokenPage.HiddenFields.ContainsKey("t"))
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingLiveIdCompactToken(LiveIdBasePage.GetRedirectionLocation(response)), response.Request, response, "t");
			}
			return liveIdCompactTokenPage;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00050E98 File Offset: 0x0004F098
		public static bool TryParse(HttpWebResponseWrapper response, out LiveIdCompactTokenPage compactTokenPage)
		{
			bool result;
			try
			{
				compactTokenPage = LiveIdCompactTokenPage.Parse(response);
				result = true;
			}
			catch
			{
				compactTokenPage = null;
				result = false;
			}
			return result;
		}
	}
}
