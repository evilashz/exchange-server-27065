using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000792 RID: 1938
	internal class LiveIdSamlTokenPage : LiveIdBasePage
	{
		// Token: 0x0600267A RID: 9850 RVA: 0x0005101C File Offset: 0x0004F21C
		public static bool TryParse(HttpWebResponseWrapper response, out LiveIdSamlTokenPage liveIdSamlTokenPage)
		{
			LiveIdSamlTokenPage liveIdSamlTokenPage2 = new LiveIdSamlTokenPage();
			liveIdSamlTokenPage2.SetPostUrl(ParsingUtility.ParseFormAction(response), response.Request);
			liveIdSamlTokenPage2.HiddenFields = ParsingUtility.ParseHiddenFields(response);
			if (!liveIdSamlTokenPage2.HiddenFields.ContainsKey("wresult"))
			{
				liveIdSamlTokenPage = null;
				return false;
			}
			liveIdSamlTokenPage = liveIdSamlTokenPage2;
			return true;
		}
	}
}
