using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000758 RID: 1880
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInResponse
	{
		// Token: 0x060024E0 RID: 9440 RVA: 0x0004D00F File Offset: 0x0004B20F
		internal LinkedInResponse()
		{
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0004D017 File Offset: 0x0004B217
		// (set) Token: 0x060024E2 RID: 9442 RVA: 0x0004D01F File Offset: 0x0004B21F
		public HttpStatusCode Code { get; internal set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x0004D028 File Offset: 0x0004B228
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x0004D030 File Offset: 0x0004B230
		public string Body { get; internal set; }
	}
}
