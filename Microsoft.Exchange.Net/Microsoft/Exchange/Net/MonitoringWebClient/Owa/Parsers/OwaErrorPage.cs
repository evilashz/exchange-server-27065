using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers
{
	// Token: 0x020007D3 RID: 2003
	internal class OwaErrorPage
	{
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x00059247 File Offset: 0x00057447
		// (set) Token: 0x06002965 RID: 10597 RVA: 0x0005924F File Offset: 0x0005744F
		public FailureReason FailureReason { get; private set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x00059258 File Offset: 0x00057458
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x00059260 File Offset: 0x00057460
		public string ExceptionType { get; private set; }

		// Token: 0x06002968 RID: 10600 RVA: 0x00059269 File Offset: 0x00057469
		private OwaErrorPage()
		{
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x00059271 File Offset: 0x00057471
		internal OwaErrorPage(FailureReason reason)
		{
			this.FailureReason = reason;
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00059280 File Offset: 0x00057480
		public static bool TryParse(HttpWebResponseWrapper response, out OwaErrorPage errorPage)
		{
			if (!OwaErrorPage.ContainsErrorPage(response))
			{
				errorPage = null;
				return false;
			}
			FailureReason failureReason = FailureReason.OwaErrorPage;
			string text = response.Headers["X-Mserv-Error"];
			string text2 = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text;
				failureReason = FailureReason.OwaMServErrorPage;
			}
			else
			{
				text2 = response.Headers["X-OWA-Error"];
				if (string.IsNullOrEmpty(text2))
				{
					text2 = response.Headers["X-Auth-Error"];
				}
				if (!string.IsNullOrEmpty(text2))
				{
					if (text2.StartsWith("Microsoft.Exchange.Net.Mserve", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("Microsoft.Exchange.Data.Directory.SystemConfiguration.InvalidPartnerIdException", StringComparison.OrdinalIgnoreCase))
					{
						failureReason = FailureReason.OwaMServErrorPage;
					}
					else if (text2.StartsWith("Microsoft.Exchange.Data.Storage.IllegalCrossServerConnectionException", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("Microsoft.Exchange.Data.Storage.WrongServerException", StringComparison.OrdinalIgnoreCase))
					{
						failureReason = FailureReason.PassiveDatabase;
					}
					else if (text2.StartsWith("Microsoft.Exchange.Data.Storage", StringComparison.OrdinalIgnoreCase))
					{
						failureReason = FailureReason.OwaMailboxErrorPage;
					}
					else if (text2.StartsWith("Microsoft.Exchange.Data.Directory", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("Microsoft.Exchange.Security.Authorization.AuthzException", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("LoadOrganzationProperties", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("Microsoft.Exchange.Clients.Owa.Core.OwaCreateClientSecurityContextFailedException", StringComparison.OrdinalIgnoreCase))
					{
						failureReason = FailureReason.OwaActiveDirectoryErrorPage;
					}
				}
			}
			errorPage = new OwaErrorPage();
			errorPage.FailureReason = failureReason;
			errorPage.ExceptionType = text2;
			return true;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000593A4 File Offset: 0x000575A4
		private static bool ContainsErrorPage(HttpWebResponseWrapper response)
		{
			return (response.Headers["X-OWA-Error"] != null && (response.StatusCode != HttpStatusCode.Found || (response.StatusCode == HttpStatusCode.Found && response.Headers["Location"].IndexOf("errorfe.aspx", StringComparison.OrdinalIgnoreCase) >= 0))) || (response.Body != null && (response.Body.IndexOf("698798E9-889B-4145-ACFC-474C378C7B4F", StringComparison.OrdinalIgnoreCase) >= 0 || response.Body.IndexOf("FDCB17B5-BE4A-492D-BA37-A74E4E1AF7BF", StringComparison.OrdinalIgnoreCase) >= 0));
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00059432 File Offset: 0x00057632
		public override string ToString()
		{
			return string.Format("ErrorPageFailureReason: {0}, Exception type: {1}", this.FailureReason, this.ExceptionType);
		}

		// Token: 0x0400249D RID: 9373
		internal const string ExceptionHeaderName = "X-OWA-Error";

		// Token: 0x0400249E RID: 9374
		internal const string MservExceptionHeaderName = "X-Mserv-Error";

		// Token: 0x0400249F RID: 9375
		internal const string OwaErrorPageMarker = "698798E9-889B-4145-ACFC-474C378C7B4F";

		// Token: 0x040024A0 RID: 9376
		internal const string OwaLiveIdErrorPageMarker = "FDCB17B5-BE4A-492D-BA37-A74E4E1AF7BF";
	}
}
