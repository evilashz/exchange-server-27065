using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A86 RID: 2694
	[Flags]
	internal enum ChainBuildOptions : uint
	{
		// Token: 0x04003265 RID: 12901
		CacheEndCert = 1U,
		// Token: 0x04003266 RID: 12902
		CacheOnlyUrlRetrieval = 4U,
		// Token: 0x04003267 RID: 12903
		RevocationCheckEndCert = 268435456U,
		// Token: 0x04003268 RID: 12904
		RevocationCheckChain = 536870912U,
		// Token: 0x04003269 RID: 12905
		RevocationCheckChainExcludeRoot = 1073741824U,
		// Token: 0x0400326A RID: 12906
		RevocationCheckCacheOnly = 2147483648U,
		// Token: 0x0400326B RID: 12907
		RevocationAccumulativeTimeout = 134217728U,
		// Token: 0x0400326C RID: 12908
		DisablePass1QualityFiltering = 64U,
		// Token: 0x0400326D RID: 12909
		ReturnLowerQualityContexts = 128U,
		// Token: 0x0400326E RID: 12910
		DisableAuthRootAutoUpdate = 256U,
		// Token: 0x0400326F RID: 12911
		TimestampTime = 512U,
		// Token: 0x04003270 RID: 12912
		DisableAia = 8192U
	}
}
