using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TraceToHeadersLogger : ITraceLogger
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		public TraceToHeadersLogger(NameValueCollection headers)
		{
			ArgumentValidator.ThrowIfNull("headers", headers);
			this.headers = headers;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B8DD File Offset: 0x00009ADD
		public void LogTraces(ITracer tracer)
		{
			if (tracer == null || NullTracer.Instance.Equals(tracer))
			{
				return;
			}
			this.StampTracesOnHeader(tracer, this.headers);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B8FD File Offset: 0x00009AFD
		private void StampTracesOnHeader(ITracer tracer, NameValueCollection headers)
		{
			headers["X-Exchange-Server-Traces"] = this.CollectTraces(tracer);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B914 File Offset: 0x00009B14
		private string CollectTraces(ITracer tracer)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				tracer.Dump(stringWriter, true, true);
			}
			return this.SanitizeHttpHeaderValue(stringBuilder.ToString());
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B964 File Offset: 0x00009B64
		private string SanitizeHttpHeaderValue(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			foreach (char c in s)
			{
				if (c == '\r')
				{
					stringBuilder.Append("\\r");
				}
				else if (c == '\n')
				{
					stringBuilder.Append("\\n");
				}
				else if (c >= ' ' && c < '\u007f')
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('.');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000513 RID: 1299
		private const char Space = ' ';

		// Token: 0x04000514 RID: 1300
		private const char Delete = '\u007f';

		// Token: 0x04000515 RID: 1301
		private const string ServerTracesHeaderName = "X-Exchange-Server-Traces";

		// Token: 0x04000516 RID: 1302
		private readonly NameValueCollection headers;
	}
}
