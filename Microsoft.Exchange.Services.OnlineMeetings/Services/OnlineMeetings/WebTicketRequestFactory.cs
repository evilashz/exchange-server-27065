using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200003D RID: 61
	internal class WebTicketRequestFactory : UcwaRequestFactory
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00007C58 File Offset: 0x00005E58
		public WebTicketRequestFactory(string webTicket)
		{
			this.webTicket = webTicket;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007C67 File Offset: 0x00005E67
		public override string LandingPageToken
		{
			get
			{
				return "webticket";
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007C70 File Offset: 0x00005E70
		protected override HttpWebRequest CreateHttpWebRequest(string httpMethod, string url)
		{
			HttpWebRequest httpWebRequest = base.CreateHttpWebRequest(httpMethod, url);
			httpWebRequest.Headers.Add("X-MS-WebTicket", this.webTicket);
			return httpWebRequest;
		}

		// Token: 0x0400016E RID: 366
		private readonly string webTicket;
	}
}
