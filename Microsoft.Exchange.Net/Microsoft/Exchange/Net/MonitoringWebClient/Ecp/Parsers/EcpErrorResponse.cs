using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers
{
	// Token: 0x020007A4 RID: 1956
	internal class EcpErrorResponse
	{
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x00053C47 File Offset: 0x00051E47
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x00053C4F File Offset: 0x00051E4F
		public FailureReason FailureReason { get; private set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x00053C58 File Offset: 0x00051E58
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x00053C60 File Offset: 0x00051E60
		public string ExceptionType { get; private set; }

		// Token: 0x06002781 RID: 10113 RVA: 0x00053C69 File Offset: 0x00051E69
		private EcpErrorResponse()
		{
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00053C71 File Offset: 0x00051E71
		internal EcpErrorResponse(FailureReason reason)
		{
			this.FailureReason = reason;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00053C80 File Offset: 0x00051E80
		public static bool TryParse(HttpWebResponseWrapper response, out EcpErrorResponse errorPage)
		{
			errorPage = null;
			if (response.Body == null)
			{
				return false;
			}
			FailureReason failureReason;
			if (response.Body.IndexOf("6DD23A7E-5C94-4d52-B537-2EA53079B2D5", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				failureReason = FailureReason.EcpErrorPage;
			}
			else if (response.Body.IndexOf("\"ErrorRecords\":[{", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				failureReason = FailureReason.EcpJsonResultErrorResponse;
			}
			else
			{
				if (response.Headers["jsonerror"] == null || !response.Headers["jsonerror"].Equals("true", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				failureReason = FailureReason.EcpJsonErrorResponse;
			}
			string text = response.Headers["X-ECP-Error"];
			if (string.IsNullOrEmpty(text) && failureReason == FailureReason.EcpJsonResultErrorResponse)
			{
				Regex regex = new Regex("\"Type\":\"(?<ExceptionName>[^\"]*)\"", RegexOptions.IgnoreCase);
				if (regex.IsMatch(response.Body))
				{
					Match match = regex.Match(response.Body);
					text = match.Result("${ExceptionName}");
				}
				else
				{
					text = response.Body;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("Microsoft.Exchange.Data.Directory", StringComparison.OrdinalIgnoreCase))
				{
					if (text.EndsWith("OverBudgetException", StringComparison.OrdinalIgnoreCase))
					{
						failureReason = FailureReason.EcpErrorPage;
					}
					else
					{
						failureReason = FailureReason.EcpActiveDirectoryErrorResponse;
					}
				}
				else if (text.StartsWith("Microsoft.Exchange.Data.Storage", StringComparison.OrdinalIgnoreCase))
				{
					failureReason = FailureReason.EcpMailboxErrorResponse;
				}
			}
			errorPage = new EcpErrorResponse();
			errorPage.FailureReason = failureReason;
			errorPage.ExceptionType = text;
			return true;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00053DB3 File Offset: 0x00051FB3
		public override string ToString()
		{
			return string.Format("ErrorPageFailureReason: {0}, Exception type: {1}", this.FailureReason, this.ExceptionType);
		}

		// Token: 0x040023A3 RID: 9123
		internal const string EcpExceptionHeaderName = "X-ECP-Error";

		// Token: 0x040023A4 RID: 9124
		internal const string EcpErrorPageMarker = "6DD23A7E-5C94-4d52-B537-2EA53079B2D5";

		// Token: 0x040023A5 RID: 9125
		internal const string EcpJsonErrorHeaderName = "jsonerror";

		// Token: 0x040023A6 RID: 9126
		internal const string EcpJsonResultErrorMarker = "\"ErrorRecords\":[{";
	}
}
