using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000776 RID: 1910
	internal class HttpWebResponseWrapperException : Exception
	{
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x0004FD74 File Offset: 0x0004DF74
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x0004FD7C File Offset: 0x0004DF7C
		public HttpWebRequestWrapper Request { get; private set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x0004FD85 File Offset: 0x0004DF85
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x0004FD8D File Offset: 0x0004DF8D
		public HttpWebResponseWrapper Response { get; private set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x0004FD96 File Offset: 0x0004DF96
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x0004FD9E File Offset: 0x0004DF9E
		public WebExceptionStatus? Status { get; private set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x0004FDA7 File Offset: 0x0004DFA7
		public virtual string ExceptionHint
		{
			get
			{
				return "HttpWebResponseException: " + this.Status;
			}
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0004FDBE File Offset: 0x0004DFBE
		public HttpWebResponseWrapperException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response) : base(message)
		{
			this.Request = request;
			this.Response = response;
			this.Status = new WebExceptionStatus?(WebExceptionStatus.Success);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0004FDE1 File Offset: 0x0004DFE1
		public HttpWebResponseWrapperException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, WebExceptionStatus status) : this(message, request, response)
		{
			this.Status = new WebExceptionStatus?(status);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0004FDF9 File Offset: 0x0004DFF9
		public HttpWebResponseWrapperException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, Exception innerException) : this(message, request, innerException)
		{
			this.Response = response;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0004FE0C File Offset: 0x0004E00C
		public HttpWebResponseWrapperException(string message, HttpWebRequestWrapper request, Exception innerException) : base(message, innerException)
		{
			this.Request = request;
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x0004FE20 File Offset: 0x0004E020
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}: {1}{2}", base.GetType().FullName, base.Message, Environment.NewLine);
				if (this.Status != null)
				{
					stringBuilder.AppendFormat("WebExceptionStatus: {0}{1}", this.Status, Environment.NewLine);
				}
				if (this.Request != null)
				{
					stringBuilder.AppendFormat("{0}{1}{1}", this.Request.ToString(RequestResponseStringFormatOptions.TruncateCookies), Environment.NewLine);
				}
				if (this.Response != null)
				{
					stringBuilder.AppendFormat("{0}{1}{1}", this.Response.ToString(RequestResponseStringFormatOptions.TruncateCookies), Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
		}
	}
}
