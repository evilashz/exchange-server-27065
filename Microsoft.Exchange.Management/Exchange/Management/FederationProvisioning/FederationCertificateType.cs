using System;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000332 RID: 818
	[Flags]
	internal enum FederationCertificateType
	{
		// Token: 0x04000C23 RID: 3107
		PreviousCertificate = 1,
		// Token: 0x04000C24 RID: 3108
		CurrentCertificate = 2,
		// Token: 0x04000C25 RID: 3109
		NextCertificate = 4
	}
}
