using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers
{
	// Token: 0x020007A5 RID: 1957
	internal class EcpHelpDeskStartPage
	{
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x00053DD0 File Offset: 0x00051FD0
		// (set) Token: 0x06002786 RID: 10118 RVA: 0x00053DD8 File Offset: 0x00051FD8
		public Uri FinalUri { get; private set; }

		// Token: 0x06002787 RID: 10119 RVA: 0x00053DE4 File Offset: 0x00051FE4
		public static EcpHelpDeskStartPage Parse(HttpWebResponseWrapper response)
		{
			if (response.Body == null || response.Body.IndexOf("4818bc65-fef6-4044-bebf-48f21be0a9d3", StringComparison.OrdinalIgnoreCase) < 0)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingEcpStartPage("4818bc65-fef6-4044-bebf-48f21be0a9d3"), response.Request, response, "4818bc65-fef6-4044-bebf-48f21be0a9d3");
			}
			if (response.Body == null || response.Body.IndexOf("EsoBarMsg", StringComparison.Ordinal) < 0)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingEcpStartPage("EsoBarMsg"), response.Request, response, "EsoBarMsg");
			}
			return new EcpHelpDeskStartPage
			{
				FinalUri = response.Request.RequestUri
			};
		}

		// Token: 0x040023A9 RID: 9129
		internal const string StartPageMarker = "4818bc65-fef6-4044-bebf-48f21be0a9d3";

		// Token: 0x040023AA RID: 9130
		internal const string EsoMessageDivId = "EsoBarMsg";
	}
}
