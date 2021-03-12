using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000779 RID: 1913
	internal class LogonException : HttpWebResponseWrapperException
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x0004FFE4 File Offset: 0x0004E1E4
		public LogonException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, LogonErrorPage logonErrorPage) : base(message, request, response)
		{
			this.logonErrorPage = logonErrorPage;
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0004FFF7 File Offset: 0x0004E1F7
		public LogonErrorType LogonErrorType
		{
			get
			{
				return this.logonErrorPage.LogonErrorType;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x00050004 File Offset: 0x0004E204
		public override string ExceptionHint
		{
			get
			{
				if (this.LogonErrorType != LogonErrorType.Unknown)
				{
					return "LogonError: " + this.LogonErrorType.ToString();
				}
				return "LogonError: " + this.logonErrorPage.ErrorString;
			}
		}

		// Token: 0x040022FC RID: 8956
		private readonly LogonErrorPage logonErrorPage;
	}
}
