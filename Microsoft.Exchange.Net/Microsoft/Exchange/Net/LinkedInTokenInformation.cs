using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200075B RID: 1883
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInTokenInformation
	{
		// Token: 0x060024EF RID: 9455 RVA: 0x0004D08D File Offset: 0x0004B28D
		internal LinkedInTokenInformation()
		{
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x0004D095 File Offset: 0x0004B295
		// (set) Token: 0x060024F1 RID: 9457 RVA: 0x0004D09D File Offset: 0x0004B29D
		public string Token { get; internal set; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x0004D0A6 File Offset: 0x0004B2A6
		// (set) Token: 0x060024F3 RID: 9459 RVA: 0x0004D0AE File Offset: 0x0004B2AE
		public string Secret { get; internal set; }

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x0004D0B7 File Offset: 0x0004B2B7
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x0004D0BF File Offset: 0x0004B2BF
		public string OAuthAccessTokenUrl { get; internal set; }
	}
}
