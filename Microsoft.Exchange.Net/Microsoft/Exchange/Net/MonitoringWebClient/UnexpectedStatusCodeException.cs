using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000780 RID: 1920
	internal class UnexpectedStatusCodeException : HttpWebResponseWrapperException
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000502CB File Offset: 0x0004E4CB
		// (set) Token: 0x0600260A RID: 9738 RVA: 0x000502D3 File Offset: 0x0004E4D3
		public HttpStatusCode[] ExpectedStatusCodes { get; private set; }

		// Token: 0x0600260B RID: 9739 RVA: 0x000502DC File Offset: 0x0004E4DC
		public UnexpectedStatusCodeException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, HttpStatusCode[] expectedCodes, HttpStatusCode actualStatusCode) : base(message, request, response)
		{
			this.ExpectedStatusCodes = expectedCodes;
			this.actualStatusCode = actualStatusCode;
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000502F8 File Offset: 0x0004E4F8
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.GetType().FullName + Environment.NewLine);
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (HttpStatusCode httpStatusCode in this.ExpectedStatusCodes)
				{
					if (stringBuilder2.Length != 0)
					{
						stringBuilder2.Append(", ");
					}
					stringBuilder2.Append(httpStatusCode.ToString());
				}
				stringBuilder.Append(MonitoringWebClientStrings.ExpectedStatusCode(stringBuilder2.ToString()));
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(MonitoringWebClientStrings.ActualStatusCode(this.actualStatusCode.ToString()));
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(base.Message);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000503D6 File Offset: 0x0004E5D6
		public override string ExceptionHint
		{
			get
			{
				return "UnexpectedHttpCode: " + this.actualStatusCode.ToString();
			}
		}

		// Token: 0x04002306 RID: 8966
		private HttpStatusCode actualStatusCode;
	}
}
