using System;
using System.Net;
using System.Text;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200004F RID: 79
	public class ServerResponse
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000C6D2 File Offset: 0x0000A8D2
		public ServerResponse(Uri uri, HttpStatusCode statusCode, string contentType, TimeSpan responseTime, string text, string headerText)
		{
			this.uri = uri;
			this.statusCode = statusCode;
			this.contentType = contentType;
			this.responseTime = responseTime;
			this.text = text;
			this.headerText = headerText;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C707 File Offset: 0x0000A907
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000C70F File Offset: 0x0000A90F
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C717 File Offset: 0x0000A917
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000C71F File Offset: 0x0000A91F
		public TimeSpan ResponseTime
		{
			get
			{
				return this.responseTime;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C727 File Offset: 0x0000A927
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000C72F File Offset: 0x0000A92F
		public string HeaderText
		{
			get
			{
				return this.headerText;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C738 File Offset: 0x0000A938
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Uri: ");
			stringBuilder.AppendLine(this.uri.AbsoluteUri);
			stringBuilder.Append("Status code: ");
			stringBuilder.AppendLine(this.statusCode.ToString());
			stringBuilder.Append("Content type: ");
			stringBuilder.AppendLine(this.contentType);
			stringBuilder.Append("Response time: ");
			stringBuilder.AppendLine(this.responseTime.ToString());
			stringBuilder.Append("Response text: ");
			stringBuilder.AppendLine(this.text);
			stringBuilder.Append("Headers: ");
			stringBuilder.AppendLine(this.headerText);
			return stringBuilder.ToString();
		}

		// Token: 0x0400014D RID: 333
		private readonly string contentType;

		// Token: 0x0400014E RID: 334
		private readonly string headerText;

		// Token: 0x0400014F RID: 335
		private readonly string text;

		// Token: 0x04000150 RID: 336
		private readonly HttpStatusCode statusCode;

		// Token: 0x04000151 RID: 337
		private readonly TimeSpan responseTime;

		// Token: 0x04000152 RID: 338
		private readonly Uri uri;
	}
}
