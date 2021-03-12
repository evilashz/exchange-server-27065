using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200039D RID: 925
	[Flags]
	internal enum AuthenticationMethodFlags
	{
		// Token: 0x0400199B RID: 6555
		None = 0,
		// Token: 0x0400199C RID: 6556
		Basic = 1,
		// Token: 0x0400199D RID: 6557
		Ntlm = 2,
		// Token: 0x0400199E RID: 6558
		Fba = 4,
		// Token: 0x0400199F RID: 6559
		Digest = 8,
		// Token: 0x040019A0 RID: 6560
		WindowsIntegrated = 16,
		// Token: 0x040019A1 RID: 6561
		LiveIdFba = 32,
		// Token: 0x040019A2 RID: 6562
		LiveIdBasic = 64,
		// Token: 0x040019A3 RID: 6563
		WSSecurity = 128,
		// Token: 0x040019A4 RID: 6564
		Certificate = 256,
		// Token: 0x040019A5 RID: 6565
		NegoEx = 512,
		// Token: 0x040019A6 RID: 6566
		OAuth = 1024,
		// Token: 0x040019A7 RID: 6567
		Adfs = 2048,
		// Token: 0x040019A8 RID: 6568
		Kerberos = 4096,
		// Token: 0x040019A9 RID: 6569
		Negotiate = 8192,
		// Token: 0x040019AA RID: 6570
		LiveIdNegotiate = 16384
	}
}
