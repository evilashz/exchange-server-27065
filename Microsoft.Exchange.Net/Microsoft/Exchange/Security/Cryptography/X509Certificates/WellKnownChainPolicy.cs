using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AEB RID: 2795
	internal enum WellKnownChainPolicy
	{
		// Token: 0x040034DF RID: 13535
		Base = 1,
		// Token: 0x040034E0 RID: 13536
		Authenticode,
		// Token: 0x040034E1 RID: 13537
		AuthenticodeTS,
		// Token: 0x040034E2 RID: 13538
		SSL,
		// Token: 0x040034E3 RID: 13539
		BasicConstraints,
		// Token: 0x040034E4 RID: 13540
		NTAuthorization,
		// Token: 0x040034E5 RID: 13541
		MicrosoftRoot
	}
}
