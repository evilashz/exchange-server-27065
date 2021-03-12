using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000069 RID: 105
	internal enum RequiredTlsAuthLevel
	{
		// Token: 0x040001C0 RID: 448
		EncryptionOnly = 1,
		// Token: 0x040001C1 RID: 449
		CertificateValidation,
		// Token: 0x040001C2 RID: 450
		DomainValidation
	}
}
