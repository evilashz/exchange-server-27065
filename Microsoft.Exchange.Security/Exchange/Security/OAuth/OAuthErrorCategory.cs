using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D5 RID: 213
	internal enum OAuthErrorCategory
	{
		// Token: 0x040006B5 RID: 1717
		Probe,
		// Token: 0x040006B6 RID: 1718
		InvalidSignature = 2000000,
		// Token: 0x040006B7 RID: 1719
		InvalidToken,
		// Token: 0x040006B8 RID: 1720
		TokenExpired,
		// Token: 0x040006B9 RID: 1721
		InvalidResource,
		// Token: 0x040006BA RID: 1722
		InvalidTenant,
		// Token: 0x040006BB RID: 1723
		InvalidUser,
		// Token: 0x040006BC RID: 1724
		InvalidClient,
		// Token: 0x040006BD RID: 1725
		InternalError,
		// Token: 0x040006BE RID: 1726
		InvalidGrant,
		// Token: 0x040006BF RID: 1727
		InvalidCertificate,
		// Token: 0x040006C0 RID: 1728
		OAuthNotAvailable = 4000000
	}
}
