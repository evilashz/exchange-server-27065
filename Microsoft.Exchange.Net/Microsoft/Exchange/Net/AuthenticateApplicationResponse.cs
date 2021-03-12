using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000710 RID: 1808
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AuthenticateApplicationResponse
	{
		// Token: 0x0600221C RID: 8732 RVA: 0x0004628C File Offset: 0x0004448C
		internal AuthenticateApplicationResponse()
		{
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00046294 File Offset: 0x00044494
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x0004629C File Offset: 0x0004449C
		public HttpStatusCode Code { get; internal set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000462A5 File Offset: 0x000444A5
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x000462AD File Offset: 0x000444AD
		public string Body { get; internal set; }
	}
}
