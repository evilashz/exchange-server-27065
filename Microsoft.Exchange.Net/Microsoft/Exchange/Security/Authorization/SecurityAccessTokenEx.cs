using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200002C RID: 44
	public class SecurityAccessTokenEx : ISecurityAccessTokenEx
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000621B File Offset: 0x0000441B
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00006223 File Offset: 0x00004423
		public SecurityIdentifier UserSid { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000622C File Offset: 0x0000442C
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006234 File Offset: 0x00004434
		public SidBinaryAndAttributes[] GroupSids { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000623D File Offset: 0x0000443D
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00006245 File Offset: 0x00004445
		public SidBinaryAndAttributes[] RestrictedGroupSids { get; set; }
	}
}
