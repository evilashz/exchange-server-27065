using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000019 RID: 25
	public enum AdfsConfigErrorReason
	{
		// Token: 0x04000145 RID: 325
		DuplicateClaims,
		// Token: 0x04000146 RID: 326
		UpnClaimMissing,
		// Token: 0x04000147 RID: 327
		GroupSidsClaimMissing,
		// Token: 0x04000148 RID: 328
		InvalidUpn,
		// Token: 0x04000149 RID: 329
		NoCertificates,
		// Token: 0x0400014A RID: 330
		CertificatesMismatch
	}
}
