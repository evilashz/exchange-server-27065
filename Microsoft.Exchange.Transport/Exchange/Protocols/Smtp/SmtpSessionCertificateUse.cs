using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003FC RID: 1020
	internal enum SmtpSessionCertificateUse
	{
		// Token: 0x040016FB RID: 5883
		DirectTrust,
		// Token: 0x040016FC RID: 5884
		STARTTLS,
		// Token: 0x040016FD RID: 5885
		RemoteDirectTrust,
		// Token: 0x040016FE RID: 5886
		RemoteSTARTTLS
	}
}
