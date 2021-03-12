using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AF7 RID: 2807
	internal enum RevocationStatus : uint
	{
		// Token: 0x0400351F RID: 13599
		Valid,
		// Token: 0x04003520 RID: 13600
		Revoked = 4U,
		// Token: 0x04003521 RID: 13601
		Unknown = 64U
	}
}
