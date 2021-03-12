using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A81 RID: 2689
	[Flags]
	internal enum CertificateSelectionOption
	{
		// Token: 0x0400324C RID: 12876
		None = 0,
		// Token: 0x0400324D RID: 12877
		WildcardAllowed = 1,
		// Token: 0x0400324E RID: 12878
		PreferedNonSelfSigned = 2
	}
}
