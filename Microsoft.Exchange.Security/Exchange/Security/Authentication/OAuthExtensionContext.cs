using System;
using System.Web;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200010C RID: 268
	internal class OAuthExtensionContext
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00039248 File Offset: 0x00037448
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00039250 File Offset: 0x00037450
		public HttpContext HttpContext { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00039259 File Offset: 0x00037459
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00039261 File Offset: 0x00037461
		public OAuthTokenHandler TokenHandler { get; set; }
	}
}
