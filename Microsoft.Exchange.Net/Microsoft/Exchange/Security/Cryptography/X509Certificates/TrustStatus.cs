using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AEA RID: 2794
	[Flags]
	internal enum TrustStatus : uint
	{
		// Token: 0x040034C7 RID: 13511
		Valid = 0U,
		// Token: 0x040034C8 RID: 13512
		IsNotTimeValid = 1U,
		// Token: 0x040034C9 RID: 13513
		IsNotTimeNested = 2U,
		// Token: 0x040034CA RID: 13514
		IsRevoked = 4U,
		// Token: 0x040034CB RID: 13515
		IsNotSignatureValid = 8U,
		// Token: 0x040034CC RID: 13516
		IsNotValidForUsage = 16U,
		// Token: 0x040034CD RID: 13517
		IsUntrustedRoot = 32U,
		// Token: 0x040034CE RID: 13518
		X509RevocationStatusUnknown = 64U,
		// Token: 0x040034CF RID: 13519
		IsCyclic = 128U,
		// Token: 0x040034D0 RID: 13520
		InvalidExtension = 256U,
		// Token: 0x040034D1 RID: 13521
		InvalidPolicyConstraints = 512U,
		// Token: 0x040034D2 RID: 13522
		InvalidBasicConstraints = 1024U,
		// Token: 0x040034D3 RID: 13523
		InvalidNameConstraints = 2048U,
		// Token: 0x040034D4 RID: 13524
		HasNotSupportedNameConstraint = 4096U,
		// Token: 0x040034D5 RID: 13525
		HasNotDefinedNameConstraint = 8192U,
		// Token: 0x040034D6 RID: 13526
		HasNotPermittedNameConstraint = 16384U,
		// Token: 0x040034D7 RID: 13527
		HasExcludedNameConstraint = 32768U,
		// Token: 0x040034D8 RID: 13528
		IsOfflineRevocation = 16777216U,
		// Token: 0x040034D9 RID: 13529
		NoIssuanceChainPolicy = 33554432U,
		// Token: 0x040034DA RID: 13530
		IsPartialChain = 65536U,
		// Token: 0x040034DB RID: 13531
		CTLIsNotTimeValid = 131072U,
		// Token: 0x040034DC RID: 13532
		CTLIsNotSignatureValid = 262144U,
		// Token: 0x040034DD RID: 13533
		CTLIsNotValidForUsage = 524288U
	}
}
