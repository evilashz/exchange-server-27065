using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200039E RID: 926
	public enum AuthenticationMethod
	{
		// Token: 0x040019AC RID: 6572
		Basic,
		// Token: 0x040019AD RID: 6573
		Digest,
		// Token: 0x040019AE RID: 6574
		Ntlm,
		// Token: 0x040019AF RID: 6575
		Fba,
		// Token: 0x040019B0 RID: 6576
		WindowsIntegrated,
		// Token: 0x040019B1 RID: 6577
		LiveIdFba,
		// Token: 0x040019B2 RID: 6578
		LiveIdBasic,
		// Token: 0x040019B3 RID: 6579
		WSSecurity,
		// Token: 0x040019B4 RID: 6580
		Certificate,
		// Token: 0x040019B5 RID: 6581
		NegoEx,
		// Token: 0x040019B6 RID: 6582
		OAuth,
		// Token: 0x040019B7 RID: 6583
		Adfs,
		// Token: 0x040019B8 RID: 6584
		Kerberos,
		// Token: 0x040019B9 RID: 6585
		Negotiate,
		// Token: 0x040019BA RID: 6586
		LiveIdNegotiate,
		// Token: 0x040019BB RID: 6587
		Misconfigured = 1000
	}
}
