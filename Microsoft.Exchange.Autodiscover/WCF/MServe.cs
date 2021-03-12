using System;
using System.Configuration;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000071 RID: 113
	internal static class MServe
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00013FEC File Offset: 0x000121EC
		static MServe()
		{
			if (!int.TryParse(ConfigurationManager.AppSettings["PodSiteStartRange"], out MServe.podSiteStartRange))
			{
				MServe.podSiteStartRange = -1;
			}
			if (!int.TryParse(ConfigurationManager.AppSettings["PodSiteEndRange"], out MServe.podSiteEndRange))
			{
				MServe.podSiteEndRange = int.MaxValue;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00014054 File Offset: 0x00012254
		internal static string GetRedirectServer(string smtpAddress)
		{
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "MServe.GetRedirectServer. Entry. smtpAddress = {0}.", smtpAddress);
			IGlobalDirectorySession globalDirectorySession = new MServDirectorySession(MServe.podRedirectTemplate);
			string empty;
			if (!globalDirectorySession.TryGetRedirectServer(smtpAddress, out empty))
			{
				empty = string.Empty;
			}
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>(0L, "MServe.GetRedirectServer. Exit. smtpAddress = {0}, redirectServer = {1}.", smtpAddress, empty);
			return empty;
		}

		// Token: 0x040002DF RID: 735
		private static string podRedirectTemplate = ConfigurationManager.AppSettings["PodRedirectTemplate"];

		// Token: 0x040002E0 RID: 736
		private static int podSiteStartRange;

		// Token: 0x040002E1 RID: 737
		private static int podSiteEndRange;
	}
}
