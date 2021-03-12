using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AE9 RID: 2793
	[Flags]
	internal enum TrustInformation : uint
	{
		// Token: 0x040034BD RID: 13501
		None = 0U,
		// Token: 0x040034BE RID: 13502
		HasExactMatchIssuer = 1U,
		// Token: 0x040034BF RID: 13503
		HasKeyMatchIssuer = 2U,
		// Token: 0x040034C0 RID: 13504
		HasNameMatchIssuer = 4U,
		// Token: 0x040034C1 RID: 13505
		IsSelfSigned = 8U,
		// Token: 0x040034C2 RID: 13506
		HasPreferredIssuer = 256U,
		// Token: 0x040034C3 RID: 13507
		HasIssuanceChainPolicy = 512U,
		// Token: 0x040034C4 RID: 13508
		HasValidNameConstraints = 1024U,
		// Token: 0x040034C5 RID: 13509
		IsComplexChain = 65536U
	}
}
