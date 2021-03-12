using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A7E RID: 2686
	[Flags]
	internal enum CertificateCreationOption
	{
		// Token: 0x0400323F RID: 12863
		None = 0,
		// Token: 0x04003240 RID: 12864
		Default = 0,
		// Token: 0x04003241 RID: 12865
		RSAProvider = 1,
		// Token: 0x04003242 RID: 12866
		DSSProvider = 2,
		// Token: 0x04003243 RID: 12867
		Exportable = 4,
		// Token: 0x04003244 RID: 12868
		Archivable = 8
	}
}
