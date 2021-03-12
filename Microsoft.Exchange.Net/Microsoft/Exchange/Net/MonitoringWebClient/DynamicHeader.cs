using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C1 RID: 1985
	public class DynamicHeader
	{
		// Token: 0x060028EB RID: 10475 RVA: 0x000578F4 File Offset: 0x00055AF4
		public DynamicHeader(string name, Func<Uri, string> valueDelegate)
		{
			this.name = name;
			this.valueDelegate = valueDelegate;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0005790C File Offset: 0x00055B0C
		internal void UpdateHeaders(HttpWebRequestWrapper request)
		{
			string text = this.valueDelegate(request.RequestUri);
			if (text != null)
			{
				request.Headers[this.name] = text;
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00057940 File Offset: 0x00055B40
		internal void UpdateHeaders(HttpWebRequest request)
		{
			Uri arg = new Uri(request.RequestUri.ToString().Replace(request.RequestUri.Host, request.Host));
			string text = this.valueDelegate(arg);
			if (text != null)
			{
				request.Headers[this.name] = text;
			}
		}

		// Token: 0x0400245F RID: 9311
		private readonly string name;

		// Token: 0x04002460 RID: 9312
		private Func<Uri, string> valueDelegate;
	}
}
