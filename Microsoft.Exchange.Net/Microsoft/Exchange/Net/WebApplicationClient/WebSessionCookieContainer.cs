using System;
using System.Net;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B30 RID: 2864
	internal class WebSessionCookieContainer : CookieContainer
	{
		// Token: 0x06003DBF RID: 15807 RVA: 0x000A0C2D File Offset: 0x0009EE2D
		public WebSessionCookieContainer(WebSession webSession)
		{
			if (webSession == null)
			{
				throw new ArgumentNullException("webSession");
			}
			this.WebSession = webSession;
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x000A0C4A File Offset: 0x0009EE4A
		// (set) Token: 0x06003DC1 RID: 15809 RVA: 0x000A0C52 File Offset: 0x0009EE52
		public WebSession WebSession { get; private set; }
	}
}
