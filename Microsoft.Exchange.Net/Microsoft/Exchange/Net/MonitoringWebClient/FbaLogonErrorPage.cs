using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200078B RID: 1931
	internal class FbaLogonErrorPage : LogonErrorPage
	{
		// Token: 0x0600265A RID: 9818 RVA: 0x00050A30 File Offset: 0x0004EC30
		private FbaLogonErrorPage(string errorReason) : base("reason=" + errorReason)
		{
			if (errorReason.Equals("2", StringComparison.OrdinalIgnoreCase))
			{
				this.logonErrorType = LogonErrorType.BadUserNameOrPassword;
				return;
			}
			this.logonErrorType = LogonErrorType.Unknown;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00050A60 File Offset: 0x0004EC60
		public static bool TryParse(HttpWebResponseWrapper response, out FbaLogonErrorPage fbaLogonErrorPage)
		{
			fbaLogonErrorPage = null;
			if (response.StatusCode != HttpStatusCode.Found || response.Headers["Location"] == null)
			{
				return false;
			}
			Uri uri;
			if (!Uri.TryCreate(response.Headers["Location"], UriKind.Absolute, out uri))
			{
				return false;
			}
			if (uri.PathAndQuery.IndexOf("logon.aspx", StringComparison.OrdinalIgnoreCase) < 0)
			{
				return false;
			}
			string errorReason;
			if (!ParsingUtility.TryParseQueryParameter(uri, "reason", out errorReason))
			{
				return false;
			}
			fbaLogonErrorPage = new FbaLogonErrorPage(errorReason);
			return true;
		}
	}
}
