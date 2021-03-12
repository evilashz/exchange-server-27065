using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws.Parsers
{
	// Token: 0x020007EC RID: 2028
	internal class RwsErrorResponse
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x0005CA25 File Offset: 0x0005AC25
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x0005CA2D File Offset: 0x0005AC2D
		public FailureReason FailureReason { get; private set; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x0005CA36 File Offset: 0x0005AC36
		// (set) Token: 0x06002A7A RID: 10874 RVA: 0x0005CA3E File Offset: 0x0005AC3E
		public string ExceptionType { get; private set; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x0005CA47 File Offset: 0x0005AC47
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x0005CA4F File Offset: 0x0005AC4F
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x06002A7D RID: 10877 RVA: 0x0005CA58 File Offset: 0x0005AC58
		private RwsErrorResponse()
		{
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x0005CA60 File Offset: 0x0005AC60
		internal RwsErrorResponse(FailureReason reason)
		{
			this.FailureReason = reason;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0005CA70 File Offset: 0x0005AC70
		public static bool TryParse(HttpWebResponseWrapper response, out RwsErrorResponse error)
		{
			error = null;
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return false;
			}
			FailureReason failureReason = FailureReason.Unknown;
			string text = response.Headers["X-RWS-Error"];
			if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("Microsoft.Exchange.Data.Directory", StringComparison.OrdinalIgnoreCase))
				{
					failureReason = FailureReason.RwsActiveDirectoryErrorResponse;
				}
				else if (text.StartsWith("Microsoft.Exchange.Management.ReportingTask.ConnectionFailedException", StringComparison.OrdinalIgnoreCase))
				{
					failureReason = FailureReason.RwsDataMartErrorResponse;
				}
				else
				{
					failureReason = FailureReason.RwsError;
				}
			}
			error = new RwsErrorResponse();
			error.FailureReason = failureReason;
			error.ExceptionType = text;
			error.StatusCode = response.StatusCode;
			return true;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0005CAF8 File Offset: 0x0005ACF8
		public override string ToString()
		{
			return string.Format("ErrorFailureReason: {0}, Exception type: {1}, Http status code: {2}", this.FailureReason, this.ExceptionType, this.StatusCode);
		}

		// Token: 0x04002538 RID: 9528
		internal const string RwsExceptionHeaderName = "X-RWS-Error";
	}
}
