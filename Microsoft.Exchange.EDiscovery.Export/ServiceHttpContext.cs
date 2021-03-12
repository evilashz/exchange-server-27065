using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000046 RID: 70
	public class ServiceHttpContext
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00016D41 File Offset: 0x00014F41
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00016D49 File Offset: 0x00014F49
		internal string AnchorMailbox { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00016D52 File Offset: 0x00014F52
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00016D5A File Offset: 0x00014F5A
		internal Guid ClientRequestId { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00016D63 File Offset: 0x00014F63
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00016D6B File Offset: 0x00014F6B
		internal WebHeaderCollection ResponseHttpHeaders { get; set; }

		// Token: 0x06000625 RID: 1573 RVA: 0x00016D74 File Offset: 0x00014F74
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			ServiceHttpContext.AppendToString(stringBuilder, "X-AnchorMailbox", this.AnchorMailbox);
			ServiceHttpContext.AppendToString(stringBuilder, "client-request-id", this.ClientRequestId.ToString());
			if (this.ResponseHttpHeaders != null)
			{
				stringBuilder.Append(this.ResponseHttpHeaders.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00016DE0 File Offset: 0x00014FE0
		internal void SetRequestHttpHeaders(WebRequest request)
		{
			ServiceHttpContext.SetHttpHeader(request, "X-AnchorMailbox", this.AnchorMailbox);
			ServiceHttpContext.SetHttpHeader(request, "client-request-id", this.ClientRequestId.ToString());
			ServiceHttpContext.SetHttpHeader(request, "return-client-request-id", "true");
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00016E30 File Offset: 0x00015030
		internal void UpdateContextFromResponse(WebResponse response)
		{
			this.ResponseHttpHeaders = response.Headers;
			HttpWebResponse httpWebResponse = response as HttpWebResponse;
			if (httpWebResponse != null && httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				Tracer.TraceError("ServiceHttpContext.UpdateContextFromResponse: HTTP status code is '{0}', it should be 'OK'. \nServiceHttpContext:\n{1}", new object[]
				{
					httpWebResponse.StatusCode,
					this
				});
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00016E84 File Offset: 0x00015084
		private static void AppendToString(StringBuilder sb, string name, string value)
		{
			sb.Append(name);
			sb.Append(":");
			sb.Append(value);
			sb.AppendLine();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00016EA9 File Offset: 0x000150A9
		private static void SetHttpHeader(WebRequest request, string name, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				request.Headers.Remove(name);
				return;
			}
			request.Headers.Set(name, value);
		}

		// Token: 0x040001B8 RID: 440
		private const string AnchorMailboxHeaderName = "X-AnchorMailbox";

		// Token: 0x040001B9 RID: 441
		private const string ClientRequestIdHeaderName = "client-request-id";

		// Token: 0x040001BA RID: 442
		private const string ReturnClientRequestIdHeaderName = "return-client-request-id";
	}
}
