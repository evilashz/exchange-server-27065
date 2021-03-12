using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B23 RID: 2851
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class HttpWebResponseEventArgs : HttpWebRequestEventArgs
	{
		// Token: 0x06003D8C RID: 15756 RVA: 0x000A04A0 File Offset: 0x0009E6A0
		public HttpWebResponseEventArgs(HttpWebRequest request, HttpWebResponse response) : base(request)
		{
			this.Response = response;
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		// (set) Token: 0x06003D8E RID: 15758 RVA: 0x000A04B8 File Offset: 0x0009E6B8
		public HttpWebResponse Response { get; set; }
	}
}
