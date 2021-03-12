using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000924 RID: 2340
	[CLSCompliant(false)]
	public enum AuthenticationService : uint
	{
		// Token: 0x04002B92 RID: 11154
		None,
		// Token: 0x04002B93 RID: 11155
		DcePrivate,
		// Token: 0x04002B94 RID: 11156
		DcePublic,
		// Token: 0x04002B95 RID: 11157
		DecPublic = 4U,
		// Token: 0x04002B96 RID: 11158
		Negotiate = 9U,
		// Token: 0x04002B97 RID: 11159
		Ntlm,
		// Token: 0x04002B98 RID: 11160
		SecureChannel = 14U,
		// Token: 0x04002B99 RID: 11161
		Kerberos = 16U,
		// Token: 0x04002B9A RID: 11162
		Dpa,
		// Token: 0x04002B9B RID: 11163
		Msn,
		// Token: 0x04002B9C RID: 11164
		Kernel = 20U,
		// Token: 0x04002B9D RID: 11165
		Digest,
		// Token: 0x04002B9E RID: 11166
		NegoExtender = 30U,
		// Token: 0x04002B9F RID: 11167
		Pku2u,
		// Token: 0x04002BA0 RID: 11168
		Live,
		// Token: 0x04002BA1 RID: 11169
		MicrosoftOnline = 82U,
		// Token: 0x04002BA2 RID: 11170
		MessageQueue = 100U,
		// Token: 0x04002BA3 RID: 11171
		Default = 4294967295U
	}
}
