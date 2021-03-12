using System;
using System.Net;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B1C RID: 2844
	internal class RedirectResponse : TextResponse
	{
		// Token: 0x06003D71 RID: 15729 RVA: 0x000A0221 File Offset: 0x0009E421
		public RedirectResponse()
		{
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x000A0229 File Offset: 0x0009E429
		public RedirectResponse(HttpWebResponse response)
		{
			this.SetResponse(response);
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000A0238 File Offset: 0x0009E438
		public override void SetResponse(HttpWebResponse response)
		{
			base.SetResponse(response);
			this.RedirectUrl = response.Headers[HttpResponseHeader.Location];
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x000A0254 File Offset: 0x0009E454
		public bool IsRedirect
		{
			get
			{
				return base.StatusCode == HttpStatusCode.Found;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x000A0263 File Offset: 0x0009E463
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x000A026B File Offset: 0x0009E46B
		public string RedirectUrl { get; private set; }
	}
}
