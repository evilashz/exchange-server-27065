using System;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000013 RID: 19
	internal enum Pop3AuthType
	{
		// Token: 0x04000087 RID: 135
		Basic,
		// Token: 0x04000088 RID: 136
		Ntlm,
		// Token: 0x04000089 RID: 137
		SSL = 16,
		// Token: 0x0400008A RID: 138
		NtlmOverSSL,
		// Token: 0x0400008B RID: 139
		TLS = 32,
		// Token: 0x0400008C RID: 140
		NtlmOverTLS
	}
}
