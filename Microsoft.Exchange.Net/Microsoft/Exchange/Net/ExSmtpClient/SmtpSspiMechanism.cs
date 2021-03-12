using System;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020006FF RID: 1791
	[Flags]
	internal enum SmtpSspiMechanism
	{
		// Token: 0x04002042 RID: 8258
		Login = 1,
		// Token: 0x04002043 RID: 8259
		Ntlm = 2,
		// Token: 0x04002044 RID: 8260
		Gssapi = 4,
		// Token: 0x04002045 RID: 8261
		TLS = 8,
		// Token: 0x04002046 RID: 8262
		None = 16,
		// Token: 0x04002047 RID: 8263
		Kerberos = 32
	}
}
