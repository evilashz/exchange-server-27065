using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AC8 RID: 2760
	internal enum ExchangeCertificateValidity
	{
		// Token: 0x040035B7 RID: 13751
		Valid,
		// Token: 0x040035B8 RID: 13752
		PrivateKeyMissing,
		// Token: 0x040035B9 RID: 13753
		KeyAlgorithmUnsupported,
		// Token: 0x040035BA RID: 13754
		SigningNotSupported,
		// Token: 0x040035BB RID: 13755
		PkixKpServerAuthNotFoundInEnhancedKeyUsage,
		// Token: 0x040035BC RID: 13756
		PrivateKeyNotAccessible,
		// Token: 0x040035BD RID: 13757
		PrivateKeyUnsupportedAlgorithm,
		// Token: 0x040035BE RID: 13758
		CspKeyContainerInfoProtected,
		// Token: 0x040035BF RID: 13759
		CspKeyContainerInfoRemovableDevice,
		// Token: 0x040035C0 RID: 13760
		CspKeyContainerInfoNotAccessible,
		// Token: 0x040035C1 RID: 13761
		CspKeyContainerInfoUnknownKeyNumber,
		// Token: 0x040035C2 RID: 13762
		PublicKeyUnsupportedSize,
		// Token: 0x040035C3 RID: 13763
		KeyUsageCorrupted,
		// Token: 0x040035C4 RID: 13764
		EnhancedKeyUsageCorrupted
	}
}
