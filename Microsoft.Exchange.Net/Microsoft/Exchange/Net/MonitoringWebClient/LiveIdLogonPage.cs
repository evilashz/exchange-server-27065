using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000785 RID: 1925
	internal class LiveIdLogonPage : LiveIdBasePage
	{
		// Token: 0x0600263F RID: 9791 RVA: 0x00050544 File Offset: 0x0004E744
		public static bool TryParse(HttpWebResponseWrapper response, out LiveIdLogonPage result, out Exception exception)
		{
			result = new LiveIdLogonPage();
			exception = null;
			try
			{
				result.SetPostUrl(ParsingUtility.ParseJavascriptStringVariable(response, "srf_uPost"), response.Request);
			}
			catch (MissingKeywordException ex)
			{
				try
				{
					result.SetPostUrl(ParsingUtility.ParseJsonField(response, "urlPost"), response.Request);
				}
				catch (MissingKeywordException ex2)
				{
					result.SetPostUrl(ParsingUtility.ParseFormAction(response), response.Request);
					if (string.IsNullOrEmpty(result.PostUrl))
					{
						exception = new MissingKeywordException(ex.Message + ex2.Message + MonitoringWebClientStrings.MissingFormAction, response.Request, response, "srf_uPost,urlPost,formAction");
					}
				}
			}
			if (exception == null)
			{
				result.HiddenFields = ParsingUtility.ParseHiddenFields(response);
				return true;
			}
			return false;
		}
	}
}
