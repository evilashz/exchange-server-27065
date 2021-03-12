using System;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200000C RID: 12
	public enum AuthenticationType
	{
		// Token: 0x0400001C RID: 28
		Unknown,
		// Token: 0x0400001D RID: 29
		LiveIdBasic,
		// Token: 0x0400001E RID: 30
		LiveIdNego2,
		// Token: 0x0400001F RID: 31
		Certificate,
		// Token: 0x04000020 RID: 32
		CertificateLinkedUser,
		// Token: 0x04000021 RID: 33
		OAuth,
		// Token: 0x04000022 RID: 34
		Kerberos,
		// Token: 0x04000023 RID: 35
		RemotePowerShellDelegated
	}
}
