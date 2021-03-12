using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A95 RID: 2709
	[Flags]
	internal enum SSLPolicyAuthorizationOptions : uint
	{
		// Token: 0x040032BA RID: 12986
		None = 0U,
		// Token: 0x040032BB RID: 12987
		IgnoreRevocation = 128U,
		// Token: 0x040032BC RID: 12988
		IgnoreUnknownCA = 256U,
		// Token: 0x040032BD RID: 12989
		IgnoreWrongUsage = 512U,
		// Token: 0x040032BE RID: 12990
		IgnoreCertCNInvalid = 4096U,
		// Token: 0x040032BF RID: 12991
		IgnoreCertDateInvalid = 8192U
	}
}
